using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    /// <summary>
    /// Sao chép các phiên bài tập cabin của một học viên (nguồn) sang học viên khác (đích).
    /// Giao diện nằm trong Designer; code chỉ lo dữ liệu và thao tác.
    /// </summary>
    public partial class frmReplication : DevExpress.XtraEditors.XtraForm
    {
        private const string DangDaoTao = "Đang Đào Tạo";

        private readonly CabinI2E_DAO _cabinI2E = new CabinI2E_DAO();
        private readonly long _nguoiLxId;      // học viên nguồn
        private int KhoaHocID;
        private int _hvVersion, _kqVersion;
        private bool _dangSaoChep;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public frmReplication()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
        }

        public frmReplication(Int64 nguoiLxId) : this()
        {
            _nguoiLxId = nguoiLxId;
        }

        private async void fReplication_Load(object sender, EventArgs e)
        {
            dt_TimeStart.DateTime = DateTime.Now;
            UpdateCopyButton();

            List<KhoaHocCabin> khoaHoc;
            List<Cabin> cabin;
            HocVien nguon;
            List<Result> baiTapNguon;
            try
            {
                var khTask = Task.Run(() => _cabinI2E.GetAllKhoaHoc().Where(x => x.LoaiDaoTao == DangDaoTao).OrderBy(c => c.TenKhoaHoc).ToList());
                var cabinTask = Task.Run(() => _cabinI2E.GetLstCabin());
                var nguonTask = Task.Run(() => _cabinI2E.GetInfoHVByNguoiLxID(_nguoiLxId));
                var btTask = Task.Run(() => _cabinI2E.GetResultByNguoiLxID(_nguoiLxId));
                await Task.WhenAll(khTask, cabinTask, nguonTask, btTask);
                khoaHoc = khTask.Result; cabin = cabinTask.Result; nguon = nguonTask.Result; baiTapNguon = btTask.Result;
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không tải được dữ liệu", ex);
                return;
            }
            if (!IsAlive) return;

            cbb_Cabin.Properties.DataSource = cabin;
            cbb_Cabin.Properties.DisplayMember = "NameCabin";
            cbb_Cabin.Properties.ValueMember = "IdThietBi";
            if (cabin.Count > 0) cbb_Cabin.EditValue = cabin[0].IdThietBi;

            lbl_NguonInfo.Text = nguon == null
                ? "Không tìm thấy học viên nguồn."
                : $"Của <b>{nguon.HoTenHV}</b>  ·  {nguon.TenKH}  ·  {baiTapNguon.Count} phiên";
            grid_BaiTapNguon.DataSource = baiTapNguon;

            cbb_KhoaHoc.Properties.DataSource = khoaHoc;
            cbb_KhoaHoc.Properties.DisplayMember = "TenKhoaHoc";
            cbb_KhoaHoc.Properties.ValueMember = "KhoaHocID";
            // Mặc định mở khóa của học viên nguồn (thường sao chép trong cùng khóa)
            var cungKhoa = khoaHoc.FirstOrDefault(k => nguon != null && k.TenKhoaHoc == nguon.TenKH);
            if (cungKhoa != null) cbb_KhoaHoc.EditValue = cungKhoa.KhoaHocID;
            else if (khoaHoc.Count > 0) cbb_KhoaHoc.EditValue = khoaHoc[0].KhoaHocID;
        }

        #region Học viên đích

        private void cbb_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            if (!(cbb_KhoaHoc.EditValue is int id)) return;
            KhoaHocID = id;
            LoadLstHvByKH(id);
        }

        private async void LoadLstHvByKH(int idKhoaHoc)
        {
            int version = ++_hvVersion;
            grid_HocVien.DataSource = null;
            gridView_HocVien.ShowLoadingPanel();

            List<HocVien> lstStudent;
            try
            {
                lstStudent = await Task.Run(() =>
                {
                    var list = _cabinI2E.GetlstHocVienByIdKhoaHoc(idKhoaHoc);
                    foreach (HocVien item in list)
                        item.Result = _cabinI2E.CheckDK(item.NguoiLxID, item.MaKH) == "Đáp ứng quy định";
                    return list;
                });
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _hvVersion) return;
                gridView_HocVien.HideLoadingPanel();
                ShowError("Không tải được danh sách học viên", ex);
                return;
            }
            if (!IsAlive || version != _hvVersion) return;

            grid_HocVien.DataSource = lstStudent;
            gridView_HocVien.HideLoadingPanel();
            gridView_HocVien.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            ShowKetQuaDich(null);
        }

        private void gridView_HocVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Dòng nhóm (Giáo viên: ...) không có học viên: trước đây ném NullReferenceException
            ShowKetQuaDich(gridView_HocVien.GetFocusedRow() as HocVien);
        }

        private HocVien HocVienDich => gridView_HocVien.GetFocusedRow() as HocVien;

        private async void ShowKetQuaDich(HocVien hv)
        {
            int version = ++_kqVersion;
            UpdateCopyButton();
            if (hv == null)
            {
                grid_Result.DataSource = null;
                lb_KetQua.Text = "Chọn một học viên ở cột bên trái.";
                return;
            }

            gridView_ResultByHv.ShowLoadingPanel();
            List<Result> ketQua;
            double tongGiay;
            string ketLuan;
            try
            {
                (ketQua, tongGiay, ketLuan) = await Task.Run(() => (
                    _cabinI2E.GetResultByNguoiLxID(hv.NguoiLxID),
                    _cabinI2E.GetTongThoiGianByNguoiLxID(hv.NguoiLxID),
                    _cabinI2E.CheckDK(hv.NguoiLxID, hv.MaKH)));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _kqVersion) return;
                gridView_ResultByHv.HideLoadingPanel();
                ShowError("Không tải được kết quả học viên", ex);
                return;
            }
            if (!IsAlive || version != _kqVersion) return;

            grid_Result.DataSource = ketQua;
            gridView_ResultByHv.HideLoadingPanel();

            // Tổng giờ thực (trước đây định dạng "hh" nên quay vòng khi vượt 24 giờ)
            var t = TimeSpan.FromSeconds(tongGiay);
            string mau = ketLuan == "Đáp ứng quy định" ? "21,128,61" : "185,28,28";
            lb_KetQua.Text = $"<b>{hv.HoTenHV}</b>  ·  {ketQua.Count} phiên  ·  tổng <b>{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}</b>  ·  <color={mau}>{ketLuan}</color>";
        }

        private void gridView_ResultByHv_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_ResultByHv.RowCount > 0 || HocVienDich == null) return;
            UiTheme.DrawEmptyState(e, "Chưa có phiên nào", "Học viên này chưa có kết quả cabin.");
        }

        #endregion

        #region Sao chép

        private void gridView_BaiTapNguon_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) => UpdateCopyButton();

        private List<Result> BaiTapDaChon()
        {
            return gridView_BaiTapNguon.GetSelectedRows()
                .Where(gridView_BaiTapNguon.IsDataRow)
                .Select(h => gridView_BaiTapNguon.GetRow(h) as Result)
                .Where(r => r != null)
                .OrderBy(r => r.ThoiDiemBatDau)
                .ToList();
        }

        private void UpdateCopyButton()
        {
            int soBai = BaiTapDaChon().Count;
            btn_InserResult.Enabled = !_dangSaoChep && soBai > 0 && HocVienDich != null;
            btn_InserResult.Text = soBai > 0 ? $"Sao chép {soBai} bài sang học viên" : "Sao chép sang học viên";
        }

        private async void btn_InserResult_Click(object sender, EventArgs e)
        {
            HocVien hv = HocVienDich;
            var baiTap = BaiTapDaChon();
            if (hv == null || baiTap.Count == 0) return;

            if (hv.NguoiLxID == _nguoiLxId)
            {
                XtraMessageBox.Show(this, "Học viên đích đang trùng với học viên nguồn. Hãy chọn học viên khác.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!(cbb_Cabin.EditValue is int idThietBi))
            {
                XtraMessageBox.Show(this, "Vui lòng chọn cabin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbb_Cabin.Focus();
                return;
            }

            DateTime batDau = dt_TimeStart.DateTime;
            if (XtraMessageBox.Show(this,
                    $"Sao chép {baiTap.Count} phiên bài tập sang học viên {hv.HoTenHV}?\n\n" +
                    $"Cabin: {cbb_Cabin.Text}\nBắt đầu từ: {batDau:dd/MM/yyyy HH:mm} (hoặc ngay sau phiên cuối cùng trong ngày của học viên).",
                    "Xác nhận sao chép", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _dangSaoChep = true;
            UpdateCopyButton();
            Cursor = Cursors.WaitCursor;
            int khoaHocId = KhoaHocID;
            try
            {
                await Task.Run(() =>
                {
                    foreach (Result nguon in baiTap)
                    {
                        // Cùng ngày với phiên cuối của học viên đích thì nối tiếp sau phiên đó, khác ngày thì bắt đầu từ giờ đã chọn
                        DateTime? ketThucCuoi = _cabinI2E.GetLastSessionEnd(hv.NguoiLxID);
                        DateTime thoiDiem = ketThucCuoi.HasValue && ketThucCuoi.Value.Date == batDau.Date && ketThucCuoi.Value > batDau
                            ? ketThucCuoi.Value
                            : batDau;

                        var re = new Result
                        {
                            BaiHocCaBinID = nguon.BaiHocCaBinID,
                            ThoiDiemBatDau = thoiDiem,
                            TotalSecond = nguon.TotalSecond,
                            QuangDuong = nguon.QuangDuong,
                            PathVideo = nguon.PathVideo ?? string.Empty
                        };
                        _cabinI2E.InsertData(re, hv.NguoiLxID, idThietBi, khoaHocId);
                    }
                    _cabinI2E.UpdateTimeExerToNguoiLx(hv.NguoiLxID);
                });
            }
            catch (Exception ex)
            {
                if (IsAlive) ShowError("Không sao chép được kết quả", ex);
                return;
            }
            finally
            {
                _dangSaoChep = false;
                if (IsAlive)
                {
                    Cursor = Cursors.Default;
                    UpdateCopyButton();
                }
            }
            if (!IsAlive) return;

            gridView_BaiTapNguon.ClearSelection();
            ShowKetQuaDich(hv);
            XtraMessageBox.Show(this, $"Đã sao chép {baiTap.Count} phiên sang học viên {hv.HoTenHV}.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show(this, $"{title}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
