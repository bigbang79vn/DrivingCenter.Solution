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
            string MakhoaHoc = "66015K22DB001";
            var data = GPLX_DAO.Instance.Get_lstNguoiLxByMaKh(MakhoaHoc);
            listLeft = new BindingList<NguoiLX>(data);
            gridControlLeft.DataSource = listLeft;

            //listRight = new BindingList<NguoiLX>();
            //gridControlRight.DataSource = listRight;

            var khoaHoc = GPLX_DAO.Instance.GetlstKh();
            SlookUpEdit_KhoaHoc.Properties.DataSource = khoaHoc;
            SlookUpEdit_KhoaHoc.Properties.DisplayMember = "KhoaHocs";
            SlookUpEdit_KhoaHoc.Properties.ValueMember = "MaKH";

            if (khoaHoc.Count > 0)
            {
                SlookUpEdit_KhoaHoc.EditValue = khoaHoc[0].MaKH;
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
                    GPLX_DAO.Instance.UpdateNguoiLxToKhoaHoc(nguoiLx, SlookUpEdit_KhoaHoc.EditValue.ToString());
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
                    GPLX_DAO.Instance.UpdateNguoiLxToKhoaHoc(nguoiLx, "66015K22DB001"); // Trả về khóa học mặc định
                }
            }
        }

        private void SlookUpEdit_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            var obj = SlookUpEdit_KhoaHoc.GetSelectedDataRow() as DTO.GPLX.KhoaHoc;
            if (obj != null)
            {
                txtHangDT.Text = obj.HangDT;
                dtNgayKG.DateTime = obj.NgayKG;
                dtNgayBG.DateTime = obj.NgayBG;

                var data = GPLX_DAO.Instance.Get_lstNguoiLxByMaKh(obj.MaKH);
                listRight = new BindingList<NguoiLX>(data);
                gridControlRight.DataSource = listRight;
            }
        }
    } 
}
