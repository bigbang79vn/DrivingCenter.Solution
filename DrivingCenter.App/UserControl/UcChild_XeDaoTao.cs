using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DAO;
using NPOI.SS.Formula.Functions;

namespace DrivingCenter.App.UserControl
{
	public partial class UcChild_XeDaoTao: DevExpress.XtraEditors.XtraUserControl
	{
        public UcChild_XeDaoTao()
		{
            InitializeComponent();
		}

        private void UcChild_XeTap_Load(object sender, EventArgs e)
        {
            Get_DmHangXe();
            SetControlsEnabled(false);
            SetButtonPanelEnable(0,1);
            LoadXeTapLai();
        }

        private void SetControlsEnabled(bool enable)
        {
            txt_BienSoXe.Enabled = enable;
            cbb_HangXe.Enabled = enable;
            txt_SoDK.Enabled = enable;
            txt_NamSX.Enabled = enable;
            txt_SoGPXTL.Enabled = enable;
            txt_CQCapGPXTL.Enabled = enable;
            date_NgayCapGPXTL.Enabled = enable;
            date_NgayHHGPXTL.Enabled = enable;
            txt_SoHuu.Enabled = enable;
            toggleSwitch_TrangThai.Enabled = enable;
        }
        private void SetButtonPanelEnable(params int[] enabledIndexes)
        {
            int totalButtons = windowsUIButtonPanel.Buttons.Count;

            for (int i = 0; i < totalButtons; i++)
            {
                var button = windowsUIButtonPanel.Buttons[i] as DevExpress.XtraBars.Docking2010.WindowsUIButton;

                if (button != null)
                {
                    button.Enabled = enabledIndexes.Contains(i);
                }
            }
        }
        private void Get_DmHangXe()
        {
            DataTable dt = AppBee_DAO.Instance.GetlstHangGPLX();
            cbb_HangXe.Properties.DataSource = dt;
            cbb_HangXe.Properties.DisplayMember = "TenHang";
            cbb_HangXe.Properties.ValueMember = "MaHang";

            cbb_HangXe.Properties.NullText = "-- Chọn giá trị --"; // placeholder
        }
        private void LoadXeTapLai()
        {
            List<XeTapLai> lst = AppBee_DAO.Instance.GetAllXeTapLai();
            gridControl_XeTapLai.DataSource = lst;
        }
        private void gridView_XeTapLai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var focusedRow = gridView_XeTapLai.GetFocusedRow() as XeTapLai;
            txt_BienSoXe.Text = focusedRow.BienSoXe;
            cbb_HangXe.Text = focusedRow.HangGPLX;
            txt_NamSX.Text = focusedRow.NamSX.ToString();
            txt_SoGPXTL.Text = focusedRow.SoGPXTL;
            date_NgayCapGPXTL.EditValue = focusedRow.NgayCapGPXTL;
            date_NgayHHGPXTL.EditValue = focusedRow.NgayHHXTL;
            toggleSwitch_TrangThai.IsOn = focusedRow.ThanhLy == true ? true : false;
        }
        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Edit")
            {
                SetControlsEnabled(true);
                SetButtonPanelEnable(2, 3);
            }
            else if (button.Caption == "Add")
            {
                SetControlsEnabled(true);
                SetButtonPanelEnable(2, 3);

                txt_BienSoXe.Text = "";
                cbb_HangXe.EditValue = null;
                txt_SoDK.Text = "";
                txt_NamSX.Text = "";
                txt_SoGPXTL.Text = "";
                txt_CQCapGPXTL.Text = "";
                date_NgayCapGPXTL.EditValue = DateTime.Today;
                date_NgayHHGPXTL.EditValue = DateTime.Today;
                txt_SoHuu.Text = "";
                toggleSwitch_TrangThai.IsOn = true;
            }

            else if (button.Caption == "Save")
            {
                XeTapLai xe = new XeTapLai();
                xe.BienSoXe = txt_BienSoXe.Text;
                xe.HangGPLX = cbb_HangXe.Text;
                xe.NamSX = int.Parse(txt_NamSX.Text);
                xe.SoGPXTL = txt_SoGPXTL.Text;
                xe.NgayCapGPXTL = date_NgayCapGPXTL.DateTime;
                xe.NgayHHXTL = date_NgayHHGPXTL.DateTime;
                xe.ThanhLy = toggleSwitch_TrangThai.IsOn;
                //DAT_DAO.Instance.UpdateXeTapLai(xe);
                LoadXeTapLai();
            }
            else if (button.Caption == "Cancel")
            {
                SetControlsEnabled(false);
                SetButtonPanelEnable(0, 1);
            }
        }
    }
}
