using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_ThietBiDAT : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly XeTapLaiDAO _xeTapLaiDao = new XeTapLaiDAO();
        private bool _isEditing;

        public UcChild_ThietBiDAT()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pnl_Header.Paint += UiTheme.PaintHeader;

            UiTheme.StylePrimary(btn_Export);
            UiTheme.StylePrimary(btn_Save);
            UiTheme.StyleDanger(btn_Delete);
            UiTheme.SetIcon(btn_Export, "svgimages/export/exporttoxlsx.svg");
            UiTheme.SetIcon(btn_Refresh, "svgimages/icon%20builder/actions_refresh.svg");
            UiTheme.SetIcon(btn_Edit, "svgimages/icon%20builder/actions_edit.svg", 16);
            UiTheme.SetIcon(btn_Save, "svgimages/save/save.svg", 16);
            UiTheme.SetIcon(btn_Cancel, "svgimages/icon%20builder/actions_undo.svg", 16);
            UiTheme.SetIcon(btn_Delete, "svgimages/icon%20builder/actions_trash.svg", 16);

            UiTheme.StyleGrid(bandedGridView, "Tìm theo IMEI, serial, biển số, mã xe...");
            bandedGridView.ColumnPanelRowHeight = 48;
            bandedGridView.Appearance.BandPanel.ForeColor = UiTheme.Brand;

            // Bỏ nền cam gán cứng của band cấu hình: chói trên skin tối, band header đã đủ phân nhóm
            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col in gridBand2.Columns)
                col.AppearanceCell.Options.UseBackColor = false;
            bandedGridView.RowCellStyle += bandedGridView_RowCellStyle;
            bandedGridView.CustomDrawEmptyForeground += bandedGridView_CustomDrawEmptyForeground;

            // Đang sửa thì giữ nguyên dòng đang chọn (không làm mờ cả lưới)
            bandedGridView.BeforeLeaveRow += (s, e) => { if (_isEditing) e.Allow = false; };
        }

        private void UcChild_ThietBiDAT_Load(object sender, EventArgs e)
        {
            LoadlstXeTapLai();
            GetLsVehicle();
            SetEditMode(false);
        }

        private void GetLsVehicle(int? focusVehicleId = null)
        {
            List<XeTapLai> lst = _xeTapLaiDao.GetAllVehicles();
            gridControl_XeTapLai.DataSource = lst;
            GridHelper.EnableRowNumbering(bandedGridView);

            // DAO nạp cột Vehicles.Active vào thuộc tính TrangThai
            int active = lst.Count(x => x.TrangThai);
            int chuaGan = lst.Count(x => string.IsNullOrWhiteSpace(x.MaXe) || x.MaXe == "Null");
            grp_List.Text = $"DANH SÁCH THIẾT BỊ  ·  {lst.Count} thiết bị  ·  {active} đang bật  ·  {lst.Count - active} đang tắt" +
                            (chuaGan > 0 ? $"  ·  {chuaGan} chưa gán xe" : string.Empty);

            if (focusVehicleId.HasValue)
            {
                int index = lst.FindIndex(x => x.VehicleID == focusVehicleId.Value);
                if (index >= 0)
                    bandedGridView.FocusedRowHandle = bandedGridView.GetRowHandle(index);
            }
            ShowDetail(bandedGridView.GetFocusedRow() as XeTapLai);
        }

        private void LoadlstXeTapLai()
        {
            List<XeTapLai> lst = _xeTapLaiDao.GetLstXeTapLai();

            cbb_XeTapLai.Properties.DataSource = lst;
            cbb_XeTapLai.Properties.DisplayMember = "BienSoXe";
            cbb_XeTapLai.Properties.ValueMember = "MaXe";
        }

        #region Chi tiết

        private void bandedGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ShowDetail(bandedGridView.GetFocusedRow() as XeTapLai);
        }

        private void ShowDetail(XeTapLai rowData)
        {
            btn_Edit.Enabled = rowData != null && !_isEditing;
            btn_Delete.Enabled = rowData != null && !_isEditing;

            if (rowData == null)
            {
                lbl_Imei.Text = "Chọn một thiết bị";
                lbl_Active.Text = " ";
                foreach (var label in new[] { val_Serial, val_Version, val_HangXe, val_NgayCapNhat })
                    label.Text = "—";
                cbb_XeTapLai.EditValue = null;
                tog_Status.IsOn = false;
                tog_LoginCCCD.IsOn = false;
                return;
            }

            lbl_Imei.Text = rowData.Imei;
            if (rowData.TrangThai)
                UiTheme.SetStatusText(lbl_Active, "Đang bật", UiTheme.Success);
            else
                UiTheme.SetStatusText(lbl_Active, "Đang tắt", UiTheme.TextMuted);

            val_Serial.Text = ValueOrDash(rowData.SerialNumber);
            val_Version.Text = ValueOrDash(rowData.Version);
            val_HangXe.Text = ValueOrDash(rowData.HangGPLX);
            val_NgayCapNhat.Text = rowData.ThoiDiem == DateTime.MinValue ? "—" : rowData.ThoiDiem.ToString("HH:mm dd/MM/yyyy");

            cbb_XeTapLai.EditValue = rowData.MaXe;
            tog_Status.IsOn = rowData.TrangThai;
            tog_LoginCCCD.IsOn = rowData.LoginCCCD;
        }

        private static string ValueOrDash(string value) =>
            string.IsNullOrWhiteSpace(value) || value == "Null" ? "—" : value;

        private void grp_Detail_Paint(object sender, PaintEventArgs e)
        {
            UiTheme.DrawSection(e.Graphics, "THÔNG TIN THIẾT BỊ", 20, 104, 340);
            UiTheme.DrawSection(e.Graphics, "GÁN XE & CẤU HÌNH", 20, 260, 340);
            UiTheme.DrawSection(e.Graphics, "THAO TÁC KHÁC", 20, 460, 340);
        }

        private void SetEditMode(bool isEdit)
        {
            _isEditing = isEdit;
            btn_Refresh.Enabled = !isEdit;
            btn_Export.Enabled = !isEdit;

            cbb_XeTapLai.Properties.ReadOnly = !isEdit;
            tog_LoginCCCD.Properties.ReadOnly = !isEdit;
            tog_Status.Properties.ReadOnly = !isEdit;

            bool hasRow = bandedGridView.GetFocusedRow() is XeTapLai;
            btn_Edit.Enabled = !isEdit && hasRow;
            btn_Delete.Enabled = !isEdit && hasRow;
            btn_Save.Visible = isEdit;
            btn_Cancel.Visible = isEdit;

            if (isEdit)
                cbb_XeTapLai.Focus();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            ShowDetail(bandedGridView.GetFocusedRow() as XeTapLai);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            var vehicleId = bandedGridView.GetFocusedRowCellValue("VehicleID");
            var imei = bandedGridView.GetFocusedRowCellValue("Imei");
            XeTapLai xeTap = cbb_XeTapLai.Properties.GetRowByKeyValue(cbb_XeTapLai.EditValue) as XeTapLai;

            if (xeTap == null)
            {
                XtraMessageBox.Show("Vui lòng chọn xe tập lái để gán cho thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbb_XeTapLai.Focus();
                return;
            }
            if (vehicleId == null) return;

            try
            {
                _xeTapLaiDao.UpdateXeTapLai((int)vehicleId, (string)imei, xeTap, tog_Status.IsOn, tog_LoginCCCD.IsOn);
                SetEditMode(false);
                GetLsVehicle((int)vehicleId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Không lưu được: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            var vehicleId = bandedGridView.GetFocusedRowCellValue("VehicleID");
            var imei = bandedGridView.GetFocusedRowCellValue("Imei");
            if (vehicleId == null) return;

            if (XtraMessageBox.Show($"Xóa thiết bị IMEI {imei}?\n\nThao tác này không thể hoàn tác.", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                _xeTapLaiDao.DeleteXeTapLai((int)vehicleId, (string)imei);
                GetLsVehicle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Không xóa được: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Lưới & thanh công cụ

        private void bandedGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (bandedGridView.GetRow(e.RowHandle) is XeTapLai row && !row.TrangThai)
                e.Appearance.ForeColor = UiTheme.TextMuted;
        }

        private void bandedGridView_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            if (bandedGridView.RowCount > 0) return;
            UiTheme.DrawEmptyState(e, "Chưa có thiết bị", "Thiết bị DAT sẽ xuất hiện khi được kết nối vào hệ thống.");
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadlstXeTapLai();
            GetLsVehicle((bandedGridView.GetFocusedRow() as XeTapLai)?.VehicleID);
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"ThietBiDAT_{DateTime.Now:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    gridControl_XeTapLai.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx()
                    {
                        ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng band
                    });

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

        #endregion
    }
}
