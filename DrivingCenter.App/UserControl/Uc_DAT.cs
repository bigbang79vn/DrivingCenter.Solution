using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_DAT : XtraUserControl
    {
        private readonly KhoaHocDatDAO _khoaHocDao = new KhoaHocDatDAO();
        private readonly PhienHocDAO _phienHocDao = new PhienHocDAO();
        private readonly BaoCaoDatDAO _baoCaoDao = new BaoCaoDatDAO();
        public Uc_DAT()
        {
            InitializeComponent();

            // refreshTimer được tạo tay (không thuộc components) nên phải tự dừng khi màn hình bị giải phóng,
            // nếu không nó vẫn truy vấn DB mỗi 5 phút sau khi người dùng đã chuyển trang
            Disposed += (s, e) =>
            {
                refreshTimer?.Stop();
                refreshTimer?.Dispose();
                countdownTimer?.Stop();
                _ketQuaDebounce?.Stop();
                _ketQuaDebounce?.Dispose();
                _boldFont?.Dispose();
            };

            ApplyTheme();
        }

        const int minuteInterval = 5; // Khoảng thời gian giữa các lần làm mới (phút)
        private TimeSpan countdownTime;
        private Font _boldFont;
        private Timer _ketQuaDebounce;

        #region Giao diện

        private void ApplyTheme()
        {
            // Nút trên nền sáng (tab Kết quả): nền màu thương hiệu, chữ trắng
            foreach (var btn in new[] { btn_ExportResultToExcel, btn_LoadKhoaHoc, btn_Extend })
                StyleFilledButton(btn, UiTheme.Brand, UiTheme.BrandDark);

            var grids = new (DevExpress.XtraGrid.Views.Grid.GridView View, string Prompt)[]
            {
                (gridView_PhienHoc, "Tìm theo giáo viên, học viên, biển số, mã phiên..."),
                (gridView_TongHopHvByDay, "Tìm theo học viên, giáo viên, khóa học..."),
                (gridView_TongHopGioDay, "Tìm theo giáo viên, khóa học, biển số..."),
                (gridView_PhienHocLoi, "Tìm theo giáo viên, học viên, biển số..."),
                (gridView_Result, "Tìm theo mã đăng ký, học viên, giáo viên..."),
                (gridView_KhoaHocDDT, "Tìm khóa học..."),
                (gridView_PhienHocBo, "Tìm theo giáo viên, học viên, biển số...")
            };
            foreach (var (view, prompt) in grids)
                StyleGrid(view, prompt);

            gridView_PhienHoc.RowCellStyle += gridView_PhienHoc_RowCellStyle;
            gridView_PhienHocLoi.RowCellStyle += gridView_PhienHoc_RowCellStyle;

            ApplyTabStyle();
            BuildToolbars();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged += Default_StyleChanged;
            Disposed += (s, e) =>
            {
                DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged -= Default_StyleChanged;
                _toolTip.Dispose();
            };
        }

        private void Default_StyleChanged(object sender, EventArgs e) => ApplyToolbarColors();

        #region Tab và thanh công cụ

        // Bố cục, chữ, font, màu (skin sáng) của tab và thanh công cụ nằm trong Uc_DAT.Designer.cs.
        // Ở đây chỉ làm phần Designer không làm được: icon SVG tô màu, thẻ trạng thái bo góc,
        // tooltip và đổi màu khi chuyển skin sáng/tối.

        private void ApplyTabStyle()
        {
            var tabs = new (DevExpress.XtraTab.XtraTabPage Page, string Icon, string Tip)[]
            {
                (xtraTab_PhienHoc, "svgimages/icon%20builder/travel_car.svg", "Các phiên học DAT trong ngày"),
                (xtraTab_TongHopHV, "svgimages/outlook%20inspired/employees.svg", "Tổng thời gian, quãng đường học viên đã học trong ngày"),
                (xtraTab_TongHopGV, "svgimages/business%20objects/bo_employee.svg", "Tổng giờ dạy của giáo viên / xe trong ngày"),
                (xtraTab_PhienLoi, "svgimages/icon%20builder/security_warning.svg", "Phiên học bị lỗi dữ liệu trong ngày"),
                (xtraTab_KetQua, "svgimages/business%20objects/bo_kpi_scorecard.svg", "Kết quả đào tạo của các khóa đang học"),
                (xtraTab_PhienHocBo, "svgimages/icon%20builder/actions_deletecircled.svg", "Phiên học đã bị khóa / bỏ trong khoảng ngày"),
                (xtraTab_LichSTD, "svgimages/icon%20builder/actions_calendar.svg", "Lịch chạy xe số tự động"),
            };
            foreach (var (page, icon, tip) in tabs)
            {
                page.Tooltip = tip;
                page.ImageOptions.SvgImage = UiTheme.Svg(icon);
                page.ImageOptions.SvgImageSize = new Size(28, 28);   // icon 28px giữ dải tab cao ~40px như ban đầu
            }
        }

        private enum ButtonKind { Primary, Secondary, Danger }

        private readonly List<(Button Button, ButtonKind Kind, string Icon)> _toolbarButtons = new List<(Button, ButtonKind, string)>();
        private readonly ToolTip _toolTip = new ToolTip();
        private Panel[] _toolbarPanels;
        private Label[] _toolbarTitles;

        private void BuildToolbars()
        {
            _toolbarPanels = new[] { panelHead, panel1, panel2, panel3, panel4 };
            _toolbarTitles = new[] { label11, lb_Tonghophocvienbyday, lb_TongHopGvByDay, lbl_PhienHocLoi, label1 };
            foreach (var panel in _toolbarPanels)
                panel.Paint += UiTheme.PaintHeader;

            const string iconXem = "svgimages/icon%20builder/actions_zoom.svg";
            AddToolbarButton(btn_ViewPhByDay, iconXem, ButtonKind.Primary, "Tải lại danh sách phiên của ngày đang chọn");
            AddToolbarButton(btn_Reload, "svgimages/icon%20builder/actions_refresh.svg", ButtonKind.Secondary,
                "Đồng bộ lại dữ liệu điểm danh trên máy chủ rồi tải danh sách");
            AddToolbarButton(btn_ViewTongHopHv, iconXem, ButtonKind.Primary, null);
            AddToolbarButton(btn_TongHopGioGv, iconXem, ButtonKind.Primary, null);
            AddToolbarButton(btn_ViewPhLoi, iconXem, ButtonKind.Primary, null);
            AddToolbarButton(button1, "svgimages/icon%20builder/actions_trash.svg", ButtonKind.Danger, "Xóa phiên lỗi đang chọn trong danh sách");
            AddToolbarButton(btn_ViewPhienHocSai, iconXem, ButtonKind.Primary, null);
            AddToolbarButton(btn_ExportPhSai, "svgimages/export/exporttoxlsx.svg", ButtonKind.Secondary, null);

            OnOffReload.ImageOptions.SvgImage = UiTheme.Svg("svgimages/icon%20builder/actions_clock.svg");
            OnOffReload.ImageOptions.SvgImageSize = new Size(16, 16);
            OnOffReload.ToolTip = $"Tự động đồng bộ và tải lại mỗi {minuteInterval} phút khi đang xem tab này";

            // Thẻ trạng thái nằm ngay sau tiêu đề; tiêu đề đổi theo ngày nên vị trí thẻ tính lại khi chữ đổi
            foreach (var badge in new[] { lb_CountUnSendTC, lblCountdown })
            {
                badge.Paint += PaintBadge;
                badge.TextChanged += (s, e) => LayoutBadges();
                badge.VisibleChanged += (s, e) => LayoutBadges();
            }
            label11.TextChanged += (s, e) => LayoutBadges();
            LayoutBadges();

            ApplyToolbarColors();
        }

        private void AddToolbarButton(Button btn, string icon, ButtonKind kind, string tooltip)
        {
            if (!string.IsNullOrEmpty(tooltip)) _toolTip.SetToolTip(btn, tooltip);
            _toolbarButtons.Add((btn, kind, icon));
        }

        private void LayoutBadges()
        {
            int x = label11.Left + label11.PreferredWidth + 12;
            foreach (var badge in new[] { lb_CountUnSendTC, lblCountdown })
            {
                if (!badge.Visible) continue;
                int width = TextRenderer.MeasureText(badge.Text, badge.Font).Width + 20;
                badge.SetBounds(x, badge.Top, width, badge.Height);
                x += width + 8;
            }
        }

        /// <summary>
        /// Thẻ trạng thái bo góc (chữ của Label để trong suốt, vẽ lại ở đây).
        /// Màu: "chưa gửi TC" > 0 → cam; đã gửi đủ → xanh lá; còn lại → xám.
        /// </summary>
        private static void PaintBadge(object sender, PaintEventArgs e)
        {
            var label = (Label)sender;
            Color back, fore;
            if (label.Tag as string == "warn") { back = Color.FromArgb(255, 237, 213); fore = Color.FromArgb(194, 65, 12); }
            else if (label.Tag as string == "ok") { back = Color.FromArgb(220, 252, 231); fore = Color.FromArgb(21, 128, 61); }
            else { back = Color.FromArgb(226, 232, 240); fore = Color.FromArgb(51, 65, 85); }

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, label.Width - 1, label.Height - 1);
            int d = rect.Height;
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            using (var brush = new SolidBrush(back))
            {
                path.AddArc(rect.X, rect.Y, d, d, 90, 180);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 180);
                path.CloseFigure();
                g.FillPath(brush, path);
            }
            TextRenderer.DrawText(g, label.Text, label.Font, rect, fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        private void SetChuaGuiTC(int count)
        {
            lb_CountUnSendTC.Tag = count > 0 ? "warn" : "ok";
            lb_CountUnSendTC.Text = count > 0 ? $"{count} phiên chưa gửi TC" : "Đã gửi đủ TC";
            lb_CountUnSendTC.Invalidate();
        }

        /// <summary>
        /// Icon nút + màu theo skin. Màu skin sáng đã có trong Designer; skin tối đổi tiêu đề sáng hơn
        /// và nút phụ nền xám đậm để không bị chói.
        /// </summary>
        private void ApplyToolbarColors()
        {
            bool dark = UiTheme.IsDarkSkin(LookAndFeel);
            Color titleColor = dark ? Color.FromArgb(147, 176, 222) : UiTheme.Brand;
            foreach (var title in _toolbarTitles) title.ForeColor = titleColor;
            lb_KetquaKh.ForeColor = titleColor;
            xtraTabControl.AppearancePage.HeaderActive.ForeColor = titleColor;

            foreach (var (btn, kind, icon) in _toolbarButtons)
            {
                if (kind == ButtonKind.Secondary)
                {
                    btn.BackColor = dark ? Color.FromArgb(58, 58, 58) : Color.White;
                    btn.ForeColor = dark ? Color.White : UiTheme.Brand;
                    btn.FlatAppearance.BorderColor = dark ? Color.FromArgb(90, 90, 90) : UiTheme.Muted;
                    btn.FlatAppearance.MouseOverBackColor = dark ? Color.FromArgb(72, 72, 72) : Color.FromArgb(241, 245, 249);
                    btn.FlatAppearance.MouseDownBackColor = btn.FlatAppearance.MouseOverBackColor;
                }

                var old = btn.Image;
                btn.Image = UiTheme.RenderSvg(UiTheme.Svg(icon), 16, btn.ForeColor);
                old?.Dispose();
            }

            var pressed = OnOffReload.AppearancePressed;
            pressed.BackColor = dark ? UiTheme.BrandDark : Color.FromArgb(225, 232, 244);
            pressed.ForeColor = dark ? Color.White : UiTheme.Brand;

            foreach (var panel in _toolbarPanels) panel.Invalidate();
        }

        #endregion

        private static void StyleFilledButton(Button btn, Color normal, Color hover)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.BackColor = normal;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Roboto", 9.75F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Không dùng UiTheme.StyleGrid vì các cột ở đây có header nền xanh/chữ trắng riêng.
        /// Lưới có nhóm (GroupCount > 0) vẫn giữ thanh nhóm để người dùng đổi cách nhóm.
        /// </summary>
        private static void StyleGrid(DevExpress.XtraGrid.Views.Grid.GridView view, string findPrompt)
        {
            view.RowHeight = Math.Max(view.RowHeight, 30);
            view.OptionsView.EnableAppearanceEvenRow = false;   // nền trắng đồng nhất, không xen màu
            view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            view.OptionsView.ShowGroupPanel = view.GroupCount > 0;
            view.GroupPanelText = "Kéo tiêu đề cột vào đây để nhóm dữ liệu";
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            view.OptionsFind.AlwaysVisible = true;
            view.OptionsFind.FindNullPrompt = findPrompt;
            view.OptionsFind.ShowFindButton = false;   // lọc ngay khi gõ
            view.CustomDrawEmptyForeground += (s, e) =>
            {
                if (view.RowCount > 0) return;
                UiTheme.DrawEmptyState(e, "Không có dữ liệu", "Chọn ngày / khóa học rồi bấm \"Xem dữ liệu\" để tải.");
            };
        }

        /// <summary>
        /// Cột "Cảnh báo": tô đỏ đậm khi phiên có lỗi (bỏ qua ký hiệu "..." = chưa gửi Tổng cục).
        /// </summary>
        private void gridView_PhienHoc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "Error") return;
            string error = (e.CellValue as string)?.Replace("...", "").Trim();
            if (string.IsNullOrEmpty(error)) return;

            if (_boldFont == null)
                _boldFont = new Font("Roboto", 9.75F, FontStyle.Bold);
            e.Appearance.ForeColor = UiTheme.DangerDark;
            e.Appearance.Font = _boldFont;
        }

        /// <summary>
        /// Hỏi nơi lưu rồi xuất lưới ra Excel (thay cho việc ghi đè một file cố định trên Desktop).
        /// </summary>
        private void ExportGrid(DevExpress.XtraGrid.GridControl grid, string defaultName, bool keepFormat = true)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = string.Join("_", defaultName.Split(Path.GetInvalidFileNameChars())) + ".xlsx"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (keepFormat)
                        grid.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    else
                        grid.ExportToXlsx(dialog.FileName);

                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(dialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Không xuất được file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Lấy dòng đang chọn của lưới; báo cho người dùng nếu chưa chọn dòng nào.
        /// </summary>
        private static T FocusedRow<T>(DevExpress.XtraGrid.Views.Grid.GridView view) where T : class
        {
            var row = view.GetFocusedRow() as T;
            if (row == null)
                XtraMessageBox.Show("Vui lòng chọn một dòng trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return row;
        }

        /// <summary>
        /// Đổi chuỗi "HH:mm:ss" (giờ có thể > 99) sang TimeSpan; chuỗi sai định dạng trả về TimeSpan.Zero.
        /// </summary>
        private static TimeSpan ParseDuration(string value)
        {
            var parts = (value ?? "").Trim().Split(':');
            if (parts.Length == 3 && int.TryParse(parts[0], out int h) && int.TryParse(parts[1], out int m) && int.TryParse(parts[2], out int s))
                return new TimeSpan(h, m, s);
            return TimeSpan.Zero;
        }

        #endregion

        private void Uc_DAT_Load(object sender, EventArgs e)
        {
            cbb_TongHopGv.Properties.Items.AddRange(new string[] { "Theo giáo viên", "Theo xe" });
            // Gán giá trị ban đầu mà không kích hoạt các handler tải dữ liệu
            _suppressLoad = true;
            cbb_TongHopGv.EditValue = "Theo giáo viên";
            dtp_PhByDay.DateTime = DateTime.Now;
            dtp_TongHopHvByDay.DateTime = DateTime.Now;
            dtp_TongHopGioDay.DateTime = DateTime.Now;
            dtp_PhienHocLoiByDay.DateTime = DateTime.Now;
            _suppressLoad = false;

            // Trước đây Load tải đồng bộ dữ liệu của MỌI tab (phiên học, tổng hợp HV/GV, khóa học + kết quả khóa đầu,
            // ngày min/max) làm giao diện đơ ~4,5 giây. Giờ chỉ tải tab đang mở, các tab khác tải khi được chọn lần đầu.
            _staleTabs.UnionWith(new[] { xtraTab_TongHopHV, xtraTab_TongHopGV, xtraTab_KetQua, xtraTab_PhienHocBo });
            xtraTabControl.SelectedPageChanged += xtraTabControl_SelectedPageChanged;

            // Cửa sổ được khôi phục từ thu nhỏ: chạy lần tự làm mới đã bị hoãn
            var hostForm = FindForm();
            if (hostForm != null)
            {
                EventHandler onResize = (s, ev) => { if (hostForm.WindowState != FormWindowState.Minimized && IsTabVisible(xtraTab_PhienHoc)) RunPendingReload(); };
                hostForm.Resize += onResize;
                Disposed += (s, ev) => hostForm.Resize -= onResize;
            }

            _ketQuaDebounce = new Timer { Interval = 250 };
            _ketQuaDebounce.Tick += KetQuaDebounce_Tick;

            refreshTimer = new Timer();
            refreshTimer.Interval = minuteInterval * 60 * 1000;
            refreshTimer.Tick += refreshTimer_Tick;

            // Bật tự làm mới mặc định: handler CheckedChanged sẽ khởi động cả 2 timer và hiện đếm ngược
            countdownTime = TimeSpan.FromMinutes(minuteInterval);
            OnOffReload.Checked = true;
            UpdateCountdownLabel();

            LoadPhienHoc();
        }

        #region Tải dữ liệu theo tab

        private bool _suppressLoad;
        private readonly HashSet<DevExpress.XtraTab.XtraTabPage> _staleTabs = new HashSet<DevExpress.XtraTab.XtraTabPage>();
        private bool _phienBoDatesLoaded;

        // Mỗi lần tải tăng version; kết quả trả về muộn của lần tải cũ sẽ bị bỏ qua
        private int _phienHocVersion, _hvVersion, _gvVersion, _ketQuaVersion;

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            EnsureTabLoaded(e.Page);
            if (e.Page == xtraTab_PhienHoc) RunPendingReload();
        }

        private bool IsTabVisible(DevExpress.XtraTab.XtraTabPage page) => xtraTabControl.SelectedTabPage == page;

        /// <summary>
        /// Tải dữ liệu cho tab nếu tab chưa từng tải hoặc điều kiện lọc (ngày...) đã đổi khi tab đang ẩn.
        /// </summary>
        private void EnsureTabLoaded(DevExpress.XtraTab.XtraTabPage page)
        {
            if (!_staleTabs.Remove(page)) return;

            if (page == xtraTab_TongHopHV) LoadTongHopHvForSelectedDay();
            else if (page == xtraTab_TongHopGV) TongHopGvTrongNgay();
            else if (page == xtraTab_KetQua) LoadKhDangDaoTao();
            else if (page == xtraTab_PhienHocBo) EnsurePhienBoDates();
        }

        /// <summary>
        /// Khoảng ngày mặc định của tab Phiên học bỏ (ngày khai giảng sớm nhất → bế giảng muộn nhất).
        /// Cũng cần cho thao tác mở khóa phiên ở tab Phiên học, nên tải khi cần lần đầu.
        /// </summary>
        private void EnsurePhienBoDates()
        {
            if (_phienBoDatesLoaded) return;
            _phienBoDatesLoaded = true;
            dtp_From.DateTime = _khoaHocDao.GetMinNgayKG();
            dtp_To.DateTime = _khoaHocDao.GetMaxNgayBG();
        }

        /// <summary>
        /// Màn hình còn dùng được để cập nhật giao diện sau một lần await.
        /// Lúc đang Dispose thì IsDisposed vẫn false, nên phải kiểm tra cả Disposing/IsHandleCreated;
        /// thiếu điều kiện này dễ gặp lỗi "Error creating window handle" khi người dùng chuyển trang lúc đang tải.
        /// </summary>
        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        private void ShowLoadError(string what, Exception ex)
        {
            if (!IsAlive) return;
            XtraMessageBox.Show($"Không tải được {what}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        //-----------Panel Left------------
        private bool isCollapsed;

        //---------------------------------
        private DataTable dt;

        private List<TongHopHVByDay> lstSum;
        private List<TongHopGV> lstSumGV;
        private List<KhoaHocDangDT> lstKhoaHoc;
        private List<PhienHoc> lstPH;
        private string HangDaoTao;
        private int KhoaHocID;
        private string MaKH;
        private string TenKhoaHoc;
        private int flag;

        #region Phiên Học

        private void LoadPhienHoc()
        {
            _ = LoadPhienHocAsync();
        }

        /// <summary>
        /// Truy vấn + tính cảnh báo chạy nền; chỉ phần gán lưới chạy trên luồng giao diện.
        /// </summary>
        private async Task LoadPhienHocAsync()
        {
            int version = ++_phienHocVersion;
            string day = dtp_PhByDay.DateTime.ToString("dd/MM/yyyy");
            label11.Text = "Phiên học ngày " + day;

            try
            {
                var result = await Task.Run(() =>
                {
                    var list = _phienHocDao.GetLstPhienByDateTrue(day);
                    int chuaGuiTC = ProcessPhienHoc(list);
                    return (list, chuaGuiTC);
                });
                if (!IsAlive || version != _phienHocVersion) return;

                lstPH = result.list;
                SetChuaGuiTC(result.chuaGuiTC);
                gridControl_PhienHoc.DataSource = lstPH;
                GridHelper.EnableRowNumbering(gridView_PhienHoc);
            }
            catch (Exception ex)
            {
                if (version == _phienHocVersion) ShowLoadError("danh sách phiên học", ex);
            }
        }

        private void btn_ViewPhByDay_Click(object sender, EventArgs e)
        {
            LoadPhienHoc();
        }

        private bool _errorShown = false;
        private async Task ReloadClick()
        {
            string datetime = dtp_PhByDay.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
            btn_Reload.Enabled = false;

            try
            {
                await _phienHocDao.ReLoad(datetime);
                if (!IsAlive) return;
                _errorShown = false; // tải lại thành công: lần lỗi sau vẫn được báo

                // await giữ ngữ cảnh luồng giao diện nên không cần Invoke
                await LoadPhienHocAsync();
                if (!IsAlive) return;

                // Khởi tạo lại bộ đếm ngược (chỉ khi đang bật tự làm mới)
                countdownTime = TimeSpan.FromMinutes(minuteInterval);
                if (OnOffReload.Checked) countdownTimer.Start();
                UpdateCountdownLabel();
            }
            catch (Exception ex)
            {
                if (IsAlive && !_errorShown)
                {
                    _errorShown = true;
                    XtraMessageBox.Show("Lỗi khi reload: " + ex.Message);
                }
            }
            finally
            {
                if (IsAlive)
                    btn_Reload.Enabled = true;
            }
        }

        private bool _reloadPending;

        private async void refreshTimer_Tick(object sender, EventArgs e)
        {
            // Mỗi lần tự làm mới gọi 2 lần spGetDiemDanhNguoiLxByDay trên server. Không ai xem tab Phiên học
            // (đang ở tab khác hoặc cửa sổ thu nhỏ) thì hoãn lại, tránh mọi máy cùng đẩy tải lên DB vô ích.
            var form = FindForm();
            bool dangXem = Visible && IsTabVisible(xtraTab_PhienHoc) && form != null && form.WindowState != FormWindowState.Minimized;
            if (!dangXem)
            {
                _reloadPending = true;
                return;
            }
            await ReloadClick();
        }

        private void RunPendingReload()
        {
            if (!_reloadPending || !OnOffReload.Checked) return;
            _reloadPending = false;
            _ = ReloadClick();
        }

        private void countdownTimer_Tick(object sender, EventArgs e)
        {
            if (countdownTime.TotalSeconds > 0)
            {
                countdownTime = countdownTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateCountdownLabel();
            }
        }

        private void UpdateCountdownLabel()
        {
            lblCountdown.Text = "Tự làm mới sau " + countdownTime.ToString(@"mm\:ss");
        }

        private void dtp_PhByDay_EditValueChanged(object sender, EventArgs e)
        {
            if (_suppressLoad) return;
            LoadPhienHoc();
        }

        private async void btn_Reload_Click(object sender, EventArgs e)
        {
            await ReloadClick();
        }

        /// <summary>
        /// Checked = đang bật tự làm mới (trước đây logic ngược: nút hiện "On" nhưng lại dừng timer).
        /// </summary>
        private void OnOffReload_CheckedChanged(object sender, EventArgs e)
        {
            if (refreshTimer == null) return;

            if (OnOffReload.Checked)
            {
                countdownTime = TimeSpan.FromMinutes(minuteInterval);
                UpdateCountdownLabel();
                refreshTimer.Stop();
                refreshTimer.Start(); // đếm lại từ đầu để khớp với đồng hồ đếm ngược
                countdownTimer.Start();
                lblCountdown.Visible = true;
            }
            else
            {
                countdownTimer.Stop();
                refreshTimer.Stop();
                lblCountdown.Visible = false;
            }
        }

        private static void CheckOverlap<TKey>(IEnumerable<PhienHoc> data, Func<PhienHoc, TKey> keySelector, Dictionary<PhienHoc, HashSet<string>> errorMap, string message)
        {
            foreach (var grp in data.GroupBy(keySelector))
            {
                var sessions = grp.OrderBy(x => x.Time_LogIn).ToList();

                for (int i = 0; i < sessions.Count - 1; i++)
                {
                    for (int j = i + 1; j < sessions.Count; j++)
                    {
                        if (sessions[j].Time_LogIn >= sessions[i].Time_LogOut)
                            break;

                        if (sessions[i].Time_LogIn < sessions[j].Time_LogOut &&
                            sessions[j].Time_LogIn < sessions[i].Time_LogOut)
                        {
                            errorMap[sessions[i]].Add(message);
                            errorMap[sessions[j]].Add(message);
                        }
                    }
                }
            }
        }

        public int ProcessPhienHoc(List<PhienHoc> lstPH)
        {
            int SendTC = 0;
            var sortedList = lstPH.OrderBy(x => x.Time_LogOut).ToList();

            var errorMap = new Dictionary<PhienHoc, HashSet<string>>();

            // Gắn sẵn ErrorList để tránh trùng lặp
            foreach (var item in sortedList)
            {
                errorMap[item] = new HashSet<string>();
            }

            // PASS 1: Nghỉ thiếu & Trùng phiên
            foreach (var grp in sortedList.GroupBy(x => new { x.NguoiLxID, x.TenKH }))
            {
                var sessions = grp.OrderBy(x => x.Time_LogIn).ToList();

                for (int k = 1; k < sessions.Count; k++)
                {
                    var prev = sessions[k - 1];
                    var curr = sessions[k];

                    if (prev.Time_LogOut > curr.Time_LogIn)
                    {
                        errorMap[curr].Add("Trùng phiên!");
                        continue;
                    }

                    var gap = curr.Time_LogIn - prev.Time_LogOut;
                    if (gap.TotalSeconds > 0 && gap.TotalSeconds < 900)
                    {
                        errorMap[curr].Add("Nghỉ Thiếu!");
                    }
                }
            }

            CheckOverlap(sortedList, x => x.NguoiLxID, errorMap, "Trùng học viên!");

            CheckOverlap(sortedList, x => x.BienSoXe, errorMap, "Trùng xe!");


            // PASS 2: Các cảnh báo khác
            foreach (var item in sortedList)
            {
                // Tỷ lệ nhận diện
                item.TyLe = item.SoLanNhanDien > 0
                    ? Math.Round((double)item.SoLanNhanDienThanhCong / item.SoLanNhanDien * 100, 1)
                    : 0;
                if (item.TyLe < 75) errorMap[item].Add("Nhận Diện!");

                // Quãng đường
                if (item.QuangDuongPhien < 5 && item.LogOut) errorMap[item].Add("Quãng đường <5!");

                // Vận tốc: phiên đang chạy mới vài phút thì vận tốc trung bình luôn thấp,
                // nên chỉ xét khi đã đăng xuất hoặc đã học từ 15 phút trở lên
                double vantoc = item.dbTime > 0 ? item.QuangDuongPhien / (item.dbTime / 3600) : 0;
                if (vantoc < 20 && (item.LogOut || item.dbTime >= 900)) errorMap[item].Add("Vận Tốc!");

                // Đăng nhập sớm
                if (item.Time_LogIn.Hour < 7) errorMap[item].Add("ĐN Sớm!");

                // Quá giờ
                var logout = item.Time_LogOut;
                if (logout.Hour > 22 || (logout.Hour == 22 && logout.Minute > 30)) errorMap[item].Add("Quá Giờ!");

                // Sai ngày giờ theo khóa học
                // Tên khóa dạng "B01-K131": lấy số sau chữ K. Tên không đúng dạng thì bỏ qua kiểm tra này
                // (trước đây int.Parse văng lỗi làm hỏng cả danh sách phiên trong ngày)
                if (TryGetSoKhoa(item.TenKH, out int khoahoc) && IsInvalidCourseTime(item, khoahoc))
                    errorMap[item].Add("Sai ngày giờ!");

                // Kiểm tra số lần chụp
                //int lanchup = Math.Max(1, (int)Math.Ceiling(item.dbTime / 300)) + 1;
                //if (lanchup < item.SoLanNhanDien && item.LogOut == true) errorMap[item].Add("Số lần chụp!");

                // Gửi Tổng cục
                if (item.LogOut && !item.SendTongCuc)
                {
                    errorMap[item].Add("...");
                    SendTC++;
                }

                // Join tất cả lỗi duy nhất thành chuỗi
                item.Error = string.Join(" ", errorMap[item]);
            }

            return SendTC;
        }

        private static bool TryGetSoKhoa(string tenKH, out int soKhoa)
        {
            soKhoa = 0;
            if (string.IsNullOrEmpty(tenKH)) return false;

            int index = tenKH.IndexOf('K'); // giữ đúng cách lấy số khóa như trước
            if (index < 0) return false;

            string digits = new string(tenKH.Substring(index + 1).TakeWhile(char.IsDigit).ToArray());
            return int.TryParse(digits, out soKhoa);
        }

        private bool IsInvalidCourseTime(PhienHoc item, int khoahoc)
        {
            DayOfWeek wk = item.Time_LogIn.DayOfWeek;

            TimeSpan timeStart = item.Time_LogIn.TimeOfDay;
            TimeSpan timeEnd = item.Time_LogOut.TimeOfDay;

            TimeSpan minTime = new TimeSpan(7, 0, 0);
            TimeSpan maxTime = new TimeSpan(22, 30, 0);

            // Tất cả khóa học chỉ được học từ 07:00 đến 22:30
            //if (timeStart < minTime || timeEnd > maxTime)
            //    return true;

            // Khóa có ký tự C:
            // Không học thứ 2, các ngày còn lại được học
            if (!string.IsNullOrEmpty(item.TenKH) &&
                item.TenKH.IndexOf("C", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return wk == DayOfWeek.Monday;
            }

            // Khóa không có C:
            // Lấy 2 số cuối của khóa học
            int soCuoi = khoahoc % 100;

            // Lớp lẻ: chỉ học thứ 3, 4, 5
            if (soCuoi % 2 != 0)
            {
                return wk != DayOfWeek.Tuesday &&
                       wk != DayOfWeek.Wednesday &&
                       wk != DayOfWeek.Thursday;
            }

            // Lớp chẵn: chỉ học thứ 6, 7, Chủ nhật
            return wk != DayOfWeek.Friday &&
                   wk != DayOfWeek.Saturday &&
                   wk != DayOfWeek.Sunday;
        }

        #region Context Menustrip Phiên học

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPhienHoc();
        }

        private void DetailSession_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHoc);
            if (row == null) return;

            using (frmDetailSession f = new frmDetailSession(row.MaPhien.ToString()))
                f.ShowDialog(FindForm());
        }

        private void mn_CheckMap_Click(object sender, EventArgs e)
        {
            if (lstPH == null) return;

            DateTime selected = dtp_PhByDay.DateTime;
            DateTime fromDay = selected.Date;
            DateTime toDay = fromDay.AddDays(1);

            List<Guid> lstGuid = _phienHocDao.CheckMapOut(fromDay, toDay);

            var result = lstPH.Where(x => lstGuid.Contains(x.MaPhien)).ToList();
            gridControl_PhienHoc.DataSource = result;
            label11.Text = $"Phiên ra ngoài bản đồ ngày {selected:dd/MM/yyyy} ({result.Count})";
        }

        private async void CheckImgNull_Click(object sender, EventArgs e)
        {
            if (lstPH == null) return;

            // Lọc trên bản sao: trước đây xóa thẳng khỏi lstPH nên các bộ lọc sau chạy trên danh sách đã bị cắt.
            // Trước đây mỗi phiên một truy vấn (N phiên = N lượt gọi DB trên luồng giao diện); giờ gộp 1 truy vấn chạy nền.
            var source = lstPH.ToList();
            label11.Text = "Đang kiểm tra ảnh đăng xuất...";
            try
            {
                var coAnh = await Task.Run(() => _phienHocDao.GetPhienCoAnhLogout(source.Select(x => x.MaPhien)));
                if (!IsAlive) return;

                var result = source.Where(item => !coAnh.Contains(item.MaPhien)).ToList();
                gridControl_PhienHoc.DataSource = result;
                label11.Text = $"Phiên thiếu ảnh đăng xuất ngày {dtp_PhByDay.DateTime:dd/MM/yyyy} ({result.Count})";
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                label11.Text = "Phiên học ngày " + dtp_PhByDay.DateTime.ToString("dd/MM/yyyy");
                ShowLoadError("thông tin ảnh đăng xuất", ex);
            }
        }
        private string GetMaDKByNguoiLxId(Int64 nguoiLxId)
        {
            var maDK = _baoCaoDao.GetMaDKByNguoiLxId(nguoiLxId);
            return maDK;
        }
        private void mn_trainingProcess_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHoc);
            if (row == null) return;

            var maDK = GetMaDKByNguoiLxId(row.NguoiLxID);
            if (string.IsNullOrEmpty(maDK)) return;

            using (frmQuaTrinhDaoTao f = new frmQuaTrinhDaoTao(maDK))
                f.ShowDialog(FindForm());
        }
        private void mn_SessionLock_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHoc);
            if (row == null) return;

            using (frmGhiChu f = new frmGhiChu(row.MaPhien.ToString()))
            {
                // Modal: chỉ tải lại danh sách sau khi khóa phiên thành công
                if (f.ShowDialog(FindForm()) == DialogResult.OK)
                    LoadPhienHoc();
            }
        }
        private void mb_SessionUnlock_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHoc);
            if (row == null) return;

            // Trước đây "Thông báo" và MessageBoxButtons bị truyền nhầm vào string.Format,
            // hộp thoại chỉ có nút OK nên luôn mở khóa mà không thật sự hỏi xác nhận
            if (XtraMessageBox.Show($"Sử dụng lại phiên học {row.MaPhien}?", "Xác nhận",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            _phienHocDao.UnlockSession(row.MaPhien.ToString());

            EnsurePhienBoDates(); // khoảng ngày của tab Phiên học bỏ giờ chỉ tải khi cần
            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
            LoadPhienHoc();
        }

        private void deleteSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHoc);
            if (row == null) return;

            if (XtraMessageBox.Show($"Bạn có chắc chắn xóa phiên học\n{row.MaPhien}?\n\nThao tác này không thể hoàn tác.", "Xác nhận xóa",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                _phienHocDao.DeletePhByID(row.MaPhien.ToString());
                // Tải lại qua LoadPhienHoc để cập nhật cả lstPH (trước đây gán vào biến cục bộ cùng tên)
                LoadPhienHoc();
            }
        }

        private void EndSession_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHoc);
            if (row == null) return;

            if (row.LogOut)
            {
                XtraMessageBox.Show("Phiên học này đã kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Phiên học chưa kết thúc. Bạn muốn kết thúc phiên học?", "Xác nhận",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                _phienHocDao.KetThucPhien(row.NguoiLxID, row.MaPhien.ToString());
                LoadPhienHoc();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExportGrid(gridControl_PhienHoc, $"Phien DAT - {dtp_PhByDay.DateTime:dd-MM-yyyy}");
        }

        #endregion Context Menustrip Phiên học

        #endregion Phiên Học

        #region Kết Quả

        private void btn_Extend_Click(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                PanelLeft.Width = PanelLeft.Width + 220;
                isCollapsed = false;
                btn_Extend.Text = "◀  Thu gọn";
            }
            else
            {
                PanelLeft.Width = PanelLeft.Width - 220;
                isCollapsed = true;
                btn_Extend.Text = "▶  Mở rộng";
            }
        }

        private void LoadKhDangDaoTao()
        {
            lstKhoaHoc = _khoaHocDao.GetAllKhoaHocDDT();
            gridControl_KhoaHoc.DataSource = lstKhoaHoc;
        }

        private void btn_LoadKhoaHoc_Click(object sender, EventArgs e)
        {
            LoadKhDangDaoTao();
        }

        /// <summary>
        /// Gộp 3 nguồn số liệu theo MaDK. Nhận hạng đào tạo qua tham số (không đọc field) để chạy nền an toàn.
        /// Dùng Dictionary thay cho 2 vòng lặp lồng nhau O(n×m); trùng MaDK thì bản ghi sau cùng thắng như trước.
        /// </summary>
        public List<TongHopHVByKhoa> TongHopKQByKhoa(int khoaHocID, string hangDaoTao)
        {
            List<TongHopHVByKhoa> lst = _baoCaoDao.TongHopHvByKhoa(khoaHocID);
            List<TongHopHVByKhoa> lstB1 = _baoCaoDao.TongHopPhB11(khoaHocID);
            List<TongHopHVByKhoa> lstBD = _baoCaoDao.TongHopPhBD(khoaHocID, hangDaoTao);

            var b1ByMaDK = new Dictionary<string, TongHopHVByKhoa>();
            foreach (var itemB1 in lstB1) if (itemB1.MaDK != null) b1ByMaDK[itemB1.MaDK] = itemB1;

            var bdByMaDK = new Dictionary<string, TongHopHVByKhoa>();
            foreach (var itemBD in lstBD) if (itemBD.MaDK != null) bdByMaDK[itemBD.MaDK] = itemBD;

            bool coSaHinh = hangDaoTao == "B2" || hangDaoTao == "B" || hangDaoTao == "C1";
            foreach (TongHopHVByKhoa item in lst)
            {
                if (item.MaDK == null) continue;

                if (coSaHinh && b1ByMaDK.TryGetValue(item.MaDK, out var b1))
                {
                    item.TongQuangDuong_B1 = b1.TongQuangDuong_B1;
                    item.TongThoiGian_B1 = b1.TongThoiGian_B1;
                }
                if (bdByMaDK.TryGetValue(item.MaDK, out var bd))
                {
                    item.TongQuangDuong_PhienDem = bd.TongQuangDuong_PhienDem;
                    item.TongThoiGian_PhienDem = bd.TongThoiGian_PhienDem;
                }
            }
            return lst;
        }

        public int HvDuDK { get; private set; } = 0;

        /// <summary>
        /// Không còn tự tăng HvDuDK bên trong (để chạy nền an toàn); đếm từ kết quả trả về.
        /// </summary>
        public List<ViewTongHopHvByKhoa> ProcessTongHop(List<TongHopHVByKhoa> lstKQ, string HangDaoTao)
        {
            return lstKQ.Select((item, index) =>
            {
                var view = new ViewTongHopHvByKhoa
                {
                    STT = index + 1,
                    NguoiLxID = item.NguoiLxID,
                    View_MaDK = item.MaDK,
                    View_HoTenGV = item.HoTenGV,
                    View_HoVaTen = item.HoVaTenHV,
                    View_HangDaoTao = item.HangDaoTao,
                    View_TongQuangDuong = item.TongQuangDuongTichLuy,
                    TongThoiGian = Math.Round(item.TongThoiGianTichLuy / 3600, 4),
                    View_TongThoiGian = FormatTimeSpan(TimeSpan.FromSeconds(item.TongThoiGianTichLuy)),
                    View_QuangDuongPhienGuiTC = item.QuangDuongPhienGuiTC,
                    View_ThoiGianPhienGuiTC = FormatTimeSpan(TimeSpan.FromSeconds(item.ThoiGianPhienGuiTC)),
                    View_TongQuangDuong_B11 = item.TongQuangDuong_B1,
                    View_TongThoiGian_B11 = FormatTimeSpan(TimeSpan.FromSeconds(item.TongThoiGian_B1)),
                    TongThoiGianSTD = Math.Round(item.TongThoiGian_B1 / 3600, 4),
                    View_TongQuangDuong_PhienDem = item.TongQuangDuong_PhienDem,
                    View_TongThoiGian_PhienDem = FormatTimeSpan(TimeSpan.FromSeconds(item.TongThoiGian_PhienDem)),
                    TongThoiGianBD = Math.Round(item.TongThoiGian_PhienDem / 3600, 4),
                    View_SoNgayHoc = item.SoNgayHocKQ
                };

                // Check điều kiện DAT New
                view.View_Status = CheckDatNewCondition(item, HangDaoTao) ? "Đủ điều kiện!" : "";

                return view;
            }).ToList();
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            int hours = ts.Days * 24 + ts.Hours;
            return $"{hours:D2} giờ {ts.Minutes:D2} phút";
        }

        private bool CheckDatNewCondition(TongHopHVByKhoa item, string hangDaoTao)
        {
            bool baseCondition = item.TongQuangDuongTichLuy > 712 && item.TongThoiGianTichLuy > 43200;
            switch (hangDaoTao)
            {
                case "B11":
                    return baseCondition && item.TongThoiGian_PhienDem > 11100 && item.SoNgayHocKQ >= 6;
                case "B":
                    return item.TongQuangDuongTichLuy > 812 && item.TongThoiGianTichLuy > 72480 &&
                           item.TongThoiGian_PhienDem > 7500 && item.TongThoiGian_B1 > 7500;
                case "B.01":
                    return baseCondition && item.TongThoiGian_PhienDem > 7500;
                case "C1":
                    return item.TongQuangDuongTichLuy > 827 && item.TongThoiGianTichLuy > 87000 &&
                           item.TongThoiGian_B1 > 3900 && item.TongThoiGian_PhienDem > 7500;
                default:
                    return item.TongQuangDuongTichLuy > 812 && item.TongThoiGianTichLuy > 72480 &&
                           item.TongThoiGian_B1 > 7500 && item.TongThoiGian_PhienDem > 11100 && item.SoNgayHocKQ >= 8;
            }
        }

        private void gridView_KhoaHocDDT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Lưới rỗng hoặc đang focus dòng nhóm (Hạng: B...) thì không có khóa học để xem
            if (!(gridView_KhoaHocDDT.GetFocusedRow() is KhoaHocDangDT rowData)) return;

            // Cập nhật ngay thông tin khóa đang chọn (các menu báo cáo dùng các field này)
            KhoaHocID = Convert.ToInt32(rowData.KhoaHocID);
            TenKhoaHoc = rowData.TenKH;
            MaKH = rowData.MaKH.ToString();
            HangDaoTao = rowData.HangDaoTao.ToString();
            lb_KetquaKh.Text = $"Kết quả học viên khóa {TenKhoaHoc}  (đang tải...)";

            // Debounce: lướt nhanh bằng phím mũi tên chỉ tải khóa cuối cùng thay vì 3 truy vấn + 1 lệnh ghi cho mỗi dòng
            _ketQuaDebounce.Stop();
            _ketQuaDebounce.Start();
        }

        private async void KetQuaDebounce_Tick(object sender, EventArgs e)
        {
            _ketQuaDebounce.Stop();
            await LoadKetQuaAsync(KhoaHocID, TenKhoaHoc, HangDaoTao);
        }

        /// <summary>
        /// Tải kết quả học viên của khóa: truy vấn, tính điều kiện và ghi SoHVDuDK chạy nền.
        /// </summary>
        private async Task LoadKetQuaAsync(int khoaHocID, string tenKhoaHoc, string hangDaoTao)
        {
            int version = ++_ketQuaVersion;
            try
            {
                var result = await Task.Run(() =>
                {
                    var views = ProcessTongHop(TongHopKQByKhoa(khoaHocID, hangDaoTao), hangDaoTao);
                    int duDK = views.Count(v => !string.IsNullOrEmpty(v.View_Status));
                    var table = new ConvertListToDataTable().ToDataTable(views);

                    // Giữ hành vi cũ: cập nhật số HV đủ điều kiện của khóa (Dashboard dùng số này)
                    _khoaHocDao.UpdateHvDuDK(duDK, khoaHocID);
                    return (views, duDK, table);
                });
                if (!IsAlive || version != _ketQuaVersion) return;

                HvDuDK = result.duDK;
                dt = result.table;
                grid_Result.DataSource = result.views;
                lb_KetquaKh.Text = "Kết quả học viên khóa " + tenKhoaHoc;
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _ketQuaVersion) return;
                lb_KetquaKh.Text = "Kết quả học viên khóa " + tenKhoaHoc;
                ShowLoadError($"kết quả khóa {tenKhoaHoc}", ex);
            }
        }

        private void cb_ShowSendTC_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_ShowSendTC.Checked)
            {
                View_QuangDuongPhienGuiTC.Visible = true;
                View_ThoiGianPhienGuiTC.Visible = true;
            }
            else
            {
                View_QuangDuongPhienGuiTC.Visible = false;
                View_ThoiGianPhienGuiTC.Visible = false;
            }
        }
        private void mn_ContextThongKeGiaoVien_Click(object sender, EventArgs e)
        {
            // Lấy giá trị của cột "ID" từ dòng hiện tại
            int focusedRowHandle = gridView_KhoaHocDDT.FocusedRowHandle;
            string khoahoc = gridView_KhoaHocDDT.GetRowCellValue(focusedRowHandle, "TenKH").ToString();

            lstSumGV = _baoCaoDao.TongHopGVsByKh(khoahoc);

            foreach (var item in lstSumGV)
            {
                if (DateTime.TryParseExact(item.NgayTap, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    DayOfWeek dayOfWeek = date.DayOfWeek;
                    if (item.TongThoiGian > 36000 && dayOfWeek != DayOfWeek.Monday)
                    {
                        item.SGhiChu = "Quá giờ dạy!";
                    }
                    else if (item.TongThoiGian > 18000 && dayOfWeek.Equals(DayOfWeek.Monday))
                    {
                        item.SGhiChu = "Quá giờ dạy Thứ 2!";
                    }
                }
                else
                {
                    Console.WriteLine("Ngày không hợp lệ!");
                }
            }

            ConvertListToDataTable convert = new ConvertListToDataTable();
            DataTable dt = convert.ToDataTable(lstSumGV);

            ExportExcelWithName.ExportToExcel(dt, "Giờ dạy", TenKhoaHoc);
        }

        private void mn_TongHopTienDo_Click(object sender, EventArgs e)
        {
            List<TienDo> lst = _baoCaoDao.TongHopTienDoDatByKH(KhoaHocID, MaKH);

            ConvertListToDataTable convert = new ConvertListToDataTable();
            DataTable dt = convert.ToDataTable(lst);

            ExportExcelWithName.ExportToExcel(dt, "Giờ dạy", TenKhoaHoc);
        }
        private void mn_ThongKeKetQuaHocVien_Click(object sender, EventArgs e)
        {
            DataTable resultBC = _baoCaoDao.BaoCaoKetQuaHocVien(KhoaHocID, 0);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;

            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }
        private void mn_ThongKeHocVienHoanThanh_Click(object sender, EventArgs e)
        {
            DataTable resultBC = _baoCaoDao.BaoCaoKetQuaHocVien(KhoaHocID, 1);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;

            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        private void mn_ThongKeHocVienKhongHoanThanh_Click(object sender, EventArgs e)
        {
            DataTable resultBC = _baoCaoDao.BaoCaoKetQuaHocVien(KhoaHocID, 2);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;


            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        private void mn_XoaPhienHocLoginIsNull_Click(object sender, EventArgs e)
        {
            List<Guid> list = _phienHocDao.GetlstPhienHocLoginIsNull(KhoaHocID);
            int sophienhocLoginIsNull = list.Count();
            if (sophienhocLoginIsNull == 0)
            {
                XtraMessageBox.Show("Không có phiên học Login Is Null !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (XtraMessageBox.Show($"Có {sophienhocLoginIsNull} phiên học Login Is Null ! \n Bạn có chắc muốn xóa {sophienhocLoginIsNull} phiên học Login Is Null? " , "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    _phienHocDao.DeletePhienHocLoginIsNull(list);
                }
            }
        }

        private void btn_ExportResultToExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TenKhoaHoc))
            {
                XtraMessageBox.Show("Vui lòng chọn khóa học ở danh sách bên trái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cb_StyleExport.Checked)
            {
                ExportGrid(grid_Result, TenKhoaHoc);
            }
            else
            {
                ExportExcelWithName.ExportToExcel(dt, TenKhoaHoc, TenKhoaHoc);
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nguoiLxId = gridView_Result.GetFocusedRowCellValue("NguoiLxID");
            if (nguoiLxId == null) return;

            var resultBC = _baoCaoDao.GetBaoCaoHocVienByNguoilxID(Convert.ToInt64(nguoiLxId));
            new ReportPrintTool(new rptResultDAT { DataSource = resultBC }).ShowPreview();
        }

        #endregion Kết Quả

        #region Tổng hợp giờ Học Viên trong ngày

        private void LoadTongHopHvForSelectedDay()
        {
            string day = dtp_TongHopHvByDay.DateTime.ToString("dd/MM/yyyy");
            lb_Tonghophocvienbyday.Text = "Tổng hợp giờ học của học viên ngày " + day;
            _ = LoadSumTimeHVAsync(day);
        }

        private async Task LoadSumTimeHVAsync(string day)
        {
            int version = ++_hvVersion;
            try
            {
                var list = await Task.Run(() => _baoCaoDao.TongHopGioHVByDay(day)
                    .Select(item =>
                    {
                        // Parse an toàn thay cho Remove(2, 6) (lỗi nếu chuỗi không đúng 8 ký tự "HH:mm:ss")
                        item.GhiChu = ParseDuration(item.TongThoiGianLD_Phien).TotalHours >= 10 ? "Quá giờ!" : "";
                        return item;
                    })
                    .ToList());
                if (!IsAlive || version != _hvVersion) return;

                lstSum = list;
                gridControl_TongHopHvByDay.DataSource = lstSum;
                GridHelper.EnableRowNumbering(gridView_TongHopHvByDay);
            }
            catch (Exception ex)
            {
                if (version == _hvVersion) ShowLoadError("tổng hợp giờ học viên", ex);
            }
        }

        private void btn_ViewTongHopHv_Click(object sender, EventArgs e)
        {
            LoadTongHopHvForSelectedDay();
        }

        private void dtp_TongHopHvByDay_EditValueChanged(object sender, EventArgs e)
        {
            if (_suppressLoad) return;
            // Tab đang ẩn: đánh dấu để tải khi người dùng mở tab
            if (!IsTabVisible(xtraTab_TongHopHV)) { _staleTabs.Add(xtraTab_TongHopHV); return; }
            LoadTongHopHvForSelectedDay();
        }

        #endregion Tổng hợp giờ Học Viên trong ngày

        #region Tổng hợp giờ dạy của giáo viên trong ngày

        private void dtp_TongHopGioDay_EditValueChanged(object sender, EventArgs e)
        {
            OnTongHopGvFilterChanged();
        }

        private void btn_TongHopGioGv_Click(object sender, EventArgs e)
        {
            TongHopGvTrongNgay();
        }
        private void cbb_TongHopGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTongHopGvFilterChanged();
        }

        private void OnTongHopGvFilterChanged()
        {
            if (_suppressLoad) return;
            if (!IsTabVisible(xtraTab_TongHopGV)) { _staleTabs.Add(xtraTab_TongHopGV); return; }
            TongHopGvTrongNgay();
        }

        private void TongHopGvTrongNgay()
        {
            flag = cbb_TongHopGv.EditValue?.ToString() == "Theo xe" ? 1 : 0;
            DateTime date = dtp_TongHopGioDay.DateTime;
            string day = date.ToString("dd/MM/yyyy");
            lb_TongHopGvByDay.Text = "Tổng hợp giờ dạy của giáo viên ngày " + day;
            _ = LoadTongHopGVAsync(day, flag, date.DayOfWeek);
        }

        private async Task LoadTongHopGVAsync(string day, int flag, DayOfWeek wk)
        {
            int version = ++_gvVersion;
            try
            {
                // Đã bỏ truy vấn GetLstGvTrue: kết quả của nó không được dùng (đoạn kiểm tra đã bị comment)
                var list = await Task.Run(() =>
                {
                    var items = _baoCaoDao.TongHopGVsByDay(day, flag);
                    foreach (TongHopGV item in items)
                    {
                        TimeSpan thoiGian = ParseDuration(item.SThoiGian);

                        // Thứ 2 tối đa 5 giờ, các ngày khác tối đa 10 giờ (khớp với mn_ContextThongKeGiaoVien: 18000s / 36000s).
                        // Trước đây điều kiện thứ 2 là "hour == 5" nên dạy 6-9 giờ thứ 2 không bị cảnh báo.
                        double gioiHanPhut = wk == DayOfWeek.Monday ? 5 * 60 : 10 * 60;
                        if (thoiGian.TotalMinutes > gioiHanPhut)
                            item.SGhiChu = wk == DayOfWeek.Monday ? "Quá giờ dạy Thứ 2! " : "Quá Giờ! ";
                    }
                    return items;
                });
                if (!IsAlive || version != _gvVersion) return;

                lstSumGV = list;
                ShowTongHopGV();
            }
            catch (Exception ex)
            {
                if (version == _gvVersion) ShowLoadError("tổng hợp giờ dạy giáo viên", ex);
            }
        }

        private void ShowTongHopGV()
        {
            gridControl_TongHopGioDay.DataSource = lstSumGV;
            GridHelper.EnableRowNumbering(gridView_TongHopGioDay);
        }

        #endregion Tổng hợp giờ dạy của giáo viên trong ngày

        #region Phiên học lỗi trong ngày

        private void btn_ViewPhLoi_Click(object sender, EventArgs e)
        {
            string day = dtp_PhienHocLoiByDay.DateTime.ToString("dd/MM/yyyy");
            lbl_PhienHocLoi.Text = "Phiên học lỗi ngày " + day;
            LoadAllPhLoi(day);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHocLoi.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Không có phiên học cần xóa!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var diemdanhnguoilxId = gridView_PhienHocLoi.GetRowCellValue(focusedRowHandle, "MaPhien");
                string day = dtp_PhienHocLoiByDay.DateTime.ToString("dd/MM/yyyy");
                if (XtraMessageBox.Show(string.Format("Bạn có chắc chắn xóa phiên học \n{0}", diemdanhnguoilxId), "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    _phienHocDao.DeletePhByID(diemdanhnguoilxId.ToString());
                    LoadAllPhLoi(day);
                }
            }
        }

        private void LoadAllPhLoi(string day)
        {
            List<PhienHoc> lstPhLoi = _phienHocDao.GetLstPhienLoiByDate(day);
            // Chỉ tính cảnh báo; trước đây gọi CheckPhInList nên lưới + bộ đếm của tab "Phiên học" bị ghi đè
            ProcessPhienHoc(lstPhLoi);
            gridControl_PhienHocLoi.DataSource = lstPhLoi;
            GridHelper.EnableRowNumbering(gridView_PhienHocLoi);
        }

        #endregion Phiên học lỗi trong ngày

        #region Phiên học sai

        private void btn_ViewPhienHocSai_Click(object sender, EventArgs e)
        {
            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
        }

        private void LoadLstPhienHocBo(string from, string to)
        {
            List<PhienHoc> lst = _phienHocDao.TongHopPhienHocLoi(from, to);

            ConvertListToDataTable convert = new ConvertListToDataTable();
            dt = convert.ToDataTable(lst);

            gridControl_PhienHocBo.DataSource = lst;
        }

        private void mn_refresh_Click(object sender, EventArgs e)
        {
            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
        }

        private void mn_UnlockSession_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHocBo);
            if (row == null) return;

            if (XtraMessageBox.Show($"Sử dụng lại phiên học {row.MaPhien}?", "Xác nhận",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            _phienHocDao.UnlockSession(row.MaPhien.ToString());
            LoadLstPhienHocBo(dtp_From.DateTime.ToString("yyyyMMdd"), dtp_To.DateTime.ToString("yyyyMMdd"));
        }

        private void mn_DeletePhienHoc_Click(object sender, EventArgs e)
        {
            var row = FocusedRow<PhienHoc>(gridView_PhienHocBo);
            if (row == null) return;

            if (XtraMessageBox.Show($"Xóa phiên học {row.MaPhien}?\n\nThao tác này không thể hoàn tác.", "Xác nhận xóa",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;

            _phienHocDao.DeletePhByID(row.MaPhien.ToString());
            LoadLstPhienHocBo(dtp_From.DateTime.ToString("yyyyMMdd"), dtp_To.DateTime.ToString("yyyyMMdd"));
        }

        private void btn_ExportPhSai_Click(object sender, EventArgs e)
        {
            ExportGrid(gridControl_PhienHocBo, $"Phien DAT bi bo {dtp_From.DateTime:dd-MM-yyyy} den {dtp_To.DateTime:dd-MM-yyyy}");
        }


        #endregion Phiên học sai

        private void mn_DetailByMaDK_Click(object sender, EventArgs e)
        {
            ShowQuaTrinhDaoTao();
        }

        private void grid_Result_DoubleClick(object sender, EventArgs e)
        {
            ShowQuaTrinhDaoTao();
        }

        private void ShowQuaTrinhDaoTao()
        {
            // Double-click vào dòng nhóm (Giáo viên: ...) hoặc vùng trống thì không có mã ĐK
            var maDK = gridView_Result.GetFocusedRowCellValue("View_MaDK") as string;
            if (string.IsNullOrEmpty(maDK)) return;

            using (frmQuaTrinhDaoTao f = new frmQuaTrinhDaoTao(maDK))
                f.ShowDialog(FindForm());
        }

        private void gridControl_PhienHoc_MouseDown(object sender, MouseEventArgs e)
        {
            // Kiểm tra chuột phải
            if (e.Button == MouseButtons.Right)
            {
                // Kiểm tra tổ hợp Shift + Ctrl
                if (Control.ModifierKeys == (Keys.Shift | Keys.Control))
                {
                    // Hiển thị các item ẩn
                    menu_Phienhoc.Items["tstrip_UpdateImei"].Visible = true;
                }
                else
                {
                    // Ẩn các item ẩn khi không nhấn Shift + Ctrl
                    menu_Phienhoc.Items["tstrip_UpdateImei"].Visible = false;
                }

                // Hiển thị ContextMenuStrip tại vị trí chuột
                menu_Phienhoc.Show(gridControl_PhienHoc, e.Location);
            }
        }

        private void tstrip_UpdateImei_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Cập nhật thông tin Imei + Serial cho phiên học này?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                var focusedRowHandle = gridView_PhienHoc.GetFocusedRow() as PhienHoc;
                if (focusedRowHandle != null)
                {
                    _phienHocDao.UpdateImeiSerial(focusedRowHandle.MaPhien.ToString());
                    LoadPhienHoc();
                }
            }
        }

        private void mn_Checkhighway_Click(object sender, EventArgs e)
        {
            List<PhienHoc> lstNull = new List<PhienHoc>();

            DateTime selected = dtp_PhByDay.DateTime;

            //string day = dtp_PhByDay.DateTime.ToString("yyyy-MM-dd 00:00:00");
            //string day = dtp_PhByDay.DateTime.ToString("dd/MM/yyyy");
            //DateTime fromDay = new DateTime(selected.Year, selected.Month, selected.Day, 0, 0, 0);
            //DateTime toDay = fromDay.AddDays(1);

            //List<Guid> lstGuid = _DAO.CheckHighway(fromDay, toDay);

            //gridControl_PhienHoc.DataSource = lstNull;

            //var result = lstPH.Where(x => lstGuid.Contains(x.MaPhien)).ToList();
            //gridControl_PhienHoc.DataSource = result;

        }

        private void mn_OptimizeTime_Click(object sender, EventArgs e)
        {
            var focusedRow = gridView_PhienHoc.GetFocusedRow() as PhienHoc;
            if (focusedRow != null)
            {
                if (XtraMessageBox.Show("Tối ưu thời gian cho phiên học này?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    _phienHocDao.OptimizeTime(focusedRow.NguoiLxID, focusedRow.MaPhien.ToString());
                    LoadPhienHoc();
                }
            }
        }
    }
}