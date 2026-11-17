using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Helper;
using System;
using System.ComponentModel;

namespace DrivingCenter.App.UserControl
{
    /// <summary>
    /// Thẻ hiển thị một máy cabin trên Uc_CabinManager: trạng thái kết nối, học viên được xếp
    /// vào máy và lịch sử kết nối.
    /// </summary>
    public partial class UcCabinClientCard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BindingList<CabinRunLog> _logs = new BindingList<CabinRunLog>();

        public event EventHandler UnassignRequested;

        public CabinClientInfo Client { get; private set; } = new CabinClientInfo();

        public UcCabinClientCard()
        {
            InitializeComponent();
            ApplyTheme();
            gridControl_Log.DataSource = _logs;
            col_ThoiGian.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        }

        private void ApplyTheme()
        {
            UiTheme.SetIcon(btn_Unassign, "svgimages/icon%20builder/actions_removecircled.svg", 16);
            gridView_Log.Appearance.HeaderPanel.ForeColor = UiTheme.Brand;
            gridView_Log.Appearance.HeaderPanel.Options.UseForeColor = true;
        }

        public void Bind(CabinClientInfo client)
        {
            Client = client;
            grp_Cabin.Text = client.Name.ToUpper();
            ShowHocVien(client.HocVien);
            SetOnline(client.Status == CabinClientStatus.Online, client.IpAddress, client.MachineName);
        }

        public void AssignHocVien(HocVien hocVien)
        {
            Client.HocVien = hocVien;
            ShowHocVien(hocVien);
            if (hocVien != null)
                AddLog($"Xếp học viên (CCCD {hocVien.SoCMT})");
        }

        private void ShowHocVien(HocVien hocVien)
        {
            lbl_HocVien.Text = hocVien?.HoTenHV ?? "Chưa gán học viên";
            lbl_HocVien.Appearance.ForeColor = hocVien == null ? UiTheme.TextMuted : UiTheme.BrandDark;
            lbl_HocVien.Appearance.Options.UseForeColor = true;
            lbl_Cccd.Text = "CCCD: " + (string.IsNullOrEmpty(hocVien?.SoCMT) ? "—" : hocVien.SoCMT);
            btn_Unassign.Enabled = hocVien != null;
        }

        public void SetOnline(bool online, string ip, string machineName)
        {
            Client.Status = online ? CabinClientStatus.Online : CabinClientStatus.Offline;
            if (online)
            {
                Client.IpAddress = ip;
                Client.MachineName = machineName;
                Client.LastSeen = DateTime.Now;
                UiTheme.SetStatusText(lbl_Status, "Đang kết nối", UiTheme.Success);
            }
            else
            {
                UiTheme.SetStatusText(lbl_Status, "Mất kết nối", UiTheme.TextMuted);
            }

            lbl_Ip.Text = string.IsNullOrEmpty(Client.IpAddress)
                ? "—"
                : Client.IpAddress + (string.IsNullOrEmpty(Client.MachineName) ? "" : "  ·  " + Client.MachineName);
            ShowLastSeen();
        }

        public void SetLastSeen(DateTime time)
        {
            Client.LastSeen = time;
            ShowLastSeen();
        }

        private void ShowLastSeen()
        {
            lbl_LastSeen.Text = Client.LastSeen.HasValue
                ? "Tín hiệu cuối: " + Client.LastSeen.Value.ToString("HH:mm:ss")
                : "Chưa nhận tín hiệu";
        }

        public void AddLog(string suKien)
        {
            _logs.Add(new CabinRunLog
            {
                ThoiGian = DateTime.Now,
                HocVien = Client.HocVien?.HoTenHV ?? string.Empty,
                SuKien = suKien
            });
        }

        private void btn_Unassign_Click(object sender, EventArgs e) => UnassignRequested?.Invoke(this, EventArgs.Empty);
    }
}
