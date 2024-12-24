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

        private void acc_GPLX_Click(object sender, EventArgs e)
        {
            if (MainContainer.Controls.Count < 0)
            {
                MainContainer.Controls.Clear();
            }
            else
            {
                MainContainer.Controls.Clear();
                Uc_GPLX uc = new Uc_GPLX();
                uc.Dock = DockStyle.Fill;
                MainContainer.Controls.Add(uc);
                this.itemNav.Caption = $"Quản Lý Cabin Điện Tử EMC";
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

        private void acc_QLLT_Click(object sender, EventArgs e)
        {
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
    }
}