using DrivingCenter.App.Helper;
using DrivingCenter.App.Network;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    /// <summary>
    /// Chạy ngầm trên máy cabin: giữ kết nối tới máy quản lý và hiện một nhãn nhỏ ở góc màn hình
    /// cho biết trạng thái kết nối và học viên được xếp vào máy này.
    /// Nhãn không nhận chuột/không lấy focus để không cản phần mềm học.
    /// </summary>
    public partial class frmCabinClient : DevExpress.XtraEditors.XtraForm
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        private static readonly Color OnlineColor = Color.FromArgb(74, 222, 128);

        private readonly int _cabinIndex = CabinSettings.CabinIndex;
        private CabinClientConnection _connection;
        private bool _online;
        private string _assigned;

        public frmCabinClient()
        {
            InitializeComponent();
        }

        // Không lấy focus khỏi phần mềm học khi hiện lên
        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                // Click xuyên qua, ẩn khỏi Alt+Tab
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE;
                return cp;
            }
        }

        private void frmCabinClient_Load(object sender, EventArgs e)
        {
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(area.Right - Width - 16, area.Bottom - Height - 16);
            UpdateView();

            _connection = new CabinClientConnection(CabinSettings.ManagerHost, CabinSettings.Port, _cabinIndex);
            _connection.Connected += () => OnUi(() => { _online = true; UpdateView(); });
            _connection.Disconnected += () => OnUi(() => { _online = false; UpdateView(); });
            _connection.MessageReceived += msg => OnUi(() => HandleMessage(msg));
            _connection.Start();

            FormClosed += (s, ev) => _connection.Dispose();
        }

        private void HandleMessage(CabinMessage msg)
        {
            switch (msg.Type)
            {
                case CabinMessage.Assign:
                    _assigned = string.IsNullOrEmpty(msg.HangGPLX) ? msg.HoTen : $"{msg.HoTen} · Hạng {msg.HangGPLX}";
                    break;
                case CabinMessage.Clear:
                    _assigned = null;
                    break;
            }
            UpdateView();
        }

        private void UpdateView()
        {
            if (_online)
            {
                lbl_Running.Text = $"●  CABIN {_cabinIndex} · ONLINE";
                lbl_Running.Appearance.ForeColor = OnlineColor;
                lbl_Detail.Text = _assigned == null ? "Chưa có học viên" : "Học viên: " + _assigned;
            }
            else
            {
                lbl_Running.Text = $"●  CABIN {_cabinIndex} · MẤT KẾT NỐI";
                lbl_Running.Appearance.ForeColor = UiTheme.Danger;
                lbl_Detail.Text = $"Đang kết nối {CabinSettings.ManagerHost}:{CabinSettings.Port}...";
            }
        }

        private void OnUi(Action action)
        {
            if (IsDisposed) return;
            if (InvokeRequired) BeginInvoke(action);
            else action();
        }
    }
}
