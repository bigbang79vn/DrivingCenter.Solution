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
        }

        private void GetKhoaHocDuKien()
        {
            List<KhoaHocGPLX> lst = GPLX_DAO.Instance.GetlstKH();

            cbb_KhoaHocDuKien.Properties.DataSource = lst;
            cbb_KhoaHocDuKien.Properties.DisplayMember = "TenKHGplx";
            cbb_KhoaHocDuKien.Properties.ValueMember = "MaKHGplx";
        }

        private void cbb_KhoaHocDuKien_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
