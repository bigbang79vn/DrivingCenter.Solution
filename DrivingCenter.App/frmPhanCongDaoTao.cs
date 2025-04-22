using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.DAT;
using DocumentFormat.OpenXml.Bibliography;

namespace DrivingCenter.App
{
	public partial class frmPhanCongDaoTao: DevExpress.XtraEditors.XtraForm
	{
		private int _monthId; 
        public frmPhanCongDaoTao(int monthId)
		{
            InitializeComponent();
			_monthId = monthId;
		}

        private void frmPhanCongDaoTao_Load(object sender, EventArgs e)
        {
            dateEdit_Start.EditValue = DateTime.Today;
            dateEdit_End.EditValue = DateTime.Today;

            LoadLichHoc(_monthId);
            LoadXeTapLai();
        }

        public void LoadLichHoc(int monthId)
        {
            List<Schedule> lst = new List<Schedule>();
            lst = DAT_DAO.Instance.GetAllSchedule(_monthId);

            gridCtrl_LichHoc.DataSource = lst;
        }

        private void LoadXeTapLai()
        {
            List<XeTapLai> lst = DAT_DAO.Instance.GetLstXeTapLai().Where(x=>x.ThanhLy == false).ToList();
            repoGrid_XeTapLai.DataSource = lst;
            repoGrid_XeTapLai.DisplayMember = "BienSoXe";
            repoGrid_XeTapLai.ValueMember = "MaXe";

            repoGrid_XeTapLai.PopupView.Columns.AddField("BienSoXe").Visible = true;
            repoGrid_XeTapLai.PopupView.Columns.AddField("MaXe").Visible = true;
            repoGrid_XeTapLai.PopupView.Columns.AddField("HangGPLX").Visible = true;

            List<XeTapLai> lstSTD = lst.Where(i => i.HangGPLX == "B11").ToList();
            repoGrid_XeSTD.DataSource = lstSTD;
            repoGrid_XeSTD.DisplayMember = "BienSoXe";
            repoGrid_XeSTD.ValueMember = "MaXe";

            repoGrid_XeSTD.PopupView.Columns.AddField("BienSoXe").Visible = true;
            repoGrid_XeSTD.PopupView.Columns.AddField("MaXe").Visible = true;
            repoGrid_XeSTD.PopupView.Columns.AddField("HangGPLX").Visible = true;

            gridControl_XeSTD.DataSource = lstSTD;
        }

        private void btn_Apdunglich_Click(object sender, EventArgs e)
        {
            Schedule rowData = gridView_LichHoc.GetFocusedRow() as Schedule;
            string timeStart = dateEdit_Start.DateTime.ToString("yyyy-MM-dd 00:00:01");
            string timeEnd = dateEdit_End.DateTime.ToString("yyyy-MM-dd 23:59:59");
            DAT_DAO.Instance.UpdatePhanCongDAT(rowData.KhoaHocId, timeStart, timeEnd);

            LoadLichHoc(_monthId);
        }
        private void gridView_LichHoc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Schedule rowData = gridView_LichHoc.GetFocusedRow() as Schedule;
            DAT_DAO.Instance.UpdateXeTapB11(rowData);
        }
    }
}