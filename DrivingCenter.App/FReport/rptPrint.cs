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
        private int _flag;

        public rptPrint()
        {
            InitializeComponent();
        }

        public rptPrint(string lstNguoiLXID, int flag) : this()
        {
            _lstNguoiLXID = lstNguoiLXID;
            _flag = flag;
        }

        private void rptPrint_Load(object sender, EventArgs e)
        {
            LoadReport(_lstNguoiLXID, _flag);
        }

        public void LoadReport(string lstNguoiLXID, int flag)
        {
            if (flag == 1)
            {
                rptPhieuTheoDoi rpt = new Report.rptPhieuTheoDoi();
                rpt.SetParameterValue("@lstNguoiLXID", lstNguoiLXID);
                rpt.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
            else if (flag == 2)
            {
                rptTheHocVien rpt = new Report.rptTheHocVien();
                rpt.SetParameterValue("@lstNguoiLXID", lstNguoiLXID);
                rpt.SetDatabaseLogon("it", "@97UiLYW0Fwkl3qQWK($4My^");
                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = rpt;
            }
            else if (flag == 3)
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