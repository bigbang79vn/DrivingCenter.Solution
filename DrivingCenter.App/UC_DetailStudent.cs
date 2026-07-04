using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using System;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class UC_DetailStudent : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
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
            pic_Image.Image = new ImageExtension().byteArrayToImage(_DAO.GetAnhChupNguoiLx(nguoiLx.NguoiLXID));

            lb_QuangDuongTL.Text = nguoiLx.TongQuangDuongTichLuy.ToString();
            lb_TimeTichLuy.Text = nguoiLx.TongThoiGianText;

            gridControl_PhienHocByNguoiLxId.DataSource = _DAO.GetResultNguoiLxByID(nguoiLx.NguoiLXID);
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
                try
                {
                    int focusedRowHandle = gridView_PhienHocByNguoiLxId.FocusedRowHandle;
                    var diemdanhnguoilxId = gridView_PhienHocByNguoiLxId.GetRowCellValue(focusedRowHandle, "DiemDanhNguoiLxID");

                    if (diemdanhnguoilxId == null)
                    {
                        throw new Exception("Giá trị DiemDanhNguoiLxID là null.");
                    }

                    frmDetailSession f = new frmDetailSession(diemdanhnguoilxId.ToString());
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else if (button.Caption == "Print")
            {
                var resultBC = _DAO.GetBaoCaoHocVienByNguoilxID(_NguoiLxid.NguoiLXID);
                new ReportPrintTool(new rptResultDAT { DataSource = resultBC }).ShowPreview();
            }
            else if (button.Caption == "Reset Changes")
            {
                GetDataSource(_NguoiLxid);
            }
            else if (button.Caption == "Delete")
            {
                int focusedRowHandle = gridView_PhienHocByNguoiLxId.FocusedRowHandle;
                var diemdanhnguoilxId = gridView_PhienHocByNguoiLxId.GetRowCellValue(focusedRowHandle, "DiemDanhNguoiLxID");

                if (XtraMessageBox.Show(string.Format("Bạn có chắc chắn xóa phiên học \n{0}", diemdanhnguoilxId), "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    _DAO.DeletePhByID(diemdanhnguoilxId.ToString());
                    gridControl_PhienHocByNguoiLxId.DataSource = _DAO.GetResultNguoiLxByID(_NguoiLxid.NguoiLXID);
                }
            }
        }
    }
}
