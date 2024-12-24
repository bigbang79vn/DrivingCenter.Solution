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
    public partial class rptPrint : DevExpress.XtraEditors.XtraForm
    {
        private string _lstNguoiLXID;
        private int _temp;

        public rptPrint()
        {
            InitializeComponent();
        }

        public rptPrint(string lstNguoiLXID, int temp) : this()
        {
            _lstNguoiLXID = lstNguoiLXID;
            _temp = temp;
        }

        private void rptPrint_Load(object sender, EventArgs e)
        {
            LoadReport(_lstNguoiLXID, _temp);
        }

        public void LoadReport(string lstNguoiLXID, int temp)
        {
            if (temp == 1)
            {
                rptPhieuTheoDoi rpt = new Report.rptPhieuTheoDoi();
                rpt.SetParameterValue("@lstNguoiLXID", lstNguoiLXID);
                rpt.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
            else if (temp == 2)
            {
                rptTheHocVien rpt = new Report.rptTheHocVien();
                rpt.SetParameterValue("@lstNguoiLXID", lstNguoiLXID);
                rpt.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");
                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
            else if (temp == 3)
            {
                rptTemTheDAT rpt = new Report.rptTemTheDAT();
                rpt.SetParameterValue("@lstNguoiLXID", lstNguoiLXID);
                rpt.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");
                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
        }
    }
}