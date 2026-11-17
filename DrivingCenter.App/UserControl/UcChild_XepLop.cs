using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DrivingCenter.App.Common;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    /// <summary>
    /// Xếp học viên từ khóa dự bị vào lớp (và trả về). Mỗi lần chuyển được lưu ngay vào CSDL GPLX:
    /// cấp số hồ sơ tiếp theo của lớp và chuyển ảnh chân dung sang thư mục lớp. Bố cục nằm trong Designer.
    /// </summary>
    public partial class UcChild_XepLop : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly GPLX_DAO _DAO = new GPLX_DAO();

        private BindingList<NguoiLX> _choXep = new BindingList<NguoiLX>();
        private BindingList<NguoiLX> _trongLop = new BindingList<NguoiLX>();
        private KhoaHocGplx _lop;
        private int _choVersion, _lopVersion, _anhVersion;
        private string _hoSoMaDK;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public UcChild_XepLop()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
            gridControlLeft.DataSource = _choXep;
            gridControlRight.DataSource = _trongLop;
            Disposed += (s, e) => picture_AnhChanDung.Image?.Dispose();
        }

        private async void UcChild_XepLop_Load(object sender, EventArgs e)
        {
            SetBusy(true);
            List<KhoaHocGplx> lop;
            try
            {
                lop = await Task.Run(() => _DAO.GetlstKh());
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                SetBusy(false);
                ShowError("Không tải được danh sách lớp", ex);
                return;
            }
            if (!IsAlive) return;

            // Khóa dự bị là nơi chứa học viên chờ, không phải lớp để xếp vào
            lop = lop.Where(k => k.MaKH != GPLX_DAO.MaKhoaDuBi).ToList();
            SlookUpEdit_KhoaHoc.Properties.DataSource = lop;
            SetBusy(false);

            _ = LoadChoXepAsync();
            if (lop.Count > 0)
                SlookUpEdit_KhoaHoc.EditValue = lop.OrderByDescending(k => k.NgayKG).First().MaKH;
            else
                UpdateHeaders();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) { btn_Refresh_Click(this, EventArgs.Empty); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Tải danh sách

        private async Task LoadChoXepAsync()
        {
            int version = ++_choVersion;
            gridViewLeft.ShowLoadingPanel();
            List<NguoiLX> lst;
            try
            {
                lst = await Task.Run(() => _DAO.Get_lstNguoiLxByMaKh(GPLX_DAO.MaKhoaDuBi));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _choVersion) return;
                gridViewLeft.HideLoadingPanel();
                ShowError("Không tải được danh sách học viên chờ", ex);
                return;
            }
            if (!IsAlive || version != _choVersion) return;

            _choXep = new BindingList<NguoiLX>(lst);
            gridControlLeft.DataSource = _choXep;
            gridViewLeft.HideLoadingPanel();
            gridViewLeft.ClearSelection();
            UpdateHeaders();
        }

        private async Task LoadLopAsync()
        {
            if (_lop == null) return;
            int version = ++_lopVersion;
            string maKH = _lop.MaKH;
            gridViewRight.ShowLoadingPanel();
            List<NguoiLX> lst;
            try
            {
                lst = await Task.Run(() => _DAO.Get_lstNguoiLxByMaKh(maKH));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _lopVersion) return;
                gridViewRight.HideLoadingPanel();
                ShowError("Không tải được danh sách học viên của lớp", ex);
                return;
            }
            if (!IsAlive || version != _lopVersion) return;

            _trongLop = new BindingList<NguoiLX>(lst);
            gridControlRight.DataSource = _trongLop;
            gridViewRight.HideLoadingPanel();
            gridViewRight.ClearSelection();
            UpdateHeaders();
        }

        private void SlookUpEdit_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            _lop = SlookUpEdit_KhoaHoc.GetSelectedDataRow() as KhoaHocGplx;
            UpdateHeaders();
            _ = LoadLopAsync();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (!btn_Refresh.Enabled) return;
            _ = LoadChoXepAsync();
            _ = LoadLopAsync();
        }

        private void UpdateHeaders()
        {
            lbl_ChoXep.Text = $"Học viên chờ xếp lớp  <color=100,116,139><size=-1>({_choXep.Count} · khóa dự bị {GPLX_DAO.MaKhoaDuBi})</size></color>";
            if (_lop == null)
            {
                lbl_TrongLop.Text = "Học viên trong lớp";
                return;
            }

            string siSo = _lop.TongSoHV > 0
                ? $"{_trongLop.Count}/{_lop.TongSoHV}" + (_trongLop.Count > _lop.TongSoHV ? " <color=185,28,28>vượt sĩ số</color>" : "")
                : _trongLop.Count.ToString();
            string ngay = _lop.NgayKG == DateTime.MinValue ? "" : $" · KG {_lop.NgayKG:dd/MM/yyyy}" + (_lop.NgayBG == DateTime.MinValue ? "" : $" – BG {_lop.NgayBG:dd/MM/yyyy}");
            lbl_TrongLop.Text = $"Lớp {_lop.TenKH}  <color=100,116,139><size=-1>(hạng {_lop.HangDT}{ngay} · sĩ số {siSo})</size></color>";
        }

        #endregion

        #region Chuyển học viên

        private async void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (_lop == null)
            {
                XtraMessageBox.Show("Chọn lớp cần xếp học viên vào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var chon = SelectedOrFocused(gridViewLeft);
            if (chon.Count == 0) return;

            string msg = $"Xếp {chon.Count} học viên vào lớp {_lop.TenKH} ({_lop.MaKH})?\n\n"
                         + DanhSach(chon)
                         + "\n\nMỗi học viên được cấp số hồ sơ mới của lớp và ảnh chân dung được chuyển sang thư mục lớp.";
            // So mã hạng GPLX của hồ sơ với mã hạng GPLX của khóa (B1 / B11 / C1), không so với hạng đào tạo (B / B.01)
            var khacHang = chon.Where(hv => !CungHang(hv.HangGPLX, _lop.HangGPLX)).ToList();
            if (khacHang.Count > 0)
                msg += $"\n\n⚠ {khacHang.Count} học viên có hạng khác lớp (lớp hạng {_lop.HangDT}, mã {_lop.HangGPLX}): "
                       + string.Join(", ", khacHang.Take(5).Select(h => $"{h.HoVaTen} ({h.HangGPLX})"));
            if (_lop.TongSoHV > 0 && _trongLop.Count + chon.Count > _lop.TongSoHV)
                msg += $"\n\n⚠ Lớp sẽ có {_trongLop.Count + chon.Count} học viên, vượt sĩ số {_lop.TongSoHV}.";

            if (XtraMessageBox.Show(msg, "Xác nhận xếp lớp", MessageBoxButtons.YesNo,
                    khacHang.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            await MoveAsync(chon, _lop.MaKH, $"lớp {_lop.TenKH}");
        }

        private async void btnMoveLeft_Click(object sender, EventArgs e)
        {
            var chon = SelectedOrFocused(gridViewRight);
            if (chon.Count == 0) return;

            if (XtraMessageBox.Show($"Trả {chon.Count} học viên của lớp {_lop?.TenKH} về khóa dự bị?\n\n{DanhSach(chon)}"
                    + "\n\nSố hồ sơ trong lớp hiện tại sẽ bị thay bằng số hồ sơ ở khóa dự bị.",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            await MoveAsync(chon, GPLX_DAO.MaKhoaDuBi, "khóa dự bị");
        }

        /// <summary>Chuyển lần lượt từng học viên (số hồ sơ cấp tăng dần nên không chạy song song).</summary>
        private async Task MoveAsync(List<NguoiLX> hocVien, string maKH, string noiDen)
        {
            SetBusy(true);
            var canhBao = new List<string>();
            int xong = 0;
            Exception loi = null;
            try
            {
                await Task.Run(() =>
                {
                    foreach (var hv in hocVien)
                    {
                        string cb = _DAO.UpdateNguoiLxToKhoaHoc(hv, maKH);
                        if (cb != null) canhBao.Add($"{hv.HoVaTen}: {cb}");
                        xong++;
                    }
                });
            }
            catch (Exception ex)
            {
                loi = ex;
            }
            if (!IsAlive) return;
            SetBusy(false);

            await Task.WhenAll(LoadChoXepAsync(), LoadLopAsync());
            if (!IsAlive) return;

            if (loi != null)
                ShowError($"Đã chuyển {xong}/{hocVien.Count} học viên sang {noiDen}, dừng ở {hocVien[xong].HoVaTen}", loi);
            else if (canhBao.Count > 0)
                XtraMessageBox.Show($"Đã chuyển {xong} học viên sang {noiDen}.\n\nLưu ý về ảnh:\n" + string.Join("\n", canhBao.Take(10)),
                    "Hoàn tất (có cảnh báo)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static string DanhSach(List<NguoiLX> hv) =>
            string.Join("\n", hv.Take(8).Select(h => $"• {h.HoVaTen} – {h.MaDK}")) + (hv.Count > 8 ? $"\n• … và {hv.Count - 8} học viên khác" : "");

        /// <summary>So mã hạng GPLX (bỏ dấu chấm, không phân biệt hoa thường); thiếu thông tin thì coi như khớp.</summary>
        private static bool CungHang(string hangHV, string hangLop)
        {
            if (string.IsNullOrWhiteSpace(hangHV) || string.IsNullOrWhiteSpace(hangLop)) return true;
            return HangGplx.Same(hangHV, hangLop);
        }

        private void gridViewLeft_DoubleClick(object sender, EventArgs e)
        {
            if (FocusHitRow(gridViewLeft, gridControlLeft)) btnMoveRight_Click(sender, e);
        }

        private void gridViewRight_DoubleClick(object sender, EventArgs e)
        {
            if (FocusHitRow(gridViewRight, gridControlRight)) btnMoveLeft_Click(sender, e);
        }

        private bool FocusHitRow(DevExpress.XtraGrid.Views.Grid.GridView view, DevExpress.XtraGrid.GridControl grid)
        {
            var hit = view.CalcHitInfo(grid.PointToClient(MousePosition));
            if (!hit.InRow || !view.IsDataRow(hit.RowHandle)) return false;
            view.ClearSelection();
            view.FocusedRowHandle = hit.RowHandle;
            return true;
        }

        /// <summary>Các dòng được tick; không tick dòng nào thì lấy dòng đang chọn.</summary>
        private static List<NguoiLX> SelectedOrFocused(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            var handles = view.GetSelectedRows().Where(view.IsDataRow).ToList();
            if (handles.Count == 0 && view.IsDataRow(view.FocusedRowHandle)) handles.Add(view.FocusedRowHandle);
            return handles.Select(h => view.GetRow(h) as NguoiLX).Where(r => r != null).ToList();
        }

        #endregion

        #region Hồ sơ học viên (khung trái)

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (view.GridControl.ContainsFocus || _hoSoMaDK == null)
                ShowHoSo(view.GetFocusedRow() as NguoiLX);
        }

        private void gridControl_Enter(object sender, EventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)((DevExpress.XtraGrid.GridControl)sender).MainView;
            ShowHoSo(view.GetFocusedRow() as NguoiLX);
        }

        private void ShowHoSo(NguoiLX hv)
        {
            if (hv == null) return;
            _hoSoMaDK = hv.MaDK;
            lb_HoTen.Text = hv.HoVaTen;
            lb_MaDK.Text = hv.MaDK;
            lb_SoHS.Text = string.IsNullOrEmpty(hv.SoHoSo) ? "Số hồ sơ: —" : $"Số hồ sơ: <b>{hv.SoHoSo}</b>";

            string Dong(string nhan, string giaTri) =>
                string.IsNullOrWhiteSpace(giaTri) ? "" : $"<color=100,116,139>{nhan}</color><br>{giaTri.Replace("<", "").Replace(">", "")}<br><br>";
            string ngaySinh = hv.NgaySinhDateTime == DateTime.MinValue ? hv.NgaySinh : hv.NgaySinhDateTime.ToString("dd/MM/yyyy");
            string cccd = hv.SoCMT + (string.IsNullOrEmpty(hv.NgayCapCMT) ? "" : $" (cấp {hv.NgayCapCMT})");
            bool coSo(string s) => !string.IsNullOrWhiteSpace(s) && s.Trim() != "0";
            string nangHang = !coSo(hv.SoNamLX) && !coSo(hv.SoKmLXAnToan) ? ""
                : $"{hv.SoNamLX} năm · {hv.SoKmLXAnToan} km an toàn";
            lb_ChiTiet.Text = Dong("Ngày sinh · Giới tính", $"{ngaySinh} · {hv.GioiTinh}")
                            + Dong("Số CMT/CCCD", cccd)
                            + Dong("Quốc tịch", hv.MaQuocTich)
                            + Dong("Hạng đăng ký", hv.HangGPLX)
                            + Dong("Tên in trên GPLX", hv.HoVaTenIn)
                            + Dong("Nơi thường trú", hv.NoiTT)
                            + Dong("Nơi cư trú", hv.NoiCT)
                            + Dong("Dành cho nâng hạng", nangHang)
                            + Dong("Ghi chú", hv.GhiChu);
            _ = LoadAnhAsync(hv.DuongDanAnh);
        }

        /// <summary>Ảnh nằm trên thư mục mạng: đọc nền, chép ra bộ nhớ để không khóa file (file còn bị chuyển khi xếp lớp).</summary>
        private async Task LoadAnhAsync(string path)
        {
            int version = ++_anhVersion;
            Image img = null;
            if (!string.IsNullOrEmpty(path))
            {
                img = await Task.Run(() =>
                {
                    try
                    {
                        if (!File.Exists(path)) return null;
                        byte[] bytes = File.ReadAllBytes(path);
                        try
                        {
                            using (var ms = new MemoryStream(bytes))
                            using (var src = Image.FromStream(ms))
                                return (Image)new Bitmap(src);
                        }
                        catch (ArgumentException)
                        {
                            // Ảnh hồ sơ thường là JPEG 2000 (.jp2), GDI+ không đọc được: đổi sang PNG bằng ImageMagick
                            using (var magick = new ImageMagick.MagickImage(bytes) { Format = ImageMagick.MagickFormat.Png })
                            using (var ms = new MemoryStream(magick.ToByteArray()))
                            using (var src = Image.FromStream(ms))
                                return (Image)new Bitmap(src);
                        }
                    }
                    catch { return null; }
                });
            }
            if (!IsAlive || version != _anhVersion) { img?.Dispose(); return; }
            var old = picture_AnhChanDung.Image;
            picture_AnhChanDung.Image = img;
            old?.Dispose();
        }

        #endregion

        #region Giao diện

        private void SetBusy(bool busy)
        {
            foreach (Control c in new Control[] { btnMoveLeft, btnMoveRight, btn_Refresh, btn_Export, SlookUpEdit_KhoaHoc })
                c.Enabled = !busy;
            if (busy) { gridViewLeft.ShowLoadingPanel(); gridViewRight.ShowLoadingPanel(); }
            else { gridViewLeft.HideLoadingPanel(); gridViewRight.HideLoadingPanel(); }
        }

        private void gridViewLeft_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewLeft.RowCount > 0) return;
            if (!string.IsNullOrEmpty(gridViewLeft.FindFilterText))
                UiTheme.DrawEmptyState(e, "Không tìm thấy", "Thử từ khóa khác.");
            else
                UiTheme.DrawEmptyState(e, "Không có học viên chờ", $"Khóa dự bị {GPLX_DAO.MaKhoaDuBi} hiện chưa có học viên.");
        }

        private void gridViewRight_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewRight.RowCount > 0 || _lop == null) return;
            if (!string.IsNullOrEmpty(gridViewRight.FindFilterText))
                UiTheme.DrawEmptyState(e, "Không tìm thấy", "Thử từ khóa khác.");
            else
                UiTheme.DrawEmptyState(e, "Lớp chưa có học viên", "Tick học viên ở danh sách chờ rồi bấm › (hoặc bấm đúp).");
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (_lop == null || _trongLop.Count == 0) return;
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = string.Join("_", $"DanhSachLop_{_lop.TenKH}".Split(Path.GetInvalidFileNameChars())) + ".xlsx"
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
