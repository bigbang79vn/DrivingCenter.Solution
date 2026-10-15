using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Report;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_DaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
        public Uc_DaoTao()
        {
            InitializeComponent();

            xtraTabControl.SelectedTabPage = xtraTabPage_LstHocVien;

            LoadCategory();
            LoadXeTapLai();
            LoadGiaoVien();
        }

        int MonthId = 0;
        int KhoaHocId;
        object TenKhoaHoc;

        private void LoadCategory()
        {
            DataTable dt = _DAO.GetCategory();

            var filteredData = dt.AsEnumerable()
                .Where(row => row.Field<int>("Level") != 0)
                .OrderByDescending(row => row.Field<int>("MonthID"))
                .CopyToDataTable();

            treeList_Category.DataSource = filteredData;
            treeList_Category.ExpandToLevel(0);
        }

        public void LoadDanhSachHocVien(int KhoaHocID)
        {
            List<NguoiLxInDAT> lst = _DAO.GetAllNguoiLxByKhoaHocID(KhoaHocID);

            gridCtrl_DanhSachHocVien.DataSource = lst;
        }

        private void LoadXeTapLai()
        {
            List<XeTapLai> lst = _DAO.GetLstXeTapLai();
            repoGrid_XeTapLai.DataSource = lst;
            repoGrid_XeTapLai.DisplayMember = "BienSoXe";
            repoGrid_XeTapLai.ValueMember = "MaXe";

            repoGrid_XeTapLai.PopupView.Columns.AddField("BienSoXe").Visible = true;
            repoGrid_XeTapLai.PopupView.Columns.AddField("MaXe").Visible = true;
            repoGrid_XeTapLai.PopupView.Columns.AddField("HangGPLX").Visible = true;
        }

        private void LoadGiaoVien()
        {
            List<GiaoVien> lst = _DAO.GetAllGV();
            repoGrid_GiaoVien.DataSource = lst;
            repoGrid_GiaoVien.DisplayMember = "HoTenGV";
            repoGrid_GiaoVien.ValueMember = "GiaoVienID";

            repoGrid_GiaoVien.PopupView.Columns.AddField("HoTenGV").Visible = true;
            repoGrid_GiaoVien.PopupView.Columns.AddField("NgaySinh").Visible = true;
            repoGrid_GiaoVien.PopupView.Columns.AddField("HangGPLX").Visible = true;
        }

        private void treeList_Category_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.Level == 0) // Root Level
            {
                e.Appearance.Font = new Font("Roboto", 10, FontStyle.Regular);
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.FromArgb(78, 109, 156);
            }
            else if (e.Node.Level == 1) // Level 1
            {
                e.Appearance.Font = new Font("Roboto", 10, FontStyle.Regular);
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.FromArgb(222, 241, 255);
            }
            else if (e.Node.Level == 2) // Level 2
            {
                e.Appearance.Font = new Font("Roboto", 10, FontStyle.Regular);
                e.Appearance.BackColor = Color.White;
            }
        }

        private void MenuItem_BoTriXeSTD_Click(object sender, EventArgs e)
        {
            frmPhanCongDaoTao f = new frmPhanCongDaoTao(MonthId);
            f.Show();
        }

        private void gridView_DanhSachHocVien_DoubleClick(object sender, EventArgs e)
        {
            NguoiLxInDAT rowData = gridView_DanhSachHocVien.GetFocusedRow() as NguoiLxInDAT;

            UC_DetailStudent ucHV = new UC_DetailStudent(rowData);

            xtraTabPage_DetailHocVien.Controls.Clear();
            ucHV.Dock = DockStyle.Fill;
            xtraTabPage_DetailHocVien.Controls.Add(ucHV);

            // Chuyển sang TabPage chi tiết
            xtraTabControl.SelectedTabPage = xtraTabPage_DetailHocVien;
        }

        #region ContextMenuStrip
        private void mn_KetQuaQuaTrinhHoc_Click(object sender, EventArgs e)
        {
            DataTable resultBC = _DAO.BaoCaoKetQuaHocVien(KhoaHocId, 0);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;


            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }
        private void mn_KetQuaDATQuaTrinhHoc_Click(object sender, EventArgs e)
        {
            DataTable resultBC = _DAO.BaoCaoKetQuaHocVien(KhoaHocId, 1);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;

            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }
        private void mn_KetQuaKHONGDATQuaTrinhHoc_Click(object sender, EventArgs e)
        {
            DataTable resultBC = _DAO.BaoCaoKetQuaHocVien(KhoaHocId, 2);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;


            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }
        
        private void MenuItem_XuatBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void btn_ExportResultToExcel_Click(object sender, EventArgs e)
        {
            string fileName = $"Danh-Sach-{TenKhoaHoc}.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridCtrl_DanhSachHocVien.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            XtraMessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void treeList_Category_DoubleClick(object sender, EventArgs e)
        {
            var focusedNode = treeList_Category.FocusedNode;
            if (focusedNode != null)
            {
                TenKhoaHoc = treeList_Category.FocusedNode.GetValue("Name");

                object IDKhoaHoc = treeList_Category.FocusedNode.GetValue("KhoaHocID");

                LoadDanhSachHocVien((int)KhoaHocId);
                xtraTabControl.SelectedTabPage = xtraTabPage_LstHocVien;
            }
        }
        private void treeList_Category_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var focusedNode = treeList_Category.FocusedNode;
            if (focusedNode != null)
            {
                KhoaHocId = (int)focusedNode["MonthID"] / 10000;
                MonthId = (int)focusedNode["MonthID"];
            }
        }

        #endregion

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Load")
            {
                LoadCategory();
            }
        }
    }
}