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
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using Google.Protobuf.WellKnownTypes;

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
            Get_DmGiaoVien();
            Get_DmTrinhDoHocVan();
            Get_DmHangGPLX();
        }

        private void Get_DmGiaoVien()
        {
            List<GiaoVien> data = DAT_DAO.Instance.GetAllGV();
            gridControl_GiaoVien.DataSource = data;
        }

        private void Get_DmTrinhDoHocVan()
        {
            DataTable dt = DAT_DAO.Instance.GetlstHocVan();
            cbb_HocVan.Properties.DataSource = dt;
            cbb_HocVan.Properties.DisplayMember = "Ten";
            cbb_HocVan.Properties.ValueMember = "Ma";
        }
        private void Get_DmHangGPLX()
        {
            DataTable dt = DAT_DAO.Instance.GetlstHangGPLX();

            cbb_HangGPLX.Properties.DataSource = dt;
            cbb_HangGPLX.Properties.DisplayMember = "TenHang";
            cbb_HangGPLX.Properties.ValueMember = "MaHang";

            cbb_HangGPLX.Properties.NullText = "-- Chọn giá trị --"; // placeholder
        }

        private void gridView_GiaoVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GiaoVien gv = gridView_GiaoVien.GetFocusedRow() as GiaoVien;

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
    }
}
