using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class UC_DetailStudent : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly Int64 _NguoiLxid;
        public UC_DetailStudent(Int64 NguoiLxId)
        {
            InitializeComponent();
            this._NguoiLxid = NguoiLxId;

            GetDataSource(_NguoiLxid);
        }

        private void GetDataSource(Int64 nguoiLxId)
        {
            NguoiLxInDAT nguoiLx = DAT_DAO.Instance.GetInfoNguoiLxByID(nguoiLxId);
            lb_MaDK.Text = nguoiLx.MaDK;
            lb_HoTen.Text = nguoiLx.HoVaTen;
            lb_HangDT.Text = nguoiLx.HangDT;
            lb_NgaySinh.Text = nguoiLx.NgaySinh.ToString("dd/MM/yyyy");
            lb_SoCMT.Text = nguoiLx.CCCD;
            lb_GioiTinh.Text = nguoiLx.GioiTinh.ToString();
            lb_KhoaHoc.Text = nguoiLx.TenKH;
            pic_Image.Image = new ImageExtension().byteArrayToImage(nguoiLx.Anh);

            lb_QuangDuongTL.Text = nguoiLx.TongQuangDuongTichLuy.ToString();
            lb_TimeTichLuy.Text = nguoiLx.TongThoiGianText;

        }
    }
}
