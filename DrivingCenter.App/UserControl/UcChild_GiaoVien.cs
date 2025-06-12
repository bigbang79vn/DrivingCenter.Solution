using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_GiaoVien : DevExpress.XtraEditors.XtraUserControl
    {
        public UcChild_GiaoVien()
        {
            InitializeComponent();
        }
        private void UcChild_GiaoVien_Load(object sender, EventArgs e)
        {
            Get_DmTrinhDoHocVan();
            Get_DmHangGPLX();
            Get_DmGiaoVien();

            SetControlsEnabled(false);
            SetButtonPanelEnable(0,1);
        }

        private int _giaoVienID;
        private void SetControlsEnabled(bool enable)
        {
            txt_MaGV.Enabled = enable;
            txt_HoDem.Enabled = enable;
            txt_TenGV.Enabled = enable;
            txt_CCCD.Enabled = enable;
            cbb_HangGPLX.Enabled = enable;
            cbb_HocVan.Enabled = enable;
            txt_MaCNDT.Enabled = enable;
            date_NgaySinh.Enabled = enable;
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
        private void DisableAllButtons()
        {
            foreach (var obj in windowsUIButtonPanel.Buttons)
            {
                if (obj is DevExpress.XtraBars.Docking2010.WindowsUIButton button)
                {
                    button.Enabled = false;
                }
            }
        }
        private void Get_DmGiaoVien()
        {
            List<GiaoVien> data = AppBee_DAO.Instance.GetAllGV();
            gridControl_GiaoVien.DataSource = data;
            GridHelper.EnableRowNumbering(gridView_GiaoVien);
        }
        private void Get_DmTrinhDoHocVan()
        {
            DataTable dt = AppBee_DAO.Instance.GetlstHocVan();
            cbb_HocVan.Properties.DataSource = dt;
            cbb_HocVan.Properties.DisplayMember = "Ten";
            cbb_HocVan.Properties.ValueMember = "Ma";

            cbb_HocVan.Properties.NullText = "-- Chọn giá trị --"; // placeholder
        }
        private void Get_DmHangGPLX()
        {
            DataTable dt = AppBee_DAO.Instance.GetlstHangGPLX();

            cbb_HangGPLX.Properties.DataSource = dt;
            cbb_HangGPLX.Properties.DisplayMember = "TenHang";
            cbb_HangGPLX.Properties.ValueMember = "MaHang";

            cbb_HangGPLX.Properties.NullText = "-- Chọn giá trị --"; // placeholder
        }

        private void gridView_GiaoVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GiaoVien gv = gridView_GiaoVien.GetFocusedRow() as GiaoVien;

            _giaoVienID = gv.GiaoVienID;
            txt_MaGV.Text = gv.MaGV;
            txt_HoDem.Text = gv.HoTenDem;
            txt_TenGV.Text = gv.TenGV;
            txt_CCCD.Text = gv.SoCMT;
            cbb_HangGPLX.EditValue = gv.HangGPLX;
            cbb_HocVan.Text = gv.TrinhDoHocVan;
            txt_MaCNDT.Text = gv.MaCNDT;
            date_NgaySinh.DateTime = gv.NgaySinh;
            if (gv.AnhChup != null)
            {
                Image image = Image.FromStream(new System.IO.MemoryStream(gv.AnhChup));
                pictureBox_AnhChup.Image = image;
                pictureBox_AnhChup.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox_AnhChup.Image = null;
            }
            toggleSwitch_TrangThai.IsOn = gv.SuDung;
        }
        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Edit")
            {
                SetControlsEnabled(true);
                DisableAllButtons();
                SetButtonPanelEnable(2, 3);
            }
            else if (button.Caption == "Add")
            {
                SetControlsEnabled(true);
                DisableAllButtons();
                SetButtonPanelEnable(2, 3);

                txt_MaGV.Text = "";
                txt_HoDem.Text = "";
                txt_TenGV.Text = "";
                txt_CCCD.Text = "";
                cbb_HangGPLX.EditValue = null;
                cbb_HocVan.EditValue = null;
                txt_MaCNDT.Text = "";
                date_NgaySinh.EditValue = null;
                pictureBox_AnhChup.Image = null;
                toggleSwitch_TrangThai.IsOn = true;
                txt_MaGV.Focus();

            }
            else if (button.Caption == "Save")
            {
                GiaoVien gv = new GiaoVien();

                gv.MaGV = txt_MaGV.Text;
                gv.HoTenDem = txt_HoDem.Text;
                gv.TenGV = txt_TenGV.Text;
                gv.SoCMT = txt_CCCD.Text;
                gv.HangGPLX = cbb_HangGPLX.EditValue.ToString();
                gv.TrinhDoHocVan = cbb_HocVan.EditValue.ToString();
                gv.MaCNDT = txt_MaCNDT.Text;
                gv.NgaySinh = date_NgaySinh.DateTime;
                gv.SuDung = toggleSwitch_TrangThai.IsOn;
                AppBee_DAO.Instance.UpdateGiaoVien(gv, _giaoVienID);
                Get_DmGiaoVien();
                SetControlsEnabled(false);
                DisableAllButtons();
                SetButtonPanelEnable(0, 1);
            }
            else if (button.Caption == "Cancel")
            {
                SetControlsEnabled(false);
                DisableAllButtons();
                SetButtonPanelEnable(0, 1);
            }
        }

        private void mn_SynsGiaoVien_Click(object sender, EventArgs e)
        {
            int result = AppBee_DAO.Instance.SynsGiaoVien();
            //XtraMessageBox.Show("Đồng bộ thành công " + result + " bản ghi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
