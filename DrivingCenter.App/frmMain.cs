using DevExpress.Utils.Frames;
using DevExpress.XtraBars;
using DrivingCenter.App.UserControl;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        bool isDarkMode = false;
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_Home uc = new Uc_Home();
                //Uc_CabinEco uc = new Uc_CabinEco();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Dashboard";
            }
        }

        private void acc_Home_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_Home uc = new Uc_Home();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Dashboard";
            }
        }

        private void acc_CabinEco_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_CabinEco uc = new Uc_CabinEco();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Quản Lý Cabin Điện Tử Ecotek";
            }
        }

        private void acc_CabinEMC_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_CabinEMC uc = new Uc_CabinEMC();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Quản Lý Cabin Điện Tử EMC";
            }
        }

        private void accChild_XeTap_Click(object sender, EventArgs e)
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
                this.itemNav.Caption = $"Danh Mục Thông Tin Xe Đào Tạo";
            }
        }

        private void accChild_GiaoVien_Click(object sender, EventArgs e)
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
                this.itemNav.Caption = $"Danh Mục Giáo Viên Dạy Lái Xe";
            }
        }
        private void acc_GPLX_Click(object sender, EventArgs e)
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
                this.itemNav.Caption = $"Quản Lý Cabin Điện Tử EMC";
            }
        }

        private void acc_XepLop_Click(object sender, EventArgs e)
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
                this.itemNav.Caption = $"Xếp Lớp";
            }
        }
        private void accChild_SapLuuLuong_Click(object sender, EventArgs e)
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
                this.itemNav.Caption = $"Sắp Xếp Lưu Lượng Xe & Giáo Viên";
            }
        }

        private void accChild_PhanCongGiaoVien_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                UcChild_PhanCongDaoTao uc = new UcChild_PhanCongDaoTao();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Phân Công Giáo Viên";
            }
        }
        private void acc_DAT_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_DAT uc = new Uc_DAT();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Quản lý Thời gian và Quãng đường tập lái";
            }
        }

        private void acc_LichHoc_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_DaoTao uc = new Uc_DaoTao();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Lịch học số tự động";
            }
        }
        private void acc_TongHopXe_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_BaoCaoTheoXe uc = new Uc_BaoCaoTheoXe();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Báo Cáo Theo Xe Tập Lái";
            }
        }

        private void acc_XeTapLai_Click(object sender, EventArgs e)
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
                this.itemNav.Caption = $"Xe Tập Lái";
            }
        }

        private void acc_TotNghiep_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_Gradute uc = new Uc_Gradute();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Kết Quả Tốt Nghiệp";
            }
        }

        private void acc_Print_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_Print uc = new Uc_Print();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Kết Quả Tốt Nghiệp";
            }
        }

        private void barButtonItem_Refresh_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void acc_TaiKhoan_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount();
            f.Show();
        }

        private void barToggleSwitchItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            isDarkMode = !isDarkMode;

            if (isDarkMode)
            {
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                barToggleSwitchItem.Caption = "Light Mode";
            }
            else
            {
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Basic");
                barToggleSwitchItem.Caption = "Dark Mode";
            }
        }
    }
}