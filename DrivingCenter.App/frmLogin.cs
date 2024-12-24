using DevExpress.XtraEditors;
using DrivingCenter.App.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private bool isPasswordVisible = false;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain f = new frmMain();
            f.ShowDialog();

            // Sau khi MainForm đóng, quay trở lại LoginForm
            this.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            isPasswordVisible = true;
            txtPassword.UseSystemPasswordChar = isPasswordVisible;
            btnShow.BringToFront();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            isPasswordVisible = false;
            txtPassword.UseSystemPasswordChar = isPasswordVisible;
            btnHide.BringToFront();
        }
    }
}