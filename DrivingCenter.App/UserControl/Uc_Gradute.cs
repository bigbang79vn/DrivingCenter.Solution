using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.Helper;
using NPOI.SS.Formula.Functions;
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
    public partial class Uc_Gradute : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_Gradute()
        {
            InitializeComponent();
        }

        private void Uc_Gradute_Load(object sender, EventArgs e)
        {
            ConnectMySQL.EstablishConnection();
            LoadListKhoaHoc();
        }

        private DataTable dt = new DataTable();
        private int selectedrowindex;
        private List<ThiSinh> lstThiSinh = new List<ThiSinh>();

        private void LoadListKhoaHoc()
        {
            DataTable dt = MySQL_DAO.GetlistKhoaHoc();
            dtgv_khoahocdangthi.DataSource = dt;
        }
        private void btn_ReadData_Click(object sender, EventArgs e)
        {
            string filePath = "";
            int STT = 0;

            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Xml |*.xml" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    filePath = openFileDialog.FileName;
                }

                //string XMLpath = Directory.GetCurrentDirectory() + @"\DATA.xml";

                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    // Load the XML file
                    xmlDoc.Load(filePath);

                    XmlNodeList nodes = xmlDoc.SelectNodes("//BAO_CAO2/DATA/NGUOI_LXS/NGUOI_LX");
                    // Iterate through child nodes
                    foreach (XmlNode node in nodes)
                    {
                        XmlNode nodeMaDK = node.SelectSingleNode("MA_DK");
                        XmlNode nodeHoten = node.SelectSingleNode("HO_VA_TEN");
                        XmlNode nodeNgaysinh = node.SelectSingleNode("NGAY_SINH");
                        XmlNode nodecmt = node.SelectSingleNode("SO_CMT");

                        ThiSinh ts = new ThiSinh();
                        ts.STT = STT + 1;
                        ts.MaDK = nodeMaDK.InnerText.ToString();
                        ts.HoVaTen = nodeHoten.InnerText.ToString();
                        string ngaysinh = nodeNgaysinh.InnerText.ToString();
                        ts.NgaySinh = DateTime.ParseExact(ngaysinh, "yyyyMMdd", CultureInfo.InvariantCulture);
                        ts.SoCMT = nodecmt.InnerText.ToString();

                        STT = ts.STT;
                        lstThiSinh.Add(ts);
                    }

                    ConvertListToDataTable convert = new ConvertListToDataTable();
                    dt = convert.ToDataTable(lstThiSinh);

                    gridControl_ThiSinh.DataSource = dt;

                    //----Format datetime ----------
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["NgaySinh"] = DateTime.Parse((dr["NgaySinh"].ToString())).ToString("dd-MM-yyyy");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void dtgv_khoahocdangthi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexHocVien = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgv_khoahocdangthi.Rows[indexHocVien];
                string IDKhoaHoc = row.Cells["IDKhoaHoc"].Value.ToString();
                lstThiSinh = MySQL_DAO.LoadlstThiSinhByKHTN(IDKhoaHoc);

                //Them STT
                int STT = 0;
                foreach (ThiSinh item in lstThiSinh)
                {
                    item.STT = STT + 1;
                    STT++;
                }

                gridControl_ThiSinh.DataSource = lstThiSinh;

                //----Format datetime ----------

                //foreach (DataRow dr in dt.Rows)
                //{
                //    dr["NgaySinh"] = DateTime.Parse((dr["NgaySinh"].ToString())).ToString("dd-MM-yyyy");
                //}
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            string fileName = $"KetQuaThiTotNghiep.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridControl_ThiSinh.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (dtgv_khoahocdangthi.SelectedCells.Count > 0)
            {
                selectedrowindex = dtgv_khoahocdangthi.SelectedCells[1].RowIndex;
                DataGridViewRow selectedRow = dtgv_khoahocdangthi.Rows[selectedrowindex];
                string IDKhoaHoc = Convert.ToString(selectedRow.Cells["IDKhoaHoc"].Value);

                //Insert tblThiSinh
                foreach (var item in lstThiSinh)
                {
                    MySQL_DAO.InsertThiSinh(IDKhoaHoc, item.MaDK);
                }

                //Xóa thí sinh Duplicate
                List<int> lstIDThiSinhDuplicate = MySQL_DAO.GetlstIDThiSinhDuplicate(IDKhoaHoc);
                foreach (var item in lstIDThiSinhDuplicate)
                {
                    MySQL_DAO.DeleteThiSinh(item, IDKhoaHoc);
                }

                //Insert KhoaHoc_ThiSinh
                List<ThiSinh> lstThiSinhInDatabase = MySQL_DAO.LoadlstThiSinhSH(IDKhoaHoc);
                foreach (var item in lstThiSinhInDatabase)
                {
                    string HangGplxByMaDK = MySQL_DAO.GetHangGplxByMaDK(item.MaDK);
                    MySQL_DAO.InsertKhoaHocThiSinh(IDKhoaHoc, item.IDThiSinh, HangGplxByMaDK);
                }
            }

            MessageBox.Show("Insert dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
            btn_Insert.Enabled = false;
        }
    }
}
