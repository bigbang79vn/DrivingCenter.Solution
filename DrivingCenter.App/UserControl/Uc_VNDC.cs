using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_VNDC : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_VNDC()
        {
            InitializeComponent();
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
        }

        private void btnGiaoVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                UcChild_GiaoVien uc = new UcChild_GiaoVien();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
            }
        }

        private void btnXeTapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                UcChild_XeDaoTao uc = new UcChild_XeDaoTao();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
            }
        }

        private void btnDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_XeTapLai uc = new Uc_XeTapLai();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
            }
        }

        private void btnImportData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                UcChild_GPLX uc = new UcChild_GPLX();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
            }
        }

        private void btnXepLop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                UcChild_XepLop uc = new UcChild_XepLop();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
            }
        }

        private void btnLuuLuongDT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                UcChild_KhoaHoc_PhanCong uc = new UcChild_KhoaHoc_PhanCong();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
            }
        }
    }
}
