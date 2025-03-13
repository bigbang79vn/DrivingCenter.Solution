using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_Print : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_Print()
        {
            InitializeComponent();
        }

        private void Uc_Print_Load(object sender, EventArgs e)
        {
            GetAllKhoaHoc();
        }

        private void GetAllKhoaHoc()
        {
            List<KhoaHoc> lst = new List<KhoaHoc>();
            lst = DAT_DAO.Instance.GetAllKhoaHoc();
            searchLookUpEdit_KhoaHoc.Properties.DataSource = lst;
            searchLookUpEdit_KhoaHoc.Properties.DisplayMember = "TenKhoaHoc";
            searchLookUpEdit_KhoaHoc.Properties.ValueMember = "KhoaHocID";
            if (lst.Count > 0)
            {
                searchLookUpEdit_KhoaHoc.EditValue = lst[0].KhoaHocID;
            }
        }

        private void LoadlstHocVienbyKhoaHoc(int idkhoahoc)
        {
            List<HocVien> lstHocVien = DAT_DAO.Instance.GetlstHocVienByKhoaHoc(idkhoahoc);
            grd_lstHocVien.DataSource = lstHocVien;
        }

        private void searchLookUpEdit_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            var selectedObject = searchLookUpEdit_KhoaHoc.GetSelectedDataRow() as KhoaHoc;

            var IdKhoaHoc = searchLookUpEdit_KhoaHoc.EditValue;

            if (IdKhoaHoc == null)
            {
                MessageBox.Show("Chưa chọn khóa học");
            }
            else
            {
                lb_lstHvbyKhoaHoc.Text = "DANH SÁCH HỌC VIÊN ";
                lb_lstHvbyKhoaHoc.Text = lb_lstHvbyKhoaHoc.Text + selectedObject.TenKhoaHoc;
                LoadlstHocVienbyKhoaHoc((int)IdKhoaHoc);
            }
        }

        private void btn_InPhieuTheoDoi_Click(object sender, EventArgs e)
        {
            string lstNguoiLxID = "";

            // Lấy các chỉ số của các hàng được chọn
            int[] selectedRowHandles = gridView_lstHocVien.GetSelectedRows();

            foreach (int rowHandle in selectedRowHandles)
            {
                var cellValue = gridView_lstHocVien.GetRowCellValue(rowHandle, "NguoiLxID");

                lstNguoiLxID = lstNguoiLxID + "," + cellValue.ToString();
            }

            DataTable resultBC = IMG_DAO.Instance.GetDataPhieuTheoDoi(lstNguoiLxID);

            // Gán DataSource cho Report
            rptPhieuTheoDoi rpt = new rptPhieuTheoDoi();
            rpt.DataSource = resultBC;

            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        private void btn_PrintlstHV_Click(object sender, EventArgs e)
        {
            string lstNguoiLxID = "";

            // Lấy các chỉ số của các hàng được chọn
            int[] selectedRowHandles = gridView_lstHocVien.GetSelectedRows();

            foreach (int rowHandle in selectedRowHandles)
            {
                var cellValue = gridView_lstHocVien.GetRowCellValue(rowHandle, "NguoiLxID");

                lstNguoiLxID = lstNguoiLxID + "," + cellValue.ToString();
            }

            DataTable resultBC = IMG_DAO.Instance.GetDataTheHocVien(lstNguoiLxID);

            // Gán DataSource cho Report
            rptTheHocVien rpt = new rptTheHocVien();
            rpt.DataSource = resultBC;

            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        private void btn_InTemTheDAT_Click(object sender, EventArgs e)
        {
            string lstNguoiLxID = "";
            int temp = 3;

            // Lấy các chỉ số của các hàng được chọn
            int[] selectedRowHandles = gridView_lstHocVien.GetSelectedRows();

            foreach (int rowHandle in selectedRowHandles)
            {
                var cellValue = gridView_lstHocVien.GetRowCellValue(rowHandle, "NguoiLxID");

                lstNguoiLxID = lstNguoiLxID + "," + cellValue.ToString();
            }

            rptPrint f = new rptPrint(lstNguoiLxID, temp);
            f.Show();
        }
    }
}