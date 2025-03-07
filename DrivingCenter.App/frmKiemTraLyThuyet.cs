using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrivingCenter.App.DTO.LyThuyet;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.IO;

namespace DrivingCenter.App
{
	public partial class frmKiemTraLyThuyet: DevExpress.XtraEditors.XtraForm
	{
		private readonly DataTable _data;
        private readonly int _khoahocId;
        public frmKiemTraLyThuyet(DataTable data, int khoahocId)
		{
            InitializeComponent();
			_data = data;
            _khoahocId = khoahocId;
		}

        private string TenKhoaHoc;
        private void frmKiemTraLyThuyet_Load(object sender, EventArgs e)
        {

            LoadGiaoVien();
            List<NguoiLxInLT> lst = DAT_DAO.Instance.GetAllNguoiLxByKhoaHoc(_khoahocId);

            int[] columnsToSelect = { 1, 4, 5, 7, 9, 11, 13, 15, 17, 19, 20 };

            var result = _data.AsEnumerable()
                       .Skip(4) // Bỏ 5 dòng đầu, lấy từ dòng thứ 6
                       .Select(row => columnsToSelect.Select(col => row[col].ToString()).ToList()).Select(item => new NguoiLxInLT
                       {
                           CCCD = item[0].ToString(),
                           KetLuan = item[1].ToString(),
                           DaoDuc = item[2].ToString(),
                           KyThuat = item[3].ToString(),
                           CauTao = item[4].ToString(),
                           PL1 = item[5].ToString(),
                           PL2 = item[6].ToString(),
                           PL3 = item[7].ToString(),
                           MoPhong = item[8].ToString(),
                           //LastLogin = item[10].ToString(),
                           TimeToDay = item[10].ToString()
                       }).ToList();

            foreach (var item in lst)
            {
                foreach (var res in result)
                {
                    if (item.CCCD == res.CCCD)
                    {
                        item.KetLuan = res.KetLuan;
                        item.DaoDuc = res.DaoDuc;
                        item.CauTao = res.CauTao;
                        item.KyThuat = res.KyThuat;
                        item.PL1 = res.PL1;
                        item.PL2 = res.PL2;
                        item.PL3 = res.PL3;
                        item.MoPhong = res.MoPhong;
                        item.TimeToDay = res.TimeToDay;
                    }
                }
            }

            TenKhoaHoc = lst.First().TenKH;
            lbl_KhoaHoc.Text = "KẾT QUẢ HỌC LÝ THUYẾT KHÓA HỌC " + TenKhoaHoc;
            gridCtrl_DanhSachHocVien.DataSource = lst;
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = $"{TenKhoaHoc}.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridCtrl_DanhSachHocVien.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            XtraMessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}