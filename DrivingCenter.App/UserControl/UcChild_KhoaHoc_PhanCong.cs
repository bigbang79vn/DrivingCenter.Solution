using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using DrivingCenter.App.Common;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    /// <summary>
    /// Phân công giáo viên + xe tập lái cho khóa học. Mỗi giáo viên trong lớp có xe, số học viên và
    /// thời gian hiệu lực riêng (sửa trực tiếp trên lưới). Bố cục, chữ, màu, icon nằm trong Designer.
    /// </summary>
    public partial class UcChild_KhoaHoc_PhanCong : DevExpress.XtraEditors.XtraUserControl
    {
        private const int SoHVMacDinh = 3;
        private const int SoNgayDenThucHanh = 25;   // thực hành bắt đầu sau khai giảng 25 ngày

        private readonly AppBee_DAO _DAO = new AppBee_DAO();
        private readonly GPLX_DAO _DAO_Gplx = new GPLX_DAO();

        private List<XeTapLai> _allXe = new List<XeTapLai>();
        private BindingList<GiaoVien> _choXep = new BindingList<GiaoVien>();
        private BindingList<PhanCongRow> _trongLop = new BindingList<PhanCongRow>();
        private List<Schedule> _daLuu = new List<Schedule>();

        private string _maKH;
        private string _tenKH;
        private DateTime _ngayKG, _ngayBG;
        private int _tongSoHV;
        private bool _dirty;
        private bool _suppressCourseChange;
        private object _courseValue;
        private int _loadVersion;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        /// <summary>Một giáo viên trong lớp: giá trị đang sửa và giá trị đã lưu (để biết dòng nào cần cập nhật).</summary>
        public sealed class PhanCongRow
        {
            public GiaoVien GiaoVien { get; set; }
            public int GiaoVienID => GiaoVien.GiaoVienID;
            public string HoTenGV => GiaoVien.HoTenGV;
            public string HangGPLX => GiaoVien.HangGPLX;

            public int? IdXeTap { get; set; }
            public int SoHV { get; set; }
            public DateTime? HieuLuc { get; set; }
            public DateTime? HetHieuLuc { get; set; }
            public string LichSTD { get; set; }
            public string LichCabin { get; set; }

            public Schedule Saved { get; set; }   // null: giáo viên mới thêm, chưa lưu

            public bool IsChanged => Saved != null && (Saved.IdXeTap != IdXeTap || (Saved.SoHV ?? 0) != SoHV
                                     || Saved.NgayHL.Date != (HieuLuc ?? DateTime.MinValue).Date
                                     || Saved.NgayHetHL.Date != (HetHieuLuc ?? DateTime.MinValue).Date);

            public string TrangThai => Saved == null ? "Mới" : IsChanged ? "Đã sửa" : "Đã lưu";
        }

        public UcChild_KhoaHoc_PhanCong()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
            gridControlLeft.DataSource = _choXep;
            gridControlRight.DataSource = _trongLop;
        }

        private async void UcChild_KhoaHoc_PhanCong_Load(object sender, EventArgs e)
        {
            SetBusy(true, "Đang tải danh sách khóa học...");
            List<DTO.GPLX.KhoaHocGplx> khoaHoc;
            try
            {
                (khoaHoc, _allXe) = await Task.Run(() => (_DAO_Gplx.GetAllKhoaHoc(), _DAO.GetAllXeTapLai()));
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                SetBusy(false, "Không tải được danh sách khóa học.");
                ShowError("Không tải được danh sách khóa học", ex);
                return;
            }
            if (!IsAlive) return;

            cbb_KhoaHocDuKien.Properties.DataSource = khoaHoc;
            SetBusy(false, null);
            if (khoaHoc.Count > 0)
                cbb_KhoaHocDuKien.EditValue = khoaHoc[0].MaKH;
            else
                lbl_TongKet.Text = "Chưa có khóa học nào từ năm 2025.";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) { btn_Refresh_Click(this, EventArgs.Empty); return true; }
            if (keyData == (Keys.Control | Keys.S)) { btn_Save_Click(this, EventArgs.Empty); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Tải khóa học

        /// <summary>Mã khóa có 3 số cuối chẵn là lớp ngoài giờ, lẻ là trong giờ (giống thủ tục usp_KhoaHoc_GiaoVien_Insert).</summary>
        private static string LoaiLop(string maKH)
        {
            if (string.IsNullOrEmpty(maKH) || maKH.Length < 3 || !int.TryParse(maKH.Substring(maKH.Length - 3), out int so))
                return "Không xác định";
            return so % 2 == 0 ? "Ngoài giờ" : "Trong giờ";
        }

        private void cbb_KhoaHocDuKien_EditValueChanged(object sender, EventArgs e)
        {
            if (_suppressCourseChange) return;
            if (_dirty && !ConfirmDiscard())
            {
                _suppressCourseChange = true;
                cbb_KhoaHocDuKien.EditValue = _courseValue;
                _suppressCourseChange = false;
                return;
            }
            _courseValue = cbb_KhoaHocDuKien.EditValue;
            if (cbb_KhoaHocDuKien.EditValue is string maKH && maKH.Length > 0)
                _ = LoadCourseAsync(maKH);
        }

        private bool ConfirmDiscard() =>
            XtraMessageBox.Show($"Phân công khóa {_tenKH} có thay đổi chưa lưu. Bỏ các thay đổi này?", "Chưa lưu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

        private sealed class DuLieuKhoa
        {
            public DataRow KhoaHoc;
            public string LoaiLop;
            public List<GiaoVien> GvTrong, GvLop;
            public List<XeTapLai> XeTrong, XeB11;
            public List<Schedule> DaLuu;
        }

        private async Task LoadCourseAsync(string maKH)
        {
            int version = ++_loadVersion;
            SetBusy(true, "Đang tải phân công...");

            DuLieuKhoa d;
            try
            {
                d = await Task.Run(() =>
                {
                    string loaiLop = LoaiLop(maKH);
                    var r = new DuLieuKhoa
                    {
                        LoaiLop = loaiLop,
                        KhoaHoc = _DAO_Gplx.KhoaHocSelected(maKH),
                        GvTrong = _DAO.Get_GvTrong(loaiLop),
                        GvLop = _DAO.Get_GiaoVienByMaKH(maKH),
                        XeTrong = _DAO.Get_XeTapLaiTrong(loaiLop),
                        DaLuu = _DAO.GetKhoaHocDaPhanCong(maKH)
                    };
                    DateTime ngayTH = ((DateTime)r.KhoaHoc["NgayKG"]).AddDays(SoNgayDenThucHanh);
                    r.XeB11 = _DAO.Get_XeB11ConTrong(loaiLop, ngayTH);
                    return r;
                });
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _loadVersion) return;
                SetBusy(false, "Không tải được phân công của khóa.");
                ShowError("Không tải được phân công của khóa", ex);
                return;
            }
            if (!IsAlive || version != _loadVersion) return;

            ShowCourse(maKH, d);
            SetBusy(false, null);
        }

        private void ShowCourse(string maKH, DuLieuKhoa d)
        {
            DataRow kh = d.KhoaHoc;
            _maKH = maKH;
            _tenKH = Convert.ToString(kh["TenKH"]);
            _ngayKG = (DateTime)kh["NgayKG"];
            _ngayBG = (DateTime)kh["NgayBG"];
            int.TryParse(Convert.ToString(kh["TongSoHV"]), out _tongSoHV);
            DateTime ngayTH = _ngayKG.AddDays(SoNgayDenThucHanh);
            string hangDT = Convert.ToString(kh["HangDT"]);

            txt_MaKH.Text = Convert.ToString(kh["MaKH"]);
            txt_TenKH.Text = _tenKH;
            txt_HangDT.Text = hangDT;
            txt_LoaiLop.Text = d.LoaiLop;
            txt_NgayKG.Text = _ngayKG.ToString("dd/MM/yyyy");
            txt_NgayBG.Text = _ngayBG.ToString("dd/MM/yyyy");
            txt_SoHV.Text = _tongSoHV.ToString();
            txt_ThoiGianDT.Text = Convert.ToString(kh["ThoiGianDT"]);
            txt_NgayTH.Text = ngayTH.ToString("dd/MM/yyyy");
            memoEdit_GhiChu.Text = Convert.ToString(kh["GhiChu"]);

            // Danh sách xe để chọn: xe còn trống + xe khóa này đang dùng
            // (trước đây xe đã gán của chính khóa bị loại khỏi danh sách nên ô chọn xe hiện trống)
            var dangDung = new HashSet<int>(d.DaLuu.Where(s => s.IdXeTap.HasValue).Select(s => s.IdXeTap.Value));
            repo_Xe.DataSource = d.XeTrong
                .Concat(_allXe.Where(x => dangDung.Contains(x.VehicleID)))
                .GroupBy(x => x.VehicleID).Select(g => g.First())
                .OrderBy(x => x.BienSoXe)
                .ToList();

            // Giáo viên trong lớp theo phân công đã lưu (mỗi giáo viên 1 dòng; bản ghi trùng sẽ được dọn khi lưu)
            _daLuu = d.DaLuu;
            var gvTheoId = d.GvLop.GroupBy(g => g.GiaoVienID).ToDictionary(g => g.Key, g => g.First());
            var rows = d.DaLuu
                .GroupBy(s => s.GiaoVienId).Select(g => g.First())
                .Select(s => new PhanCongRow
                {
                    GiaoVien = gvTheoId.TryGetValue(s.GiaoVienId, out var gv) ? gv
                               : new GiaoVien { GiaoVienID = s.GiaoVienId, HoTenGV = s.GiaoVien ?? $"GV #{s.GiaoVienId}" },
                    IdXeTap = s.IdXeTap,
                    SoHV = s.SoHV ?? 0,
                    HieuLuc = s.NgayHL == DateTime.MinValue ? (DateTime?)null : s.NgayHL,
                    HetHieuLuc = s.NgayHetHL == DateTime.MinValue ? (DateTime?)null : s.NgayHetHL,
                    LichSTD = Lich(s.MaXeTapB11, s.TimeInB11, s.TimeOutB11),
                    LichCabin = Lich(s.Cabin, s.TimeInCabin, s.TimeOutCabin),
                    Saved = s
                })
                .ToList();
            _trongLop = new BindingList<PhanCongRow>(rows);
            gridControlRight.DataSource = _trongLop;

            // Giáo viên trống (bỏ người đã ở trong lớp: phân công đã hết hiệu lực vẫn hiện ở cả hai bên)
            var trongLop = new HashSet<int>(rows.Select(r => r.GiaoVienID));
            _choXep = new BindingList<GiaoVien>(d.GvTrong.Where(g => g.SuDung && !trongLop.Contains(g.GiaoVienID)).ToList());
            gridControlLeft.DataSource = _choXep;

            // Lịch xe STĐ (chỉ gợi ý: chức năng lưu đang phát triển). Khóa B.01 không học xe STĐ riêng.
            bool coSTD = !HangGplx.IsSoTuDong(hangDT);
            cbb_XeSTD.Properties.DataSource = d.XeB11;
            cbb_XeSTD.EditValue = null;
            date_STDStart.DateTime = ngayTH;
            date_STDEnd.DateTime = _ngayBG;
            foreach (Control c in new Control[] { lbl_STD, cbb_XeSTD, lbl_STDTu, date_STDStart, lbl_STDDen, date_STDEnd })
                c.Enabled = coSTD;

            gridViewLeft.ClearSelection();
            gridViewRight.ClearSelection();
            SetDirty(false);
        }

        private static string Lich(string ten, DateTime? tu, DateTime? den)
        {
            if (string.IsNullOrWhiteSpace(ten)) return "";
            if (!tu.HasValue && !den.HasValue) return ten;
            return $"{ten}  {tu:dd/MM}–{den:dd/MM}";
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maKH)) return;
            if (_dirty && !ConfirmDiscard()) return;
            _ = LoadCourseAsync(_maKH);
        }

        #endregion

        #region Chuyển giáo viên

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            var chon = SelectedOrFocused(gridViewLeft).Cast<GiaoVien>().ToList();
            if (chon.Count == 0) return;

            DateTime ngayTH = _ngayKG.AddDays(SoNgayDenThucHanh);
            foreach (var gv in chon)
            {
                _choXep.Remove(gv);
                // Giáo viên từng có trong khóa (đã lưu) thì dùng lại bản ghi cũ thay vì thêm mới
                var saved = _daLuu.FirstOrDefault(s => s.GiaoVienId == gv.GiaoVienID);
                _trongLop.Add(new PhanCongRow
                {
                    GiaoVien = gv,
                    IdXeTap = null,
                    SoHV = SoHVMacDinh,
                    HieuLuc = ngayTH,
                    HetHieuLuc = _ngayBG,
                    LichSTD = saved == null ? "" : Lich(saved.MaXeTapB11, saved.TimeInB11, saved.TimeOutB11),
                    LichCabin = saved == null ? "" : Lich(saved.Cabin, saved.TimeInCabin, saved.TimeOutCabin),
                    Saved = saved
                });
            }
            gridViewLeft.ClearSelection();
            SetDirty(true);

            // Chọn dòng vừa thêm và mở ô chọn xe
            int handle = gridViewRight.GetRowHandle(_trongLop.Count - 1);
            gridViewRight.FocusedRowHandle = handle;
            gridViewRight.FocusedColumn = colR_Xe;
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            gridViewRight.CloseEditor();
            var chon = SelectedOrFocused(gridViewRight).Cast<PhanCongRow>().ToList();
            if (chon.Count == 0) return;

            foreach (var row in chon)
            {
                _trongLop.Remove(row);
                _choXep.Add(row.GiaoVien);
            }
            gridViewRight.ClearSelection();
            SetDirty(true);
        }

        private void gridViewLeft_DoubleClick(object sender, EventArgs e)
        {
            var hit = gridViewLeft.CalcHitInfo(gridControlLeft.PointToClient(MousePosition));
            if (!hit.InRow || !gridViewLeft.IsDataRow(hit.RowHandle)) return;
            gridViewLeft.ClearSelection();
            gridViewLeft.FocusedRowHandle = hit.RowHandle;
            btnMoveRight_Click(sender, e);
        }

        /// <summary>Các dòng được tick; không tick dòng nào thì lấy dòng đang chọn.</summary>
        private static IEnumerable<object> SelectedOrFocused(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            var handles = view.GetSelectedRows().Where(view.IsDataRow).ToList();
            if (handles.Count == 0 && view.IsDataRow(view.FocusedRowHandle)) handles.Add(view.FocusedRowHandle);
            return handles.Select(view.GetRow).Where(r => r != null).ToList();
        }

        #endregion

        #region Sửa trên lưới

        // Một xe chỉ gán cho 1 giáo viên trong lớp; ngày hết hiệu lực phải sau ngày bắt đầu
        private void gridViewRight_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (!(gridViewRight.GetFocusedRow() is PhanCongRow row)) return;
            var col = gridViewRight.FocusedColumn;

            if (col == colR_Xe && e.Value is int xe)
            {
                var khac = _trongLop.FirstOrDefault(r => r != row && r.IdXeTap == xe);
                if (khac != null)
                {
                    e.Valid = false;
                    e.ErrorText = $"Xe {BienSo(xe)} đã gán cho {khac.HoTenGV}. Bỏ xe của giáo viên đó trước.";
                }
            }
            else if (col == colR_SoHV && e.Value != null && Convert.ToInt32(e.Value) < 0)
            {
                e.Valid = false;
                e.ErrorText = "Số học viên không được âm.";
            }
            else if (col == colR_HieuLuc && e.Value is DateTime tu && row.HetHieuLuc.HasValue && tu.Date > row.HetHieuLuc.Value.Date)
            {
                e.Valid = false;
                e.ErrorText = "Ngày bắt đầu hiệu lực phải trước ngày kết thúc.";
            }
            else if (col == colR_HetHieuLuc && e.Value is DateTime den && row.HieuLuc.HasValue && den.Date < row.HieuLuc.Value.Date)
            {
                e.Valid = false;
                e.ErrorText = "Ngày kết thúc hiệu lực phải sau ngày bắt đầu.";
            }
        }

        private void gridViewRight_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
            e.WindowCaption = "Giá trị không hợp lệ";
        }

        private void gridViewRight_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridViewRight.RefreshRow(e.RowHandle);
            SetDirty(true);
        }

        private void gridViewRight_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (!(gridViewRight.GetRow(e.RowHandle) is PhanCongRow row)) return;
            if (e.Column == colR_TrangThai)
            {
                if (row.Saved == null) { e.Appearance.ForeColor = UiTheme.Success; e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold; }
                else if (row.IsChanged) { e.Appearance.ForeColor = UiTheme.Warning; e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold; }
                else e.Appearance.ForeColor = UiTheme.TextMuted;
            }
            else if (e.Column == colR_Xe && !row.IdXeTap.HasValue)
            {
                e.Appearance.ForeColor = UiTheme.TextMuted;
                e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Italic;
            }
        }

        private void gridViewLeft_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewLeft.RowCount > 0 || string.IsNullOrEmpty(_maKH)) return;
            if (!string.IsNullOrEmpty(gridViewLeft.FindFilterText))
                UiTheme.DrawEmptyState(e, "Không tìm thấy", "Thử từ khóa khác.");
            else
                UiTheme.DrawEmptyState(e, "Không còn giáo viên trống", "Mọi giáo viên đều đang có lớp.");
        }

        private void gridViewRight_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewRight.RowCount > 0 || string.IsNullOrEmpty(_maKH)) return;
            UiTheme.DrawEmptyState(e, "Lớp chưa có giáo viên", "Tick giáo viên ở danh sách bên trái rồi bấm › (hoặc bấm đúp).");
        }

        private string BienSo(int? xe) => xe.HasValue ? _allXe.FirstOrDefault(x => x.VehicleID == xe.Value)?.BienSoXe ?? $"#{xe}" : "—";

        #endregion

        #region Lưu

        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maKH) || !btn_Save.Enabled) return;
            gridViewRight.CloseEditor();
            if (!gridViewRight.UpdateCurrentRow()) return;

            // Kiểm tra trước khi lưu
            var thieuNgay = _trongLop.Where(r => !r.HieuLuc.HasValue || !r.HetHieuLuc.HasValue).ToList();
            if (thieuNgay.Count > 0)
            {
                XtraMessageBox.Show("Chưa nhập thời gian hiệu lực cho: " + string.Join(", ", thieuNgay.Select(r => r.HoTenGV)),
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var trungXe = _trongLop.Where(r => r.IdXeTap.HasValue).GroupBy(r => r.IdXeTap).FirstOrDefault(g => g.Count() > 1);
            if (trungXe != null)
            {
                XtraMessageBox.Show($"Xe {BienSo(trungXe.Key)} đang gán cho nhiều giáo viên: {string.Join(", ", trungXe.Select(r => r.HoTenGV))}.",
                    "Trùng xe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var them = _trongLop.Where(r => r.Saved == null).ToList();
            var sua = _trongLop.Where(r => r.IsChanged).ToList();
            var giuLai = new HashSet<int>(_trongLop.Where(r => r.Saved != null).Select(r => r.Saved.Id));
            var xoa = _daLuu.Where(s => !giuLai.Contains(s.Id)).ToList();
            if (them.Count + sua.Count + xoa.Count == 0)
            {
                XtraMessageBox.Show("Không có thay đổi nào để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string msg = $"Lưu phân công khóa {_tenKH}:\n";
            if (them.Count > 0) msg += $"\n• Thêm {them.Count} giáo viên";
            if (sua.Count > 0) msg += $"\n• Cập nhật {sua.Count} giáo viên";
            if (xoa.Count > 0) msg += $"\n• Xóa {xoa.Count} phân công: " + string.Join(", ", xoa.Take(6).Select(s => s.GiaoVien ?? $"GV #{s.GiaoVienId}"))
                                       + (xoa.Count > 6 ? $" … (+{xoa.Count - 6})" : "");
            int chuaCoXe = _trongLop.Count(r => !r.IdXeTap.HasValue);
            if (chuaCoXe > 0) msg += $"\n\nLưu ý: {chuaCoXe} giáo viên chưa được gán xe (vẫn được lưu, gán xe sau).";
            if (XtraMessageBox.Show(msg, "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Mỗi giáo viên lưu với xe / số HV / hiệu lực của riêng mình
            // (trước đây lấy số HV và ngày của giáo viên đang chọn áp cho tất cả, và bỏ qua giáo viên chưa có xe)
            string maKH = _maKH;
            var lenhThem = them.Select(r => (r.GiaoVienID, r.IdXeTap, r.SoHV, r.HieuLuc.Value, r.HetHieuLuc.Value)).ToList();
            var lenhSua = sua.Select(r => (r.Saved.Id, r.GiaoVienID, r.IdXeTap, r.SoHV, r.HieuLuc.Value, r.HetHieuLuc.Value)).ToList();
            SetBusy(true, "Đang lưu phân công...");
            try
            {
                await Task.Run(() =>
                {
                    foreach (var s in xoa) _DAO.DeleteKhoaHocGiaoVien(s);
                    foreach (var (id, gv, xe, soHV, tu, den) in lenhSua) _DAO.UpdateKhoaHocGiaoVien(id, maKH, gv, xe, soHV, tu, den);
                    foreach (var (gv, xe, soHV, tu, den) in lenhThem) _DAO.InsertKhoaHocGiaoVien(maKH, gv, xe, soHV, tu, den);
                });
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                SetBusy(false, null);
                ShowError("Không lưu được phân công (một phần thay đổi có thể đã được lưu, bấm Làm mới để xem lại)", ex);
                return;
            }
            if (!IsAlive) return;

            _dirty = false;
            await LoadCourseAsync(maKH);
            XtraMessageBox.Show($"Đã lưu phân công khóa {_tenKH}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Trạng thái giao diện

        private void SetDirty(bool dirty)
        {
            _dirty = dirty;
            UpdateSummary();
        }

        private void SetBusy(bool busy, string message)
        {
            if (busy)
            {
                gridViewRight.ShowLoadingPanel();
                gridViewLeft.ShowLoadingPanel();
            }
            else
            {
                gridViewRight.HideLoadingPanel();
                gridViewLeft.HideLoadingPanel();
            }
            foreach (Control c in new Control[] { btn_Save, btn_Refresh, btn_Export, btnMoveLeft, btnMoveRight, cbb_KhoaHocDuKien })
                c.Enabled = !busy;
            if (message != null) lbl_TongKet.Text = message;
            if (!busy) UpdateSummary();
        }

        private void UpdateSummary()
        {
            int soGV = _trongLop.Count;
            int coXe = _trongLop.Count(r => r.IdXeTap.HasValue);
            int tongHV = _trongLop.Sum(r => r.SoHV);

            lbl_ChoXep.Text = $"Giáo viên chờ xếp lớp  <color=100,116,139><size=-1>({_choXep.Count})</size></color>";
            lbl_TrongLop.Text = $"Giáo viên trong lớp  <color=100,116,139><size=-1>({soGV})</size></color>"
                                + (_dirty ? "   <color=217,119,6><size=-1>● Có thay đổi chưa lưu</size></color>" : "");

            if (string.IsNullOrEmpty(_maKH)) return;
            string mauHV = _tongSoHV > 0 && tongHV != _tongSoHV ? "185,28,28" : "21,128,61";
            lbl_TongKet.Text = $"<b>{soGV}</b> giáo viên  ·  <b>{coXe}</b> xe"
                               + (soGV > coXe ? $"  ·  <color=217,119,6>{soGV - coXe} chưa có xe</color>" : "")
                               + $"<br>Đã chia <color={mauHV}><b>{tongHV}</b></color> / {_tongSoHV} học viên của khóa";
            btn_Save.Text = _dirty ? "Lưu phân công •" : "Lưu phân công";
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (_trongLop.Count == 0) return;
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = string.Join("_", $"PhanCong_{_tenKH}".Split(System.IO.Path.GetInvalidFileNameChars())) + ".xlsx"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    gridControlRight.ExportToXlsx(dialog.FileName, new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Process.Start(new ProcessStartInfo(dialog.FileName) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    ShowError("Không xuất được file", ex);
                }
            }
        }

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show($"{title}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
