using DevExpress.XtraEditors;
using DrivingCenter.App.UserControl;
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
    public partial class frmTemp : DevExpress.XtraEditors.XtraForm
    {
        public frmTemp()
        {
            InitializeComponent();
        }

        private void frmTemp_Load(object sender, EventArgs e)
        {
            Initialise();
        }

        public void Initialise() 
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
                //this.itemNav.Caption = $"Kết Quả Tốt Nghiệp";
            }

        }
    }
}