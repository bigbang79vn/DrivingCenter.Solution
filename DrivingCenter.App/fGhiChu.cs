using DrivingCenter.App.DAO;
using System;

namespace DrivingCenter.App
{
    public partial class fGhiChu : DevExpress.XtraEditors.XtraForm
    {
        private string _diemdanhnguoilxId;
        private readonly DAT_DAO _DAO = new DAT_DAO();
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
           _DAO.LockSession(_diemdanhnguoilxId, note);
            this.Close();
        }
    }
}