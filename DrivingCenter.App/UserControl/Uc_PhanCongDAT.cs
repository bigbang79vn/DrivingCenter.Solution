using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Common;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_PhanCongDAT : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
        public Uc_PhanCongDAT()
        {
            InitializeComponent();
        }

        private void Uc_PhanCongDAT_Load(object sender, EventArgs e)
        {

        }

        private DataTableCollection tableCollection;
        private DataTable dt;
        private void ReadDatafromExcel()
        {
            grdCtrl_Assign.DataBindings.Clear();

            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });

                                tableCollection = result.Tables;

                                if (tableCollection.Count == 0)
                                {
                                    XtraMessageBox.Show("File Excel không chứa Sheet nào.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                cbb_KhoaHoc.Properties.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                {
                                    cbb_KhoaHoc.Properties.Items.Add(table.TableName);
                                }
                                cbb_KhoaHoc.SelectedIndex = 0;

                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        XtraMessageBox.Show($"Không thể đọc file vì đang được sử dụng bởi ứng dụng khác.\nVui lòng đóng file Excel và thử lại.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Assign()
        {
            dt = tableCollection[cbb_KhoaHoc.SelectedItem.ToString()];
            List<AssignmentGV> lst = new List<AssignmentGV>();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AssignmentGV asg = new AssignmentGV();
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
                DialogResult dialogResult = XtraMessageBox.Show("Phân công đào tạo dựa vào danh sách?", "Thông Báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (var item in lst)
                    {
                        _DAO.Assignment(item);
                    }
                    XtraMessageBox.Show(string.Format("Hoàn thành phân công đào tạo khóa {0}", cbb_KhoaHoc.SelectedItem.ToString()), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grp_PhanCongDAT_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Open")
            {
                ReadDatafromExcel();
                grdCtrl_Assign.DataSource = dt;
                GridHelper.EnableRowNumbering(gridView_Assign, 45);
            }
            if (e.Button.Properties.Caption == "Assign")
            {
                Assign();
            }
        }

        private void cbb_KhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            grp_PhanCongDAT.Text = $"Phân công giáo viên khóa học: {cbb_KhoaHoc.SelectedItem.ToString()}";
            grdCtrl_Assign.DataBindings.Clear();
            dt = tableCollection[cbb_KhoaHoc.SelectedItem.ToString()];
            grdCtrl_Assign.DataSource = dt;
        }
    }
}
