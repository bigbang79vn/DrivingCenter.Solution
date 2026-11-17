using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrivingCenter.App.Network
{
    /// <summary>
    /// Máy chủ TCP chạy trên máy quản lý. Các máy cabin tự kết nối tới, gửi HELLO kèm số cabin
    /// rồi gửi PING định kỳ. Sự kiện được phát từ luồng nền, giao diện phải tự Invoke.
    /// </summary>
    public class CabinServer : IDisposable
    {
        public static readonly TimeSpan HeartbeatTimeout = TimeSpan.FromSeconds(20);

        private class Session
        {
            public int Index;
            public TcpClient Tcp;
            public StreamWriter Writer;
            public DateTime LastSeen;
            public readonly object WriteLock = new object();
        }

        private readonly int _port;
        private readonly ConcurrentDictionary<int, Session> _sessions = new ConcurrentDictionary<int, Session>();
        private TcpListener _listener;
        private Timer _watchdog;
        private CancellationTokenSource _cts;

        public event Action<int, string, string> ClientConnected;   // số cabin, IP, tên máy
        public event Action<int, DateTime> ClientSeen;              // số cabin, thời điểm PING
        public event Action<int> ClientDisconnected;

        public CabinServer(int port)
        {
            _port = port;
        }

        public int Port => _port;

        public void Start()
        {
            _cts = new CancellationTokenSource();
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _watchdog = new Timer(_ => DropStaleSessions(), null, 5000, 5000);
            Task.Run(() => AcceptLoopAsync(_cts.Token));
        }

        public bool IsConnected(int index) => _sessions.ContainsKey(index);

        public bool Send(int index, CabinMessage message)
        {
            if (!_sessions.TryGetValue(index, out Session session)) return false;
            try
            {
                lock (session.WriteLock)
                    session.Writer.WriteLine(message.ToLine());
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is ObjectDisposedException)
            {
                session.Tcp.Close();
                return false;
            }
        }

        private async Task AcceptLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                TcpClient tcp;
                try { tcp = await _listener.AcceptTcpClientAsync(); }
                catch (Exception) when (token.IsCancellationRequested) { return; }
                catch (SocketException) { continue; }

                _ = Task.Run(() => HandleClientAsync(tcp));
            }
        }

        private async Task HandleClientAsync(TcpClient tcp)
        {
            Session session = null;
            try
            {
                tcp.NoDelay = true;
                var stream = tcp.GetStream();
                var reader = new StreamReader(stream, new UTF8Encoding(false));
                var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true, NewLine = "\n" };

                // Gói đầu tiên phải là HELLO với số cabin hợp lệ
                CabinMessage hello = CabinMessage.FromLine(await reader.ReadLineAsync() ?? string.Empty);
                if (hello == null || hello.Type != CabinMessage.Hello || hello.CabinIndex < 1)
                {
                    tcp.Close();
                    return;
                }

                session = new Session { Index = hello.CabinIndex, Tcp = tcp, Writer = writer, LastSeen = DateTime.Now };

                // Máy cabin kết nối lại khi phiên cũ chưa hết hạn: bỏ phiên cũ
                _sessions.AddOrUpdate(session.Index, session, (_, old) => { old.Tcp.Close(); return session; });

                string ip = ((IPEndPoint)tcp.Client.RemoteEndPoint).Address.ToString();
                ClientConnected?.Invoke(session.Index, ip, hello.MachineName);

                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    CabinMessage msg = CabinMessage.FromLine(line);
                    if (msg?.Type == CabinMessage.Ping)
                    {
                        session.LastSeen = DateTime.Now;
                        ClientSeen?.Invoke(session.Index, session.LastSeen);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is ObjectDisposedException || ex is SocketException || ex is InvalidOperationException)
            {
                // Mất kết nối: xử lý chung ở finally
            }
            finally
            {
                tcp.Close();
                // Chỉ báo ngắt kết nối khi phiên này vẫn là phiên hiện tại của cabin
                if (session != null && ((ICollection<KeyValuePair<int, Session>>)_sessions)
                        .Remove(new KeyValuePair<int, Session>(session.Index, session)))
                    ClientDisconnected?.Invoke(session.Index);
            }
        }

        private void DropStaleSessions()
        {
            DateTime limit = DateTime.Now - HeartbeatTimeout;
            foreach (Session s in _sessions.Values.Where(s => s.LastSeen < limit))
                s.Tcp.Close();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _watchdog?.Dispose();
            _listener?.Stop();
            foreach (Session s in _sessions.Values)
                s.Tcp.Close();
        }
    }
}
