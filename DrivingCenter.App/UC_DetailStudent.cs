using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
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
        private readonly NguoiLxInDAT _NguoiLxid;
        public UC_DetailStudent(NguoiLxInDAT NguoiLxId)
        {
            InitializeComponent();
            this._NguoiLxid = NguoiLxId;

            GetDataSource(_NguoiLxid);
        }

        private void GetDataSource(NguoiLxInDAT nguoiLx)
        {
            lb_MaDK.Text = nguoiLx.MaDK;
            lb_HoTen.Text = nguoiLx.HoVaTen;
            lb_HangDT.Text = nguoiLx.HangDT;
            lb_NgaySinh.Text = nguoiLx.NgaySinh.ToString("dd/MM/yyyy");
            lb_SoCMT.Text = nguoiLx.CCCD;
            lb_NgayCapCMT.Text = nguoiLx.NgayCapCMT.ToString("dd/MM/yyyy");
            lb_GioiTinh.Text = nguoiLx.GioiTinh.ToString();
            lb_KhoaHoc.Text = nguoiLx.TenKH;
            pic_Image.Image = new ImageExtension().byteArrayToImage(DAT_DAO.Instance.GetAnhChupNguoiLx(nguoiLx.NguoiLXID));

            lb_QuangDuongTL.Text = nguoiLx.TongQuangDuongTichLuy.ToString();
            lb_TimeTichLuy.Text = nguoiLx.TongThoiGianText;

            gridControl_PhienHocByNguoiLxId.DataSource = DAT_DAO.Instance.GetResultNguoiLxByID(nguoiLx.NguoiLXID);
        }

        private void gridView_PhienHocByNguoiLxId_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Detail")
            {
                int focusedRowHandle = gridView_PhienHocByNguoiLxId.FocusedRowHandle;
                var diemdanhnguoilxId = gridView_PhienHocByNguoiLxId.GetRowCellValue(focusedRowHandle, "DiemDanhNguoiLxID");

                frmDetailSession f = new frmDetailSession(diemdanhnguoilxId.ToString());
                f.ShowDialog();
            }
            else if (button.Caption == "Print")
            {
                var resultBC = DAT_DAO.Instance.GetBaoCaoHocVienByNguoilxID(_NguoiLxid.NguoiLXID);
                new ReportPrintTool(new rptResultDAT { DataSource = resultBC }).ShowPreview();
            }
            else if (button.Caption == "Reset Changes")
            {
                XtraMessageBox.Show("Reset Changes", "Alert", MessageBoxButtons.OK);
            }
            else if (button.Caption == "Delete")
            {
                int focusedRowHandle = gridView_PhienHocByNguoiLxId.FocusedRowHandle;
                var diemdanhnguoilxId = gridView_PhienHocByNguoiLxId.GetRowCellValue(focusedRowHandle, "DiemDanhNguoiLxID");

                if (XtraMessageBox.Show(string.Format("Bạn có chắc chắn xóa phiên học \n{0}", diemdanhnguoilxId), "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    DAT_DAO.Instance.DeletePhByID(diemdanhnguoilxId.ToString());
                    gridControl_PhienHocByNguoiLxId.DataSource = DAT_DAO.Instance.GetResultNguoiLxByID(_NguoiLxid.NguoiLXID);
                }
            }
        }
    }
}
