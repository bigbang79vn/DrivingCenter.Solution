using DevExpress.Images;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.Enums;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_GPLX : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly string[] ImportColumns =
        {
            "HoVaTen", "NgaySinh", "GioiTinh", "SoCMT", "NgayCapCMT", "NoiCapCMT",
            "NoiTT_MaDVHC", "NoiCT_MaDVHC", "SoGPLXDaCo", "HangGPLXDaCo",
            "NgayTTGPLXDaCo", "NgayCapGPLXDaCo", "DonViCapGPLXDaCo"
        };

        private static readonly string[] UpdateColumns = { "MaDK", "Lop", "SoCNTN" };

        // Cột không được để trống — ô trống sẽ được tô đỏ trong lưới xem trước
        private static readonly string[] ImportCriticalColumns = { "HoVaTen", "NgaySinh", "GioiTinh", "SoCMT" };


        private static readonly Color ColorBrand = Color.FromArgb(78, 109, 156);
        private static readonly Color ColorSuccess = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorMuted = Color.FromArgb(203, 213, 225);
        private static readonly Color ColorErrorBg = Color.FromArgb(254, 226, 226);
        private static readonly Color ColorErrorFg = Color.FromArgb(185, 28, 28);

        private readonly Dictionary<LabelControl, StatusKind> _statusKinds = new Dictionary<LabelControl, StatusKind>();
        private bool _importSheetValid;
        private bool _updateSheetValid;

        private readonly GPLX_DAO _gplx_Dao = new GPLX_DAO();
        private readonly GPLX_CDB_CSDT_DAO _DAO = new GPLX_CDB_CSDT_DAO();

        private List<KhoaHocGplx> _lstKhoaHoc = new List<KhoaHocGplx>();
        private KhoaHocGplx kh;

        // Mỗi tab giữ dữ liệu Excel riêng, tránh đọc file ở tab này ghi đè tab kia
        private DataTableCollection _importTables;
        private DataTableCollection _updateTables;
        private DataTable dt;

        public UcChild_GPLX()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void UcChild_GPLX_Load(object sender, EventArgs e)
        {
            dtp_NgayQD.DateTime = DateTime.Now;

            lbl_ImportColumns.Text = "Sheet phải có các cột: " + string.Join(", ", ImportColumns);
            lbl_UpdateColumns.Text = "Sheet phải có các cột: " + string.Join(", ", UpdateColumns);
            SetStatus(lbStatus, "Chưa chọn file. Bấm nút … ở bước 1 để bắt đầu.", StatusKind.Neutral);
            SetStatus(lbStatusUpdate, "Chưa chọn file. Bấm nút … ở bước 1 để bắt đầu.", StatusKind.Neutral);

            LoadKH();
            LoadlstStudentInReportCNTN();
        }

        #region Giao diện

        private static SvgImage Svg(string key)
        {
            try { return ImageResourceCache.Default.GetSvgImage(key); }
            catch { return null; }
        }

        private void ApplyTheme()
        {
            // Icon cho tab
            var tabIconSize = new Size(20, 20);
            tab_Import.ImageOptions.SvgImage = Svg("svgimages/scheduling/import.svg");
            tab_UpdateResult.ImageOptions.SvgImage = Svg("svgimages/outlook%20inspired/tasks.svg");
            tab_Certificate.ImageOptions.SvgImage = Svg("svgimages/print/print.svg");
            foreach (DevExpress.XtraTab.XtraTabPage page in tabControl.TabPages)
                page.ImageOptions.SvgImageSize = tabIconSize;

            // Nút hành động chính: nền màu thương hiệu
            SetButtonIcon(btn_Import, "svgimages/scheduling/import.svg");
            SetButtonIcon(btn_UpdateResult, "svgimages/icon%20builder/actions_checkcircled.svg");
            SetButtonIcon(btn_ExportExcel, "svgimages/export/exporttoxlsx.svg");
            foreach (var btn in new[] { btn_Import, btn_UpdateResult, btn_PrintB11, btn_PrintB2, btn_PrintCertificate })
                StylePrimary(btn);

            // Lưới: cùng một phong cách
            foreach (var view in new[] { gridView_ImportData, gridView_UpdateResult, gridView_Certificate })
            {
                view.RowHeight = 30;
                view.ColumnPanelRowHeight = 34;
                view.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
                view.OptionsView.EnableAppearanceEvenRow = true;
                view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.Appearance.HeaderPanel.ForeColor = ColorBrand;
                view.Appearance.HeaderPanel.Options.UseForeColor = true;
            }
            gridView_Certificate.CustomColumnDisplayText += gridView_Certificate_CustomColumnDisplayText;
            gridView_ImportData.RowCellStyle += gridView_Preview_RowCellStyle;
            gridView_UpdateResult.RowCellStyle += gridView_Preview_RowCellStyle;

            txt_SoQD.EditValueChanged += (s, e) => RefreshSteps();
            toggleSwitch_UpdateResult.Toggled += (s, e) => RefreshSteps();
        }

        private static void SetButtonIcon(SimpleButton button, string svgKey) => UiTheme.SetIcon(button, svgKey);

        private static void StylePrimary(SimpleButton button) => UiTheme.StylePrimary(button);

        private void RefreshSteps()
        {
            grp_ImportSide.Invalidate();
            grp_UpdateSide.Invalidate();
        }

        /// <summary>
        /// Vạch nhấn màu thương hiệu bên trái + đường kẻ mảnh phía dưới cho header của tab.
        /// </summary>
        private void pnl_Header_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Control)sender;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var accent = new SolidBrush(ColorBrand))
                e.Graphics.FillRectangle(accent, 0, 8, 4, panel.Height - 16);
            using (var line = new Pen(ColorMuted))
                e.Graphics.DrawLine(line, 0, panel.Height - 1, panel.Width, panel.Height - 1);
        }

        /// <summary>
        /// Vẽ stepper (số thứ tự / dấu ✓ nối bằng đường dọc) và ô trạng thái có màu.
        /// </summary>
        private void grp_Side_Paint(object sender, PaintEventArgs e)
        {
            bool isImport = sender == grp_ImportSide;
            LabelControl[] labels = isImport
                ? new[] { lbl_ImportStep1, lbl_ImportStep2, lbl_ImportStep3 }
                : new[] { lbl_UpdateStep1, lbl_UpdateStep2, lbl_UpdateStep3 };
            bool[] done = isImport
                ? new[] { _importTables != null, _importSheetValid, kh != null }
                : new[] { _updateTables != null, _updateSheetValid, !toggleSwitch_UpdateResult.IsOn || !string.IsNullOrWhiteSpace(txt_SoQD.Text) };
            int active = Array.IndexOf(done, false);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            const int r = 13;
            const int cx = 27;

            for (int i = 0; i < labels.Length - 1; i++)
            {
                int y1 = labels[i].Top + labels[i].Height / 2 + r + 3;
                int y2 = labels[i + 1].Top + labels[i + 1].Height / 2 - r - 3;
                using (var pen = new Pen(done[i] ? ColorSuccess : ColorMuted, 2))
                    g.DrawLine(pen, cx, y1, cx, y2);
            }

            using (var font = new Font("Roboto", 9.75F, FontStyle.Bold))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    int cy = labels[i].Top + labels[i].Height / 2;
                    var circle = new Rectangle(cx - r, cy - r, r * 2, r * 2);

                    if (done[i])
                    {
                        using (var b = new SolidBrush(ColorSuccess)) g.FillEllipse(b, circle);
                        g.DrawString("✓", font, Brushes.White, circle, format);
                    }
                    else if (i == active)
                    {
                        using (var b = new SolidBrush(ColorBrand)) g.FillEllipse(b, circle);
                        g.DrawString((i + 1).ToString(), font, Brushes.White, circle, format);
                    }
                    else
                    {
                        using (var pen = new Pen(ColorMuted, 2)) g.DrawEllipse(pen, circle);
                        using (var b = new SolidBrush(Color.Gray)) g.DrawString((i + 1).ToString(), font, b, circle, format);
                    }
                }
            }

            // Ô trạng thái: nền nhạt + vạch màu bên trái
            LabelControl status = isImport ? lbStatus : lbStatusUpdate;
            _statusKinds.TryGetValue(status, out StatusKind kind);
            GetStatusColors(kind, out Color bg, out Color accent, out _);
            var box = new Rectangle(14, status.Top - 9, 350, status.Height + 18);
            using (var b = new SolidBrush(bg)) g.FillRectangle(b, box);
            using (var b = new SolidBrush(accent)) g.FillRectangle(b, box.X, box.Y, 4, box.Height);
        }

        private static void GetStatusColors(StatusKind kind, out Color bg, out Color accent, out Color text)
        {
            switch (kind)
            {
                case StatusKind.Success:
                    bg = Color.FromArgb(220, 252, 231); accent = ColorSuccess; text = Color.FromArgb(21, 128, 61); break;
                case StatusKind.Warning:
                    bg = Color.FromArgb(254, 243, 199); accent = Color.FromArgb(217, 119, 6); text = Color.FromArgb(180, 83, 9); break;
                case StatusKind.Error:
                    bg = ColorErrorBg; accent = Color.FromArgb(220, 38, 38); text = ColorErrorFg; break;
                case StatusKind.Info:
                    bg = Color.FromArgb(219, 234, 254); accent = ColorBrand; text = Color.FromArgb(30, 64, 175); break;
                default:
                    bg = Color.FromArgb(241, 245, 249); accent = Color.FromArgb(148, 163, 184); text = Color.FromArgb(71, 85, 105); break;
            }
        }

        private void gridView_Preview_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string[] critical = sender == gridView_ImportData ? ImportCriticalColumns : UpdateColumns;
            if (!critical.Contains(e.Column.FieldName)) return;

            object value = ((GridView)sender).GetRowCellValue(e.RowHandle, e.Column);
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                e.Appearance.BackColor = ColorErrorBg;
                e.Appearance.ForeColor = ColorErrorFg;
            }
        }

        /// <summary>
        /// NgayKG/NgayBG là chuỗi dạng "dd/MM/yyyy HH:mm:ss" — chỉ hiển thị phần ngày.
        /// </summary>
        private void gridView_Certificate_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if ((e.Column == NgayKG || e.Column == NgayBG) && e.Value is string s && s.Length > 10
                && DateTime.TryParse(s, CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out DateTime d))
            {
                e.DisplayText = d.ToString("dd/MM/yyyy");
            }
        }

        private static int CountInvalidRows(DataTable table, string[] criticalColumns)
        {
            var cols = criticalColumns.Where(table.Columns.Contains).ToList();
            return table.Rows.Cast<DataRow>()
                .Count(row => cols.Any(c => row[c] == DBNull.Value || string.IsNullOrWhiteSpace(row[c].ToString())));
        }

        #endregion

        #region Dùng chung

        private static DataTableCollection ReadExcel(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                });
                return result.Tables;
            }
        }

        /// <summary>
        /// Mở hộp thoại chọn file Excel, đọc các sheet và đổ vào combobox.
        /// </summary>
        private DataTableCollection PickExcelFile(ButtonEdit fileEdit, ComboBoxEdit sheetCombo, LabelControl status)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return null;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    DataTableCollection tables = ReadExcel(openFileDialog.FileName);
                    fileEdit.Text = openFileDialog.FileName;

                    sheetCombo.Properties.Items.Clear();
                    foreach (DataTable table in tables)
                        sheetCombo.Properties.Items.Add(table.TableName);

                    if (tables.Count == 0)
                    {
                        SetStatus(status, "File không có sheet nào.", StatusKind.Warning);
                        return null;
                    }
                    return tables;
                }
                catch (IOException ex)
                {
                    XtraMessageBox.Show($"Không thể đọc file vì đang được sử dụng bởi ứng dụng khác.\nVui lòng đóng file Excel và thử lại.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
                return null;
            }
        }

        /// <summary>
        /// Hiển thị sheet lên grid xem trước và kiểm tra đủ cột bắt buộc.
        /// </summary>
        private bool BindPreview(DataTable table, GroupControl group, DevExpress.XtraGrid.GridControl grid,
            GridView view, string[] requiredColumns, string[] criticalColumns, LabelControl status)
        {
            grid.DataSource = table;
            GridHelper.EnableRowNumbering(view);
            view.BestFitColumns();

            var missing = requiredColumns
                .Where(c => !table.Columns.Contains(c))
                .ToList();
            int invalid = CountInvalidRows(table, criticalColumns);

            group.Text = $"XEM TRƯỚC  ·  {table.TableName}  ·  {table.Rows.Count} dòng  ·  {table.Columns.Count} cột" +
                         (invalid > 0 ? $"  ·  {invalid} dòng thiếu dữ liệu" : string.Empty);

            if (missing.Count > 0)
            {
                SetStatus(status, "Sheet thiếu cột: " + string.Join(", ", missing), StatusKind.Error);
                return false;
            }
            if (table.Rows.Count == 0)
            {
                SetStatus(status, "Sheet không có dữ liệu.", StatusKind.Warning);
                return false;
            }
            if (invalid > 0)
            {
                SetStatus(status, $"{table.Rows.Count} dòng, trong đó {invalid} dòng thiếu dữ liệu bắt buộc (ô tô đỏ). Kiểm tra lại trước khi thực hiện.", StatusKind.Warning);
                return true;
            }

            SetStatus(status, $"Sẵn sàng: {table.Rows.Count} dòng dữ liệu hợp lệ.", StatusKind.Info);
            return true;
        }

        private void SetStatus(LabelControl label, string text, StatusKind kind)
        {
            GetStatusColors(kind, out _, out _, out Color textColor);
            label.Text = text;
            label.Appearance.ForeColor = textColor;
            _statusKinds[label] = kind;
            RefreshSteps();
        }

        private void gridView_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            var view = (GridView)sender;
            if (view.RowCount > 0) return;

            string title, hint;
            if (view == gridView_Certificate)
            {
                title = "Chưa có dữ liệu in chứng chỉ";
                hint = "Cập nhật kết quả tốt nghiệp ở tab \"Cập nhật kết quả tốt nghiệp\" để có danh sách in.";
            }
            else
            {
                title = "Chưa có dữ liệu xem trước";
                hint = "Chọn file Excel ở bước 1 bên trái, dữ liệu sẽ hiển thị tại đây.";
            }

            var center = new Point(e.Bounds.X + e.Bounds.Width / 2, e.Bounds.Y + e.Bounds.Height / 2);
            using (var titleFont = new Font("Roboto", 13F, FontStyle.Bold))
            using (var hintFont = new Font("Roboto", 9.75F))
            using (var titleBrush = new SolidBrush(ColorBrand))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Cache.DrawString(title, titleFont, titleBrush, new Rectangle(e.Bounds.X, center.Y - 30, e.Bounds.Width, 28), format);
                e.Cache.DrawString(hint, hintFont, Brushes.Gray, new Rectangle(e.Bounds.X, center.Y + 2, e.Bounds.Width, 22), format);
            }
            e.Handled = true;
        }

        #endregion

        #region Import Data

        private void LoadKH()
        {
            _lstKhoaHoc = _DAO.GetlstKh();
            cbx_KhoaHoc.Properties.DataSource = _lstKhoaHoc;
            cbx_KhoaHoc.Properties.DisplayMember = "TenKH";
            cbx_KhoaHoc.Properties.ValueMember = "MaKH";

            if (_lstKhoaHoc.Count > 0)
            {
                cbx_KhoaHoc.EditValue = _lstKhoaHoc[0].MaKH;
            }
        }

        public string ConvertNgaySinh(string birthday)
        {
            if (DateTime.TryParse(birthday, CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out DateTime ngaysinh))
            {
                return ngaysinh.ToString("yyyyMMdd");
            }
            return string.Empty;
        }

        private DataTable CurrentImportTable =>
            _importTables != null && cb_SheetDataImport.SelectedItem != null
                ? _importTables[cb_SheetDataImport.SelectedItem.ToString()]
                : null;

        private void be_FileImport_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var tables = PickExcelFile(be_FileImport, cb_SheetDataImport, lbStatus);
            if (tables == null) return;

            _importTables = tables;
            cb_SheetDataImport.SelectedIndex = -1;
            cb_SheetDataImport.SelectedIndex = 0;
        }

        private void cb_SheetDataImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = CurrentImportTable;
            if (table == null) return;

            _importSheetValid = BindPreview(table, grp_ImportPreview, grd_ImportData, gridView_ImportData, ImportColumns, ImportCriticalColumns, lbStatus);
            btn_Import.Enabled = _importSheetValid;
            UpdateSoHSRange();
            RefreshSteps();
        }

        private void cbx_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            string selectedMaKH = cbx_KhoaHoc.EditValue?.ToString();
            kh = _lstKhoaHoc.FirstOrDefault(x => x.MaKH == selectedMaKH);

            txtHangDaoTao.Text = kh?.HangDT ?? string.Empty;
            RefreshSteps();
        }

        private void nbmSoHS_EditValueChanged(object sender, EventArgs e)
        {
            UpdateSoHSRange();
        }

        private void UpdateSoHSRange()
        {
            int count = CurrentImportTable?.Rows.Count ?? 0;
            int start = (int)nbmSoHS.Value;

            lbl_SoHSRange.Text = count > 0
                ? $"Số hồ sơ sẽ cấp: {start:000} → {start + count - 1:000} ({count} học viên)"
                : "Số hồ sơ sẽ cấp: --";
        }

        private async void btn_Import_Click(object sender, EventArgs e)
        {
            DataTable dt = CurrentImportTable;
            if (dt == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn Sheet chứa Data!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_SheetDataImport.Focus();
                return;
            }
            if (kh == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbx_KhoaHoc.Focus();
                return;
            }

            List<NguoiLxDto> lstNguoiLX = new List<NguoiLxDto>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Tạo mới NguoiLX
                NguoiLxDto nlx = new NguoiLxDto();

                nlx.MaKH = kh.MaKH;
                nlx.MaHangGPLX = kh.HangGPLX;
                nlx.HangDaoTao = kh.HangDT;
                nlx.SoHoSo = string.Format("{0:000}", (int)nbmSoHS.Value + i).ToString();
                nlx.NguoiNhanHSo = "";
                nlx.HoVaTen = dt.Rows[i]["HoVaTen"].ToString().Trim().ToUpper();
                nlx.HoVaTenIn = nlx.HoVaTen.ToUpper();
                nlx.NgaySinh = ConvertNgaySinh(dt.Rows[i]["NgaySinh"].ToString());
                nlx.GioiTinh = dt.Rows[i]["GioiTinh"].ToString() == "Nam" ? "M" : "F";
                nlx.MaQuocTich = "VNM";
                nlx.SoCMT = dt.Rows[i]["SoCMT"].ToString();
                nlx.NgayCap = ConvertNgaySinh(dt.Rows[i]["NgayCapCMT"].ToString());
                nlx.NoiCap = string.Format("{0:00}", dt.Rows[i]["NoiCapCMT"]).ToString();
                nlx.MaDVHC_TT = dt.Rows[i]["NoiTT_MaDVHC"].ToString();
                nlx.ChiTiet_TT = "";
                nlx.MaDVHC_CT = dt.Rows[i]["NoiCT_MaDVHC"].ToString();
                nlx.ChiTiet_CT = "";
                nlx.SoNamLX = 0;
                nlx.SoKmLX = 0;
                nlx.GiayTo = "1,2,3,";
                nlx.SoGPLXDaCo = dt.Rows[i]["SoGPLXDaCo"].ToString();
                nlx.HangGPLXDaCo = dt.Rows[i]["HangGPLXDaCo"].ToString();
                nlx.NgayTTGPLXDaCo = dt.Rows[i]["NgayTTGPLXDaCo"].ToString();
                nlx.NgayCapGPLXDaCo = dt.Rows[i]["NgayCapGPLXDaCo"].ToString();
                nlx.NgayHHGPLXDaCo = "";
                nlx.DVCapGPLXDaCo = dt.Rows[i]["DonViCapGPLXDaCo"].ToString();
                nlx.NoiCapGPLXDaCo = "";
                nlx.GhiChu = "";
                nlx.SO_CMND_CU = "";
                nlx.CHON_IN_GPLX = 1;

                lstNguoiLX.Add(nlx);
            }

            string confirm = $"Import {lstNguoiLX.Count} học viên vào khóa:\n\n" +
                             $"   • Khóa học: {kh.TenKH} ({kh.MaKH})\n" +
                             $"   • Hạng đào tạo: {kh.HangDT}\n" +
                             $"   • Số hồ sơ: {lstNguoiLX.First().SoHoSo} → {lstNguoiLX.Last().SoHoSo}\n" +
                             (CountInvalidRows(dt, ImportCriticalColumns) is int invalid && invalid > 0
                                ? $"\n⚠ Có {invalid} dòng thiếu họ tên / ngày sinh / giới tính / số CMT.\n"
                                : string.Empty) +
                             "\n" +
                             "Bạn chắc chắn thông tin trên là đúng?";

            if (XtraMessageBox.Show(confirm, "Xác nhận import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                SetStatus(lbStatus, "Đã hủy. Kiểm tra lại khóa học / số hồ sơ rồi thử lại.", StatusKind.Warning);
                cbx_KhoaHoc.Focus();
                return;
            }

            btn_Import.Enabled = false;
            SetStatus(lbStatus, $"Đang import {lstNguoiLX.Count} học viên...", StatusKind.Info);
            try
            {
                bool rs = await Task.Run(() => _DAO.InsertHocVien(lstNguoiLX));

                if (rs)
                {
                    SetStatus(lbStatus, $"Import thành công {lstNguoiLX.Count} học viên cho khóa {kh.TenKH}.", StatusKind.Success);
                    XtraMessageBox.Show($"Import thành công {lstNguoiLX.Count} học viên cho khóa {kh.TenKH}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SetStatus(lbStatus, "Import thất bại! Vui lòng kiểm tra lại dữ liệu.", StatusKind.Error);
                    XtraMessageBox.Show("Import thất bại! Vui lòng kiểm tra lại dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                SetStatus(lbStatus, "Lỗi: " + ex.Message, StatusKind.Error);
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_Import.Enabled = true;
            }
        }

        #endregion

        #region Update Result Graduate

        private void be_FileUpdate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var tables = PickExcelFile(be_FileUpdate, cbb_SheetDataUpdate, lbStatusUpdate);
            if (tables == null) return;

            _updateTables = tables;
            cbb_SheetDataUpdate.SelectedIndex = -1;
            cbb_SheetDataUpdate.SelectedIndex = 0;
        }

        private void cbb_SheetDataUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updateTables == null || cbb_SheetDataUpdate.SelectedItem == null) return;

            dt = _updateTables[cbb_SheetDataUpdate.SelectedItem.ToString()];
            _updateSheetValid = BindPreview(dt, grp_UpdatePreview, gridControl_UpdateResult, gridView_UpdateResult, UpdateColumns, UpdateColumns, lbStatusUpdate);
            btn_UpdateResult.Enabled = _updateSheetValid;
            RefreshSteps();
        }

        private void dtp_NgayQD_EditValueChanged(object sender, EventArgs e)
        {
            dtp_NgayVaoSo.DateTime = dtp_NgayQD.DateTime;
            dtp_NgayIn.DateTime = dtp_NgayQD.DateTime;
        }

        private async void btn_UpdateResult_Click(object sender, EventArgs e)
        {
            if (dt == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn Sheet chứa Data!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbb_SheetDataUpdate.Focus();
                return;
            }

            bool updateResult = toggleSwitch_UpdateResult.IsOn;
            if (updateResult && string.IsNullOrWhiteSpace(txt_SoQD.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập Số quyết định tốt nghiệp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_SoQD.Focus();
                return;
            }

            //Khởi tạo danh sách
            List<NguoiLX_HoSo_Update> lstNguoiLxUpdates = new List<NguoiLX_HoSo_Update>();
            List<HvTotNghiep> lstNguoiLxCCTN = new List<HvTotNghiep>();
            List<int> invalidRows = new List<int>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string maDK = dt.Rows[i]["MaDK"].ToString().ToUpper().Trim();
                string lop = dt.Rows[i]["Lop"].ToString().ToUpper().Trim();
                if (string.IsNullOrEmpty(maDK) || lop.Length < 2)
                {
                    invalidRows.Add(i + 1);
                    continue;
                }

                //Cập nhật kết quả tốt nghiệp
                NguoiLX_HoSo_Update nguoiLx = new NguoiLX_HoSo_Update();
                nguoiLx.MaDK = maDK;
                string hang = lop.Substring(0, 2);
                nguoiLx.SoGiayCNTN = txt_SoCNTN.Text + (hang == "C1" ? "C1" : "B") + "|" + dt.Rows[i]["SoCNTN"].ToString().ToUpper().Trim();
                nguoiLx.TT_XuLy = "09";
                nguoiLx.SoQuyetDinhTN = txt_SoQD.Text;
                nguoiLx.NgayRaQDTN = dtp_NgayQD.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                nguoiLx.SoSoTN = txt_SoSoCNTN.Text;
                nguoiLx.NgayVaoSoTN = dtp_NgayVaoSo.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                nguoiLx.NgayInGiayTN = dtp_NgayIn.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                lstNguoiLxUpdates.Add(nguoiLx);

                //Cập nhật dữ liệu in chứng chỉ tốt nghiệp
                HvTotNghiep nguoiLxCCTN = new HvTotNghiep();
                nguoiLxCCTN.MaDK = dt.Rows[i]["MaDK"].ToString().ToUpper();
                nguoiLxCCTN.NgayQDTN = dtp_NgayQD.DateTime.ToString("dd/MM/yyyy");
                lstNguoiLxCCTN.Add(nguoiLxCCTN);
            }

            string confirm = $"Cập nhật {lstNguoiLxCCTN.Count} học viên:\n\n" +
                             (updateResult
                                ? $"   • Kết quả TN: QĐ số {txt_SoQD.Text} ngày {dtp_NgayQD.DateTime:dd/MM/yyyy}\n"
                                : "   • Không cập nhật kết quả TN\n") +
                             "   • Làm mới dữ liệu in CCTN (xóa dữ liệu in cũ)\n" +
                             (invalidRows.Count > 0 ? $"\nBỏ qua {invalidRows.Count} dòng thiếu MaDK/Lop: {string.Join(", ", invalidRows.Take(10))}{(invalidRows.Count > 10 ? "..." : "")}\n" : "") +
                             "\nBạn chắc chắn muốn tiếp tục?";

            if (XtraMessageBox.Show(confirm, "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                SetStatus(lbStatusUpdate, "Đã hủy cập nhật.", StatusKind.Warning);
                return;
            }

            btn_UpdateResult.Enabled = false;
            SetStatus(lbStatusUpdate, $"Đang cập nhật {lstNguoiLxCCTN.Count} học viên...", StatusKind.Info);
            try
            {
                await Task.Run(() =>
                {
                    if (updateResult)
                    {
                        foreach (NguoiLX_HoSo_Update item in lstNguoiLxUpdates)
                            _DAO.UpdateResultGraduate(item);
                    }

                    _DAO.DeleteDataReportOld();
                    foreach (HvTotNghiep item in lstNguoiLxCCTN)
                        _DAO.InsertDataReportCNTN(item);
                });

                string msg = (updateResult ? $"Đã cập nhật kết quả TN {lstNguoiLxUpdates.Count} học viên.\n" : "") +
                             $"Đã cập nhật dữ liệu in CCTN {lstNguoiLxCCTN.Count} học viên.";
                SetStatus(lbStatusUpdate, msg, StatusKind.Success);
                XtraMessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadlstStudentInReportCNTN();
            }
            catch (Exception ex)
            {
                SetStatus(lbStatusUpdate, "Lỗi: " + ex.Message, StatusKind.Error);
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_UpdateResult.Enabled = true;
            }
        }

        #endregion Update Result Graduate

        #region In chứng chỉ tốt nghiệp

        public void LoadlstStudentInReportCNTN()
        {
            List<HvTotNghiep> lst = _gplx_Dao.GetHvTotNghiep();

            grd_Certificate.DataSource = lst;
            GridHelper.EnableRowNumbering(gridView_Certificate);
            UpdateSelectedCount();
        }

        private void gridView_Certificate_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateSelectedCount();
        }

        private void UpdateSelectedCount()
        {
            int count = gridView_Certificate.GetSelectedRows().Count(h => h >= 0);
            lbl_SelectedCount.Text = $"Đã chọn: {count} / {gridView_Certificate.DataRowCount} học viên";
        }

        /// <summary>
        /// Danh sách MaDK của các học viên được tick, dạng ",MA1,MA2" như DAO đang dùng.
        /// </summary>
        private string GetSelectedMaDK()
        {
            string lstId = "";
            foreach (int rowHandle in gridView_Certificate.GetSelectedRows())
            {
                if (rowHandle < 0) continue;
                lstId = lstId + "," + gridView_Certificate.GetRowCellValue(rowHandle, "MaDK");
            }

            if (string.IsNullOrEmpty(lstId))
                XtraMessageBox.Show("Bạn phải chọn học viên cần in chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return lstId;
        }

        private void PrintCCTN(int hangDT)
        {
            string lstId = GetSelectedMaDK();
            if (string.IsNullOrEmpty(lstId)) return;

            DataTable data = _DAO.GetDataPrintCCTN(lstId);
            rptPrintReportCNTN f = new rptPrintReportCNTN(data, hangDT);
            f.Show();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadlstStudentInReportCNTN();
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"DanhSachInCCTN_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    gridView_Certificate.ExportToXlsx(dialog.FileName);
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Không xuất được file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_PrintB11_Click(object sender, EventArgs e)
        {
            PrintCCTN(1);
        }

        private void btn_PrintB2_Click(object sender, EventArgs e)
        {
            PrintCCTN(2);
        }

        private void btn_PrintCertificate_Click(object sender, EventArgs e)
        {
            string lstId = GetSelectedMaDK();
            if (string.IsNullOrEmpty(lstId)) return;

            DataTable data = _gplx_Dao.GetDataPrintCCTN(lstId);
            new ReportPrintTool(new rptCertificate { DataSource = data }).ShowPreview();
        }

        private void btn_SaveInfo_Click(object sender, EventArgs e)
        {
            gridView_Certificate.CloseEditor();
            gridView_Certificate.UpdateCurrentRow();

            int focusedRowHandle = gridView_Certificate.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Hãy chọn (bấm vào) dòng học viên cần lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string HoVaTen = gridView_Certificate.GetRowCellValue(focusedRowHandle, "HoVaTen").ToString();
            string NgaySinh = gridView_Certificate.GetRowCellValue(focusedRowHandle, "NgaySinh").ToString();
            string GioiTinh = gridView_Certificate.GetRowCellValue(focusedRowHandle, "GioiTinh").ToString();
            string SoVaoSo = gridView_Certificate.GetRowCellValue(focusedRowHandle, "SoVaoSo").ToString();
            string MaDK = gridView_Certificate.GetRowCellValue(focusedRowHandle, "MaDK").ToString();

            if (!DateTime.TryParse(NgaySinh, out DateTime ngaysinh))
            {
                XtraMessageBox.Show($"Ngày sinh \"{NgaySinh}\" không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string HoVaTenEng = StringUtil.RemoveVietnameseSign(HoVaTen);
            string GioiTinhEng = GioiTinh == "Nam" ? "Mr" : "Ms";
            string NgaySinhEng = ngaysinh.ToString("dd MMMM yyyy");

            var result = XtraMessageBox.Show(string.Format("Cập nhật thông tin học viên {0}?", HoVaTen), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                _DAO.UpdateInfo(MaDK, HoVaTen, HoVaTenEng, NgaySinh, NgaySinhEng, GioiTinh, GioiTinhEng, SoVaoSo);
            }
            LoadlstStudentInReportCNTN();
        }

        #endregion In chứng chỉ tốt nghiệp
    }
}
