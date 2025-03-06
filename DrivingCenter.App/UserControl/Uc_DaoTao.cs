using DevExpress.Export;
using DevExpress.Map.Kml.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Filtering.Templates;
using DevExpress.XtraPrinting;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.DTO.LyThuyet;
using DrivingCenter.App.Helper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_DaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_DaoTao()
        {
            InitializeComponent();

            tblLayout_ImportData.Dock = DockStyle.Fill;

            LoadCategory();
            LoadXeTapLai();
            LoadGiaoVien();
        }

        private bool isCollapsed;
        int MonthId = 0;
        int KhoaHocId;
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        #region Lịch học

        private void InsertLichHoc()
        {
            DAT_DAO.Instance.InsertLichHoc();
        }

        private void LoadCategory()
        {
            DataTable dt = DAT_DAO.Instance.GetCategory();

            treeList_Category.DataSource = dt;
            //treeList_Category.ExpandAll();
            treeList_Category.ExpandToLevel(1);
        }


        public void LoadDanhSachHocVien(int KhoaHocID)
        {
            List<NguoiLxInDAT> lst = DAT_DAO.Instance.GetAllNguoiLxByKhoaHocID(KhoaHocID);

            gridCtrl_DanhSachHocVien.DataSource = lst;
        }

        private void LoadXeTapLai()
        {
            List<XeTapLai> lst = DAT_DAO.Instance.GetLstXeTapLai();
            repoGrid_XeTapLai.DataSource = lst;
            repoGrid_XeTapLai.DisplayMember = "BienSoXe";
            repoGrid_XeTapLai.ValueMember = "MaXe";

            repoGrid_XeTapLai.PopupView.Columns.AddField("BienSoXe").Visible = true;
            repoGrid_XeTapLai.PopupView.Columns.AddField("MaXe").Visible = true;
            repoGrid_XeTapLai.PopupView.Columns.AddField("HangGPLX").Visible = true;
        }

        private void LoadGiaoVien()
        {
            List<GiaoVien> lst = DAT_DAO.Instance.GetAllGV();
            repoGrid_GiaoVien.DataSource = lst;
            repoGrid_GiaoVien.DisplayMember = "HoTenGV";
            repoGrid_GiaoVien.ValueMember = "GiaoVienID";

            repoGrid_GiaoVien.PopupView.Columns.AddField("HoTenGV").Visible = true;
            repoGrid_GiaoVien.PopupView.Columns.AddField("NgaySinh").Visible = true;
            repoGrid_GiaoVien.PopupView.Columns.AddField("HangGPLX").Visible = true;
        }

        private void treeList_Category_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var focusedNode = e.Node;
            if (focusedNode != null)
            {
                KhoaHocId = (int)focusedNode["MonthID"] / 10000;
                MonthId = (int)focusedNode["MonthID"];
                //if(treeList_Category.FocusedNode.GetValue("Name"))
                object tenkhoa = treeList_Category.FocusedNode.GetValue("Name");

                object IDKhoaHoc = treeList_Category.FocusedNode.GetValue("KhoaHocID");
                if (IDKhoaHoc != null)
                {
                    lb_KetquaKh.Text = "KHÓA HỌC " + tenkhoa.ToString();
                }
                else
                {
                    lb_KetquaKh.Text = tenkhoa.ToString();
                }
                LoadDanhSachHocVien((int)KhoaHocId);
            }
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
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.FromArgb(78, 109, 156);
            }
            else if (e.Node.Level == 2) // Level 2
            {
                e.Appearance.Font = new Font("Roboto", 10, FontStyle.Regular);
                e.Appearance.BackColor = Color.FromArgb(222, 241, 255);
            }
        }

        private void MenuItem_BoTriXeSTD_Click(object sender, EventArgs e)
        {
            frmPhanCongDaoTao f = new frmPhanCongDaoTao(MonthId);
            f.Show();
        }

        private void gridView_DanhSachHocVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            NguoiLxInDAT rowData = gridView_DanhSachHocVien.GetFocusedRow() as NguoiLxInDAT;
            //DAT_DAO.Instance.UpdateXeTapB11(rowData);
        }


        #endregion

        #region Phân công đào tạo

        public DataTableCollection tableCollection;
        private void btn_ReadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;

                    //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            cb_SheetData.Items.Clear();

                            MessageBox.Show("Chọn Sheet chứa thông tin học viên cần phân công", "Thông báo", MessageBoxButtons.OK);

                            foreach (DataTable table in tableCollection)
                            {
                                cb_SheetData.Items.Add(table.TableName);
                            }
                        }
                    }
                }
            }
        }

        private void cb_SheetData_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[cb_SheetData.SelectedItem.ToString()];
            grdCtrl_ImportData.DataSource = dt;
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            if (cb_SheetData.SelectedItem == null)
            {
                MessageBox.Show(string.Format("Bạn chưa chọn Sheet chứa Data!"));
                cb_SheetData.Focus();
            }
            else
            {
                DataTable dt = tableCollection[cb_SheetData.SelectedItem.ToString()];
                List<Assignment> lst = new List<Assignment>();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Assignment asg = new Assignment();
                        asg.GiaoVien = dt.Rows[i]["GiaoVien"].ToString().Trim();
                        asg.TenGV = asg.GiaoVien.Substring(asg.GiaoVien.Trim().LastIndexOf(' ') + 1);
                        int lastIndex = asg.GiaoVien.LastIndexOf(' ');
                        asg.HoTenDem = asg.GiaoVien.Trim().Substring(0, lastIndex);
                        asg.MaDK = dt.Rows[i]["MaDK"].ToString().Trim();
                        asg.BienSoXe = dt.Rows[i]["BienSoXe"].ToString().Trim();
                        asg.MaXeTap = asg.BienSoXe.Replace(" ", "").Replace(".", "").Replace("-", "");

                        lst.Add(asg);
                    }
                }
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Phân công đào tạo dựa vào danh sách?", "Thông Báo", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (var item in lst)
                        {
                            DAT_DAO.Instance.Assignment(item);
                        }
                        MessageBox.Show(string.Format("Hoàn thành phân công đào tạo khóa {0}", cb_SheetData.SelectedIndex.ToString()), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        #endregion

        private void gridView_DanhSachHocVien_DoubleClick(object sender, EventArgs e)
        {
            NguoiLxInDAT rowData = gridView_DanhSachHocVien.GetFocusedRow() as NguoiLxInDAT;
            MessageBox.Show(rowData.NguoiLXID.ToString(), "Thông Báo");
        }

        private void MenuItem_CheckLyThuyet_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;

                    //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;

                            foreach (DataTable table in tableCollection)
                            {
                                DataTable dt = tableCollection["Worksheet"];
                                frmKiemTraLyThuyet f = new frmKiemTraLyThuyet(dt, KhoaHocId);
                                f.Show();
                            }
                        }
                    }
                }
            }
        }

        private void MenuItem_PhanCong_Click(object sender, EventArgs e)
        {
            InsertLichHoc();
        }

        private void btn_LoadKhoaHoc_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btn_Extend_Click(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                PanelLeft.Width = PanelLeft.Width + 234;
                isCollapsed = false;
                this.Refresh();
            }
            else
            {
                PanelLeft.Width = PanelLeft.Width - 234;
                isCollapsed = true;
                this.Refresh();
            }
        }

        private void btn_ExportResultToExcel_Click(object sender, EventArgs e)
        {
            string fileName = $"Danh_Sach.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridCtrl_DanhSachHocVien.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
