using DevExpress.Export;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_DaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly KhoaHocDatDAO _khoaHocDao = new KhoaHocDatDAO();
        private readonly GiaoVienDatDAO _giaoVienDao = new GiaoVienDatDAO();
        private readonly XeTapLaiDAO _xeTapLaiDao = new XeTapLaiDAO();
        private readonly BaoCaoDatDAO _baoCaoDao = new BaoCaoDatDAO();
        private readonly HocVienDatDAO _hocVienDao = new HocVienDatDAO();

        // Cấp của node sau khi bỏ node gốc "Khoá học lái xe": Năm -> Tháng -> Khóa
        private const int LevelNam = 0;
        private const int LevelThang = 1;
        private const int LevelKhoa = 2;

        private static readonly Font NodeBold = new Font("Roboto", 9.75F, FontStyle.Bold);
        private static readonly Font SmallBold = new Font("Roboto", 8.25F, FontStyle.Bold);
        private static readonly Font SmallFont = new Font("Roboto", 8.25F);
        private static readonly Color FocusTint = Color.FromArgb(225, 232, 244);

        private int MonthId;          // Tháng dùng cho "Bố trí xe STĐ" (node tháng, hoặc tháng chứa khóa)
        private int KhoaHocId;        // Khóa đang hiển thị danh sách
        private string TenKhoaHoc;
        private int _loadVersion;     // Bỏ kết quả của lần tải cũ khi người dùng đã chọn khóa khác
        private bool _loading;
        private List<NguoiLxInDAT> _hocVien = new List<NguoiLxInDAT>();

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public Uc_DaoTao()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pnl_Header.Paint += UiTheme.PaintHeader;

            UiTheme.SetIcon(btn_Refresh, "svgimages/icon%20builder/actions_refresh.svg");
            UiTheme.SetIcon(btn_Export, "svgimages/export/exporttoxlsx.svg");
            UiTheme.SetIcon(btn_BaoCao, "svgimages/print/preview.svg");
            UiTheme.SetIcon(btn_BoTriXe, "svgimages/business%20objects/bo_scheduler.svg");
            UiTheme.StylePrimary(btn_BoTriXe);

            UiTheme.StyleGrid(gridView_DanhSachHocVien, "Tìm theo mã ĐK, họ tên, CCCD, giáo viên, biển số xe...");
            gridView_DanhSachHocVien.OptionsView.ShowGroupPanel = false;
            gridView_DanhSachHocVien.OptionsFind.ShowFindButton = false;
            gridView_DanhSachHocVien.OptionsFind.ShowClearButton = false;
            treeList_Category.OptionsFind.ShowFindButton = false;
            treeList_Category.OptionsFind.ShowClearButton = false;

            ApplySkinColors();
            UserLookAndFeel.Default.StyleChanged += Default_StyleChanged;
            Disposed += (s, e) => UserLookAndFeel.Default.StyleChanged -= Default_StyleChanged;

            UpdateCommandState();
        }

        private void Default_StyleChanged(object sender, EventArgs e)
        {
            ApplySkinColors();
            pnl_Kpi.Invalidate();
        }

        private bool IsDarkSkin => UiTheme.IsDarkSkin(LookAndFeel);

        private void ApplySkinColors()
        {
            // Nền sáng + chữ xanh trên skin sáng; nền xanh đậm + chữ trắng trên skin tối
            var focused = treeList_Category.Appearance.FocusedRow;
            focused.BackColor = IsDarkSkin ? UiTheme.BrandDark : FocusTint;
            focused.ForeColor = IsDarkSkin ? Color.White : UiTheme.Brand;
            focused.Font = NodeBold;
            focused.Options.UseBackColor = true;
            focused.Options.UseForeColor = true;
            focused.Options.UseFont = true;
            treeList_Category.Appearance.HideSelectionRow.Assign(focused);

            lbl_CourseName.Appearance.ForeColor = IsDarkSkin ? Color.FromArgb(147, 176, 222) : UiTheme.Brand;
            lbl_CourseName.Appearance.Options.UseForeColor = true;
        }

        private async void Uc_DaoTao_Load(object sender, EventArgs e)
        {
            await LoadCategoryAsync(selectLatest: true);
            await LoadLookupsAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                btn_Refresh.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Tải dữ liệu

        private async Task LoadCategoryAsync(bool selectLatest)
        {
            int keepFocus = treeList_Category.FocusedNode != null ? Convert.ToInt32(treeList_Category.FocusedNode["MonthID"]) : 0;

            DataTable data;
            try
            {
                data = await Task.Run(() =>
                {
                    DataTable dt = _khoaHocDao.GetCategory();
                    var rows = dt.AsEnumerable()
                        .Where(row => row.Field<int>("Level") != 0)
                        .OrderByDescending(row => row.Field<int>("MonthID"));
                    return rows.Any() ? rows.CopyToDataTable() : dt.Clone();
                });
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không tải được danh sách khóa học", ex);
                return;
            }
            if (!IsAlive) return;

            treeList_Category.BeginUpdate();
            try
            {
                treeList_Category.DataSource = data;
                treeList_Category.CollapseAll();
            }
            finally
            {
                treeList_Category.EndUpdate();
            }

            int khoa = data.AsEnumerable().Count(r => r.Field<int>("Level") == 3);
            grp_KhoaHoc.Text = $"KHÓA HỌC  ·  {khoa} khóa";

            TreeListNode target = keepFocus != 0 ? treeList_Category.FindNodeByKeyID(keepFocus) : null;
            if (target == null && selectLatest)
                target = FirstCourseNode();

            if (target != null)
            {
                target.ParentNode?.ParentNode?.Expand();
                target.ParentNode?.Expand();
                treeList_Category.FocusedNode = target;
                treeList_Category.MakeNodeVisible(target);
            }
            else
            {
                // Mặc định mở năm mới nhất để người dùng thấy ngay các tháng
                if (treeList_Category.Nodes.Count > 0) treeList_Category.Nodes[0].Expand();
            }
        }

        private TreeListNode FirstCourseNode()
        {
            foreach (TreeListNode year in treeList_Category.Nodes)
                foreach (TreeListNode month in year.Nodes)
                    if (month.Nodes.Count > 0)
                        return month.Nodes[0];
            return null;
        }

        private async Task LoadLookupsAsync()
        {
            List<XeTapLai> xe;
            List<GiaoVien> gv;
            try
            {
                var xeTask = Task.Run(() => _xeTapLaiDao.GetLstXeTapLai());
                var gvTask = Task.Run(() => _giaoVienDao.GetAllGV());
                xe = await xeTask;
                gv = await gvTask;
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không tải được danh mục giáo viên / xe tập lái", ex);
                return;
            }
            if (!IsAlive) return;

            repoGrid_XeTapLai.DataSource = xe;
            repoGrid_XeTapLai.DisplayMember = "BienSoXe";
            repoGrid_XeTapLai.ValueMember = "MaXe";
            SetPopupColumns(repoGrid_XeTapLai.PopupView, ("BienSoXe", "Biển số"), ("MaXe", "Mã xe"), ("HangGPLX", "Hạng"));

            repoGrid_GiaoVien.DataSource = gv;
            repoGrid_GiaoVien.DisplayMember = "HoTenGV";
            repoGrid_GiaoVien.ValueMember = "GiaoVienID";
            SetPopupColumns(repoGrid_GiaoVien.PopupView, ("HoTenGV", "Họ tên"), ("NgaySinh", "Ngày sinh"), ("HangGPLX", "Hạng GPLX"));

            // Tên GV / biển số chỉ hiển thị được sau khi có danh mục
            gridView_DanhSachHocVien.RefreshData();
        }

        private static void SetPopupColumns(ColumnView view, params (string field, string caption)[] columns)
        {
            view.Columns.Clear();
            foreach (var (field, caption) in columns)
            {
                var col = view.Columns.AddField(field);
                col.Caption = caption;
                col.Visible = true;
            }
        }

        public async void LoadDanhSachHocVien(int khoaHocId)
        {
            int version = ++_loadVersion;
            KhoaHocId = khoaHocId;
            SetLoading(true);

            List<NguoiLxInDAT> lst;
            try
            {
                lst = await Task.Run(() => _hocVienDao.GetAllNguoiLxByKhoaHocID(khoaHocId));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _loadVersion) return;
                SetLoading(false);
                ShowError("Không tải được danh sách học viên", ex);
                return;
            }
            if (!IsAlive || version != _loadVersion) return;

            _hocVien = lst;
            gridView_DanhSachHocVien.BeginUpdate();
            try
            {
                gridCtrl_DanhSachHocVien.DataSource = lst;
                GridHelper.EnableRowNumbering(gridView_DanhSachHocVien, 40);
                gridView_DanhSachHocVien.BestFitColumns();
            }
            finally
            {
                gridView_DanhSachHocVien.EndUpdate();
            }
            SetLoading(false);
            gridView_DanhSachHocVien.MoveFirst();
            gridView_DanhSachHocVien.TopRowIndex = 0;
        }

        private void ClearDanhSach()
        {
            ++_loadVersion;
            KhoaHocId = 0;
            _hocVien = new List<NguoiLxInDAT>();
            gridCtrl_DanhSachHocVien.DataSource = null;
            SetLoading(false);
        }

        private void SetLoading(bool loading)
        {
            _loading = loading;
            if (loading)
            {
                // Không để danh sách / KPI của khóa trước hiển thị như thể thuộc khóa mới
                _hocVien = new List<NguoiLxInDAT>();
                gridCtrl_DanhSachHocVien.DataSource = null;
                gridView_DanhSachHocVien.ShowLoadingPanel();
            }
            else
            {
                gridView_DanhSachHocVien.HideLoadingPanel();
            }
            pnl_Kpi.Invalidate();
            UpdateCommandState();
        }

        #endregion

        #region Cây khóa học

        private void treeList_Category_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null)
            {
                MonthId = 0;
                ShowCourseHeader(null);
                ClearDanhSach();
                return;
            }

            int monthId = Convert.ToInt32(node["MonthID"]);
            if (node.Level == LevelKhoa)
            {
                // Khóa nằm trong tháng: "Bố trí xe STĐ" mở theo tháng chứa khóa
                MonthId = Convert.ToInt32(node["ParentMonthID"]);
                ShowCourseHeader(node);
                int khoaHocId = GetKhoaHocId(node);
                // TreeList có thể báo đổi node 2 lần cho cùng một khóa khi nạp lại cây: không truy vấn lại
                if (khoaHocId != KhoaHocId) LoadDanhSachHocVien(khoaHocId);
            }
            else
            {
                MonthId = node.Level == LevelThang ? monthId : 0;
                ShowCourseHeader(node);
                ClearDanhSach();
            }
        }

        private static int GetKhoaHocId(TreeListNode node)
        {
            object id = node["KhoaHocID"];
            // Dữ liệu cũ: MonthID của node khóa = KhoaHocID * 10000
            return id == null || id == DBNull.Value ? Convert.ToInt32(node["MonthID"]) / 10000 : Convert.ToInt32(id);
        }

        private void ShowCourseHeader(TreeListNode node)
        {
            if (node == null)
            {
                TenKhoaHoc = null;
                lbl_CourseName.Text = "Chưa chọn khóa học";
                lbl_CourseInfo.Text = "Chọn một khóa học trong cây bên trái.";
                return;
            }

            if (node.Level != LevelKhoa)
            {
                TenKhoaHoc = null;
                lbl_CourseName.Text = Convert.ToString(node["Name"]);
                int count = node.Level == LevelThang
                    ? node.Nodes.Count
                    : node.Nodes.Cast<TreeListNode>().Sum(m => m.Nodes.Count);
                lbl_CourseInfo.Text = node.Level == LevelThang
                    ? $"{count} khóa học trong tháng  ·  Chọn một khóa để xem học viên, hoặc bấm \"Bố trí xe STĐ\" để phân công cho cả tháng."
                    : $"{count} khóa học trong năm  ·  Mở một tháng để xem các khóa.";
                return;
            }

            TenKhoaHoc = Convert.ToString(node["Name"]);
            lbl_CourseName.Text = TenKhoaHoc;

            var parts = new List<string>();
            AddPart(parts, "Mã khóa", node["MaKH"]);
            AddPart(parts, "Hạng", node["HangDaoTao"]);
            AddDatePart(parts, "Khai giảng", node["NgayKG"]);
            AddDatePart(parts, "Bế giảng", node["NgayBG"]);
            AddDatePart(parts, "Sát hạch", node["NgaySatHach"]);
            AddDatePart(parts, "Hạn đào tạo", node["ngayDaoTao"]);
            lbl_CourseInfo.Text = string.Join("   ·   ", parts);
        }

        private static void AddPart(List<string> parts, string label, object value)
        {
            string text = Convert.ToString(value);
            if (!string.IsNullOrWhiteSpace(text)) parts.Add($"{label}: {text.Trim()}");
        }

        private static void AddDatePart(List<string> parts, string label, object value)
        {
            if (value is DateTime d) parts.Add($"{label}: {d:dd/MM/yyyy}");
        }

        private void treeList_Category_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.Column == Col_Hang)
            {
                // Hạng đào tạo chỉ có ở node khóa: hiển thị dạng nhãn nhỏ, màu nhạt
                if (e.Node.Level != LevelKhoa || string.IsNullOrWhiteSpace(e.CellText)) return;
                e.Appearance.ForeColor = UiTheme.TextMuted;
                e.Appearance.Font = SmallBold;
                return;
            }

            if (e.Node.Level == LevelNam)
            {
                e.Appearance.Font = NodeBold;
                if (!e.Node.Focused && !IsDarkSkin) e.Appearance.ForeColor = UiTheme.Brand;
            }
        }

        private void treeList_Category_CustomDrawEmptyArea(object sender, CustomDrawEmptyAreaEventArgs e)
        {
            if (treeList_Category.Nodes.Count > 0) return;
            using (var brush = new SolidBrush(UiTheme.TextMuted))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                e.Cache.DrawString("Chưa có khóa học", SmallFont, brush, e.Bounds, format);
            e.Handled = true;
        }

        private void treeList_Category_MouseDown(object sender, MouseEventArgs e)
        {
            // Chuột phải chọn luôn node dưới con trỏ để menu ngữ cảnh làm việc đúng khóa
            if (e.Button != MouseButtons.Right) return;
            var hit = treeList_Category.CalcHitInfo(e.Location);
            if (hit.Node != null) treeList_Category.FocusedNode = hit.Node;
        }

        private void contextMenuStrip_KhoaHoc_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var node = treeList_Category.FocusedNode;
            if (node == null || node.Level == LevelNam)
            {
                e.Cancel = true;
                return;
            }

            bool isKhoa = node.Level == LevelKhoa;
            mn_XemDanhSach.Visible = isKhoa;
            mn_Baocaokhoahoc.Visible = isKhoa;
            toolStripSeparator1.Visible = isKhoa;
            MenuItem_BoTriXeSTD.Enabled = MonthId != 0;
        }

        private void mn_XemDanhSach_Click(object sender, EventArgs e)
        {
            if (KhoaHocId != 0) LoadDanhSachHocVien(KhoaHocId);
        }

        #endregion

        #region KPI khóa học

        private void pnl_Kpi_Resize(object sender, EventArgs e) => pnl_Kpi.Invalidate();

        private void pnl_Kpi_Paint(object sender, PaintEventArgs e)
        {
            int total = _hocVien.Count;
            int coGV = _hocVien.Count(x => x.GiaoVienID != 0);
            int coXe = _hocVien.Count(x => !string.IsNullOrWhiteSpace(x.MaXeTap));
            int daHoc = _hocVien.Count(x => x.TongThoiGianTichLuy > 0);
            double km = _hocVien.Sum(x => x.TongQuangDuongTichLuy ?? 0);
            bool none = KhoaHocId == 0 || _loading;

            var cards = new[]
            {
                new UiTheme.KpiCard("HỌC VIÊN", none ? "—" : total.ToString("n0"),
                    _loading ? "Đang tải..." : none ? "Chưa chọn khóa" : "trong khóa", UiTheme.Brand),
                new UiTheme.KpiCard("ĐÃ PHÂN CÔNG GV", none ? "—" : $"{coGV}/{total}",
                    none ? "" : total - coGV > 0 ? $"{total - coGV} học viên chưa có GV" : "Đầy đủ", UiTheme.Success, !none && total - coGV > 0),
                new UiTheme.KpiCard("ĐÃ GÁN XE TẬP", none ? "—" : $"{coXe}/{total}",
                    none ? "" : total - coXe > 0 ? $"{total - coXe} học viên chưa có xe" : "Đầy đủ", UiTheme.Success, !none && total - coXe > 0),
                new UiTheme.KpiCard("ĐÃ CÓ DỮ LIỆU DAT", none ? "—" : $"{daHoc}/{total}",
                    none ? "" : $"Tổng {km:n0} km", UiTheme.Warning),
            };

            UiTheme.DrawKpiCards(e.Graphics, pnl_Kpi.ClientRectangle, cards, UiTheme.SkinText(LookAndFeel));
        }

        #endregion

        #region Lưới học viên

        private void gridView_DanhSachHocVien_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == TongThoiGianTichLuy && e.Value is int seconds)
            {
                // Hiển thị tổng giờ thực, sắp xếp vẫn theo số giây
                var ts = TimeSpan.FromSeconds(seconds);
                e.DisplayText = $"{(int)ts.TotalHours:00}:{ts.Minutes:00}";
            }
            else if (e.Column == GiaoVienID && Equals(e.Value, 0))
            {
                e.DisplayText = "Chưa phân công giáo viên";
            }
            else if ((e.Column == MaXeTap || e.Column == MaXeTapB11) && string.IsNullOrWhiteSpace(e.Value as string))
            {
                e.DisplayText = e.Column == MaXeTap ? "Chưa gán xe" : "—";
            }
        }

        private void gridView_DanhSachHocVien_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column == GiaoVienID && Equals(e.CellValue, 0)
                || e.Column == MaXeTap && string.IsNullOrWhiteSpace(e.CellValue as string))
            {
                e.Appearance.ForeColor = UiTheme.Warning;
                e.Appearance.FontStyleDelta = FontStyle.Italic;
            }
            else if (e.Column == MaXeTapB11 && string.IsNullOrWhiteSpace(e.CellValue as string))
            {
                e.Appearance.ForeColor = UiTheme.TextMuted;
            }
            else if (e.Column == MaXeTapB11)
            {
                e.Appearance.ForeColor = UiTheme.Brand;
            }
        }

        private void gridView_DanhSachHocVien_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_DanhSachHocVien.RowCount > 0 || _loading) return;
            if (KhoaHocId == 0)
                UiTheme.DrawEmptyState(e, "Chưa chọn khóa học", "Chọn một khóa học trong cây bên trái để xem danh sách học viên.");
            else if (string.IsNullOrEmpty(gridView_DanhSachHocVien.FindFilterText))
                UiTheme.DrawEmptyState(e, "Khóa học chưa có học viên", "Danh sách học viên sẽ hiển thị khi khóa được đồng bộ từ GPLX.");
            else
                UiTheme.DrawEmptyState(e, "Không tìm thấy học viên", "Thử từ khóa khác hoặc xóa ô tìm kiếm.");
        }

        private void gridView_DanhSachHocVien_DoubleClick(object sender, EventArgs e)
        {
            // Chỉ mở khi bấm đúp vào dòng dữ liệu (không phải header, dòng nhóm, vùng trống)
            var pt = gridCtrl_DanhSachHocVien.PointToClient(Control.MousePosition);
            var hit = gridView_DanhSachHocVien.CalcHitInfo(pt);
            if (!hit.InDataRow) return;
            OpenQuaTrinhDaoTao(gridView_DanhSachHocVien.GetRow(hit.RowHandle) as NguoiLxInDAT);
        }

        private void gridView_DanhSachHocVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            OpenQuaTrinhDaoTao(gridView_DanhSachHocVien.GetFocusedRow() as NguoiLxInDAT);
            e.Handled = true;
        }

        private void OpenQuaTrinhDaoTao(NguoiLxInDAT hocVien)
        {
            if (string.IsNullOrEmpty(hocVien?.MaDK)) return;

            // Form chỉ xem, không thay đổi dữ liệu học viên nên không cần tải lại danh sách khi đóng
            using (var f = new frmQuaTrinhDaoTao(hocVien.MaDK))
                f.ShowDialog(FindForm());
        }

        #endregion

        #region Lệnh

        private void UpdateCommandState()
        {
            bool hasKhoa = KhoaHocId != 0 && !_loading;
            btn_BaoCao.Enabled = hasKhoa;
            btn_Export.Enabled = hasKhoa && _hocVien.Count > 0;
            btn_BoTriXe.Enabled = MonthId != 0;
        }

        private async void btn_Refresh_Click(object sender, EventArgs e)
        {
            btn_Refresh.Enabled = false;
            try
            {
                // Khóa đang xem được chọn lại sau khi nạp cây và tải lại danh sách (KhoaHocId = 0 để không bị bỏ qua)
                ClearDanhSach();
                await LoadCategoryAsync(selectLatest: false);
                await LoadLookupsAsync();
            }
            finally
            {
                if (IsAlive) btn_Refresh.Enabled = true;
            }
        }

        private void MenuItem_BoTriXeSTD_Click(object sender, EventArgs e)
        {
            if (MonthId == 0)
            {
                XtraMessageBox.Show("Hãy chọn một tháng hoặc một khóa học trước.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var f = new frmPhanCongDaoTao(MonthId);
            // Phân công xong thì danh sách đang xem cập nhật giáo viên / xe mới
            f.FormClosed += (s, args) =>
            {
                if (IsAlive && KhoaHocId != 0) LoadDanhSachHocVien(KhoaHocId);
            };
            f.Show(FindForm());
        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            cms_BaoCao.Show(btn_BaoCao, new Point(0, btn_BaoCao.Height));
        }

        private async void mn_BaoCao_Click(object sender, EventArgs e)
        {
            if (KhoaHocId == 0) return;
            int kieuKetQua = int.Parse((string)((ToolStripItem)sender).Tag);   // 0: tất cả, 1: đạt, 2: không đạt
            int khoaHocId = KhoaHocId;

            Cursor = Cursors.WaitCursor;
            btn_BaoCao.Enabled = false;
            try
            {
                DataTable resultBC = await Task.Run(() => _baoCaoDao.BaoCaoKetQuaHocVien(khoaHocId, kieuKetQua));
                if (!IsAlive) return;

                if (resultBC == null || resultBC.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu cho báo cáo này.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var rpt = new rptBaoCaoKetQuaHocDAT { DataSource = resultBC };
                new ReportPrintTool(rpt).ShowPreview();
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không lập được báo cáo", ex);
            }
            finally
            {
                if (IsAlive)
                {
                    Cursor = Cursors.Default;
                    UpdateCommandState();
                }
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"DanhSachHocVien_{SafeFileName(TenKhoaHoc)}_{DateTime.Now:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    gridView_DanhSachHocVien.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx
                    {
                        ExportType = ExportType.WYSIWYG,
                        SheetName = SafeFileName(TenKhoaHoc)
                    });
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Không xuất được file", ex);
                }
            }
        }

        private static string SafeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "KhoaHoc";
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                name = name.Replace(c, '-');
            return name.Trim();
        }

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show(this, $"{title}.\n\nChi tiết: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
