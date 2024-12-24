using DevExpress.XtraEditors;
using DrivingCenter.App.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.FReport
{
    public partial class rptPrintReportCNTN : DevExpress.XtraEditors.XtraForm
    {
        public rptPrintReportCNTN(DataTable dt, int hangDT)
        {
            InitializeComponent();
            LoadReport(dt, hangDT);
        }

        //----Method print report with storeprocedures
        //public void LoadReport(string lstMaDK)
        //{
        //    Report.rptChungChiStore rpt = new Report.rptChungChiStore();
        //    rpt.SetParameterValue("@lstMaDK", lstMaDK);
        //    rpt.SetDatabaseLogon("it", "vNdc@123");

        //    crystalReportViewer.ReportSource = null;
        //    crystalReportViewer.ReportSource = rpt;
        //}

        //----Method print report with dataset
        public void LoadReport(DataTable dt, int hangDT)
        {
            rptChungChi rpt = new rptChungChi();
            rptChungChi_New rptNew = new rptChungChi_New();
            if (hangDT == 1)
            {
                rptNew.Database.Tables["NguoiLxCNTN"].SetDataSource(dt);
                rptNew.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rptNew;
            }
            else
            {
                rpt.Database.Tables["NguoiLxCNTN"].SetDataSource(dt);
                rpt.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
        }
    }
}