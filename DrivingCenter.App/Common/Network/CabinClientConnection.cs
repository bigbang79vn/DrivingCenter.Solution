using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrivingCenter.App.Network
{
    /// <summary>
    /// Kết nối từ máy cabin tới máy quản lý: tự kết nối lại khi mất mạng, gửi PING định kỳ.
    /// Sự kiện được phát từ luồng nền, giao diện phải tự Invoke.
    /// </summary>
    public class CabinClientConnection : IDisposable
    {
        public static readonly TimeSpan PingInterval = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan ReconnectDelay = TimeSpan.FromSeconds(5);

        private readonly string _host;
        private readonly int _port;
        private readonly int _cabinIndex;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private TcpClient _tcp;

        public event Action Connected;
        public event Action Disconnected;
        public event Action<CabinMessage> MessageReceived;

        public CabinClientConnection(string host, int port, int cabinIndex)
        {
            _host = host;
            _port = port;
            _cabinIndex = cabinIndex;
        }

        public void Start() => Task.Run(() => RunAsync(_cts.Token));

        private async Task RunAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                bool wasConnected = false;
                try
                {
                    _tcp = new TcpClient { NoDelay = true };
                    await _tcp.ConnectAsync(_host, _port);

                    var stream = _tcp.GetStream();
                    var reader = new StreamReader(stream, new UTF8Encoding(false));
                    var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true, NewLine = "\n" };
                    var writeLock = new SemaphoreSlim(1, 1);

                    await SendAsync(writer, writeLock, new CabinMessage
                    {
                        Type = CabinMessage.Hello,
                        CabinIndex = _cabinIndex,
                        MachineName = Environment.MachineName
                    });
                    wasConnected = true;
                    Connected?.Invoke();

                    using (var pingCts = CancellationTokenSource.CreateLinkedTokenSource(token))
                    {
                        Task ping = PingLoopAsync(writer, writeLock, pingCts.Token);

                        string line;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            CabinMessage msg = CabinMessage.FromLine(line);
                            if (msg != null)
                                MessageReceived?.Invoke(msg);
                        }

                        pingCts.Cancel();
                        await ping;
                    }
                }
                catch (Exception ex) when (ex is IOException || ex is SocketException || ex is ObjectDisposedException || ex is InvalidOperationException)
                {
                    // Không kết nối được hoặc mất kết nối: thử lại sau
                }
                finally
                {
                    _tcp?.Close();
                }

                if (wasConnected)
                    Disconnected?.Invoke();

                try { await Task.Delay(ReconnectDelay, token); }
                catch (TaskCanceledException) { return; }
            }
        }

        private async Task PingLoopAsync(StreamWriter writer, SemaphoreSlim writeLock, CancellationToken token)
        {
            var ping = new CabinMessage { Type = CabinMessage.Ping, CabinIndex = _cabinIndex };
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(PingInterval, token);
                    await SendAsync(writer, writeLock, ping);
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception ex) when (ex is IOException || ex is ObjectDisposedException)
            {
                // Gửi lỗi: đóng socket để vòng đọc thoát và kết nối lại
                _tcp?.Close();
            }
        }

        private static async Task SendAsync(StreamWriter writer, SemaphoreSlim writeLock, CabinMessage message)
        {
            await writeLock.WaitAsync();
            try { await writer.WriteLineAsync(message.ToLine()); }
            finally { writeLock.Release(); }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _tcp?.Close();
        }
    }
}
