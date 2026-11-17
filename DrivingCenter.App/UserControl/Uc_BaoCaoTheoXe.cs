using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_BaoCaoTheoXe : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly XeTapLaiDAO _xeTapLaiDao = new XeTapLaiDAO();
        private readonly PhienHocDAO _phienHocDao = new PhienHocDAO();

        // Giao diện nằm trong Designer; code chỉ lo dữ liệu, thẻ KPI và thao tác.
        private XeTapLai _xe;                                   // xe của báo cáo đang hiển thị
        private DateTime _from, _to;
        private List<PhienHoc> _phien = new List<PhienHoc>();
        private bool _loading;
        private int _loadVersion;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public Uc_BaoCaoTheoXe()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
        }

        private async void Uc_BaoCaoTheoXe_Load(object sender, EventArgs e)
        {
            SetRange(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
            UpdateCommandState();
            await LoadlstXeAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                btn_View.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Bộ lọc

        private async Task LoadlstXeAsync()
        {
            List<XeTapLai> lst;
            try
            {
                lst = await Task.Run(() => _xeTapLaiDao.GetLstXeTapLai().Where(x => !x.ThanhLy).OrderBy(x => x.BienSoXe).ToList());
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không tải được danh sách xe tập lái", ex);
                return;
            }
            if (!IsAlive) return;

            cbb_XeTapLai.Properties.DataSource = lst;
            cbb_XeTapLai.Properties.DisplayMember = "BienSoXe";
            cbb_XeTapLai.Properties.ValueMember = "MaXe";
            cbb_XeTapLai.Focus();
        }

        private void cbb_XeTapLai_EditValueChanged(object sender, EventArgs e)
        {
            UpdateCommandState();
        }

        private void SetRange(DateTime from, DateTime to)
        {
            dtk_From.DateTime = from;
            dtk_To.DateTime = to;
        }

        /// <summary>Nút chọn nhanh khoảng ngày; nếu đã chọn xe thì xem báo cáo luôn.</summary>
        private void QuickRange_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime firstOfMonth = new DateTime(today.Year, today.Month, 1);
            switch ((string)((Control)sender).Tag)
            {
                case "homnay": SetRange(today, today); break;
                case "7ngay": SetRange(today.AddDays(-6), today); break;
                case "thangnay": SetRange(firstOfMonth, today); break;
                case "thangtruoc": SetRange(firstOfMonth.AddMonths(-1), firstOfMonth.AddDays(-1)); break;
            }
            if (cbb_XeTapLai.GetSelectedDataRow() is XeTapLai) btn_View.PerformClick();
        }

        #endregion

        #region Tải báo cáo

        private async void btn_View_Click(object sender, EventArgs e)
        {
            var xe = cbb_XeTapLai.GetSelectedDataRow() as XeTapLai;
            if (xe == null)
            {
                XtraMessageBox.Show("Vui lòng chọn một xe tập lái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbb_XeTapLai.Focus();
                return;
            }

            DateTime from = dtk_From.DateTime.Date, to = dtk_To.DateTime.Date;
            if (from > to)
            {
                XtraMessageBox.Show("\"Từ ngày\" phải nhỏ hơn hoặc bằng \"Đến ngày\".", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtk_From.Focus();
                return;
            }

            int version = ++_loadVersion;
            _xe = xe;
            _from = from;
            _to = to;
            SetLoading(true);

            List<PhienHoc> rs;
            try
            {
                rs = await Task.Run(() => _phienHocDao.LstPhienHocTheoXe(xe.MaXe, from, to));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _loadVersion) return;
                SetLoading(false);
                ShowError("Không tải được báo cáo", ex);
                return;
            }
            if (!IsAlive || version != _loadVersion) return;

            _phien = rs;
            gridControl_BaoCaoTheoXe.DataSource = rs;
            GridHelper.EnableRowNumbering(gridView_BaoCaoTheoXe);
            SetLoading(false);
        }

        private void SetLoading(bool loading)
        {
            _loading = loading;
            if (loading)
            {
                _phien = new List<PhienHoc>();
                gridControl_BaoCaoTheoXe.DataSource = null;
                gridView_BaoCaoTheoXe.ShowLoadingPanel();
            }
            else
            {
                gridView_BaoCaoTheoXe.HideLoadingPanel();
            }
            // Dòng tổng chỉ có nghĩa khi có dữ liệu (tránh hiện "Tổng:  phiên" trống)
            gridView_BaoCaoTheoXe.OptionsView.ShowFooter = !loading && _phien.Count > 0;
            pnl_Kpi.Invalidate();
            UpdateCommandState();
        }

        private void UpdateCommandState()
        {
            btn_View.Enabled = !_loading;
            btn_Export.Enabled = !_loading && _phien.Count > 0;
        }

        #endregion

        #region KPI & lưới

        private void pnl_Kpi_Resize(object sender, EventArgs e) => pnl_Kpi.Invalidate();

        private void pnl_Kpi_Paint(object sender, PaintEventArgs e)
        {
            bool none = _xe == null || _loading;
            int soPhien = _phien.Count;
            double km = _phien.Sum(x => x.QuangDuongPhien);
            double gio = _phien.Sum(x => x.dbTime);
            int soHV = _phien.Select(x => x.NguoiLxID).Distinct().Count();
            int soGV = _phien.Select(x => x.HoTenGV).Distinct().Count();
            int chuaGui = _phien.Count(x => !x.SendTongCuc);
            string dash = "—";

            var cards = new[]
            {
                new UiTheme.KpiCard("XE TẬP LÁI", _xe?.BienSoXe ?? dash,
                    _loading ? "Đang tải..." : _xe == null ? "Chọn xe rồi bấm \"Xem báo cáo\"" : $"Hạng {_xe.HangGPLX}  ·  {_from:dd/MM/yyyy} – {_to:dd/MM/yyyy}", UiTheme.Brand),
                new UiTheme.KpiCard("SỐ PHIÊN", none ? dash : soPhien.ToString("n0"),
                    none ? "" : $"{soHV} học viên  ·  {soGV} giáo viên", UiTheme.Brand),
                new UiTheme.KpiCard("QUÃNG ĐƯỜNG", none ? dash : $"{km:n1} km",
                    none || soPhien == 0 ? "" : $"TB {km / soPhien:n1} km / phiên", UiTheme.Success),
                new UiTheme.KpiCard("THỜI GIAN", none ? dash : GioPhut(gio),
                    none || soPhien == 0 ? "" : $"≈ {gio:n1} giờ", UiTheme.Success),
                new UiTheme.KpiCard("CHƯA GỬI TỔNG CỤC", none ? dash : chuaGui.ToString("n0"),
                    none ? "" : chuaGui > 0 ? "phiên cần kiểm tra" : "Đã gửi đủ", UiTheme.Success, !none && chuaGui > 0),
            };

            UiTheme.DrawKpiCards(e.Graphics, pnl_Kpi.DisplayRectangle, cards, UiTheme.SkinText(LookAndFeel));
        }

        private static string GioPhut(double gio)
        {
            var ts = TimeSpan.FromHours(gio);
            return $"{(int)ts.TotalHours:00}:{ts.Minutes:00}";
        }

        private void gridView_BaoCaoTheoXe_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // Phiên chưa gửi Tổng cục: tô nền cam nhạt ô "Gửi TC" để dễ thấy
            if (e.Column == SendTongCuc && Equals(e.CellValue, false))
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(40, UiTheme.Warning);
        }

        private void gridView_BaoCaoTheoXe_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_BaoCaoTheoXe.RowCount > 0 || _loading) return;
            if (_xe == null)
                UiTheme.DrawEmptyState(e, "Chưa có báo cáo", "Chọn xe tập lái và khoảng ngày rồi bấm \"Xem báo cáo\".");
            else if (string.IsNullOrEmpty(gridView_BaoCaoTheoXe.FindFilterText))
                UiTheme.DrawEmptyState(e, "Xe không có phiên học", $"{_xe.BienSoXe} không chạy phiên nào từ {_from:dd/MM/yyyy} đến {_to:dd/MM/yyyy}.");
            else
                UiTheme.DrawEmptyState(e, "Không tìm thấy phiên", "Thử từ khóa khác hoặc xóa ô tìm kiếm.");
        }

        #endregion

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (_xe == null || _phien.Count == 0) return;

            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"BaoCaoXe_{_xe.MaXe}_{_from:yyyyMMdd}-{_to:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    gridControl_BaoCaoTheoXe.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx
                    {
                        ExportType = ExportType.WYSIWYG,
                        SheetName = _xe.MaXe
                    });
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(dialog.FileName) { UseShellExecute = true });
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Không xuất được file", ex);
                }
            }
        }

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show(this, $"{title}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
