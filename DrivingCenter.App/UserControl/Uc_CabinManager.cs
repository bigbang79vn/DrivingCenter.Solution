using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace DrivingCenter.App.UserControl
{
    /// <summary>
    /// Trang theo dõi 4 máy cabin: mở cổng TCP cho các máy cabin kết nối tới,
    /// hiển thị trạng thái kết nối và xếp học viên vào từng máy.
    /// </summary>
    public partial class Uc_CabinManager : DevExpress.XtraEditors.XtraUserControl
    {
        private const int CabinCount = 4;

        private readonly CabinEco_DAO _DAO = new CabinEco_DAO();
        private UcCabinClientCard[] _cards;
        private CabinServer _server;

        public Uc_CabinManager()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pnl_Header.Paint += UiTheme.PaintHeader;

            UiTheme.StylePrimary(btn_Assign);
            UiTheme.SetIcon(btn_Assign, "svgimages/arrows/doublenext.svg", 16);
            UiTheme.SetIcon(btn_AutoAssign, "svgimages/arrows/movedown.svg", 16);
            UiTheme.SetIcon(btn_Refresh, "svgimages/icon%20builder/actions_refresh.svg", 16);

            UiTheme.StyleGrid(gridView_HocVien, "Tìm theo tên, CCCD...");
            gridView_HocVien.CustomUnboundColumnData += gridView_HocVien_CustomUnboundColumnData;
            gridView_HocVien.CustomDrawEmptyForeground += (s, e) =>
                UiTheme.DrawEmptyState(e, "Chưa có học viên", "Chọn khóa học để hiển thị danh sách");
        }

        private void Uc_CabinManager_Load(object sender, EventArgs e)
        {
            _cards = new[] { card_Cabin1, card_Cabin2, card_Cabin3, card_Cabin4 };
            for (int i = 0; i < _cards.Length; i++)
            {
                var card = _cards[i];
                card.Bind(new CabinClientInfo
                {
                    Index = i + 1,
                    Name = "Cabin " + (i + 1),
                    Status = CabinClientStatus.Offline
                });
                card.UnassignRequested += Card_UnassignRequested;
                cbo_Cabin.Properties.Items.Add(card.Client.Name);
            }
            cbo_Cabin.SelectedIndex = 0;

            StartServer();
            LoadKhoaHoc();
        }

        #region Kết nối máy cabin

        private void StartServer()
        {
            _server = new CabinServer(CabinSettings.Port);
            _server.ClientConnected += (index, ip, machine) => OnUi(() => Server_ClientConnected(index, ip, machine));
            _server.ClientSeen += (index, time) => OnUi(() => GetCard(index)?.SetLastSeen(time));
            _server.ClientDisconnected += index => OnUi(() => Server_ClientDisconnected(index));

            try
            {
                _server.Start();
                Disposed += (s, e) => _server.Dispose();
                UpdateServerStatus();
            }
            catch (SocketException ex)
            {
                _server = null;
                UiTheme.SetStatusText(lbl_Server, $"Không mở được cổng {CabinSettings.Port}: {ex.Message}", UiTheme.Danger);
            }
        }

        private void Server_ClientConnected(int index, string ip, string machine)
        {
            var card = GetCard(index);
            if (card == null) return;

            card.SetOnline(true, ip, machine);
            card.AddLog($"Kết nối từ {ip} ({machine})");
            SendAssignment(card);
            UpdateServerStatus();
        }

        private void Server_ClientDisconnected(int index)
        {
            var card = GetCard(index);
            if (card == null) return;

            card.SetOnline(false, null, null);
            card.AddLog("Mất kết nối");
            UpdateServerStatus();
        }

        private void SendAssignment(UcCabinClientCard card)
        {
            if (_server == null || card.Client.Status != CabinClientStatus.Online) return;

            HocVien hv = card.Client.HocVien;
            _server.Send(card.Client.Index, hv == null
                ? new CabinMessage { Type = CabinMessage.Clear }
                : new CabinMessage { Type = CabinMessage.Assign, HoTen = hv.HoTenHV, HangGPLX = hv.HangGPLX });
        }

        private void UpdateServerStatus()
        {
            int online = _cards.Count(c => c.Client.Status == CabinClientStatus.Online);
            UiTheme.SetStatusText(lbl_Server, $"Cổng {CabinSettings.Port}  ·  {online}/{CabinCount} máy đang kết nối",
                online > 0 ? UiTheme.Success : UiTheme.TextMuted);
        }

        private UcCabinClientCard GetCard(int index) =>
            index >= 1 && index <= _cards.Length ? _cards[index - 1] : null;

        private void OnUi(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            BeginInvoke(action);
        }

        #endregion

        #region Học viên

        private void LoadKhoaHoc()
        {
            lookUp_KhoaHoc.Properties.DataSource = _DAO.GetAllKhoaHoc();
        }

        private void LoadHocVien()
        {
            if (lookUp_KhoaHoc.EditValue == null)
            {
                gridControl_HocVien.DataSource = null;
                grp_HocVien.Text = "DANH SÁCH HỌC VIÊN";
                return;
            }

            List<HocVien> lst = _DAO.LoadLstHocVienById(Convert.ToInt32(lookUp_KhoaHoc.EditValue));
            gridControl_HocVien.DataSource = lst;
            GridHelper.EnableRowNumbering(gridView_HocVien);
            grp_HocVien.Text = $"DANH SÁCH HỌC VIÊN  ·  {lst.Count} học viên";
        }

        private void lookUp_KhoaHoc_EditValueChanged(object sender, EventArgs e) => LoadHocVien();

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadKhoaHoc();
            LoadHocVien();
        }

        private void gridView_HocVien_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column != col_Cabin || !e.IsGetData) return;
            var hv = e.Row as HocVien;
            e.Value = FindCardOf(hv)?.Client.Name ?? string.Empty;
        }

        private UcCabinClientCard FindCardOf(HocVien hv)
        {
            if (hv == null || _cards == null) return null;
            return _cards.FirstOrDefault(c => c.Client.HocVien != null && c.Client.HocVien.SoCMT == hv.SoCMT);
        }

        private void AssignToCard(HocVien hv, UcCabinClientCard card)
        {
            if (hv == null || card == null) return;

            var current = FindCardOf(hv);
            if (current != null && current != card)
            {
                XtraMessageBox.Show($"Học viên {hv.HoTenHV} đã được xếp vào {current.Client.Name}.", "Thông báo");
                return;
            }

            card.AssignHocVien(hv);
            SendAssignment(card);
            gridView_HocVien.RefreshData();
        }

        private void btn_Assign_Click(object sender, EventArgs e)
        {
            AssignToCard(gridView_HocVien.GetFocusedRow() as HocVien, _cards[cbo_Cabin.SelectedIndex]);
        }

        private void gridView_HocVien_DoubleClick(object sender, EventArgs e) => btn_Assign_Click(sender, e);

        private void btn_AutoAssign_Click(object sender, EventArgs e)
        {
            var selected = gridView_HocVien.GetSelectedRows()
                .Where(h => h >= 0)
                .Select(h => gridView_HocVien.GetRow(h) as HocVien)
                .Where(hv => hv != null && FindCardOf(hv) == null)
                .ToList();
            var freeCards = _cards.Where(c => c.Client.HocVien == null).ToList();

            if (selected.Count == 0 || freeCards.Count == 0)
            {
                XtraMessageBox.Show("Không có học viên chưa xếp hoặc không còn cabin trống.", "Thông báo");
                return;
            }

            for (int i = 0; i < Math.Min(selected.Count, freeCards.Count); i++)
                AssignToCard(selected[i], freeCards[i]);
        }

        private void Card_UnassignRequested(object sender, EventArgs e)
        {
            var card = (UcCabinClientCard)sender;
            card.AddLog("Bỏ xếp học viên");
            card.AssignHocVien(null);
            SendAssignment(card);
            gridView_HocVien.RefreshData();
        }

        #endregion
    }
}
