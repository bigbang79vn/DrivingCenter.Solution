using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DTO.GPLX;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_LichHoc : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_LichHoc()
        {
            InitializeComponent();

            tblLayout_LichHoc.Dock = DockStyle.Fill;
            tblLayout_ImportData.Dock = DockStyle.Fill;

            LoadCategory();
            LoadXeTapLai();
            LoadCabin();

        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        #region Lịch học
        
        private void LoadCategory()
        {
            DataTable dt = DAT_DAO.Instance.GetCategory();

            treeList_Category.DataSource = dt;
            //treeList_Category.ExpandAll();
        }


        public void LoadLichHoc(int monthId)
        {
            List<Schedule> lst = new List<Schedule>();
            lst = DAT_DAO.Instance.GetAllSchedule(monthId);

            gridCtrl_LichHoc.DataSource = lst;
        }

        private void ExpandParentNodes(TreeListNode node)
        {
            while (node != null)
            {
                node.Expanded = true;
                node = node.ParentNode;
            }
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

        private void LoadCabin()
        {
            List<Cabin> lst = CabinEco_DAO.Instance.GetLstCabin();
            repoGrid_Cabin.DataSource = lst;
            repoGrid_Cabin.DisplayMember = "NameCabin";
            repoGrid_Cabin.ValueMember = "IdThietBi";

            repoGrid_Cabin.PopupView.Columns.AddField("NameCabin").Visible = true;
            repoGrid_Cabin.PopupView.Columns.AddField("GhiChu").Visible = true;
        }

        private void treeList_Category_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var focusedNode = e.Node;
            if (focusedNode != null)
            {
                ExpandParentNodes(focusedNode);
                int MonthId = (int)focusedNode["MonthID"];
                LoadLichHoc(MonthId);
            }
        }

        private void gridView_LichHoc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Schedule rowData = gridView_LichHoc.GetFocusedRow() as Schedule;
            DAT_DAO.Instance.UpdateXeTapB11(rowData);
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

    }
}
