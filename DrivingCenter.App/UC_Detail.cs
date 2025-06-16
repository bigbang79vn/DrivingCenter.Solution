using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;

namespace DrivingCenter.App
{
    public partial class UC_Detail : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 _NguoiLxId;

        public UC_Detail(Int64 nguoilxId)
        {
            InitializeComponent();
            this._NguoiLxId = nguoilxId;
            LoadInfoHocVienById(nguoilxId);
            LoadBaiTapCabin();
        }

        private void LoadBaiTapCabin()
        {
            List<Exercise> lstBaiTap = CabinI2E_DAO.Instance.GetListBaiTap();

            lkDanhMucBaiTap.DataSource = lstBaiTap;
            lkDanhMucBaiTap.ValueMember = "ID";
            lkDanhMucBaiTap.DisplayMember = "TenBaiTap";
        }

        private void LoadInfoHocVienById(Int64? NguoiLxID)
        {
            HocVien hv = CabinI2E_DAO.Instance.GetInfoHVByNguoiLxID(NguoiLxID);

            if (hv != null)
            {
                //------Hiển thị thông tin cơ bản------
                lbl_MaDK.Text = hv.MaDK;
                lbl_HoTen.Text = hv.HoTenHV;
                DateTime ngaysinh = hv.NgaySinh;
                string NgaySinh = ngaysinh.ToString("dd/MM/yyyy");
                lbl_NgaySinh.Text = NgaySinh;

                ImageExtension img = new ImageExtension();
                var image = img.byteArrayToImage(hv.Anh);
                pictureBox1.Image = image;

                //------Hiển thị thông tin chi tiết------

                lb_MaDK.Text = hv.MaDK;
                lb_HoTen.Text = hv.HoTenHV;
                lb_NgaySinh.Text = NgaySinh;
                lb_GioiTinh.Text = hv.GioiTinh;
                lb_SoCMT.Text = hv.SoCMT;
                DateTime ngaycapCMT = hv.NgayCapCMT;
                string NgayCapCMT = ngaycapCMT.ToString("dd/MM/yyyy");
                lb_NgayCapCMT.Text = NgayCapCMT;
                lb_HangDT.Text = hv.HangGPLX;
                lb_GiaoVien.Text = hv.TenGV;
                lb_KhoaHoc.Text = hv.TenKH;
                pictureBox2.Image = image;

                lb_KetQua.Text = CabinI2E_DAO.Instance.CountBaiTap(_NguoiLxId).ToString() + " / " + hv.ToTalSecond;
                lb_KetLuan.Text = CabinI2E_DAO.Instance.CheckDK(hv.NguoiLxID, hv.MaKH);

                List<Result> lstKetQuaTap = CabinI2E_DAO.Instance.GetResultByNguoiLxID(NguoiLxID);

                grd_Result.DataSource = lstKetQuaTap;
            }
        }
        //-----------------------
        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Delete")
            {
                Delete();
            }
            else if (button.Caption == "Print")
            {
                Print();
            }
            else if (button.Caption == "Save")
            {
                if (gridView != null)
                {
                    foreach (var rowHandle in this.gridView.GetSelectedRows())
                    {
                        var IDBaiTap = (int)this.gridView.GetRowCellValue(rowHandle, "BaiHocCaBinID");
                        Int64 DiemdanhnguoiLxID = Convert.ToInt64(gridView.GetRowCellValue(rowHandle, "DiemDanhNguoiLxID"));

                        CabinI2E_DAO.Instance.UpdateBaiTap(IDBaiTap, DiemdanhnguoiLxID);

                        LoadInfoHocVienById(_NguoiLxId);
                    }
                }
            }
        }

        private void Delete()
        {
            if (gridView != null)
            {
                foreach (var rowHandle in this.gridView.GetSelectedRows())
                {
                    Int64 DiemdanhnguoiLxID = Convert.ToInt64(gridView.GetRowCellValue(rowHandle, "DiemDanhNguoiLxID"));

                    CabinI2E_DAO.Instance.DeleteResultExcer(DiemdanhnguoiLxID);

                    LoadInfoHocVienById(_NguoiLxId);
                }
            }
        }

        private void Print()
        {
            int? khoahocId = null;
            rptPrintResult_EMC f = new rptPrintResult_EMC(_NguoiLxId.ToString(), khoahocId, 1);
            f.Show();
        }
    }
}