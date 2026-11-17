using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    /// <summary>
    /// Bố trí xe số tự động (XeB11) cho giáo viên của các khóa trong tháng và đặt thời gian chạy DAT của khóa.
    /// Bên phải là danh sách xe STĐ kèm mức đang sử dụng ("sử dụng đến"). Bố cục nằm trong Designer.
    /// </summary>
    public partial class frmPhanCongDaoTao : DevExpress.XtraEditors.XtraForm
    {
        private readonly PhanCongDAO _phanCongDao = new PhanCongDAO();
        private readonly int _monthId;

        private List<Schedule> _lich = new List<Schedule>();
        private List<XeSTDSuDung> _xeSTD = new List<XeSTDSuDung>();
        private string _xeChon;          // xe STĐ đang chọn bên phải: tô các dòng dùng xe này
        private Schedule _khoaChon;      // khóa đang chọn để áp dụng thời gian chạy DAT
        private bool _loading = true;
        private int _version;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public frmPhanCongDaoTao(int monthId)
        {
            InitializeComponent();
            _monthId = monthId;
            pnl_Header.Paint += UiTheme.PaintHeader;
            GridHelper.EnableRowNumbering(gridView_LichHoc, 40);
        }

        private async void frmPhanCongDaoTao_Load(object sender, EventArgs e)
        {
            // MonthID dạng yyyyMM (vd. 202610)
            string thang = _monthId > 100000 ? $"{_monthId % 100:00}/{_monthId / 100}" : _monthId.ToString();
            Text = $"Bố trí xe tập lái – tháng {thang}";
            lbl_Title.Text = $"Bố trí xe tập lái và xe số tự động – tháng {thang}";
            await LoadAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) { btn_Refresh_Click(this, EventArgs.Empty); return true; }
            if (keyData == Keys.Escape) { Close(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (btn_Refresh.Enabled) _ = LoadAsync();
        }

        #region Tải dữ liệu

        private async Task LoadAsync()
        {
            int version = ++_version;
            _loading = true;
            btn_Refresh.Enabled = false;
            gridView_LichHoc.ShowLoadingPanel();
            gridView_XeSTD.ShowLoadingPanel();

            (List<Schedule> lich, List<XeSTDSuDung> xeSTD, List<XeTapLaiMa> xeTap) d;
            try
            {
                int monthId = _monthId;
                d = await Task.Run(() => (_phanCongDao.GetAllSchedule(monthId), _phanCongDao.GetXeSTDSuDung(), _phanCongDao.GetXeTapLaiMa()));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _version) return;
                gridView_LichHoc.HideLoadingPanel();
                gridView_XeSTD.HideLoadingPanel();
                btn_Refresh.Enabled = true;
                ShowError("Không tải được bố trí xe", ex);
                return;
            }
            if (!IsAlive || version != _version) return;

            _loading = false;
            _lich = d.lich;
            _xeSTD = d.xeSTD;
            repo_XeTap.DataSource = d.xeTap;
            repo_XeSTD.DataSource = _xeSTD;

            // Giữ khóa đang chọn sau khi tải lại
            int? khoaCu = _khoaChon?.KhoaHocId;
            gridCtrl_LichHoc.DataSource = _lich;
            gridControl_XeSTD.DataSource = _xeSTD;
            gridView_LichHoc.HideLoadingPanel();
            gridView_XeSTD.HideLoadingPanel();
            btn_Refresh.Enabled = true;

            int dangDung = _xeSTD.Count(x => x.SoKhoaDangDung > 0);
            lbl_XeSTD.Text = $"Xe số tự động (B.01)  <color=100,116,139><size=-1>({_xeSTD.Count} xe · {dangDung} đang dùng)</size></color>";

            if (khoaCu.HasValue)
            {
                int h = gridView_LichHoc.LocateByValue("KhoaHocId", khoaCu.Value);
                if (h >= 0) gridView_LichHoc.FocusedRowHandle = h;
            }
            SelectKhoa(FocusedSchedule());
        }

        /// <summary>Chỉ tải lại mức sử dụng xe STĐ (sau khi đổi xe của một dòng).</summary>
        private async Task ReloadXeSTDAsync()
        {
            try
            {
                var xe = await Task.Run(() => _phanCongDao.GetXeSTDSuDung());
                if (!IsAlive) return;
                _xeSTD = xe;
                repo_XeSTD.DataSource = _xeSTD;
                gridControl_XeSTD.DataSource = _xeSTD;
            }
            catch { /* lần làm mới sau sẽ cập nhật */ }
        }

        #endregion

        #region Xe STĐ của giáo viên

        private async void gridView_LichHoc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // Chỉ cột Xe STĐ sửa được (trước đây sửa cột Xe tập lái cũng gọi lưu XeB11 nên đổi nhầm xe STĐ)
            if (e.Column != colXeSTD || !(gridView_LichHoc.GetRow(e.RowHandle) is Schedule row)) return;

            string cu = row.MaXeTapB11;
            row.MaXeTapB11 = e.Value as string ?? "";
            try
            {
                await Task.Run(() => _phanCongDao.UpdateXeTapB11(row));
            }
            catch (Exception ex)
            {
                row.MaXeTapB11 = cu;
                gridView_LichHoc.RefreshRow(e.RowHandle);
                ShowError($"Không lưu được xe STĐ cho {row.GiaoVien} – khóa {row.TenKH}", ex);
                return;
            }
            if (!IsAlive) return;
            await ReloadXeSTDAsync();
        }

        private void gridView_XeSTD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _xeChon = (gridView_XeSTD.GetRow(e.FocusedRowHandle) as XeSTDSuDung)?.MaXe;
            gridView_LichHoc.LayoutChanged();
        }

        private void gridView_LichHoc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (_xeChon == null || !(gridView_LichHoc.GetRow(e.RowHandle) is Schedule r) || r.MaXeTapB11 != _xeChon) return;
            e.Appearance.BackColor = Color.FromArgb(45, UiTheme.Brand);
            if (e.Column == colXeSTD) e.Appearance.FontStyleDelta = FontStyle.Bold | FontStyle.Underline;
        }

        private void gridView_XeSTD_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if ((e.Column == colS_SoKhoa || e.Column == colS_SoHV) && e.Value is int n && n == 0) e.DisplayText = "—";
            else if (e.Column == colS_SuDungDen && e.Value == null) e.DisplayText = "Chưa dùng";
        }

        private void gridView_XeSTD_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (!(gridView_XeSTD.GetRow(e.RowHandle) is XeSTDSuDung x)) return;
            if (x.SoKhoaDangDung == 0)
                e.Appearance.ForeColor = UiTheme.TextMuted;
            else if (e.Column == colS_SuDungDen)
                e.Appearance.ForeColor = UiTheme.Brand;
            else if (e.Column == colS_SoKhoa && x.SoKhoaDangDung >= 3)
            {
                // Một xe STĐ dùng chung cho nhiều khóa cùng lúc: tô cam để dễ thấy xe đang bị dồn lịch
                e.Appearance.ForeColor = UiTheme.Warning;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
        }

        #endregion

        #region Thời gian chạy DAT của khóa

        /// <summary>Dòng dữ liệu đang chọn; đang đứng ở dòng nhóm khóa thì lấy dòng đầu của nhóm.</summary>
        private Schedule FocusedSchedule()
        {
            int h = gridView_LichHoc.FocusedRowHandle;
            if (gridView_LichHoc.IsGroupRow(h))
            {
                if (gridView_LichHoc.GetRowLevel(h) == 0) return null;   // nhóm hạng: chưa chọn khóa cụ thể
                h = gridView_LichHoc.GetDataRowHandleByGroupRowHandle(h);
            }
            return gridView_LichHoc.GetRow(h) as Schedule;
        }

        private void gridView_LichHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SelectKhoa(FocusedSchedule());
        }

        private void SelectKhoa(Schedule s)
        {
            bool doiKhoa = s?.KhoaHocId != _khoaChon?.KhoaHocId;
            _khoaChon = s;
            btn_Apdunglich.Enabled = s != null;
            if (s == null)
            {
                lbl_ApDung.Text = "Thời gian chạy DAT – chọn một khóa (hoặc giáo viên của khóa) ở lưới trên";
                return;
            }

            int soGV = _lich.Count(x => x.KhoaHocId == s.KhoaHocId);
            lbl_ApDung.Text = $"Thời gian chạy DAT – khóa <b>{s.TenKH}</b>  <color=100,116,139>(hạng {s.HangDT} · {soGV} giáo viên; áp dụng cho mọi học viên của khóa)</color>";
            btn_Apdunglich.Text = $"Áp dụng cho khóa {s.TenKH}";
            if (doiKhoa)
            {
                // Điền sẵn thời gian hiện tại của khóa (trước đây luôn mặc định hôm nay)
                dateEdit_Start.EditValue = s.ThoiDiemChayDAT?.Date;
                dateEdit_End.EditValue = s.ThoiDiemKetThucDAT?.Date;
            }
        }

        private async void btn_Apdunglich_Click(object sender, EventArgs e)
        {
            var khoa = _khoaChon;
            if (khoa == null) return;
            if (!(dateEdit_Start.EditValue is DateTime tu) || !(dateEdit_End.EditValue is DateTime den))
            {
                XtraMessageBox.Show("Nhập đủ ngày bắt đầu và ngày kết thúc chạy DAT.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (den.Date < tu.Date)
            {
                XtraMessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hienTai = khoa.ThoiDiemChayDAT.HasValue
                ? $"{khoa.ThoiDiemChayDAT:dd/MM/yyyy} – {khoa.ThoiDiemKetThucDAT:dd/MM/yyyy}" : "chưa đặt";
            if (XtraMessageBox.Show($"Đặt thời gian chạy DAT cho toàn bộ học viên khóa {khoa.TenKH}:\n\n"
                    + $"{tu:dd/MM/yyyy} – {den:dd/MM/yyyy}   (hiện tại: {hienTai})",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btn_Apdunglich.Enabled = false;
            try
            {
                int khoaHocId = khoa.KhoaHocId;
                await Task.Run(() => _phanCongDao.UpdatePhanCongDAT(khoaHocId, tu, den));
            }
            catch (Exception ex)
            {
                if (IsAlive) btn_Apdunglich.Enabled = true;
                ShowError("Không áp dụng được thời gian chạy DAT", ex);
                return;
            }
            if (!IsAlive) return;
            _khoaChon = null;   // nạp lại ngày mới của khóa vào ô ngày
            await LoadAsync();
        }

        #endregion

        private void gridView_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (view.RowCount > 0 || _loading) return;
            if (view == gridView_XeSTD)
                UiTheme.DrawEmptyState(e, "Chưa có xe số tự động", "Không có xe hạng B.01 trong danh mục xe DAT.");
            else
                UiTheme.DrawEmptyState(e, "Tháng này chưa có khóa nào", "Chưa có học viên được phân giáo viên trong các khóa của tháng.");
        }

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show($"{title}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
