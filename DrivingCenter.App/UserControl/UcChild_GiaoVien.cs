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
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_GiaoVien : DevExpress.XtraEditors.XtraUserControl
    {
        public UcChild_GiaoVien()
        {
            InitializeComponent();
        }

        private void UcChild_GiaoVien_Load(object sender, EventArgs e)
        {
            Get_DmGiaoVien();
        }

        private void Get_DmGiaoVien()
        {
            List<GiaoVien> data = DAT_DAO.Instance.GetAllGV();
            gridControl_GiaoVien.DataSource = data;
        }
    }
}
