using DevExpress.XtraCharts;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_Home : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
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
            List<int> yearDashboards = _DAO.LstYear();

            cbb_Year.DataSource = yearDashboards;
            cbb_Year.DisplayMember = "Nam";
        }

        private void LoadDashboarByYear(int year)
        {
            lb_SoHocVienByNam.Text = _DAO.LoadSoHocVienByYear(year).ToString();
            lb_SoKhoaHocByNam.Text = _DAO.LoadSoKhoaHocByYear(year).ToString();
            lb_SumSoHocVien.Text = _DAO.LoadSoHocVien().ToString();
            lb_SumSoKhoaHoc.Text = _DAO.LoadSoKhoaHoc().ToString();
            lb_SoGiaoVien.Text = _DAO.LoadSoGiaoVien().ToString();
            lb_SoXeTapLai.Text = _DAO.LoadSoXeTapLai().ToString();
        }

        private async Task TaskXeOnline()
        {
            // Chạy tác vụ nặng trong thread pool
            var lstXe = await Task.Run(() => _DAO.GetLstXeTapLaiOnline());

            // Cập nhật giao diện trên UI thread
            gridControl_XeOnline.Invoke(new Action(() =>
            {
                gridControl_XeOnline.DataSource = lstXe;
                GridHelper.EnableRowNumbering(gridView_XeOnline);
            }));
        }

        private void LoadKhoaHocDDT()
        {
            _DAO.UpdateKhoaHocDangDT();
            List<KhoaHocDangDT> lst = _DAO.GetAllKhoaHocDDT();
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
                    int Sophien = _DAO.CountPhien(i, month, year);
                    //decimal TongKm = _DAO.SumKmByDay(i, month, year);
                    int TongSoKm = 1212;
                    tempList.Add(new ChartSoPhien(i.ToString(), Sophien.ToString(), TongSoKm.ToString()));
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
