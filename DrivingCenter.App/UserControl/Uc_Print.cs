using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Entities.DAT;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    /// <summary>
    /// In thẻ học viên, phiếu theo dõi và tem thẻ DAT cho các học viên được chọn của một khóa học.
    /// </summary>
    public partial class Uc_Print : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly KhoaHocDatDAO _khoaHocDao = new KhoaHocDatDAO();
        private readonly HocVienDatDAO _hocVienDao = new HocVienDatDAO();
        private readonly IMG_DAO _IMG_DAO = new IMG_DAO();

        private List<HocVien> _hocVien = new List<HocVien>();
        private bool _loadingKhoaHoc = true;
        private bool _loadingHocVien;
        private bool _printing;
        private int _loadVersion;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public Uc_Print()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
            GridHelper.EnableRowNumbering(gridView_lstHocVien);
        }

        private async void Uc_Print_Load(object sender, EventArgs e)
        {
            await LoadKhoaHocAsync();
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

        #region Tải dữ liệu

        private async Task LoadKhoaHocAsync()
        {
            _loadingKhoaHoc = true;
            UpdateState();
            List<KhoaHocDat> lst;
            try
            {
                lst = await Task.Run(() => _khoaHocDao.GetAllKhoaHoc());
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                _loadingKhoaHoc = false;
                ErrorLog.Write("Uc_Print.LoadKhoaHoc", ex);
                lbl_Summary.Text = "<color=185,28,28>Không tải được danh sách khóa học.</color>";
                MessageBox.Show("Không tải được danh sách khóa học:\n" + ErrorLog.Unwrap(ex).Message, "In ấn",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsAlive) return;

            object giuLai = sle_KhoaHoc.EditValue;
            _loadingKhoaHoc = false;
            sle_KhoaHoc.Properties.DataSource = lst;

            // Giữ khóa đang chọn khi làm mới; lần đầu chọn khóa khai giảng gần nhất
            object chon = giuLai != null && lst.Any(k => Equals(k.KhoaHocID, giuLai))
                ? giuLai
                : lst.OrderByDescending(k => k.NgayKG).Select(k => (object)k.KhoaHocID).FirstOrDefault();

            if (Equals(sle_KhoaHoc.EditValue, chon))
                await LoadHocVienAsync();
            else
                sle_KhoaHoc.EditValue = chon;   // EditValueChanged sẽ tải học viên

            UpdateState();
        }

        private async void sle_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            await LoadHocVienAsync();
        }

        private async Task LoadHocVienAsync()
        {
            int version = ++_loadVersion;
            if (!(sle_KhoaHoc.EditValue is int idKhoaHoc))
            {
                _hocVien = new List<HocVien>();
                grd_lstHocVien.DataSource = _hocVien;
                UpdateState();
                return;
            }

            _loadingHocVien = true;
            grd_lstHocVien.DataSource = null;
            UpdateState();

            List<HocVien> lst;
            try
            {
                lst = await Task.Run(() => _hocVienDao.GetlstHocVienByKhoaHoc(idKhoaHoc));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _loadVersion) return;
                _loadingHocVien = false;
                ErrorLog.Write("Uc_Print.LoadHocVien", ex);
                UpdateState();
                MessageBox.Show("Không tải được danh sách học viên:\n" + ErrorLog.Unwrap(ex).Message, "In ấn",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsAlive || version != _loadVersion) return;   // đã đổi sang khóa khác trong lúc chờ

            _loadingHocVien = false;
            _hocVien = lst;
            grd_lstHocVien.DataSource = _hocVien;
            gridView_lstHocVien.ClearSelection();
            UpdateState();
        }

        private async void btn_Refresh_Click(object sender, EventArgs e)
        {
            await LoadKhoaHocAsync();
        }

        #endregion

        #region Chọn học viên

        private void gridView_lstHocVien_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateState();
        }

        private void btn_BoChon_Click(object sender, EventArgs e)
        {
            gridView_lstHocVien.ClearSelection();
        }

        /// <summary>Học viên đang được tick, theo thứ tự hiển thị trên lưới.</summary>
        private List<HocVien> SelectedHocVien()
        {
            return gridView_lstHocVien.GetSelectedRows()
                .Where(h => h >= 0)
                .OrderBy(h => gridView_lstHocVien.GetVisibleIndex(h))
                .Select(h => gridView_lstHocVien.GetRow(h) as HocVien)
                .Where(hv => hv != null)
                .ToList();
        }

        private void UpdateState()
        {
            int tong = _hocVien.Count;
            int chon = _loadingHocVien ? 0 : SelectedHocVien().Count;
            var kh = sle_KhoaHoc.GetSelectedDataRow() as KhoaHocDat
                     ?? sle_KhoaHoc.Properties.GetRowByKeyValue(sle_KhoaHoc.EditValue) as KhoaHocDat;

            if (_loadingKhoaHoc)
                lbl_Summary.Text = "Đang tải khóa học...";
            else if (kh == null)
                lbl_Summary.Text = "Chọn khóa học để hiển thị danh sách học viên.";
            else if (_loadingHocVien)
                lbl_Summary.Text = $"Hạng <b>{kh.HangDaoTao}</b>  ·  KG {kh.NgayKG:dd/MM/yyyy} – BG {kh.NgayBG:dd/MM/yyyy}  ·  Đang tải học viên...";
            else
                lbl_Summary.Text = $"Hạng <b>{kh.HangDaoTao}</b>  ·  KG {kh.NgayKG:dd/MM/yyyy} – BG {kh.NgayBG:dd/MM/yyyy}  ·  <b>{tong}</b> học viên  ·  "
                    + (chon > 0 ? $"<color=78,109,156>đã chọn <b>{chon}</b></color>" : "chưa chọn học viên nào");

            bool coChon = chon > 0 && !_printing;
            btn_InThe.Enabled = coChon;
            btn_InPhieu.Enabled = coChon;
            btn_InTem.Enabled = coChon;
            btn_BoChon.Enabled = chon > 0;
            btn_InThe.Text = chon > 0 ? $"In thẻ học viên ({chon})" : "In thẻ học viên";
            btn_Refresh.Enabled = !_loadingKhoaHoc && !_printing;
        }

        private void gridView_lstHocVien_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (_loadingKhoaHoc || _loadingHocVien)
                UiTheme.DrawEmptyState(e, "Đang tải dữ liệu...", "Vui lòng chờ trong giây lát.");
            else if (!(sle_KhoaHoc.EditValue is int))
                UiTheme.DrawEmptyState(e, "Chưa chọn khóa học", "Chọn khóa học ở ô phía trên để hiển thị học viên.");
            else if (_hocVien.Count == 0)
                UiTheme.DrawEmptyState(e, "Khóa học chưa có học viên", "Khóa học này chưa có học viên nào được phân giáo viên.");
            else
                UiTheme.DrawEmptyState(e, "Không tìm thấy học viên", "Thử từ khóa khác hoặc xóa ô tìm kiếm.");
        }

        #endregion

        #region In

        private async void btn_InThe_Click(object sender, EventArgs e)
        {
            await PrintAsync("thẻ học viên", _IMG_DAO.GetDataTheHocVien, () => new rptTheHocVien());
        }

        private async void btn_InPhieu_Click(object sender, EventArgs e)
        {
            await PrintAsync("phiếu theo dõi", _IMG_DAO.GetDataPhieuTheoDoi, () => new rptPhieuTheoDoi());
        }

        private async void btn_InTem_Click(object sender, EventArgs e)
        {
            await PrintAsync("tem thẻ DAT", _IMG_DAO.GetDataTemTheDAT, () => new rptTemTheDAT());
        }

        /// <summary>
        /// Lấy dữ liệu in của các học viên đã chọn (chạy nền, có lớp phủ chờ) rồi mở xem trước báo cáo.
        /// </summary>
        private async Task PrintAsync(string tenBan, Func<string, DataTable> layDuLieu, Func<XtraReport> taoBaoCao)
        {
            var chon = SelectedHocVien();
            if (chon.Count == 0)
            {
                MessageBox.Show("Hãy tick chọn ít nhất một học viên cần in.", "In ấn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Định dạng danh sách giữ như bản cũ: ",id1,id2,..." (thủ tục tự tách theo dấu phẩy)
            string lstNguoiLxID = "," + string.Join(",", chon.Select(hv => hv.NguoiLxID));

            _printing = true;
            UpdateState();
            IOverlaySplashScreenHandle overlay = null;
            DataTable data;
            try
            {
                overlay = SplashScreenManager.ShowOverlayForm(grd_lstHocVien);
                data = await Task.Run(() => layDuLieu(lstNguoiLxID));
            }
            catch (Exception ex)
            {
                ErrorLog.Write("Uc_Print.Print " + tenBan, ex);
                CloseOverlay(ref overlay);
                if (IsAlive)
                {
                    _printing = false;
                    UpdateState();
                    MessageBox.Show($"Không lấy được dữ liệu in {tenBan}:\n" + ErrorLog.Unwrap(ex).Message, "In ấn",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            CloseOverlay(ref overlay);
            if (!IsAlive) return;
            _printing = false;
            UpdateState();

            if (data == null || data.Rows.Count == 0)
            {
                MessageBox.Show($"Không có dữ liệu {tenBan} cho {chon.Count} học viên đã chọn.", "In ấn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            XtraReport rpt = taoBaoCao();
            rpt.DataSource = data;
            new ReportPrintTool(rpt).ShowPreview();
        }

        private static void CloseOverlay(ref IOverlaySplashScreenHandle overlay)
        {
            if (overlay == null) return;
            SplashScreenManager.CloseOverlayForm(overlay);
            overlay = null;
        }

        #endregion
    }
}
