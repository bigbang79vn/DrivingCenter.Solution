using DevExpress.XtraEditors;
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
    public partial class rptPrintResult_EMC : DevExpress.XtraEditors.XtraForm
    {
        public rptPrintResult_EMC(string lstNguoiLXID, int? khoaHocId, int flag)
        {
            InitializeComponent();
            LoadReport(lstNguoiLXID, khoaHocId, flag);
        }

        public void LoadReport(string lstNguoiLXID, int? khoaHocId, int flag)
        {
            if (flag == 1)
            {
                Report.rptResult_EMC rpt = new Report.rptResult_EMC();
                rpt.SetParameterValue("@listNguoiLxID", lstNguoiLXID);
                rpt.SetDatabaseLogon("it", "hieu*123");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
            else if (flag == 2)
            {
                Report.rptTongHopKhoaHocEMC rpt = new Report.rptTongHopKhoaHocEMC();
                rpt.SetParameterValue("@KhoaHocID", khoaHocId);
                rpt.SetDatabaseLogon("it", "hieu*123");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
        }
    }
}