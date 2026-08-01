using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Dtos.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_GiaoVien : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
        private readonly GPLX_CDB_CSDT_DAO _gplx = new GPLX_CDB_CSDT_DAO();
        public UcChild_GiaoVien()
        {
            InitializeComponent();
        }
        private void UcChild_GiaoVien_Load(object sender, EventArgs e)
        {

            Get_DmTrinhDoHocVan();
            Get_DmHangGPLX();
            Get_DmGiaoVienDAT();
            SetControlsEnabled(false);
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
        private void Get_DmGiaoVienDAT()
        {
            List<GiaoVien> data = _DAO.GetAllGV();
            gridControl_GiaoVien.DataSource = data;
            GridHelper.EnableRowNumbering(gridView_GiaoVien);
        }
        private void Get_DmTrinhDoHocVan()
        {
            DataTable dt = _DAO.GetlstHocVan();
            cbb_HocVan.Properties.DataSource = dt;
            cbb_HocVan.Properties.DisplayMember = "Ten";
            cbb_HocVan.Properties.ValueMember = "Ma";

            cbb_HocVan.Properties.NullText = "-- Chọn giá trị --"; // placeholder
        }
        private void Get_DmHangGPLX()
        {
            DataTable dt = _DAO.GetlstHangGPLX();

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

        private void groupControl_GiaoVien_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Import")
            {
                //---
            }
            if(e.Button.Properties.Caption == "Synchronize")
            {
                DialogResult dialogResult = MessageBox.Show("Đồng bộ lại giáo viên từ hệ thống GPLX?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Synchronize();
                    Get_DmGiaoVienDAT();
                    SetControlsEnabled(false);
                }
            }    
        }

        private void Synchronize()
        {
            var giaoViens = _gplx.GetAllGv();

            foreach (var item in giaoViens)
            {
                GiaoVienDto dto = new GiaoVienDto
                {
                    MaGV = item.MaGV,
                    MaCSDT = item.MaCSDT,
                    HoTenDem = item.HoTenDem ,
                    TenGV = item.TenGV,
                    NgaySinh = item.NgaySinh,
                    SoCMT = item.SoCMT,
                    HangGPLX = item.HangGPLX,
                    HangGPLXDuocDT = item.HangGPLXDuocDT,
                    SoDienThoai = item.SoDienThoai,
                    DiaChi = item.DiaChi,
                    MaCNDT = item.MaCNDT,
                    MaTrinhDoHocVan = item.MaTrinhDoHocVan,
                    MonGiangDay = item.MonGiangDay,
                    AnhChup = item.AnhChup
                };

                _DAO.AddGiaoVien(item);
            }
        }
    }
}
