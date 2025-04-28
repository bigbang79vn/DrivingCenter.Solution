using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DevExpress.XtraGrid.Views.Grid;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_KhoaHoc_PhanCong : DevExpress.XtraEditors.XtraUserControl
    {
        public UcChild_KhoaHoc_PhanCong()
        {
            InitializeComponent();
        }

        private void UcChild_KhoaHoc_PhanCong_Load(object sender, EventArgs e)
        {
            GetKhoaHocDuKien();
            Get_DmGiaoVien();
        }

        private string MaKhDuKien;
        private void GetKhoaHocDuKien()
        {
            List<KhoaHocGPLX> lst = GPLX_DAO.Instance.GetlstKH();

            cbb_KhoaHocDuKien.Properties.DataSource = lst;
            cbb_KhoaHocDuKien.Properties.DisplayMember = "TenKHGplx";
            cbb_KhoaHocDuKien.Properties.ValueMember = "MaKHGplx";
        }
        private void Get_DmGiaoVien()
        {
            var date = AppBee_DAO.Instance.GetAllGV().Where(x => x.SuDung == true).ToList();
            listLeft = new BindingList<GiaoVien>(date);
            gridControlLeft.DataSource = listLeft;

            listRight = new BindingList<GiaoVien>();
            gridControlRight.DataSource = listRight;
        }
        private void cbb_KhoaHocDuKien_EditValueChanged(object sender, EventArgs e)
        {
            MaKhDuKien = cbb_KhoaHocDuKien.EditValue.ToString();

            if (MaKhDuKien != null)
            {
                DataRow row = GPLX_DAO.Instance.KhoaHocSelected(MaKhDuKien);
                txt_MaKH.Text = row["MaKH"].ToString();
                txt_TenKH.Text = row["TenKH"].ToString();
                txt_HangDT.Text = row["HangDT"].ToString();
                dateEdit_NgayKG.DateTime = (DateTime)row["NgayKG"];
                dateEdit_NgayBG.DateTime = (DateTime)row["NgayBG"];
                txt_SoHV.Text = row["TongSoHV"].ToString();
                txt_ThoiGianDT.Text = row["ThoiGianDT"].ToString();
                memoEdit_GhiChu.Text = row["GhiChu"].ToString();
            }
        }

        BindingList<GiaoVien> listLeft = new BindingList<GiaoVien>();
        BindingList<GiaoVien> listRight = new BindingList<GiaoVien>();
        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewLeft.GetSelectedRows();
            foreach (var rowHandle in selectedRows.Reverse()) // Reverse để xóa không lỗi index
            {
                var giaoVien = gridViewLeft.GetRow(rowHandle) as GiaoVien;
                if (giaoVien != null)
                {
                    listRight.Add(giaoVien);
                    listLeft.Remove(giaoVien);
                }
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewRight.GetSelectedRows();
            foreach (var rowHandle in selectedRows.Reverse())
            {
                var giaoVien = gridViewRight.GetRow(rowHandle) as GiaoVien;
                if (giaoVien != null)
                {
                    listLeft.Add(giaoVien);
                    listRight.Remove(giaoVien);
                }
            }
        }

        private void gridViewLeft_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }

        private void gridViewRight_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }
    }
}
