using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    /// <summary>
    /// Chi tiết một phiên học DAT: thông tin, ảnh đăng nhập/xuất, ảnh xác thực, hành trình GPS và bản đồ.
    /// Bố cục, chữ, màu nằm trong Designer; code chỉ tải dữ liệu, vẽ thẻ KPI và bản đồ.
    /// </summary>
    public partial class frmDetailSession : DevExpress.XtraEditors.XtraForm
    {
        private readonly PhienHocDAO _phienHocDao = new PhienHocDAO();
        private readonly IMG_DAO _IMG_DAO = new IMG_DAO();
        private readonly string _diemdanhnguoilxId;

        private ChiTiet _data;
        private bool _loading = true;
        private long NguoiLxID;
        private int KhoaHocID;

        private GMapOverlay _overlay;
        private GMarkerGoogle _cursor;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        private sealed class ChiTiet
        {
            public DataRow Info;
            public List<PointLatLng> Points;
            public List<ChiTietPhienHoc> History, Vehicle;
            public byte[] AnhLogin, AnhLogout;
        }

        public frmDetailSession(string diemdanhnguoilxId)
        {
            InitializeComponent();
            _diemdanhnguoilxId = diemdanhnguoilxId;
            pnl_Header.Paint += UiTheme.PaintHeader;
            FormClosed += (s, e) => { Img_Login.Image?.Dispose(); Img_Logout.Image?.Dispose(); };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) { Close(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void frmDetailSession_Load(object sender, EventArgs e)
        {
            InitMap();
            gridView_HistoryData.ShowLoadingPanel();
            gridView_VehicleHistory.ShowLoadingPanel();

            ChiTiet d;
            try
            {
                string id = _diemdanhnguoilxId;
                d = await Task.Run(() =>
                {
                    var info = _phienHocDao.GetDetailSession(id);
                    var img = _IMG_DAO.GetImgById(id);
                    var r = new ChiTiet
                    {
                        Info = info.Rows.Count > 0 ? info.Rows[0] : null,
                        Points = _phienHocDao.GetlstPoint(id),
                        History = _phienHocDao.GetHistoryData(id),
                        Vehicle = _phienHocDao.GetVehicleHistory(id),
                        AnhLogin = img.Rows.Count > 0 ? img.Rows[0]["AnhChup"] as byte[] : null,
                        AnhLogout = img.Rows.Count > 0 ? img.Rows[0]["AnhChup_Logout"] as byte[] : null
                    };
                    foreach (var item in r.History.Concat(r.Vehicle))
                        item.ThoiGianText = GioPhutGiay(item.ThoiGian);
                    return r;
                });
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                gridView_HistoryData.HideLoadingPanel();
                gridView_VehicleHistory.HideLoadingPanel();
                lb_Sub.Text = "Không tải được dữ liệu phiên.";
                XtraMessageBox.Show($"Không tải được chi tiết phiên học.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsAlive) return;

            _data = d;
            _loading = false;
            ShowInfo(d.Info);
            ShowImages(d.AnhLogin, d.AnhLogout);

            gridControl_HistoryData.DataSource = d.History;
            gridControl_VehicleHistory.DataSource = d.Vehicle;
            GridHelper.EnableRowNumbering(gridView_HistoryData, 40);
            GridHelper.EnableRowNumbering(gridView_VehicleHistory, 45);
            gridView_HistoryData.HideLoadingPanel();
            gridView_VehicleHistory.HideLoadingPanel();
            tab_XacThuc.Text = $"Ảnh xác thực ({d.History.Count})";
            tab_HanhTrinh.Text = $"Hành trình GPS ({d.Vehicle.Count})";

            DrawRoute(d);
            pnl_Kpi.Invalidate();
        }

        /// <summary>Giờ:phút:giây, giờ có thể vượt 24 (trước đây dùng t.Hours nên mất phần ngày).</summary>
        private static string GioPhutGiay(double giay)
        {
            var t = TimeSpan.FromSeconds(Math.Max(0, giay));
            return $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}";
        }

        #region Thông tin & ảnh

        private DateTime? _tu, _den;      // _den chỉ có giá trị khi phiên đã đăng xuất thật (cột Logout = 1)
        private DateTime? _capNhat;       // phiên đang chạy: thiết bị vẫn ghi ThoiDiem_Logout = lần gửi dữ liệu gần nhất
        private bool _daDangXuat;
        private double _km, _giay;

        private void ShowInfo(DataRow r)
        {
            if (r == null)
            {
                lb_HocVien.Text = "Không tìm thấy phiên học";
                lb_Sub.Text = "Không có dữ liệu cho mã phiên này.";
                txt_MaPhien.Text = _diemdanhnguoilxId;
                return;
            }

            string S(string c) => r[c] == DBNull.Value ? "" : Convert.ToString(r[c]).Trim();
            _tu = r["ThoiDiem"] as DateTime?;
            // Trạng thái lấy theo cột Logout: phiên đang chạy vẫn có ThoiDiem_Logout (thiết bị cập nhật liên tục)
            // nên trước đây báo nhầm "Phiên đã kết thúc"
            _daDangXuat = r.Table.Columns.Contains("Logout") && r["Logout"] != DBNull.Value && Convert.ToBoolean(r["Logout"]);
            var tdLogout = r["ThoiDiem_Logout"] as DateTime?;
            _den = _daDangXuat ? tdLogout : null;
            _capNhat = _daDangXuat ? null : tdLogout;
            _km = r["TongQuangDuongLD_Phien"] == DBNull.Value ? 0 : Convert.ToDouble(r["TongQuangDuongLD_Phien"]);
            _giay = r["TongThoiGianLD_Phien"] == DBNull.Value ? 0 : Convert.ToDouble(r["TongThoiGianLD_Phien"]);
            NguoiLxID = Convert.ToInt64(r["NguoiLxID"]);
            KhoaHocID = Convert.ToInt32(r["KhoaHocID"]);

            lb_HocVien.Text = S("HoVaTen");
            string hang = S("HangXe");
            lb_Sub.Text = $"Khóa {S("TenKH")}   ·   GV {S("TenGV")}   ·   Xe {S("BienSo")}{(hang.Length > 0 ? $" ({hang})" : "")}";
            txt_MaPhien.Text = _diemdanhnguoilxId;
            Text = $"Chi tiết phiên học – {S("HoVaTen")}";
            lb_TrangThai.Text = _daDangXuat
                ? "<color=21,128,61>● Phiên đã kết thúc</color>"
                : "<color=217,119,6>● Phiên đang học (chưa đăng xuất)</color>";
        }

        private void ShowImages(byte[] login, byte[] logout)
        {
            var oldIn = Img_Login.Image;
            var oldOut = Img_Logout.Image;
            Img_Login.Image = login?.Length > 0 ? ImageExtension.byteArrayToImage(login) : null;
            Img_Logout.Image = logout?.Length > 0 ? ImageExtension.byteArrayToImage(logout) : null;
            oldIn?.Dispose();
            oldOut?.Dispose();
            lbl_Login.Text = "ẢNH ĐĂNG NHẬP" + (_tu.HasValue ? $"  ·  {_tu:HH:mm:ss}" : "");
            lbl_Logout.Text = "ẢNH ĐĂNG XUẤT" + (_den.HasValue ? $"  ·  {_den:HH:mm:ss}" : "");
        }

        private void pnl_Kpi_Resize(object sender, EventArgs e) => pnl_Kpi.Invalidate();

        private void pnl_Kpi_Paint(object sender, PaintEventArgs e)
        {
            string dash = "—";
            bool none = _loading || _data?.Info == null;
            int anh = _data?.History.Count ?? 0;
            int khop = _data?.History.Count(h => h.XacThuc) ?? 0;

            var cards = new[]
            {
                new UiTheme.KpiCard("QUÃNG ĐƯỜNG", none ? dash : $"{_km:n2} km",
                    none ? (_loading ? "Đang tải..." : "") : $"{_data.Vehicle.Count} điểm GPS", UiTheme.Brand),
                new UiTheme.KpiCard("THỜI GIAN HỌC", none ? dash : GioPhutGiay(_giay),
                    none ? "" : anh == 0 ? "Không có ảnh xác thực" : $"{khop}/{anh} ảnh nhận diện khớp", UiTheme.Success,
                    warn: !none && anh > 0 && khop < anh),
                new UiTheme.KpiCard("ĐĂNG NHẬP", none || !_tu.HasValue ? dash : $"{_tu:HH:mm:ss}",
                    none || !_tu.HasValue ? "" : $"{_tu:dd/MM/yyyy}", Color.FromArgb(99, 102, 241)),
                new UiTheme.KpiCard("ĐĂNG XUẤT", none || !_den.HasValue ? (none ? dash : "Đang học") : $"{_den:HH:mm:ss}",
                    none ? "" : _den.HasValue ? $"{_den:dd/MM/yyyy}"
                        : _capNhat.HasValue ? $"Chưa đăng xuất · dữ liệu mới nhất {_capNhat:HH:mm:ss}" : "Chưa đăng xuất",
                    UiTheme.Warning, warn: !none && !_den.HasValue),
            };
            UiTheme.DrawKpiCards(e.Graphics, pnl_Kpi.ClientRectangle, cards, UiTheme.SkinText(LookAndFeel));
        }

        private void btn_CopyMa_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(_diemdanhnguoilxId);
                DevExpress.Utils.ToolTipController.DefaultController.ShowHint("Đã sao chép mã phiên học", btn_CopyMa, DevExpress.Utils.ToolTipLocation.BottomLeft);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Không sao chép được.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Bản đồ

        private void InitMap()
        {
            gmapControl.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmapControl.ShowCenter = false;
            _overlay = new GMapOverlay("phien");
            gmapControl.Overlays.Add(_overlay);
        }

        private void DrawRoute(ChiTiet d)
        {
            var points = d.Points;
            if (points.Count == 0)
            {
                // Không có GPS: đặt bản đồ ở vị trí đăng nhập (nếu có) — trước đây _points[0] làm văng lỗi
                if (d.Info != null && d.Info["Lat"] != DBNull.Value && d.Info["Lng"] != DBNull.Value)
                {
                    var p = new PointLatLng(Convert.ToDouble(d.Info["Lat"]), Convert.ToDouble(d.Info["Lng"]));
                    if (p.Lat != 0 || p.Lng != 0)
                    {
                        _overlay.Markers.Add(Marker(p, GMarkerGoogleType.blue_dot, "Đăng nhập" + (_tu.HasValue ? $" {_tu:HH:mm:ss}" : "")));
                        gmapControl.Position = p;
                    }
                }
                tab_HanhTrinh.Text = "Hành trình GPS (không có dữ liệu)";
                return;
            }

            _overlay.Routes.Add(new GMapRoute(points, "hanhTrinh") { Stroke = new Pen(Color.FromArgb(220, 37, 99, 235), 4) });
            _overlay.Markers.Add(Marker(points[0], GMarkerGoogleType.blue_dot, "Bắt đầu" + (_tu.HasValue ? $" {_tu:HH:mm:ss}" : "")));
            _overlay.Markers.Add(Marker(points[points.Count - 1], GMarkerGoogleType.red_dot,
                _den.HasValue ? $"Kết thúc {_den:HH:mm:ss}" : "Điểm GPS cuối (chưa đăng xuất)"));

            // Vừa khung toàn bộ hành trình (trước đây chỉ đặt tâm ở điểm đầu với zoom cố định)
            gmapControl.ZoomAndCenterRoutes(_overlay.Id);
        }

        private static GMarkerGoogle Marker(PointLatLng p, GMarkerGoogleType type, string tip)
        {
            var m = new GMarkerGoogle(p, type) { ToolTipText = tip, ToolTipMode = MarkerTooltipMode.Always };
            m.ToolTip = new CustomToolTip(m);
            return m;
        }

        // Chọn dòng ở lưới nào cũng đánh dấu vị trí đó trên bản đồ
        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (!(view.GetRow(e.FocusedRowHandle) is ChiTietPhienHoc row) || (row.Lat == 0 && row.Lng == 0)) return;

            var p = new PointLatLng(row.Lat, row.Lng);
            if (_cursor == null)
            {
                _cursor = new GMarkerGoogle(p, GMarkerGoogleType.green) { ToolTipMode = MarkerTooltipMode.OnMouseOver };
                _overlay.Markers.Add(_cursor);
            }
            _cursor.Position = p;
            _cursor.ToolTipText = $"{row.ThoiDiem:HH:mm:ss} · {row.VanToc:n1} km/h";
            gmapControl.Position = p;
        }

        #endregion

        #region Lưới

        private void gridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if ((e.Column == colH_XacThuc || e.Column == colV_XacThuc) && e.Value is bool ok)
                e.DisplayText = ok ? "✓ Khớp" : "✗ Không khớp";
        }

        private void gridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if ((e.Column == colH_XacThuc || e.Column == colV_XacThuc) && e.CellValue is bool ok)
                e.Appearance.ForeColor = ok ? UiTheme.Success : UiTheme.DangerDark;
        }

        private void gridView_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (view.RowCount > 0 || _loading) return;
            if (view == gridView_HistoryData)
                UiTheme.DrawEmptyState(e, "Không có ảnh xác thực", "Thiết bị chưa gửi ảnh nhận diện nào trong phiên này.");
            else
                UiTheme.DrawEmptyState(e, "Không có dữ liệu GPS", "Thiết bị chưa gửi hành trình của phiên này.");
        }

        #endregion

        #region Thao tác trên ảnh xác thực

        private ChiTietPhienHoc FocusedHistory() => gridView_HistoryData.GetFocusedRow() as ChiTietPhienHoc;

        private void Menu_DetailSession_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Trước đây bấm khi chưa chọn dòng (hoặc lưới trống) thì văng lỗi ép kiểu
            var row = FocusedHistory();
            if (row == null || _data?.Info == null) { e.Cancel = true; return; }
            mn_InsertHinhDem.Enabled = row.AnhXacThuc?.Length > 0;
            update_ImageLogin.Enabled = update_ImageLoout.Enabled = row.AnhXacThuc?.Length > 0;
        }

        private void update_ImageLogin_Click(object sender, EventArgs e) => _ = ReplaceImageAsync(dangNhap: true);

        private void update_ImageLoout_Click(object sender, EventArgs e) => _ = ReplaceImageAsync(dangNhap: false);

        private async Task ReplaceImageAsync(bool dangNhap)
        {
            var row = FocusedHistory();
            if (row == null) return;
            string loai = dangNhap ? "đăng nhập" : "đăng xuất";
            bool daCo = (dangNhap ? Img_Login.Image : Img_Logout.Image) != null;
            if (XtraMessageBox.Show($"Dùng ảnh xác thực lúc {row.ThoiDiem:HH:mm:ss} làm ảnh {loai} của phiên?"
                    + (daCo ? $"\n\nẢnh {loai} hiện tại sẽ bị thay thế." : ""),
                    "Xác nhận", MessageBoxButtons.YesNo, daCo ? MessageBoxIcon.Warning : MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            string id = _diemdanhnguoilxId;
            Guid historyId = row.Id;
            try
            {
                var img = await Task.Run(() =>
                {
                    if (dangNhap) _phienHocDao.UpdateImageLogin(id, historyId);
                    else _phienHocDao.UpdateImageLogout(id, historyId);
                    return _IMG_DAO.GetImgById(id);
                });
                if (!IsAlive) return;
                if (img.Rows.Count > 0)
                    ShowImages(img.Rows[0]["AnhChup"] as byte[], img.Rows[0]["AnhChup_Logout"] as byte[]);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Không cập nhật được ảnh {loai}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void mn_InsertHinhDem_Click(object sender, EventArgs e)
        {
            var row = FocusedHistory();
            if (row == null) return;
            if (XtraMessageBox.Show($"Thêm ảnh xác thực lúc {row.ThoiDiem:HH:mm:ss} vào mẫu khuôn mặt ban đêm (VT_11) của học viên {lb_HocVien.Text}?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int khoaHoc = KhoaHocID;
            long nguoiLx = NguoiLxID;
            string id = _diemdanhnguoilxId;
            Guid historyId = row.Id;
            try
            {
                await Task.Run(() => _phienHocDao.InsertHinhDem(khoaHoc, nguoiLx, id, historyId));
                if (IsAlive)
                    XtraMessageBox.Show("Đã thêm mẫu khuôn mặt ban đêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Không thêm được mẫu khuôn mặt.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
