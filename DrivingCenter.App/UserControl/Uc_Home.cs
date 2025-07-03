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
        }

        private List<ChartSoPhien> lst;
        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;

        private async void Uc_Home_Load(object sender, EventArgs e)
        {
            AddItemYearToCombobox();
            LoadKhoaHocDDT();
            LoadDashboarByYear(year);

            await Task.WhenAll(TaskChart(), TaskXeOnline());
        }

        private void AddItemYearToCombobox()
        {
            List<int> yearDashboards = DAT_DAO.Instance.LstYear();

            cbb_Year.DataSource = yearDashboards;
            cbb_Year.DisplayMember = "Nam";
        }

        private void LoadDashboarByYear(int year)
        {
            lb_SoHocVienByNam.Text = DAT_DAO.Instance.LoadSoHocVienByYear(year).ToString();
            lb_SoKhoaHocByNam.Text = DAT_DAO.Instance.LoadSoKhoaHocByYear(year).ToString();
            lb_SumSoHocVien.Text = DAT_DAO.Instance.LoadSoHocVien().ToString();
            lb_SumSoKhoaHoc.Text = DAT_DAO.Instance.LoadSoKhoaHoc().ToString();
            lb_SoGiaoVien.Text = DAT_DAO.Instance.LoadSoGiaoVien().ToString();
            lb_SoXeTapLai.Text = DAT_DAO.Instance.LoadSoXeTapLai().ToString();
        }

        private async Task TaskXeOnline()
        {
            // Chạy tác vụ nặng trong thread pool
            var lstXe = await Task.Run(() => DAT_DAO.Instance.GetLstXeTapLaiOnline());

            // Cập nhật giao diện trên UI thread
            gridControl_XeOnline.Invoke(new Action(() =>
            {
                gridControl_XeOnline.DataSource = lstXe;
                GridHelper.EnableRowNumbering(gridView_XeOnline);
            }));
        }

        private void LoadKhoaHocDDT()
        {
            DAT_DAO.Instance.UpdateKhoaHocDangDT();
            List<KhoaHocDangDT> lst = DAT_DAO.Instance.GetAllKhoaHocDDT();
            gridControl_KhoaHocDDT.DataSource = lst;
        }

        private async Task TaskChart()
        {
            // Tính toán dữ liệu trong thread pool
            var result = await Task.Run(() =>
            {
                var tempList = new List<ChartSoPhien>();
                int numDay = DateTime.DaysInMonth(year, month);

                for (int i = 1; i <= numDay; i++)
                {
                    int Sophien = DAT_DAO.Instance.CountPhien(i, month, year);
                    float TongKm = 2323; // Hoặc tính đúng ở đây
                    tempList.Add(new ChartSoPhien(i.ToString(), Sophien.ToString(), TongKm.ToString()));
                }

                return tempList;
            });

            // Cập nhật UI trên UI thread
            lst = result;

            lb_Month.Text = "Tháng " + month.ToString();

            Series chartSoPhien = new Series("Số Phiên", ViewType.Area);

            foreach (var point in lst)
            {
                chartSoPhien.Points.Add(new SeriesPoint(point.Day, point.SoPhien));
            }

            cc_Chart.Series.Clear();
            cc_Chart.Series.Add(chartSoPhien);
        }

        private void cbb_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = (int)cbb_Year.SelectedValue;
            LoadDashboarByYear(year);
        }
    }
}
