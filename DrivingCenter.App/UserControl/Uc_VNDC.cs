using DevExpress.XtraBars;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_VNDC : DevExpress.XtraEditors.XtraUserControl
    {
        private const string RootCaption = "Trung Tâm GDNN Việt Nhật";

        public Uc_VNDC()
        {
            InitializeComponent();
        }

        #region Điều hướng

        /// <summary>
        /// Hiển thị màn hình con vào MainContainer, giải phóng màn hình cũ
        /// và đánh dấu nút đang chọn trên Ribbon.
        /// </summary>
        private void ShowChild<T>(BarButtonItem source, string groupCaption) where T : Control, new()
        {
            // Bấm lại nút đang mở thì giữ nguyên màn hình, không tạo lại
            if (MainContainer.Controls.Count == 1 && MainContainer.Controls[0] is T)
            {
                source.Down = true;
                return;
            }

            Cursor = Cursors.WaitCursor;
            MainContainer.SuspendLayout();
            try
            {
                foreach (var old in MainContainer.Controls.Cast<Control>().ToList())
                    old.Dispose();

                var uc = new T { Dock = DockStyle.Fill };
                MainContainer.Controls.Add(uc);

                source.Down = true;
                bsi_Breadcrumb.Caption = $"{RootCaption}  ›  {groupCaption}  ›  {source.Caption}";
            }
            finally
            {
                MainContainer.ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        private void btnGiaoVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<UcChild_GiaoVien>(btnGiaoVien, rpg_DanhMuc.Text);
        }

        private void btnXeTapLai_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<UcChild_XeDaoTao>(btnXeTapLai, rpg_DanhMuc.Text);
        }

        private void btnDevice_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<UcChild_ThietBiDAT>(btnDevice, rpg_DanhMuc.Text);
        }

        private void btnXepLop_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<UcChild_XepLop>(btnXepLop, rpg_DaoTao.Text);
        }

        private void btn_PhanCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<Uc_PhanCongDAT>(btn_PhanCong, rpg_DaoTao.Text);
        }

        private void btnLuuLuongDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<UcChild_KhoaHoc_PhanCong>(btnLuuLuongDT, rpg_DaoTao.Text);
        }

        private void btnImportData_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowChild<UcChild_GPLX>(btnImportData, rpg_DuLieu.Text);
        }

        private void btnKhoaHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmKhoaHocDangDT frm = new frmKhoaHocDangDT())
            {
                frm.ShowDialog(FindForm());
            }
        }

        #endregion

        private void MainContainer_Paint(object sender, PaintEventArgs e)
        {
            if (MainContainer.Controls.Count > 0) return;

            // Trạng thái trống: hướng dẫn người dùng chọn chức năng
            var bounds = MainContainer.ClientRectangle;
            using (var titleFont = new Font("Roboto", 14F, FontStyle.Bold))
            using (var hintFont = new Font("Roboto", 10F))
            using (var titleBrush = new SolidBrush(Color.FromArgb(78, 109, 156)))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                var titleRect = new Rectangle(bounds.X, bounds.Y + bounds.Height / 2 - 30, bounds.Width, 30);
                var hintRect = new Rectangle(bounds.X, bounds.Y + bounds.Height / 2 + 4, bounds.Width, 24);

                e.Graphics.DrawString("QUẢN LÝ ĐÀO TẠO DAT", titleFont, titleBrush, titleRect, format);
                e.Graphics.DrawString("Chọn một chức năng trên thanh công cụ phía trên để bắt đầu", hintFont, Brushes.Gray, hintRect, format);
            }
        }
    }
}
