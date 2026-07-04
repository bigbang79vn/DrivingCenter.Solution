using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.LyThuyet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App
{
	public partial class frmKiemTraLyThuyet: DevExpress.XtraEditors.XtraForm
	{
        private readonly DAT_DAO _DAO = new DAT_DAO();
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
            List<NguoiLxInLT> lst = _DAO.GetAllNguoiLxByKhoaHoc(_khoahocId);

            int[] columnsToSelect = { 1, 9, 10, 12, 14, 16, 18, 20, 22, 26, 27 };

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
            List<GiaoVien> lst = _DAO.GetAllGV();
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