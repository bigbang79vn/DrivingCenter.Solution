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
    public partial class rptPrintReport_ECO : DevExpress.XtraEditors.XtraForm
    {
        public rptPrintReport_ECO(string lstMaDK, int idKhoaHoc, int flag)
        {
            InitializeComponent();
            LoadReport(lstMaDK, idKhoaHoc, flag);
        }

        public void LoadReport(string lstMaDK, int idKhoaHoc, int flag)
        {
            if (flag == 1)
            {
                rptResult_Eco rpt = new rptResult_Eco();
                rpt.SetParameterValue("@lstMaDK", lstMaDK);
                rpt.SetDatabaseLogon("it", "hieu*123");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
            else if (flag == 2)
            {
                rptTongHopKhoaHocEco rpt = new rptTongHopKhoaHocEco();
                rpt.SetParameterValue("@flag", 0);
                rpt.SetParameterValue("@ID_KhoaHoc", idKhoaHoc);
                rpt.SetParameterValue("@NguongHang", 10800);
                rpt.SetDatabaseLogon("it", "hieu*123");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
        }
    }
}