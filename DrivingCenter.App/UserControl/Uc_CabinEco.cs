using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_CabinEco : DevExpress.XtraEditors.XtraUserControl
    {
        private const string DangDaoTao = "Đang đào tạo";
        private const string DaKetThuc = "Đã kết thúc";

        private readonly CabinEco_DAO _DAO = new CabinEco_DAO();
        private readonly GiaoVienDatDAO _giaoVienDao = new GiaoVienDatDAO();

        // Giao diện nằm trong Designer; code chỉ lo dữ liệu và thao tác.
        private int IdKhoaHoc;
        private string TenKH;
        private List<HocVien> _lstStudent = new List<HocVien>();
        private int _hvVersion, _tkVersion;
        private bool _thongKeLoaded;
        private bool _tkLoading;   // đang tải thống kê: không vẽ "không có dữ liệu" / footer
        private readonly Timer _detailDebounce = new Timer { Interval = 250 };

        // Chi tiết học viên (khung bên phải)
        private string IdHocVien;
        private string SoCCCD;
        private int _detailVersion;
        private List<Exercise> _danhMucBaiTap;
        private readonly Dictionary<long, int> _baiTapDaDoi = new Dictionary<long, int>();   // ID kết quả → bài tập mới

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public Uc_CabinEco()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
            foreach (var bar in new[] { pnl_ToolbarTK, pnl_ToolbarKH })
                bar.Paint += UiTheme.PaintHeader;

            _detailDebounce.Tick += DetailDebounce_Tick;
            Disposed += (s, e) => _detailDebounce.Dispose();
        }

        private async void Uc_CabinEco_Load(object sender, EventArgs e)
        {
            // Đặt ngày trước khi gắn dữ liệu; tab Thống kê chỉ tải khi được mở
            _thongKeLoaded = false;
            dtp_DayTHCabin.EditValueChanged -= dtp_DayTHCabin_EditValueChanged;
            dtp_DayTHCabin.DateTime = DateTime.Today;
            dtp_DayTHCabin.EditValueChanged += dtp_DayTHCabin_EditValueChanged;
            dt_TimeStart.DateTime = DateTime.Now;

            await InitializeAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                if (xtraTabControl.SelectedTabPage == tab_DangDaoTao) mn_refresh_Click(this, EventArgs.Empty);
                else if (xtraTabControl.SelectedTabPage == tab_ThongKe) _ = LoadThCabinAsync();
                else btn_RefreshKH_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tab_ThongKe && !_thongKeLoaded) _ = LoadThCabinAsync();
        }

        #region Khóa học

        /// <summary>Tải khóa học (tab Khóa học), khóa đang đào tạo (ô chọn) và danh sách cabin.</summary>
        public async Task InitializeAsync()
        {
            List<KhoaHocCabin> lst;
            List<Cabin> cabins;
            try
            {
                (lst, cabins) = await Task.Run(() => (_DAO.GetAllKhoaHoc(), _DAO.GetLstCabin()));
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không tải được danh sách khóa học cabin Eco", ex);
                return;
            }
            if (!IsAlive) return;

            grd_QuanLyKhoaHoc.DataSource = lst;
            BindCabin(cabins);

            // Status = false: khóa đang đào tạo
            var dangDaoTao = lst.Where(x => !x.Status).OrderBy(x => x.TenKhoaHoc).ToList();
            BuildCourseMenus(dangDaoTao);
            cbb_KhoaHocEco.Properties.DataSource = dangDaoTao;
            cbb_KhoaHocEco.Properties.DisplayMember = "TenKhoaHoc";
            cbb_KhoaHocEco.Properties.ValueMember = "KhoaHocID";

            if (dangDaoTao.Count == 0)
            {
                lbl_HvSummary.Text = "Chưa có khóa nào đang đào tạo. Đổi trạng thái khóa ở tab Khóa học.";
                return;
            }
            int chon = IdKhoaHoc != 0 && dangDaoTao.Any(x => x.KhoaHocID == IdKhoaHoc) ? IdKhoaHoc : dangDaoTao[0].KhoaHocID;
            if (Equals(cbb_KhoaHocEco.EditValue, chon))
                LoadLstHvByKH(chon);
            else
                cbb_KhoaHocEco.EditValue = chon;
        }

        private void BindCabin(List<Cabin> cabins)
        {
            object giuLai = cbb_Cabin.EditValue;
            cbb_Cabin.Properties.DataSource = cabins;
            cbb_Cabin.EditValue = cabins.Any(c => Equals(c.IdThietBi, giuLai)) ? giuLai : cabins.FirstOrDefault()?.IdThietBi;

            mn_TransferCabin.DropDownItems.Clear();
            foreach (var item in cabins)
                mn_TransferCabin.DropDownItems.Add(new ToolStripMenuItem(item.NameCabin, null, MenuItemChangeCabin_Click) { Tag = item });
        }

        /// <summary>Menu con "chuyển sang khóa" (toàn bộ kết quả / phiên đã chọn) lấy từ các khóa đang đào tạo.</summary>
        private void BuildCourseMenus(List<KhoaHocCabin> dangDaoTao)
        {
            mn_ChangeCourse.DropDownItems.Clear();
            mn_ChuyenKhoaHoc.DropDownItems.Clear();
            foreach (var item in dangDaoTao)
            {
                mn_ChangeCourse.DropDownItems.Add(new ToolStripMenuItem(item.TenKhoaHoc, null, MenuItem_Click) { Tag = item });
                mn_ChuyenKhoaHoc.DropDownItems.Add(new ToolStripMenuItem(item.TenKhoaHoc, null, MenuItemChangeCourse_Click) { Tag = item });
            }
        }

        private async void btn_RefreshKH_Click(object sender, EventArgs e)
        {
            await InitializeAsync();
        }

        private void gridView_QLKhoaHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Trước đây đọc rowData.KhoaHocID trước khi kiểm tra null → lỗi khi focus vào dòng nhóm
            KhoaHocCabin rowData = gridView_QLKhoaHoc.GetFocusedRow() as KhoaHocCabin;
            bool coKhoa = rowData != null;
            btn_SaveKH.Enabled = coKhoa;
            txt_TenKhoaHoc.Properties.ReadOnly = !coKhoa;
            txt_HangDT.Properties.ReadOnly = !coKhoa;
            cbb_TrangThai.Properties.ReadOnly = !coKhoa;
            if (!coKhoa) return;

            txt_MaKH.Text = rowData.MaKhoaHoc;
            txt_TenKhoaHoc.Text = rowData.TenKhoaHoc;
            date_NgayKG.Text = rowData.NgayKhaiGiang.ToString("dd/MM/yyyy");
            date_NgayBG.Text = rowData.NgayBeGiang.ToString("dd/MM/yyyy");
            txt_MaBCI.Text = rowData.MaBC1;
            txt_HangDT.Text = rowData.HangDaoTao;
            cbb_TrangThai.Text = rowData.Status ? DaKetThuc : DangDaoTao;
        }

        private void gridView_QLKhoaHoc_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == Status && e.Value is bool daKetThuc)
                e.DisplayText = daKetThuc ? DaKetThuc : DangDaoTao;
        }

        private async void btn_SaveKH_Click(object sender, EventArgs e)
        {
            var khoaHoc = gridView_QLKhoaHoc.GetFocusedRow() as KhoaHocCabin;
            if (khoaHoc == null) return;

            string tenkhoahoc = txt_TenKhoaHoc.Text.Trim();
            string hangdaotao = txt_HangDT.Text.Trim();
            if (tenkhoahoc.Length == 0 || hangdaotao.Length == 0)
            {
                XtraMessageBox.Show("Tên khóa học và hạng đào tạo không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                (tenkhoahoc.Length == 0 ? (Control)txt_TenKhoaHoc : txt_HangDT).Focus();
                return;
            }

            int status = cbb_TrangThai.Text == DaKetThuc ? 1 : 0;
            int id = khoaHoc.KhoaHocID;
            try
            {
                await Task.Run(() => _DAO.UpdateKhoaHoc(id, tenkhoahoc, hangdaotao, status));
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

        private void cbb_KhoaHocEco_EditValueChanged(object sender, EventArgs e)
        {
            if (!(cbb_KhoaHocEco.EditValue is int id)) return;
            IdKhoaHoc = id;
            TenKH = cbb_KhoaHocEco.Text;
            LoadLstHvByKH(IdKhoaHoc);
        }

        /// <summary>Tải học viên của khóa ở luồng nền; <paramref name="focusMaDK"/> giữ lại học viên đang chọn.</summary>
        private async void LoadLstHvByKH(int idKhoaHoc, string focusMaDK = null)
        {
            int version = ++_hvVersion;
            _lstStudent = new List<HocVien>();
            grd_HocVien.DataSource = null;
            ShowDetail(null);
            gridview_HocVien.ShowLoadingPanel();
            lbl_HvSummary.Text = "Đang tải học viên...";
            btn_RefreshHV.Enabled = false;

            List<HocVien> lst;
            try
            {
                lst = await Task.Run(() => _DAO.LoadLstHocVienById(idKhoaHoc));
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

            _lstStudent = lst;
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

            if (!FocusStudent(focusMaDK)) FocusFirstDataRow(gridview_HocVien);
            // FocusedRowChanged không phát khi handle đang focus trùng với trước (vd. đổi khóa → vẫn dòng 0),
            // nên tự hiện chi tiết thay vì chờ sự kiện
            _detailDebounce.Stop();
            ShowDetail(gridview_HocVien.GetFocusedRow() as HocVien);
        }

        private bool FocusStudent(string maDK)
        {
            if (string.IsNullOrEmpty(maDK)) return false;
            int handle = gridview_HocVien.LocateByValue("MaDK", maDK);
            if (handle < 0) return false;
            gridview_HocVien.ClearSelection();
            gridview_HocVien.FocusedRowHandle = handle;
            gridview_HocVien.SelectRow(handle);
            gridview_HocVien.MakeRowVisible(handle);
            return true;
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
            // Chờ người dùng dừng di chuyển rồi mới tải chi tiết
            _detailDebounce.Stop();
            _detailDebounce.Start();
        }

        private void DetailDebounce_Tick(object sender, EventArgs e)
        {
            _detailDebounce.Stop();
            if (!IsAlive) return;
            ShowDetail(gridview_HocVien.GetFocusedRow() as HocVien);
        }

        /// <summary>Tải lại danh sách (thời gian / kết luận thay đổi) và giữ học viên đang xem.</summary>
        private void ReloadAfterChange(string maDK)
        {
            _detailDebounce.Stop();
            LoadLstHvByKH(IdKhoaHoc, maDK);
        }

        private void gridview_HocVien_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == Result && e.Value is bool ok)
                e.DisplayText = ok ? "Đạt" : "Chưa đạt";
            else if (e.Column == ToTalSecond && (e.Value as string) == "...")   // spGetLstHvByKhoaHoc trả "..." khi chưa có phiên nào
                e.DisplayText = "Chưa học";
        }

        private void gridview_HocVien_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridview_HocVien.RowCount > 0 || IdKhoaHoc == 0) return;
            if (!string.IsNullOrEmpty(gridview_HocVien.FindFilterText))
                UiTheme.DrawEmptyState(e, "Không tìm thấy học viên", "Thử từ khóa khác hoặc xóa ô tìm kiếm.");
        }

        #endregion

        #region Chi tiết học viên (khung bên phải)

        private void ShowDetail(HocVien hocVien)
        {
            if (hocVien == null)
            {
                IdHocVien = null;
                SoCCCD = null;
                ++_detailVersion;
                pnl_DetailHV.Visible = false;
                LayoutPanel.Invalidate();
                return;
            }
            if (IdHocVien == hocVien.MaDK && pnl_DetailHV.Visible) return;
            _ = LoadDetailAsync(hocVien.MaDK);
        }

        private sealed class ChiTietHV
        {
            public DataRow Info;
            public List<Result> KetQua;
        }

        private async Task LoadDetailAsync(string maDK)
        {
            int version = ++_detailVersion;
            IdHocVien = maDK;
            SoCCCD = null;
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
                    var dt = _DAO.LoadInfoHVByMaDK(maDK);
                    return new ChiTietHV
                    {
                        Info = dt.Rows.Count > 0 ? dt.Rows[0] : null,
                        KetQua = _DAO.lstResultById(maDK)
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

            // Ảnh có thể NULL: trước đây ép kiểu (byte[]) nên lỗi với học viên chưa có ảnh
            DataRow hv = data.Info;
            var oldImage = pic_Avatar.Image;
            pic_Avatar.Image = hv?["Images"] is byte[] anh && anh.Length > 0 ? ImageExtension.byteArrayToImage(anh) : null;
            if (!ReferenceEquals(oldImage, pic_Avatar.Image)) oldImage?.Dispose();

            if (hv == null)
            {
                lb_HoTen.Text = "Không tìm thấy học viên";
                lb_MaDK.Text = lb_Info1.Text = lb_Info2.Text = lb_KetQua.Text = "—";
            }
            else
            {
                SoCCCD = Convert.ToString(hv["SoCMT"]);
                string ngaySinh = hv["NgaySinh"] is DateTime ns ? ns.ToString("dd/MM/yyyy") : Convert.ToString(hv["NgaySinh"]);
                lb_HoTen.Text = Convert.ToString(hv["HoTen"]);
                lb_MaDK.Text = "Mã ĐK: " + Convert.ToString(hv["ID"]);
                lb_Info1.Text = $"Ngày sinh {ngaySinh}   ·   {hv["GioiTinh"]}   ·   CCCD {SoCCCD}";
                lb_Info2.Text = $"Khóa {hv["Ten"]}   ·   Hạng {hv["HangDaoTao"]}";
                lb_KetQua.Text = KetQuaText(data.KetQua);
            }

            grd_Result.DataSource = data.KetQua;
            GridHelper.EnableRowNumbering(gridView_Result, 36);
            gridView_Result.HideLoadingPanel();
            gridView_Result.ClearSelection();
            UpdateDetailButtons();
        }

        /// <summary>Số phiên, tổng thời gian và bài còn thiếu (bài chưa có phiên nào từ 60 giây trở lên).</summary>
        private string KetQuaText(List<Result> ketQua)
        {
            // Phiên định dạng mới không đọc được bài tập / thời lượng nên không tính được bài còn thiếu
            int moi = ketQua.Count(x => x.MaHoaMoi);
            if (moi > 0)
                return $"<b>{ketQua.Count}</b> phiên   ·   <color=217,119,6><b>{moi} phiên ghi bằng phần mềm cabin mới</b> "
                     + "(chưa giải mã được bài tập, giờ kết thúc, thời lượng)</color>";

            var daHoc = new HashSet<int>(ketQua.Where(x => x.TotalSecond >= 60).Select(x => x.BaiHocCaBinID));
            var conThieu = (_danhMucBaiTap ?? new List<Exercise>()).Where(t => !daHoc.Contains(t.ID)).ToList();
            var tong = TimeSpan.FromSeconds(ketQua.Sum(x => x.TotalSecond));
            string text = $"<b>{ketQua.Count}</b> phiên   ·   tổng <b>{(int)tong.TotalHours:00}:{tong.Minutes:00}:{tong.Seconds:00}</b>   ·   ";
            if (conThieu.Count == 0)
                return text + "<color=21,128,61><b>Đã học đủ tất cả bài tập</b></color>";
            string ds = string.Join(", ", conThieu.Take(4).Select(t => $"Bài {t.ID}"));
            if (conThieu.Count > 4) ds += $" … (+{conThieu.Count - 4})";
            return text + $"<color=185,28,28><b>Còn thiếu {conThieu.Count} bài:</b> {ds}</color>";
        }

        private void UpdateDetailButtons()
        {
            btn_SaveBT.Enabled = _baiTapDaDoi.Count > 0;
            btn_SaveBT.Text = _baiTapDaDoi.Count > 0 ? $"Lưu bài tập đã đổi ({_baiTapDaDoi.Count})" : "Lưu bài tập đã đổi";
            btn_DeleteBT.Enabled = SelectedResults().Count > 0;
            btn_PrintHV.Enabled = IdHocVien != null;
        }

        private List<Result> SelectedResults() => gridView_Result.GetSelectedRows()
            .Where(gridView_Result.IsDataRow)
            .Select(h => gridView_Result.GetRow(h) as Result)
            .Where(r => r != null)
            .ToList();

        // Đổi tên bài tập trên lưới: ghi nhận dòng đã đổi (trước đây chỉ lưu các dòng đang được chọn)
        private void gridView_Result_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != colR_TenBai || !(gridView_Result.GetRow(e.RowHandle) is Result r) || !(e.Value is int baiTap)) return;
            _baiTapDaDoi[r.DiemDanhNguoiLxID] = baiTap;
            UpdateDetailButtons();
        }

        private void gridView_Result_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) => UpdateDetailButtons();

        private void gridView_Result_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0 || !(grd_Result.DataSource is List<Result> lst) || e.ListSourceRowIndex >= lst.Count) return;
            if (!lst[e.ListSourceRowIndex].MaHoaMoi) return;
            if (e.Column == colR_MaBai || e.Column == colR_ThoiGian) e.DisplayText = "—";
            else if (e.Column == colR_TenBai) e.DisplayText = "Phần mềm cabin mới – chưa giải mã được";
            else if (e.Column == colR_KetThuc) e.DisplayText = "";
        }

        // Không cho đổi bài với phiên định dạng mới: ghi ID bài theo kiểu cũ sẽ làm hỏng dữ liệu của phiên đó
        private void gridView_Result_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gridView_Result.GetFocusedRow() is Result r && r.MaHoaMoi) e.Cancel = true;
        }

        private void gridView_Result_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (gridView_Result.GetRow(e.RowHandle) is Result moi && moi.MaHoaMoi && e.Column == colR_TenBai)
            {
                e.Appearance.ForeColor = UiTheme.TextMuted;
                e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Italic;
            }
            if (e.Column == colR_TenBai && gridView_Result.GetRow(e.RowHandle) is Result r && _baiTapDaDoi.ContainsKey(r.DiemDanhNguoiLxID))
            {
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(40, UiTheme.Warning);
                e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            }
        }

        private void gridView_Result_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_Result.RowCount > 0 || IdHocVien == null) return;
            UiTheme.DrawEmptyState(e, "Chưa có kết quả", "Học viên chưa có phiên tập cabin nào.");
        }

        private async void btn_SaveBT_Click(object sender, EventArgs e)
        {
            gridView_Result.CloseEditor();
            gridView_Result.UpdateCurrentRow();
            if (_baiTapDaDoi.Count == 0 || IdHocVien == null) return;

            var thayDoi = _baiTapDaDoi.ToList();
            string maDK = IdHocVien;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var item in thayDoi)
                        _DAO.UpdateBaiTap(item.Key, item.Value);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không lưu được bài tập", ex);
                return;
            }
            if (!IsAlive) return;
            ReloadAfterChange(maDK);
        }

        private async void btn_DeleteBT_Click(object sender, EventArgs e)
        {
            if (IdHocVien == null) return;
            var phien = SelectedResults();
            if (phien.Count == 0) return;

            string danhSach = string.Join("\n", phien.Take(8).Select(r => $"• Bài {r.BaiHocCaBinID} – {r.ThoiDiemBatDau:HH:mm dd/MM/yyyy}"));
            if (phien.Count > 8) danhSach += $"\n• … và {phien.Count - 8} phiên khác";
            if (XtraMessageBox.Show($"Xóa {phien.Count} phiên tập của {lb_HoTen.Text}?\n\n{danhSach}\n\nThao tác này không hoàn tác được.",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            string maDK = IdHocVien;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var r in phien)
                        _DAO.DeleteResultExcer((int)r.DiemDanhNguoiLxID);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không xóa được phiên tập", ex);
                return;
            }
            if (!IsAlive) return;
            ReloadAfterChange(maDK);
        }

        private void btn_PrintHV_Click(object sender, EventArgs e)
        {
            if (IdHocVien != null) ShowReportKetQua(IdHocVien);
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
                e.Graphics.DrawString("Ảnh, thông tin, bài còn thiếu và kết quả từng phiên cabin sẽ hiện ở đây.", hintFont, hint,
                    new System.Drawing.RectangleF(0, cy + 2, b.Width, 22), format);
            }
        }

        #endregion

        #region Chuyển khóa / chuyển cabin

        private void mn_Change_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mn_ChuyenKhoaHoc.Enabled = SelectedResults().Count > 0 && mn_ChuyenKhoaHoc.DropDownItems.Count > 0;
        }

        /// <summary>Mã ĐK của cùng học viên (theo CCCD) ở khóa đích; rỗng nếu học viên không có trong khóa đó.</summary>
        private async Task<string> FindMaDKInCourse(KhoaHocCabin khoa)
        {
            string cccd = SoCCCD;
            string maMoi = string.IsNullOrEmpty(cccd) ? string.Empty : await Task.Run(() => _DAO.GetMaDKNew(khoa.KhoaHocID, cccd));
            if (string.IsNullOrEmpty(maMoi))
                XtraMessageBox.Show($"Không có học viên cùng CCCD {cccd} trong khóa {khoa.TenKhoaHoc}.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            return maMoi;
        }

        private async Task MoveResults(List<Result> phien, KhoaHocCabin khoa)
        {
            string maMoi = await FindMaDKInCourse(khoa);
            if (string.IsNullOrEmpty(maMoi) || !IsAlive) return;
            if (maMoi == IdHocVien)
            {
                XtraMessageBox.Show("Các phiên này đã thuộc khóa được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (XtraMessageBox.Show($"Chuyển {phien.Count} phiên tập của {lb_HoTen.Text} sang khóa {khoa.TenKhoaHoc} (mã ĐK {maMoi})?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string maDK = IdHocVien;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var r in phien)
                        _DAO.ChangeCourse(khoa.KhoaHocID, maMoi, (int)r.DiemDanhNguoiLxID);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không chuyển được khóa", ex);
                return;
            }
            if (IsAlive) ReloadAfterChange(maDK);
        }

        // Chuyển toàn bộ kết quả của học viên sang khóa khác
        private async void MenuItem_Click(object sender, EventArgs e)
        {
            if (IdHocVien == null || !((sender as ToolStripMenuItem)?.Tag is KhoaHocCabin khoa)) return;
            if (!(grd_Result.DataSource is List<Result> tatCa) || tatCa.Count == 0)
            {
                XtraMessageBox.Show("Học viên chưa có kết quả để chuyển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            await MoveResults(tatCa.ToList(), khoa);
        }

        // Chuyển các phiên đang chọn sang khóa khác
        private async void MenuItemChangeCourse_Click(object sender, EventArgs e)
        {
            if (IdHocVien == null || !((sender as ToolStripMenuItem)?.Tag is KhoaHocCabin khoa)) return;
            var phien = SelectedResults();
            if (phien.Count == 0)
            {
                XtraMessageBox.Show("Chọn ít nhất một phiên tập cần chuyển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await MoveResults(phien, khoa);
        }

        // Chuyển cabin cho toàn bộ kết quả của học viên
        private async void MenuItemChangeCabin_Click(object sender, EventArgs e)
        {
            if (IdHocVien == null || !((sender as ToolStripMenuItem)?.Tag is Cabin cabin)) return;
            if (!(grd_Result.DataSource is List<Result> tatCa) || tatCa.Count == 0)
            {
                XtraMessageBox.Show("Học viên chưa có kết quả để chuyển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (XtraMessageBox.Show($"Chuyển {tatCa.Count} phiên tập của {lb_HoTen.Text} sang cabin {cabin.NameCabin}?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string maDK = IdHocVien;
            var phien = tatCa.ToList();
            try
            {
                await Task.Run(() =>
                {
                    foreach (var r in phien)
                        _DAO.ChangeCabin(cabin, (int)r.DiemDanhNguoiLxID);
                });
            }
            catch (Exception ex)
            {
                ShowError("Không chuyển được cabin", ex);
                return;
            }
            if (IsAlive) ReloadAfterChange(maDK);
        }

        #endregion

        #region Menu & báo cáo

        private void contextMenuStrip_DTT_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool coHocVien = gridview_HocVien.GetFocusedRow() is HocVien && IdHocVien != null;
            bool coKhoa = IdKhoaHoc != 0;
            mn_ChangeCourse.Enabled = coHocVien && mn_ChangeCourse.DropDownItems.Count > 0;
            mn_TransferCabin.Enabled = coHocVien && mn_TransferCabin.DropDownItems.Count > 0;
            mn_PrintAllReport.Enabled = coKhoa;
            mn_TongHopBaoCao.Enabled = mn_HocVienDuDieuKien.Enabled = mn_HocVienKoDuDieuKien.Enabled = coKhoa;
            mn_SyncTeacher.Enabled = coKhoa;

            // Các mục ẩn chỉ hiện khi giữ Ctrl + Shift (như bản cũ)
            bool an = Control.ModifierKeys != (Keys.Shift | Keys.Control);
            toolStripSeparator4.Visible = mn_CreateAll.Visible = mn_Create01Result.Visible = !an;
        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc == 0) return;
            cms_BaoCao.Show(btn_BaoCao, new System.Drawing.Point(0, btn_BaoCao.Height));
        }

        private void mn_refresh_Click(object sender, EventArgs e)
        {
            if (IdKhoaHoc != 0) LoadLstHvByKH(IdKhoaHoc, IdHocVien);
        }

        private async void mn_ThemKhoaHoc_Click(object sender, EventArgs e)
        {
            if (ThemKhoaHoc()) await InitializeAsync();
        }

        private void ShowReportKetQua(string lstMaDK)
        {
            DataTable resultBC;
            try
            {
                Cursor = Cursors.WaitCursor;
                resultBC = _DAO.GetDataReportByMaDK(lstMaDK);
            }
            catch (Exception ex)
            {
                ShowError("Không tải được dữ liệu báo cáo", ex);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            if (resultBC == null || resultBC.Rows.Count == 0)
            {
                XtraMessageBox.Show("Học viên chưa có kết quả học cabin Eco.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            new ReportPrintTool(new rptResultEco { DataSource = resultBC }).ShowPreview();
        }

        // In kết quả các học viên đang chọn (Ctrl/Shift + bấm để chọn nhiều)
        private void mn_PrintAllReport_Click(object sender, EventArgs e)
        {
            var maDK = gridview_HocVien.GetSelectedRows()
                .Where(gridview_HocVien.IsDataRow)
                .Select(h => (gridview_HocVien.GetRow(h) as HocVien)?.MaDK)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            if (maDK.Count == 0)
            {
                XtraMessageBox.Show("Chọn học viên cần in kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ShowReportKetQua(string.Join(",", maDK));
        }

        private void mn_BcKetQua_Click(object sender, EventArgs e)
        {
            var maDK = _lstStudent.Select(x => x.MaDK).Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (maDK.Count == 0) return;
            ShowReportKetQua(string.Join(",", maDK));
        }

        private void ShowBaoCaoTongHop(int flag, string khongCo)
        {
            if (IdKhoaHoc == 0) return;
            DataTable resultBC;
            try
            {
                Cursor = Cursors.WaitCursor;
                resultBC = _DAO.GetBaoCaoByKhoaHocID(IdKhoaHoc, flag);
            }
            catch (Exception ex)
            {
                ShowError("Không tải được dữ liệu báo cáo", ex);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            if (resultBC == null || resultBC.Rows.Count == 0)
            {
                XtraMessageBox.Show(khongCo, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            new ReportPrintTool(new rptBaoCaoTongHopEco { DataSource = resultBC }).ShowPreview();
        }

        private void mn_TongHopBaoCao_Click(object sender, EventArgs e) => ShowBaoCaoTongHop(0, "Khóa chưa có dữ liệu tổng hợp.");

        private void mn_HocVienDuDieuKien_Click(object sender, EventArgs e) => ShowBaoCaoTongHop(1, "Không có học viên đủ điều kiện thi.");

        private void mn_HocVienKoDuDieuKien_Click(object sender, EventArgs e) => ShowBaoCaoTongHop(2, "Không có học viên chưa đủ điều kiện thi.");

        /// <summary>Thêm giáo viên mới từ DAT sang cabin Eco rồi gán giáo viên cho học viên của khóa theo DAT.</summary>
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
                        .Where(x => x.MaDK != null)
                        .GroupBy(x => x.MaDK).ToDictionary(g => g.Key, g => g.First().GiaoVienID);
                    int gan = 0;
                    foreach (HocVien hv in _DAO.LoadLstHocVienById(idKhoaHoc))
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
            LoadLstHvByKH(idKhoaHoc, IdHocVien);
        }

        #endregion

        #region Tạo kết quả (mục ẩn, giữ nguyên logic cũ)

        private void mn_CreateAll_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure add all result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (!(gridview_HocVien.GetFocusedRow() is HocVien rowData) || !(cbb_Cabin.GetSelectedDataRow() is Cabin selectedRow)) return;

                string IdHocVien = rowData.MaDK.ToString();
                int IdCabin = selectedRow.IdThietBi;
                string ghichu = selectedRow.GhiChu;

                string Datecreate = dt_TimeStart.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                List<int> lst = new List<int>() { 1, 3, 4, 5, 6, 7, 8 };
                int LanTap = 1;

                Random rd = new Random();
                bool isB11orB2 = rowData.HangGPLX == "B11" || rowData.HangGPLX == "B2";
                int minute = rd.Next(isB11orB2 ? 18 : 12, isB11orB2 ? 28 : 17);

                _DAO.CreateResult(IdCabin, ghichu, IdHocVien, 2, Datecreate, IdKhoaHoc, LanTap, LanTap, minute);

                while (lst.Count != 0)
                {
                    int ID = _DAO.GetIdMaxByIdHocVien(IdHocVien);
                    DateTime timeout = _DAO.GetTimeOut(ID);

                    int index = rd.Next(lst.Count);
                    int IdBaiTap = lst[index];
                    Datecreate = timeout.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    LanTap++;

                    int minute2 = rd.Next(isB11orB2 ? 18 : 12, isB11orB2 ? 28 : 17);
                    _DAO.CreateResult(IdCabin, ghichu, IdHocVien, IdBaiTap, Datecreate, IdKhoaHoc, LanTap, LanTap, minute2);
                    lst.Remove(IdBaiTap);
                }

                ReloadAfterChange(rowData.MaDK);
            }
        }

        private void mn_Create01Result_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure add 01 result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (!(gridview_HocVien.GetFocusedRow() is HocVien rowData) || !(cbb_Cabin.GetSelectedDataRow() is Cabin selectedRow)) return;

                string IdHocVien = rowData.MaDK.ToString();
                int IdCabin = selectedRow.IdThietBi;
                string ghichu = selectedRow.GhiChu;

                string Datecreate = dt_TimeStart.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                int LanTap = _DAO.GetMaxLanTap(IdHocVien);

                string day = dt_TimeStart.DateTime.ToString("dd/MM/yyyy");
                int count = _DAO.CountLanTapByDay(day, IdHocVien);

                if (count > 0)
                {
                    int MaxID_BaiTap = _DAO.GetMaxIdBaiTap(IdHocVien);
                    int ID = _DAO.GetIdMaxByIdHocVien(IdHocVien);
                    DateTime timeout = _DAO.GetTimeOut(ID);
                    Datecreate = timeout.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    Random rd = new Random();
                    bool isB11orB2 = rowData.HangGPLX == "B11" || rowData.HangGPLX == "B2";
                    int minute = rd.Next(isB11orB2 ? 18 : 12, isB11orB2 ? 28 : 17);

                    if (MaxID_BaiTap < 8)
                    {
                        MaxID_BaiTap++;
                    }
                    LanTap++;

                    if (checkbox.Checked)
                    {
                        _DAO.CreateResult(IdCabin, ghichu, IdHocVien, 3, Datecreate, IdKhoaHoc, LanTap, LanTap, rd.Next(30, 34));
                    }
                    else
                    {
                        _DAO.CreateResult(IdCabin, ghichu, IdHocVien, MaxID_BaiTap, Datecreate, IdKhoaHoc, LanTap, LanTap, minute);
                    }
                }
                else
                {
                    LanTap++;
                    _DAO.CreateResult(IdCabin, ghichu, IdHocVien, 2, Datecreate, IdKhoaHoc, LanTap, LanTap, 0);
                }

                ReloadAfterChange(rowData.MaDK);
            }
        }

        #endregion

        #region Thống kê theo ngày

        private async Task LoadThCabinAsync()
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
            _ = LoadThCabinAsync();
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

        private void Mn_ViewResult_Click(object sender, EventArgs e)
        {
            if (gridView_TongHop.GetFocusedRow() is ThongKe rowData && !string.IsNullOrEmpty(rowData.MaDK))
                ShowReportKetQua(rowData.MaDK);
        }

        private void btn_ExportTK_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"ThongKeCabinEco_{dtp_DayTHCabin.DateTime:yyyyMMdd}.xlsx"
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

        #region Nhập khóa học từ XML

        /// <summary>Nhập khóa học + học viên từ file XML báo cáo 1. Trả về true nếu nhập thành công.</summary>
        private bool ThemKhoaHoc()
        {
            string filePath;
            using (var openFileDialog = new OpenFileDialog { Filter = "File XML báo cáo 1|*.xml", ValidateNames = true, Multiselect = false })
            {
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return false;
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
                    return false;
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
                    XtraMessageBox.Show($"Khóa {kh.TenKhoaHoc} ({kh.MaKhoaHoc}) đã có trên cabin Eco. Vẫn nhập lại học viên?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return false;

                _DAO.InsertKhoaHoc(kh);
                int khoahocId = _DAO.GetKhoaHocIdByMaKhoaHoc(kh.MaKhoaHoc);

                int STT = 0;
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
                        SoCMT = item.Element("SO_CMT")?.Value,
                        NgayCapCMT = ParseDate(item.Element("NGAY_CAP_CMT")?.Value),
                        HangGPLX = (string)item.Element("HO_SO")?.Element("HANG_GPLX"),
                        ImageBase64 = (string)item.Element("HO_SO")?.Element("ANH_CHAN_DUNG")
                    };
                    STT++;
                    _DAO.InsertNguoiLxBase(khoahocId, STT, hv, ConvertBase64ToBytes(hv.ImageBase64));
                }

                XtraMessageBox.Show($"Đã nhập khóa {kh.TenKhoaHoc} với {STT} học viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                ShowError("Không nhập được khóa học", ex);
                return false;
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
