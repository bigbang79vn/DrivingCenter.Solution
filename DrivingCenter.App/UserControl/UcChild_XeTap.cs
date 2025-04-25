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
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DAO;

namespace DrivingCenter.App.UserControl
{
	public partial class UcChild_XeTap: DevExpress.XtraEditors.XtraUserControl
	{
        public UcChild_XeTap()
		{
            InitializeComponent();
		}

        private void UcChild_XeTap_Load(object sender, EventArgs e)
        {
            LoadXeTapLai();
        }

        private void LoadXeTapLai()
        {
            List<XeTapLai> lst = DAT_DAO.Instance.GetLstXeTapLai();
            gridControl_XeTapLai.DataSource = lst;
        }
        private void gridView_XeTapLai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var focusedRow = gridView_XeTapLai.GetFocusedRow() as XeTapLai;
            txt_BienSoXe.Text = focusedRow.BienSoXe;
            cbb_HangXe.Text = focusedRow.HangGPLX;
            txt_NamSX.Text = focusedRow.NamSX.ToString();
            txt_SoGPXTL.Text = focusedRow.SoGPXTL;
            date_NgayCapGPXTL.EditValue = focusedRow.NgayCapGPXTL;
            date_NgayHHGPXTL.EditValue = focusedRow.NgayHHXTL;
            check_ThanhLy.Text = focusedRow.ThanhLy == true ? "Đã thanh lý" : "Chưa thanh lý";
            check_ThanhLy.Checked = focusedRow.ThanhLy == true ? true : false;
        }
    }
}
