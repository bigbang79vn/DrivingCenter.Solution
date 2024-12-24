using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
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
    public partial class fGhiChu : DevExpress.XtraEditors.XtraForm
    {
        private string _diemdanhnguoilxId;
        public fGhiChu()
        {
            InitializeComponent();
        }

        public fGhiChu(string diemdanhnguoilxId) : this()
        {
            _diemdanhnguoilxId = diemdanhnguoilxId;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_AcceptGhiChu_Click(object sender, EventArgs e)
        {
            string note = txtGhiChu.Text;
            DAT_DAO.Instance.LockSession(_diemdanhnguoilxId, note);
            this.Close();
        }
    }
}