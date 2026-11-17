using DevExpress.Export;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DrivingCenter.App.Common;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmQuaTrinhDaoTao : DevExpress.XtraEditors.XtraForm
    {
        private const string HangSoTuDong = HangGplx.B01;
        private const int GioBatDauBanDem = 18;

        private readonly BaoCaoDatDAO _baoCaoDao = new BaoCaoDatDAO();
        private readonly HocVienDatDAO _hocVienDao = new HocVienDatDAO();
        private readonly IMG_DAO _IMG_DAO = new IMG_DAO();
        private readonly string _madk;

        private NguoiLxInDAT _hocVien;
        private TongHop _tongHop = TongHop.Empty;
        private bool _loading;
        private int _loadVersion;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public frmQuaTrinhDaoTao(string madk)
        {
            InitializeComponent();
            _madk = madk;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pnl_Header.Paint += UiTheme.PaintHeader;

            UiTheme.SetIcon(btn_Refresh, "svgimages/icon%20builder/actions_refresh.svg");
            UiTheme.SetIcon(btn_Export, "svgimages/export/exporttoxlsx.svg");
            UiTheme.SetIcon(btn_ChiTietPhien, "svgimages/business%20objects/bo_scheduler.svg");
            UiTheme.StylePrimary(btn_ChiTietPhien);

            UiTheme.StyleGrid(grdView_KetQuaDaoTao, "Tìm theo biển số, hạng xe, ngày...");
            grdView_KetQuaDaoTao.RowHeight = 84;
            grdView_KetQuaDaoTao.OptionsFind.ShowFindButton = false;
            grdView_KetQuaDaoTao.OptionsFind.ShowClearButton = false;

            // Ảnh không đưa vào file Excel (file rất nặng và không đọc được trên lưới Excel)
            Img_Login.OptionsColumn.Printable = DefaultBoolean.False;
            Img_Logout.OptionsColumn.Printable = DefaultBoolean.False;

            ApplySkinColors();
            UserLookAndFeel.Default.StyleChanged += Default_StyleChanged;
            FormClosed += (s, e) => UserLookAndFeel.Default.StyleChanged -= Default_StyleChanged;

            UpdateCommandState();
        }

        private void Default_StyleChanged(object sender, EventArgs e)
        {
            ApplySkinColors();
            pnl_Kpi.Invalidate();
            pnl_Profile.Invalidate();
        }

        private void ApplySkinColors()
        {
            lb_HoTen.Appearance.ForeColor = UiTheme.IsDarkSkin(LookAndFeel) ? Color.FromArgb(147, 176, 222) : UiTheme.Brand;
            lb_HoTen.Appearance.Options.UseForeColor = true;
        }

        private async void frmQuaTrinhDaoTao_Load(object sender, EventArgs e)
        {
            await LoadAsync();
        }

        private void frmQuaTrinhDaoTao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btn_Refresh.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }

        #region Tải dữ liệu

        private async Task LoadAsync()
        {
            if (string.IsNullOrWhiteSpace(_madk))
            {
                XtraMessageBox.Show(this, "Mã đăng ký không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            int version = ++_loadVersion;
            SetLoading(true);

            // Thông tin, ảnh và danh sách phiên lấy song song, không chặn giao diện
            var infoTask = Task.Run(() => _hocVienDao.GetInfoHocVienByMaDK(_madk));
            var avatarTask = Task.Run(() => LoadAvatar(_madk));
            var ketQuaTask = Task.Run(() => _baoCaoDao.GetKetQuaDaoTaoByMaDK(_madk));

            try
            {
                await Task.WhenAll(infoTask, avatarTask, ketQuaTask);
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _loadVersion) return;
                SetLoading(false);
                XtraMessageBox.Show(this, "Không tải được quá trình đào tạo.\n\nChi tiết: " + ex.GetBaseException().Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsAlive || version != _loadVersion) return;

            _hocVien = infoTask.Result;
            ShowProfile(_hocVien, avatarTask.Result);

            DataTable ketQua = ketQuaTask.Result;
            _tongHop = TongHop.Tinh(ketQua, _hocVien?.HangGPLX);
            gridControl_KetQua.DataSource = ketQua;
            GridHelper.EnableRowNumbering(grdView_KetQuaDaoTao);
            FocusFirstDataRow();

            val_SoPhien.Text = _tongHop.SoPhien.ToString("n0");
            val_GuiTC.Text = _tongHop.SoPhien == 0 ? "—" : $"{_tongHop.DaGuiTC}/{_tongHop.SoPhien} phiên";
            val_GuiTC.Appearance.ForeColor = _tongHop.DaGuiTC < _tongHop.SoPhien ? UiTheme.Warning : UiTheme.Success;
            val_GuiTC.Appearance.Options.UseForeColor = _tongHop.SoPhien > 0;

            SetLoading(false);
        }

        private Image LoadAvatar(string madk)
        {
            DataTable img = _IMG_DAO.GetImgByMaDK(madk);
            if (img.Rows.Count == 0 || !(img.Rows[0]["Anh"] is byte[] data) || data.Length == 0) return null;

            // Image.FromStream cần stream sống suốt vòng đời ảnh: copy sang Bitmap để đóng stream an toàn
            using (var ms = new MemoryStream(data))
            using (var source = Image.FromStream(ms))
                return new Bitmap(source);
        }

        private void ShowProfile(NguoiLxInDAT hv, Image avatar)
        {
            var old = pic_Avatar.Image;
            pic_Avatar.Image = avatar;
            old?.Dispose();

            if (hv == null)
            {
                lb_HoTen.Text = "Không tìm thấy học viên";
                lb_MaDK.Text = "Mã ĐK: " + _madk;
                val_NgaySinh.Text = val_KhoaHoc.Text = val_HangDT.Text = "—";
                Text = "Quá trình đào tạo học viên";
                return;
            }

            lb_HoTen.Text = hv.HoVaTen;
            lb_MaDK.Text = "Mã ĐK: " + hv.MaDK;
            val_NgaySinh.Text = hv.NgaySinh == DateTime.MinValue ? "—" : hv.NgaySinh.ToString("dd/MM/yyyy");
            val_KhoaHoc.Text = string.IsNullOrWhiteSpace(hv.TenKH) ? "—" : hv.TenKH;
            val_HangDT.Text = string.IsNullOrWhiteSpace(hv.HangGPLX) ? "—" : hv.HangGPLX;
            Text = $"Quá trình đào tạo — {hv.HoVaTen} ({hv.MaDK})";
        }

        private void SetLoading(bool loading)
        {
            _loading = loading;
            if (loading) grdView_KetQuaDaoTao.ShowLoadingPanel();
            else grdView_KetQuaDaoTao.HideLoadingPanel();
            pnl_Kpi.Invalidate();
            UpdateCommandState();
        }

        private void UpdateCommandState()
        {
            btn_Refresh.Enabled = !_loading;
            btn_Export.Enabled = !_loading && _tongHop.SoPhien > 0;
            btn_ChiTietPhien.Enabled = !_loading && FocusedMaPhien() != null;
        }

        #endregion

        #region Tổng hợp

        /// <summary>
        /// Tổng hợp quãng đường / thời gian (giây) của các phiên: tổng, ban đêm, xe số tự động.
        /// </summary>
        private sealed class TongHop
        {
            public static readonly TongHop Empty = new TongHop();

            public int SoPhien;
            public int DaGuiTC;
            public double Km, Giay;
            public double KmDem, GiayDem;
            public int SoPhienDem;
            public double KmSTD, GiaySTD;
            public int SoPhienSTD;

            public static TongHop Tinh(DataTable ketQua, string hangDaoTao)
            {
                var t = new TongHop();
                if (ketQua == null) return t;

                // Ban đêm chỉ tính xe đúng hạng đào tạo: học viên B.01 tính xe B.01, còn lại tính xe B / C1
                bool hocVienSTD = HangGplx.IsSoTuDong(hangDaoTao);

                foreach (DataRow r in ketQua.Rows)
                {
                    double km = r.Field<double?>("TongQuangDuongLD_Phien") ?? 0;
                    double giay = r.Field<double?>("Time") ?? 0;
                    string hangXe = r.Field<string>("HangXe");
                    DateTime? login = r.Field<DateTime?>("ThoiDiem");

                    t.SoPhien++;
                    t.Km += km;
                    t.Giay += giay;
                    if (r.Field<bool?>("SendTongCuc") == true) t.DaGuiTC++;

                    if (IsBanDem(login) && (hocVienSTD ? HangGplx.IsSoTuDong(hangXe) : HangGplx.Same(hangXe, HangGplx.B) || HangGplx.Same(hangXe, HangGplx.C1)))
                    {
                        t.SoPhienDem++;
                        t.KmDem += km;
                        t.GiayDem += giay;
                    }

                    if (HangGplx.IsSoTuDong(hangXe))
                    {
                        t.SoPhienSTD++;
                        t.KmSTD += km;
                        t.GiaySTD += giay;
                    }
                }
                return t;
            }
        }

        private static bool IsBanDem(DateTime? login) => login.HasValue && login.Value.Hour >= GioBatDauBanDem;

        private static string GioPhut(double seconds)
        {
            var ts = TimeSpan.FromSeconds(seconds);
            return $"{(int)ts.TotalHours:00}:{ts.Minutes:00}";
        }

        private void pnl_Kpi_Resize(object sender, EventArgs e) => pnl_Kpi.Invalidate();

        private void pnl_Kpi_Paint(object sender, PaintEventArgs e)
        {
            var t = _tongHop;
            bool none = _loading;
            string dash = "—";

            var cards = new[]
            {
                new UiTheme.KpiCard("QUÃNG ĐƯỜNG", none ? dash : $"{t.Km:n1} km",
                    none ? "Đang tải..." : $"{t.SoPhien} phiên hợp lệ", UiTheme.Brand),
                new UiTheme.KpiCard("THỜI GIAN (GIỜ:PHÚT)", none ? dash : GioPhut(t.Giay),
                    none ? "" : $"≈ {t.Giay / 3600:n2} giờ", UiTheme.Success),
                new UiTheme.KpiCard("BAN ĐÊM", none ? dash : GioPhut(t.GiayDem),
                    none ? "" : $"{t.KmDem:n1} km · {t.SoPhienDem} phiên", Color.FromArgb(99, 102, 241)),
                new UiTheme.KpiCard("SỐ TỰ ĐỘNG (B.01)", none ? dash : GioPhut(t.GiaySTD),
                    none ? "" : $"{t.KmSTD:n1} km · {t.SoPhienSTD} phiên", UiTheme.Warning),
            };

            UiTheme.DrawKpiCards(e.Graphics, pnl_Kpi.ClientRectangle, cards, UiTheme.SkinText(LookAndFeel));
        }

        private void pnl_Profile_Paint(object sender, PaintEventArgs e)
        {
            UiTheme.DrawSection(e.Graphics, "THÔNG TIN HỌC VIÊN", 20, 330, 262);
            UiTheme.DrawSection(e.Graphics, "CÁCH TÍNH", 20, 514, 262);

            // Vạch phân cách với vùng danh sách phiên
            using (var pen = new Pen(UiTheme.Muted))
                e.Graphics.DrawLine(pen, pnl_Profile.Width - 1, 0, pnl_Profile.Width - 1, pnl_Profile.Height);
        }

        #endregion

        #region Lưới phiên học

        private void grdView_KetQuaDaoTao_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == Time_Phien && e.Value is double seconds)
            {
                var ts = TimeSpan.FromSeconds(seconds);
                e.DisplayText = $"{(int)ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}";
            }
            else if (e.Column == Login && !e.IsForGroupRow && e.Value is DateTime login && IsBanDem(login))
            {
                // Gán lại toàn bộ chuỗi: sự kiện được gọi nhiều lần cho cùng một ô
                e.DisplayText = login.ToString(Login.DisplayFormat.FormatString) + "  ☾";
            }
        }

        private void grdView_KetQuaDaoTao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column == Login && e.CellValue is DateTime login && IsBanDem(login))
            {
                e.Appearance.ForeColor = Color.FromArgb(99, 102, 241);
            }
            else if (e.Column == Result_Sent && Equals(e.CellValue, false))
            {
                e.Appearance.BackColor = Color.FromArgb(40, UiTheme.Warning);
            }
        }

        private void grdView_KetQuaDaoTao_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            if (grdView_KetQuaDaoTao.RowCount > 0 || _loading) return;
            if (string.IsNullOrEmpty(grdView_KetQuaDaoTao.FindFilterText))
                UiTheme.DrawEmptyState(e, "Chưa có phiên học", "Học viên chưa có phiên DAT nào từ 5 phút trở lên.");
            else
                UiTheme.DrawEmptyState(e, "Không tìm thấy phiên", "Thử từ khóa khác hoặc xóa ô tìm kiếm.");
        }

        private void grdView_KetQuaDaoTao_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e) => UpdateCommandState();

        private void grdView_KetQuaDaoTao_DoubleClick(object sender, EventArgs e)
        {
            // Bấm đúp vào dòng nhóm "Xe hạng ..." hoặc header thì không có mã phiên
            var pt = gridControl_KetQua.PointToClient(MousePosition);
            if (!grdView_KetQuaDaoTao.CalcHitInfo(pt).InDataRow) return;
            OpenChiTietPhien();
        }

        private void grdView_KetQuaDaoTao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            OpenChiTietPhien();
            e.Handled = true;
        }

        private void btn_ChiTietPhien_Click(object sender, EventArgs e) => OpenChiTietPhien();

        private void FocusFirstDataRow()
        {
            // Dòng đầu tiên là dòng nhóm "Xe hạng ...": chọn phiên đầu tiên để nút "Xem chi tiết phiên" dùng được ngay
            var view = grdView_KetQuaDaoTao;
            for (int i = 0; i < view.RowCount; i++)
            {
                int handle = view.GetVisibleRowHandle(i);
                if (!view.IsDataRow(handle)) continue;
                view.FocusedRowHandle = handle;
                break;
            }
            view.TopRowIndex = 0;
        }

        private Guid? FocusedMaPhien()
        {
            int handle = grdView_KetQuaDaoTao.FocusedRowHandle;
            if (!grdView_KetQuaDaoTao.IsDataRow(handle)) return null;
            return grdView_KetQuaDaoTao.GetRowCellValue(handle, MaPhien) is Guid id && id != Guid.Empty ? id : (Guid?)null;
        }

        private void OpenChiTietPhien()
        {
            Guid? maPhien = FocusedMaPhien();
            if (maPhien == null) return;

            using (var f = new frmDetailSession(maPhien.Value.ToString()))
                f.ShowDialog(this);
        }

        #endregion

        #region Lệnh

        private async void btn_Refresh_Click(object sender, EventArgs e)
        {
            await LoadAsync();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            string ten = _hocVien?.HoVaTen ?? _madk;
            foreach (char c in Path.GetInvalidFileNameChars())
                ten = ten.Replace(c, '-');

            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"QuaTrinhDaoTao_{ten}_{DateTime.Now:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    grdView_KetQuaDaoTao.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    if (XtraMessageBox.Show(this, "Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(this, "Không xuất được file: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}
