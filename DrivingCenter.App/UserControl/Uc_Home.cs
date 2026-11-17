using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_Home : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly Color ColorBrand = Color.FromArgb(78, 109, 156);
        private static readonly Color ColorRunning = Color.FromArgb(22, 163, 74);

        private readonly ThongKeDatDAO _thongKeDao = new ThongKeDatDAO();
        private readonly KhoaHocDatDAO _khoaHocDao = new KhoaHocDatDAO();
        private readonly XeTapLaiDAO _xeTapLaiDao = new XeTapLaiDAO();

        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;
        private bool _isInitializing = true;
        private bool _isLoading;

        public Uc_Home()
        {
            InitializeComponent();
        }

        private async void Uc_Home_Load(object sender, EventArgs e)
        {
            InitFilters();
            InitGridXeOnline();
            _isInitializing = false;

            await RefreshAllAsync();
            tmr_XeOnline.Start();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                btn_Refresh.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Khởi tạo

        private void InitFilters()
        {
            List<int> years = _thongKeDao.LstYear();
            if (!years.Contains(year))
                years.Insert(0, year);

            cbe_Year.Properties.Items.Clear();
            cbe_Year.Properties.Items.AddRange(years.OrderByDescending(y => y).ToArray());
            cbe_Year.EditValue = year;

            cbe_Month.Properties.Items.Clear();
            for (int i = 1; i <= 12; i++)
                cbe_Month.Properties.Items.Add(new MonthItem(i));
            cbe_Month.SelectedIndex = month - 1;
        }

        private void InitGridXeOnline()
        {
            GridHelper.EnableRowNumbering(gridView_XeOnline);

            // Xe đang chạy (vận tốc > 0) được tô xanh để dễ nhận biết
            var rule = new GridFormatRule
            {
                Column = col_VanToc,
                ApplyToRow = false,
                Rule = new DevExpress.XtraEditors.FormatConditionRuleValue
                {
                    Condition = DevExpress.XtraEditors.FormatCondition.Greater,
                    Value1 = 0d
                }
            };
            var ruleValue = (DevExpress.XtraEditors.FormatConditionRuleValue)rule.Rule;
            ruleValue.Appearance.ForeColor = ColorRunning;
            ruleValue.Appearance.Font = new Font(gridView_XeOnline.Appearance.Row.Font, FontStyle.Bold);
            ruleValue.Appearance.Options.UseForeColor = true;
            ruleValue.Appearance.Options.UseFont = true;
            gridView_XeOnline.FormatRules.Add(rule);
        }

        #endregion

        #region Tải dữ liệu

        private async Task RefreshAllAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            btn_Refresh.Enabled = false;

            IOverlaySplashScreenHandle overlay = null;
            try
            {
                overlay = SplashScreenManager.ShowOverlayForm(tlp_Content);

                await Task.WhenAll(
                    LoadKpiAsync(year),
                    LoadKhoaHocDDTAsync(),
                    LoadChartAsync(year, month),
                    LoadXeOnlineAsync());

                lbl_LastUpdate.Text = $"Cập nhật lúc {DateTime.Now:HH:mm:ss dd/MM/yyyy}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Không tải được dữ liệu Dashboard.\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (overlay != null)
                    SplashScreenManager.CloseOverlayForm(overlay);
                btn_Refresh.Enabled = true;
                _isLoading = false;
            }
        }

        private async Task LoadKpiAsync(int year)
        {
            var kpi = await Task.Run(() => new
            {
                HocVienNam = _thongKeDao.LoadSoHocVienByYear(year),
                KhoaHocNam = _thongKeDao.LoadSoKhoaHocByYear(year),
                TongHocVien = _thongKeDao.LoadSoHocVien(),
                TongKhoaHoc = _thongKeDao.LoadSoKhoaHoc(),
                GiaoVien = _thongKeDao.LoadSoGiaoVien(),
                XeTapLai = _thongKeDao.LoadSoXeTapLai()
            });
            if (IsDisposed) return; // người dùng đã chuyển trang trong lúc đang tải

            lbl_HocVienTitle.Text = $"HỌC VIÊN NĂM {year}";
            lbl_HocVienValue.Text = kpi.HocVienNam.ToString("N0");
            lbl_HocVienSub.Text = $"Tổng tích lũy: {kpi.TongHocVien:N0}";

            lbl_KhoaHocTitle.Text = $"KHÓA HỌC NĂM {year}";
            lbl_KhoaHocValue.Text = kpi.KhoaHocNam.ToString("N0");
            lbl_KhoaHocSub.Text = $"Tổng tích lũy: {kpi.TongKhoaHoc:N0}";

            lbl_GiaoVienValue.Text = kpi.GiaoVien.ToString("N0");
            lbl_XeTapLaiValue.Text = kpi.XeTapLai.ToString("N0");
        }

        private async Task LoadKhoaHocDDTAsync()
        {
            var lst = await Task.Run(() =>
            {
                _khoaHocDao.UpdateKhoaHocDangDT();
                return _khoaHocDao.GetAllKhoaHocDDT();
            });
            if (IsDisposed) return; // người dùng đã chuyển trang trong lúc đang tải

            gridControl_KhoaHocDDT.DataSource = lst;
            grp_KhoaHoc.Text = $"KHÓA HỌC ĐANG ĐÀO TẠO ({lst.Count})";
        }

        private async Task LoadXeOnlineAsync()
        {
            var lstXe = await Task.Run(() => _xeTapLaiDao.GetLstXeTapLaiOnline());
            if (IsDisposed) return; // người dùng đã chuyển trang trong lúc đang tải

            int topRow = gridView_XeOnline.TopRowIndex;
            gridControl_XeOnline.DataSource = lstXe;
            gridView_XeOnline.TopRowIndex = topRow;

            grp_XeOnline.Text = $"XE TẬP LÁI ĐANG ONLINE ({lstXe.Count}) · tự làm mới mỗi {tmr_XeOnline.Interval / 1000}s";
            lbl_XeTapLaiSub.Text = $"Đang online: {lstXe.Count:N0}";
        }

        private async Task LoadChartAsync(int year, int month)
        {
            var lst = await Task.Run(() =>
            {
                var tempList = new List<ChartSoPhien>();
                int numDay = DateTime.DaysInMonth(year, month);

                for (int i = 1; i <= numDay; i++)
                {
                    int Sophien = _thongKeDao.CountPhien(i, month, year);
                    //decimal TongKm = _DAO.SumKmByDay(i, month, year);
                    int TongSoKm = 1212;
                    tempList.Add(new ChartSoPhien(i.ToString(), Sophien.ToString(), TongSoKm.ToString()));
                }

                return tempList;
            });
            if (IsDisposed) return; // người dùng đã chuyển trang trong lúc đang tải

            // Area thường (không spline): spline nội suy làm đường cong võng xuống dưới 0
            var series = new Series("Số Phiên", ViewType.Area)
            {
                ArgumentScaleType = ScaleType.Qualitative,
                CrosshairLabelPattern = "Ngày {A}/" + month + ": {V:N0} phiên"
            };

            int tongPhien = 0;
            foreach (var point in lst)
            {
                int soPhien = int.TryParse(point.SoPhien, out int v) ? v : 0;
                tongPhien += soPhien;
                series.Points.Add(new SeriesPoint(point.Day, soPhien));
            }

            var view = (AreaSeriesView)series.View;
            view.Color = Color.FromArgb(160, ColorBrand);
            view.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            view.Border.Color = ColorBrand;
            view.Border.Thickness = 2;
            view.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            view.MarkerOptions.Kind = MarkerKind.Circle;
            view.MarkerOptions.Size = 6;
            view.MarkerOptions.Color = ColorBrand;
            view.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Gradient;
            if (view.FillStyle.Options is PolygonGradientFillOptions gradient)
            {
                gradient.GradientMode = PolygonGradientMode.TopToBottom;
                gradient.Color2 = Color.FromArgb(20, ColorBrand);
            }

            cc_Chart.Series.Clear();
            cc_Chart.Series.Add(series);

            if (cc_Chart.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Title.Text = "Ngày";
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.GridLines.Visible = false;
                diagram.AxisY.Title.Text = "Số phiên";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dot;
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
                int maxPhien = Math.Max(5, lst.Max(p => int.TryParse(p.SoPhien, out int v) ? v : 0));
                diagram.AxisY.WholeRange.SetMinMaxValues(0, Math.Ceiling(maxPhien * 1.1));
                diagram.AxisY.VisualRange.SetMinMaxValues(0, Math.Ceiling(maxPhien * 1.1));
                diagram.AxisY.Label.TextPattern = "{V:N0}";
            }

            int soNgay = lst.Count(p => p.SoPhien != "0");
            double tbNgay = soNgay > 0 ? (double)tongPhien / soNgay : 0;
            grp_Chart.Text = $"SỐ PHIÊN HỌC THÁNG {month:00}/{year}  ·  Tổng {tongPhien:N0} phiên  ·  TB {tbNgay:N1} phiên/ngày có học";
        }

        #endregion

        #region Sự kiện

        private async void btn_Refresh_Click(object sender, EventArgs e)
        {
            await RefreshAllAsync();
        }

        private async void cbe_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || !(cbe_Year.EditValue is int selectedYear)) return;

            year = selectedYear;
            await RefreshAllAsync();
        }

        private async void cbe_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || !(cbe_Month.EditValue is MonthItem item) || _isLoading) return;

            month = item.Month;
            await LoadChartAsync(year, month);
        }

        private async void tmr_XeOnline_Tick(object sender, EventArgs e)
        {
            if (_isLoading || !Visible) return;

            try
            {
                await LoadXeOnlineAsync();
            }
            catch
            {
                // Bỏ qua lỗi mạng tạm thời, lần tick sau sẽ thử lại
            }
        }

        private void gridView_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            var view = (GridView)sender;
            if (view.RowCount > 0) return;

            string text = view == gridView_XeOnline
                ? "Hiện không có xe tập lái nào online"
                : "Không có khóa học đang đào tạo";

            using (var font = new Font("Roboto", 10F, FontStyle.Italic))
            {
                e.Cache.DrawString(text, font, Brushes.Gray, e.Bounds,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            e.Handled = true;
        }

        #endregion

        private sealed class MonthItem
        {
            public int Month { get; }
            public MonthItem(int month) { Month = month; }
            public override string ToString() => "Tháng " + Month;
        }
    }
}
