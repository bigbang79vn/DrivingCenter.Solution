using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.GPLX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_XepLop : DevExpress.XtraEditors.XtraUserControl
    {
        public UcChild_XepLop()
        {
            InitializeComponent();
        }

        private void UcChild_XepLop_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        BindingList<NguoiLX> listLeft = new BindingList<NguoiLX>();
        BindingList<NguoiLX> listRight = new BindingList<NguoiLX>();
        private void Initialize()
        {   
            // Load danh sách người lái xe chưa xếp lớp vào gridControlLeft
            var data = GPLX_DAO.Instance.Get_lstNguoiLxChuaXepLop();
            listLeft = new BindingList<NguoiLX>(data);
            gridControlLeft.DataSource = listLeft;

            listRight = new BindingList<NguoiLX>();
            gridControlRight.DataSource = listRight;

            var khoaHoc = GPLX_DAO.Instance.Get_lstKhoaHoc();
            SlookUpEdit_KhoaHoc.Properties.DataSource = khoaHoc;
            SlookUpEdit_KhoaHoc.Properties.DisplayMember = "TenKHGplx";
            SlookUpEdit_KhoaHoc.Properties.ValueMember = "MaKHGplx";

            if (khoaHoc.Count > 0)
            {
                SlookUpEdit_KhoaHoc.EditValue = khoaHoc[0].MaKHGplx;
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewLeft.GetSelectedRows();
            foreach (var rowHandle in selectedRows.Reverse())
            {
                var nguoiLx = gridViewLeft.GetRow(rowHandle) as NguoiLX;
                if (nguoiLx != null)
                {
                    listRight.Add(nguoiLx);
                    listLeft.Remove(nguoiLx);
                }
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewRight.GetSelectedRows();
            foreach (var rowHandle in selectedRows.Reverse())
            {
                var nguoiLx = gridViewRight.GetRow(rowHandle) as NguoiLX;
                if (nguoiLx != null)
                {
                    listLeft.Add(nguoiLx);
                    listRight.Remove(nguoiLx);
                }
            }
        }
    }
}
