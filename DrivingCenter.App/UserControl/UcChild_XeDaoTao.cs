using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Enums;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_XeDaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        private const int SapHetHanSoNgay = 30;

        private readonly XeTapLaiDAO _DAO = new XeTapLaiDAO();
        private int _IdXeTapLai;
        private bool _isEditing;
        private Font _boldFont;

        public UcChild_XeDaoTao()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pnl_Header.Paint += UiTheme.PaintHeader;

            UiTheme.StylePrimary(btn_ImportXml);
            UiTheme.StylePrimary(btn_Save);
            UiTheme.SetIcon(btn_ImportXml, "svgimages/scheduling/import.svg");
            UiTheme.SetIcon(btn_Export, "svgimages/export/exporttoxlsx.svg");
            UiTheme.SetIcon(btn_Refresh, "svgimages/icon%20builder/actions_refresh.svg");
            UiTheme.SetIcon(btn_Edit, "svgimages/icon%20builder/actions_edit.svg", 16);
            UiTheme.SetIcon(btn_Save, "svgimages/save/save.svg", 16);
            UiTheme.SetIcon(btn_Cancel, "svgimages/icon%20builder/actions_undo.svg", 16);

            UiTheme.StyleGrid(gridView_XeTapLai, "Tìm theo biển số, mã xe, số GPXTL, chủ sở hữu...");

            // Cột bool mặc định vẽ checkbox, bỏ qua DisplayText — dùng editor chữ để hiện "Đang sử dụng/Đã thanh lý"
            var textRepo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            gridControl_XeTapLai.RepositoryItems.Add(textRepo);
            ThanhLy.ColumnEdit = textRepo;

            // Đang sửa thì giữ nguyên dòng đang chọn (không làm mờ cả lưới)
            gridView_XeTapLai.BeforeLeaveRow += (s, e) => { if (_isEditing) e.Allow = false; };
        }

        private void UcChild_XeTap_Load(object sender, EventArgs e)
        {
            LoadXeTapLai();
            SetEditMode(false);
        }

        private void LoadXeTapLai(int? focusVehicleId = null)
        {
            List<XeTapLai> lst = _DAO.GetLstXeTapLai()
                .OrderBy(o => o.BienSoXe)
                .ToList();
            gridControl_XeTapLai.DataSource = lst;
            GridHelper.EnableRowNumbering(gridView_XeTapLai);

            int dangDung = lst.Count(x => !x.ThanhLy);
            int sapHetHan = lst.Count(x => !x.ThanhLy && x.NgayHHXTL.Date <= DateTime.Today.AddDays(SapHetHanSoNgay));
            grp_List.Text = $"DANH SÁCH XE TẬP LÁI  ·  {lst.Count} xe  ·  {dangDung} đang sử dụng  ·  {lst.Count - dangDung} đã thanh lý" +
                            (sapHetHan > 0 ? $"  ·  ⚠ {sapHetHan} GPXTL hết hạn / sắp hết hạn" : string.Empty);

            if (focusVehicleId.HasValue)
            {
                int index = lst.FindIndex(x => x.VehicleID == focusVehicleId.Value);
                if (index >= 0)
                    gridView_XeTapLai.FocusedRowHandle = gridView_XeTapLai.GetRowHandle(index);
            }
            ShowDetail(gridView_XeTapLai.GetFocusedRow() as XeTapLai);
        }

        #region Chi tiết

        private void gridView_XeTapLai_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ShowDetail(gridView_XeTapLai.GetFocusedRow() as XeTapLai);
        }

        private void ShowDetail(XeTapLai xe)
        {
            btn_Edit.Enabled = xe != null && !_isEditing;
            if (xe == null)
            {
                lbl_BienSo.Text = "Chọn một xe";
                lbl_HangXe.Text = " ";
                lbl_TrangThai.Text = " ";
                foreach (var label in new[] { val_MaXe, val_NamSX, val_SoGPXTL, val_NgayCap, val_NgayHH, val_SuDungDen })
                    label.Text = "—";
                txt_SoHuu.Text = string.Empty;
                toggleSwitch_TrangThai.IsOn = false;
                return;
            }

            _IdXeTapLai = xe.VehicleID;
            lbl_BienSo.Text = xe.BienSoXe;
            lbl_HangXe.Text = "Hạng " + xe.HangGPLX;
            if (xe.ThanhLy)
                UiTheme.SetStatusText(lbl_TrangThai, "Đã thanh lý", UiTheme.TextMuted);
            else
                UiTheme.SetStatusText(lbl_TrangThai, "Đang sử dụng", UiTheme.Success);

            val_MaXe.Text = xe.MaXe;
            val_NamSX.Text = xe.NamSX > 0 ? xe.NamSX.ToString() : "—";
            val_SoGPXTL.Text = string.IsNullOrWhiteSpace(xe.SoGPXTL) ? "—" : xe.SoGPXTL;
            val_NgayCap.Text = xe.NgayCapGPXTL == DateTime.MinValue ? "—" : xe.NgayCapGPXTL.ToString("dd/MM/yyyy");
            val_SuDungDen.Text = xe.SuDungDen?.ToString("dd/MM/yyyy") ?? "—";

            int conLai = (xe.NgayHHXTL.Date - DateTime.Today).Days;
            if (xe.NgayHHXTL == DateTime.MinValue)
            {
                val_NgayHH.Text = "Chưa có GPXTL";
                val_NgayHH.Appearance.ForeColor = UiTheme.Danger;
            }
            else if (conLai < 0)
            {
                val_NgayHH.Text = $"{xe.NgayHHXTL:dd/MM/yyyy}  (đã hết hạn)";
                val_NgayHH.Appearance.ForeColor = UiTheme.Danger;
            }
            else if (conLai <= SapHetHanSoNgay)
            {
                val_NgayHH.Text = $"{xe.NgayHHXTL:dd/MM/yyyy}  (còn {conLai} ngày)";
                val_NgayHH.Appearance.ForeColor = UiTheme.Warning;
            }
            else
            {
                val_NgayHH.Text = xe.NgayHHXTL.ToString("dd/MM/yyyy");
                val_NgayHH.Appearance.ForeColor = UiTheme.Success;
            }
            val_NgayHH.Appearance.Options.UseForeColor = true;

            txt_SoHuu.Text = xe.ChuSoHuu;
            toggleSwitch_TrangThai.IsOn = xe.ThanhLy;
        }

        private void grp_Detail_Paint(object sender, PaintEventArgs e)
        {
            UiTheme.DrawSection(e.Graphics, "THÔNG TIN ĐĂNG KÝ", 20, 114, 340);
            UiTheme.DrawSection(e.Graphics, "THÔNG TIN QUẢN LÝ", 20, 342, 340);
        }

        private void SetEditMode(bool isEdit)
        {
            _isEditing = isEdit;

            txt_SoHuu.Properties.ReadOnly = !isEdit;
            toggleSwitch_TrangThai.Properties.ReadOnly = !isEdit;
            btn_Refresh.Enabled = !isEdit;
            btn_Export.Enabled = !isEdit;

            btn_Edit.Enabled = !isEdit && gridView_XeTapLai.GetFocusedRow() is XeTapLai;
            btn_Save.Visible = isEdit;
            btn_Cancel.Visible = isEdit;
            btn_ImportXml.Enabled = !isEdit;

            if (isEdit)
                txt_SoHuu.Focus();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            ShowDetail(gridView_XeTapLai.GetFocusedRow() as XeTapLai);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!(gridView_XeTapLai.GetFocusedRow() is XeTapLai current)) return;

            if (toggleSwitch_TrangThai.IsOn && !current.ThanhLy &&
                XtraMessageBox.Show($"Xác nhận chuyển xe {current.BienSoXe} sang trạng thái ĐÃ THANH LÝ?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            XeTapLai xe = new XeTapLai
            {
                VehicleID = current.VehicleID,
                MaXe = current.MaXe,
                ThanhLy = toggleSwitch_TrangThai.IsOn,
                ChuSoHuu = txt_SoHuu.Text.Trim()
            };

            try
            {
                int rows = _DAO.UpdateXeDaoTao(xe);
                if (rows == 0)
                {
                    XtraMessageBox.Show($"Không tìm thấy xe {current.BienSoXe} trong CSDL (có thể đã bị xóa). Bấm Làm mới rồi thử lại.",
                        "Không lưu được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SetEditMode(false);
                LoadXeTapLai(current.VehicleID);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Không lưu được: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Lưới

        private void gridView_XeTapLai_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == ThanhLy && e.Value is bool thanhLy)
                e.DisplayText = thanhLy ? "Đã thanh lý" : "Đang sử dụng";
        }

        private void gridView_XeTapLai_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (!(gridView_XeTapLai.GetRow(e.RowHandle) is XeTapLai xe)) return;

            if (xe.ThanhLy)
            {
                e.Appearance.ForeColor = UiTheme.TextMuted;
                return;
            }

            if (e.Column == ThanhLy)
            {
                e.Appearance.ForeColor = UiTheme.Success;
            }
            else if (e.Column == NgayHHGPXTL)
            {
                int conLai = (xe.NgayHHXTL.Date - DateTime.Today).Days;
                if (conLai < 0)
                    e.Appearance.ForeColor = UiTheme.Danger;
                else if (conLai <= SapHetHanSoNgay)
                    e.Appearance.ForeColor = UiTheme.Warning;
                else
                    return;
                if (_boldFont == null)
                    _boldFont = new Font(gridView_XeTapLai.Appearance.Row.Font, FontStyle.Bold);
                e.Appearance.Font = _boldFont;
            }
        }

        private void gridView_XeTapLai_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            if (gridView_XeTapLai.RowCount > 0) return;
            UiTheme.DrawEmptyState(e, "Chưa có xe tập lái", "Bấm \"Đồng bộ xe\" để lấy danh sách xe từ hệ thống.");
        }

        #endregion

        #region Thanh công cụ

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadXeTapLai((gridView_XeTapLai.GetFocusedRow() as XeTapLai)?.VehicleID);
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"DanhSachXeTapLai_{DateTime.Now:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    gridView_XeTapLai.ExportToXlsx(dialog.FileName);
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Không xuất được file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Import file XML danh sách xe tập lái (DSXTL): đọc file (Helper), so sánh với CSDL (DAO, chỉ đọc),
        /// hỏi xác nhận rồi mới ghi. Quy tắc so sánh / cập nhật nằm ở XeTapLaiDAO.BuildImportPlan và ApplyImport.
        /// </summary>
        private async void btn_ImportXml_Click(object sender, EventArgs e)
        {
            string path;
            using (var dialog = new OpenFileDialog
            {
                Title = "Chọn file XML danh sách xe tập lái",
                Filter = "Danh sách xe tập lái (*.xml)|*.xml|Tất cả (*.*)|*.*"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                path = dialog.FileName;
            }

            string caption = btn_ImportXml.Text;
            btn_ImportXml.Enabled = false;
            btn_ImportXml.Text = "Đang đọc file...";
            try
            {
                List<XeTapLaiXml> ds = await Task.Run(() => _DAO.BuildImportPlan(XeTapLaiXmlReader.Read(path)));
                int them = ds.Count(x => x.HanhDong == ImportAction.ThemMoi);
                int sua = ds.Count(x => x.HanhDong == ImportAction.CapNhat);
                string tenFile = System.IO.Path.GetFileName(path);

                if (them + sua == 0)
                {
                    XtraMessageBox.Show($"{tenFile}: {ds.Count} xe, tất cả đã có và không có thay đổi.",
                        "Import Xml", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (XtraMessageBox.Show(XacNhanText(tenFile, ds, them, sua), "Xác nhận Import Xml",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                btn_ImportXml.Text = "Đang cập nhật...";
                var (soThem, soSua) = await Task.Run(() => _DAO.ApplyImport(ds));

                LoadXeTapLai((gridView_XeTapLai.GetFocusedRow() as XeTapLai)?.VehicleID);
                XtraMessageBox.Show($"Import xong: Thêm mới {soThem} xe, Cập nhật {soSua} xe, Giữ nguyên {ds.Count - them - sua} xe.",
                    "Import Xml", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorLog.Write("UcChild_XeDaoTao.ImportXml", ex);
                XtraMessageBox.Show("Import thất bại, chưa có thay đổi nào được ghi:\n" + ErrorLog.Unwrap(ex).Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                {
                    btn_ImportXml.Text = caption;
                    btn_ImportXml.Enabled = !_isEditing;
                }
            }
        }

        /// <summary>Nội dung hộp xác nhận: số xe theo từng loại và danh sách thay đổi (tối đa 12 dòng mỗi nhóm).</summary>
        private static string XacNhanText(string tenFile, List<XeTapLaiXml> ds, int them, int sua)
        {
            const int max = 12;
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"File {tenFile}: {ds.Count} xe");
            sb.AppendLine();
            sb.AppendLine($"•  Thêm mới: {them} xe");
            sb.AppendLine($"•  Cập nhật: {sua} xe");
            sb.AppendLine($"•  Giữ nguyên: {ds.Count - them - sua} xe");

            void Nhom(string tieuDe, ImportAction hanhDong, Func<XeTapLaiXml, string> dong)
            {
                var list = ds.Where(x => x.HanhDong == hanhDong).ToList();
                if (list.Count == 0) return;
                sb.AppendLine();
                sb.AppendLine(tieuDe + ":");
                foreach (var x in list.Take(max)) sb.AppendLine("   " + dong(x));
                if (list.Count > max) sb.AppendLine($"   ... và {list.Count - max} xe khác");
            }

            Nhom("Xe thêm mới", ImportAction.ThemMoi, x => $"{x.BienSoXe}  ·  hạng {x.HangDaoTao}  ·  GPXTL {x.SoGPXTL}");
            Nhom("Xe cập nhật", ImportAction.CapNhat, x => $"{x.BienSoXe}:  {string.Join(";  ", x.ThayDoi)}");

            sb.AppendLine();
            sb.Append("Tiếp tục cập nhật vào CSDL?");
            return sb.ToString();
        }

        #endregion
    }
}
