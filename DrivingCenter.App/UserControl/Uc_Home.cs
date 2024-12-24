using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
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
    public partial class Uc_Home : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_Home()
        {
            InitializeComponent();

            Task tsk = TaskChart();
            Task xeonline = TaskXeOnline();
        }

        private List<ChartSoPhien> lst;
        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;

        private void Uc_Home_Load(object sender, EventArgs e)
        {
            AddItemYearToCombobox();
            LoadKhoaHocDDT();
            LoadDashboarByYear(year);
        }

        private void AddItemYearToCombobox()
        {
            List<int> yearDashboards = DAT_DAO.Instance.LstYear();

            cbb_Year.DataSource = yearDashboards;
            cbb_Year.DisplayMember = "Nam";
        }

        private void LoadDashboarByYear(int year)
        {

                int SoHvByYear = DAT_DAO.Instance.LoadSoHocVienByYear(year);
                lb_SoHocVienByNam.Text = SoHvByYear.ToString();

                int SoKhByYear = DAT_DAO.Instance.LoadSoKhoaHocByYear(year);
                lb_SoKhoaHocByNam.Text = SoKhByYear.ToString();

                int SoHv = DAT_DAO.Instance.LoadSoHocVien();
                lb_SumSoHocVien.Text = SoHv.ToString();

                int SoKH = DAT_DAO.Instance.LoadSoKhoaHoc();
                lb_SumSoKhoaHoc.Text = SoKH.ToString();

                int SoGv = DAT_DAO.Instance.LoadSoGiaoVien();
                lb_SoGiaoVien.Text = SoGv.ToString();

                int SoXeTl = DAT_DAO.Instance.LoadSoXeTapLai();
                lb_SoXeTapLai.Text = SoXeTl.ToString();

        }

        private async Task TaskXeOnline()
        {
            Task tsk = Task.Run(() =>
            {
                List<XeTapLai> lst = DAT_DAO.Instance.GetLstXeTapLaiOnline();
                int stt = 0;
                foreach (XeTapLai item in lst)
                {
                    item.STT = stt + 1;
                    stt++;
                }
                gridControl_XeOnline.DataSource = lst;
            });
            await tsk;
        }

        private void LoadKhoaHocDDT()
        {

                DAT_DAO.Instance.UpdateKhoaHocDangDT();

                List<KhoaHocDangDT> lst = DAT_DAO.Instance.GetAllKhoaHocDDT();

                gridControl_KhoaHocDDT.DataSource = lst;

        }

        private async Task TaskChart()
        {
            Task tsk = Task.Run(() =>
            {
                lb_Month.Text = lb_Month.Text + " " + month.ToString();
                lst = new List<ChartSoPhien>();

                int numDay = DateTime.DaysInMonth(year, month);

                for (int i = 1; i <= numDay; i++)
                {
                    int Sophien = DAT_DAO.Instance.CountPhien(i, month, year);
                    //float TongKm = DAT_DAO.Instance.SumKmByDay(i, month, year);
                    float TongKm = 2323;
                    ChartSoPhien day = new ChartSoPhien(i.ToString(), Sophien.ToString(), TongKm.ToString());
                    lst.Add(day);
                }

                //Tạo chart
                Series chartSoPhien = new Series("Số Phiên", ViewType.Area);
                //Series chartSoKm = new Series("Số Km", ViewType.Line);

                foreach (var point in lst)
                {
                    chartSoPhien.Points.Add(new SeriesPoint(point.Day, point.SoPhien));
                    //chartSoKm.Points.Add(new SeriesPoint(point.Day, point.TongSoKm));
                }

                cc_Chart.Series.Clear();
                cc_Chart.Series.Add(chartSoPhien);
                //cc_Chart.Series.Add(chartSoKm);
            });
            await tsk;
        }

        private void cbb_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = (int)cbb_Year.SelectedValue;
            LoadDashboarByYear(year);
        }
    }
}
