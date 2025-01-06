using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_XeTapLai : DevExpress.XtraEditors.XtraUserControl
    {
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
            List<XeTapLai> lst = DAT_DAO.Instance.GetAllVehicles();
            int stt = 0;
            foreach (XeTapLai item in lst)
            {
                item.STT = stt + 1;
                stt++;
            }
            gridControl_XeTapLai.DataSource = lst;
        }

        private void LoadlstXeTapLai()
        {
            List<XeTapLai> lst = DAT_DAO.Instance.GetLstXeTapLai();

            cbb_XeTapLai.Properties.DataSource = lst;
            cbb_XeTapLai.Properties.DisplayMember = "BienSoXe";
            cbb_XeTapLai.Properties.ValueMember = "MaXe";
        }

        private void gridView_XeTapLai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            XeTapLai rowData = gridView_XeTapLai.GetFocusedRow() as XeTapLai;

            if (rowData != null)
            {
                txt_Imei.Text = rowData.Imei;
                cbb_XeTapLai.EditValue = rowData.MaXe;
                txt_HangXe.Text = rowData.HangGPLX;
                date_NgayCapNhat.Text = rowData.ThoiDiem.ToString("dd/MM/yyyy");
                txt_Version.Text = rowData.Version;
                check_Status.EditValue = rowData.TrangThai;
            }
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Edit")
            {
                gridControl_XeTapLai.Enabled = false;
                cbb_XeTapLai.ReadOnly = false;
                cbb_XeTapLai.Focus();
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[0]).Enabled = false;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[1]).Enabled = true;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[2]).Enabled = true;
            }
            else if (button.Caption == "Update")
            {
                Update();
                gridControl_XeTapLai.Enabled = true;
                cbb_XeTapLai.ReadOnly = true;
                GetLsVehicle();
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[0]).Enabled = true;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[1]).Enabled = false;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[2]).Enabled = false;
            }
            else if (button.Caption == "Undo")
            {
                gridControl_XeTapLai.Enabled = true;
                cbb_XeTapLai.ReadOnly = true;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[0]).Enabled = true;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[1]).Enabled = false;
                ((DevExpress.XtraBars.Docking2010.WindowsUIButton)windowsUIButtonPanel.Buttons[2]).Enabled = false;
            }
            else if (button.Caption == "Export")
            {
                List<XeTapLai> lst = DAT_DAO.Instance.GetAllVehicles();
                ConvertListToDataTable convert = new ConvertListToDataTable();
                DataTable dt = convert.ToDataTable(lst);
                ExportExcel export = new ExportExcel();
                export.ExportToExcel(dt, "Kết Quả");
            }
            else if (button.Caption == "Delete")
            {
                var resul = MessageBox.Show("Bạn có chắc chắn xóa thiết bị này không?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK;
                if (resul)
                {
                    Delete();
                    GetLsVehicle();
                }
            }
        }

        private new void Update()
        {
            var vehicleId = gridView_XeTapLai.GetFocusedRowCellValue("VehicleID");
            var Imei = gridView_XeTapLai.GetFocusedRowCellValue("Imei");
            var selectedValue = cbb_XeTapLai.EditValue;
            XeTapLai xeTap = cbb_XeTapLai.Properties.GetRowByKeyValue(selectedValue) as XeTapLai;
            if (xeTap != null && vehicleId != null)
            {
                DAT_DAO.Instance.UpdateXeTapLai((int)vehicleId, (string)Imei, xeTap);
            }
        }

        private void Delete()
        {
            var vehicleId = gridView_XeTapLai.GetFocusedRowCellValue("VehicleID");
            var Imei = gridView_XeTapLai.GetFocusedRowCellValue("Imei");
            if (vehicleId != null)
            {
                DAT_DAO.Instance.DeleteXeTapLai((int)vehicleId, (string)Imei);
            }
        }
    }
}