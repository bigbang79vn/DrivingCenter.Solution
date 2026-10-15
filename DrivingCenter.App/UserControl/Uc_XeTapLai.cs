using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_XeTapLai : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
        public Uc_XeTapLai()
        {
            InitializeComponent();

            // Vô hiệu hóa nút Update, Undo Index = 1,2
            ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[1]).Enabled = false;
            ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[2]).Enabled = false;
        }

        private void Uc_XeTapLai_Load(object sender, EventArgs e)
        {
            LoadlstXeTapLai();
            GetLsVehicle();
        }

        private void GetLsVehicle()
        {
            List<XeTapLai> lst = _DAO.GetAllVehicles();
            GridHelper.EnableRowNumbering(bandedGridView);
            gridControl_XeTapLai.DataSource = lst;
        }

        private void LoadlstXeTapLai()
        {
            List<XeTapLai> lst = _DAO.GetLstXeTapLai();

            cbb_XeTapLai.Properties.DataSource = lst;
            cbb_XeTapLai.Properties.DisplayMember = "BienSoXe";
            cbb_XeTapLai.Properties.ValueMember = "MaXe";
        }

        private void SetEditMode(bool isEdit)
        {
            gridControl_XeTapLai.Enabled = !isEdit;

            cbb_XeTapLai.ReadOnly = !isEdit;
            tog_LoginCCCD.ReadOnly = !isEdit;
            tog_Status.ReadOnly = !isEdit;

            // Button
            ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[0]).Enabled = !isEdit; // Edit
            ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[1]).Enabled = isEdit;  // Update
            ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[2]).Enabled = isEdit;  // Undo

            if (isEdit)
                cbb_XeTapLai.Focus();
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Edit")
            {
                SetEditMode(true);  
            }
            else if (button.Caption == "Update")
            {
                Update();
                SetEditMode(false);
                GetLsVehicle();
            }
            else if (button.Caption == "Undo")
            {
                SetEditMode(false);
            }
            else if (button.Caption == "Export")
            {
                string fileName = $"XeTapLai.xlsx";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                gridControl_XeTapLai.ExportToXlsx(filePath, new XlsxExportOptionsEx()
                {
                    ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
                });

                var result = XtraMessageBox.Show("Xuất Excel thành công! Mở file?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            else if (button.Caption == "Delete")
            {
                var resul = XtraMessageBox.Show("Bạn có chắc chắn xóa thiết bị này không?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK;
                if (resul)
                {
                    Delete();
                    GetLsVehicle();
                }
            }
        }

        private void Update()
        {
            var vehicleId = bandedGridView.GetFocusedRowCellValue("VehicleID");
            var Imei = bandedGridView.GetFocusedRowCellValue("Imei");
            var selectedValue = cbb_XeTapLai.EditValue;
            bool status = tog_Status.IsOn ? true : false;
            bool loginCCCD = tog_LoginCCCD.IsOn ? true : false;
            XeTapLai xeTap = cbb_XeTapLai.Properties.GetRowByKeyValue(selectedValue) as XeTapLai;
            if (xeTap != null && vehicleId != null)
            {
                _DAO.UpdateXeTapLai((int)vehicleId, (string)Imei, xeTap, status, loginCCCD);
            }
        }

        private void Delete()
        {
            var vehicleId = bandedGridView.GetFocusedRowCellValue("VehicleID");
            var Imei = bandedGridView.GetFocusedRowCellValue("Imei");
            if (vehicleId != null)
            {
                _DAO.DeleteXeTapLai((int)vehicleId, (string)Imei);
            }
        }

        private void bandedGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            XeTapLai rowData = bandedGridView.GetFocusedRow() as XeTapLai;

            if (rowData != null)
            {
                txt_Imei.Text = rowData.Imei;
                cbb_XeTapLai.EditValue = rowData.MaXe;
                txt_HangXe.Text = rowData.HangGPLX;
                date_NgayCapNhat.Text = rowData.ThoiDiem.ToString("dd/MM/yyyy");
                txt_Version.Text = rowData.Version;
                tog_Status.IsOn = rowData.TrangThai;
                tog_Active.IsOn = rowData.Active;

                tog_LoginCCCD.IsOn = rowData.LoginCCCD;
            }
        }
    }
}