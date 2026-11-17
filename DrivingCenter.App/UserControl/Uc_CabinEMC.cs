using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Helper;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_CabinEMC : DevExpress.XtraEditors.XtraUserControl
    {
        private const string DangDaoTao = "Đang Đào Tạo";

        private readonly CabinI2E_DAO _DAO = new CabinI2E_DAO();
        private readonly GiaoVienDatDAO _giaoVienDao = new GiaoVienDatDAO();

        // Giao diện nằm trong Designer; code chỉ lo dữ liệu và thao tác.
        private string TenKH;
        private int IdKhoaHoc;
        private List<HocVien> _hocVien = new List<HocVien>();
        private int _hvVersion, _tkVersion;
        private bool _thongKeLoaded;
        private bool _tkLoading;   // đang tải thống kê: không vẽ "không có dữ liệu" / footer
        private readonly Timer _detailDebounce = new Timer { Interval = 250 };

        // Chi tiết học viên (khung bên phải): trước đây là UC_Detail tạo mới mỗi lần chọn học viên
        private long? _detailId;
        private int _detailVersion;
        private List<Exercise> _danhMucBaiTap;
        private readonly Dictionary<long, int> _baiTapDaDoi = new Dictionary<long, int>();   // DiemDanhNguoiLxID → bài tập mới

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public Uc_CabinEMC()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
            foreach (var bar in new[] { pnl_ToolbarTK, pnl_ToolbarKH })
                bar.Paint += UiTheme.PaintHeader;

            _detailDebounce.Tick += DetailDebounce_Tick;
            Disposed += (s, e) => _detailDebounce.Dispose();
        }

        private async void Uc_CabinEMC_Load(object sender, EventArgs e)
        {
            // Đặt ngày trước khi gắn dữ liệu; tab Thống kê chỉ tải khi được mở
            _thongKeLoaded = false;
            dtp_DayTHCabin.EditValueChanged -= dtp_DayTHCabin_EditValueChanged;
            dtp_DayTHCabin.DateTime = DateTime.Today;
            dtp_DayTHCabin.EditValueChanged += dtp_DayTHCabin_EditValueChanged;

            await InitializeAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                if (xtraTabControl.SelectedTabPage == tab_DangDaoTao) mn_refresh_Click(this, EventArgs.Empty);
                else if (xtraTabControl.SelectedTabPage == tab_ThongKe) _ = LoadTongHopKQAsync();
                else btn_RefreshKH_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tab_ThongKe && !_thongKeLoaded) _ = LoadTongHopKQAsync();
        }

        #region Khóa học

        /// <summary>Tải toàn bộ khóa học (tab Khóa học) và danh sách khóa đang đào tạo (ô chọn tab Đang đào tạo).</summary>
        public async Task InitializeAsync(int? chonKhoaHocId = null)
        {
            List<KhoaHocCabin> lst;
            try
            {
                lst = await Task.Run(() => _DAO.GetAllKhoaHoc());
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không tải được danh sách khóa học cabin", ex);
                return;
            }
            if (!IsAlive) return;

            grd_QuanLyKhoaHoc.DataSource = lst;

            var dangDaoTao = lst.Where(x => x.LoaiDaoTao == DangDaoTao).OrderBy(x => x.TenKhoaHoc).ToList();
            int? giuLai = chonKhoaHocId ?? (IdKhoaHoc != 0 ? IdKhoaHoc : (int?)null);
            cbb_KhoaHocEMC.Properties.DataSource = dangDaoTao;
            cbb_KhoaHocEMC.Properties.DisplayMember = "TenKhoaHoc";
            cbb_KhoaHocEMC.Properties.ValueMember = "KhoaHocID";

            if (dangDaoTao.Count == 0)
            {
                lbl_HvSummary.Text = "Chưa có khóa nào ở trạng thái \"Đang Đào Tạo\". Đổi trạng thái khóa ở tab Khóa học.";
                return;
            }
            int chon = giuLai.HasValue && dangDaoTao.Any(x => x.KhoaHocID == giuLai.Value) ? giuLai.Value : dangDaoTao[0].KhoaHocID;
            if (Equals(cbb_KhoaHocEMC.EditValue, chon))
                LoadLstHvByKH(chon);
            else
                cbb_KhoaHocEMC.EditValue = chon;
        }

        private async void btn_RefreshKH_Click(object sender, EventArgs e)
        {
            await InitializeAsync();
        }

        private void gridView_QLKhoaHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            KhoaHocCabin rowData = gridView_QLKhoaHoc.GetFocusedRow() as KhoaHocCabin;
            bool coKhoa = rowData != null;
            btn_SaveKH.Enabled = coKhoa;
            txt_TenKhoaHoc.Properties.ReadOnly = !coKhoa;
            cbb_TrangThai.Properties.ReadOnly = !coKhoa;
            if (!coKhoa) return;

            txt_MaKH.Text = rowData.MaKhoaHoc;
            txt_TenKhoaHoc.Text = rowData.TenKhoaHoc;
            date_NgayKG.Text = rowData.NgayKhaiGiang.ToString("dd/MM/yyyy");
            date_NgayBG.Text = rowData.NgayBeGiang.ToString("dd/MM/yyyy");
            txt_MaBCI.Text = rowData.MaBC1;
            txt_HangDT.Text = rowData.HangDaoTao;
            cbb_TrangThai.Text = rowData.LoaiDaoTao;
        }

        private async void btn_SaveKH_Click(object sender, EventArgs e)
        {
            var khoaHoc = gridView_QLKhoaHoc.GetFocusedRow() as KhoaHocCabin;
            if (khoaHoc == null) return;

            string tenkhoahoc = txt_TenKhoaHoc.Text.Trim();
            if (tenkhoahoc.Length == 0)
            {
                XtraMessageBox.Show("Tên khóa học không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_TenKhoaHoc.Focus();
                return;
            }

            int loaidaotao = cbb_TrangThai.Text == "Chưa Đào Tạo" ? 1 : cbb_TrangThai.Text == DangDaoTao ? 2 : 3;
            string makhoahoc = khoaHoc.MaKhoaHoc;
            try
            {
                await Task.Run(() => _DAO.UpdateStatusKhoaHoc(tenkhoahoc, loaidaotao, makhoahoc));
            }
            catch (Exception ex)
            {
                ShowError("Không cập nhật được khóa học", ex);
                return;
            }
            if (!IsAlive) return;

            await InitializeAsync();
            XtraMessageBox.Show($"Đã cập nhật khóa {tenkhoahoc}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Học viên đang đào tạo

        private void cbb_KhoaHocEMC_EditValueChanged(object sender, EventArgs e)
        {
            if (!(cbb_KhoaHocEMC.EditValue is int id)) return;
            IdKhoaHoc = id;
            TenKH = cbb_KhoaHocEMC.Text;
            LoadLstHvByKH(IdKhoaHoc);
        }

        /// <summary>
        /// Cập nhật thời gian / bài tập rồi kiểm tra điều kiện cho từng học viên của khóa.
        /// Mỗi học viên tốn 2 lệnh DB nên chạy nền (trước đây chạy trên luồng giao diện, mở màn hình mất ~9 giây).
        /// </summary>
        public async void LoadLstHvByKH(int idKhoaHoc)
        {
            int version = ++_hvVersion;
            _hocVien = new List<HocVien>();
            grd_HocVien.DataSource = null;
            ShowDetail(null);
            gridview_HocVien.ShowLoadingPanel();
            lbl_HvSummary.Text = "Đang tính thời gian và kết quả học viên...";
            btn_RefreshHV.Enabled = false;

            List<HocVien> lst;
            try
            {
                lst = await Task.Run(() =>
                {
                    foreach (var nguoiLxId in _DAO.GetlstNguoiLxID(idKhoaHoc))
                        _DAO.UpdateTimeExerToNguoiLx(nguoiLxId);

                    var students = _DAO.GetlstHocVienByIdKhoaHoc(idKhoaHoc);
                    foreach (HocVien item in students)
                        item.Result = _DAO.CheckDK(item.NguoiLxID, item.MaKH) == "Đáp ứng quy định";
                    return students;
                });
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _hvVersion) return;
                gridview_HocVien.HideLoadingPanel();
                btn_RefreshHV.Enabled = true;
                lbl_HvSummary.Text = "Không tải được danh sách học viên.";
                ShowError("Không tải được danh sách học viên", ex);
                return;
            }
            if (!IsAlive || version != _hvVersion) return;

            _hocVien = lst;
            grd_HocVien.DataSource = lst;
            GridHelper.EnableRowNumbering(gridview_HocVien, 40);
            gridview_HocVien.HideLoadingPanel();
            btn_RefreshHV.Enabled = true;

            int dat = lst.Count(x => x.Result);
            int chuaDat = lst.Count - dat;
            lbl_HvSummary.Text = lst.Count == 0
                ? "Khóa chưa có học viên."
                : $"<b>{lst.Count}</b> học viên   ·   <color=21,128,61>{dat} đủ điều kiện</color>"
                  + (chuaDat > 0 ? $"   ·   <color=185,28,28>{chuaDat} chưa đạt</color>" : "");

            FocusFirstDataRow(gridview_HocVien);

            _detailDebounce.Stop();   // FocusedRowChanged không phát nếu handle không đổi

            ShowDetail(gridview_HocVien.GetFocusedRow() as HocVien);
        }

        private static void FocusFirstDataRow(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            for (int i = 0; i < view.RowCount; i++)
            {
                int handle = view.GetVisibleRowHandle(i);
                if (!view.IsDataRow(handle)) continue;
                view.FocusedRowHandle = handle;
                break;
            }
            view.TopRowIndex = 0;
        }

        private void gridview_HocVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Chờ người dùng dừng di chuyển rồi mới tải chi tiết (mỗi học viên tốn vài truy vấn)
            _detailDebounce.Stop();
            _detailDebounce.Start();
        }

        private void DetailDebounce_Tick(object sender, EventArgs e)
        {
            _detailDebounce.Stop();
            if (!IsAlive) return;
            ShowDetail(gridview_HocVien.GetFocusedRow() as HocVien);
        }

        #endregion

        #region Chi tiết học viên (khung bên phải)

        /// <summary>
        /// Hiện chi tiết học viên bên phải. Dòng nhóm (Giáo viên: ...) không có học viên nên ẩn khung chi tiết.
        /// </summary>
        private void ShowDetail(HocVien hocVien)
        {
            if (hocVien == null)
            {
                _detailId = null;
                ++_detailVersion;
                pnl_DetailHV.Visible = false;
                LayoutPanel.Invalidate();
                return;
            }
            if (_detailId == hocVien.NguoiLxID && pnl_DetailHV.Visible) return;
            _ = LoadDetailAsync(hocVien.NguoiLxID);
        }

        private sealed class ChiTietHV
        {
            public HocVien Info;
            public int SoBaiTap;
            public string KetLuan;
            public List<Result> KetQua;
        }

        private async Task LoadDetailAsync(long nguoiLxId)
        {
            int version = ++_detailVersion;
            _detailId = nguoiLxId;
            _baiTapDaDoi.Clear();
            pnl_DetailHV.Visible = true;
            gridView_Result.ShowLoadingPanel();
            UpdateDetailButtons();

            ChiTietHV data;
            try
            {
                bool canDanhMuc = _danhMucBaiTap == null;
                data = await Task.Run(() =>
                {
                    if (canDanhMuc) _danhMucBaiTap = _DAO.GetListBaiTap();
                    var info = _DAO.GetInfoHVByNguoiLxID(nguoiLxId);
                    return new ChiTietHV
                    {
                        Info = info,
                        SoBaiTap = _DAO.CountBaiTap(nguoiLxId),
                        KetLuan = info == null ? null : _DAO.CheckDK(info.NguoiLxID, info.MaKH),
                        KetQua = _DAO.GetResultByNguoiLxID(nguoiLxId)
                    };
                });
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _detailVersion) return;
                gridView_Result.HideLoadingPanel();
                ShowError("Không tải được chi tiết học viên", ex);
                return;
            }
            if (!IsAlive || version != _detailVersion) return;

            if (lkDanhMucBaiTap.DataSource == null) lkDanhMucBaiTap.DataSource = _danhMucBaiTap;

            HocVien hv = data.Info;
            var oldImage = pic_Avatar.Image;
            pic_Avatar.Image = hv?.Anh == null ? null : ImageExtension.byteArrayToImage(hv.Anh);
            if (!ReferenceEquals(oldImage, pic_Avatar.Image)) oldImage?.Dispose();

            if (hv == null)
            {
                lb_HoTen.Text = "Không tìm thấy học viên";
                lb_MaDK.Text = lb_Info1.Text = lb_Info2.Text = lb_KetQua.Text = "—";
            }
            else
            {
                lb_HoTen.Text = hv.HoTenHV;
                lb_MaDK.Text = "Mã ĐK: " + hv.MaDK;
                lb_Info1.Text = $"Ngày sinh {hv.NgaySinh:dd/MM/yyyy}   ·   {hv.GioiTinh}   ·   CCCD {hv.SoCMT} (cấp {hv.NgayCapCMT:dd/MM/yyyy})";
                lb_Info2.Text = $"Khóa {hv.TenKH}   ·   Hạng {hv.HangGPLX}   ·   GV {hv.TenGV}";
                bool dat = data.KetLuan == "Đáp ứng quy định";
                string mau = dat ? "21,128,61" : "185,28,28";
                lb_KetQua.Text = $"<b>{data.SoBaiTap}</b> bài tập   ·   tổng <b>{hv.ToTalSecond}</b>   ·   <color={mau}><b>{data.KetLuan}</b></color>";
            }

            grd_Result.DataSource = data.KetQua;
            GridHelper.EnableRowNumbering(gridView_Result, 36);
            gridView_Result.HideLoadingPanel();
            gridView_Result.ClearSelection();
            UpdateDetailButtons();
        }

        private void UpdateDetailButtons()
        {
            btn_SaveBT.Enabled = _baiTapDaDoi.Count > 0;
            btn_SaveBT.Text = _baiTapDaDoi.Count > 0 ? $"Lưu bài tập đã đổi ({_baiTapDaDoi.Count})" : "Lưu bài tập đã đổi";
            btn_DeleteBT.Enabled = gridView_Result.GetSelectedRows().Any(gridView_Result.IsDataRow);
            btn_PrintHV.Enabled = _detailId.HasValue;
        }

        // Đổi tên bài tập trên lưới: ghi nhận dòng đã đổi (trước đây chỉ lưu các dòng đang được chọn)
        private void gridView_Result_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != colR_TenBai || !(gridView_Result.GetRow(e.RowHandle) is Result r) || !(e.Value is int baiTap)) return;
            _baiTapDaDoi[r.DiemDanhNguoiLxID] = baiTap;
            UpdateDetailButtons();
        }

        private void gridView_Result_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) => UpdateDetailButtons();

        private void gridView_Result_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == colR_TenBai && gridView_Result.GetRow(e.RowHandle) is Result r && _baiTapDaDoi.ContainsKey(r.DiemDanhNguoiLxID))
            {
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(40, UiTheme.Warning);
                e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            }
        }

        private void gridView_Result_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_Result.RowCount > 0 || !_detailId.HasValue) return;
            UiTheme.DrawEmptyState(e, "Chưa có kết quả", "Học viên chưa có phiên tập cabin nào.");
        }

        private async void btn_SaveBT_Click(object sender, EventArgs e)
        {
            gridView_Result.CloseEditor();
            gridView_Result.UpdateCurrentRow();
            if (_baiTapDaDoi.Count == 0 || !_detailId.HasValue) return;

            var thayDoi = _baiTapDaDoi.ToList();
            long nguoiLxId = _detailId.Value;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var item in thayDoi)
                        _DAO.UpdateBaiTap(item.Value, item.Key);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không lưu được bài tập", ex);
                return;
            }
            if (!IsAlive) return;
            await LoadDetailAsync(nguoiLxId);
        }

        private async void btn_DeleteBT_Click(object sender, EventArgs e)
        {
            if (!_detailId.HasValue) return;
            var phien = gridView_Result.GetSelectedRows()
                .Where(gridView_Result.IsDataRow)
                .Select(h => gridView_Result.GetRow(h) as Result)
                .Where(r => r != null)
                .ToList();
            if (phien.Count == 0) return;

            // Xóa là không hoàn tác: trước đây không hỏi lại và tải lại toàn bộ sau mỗi dòng
            string danhSach = string.Join("\n", phien.Take(8).Select(r => $"• Bài {r.BaiHocCaBinID} – {r.ThoiDiemBatDau:HH:mm dd/MM/yyyy}"));
            if (phien.Count > 8) danhSach += $"\n• … và {phien.Count - 8} phiên khác";
            if (XtraMessageBox.Show($"Xóa {phien.Count} phiên tập của {lb_HoTen.Text}?\n\n{danhSach}\n\nThao tác này không hoàn tác được.",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            long nguoiLxId = _detailId.Value;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var r in phien)
                        _DAO.DeleteResultExcer(r.DiemDanhNguoiLxID);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không xóa được phiên tập", ex);
                return;
            }
            if (!IsAlive) return;
            await LoadDetailAsync(nguoiLxId);
        }

        private void btn_PrintHV_Click(object sender, EventArgs e)
        {
            if (_detailId.HasValue)
                new rptPrintResult_EMC(_detailId.Value.ToString(), null, 1).Show();
        }

        private void LayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            if (pnl_DetailHV.Visible) return;
            var b = LayoutPanel.ClientRectangle;
            using (var titleFont = new System.Drawing.Font("Roboto", 13F, System.Drawing.FontStyle.Bold))
            using (var hintFont = new System.Drawing.Font("Roboto", 9.75F))
            using (var format = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center })
            using (var title = new System.Drawing.SolidBrush(UiTheme.Brand))
            using (var hint = new System.Drawing.SolidBrush(UiTheme.TextMuted))
            {
                int cy = b.Height / 2;
                e.Graphics.DrawString("Chọn một học viên", titleFont, title, new System.Drawing.RectangleF(0, cy - 30, b.Width, 26), format);
                e.Graphics.DrawString("Ảnh, thông tin và kết quả từng bài tập cabin sẽ hiện ở đây.", hintFont, hint,
                    new System.Drawing.RectangleF(0, cy + 2, b.Width, 22), format);
            }
        }

        private void gridview_HocVien_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == Result && e.Value is bool ok)
                e.DisplayText = ok ? "Đạt" : "Chưa đạt";
        }

        private void gridview_HocVien_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridview_HocVien.RowCount > 0 || IdKhoaHoc == 0) return;
            if (!string.IsNullOrEmpty(gridview_HocVien.FindFilterText))
                UiTheme.DrawEmptyState(e, "Không tìm thấy học viên", "Thử từ khóa khác hoặc xóa ô tìm kiếm.");
        }

        #endregion

        #region Thống kê theo ngày

        private async Task LoadTongHopKQAsync()
        {
            int version = ++_tkVersion;
            _thongKeLoaded = true;
            _tkLoading = true;
            gridView_TongHop.OptionsView.ShowFooter = false;
            DateTime day = dtp_DayTHCabin.DateTime.Date;
            lb_ThongKe.Text = $"Thống kê kết quả cabin ngày {day:dd/MM/yyyy}";
            gridView_TongHop.ShowLoadingPanel();

            List<ThongKe> lst;
            try
            {
                lst = await Task.Run(() => _DAO.LstThongKe(day.ToString("dd/MM/yyyy")));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _tkVersion) return;
                _tkLoading = false;
                gridView_TongHop.HideLoadingPanel();
                ShowError("Không tải được thống kê", ex);
                return;
            }
            if (!IsAlive || version != _tkVersion) return;

            grd_ThongKe.DataSource = lst;
            GridHelper.EnableRowNumbering(gridView_TongHop, 40);
            _tkLoading = false;
            gridView_TongHop.HideLoadingPanel();
            gridView_TongHop.OptionsView.ShowFooter = lst.Count > 0;
            btn_ExportTK.Enabled = lst.Count > 0;
            lb_ThongKe.Text += $"  ·  {lst.Count} học viên";
        }

        private void dtp_DayTHCabin_EditValueChanged(object sender, EventArgs e)
        {
            _ = LoadTongHopKQAsync();
        }

        private void ShiftDay_Click(object sender, EventArgs e)
        {
            int delta = int.Parse((string)((Control)sender).Tag);
            dtp_DayTHCabin.DateTime = dtp_DayTHCabin.DateTime.Date.AddDays(delta);
        }

        private void gridView_TongHop_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_TongHop.RowCount > 0 || _tkLoading) return;
            UiTheme.DrawEmptyState(e, "Không có học viên học cabin",
                $"Ngày {dtp_DayTHCabin.DateTime:dd/MM/yyyy} chưa có phiên học cabin nào.");
        }

        private void btn_ExportTK_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"ThongKeCabinEMC_{dtp_DayTHCabin.DateTime:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    grd_ThongKe.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Process.Start(new ProcessStartInfo(dialog.FileName) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    ShowError("Không xuất được file", ex);
                }
            }
        }

        #endregion

        #region Menu & báo cáo

        private void contextMenuStrip_DTT_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool coHocVien = gridview_HocVien.GetFocusedRow() is HocVien;
            bool coKhoa = IdKhoaHoc != 0;
            mn_ReplicationResult.Enabled = coHocVien;
            mn_PrintAllReport.Enabled = coKhoa;
            mn_TongHopBaoCao.Enabled = coKhoa;
            mn_SyncTeacher.Enabled = coKhoa;
        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc == 0) return;
            cms_BaoCao.Show(btn_BaoCao, new System.Drawing.Point(0, btn_BaoCao.Height));
        }

        private void mn_refresh_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc != 0) LoadLstHvByKH(IdKhoaHoc);
        }

        private async void thêmKhóaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? khoaMoi = ThemKhoaHoc();
            if (khoaMoi.HasValue) await InitializeAsync();
        }

        private void mn_ReplicationResult_Click(object sender, EventArgs e)
        {
            if (!(gridview_HocVien.GetFocusedRow() is HocVien rowData)) return;

            // Modal: cập nhật xong thì tính lại thời gian / kết quả của khóa
            using (var f = new frmReplication(rowData.NguoiLxID))
                f.ShowDialog(FindForm());
            LoadLstHvByKH(IdKhoaHoc);
        }

        private void mn_PrintAllReport_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc == 0) return;
            string lstNguoiLxID = _DAO.GetLstNguoiLxID(IdKhoaHoc);
            new rptPrintResult_EMC(lstNguoiLxID, null, 1).Show();
        }

        private void mn_TongHopBaoCao_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc == 0) return;
            new rptPrintResult_EMC(null, IdKhoaHoc, 2).Show();
        }

        /// <summary>
        /// Thêm giáo viên mới từ DAT sang cabin rồi gán giáo viên cho học viên của khóa theo DAT.
        /// Trước đây cả 2 danh sách đều đọc từ DB cabin nên không có giáo viên nào được thêm và việc gán GV không đổi gì.
        /// </summary>
        private async void mn_SyncTeacher_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc == 0) return;
            if (XtraMessageBox.Show($"Đồng bộ giáo viên của khóa {TenKH} từ hệ thống DAT?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int idKhoaHoc = IdKhoaHoc;
            string tenKH = TenKH;
            (int themGV, int ganGV) ketQua;
            try
            {
                ketQua = await Task.Run(() =>
                {
                    var daCo = new HashSet<int>(_DAO.GetAllGV().Select(x => x.GiaoVienID));
                    var gvMoi = _giaoVienDao.GetAllGV().Where(x => !daCo.Contains(x.GiaoVienID)).ToList();
                    foreach (GiaoVien item in gvMoi)
                        _DAO.InsertTeacherNew(item);

                    var gvTheoMaDK = _giaoVienDao.GetListGvByNameKH(tenKH)
                        .GroupBy(x => x.MaDK).ToDictionary(g => g.Key, g => g.First().GiaoVienID);
                    int gan = 0;
                    foreach (HocVien hv in _DAO.GetlstHocVienByIdKhoaHoc(idKhoaHoc))
                    {
                        if (hv.MaDK == null || !gvTheoMaDK.TryGetValue(hv.MaDK, out int gvId)) continue;
                        _DAO.UpdateGv(gvId, hv.MaDK);
                        gan++;
                    }
                    return (gvMoi.Count, gan);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không đồng bộ được giáo viên", ex);
                return;
            }
            if (!IsAlive) return;

            XtraMessageBox.Show($"Đồng bộ xong: thêm {ketQua.themGV} giáo viên mới, cập nhật giáo viên cho {ketQua.ganGV} học viên.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadLstHvByKH(idKhoaHoc);
        }

        #endregion

        #region Nhập khóa học từ XML

        /// <summary>Nhập khóa học + học viên từ file XML báo cáo 1. Trả về KhoaHocID nếu nhập thành công.</summary>
        private int? ThemKhoaHoc()
        {
            string filePath;
            using (var openFileDialog = new OpenFileDialog { Filter = "File XML báo cáo 1|*.xml", ValidateNames = true, Multiselect = false })
            {
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return null;
                filePath = openFileDialog.FileName;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                XDocument xdoc = XDocument.Load(filePath);
                XElement khoaHocNode = xdoc.Descendants("KHOA_HOC").FirstOrDefault();
                if (khoaHocNode == null)
                {
                    XtraMessageBox.Show("File không có thông tin khóa học (thẻ KHOA_HOC).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                var kh = new KhoaHocCabin
                {
                    MaKhoaHoc = khoaHocNode.Element("MA_KHOA_HOC")?.Value,
                    TenKhoaHoc = khoaHocNode.Element("TEN_KHOA_HOC")?.Value,
                    NgayKhaiGiang = ParseDate(khoaHocNode.Element("NGAY_KHAI_GIANG")?.Value),
                    MaBC1 = khoaHocNode.Element("MA_BCI")?.Value,
                    NgayBeGiang = ParseDate(khoaHocNode.Element("NGAY_BE_GIANG")?.Value),
                    NgaySatHach = ParseDate(khoaHocNode.Element("NGAY_SAT_HACH")?.Value),
                    HangDaoTao = khoaHocNode.Element("HANG_GPLX")?.Value,
                    NgayDaoTao = ParseDate(khoaHocNode.Element("NGAY_BCI")?.Value)
                };

                if (_DAO.GetKhoaHocIdByMaKhoaHoc(kh.MaKhoaHoc) != 0 &&
                    XtraMessageBox.Show($"Khóa {kh.TenKhoaHoc} ({kh.MaKhoaHoc}) đã có trên cabin. Vẫn nhập lại học viên?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return null;

                _DAO.InsertKhoaHoc(kh);
                int khoahocId = _DAO.GetKhoaHocIdByMaKhoaHoc(kh.MaKhoaHoc);

                int soHV = 0;
                foreach (var item in xdoc.Descendants("NGUOI_LX"))
                {
                    var hv = new HocVien
                    {
                        MaDK = item.Element("MA_DK")?.Value,
                        HoTenDem = item.Element("HO_TEN_DEM")?.Value,
                        Ten = item.Element("TEN")?.Value,
                        HoTenHV = item.Element("HO_VA_TEN")?.Value,
                        NgaySinh = ParseDate(item.Element("NGAY_SINH")?.Value),
                        GioiTinh = item.Element("GIOI_TINH")?.Value == "F" ? "Nữ" : "Nam",
                        QuocTich = item.Element("MA_QUOC_TICH")?.Value,
                        SoCMT = item.Element("SO_CMT")?.Value,
                        NgayCapCMT = ParseDate(item.Element("NGAY_CAP_CMT")?.Value),
                        HangGPLX = (string)item.Element("HO_SO")?.Element("HANG_GPLX"),
                        ImageBase64 = (string)item.Element("HO_SO")?.Element("ANH_CHAN_DUNG")
                    };

                    _DAO.InsertNguoiLxBase(khoahocId, kh.TenKhoaHoc, hv, ConvertBase64ToBytes(hv.ImageBase64));
                    soHV++;
                }

                XtraMessageBox.Show($"Đã nhập khóa {kh.TenKhoaHoc} với {soHV} học viên.\nĐổi trạng thái khóa sang \"Đang Đào Tạo\" ở tab Khóa học để theo dõi kết quả.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return khoahocId;
            }
            catch (Exception ex)
            {
                ShowError("Không nhập được khóa học", ex);
                return null;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>Ngày trong file XML có thể ở dạng yyyyMMdd hoặc dạng ngày thông thường.</summary>
        private static DateTime ParseDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return DateTime.MinValue;
            value = value.Trim();
            if (DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
                return d;
            // Giữ cách đọc cũ (Convert.ToDateTime theo culture máy), thử thêm dạng chuẩn ISO
            if (DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out d)) return d;
            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Ảnh chân dung (base64) → JPEG. Không có ảnh thì trả null
        /// (trước đây trả lại ảnh của học viên trước đó vì dùng chung một biến).
        /// </summary>
        private static byte[] ConvertBase64ToBytes(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return null;

            using (var image = new MagickImage(Convert.FromBase64String(base64String)))
            using (var ms = new MemoryStream())
            {
                image.Format = MagickFormat.Jpeg;
                image.Write(ms);
                return ms.ToArray();
            }
        }

        #endregion

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show($"{title}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
