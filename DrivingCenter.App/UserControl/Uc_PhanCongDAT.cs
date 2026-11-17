using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.Helper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_PhanCongDAT : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly PhanCongDAO _phanCongDao = new PhanCongDAO();
        private DataTableCollection _sheets;
        private string _fileName;
        private List<DongPhanCong> _rows = new List<DongPhanCong>();
        private int _checkVersion;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public enum LoaiDong { HopLe, KhongDoi, Loi, DaApDung }

        /// <summary>Một dòng của sheet sau khi đối chiếu với dữ liệu DAT.</summary>
        public sealed class DongPhanCong
        {
            public int Dong { get; set; }               // số dòng trong Excel
            public string MaDK { get; set; }
            public string HocVien { get; set; }
            public string KhoaHoc { get; set; }
            public string GiaoVien { get; set; }        // tên giáo viên trong file
            public string BienSoXe { get; set; }        // biển số trong file
            public string ThayDoi { get; set; }
            public string TrangThai { get; set; }
            public LoaiDong Loai { get; set; }
            public int? GiaoVienID { get; set; }        // giáo viên khớp được (để gán)
            public string MaXe { get; set; }            // mã xe sẽ gán
        }

        public Uc_PhanCongDAT()
        {
            InitializeComponent();
            pnl_Header.Paint += UiTheme.PaintHeader;
            GridHelper.EnableRowNumbering(gridView_Assign, 45);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O)) { btn_Open_Click(this, EventArgs.Empty); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Mở file

        private async void btn_Open_Click(object sender, EventArgs e)
        {
            if (!btn_Open.Enabled) return;
            string path;
            using (var dialog = new OpenFileDialog { Filter = "Excel Workbook|*.xls;*.xlsx", Title = "Chọn file phân công" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                path = dialog.FileName;
            }

            SetBusy(true, "Đang đọc file...");
            DataTableCollection sheets;
            try
            {
                sheets = await Task.Run(() =>
                {
                    using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        return reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                        }).Tables;
                });
            }
            catch (IOException ex)
            {
                if (!IsAlive) return;
                SetBusy(false, null);
                ShowError("Không đọc được file (có thể đang mở trong Excel, hãy đóng file rồi thử lại)", ex);
                return;
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                SetBusy(false, null);
                ShowError("Không đọc được file Excel", ex);
                return;
            }
            if (!IsAlive) return;
            SetBusy(false, null);

            if (sheets.Count == 0)
            {
                XtraMessageBox.Show("File Excel không có sheet nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _sheets = sheets;
            _fileName = Path.GetFileName(path);
            cbb_KhoaHoc.Properties.Items.Clear();
            foreach (DataTable table in sheets)
                cbb_KhoaHoc.Properties.Items.Add(table.TableName);
            cbb_KhoaHoc.Enabled = true;
            if (cbb_KhoaHoc.SelectedIndex == 0)
                cbb_KhoaHoc_SelectedIndexChanged(cbb_KhoaHoc, EventArgs.Empty);
            else
                cbb_KhoaHoc.SelectedIndex = 0;
        }

        private void cbb_KhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_sheets == null || !(cbb_KhoaHoc.SelectedItem is string sheet)) return;
            _ = CheckSheetAsync(_sheets[sheet]);
        }

        #endregion

        #region Kiểm tra

        private static string Norm(string s) =>
            Regex.Replace((s ?? "").Normalize(NormalizationForm.FormC).Trim(), @"\s+", " ").ToUpperInvariant();

        /// <summary>Biển số / mã xe chỉ giữ chữ và số (47A-500.14 → 47A50014), giống cách tạo MaXe.</summary>
        private static string NormXe(string s) => Regex.Replace(s ?? "", "[^0-9A-Za-z]", "").ToUpperInvariant();

        private static DataColumn FindColumn(DataTable t, string name) =>
            t.Columns.Cast<DataColumn>().FirstOrDefault(c => string.Equals(c.ColumnName.Trim(), name, StringComparison.OrdinalIgnoreCase));

        private async Task CheckSheetAsync(DataTable table)
        {
            int version = ++_checkVersion;
            var cGV = FindColumn(table, "GiaoVien");
            var cMa = FindColumn(table, "MaDK");
            var cXe = FindColumn(table, "BienSoXe");
            var thieu = new[] { ("GiaoVien", cGV), ("MaDK", cMa), ("BienSoXe", cXe) }.Where(x => x.Item2 == null).Select(x => x.Item1).ToList();
            if (thieu.Count > 0)
            {
                _rows = new List<DongPhanCong>();
                grdCtrl_Assign.DataSource = _rows;
                btn_Assign.Enabled = false;
                lbl_Summary.Text = $"<color=185,28,28>Sheet \"{table.TableName}\" thiếu cột: <b>{string.Join(", ", thieu)}</b>.</color> "
                                   + "Dòng đầu của sheet phải là tiêu đề GiaoVien, MaDK, BienSoXe.";
                return;
            }

            // Đọc dòng của sheet (bỏ dòng trống hoàn toàn); dòng 1 là tiêu đề nên dữ liệu bắt đầu từ dòng 2
            var rows = table.Rows.Cast<DataRow>().Select((r, i) => new DongPhanCong
            {
                Dong = i + 2,
                MaDK = Convert.ToString(r[cMa]).Trim(),
                GiaoVien = Regex.Replace(Convert.ToString(r[cGV]).Trim(), @"\s+", " "),
                BienSoXe = Convert.ToString(r[cXe]).Trim()
            }).Where(r => r.MaDK.Length + r.GiaoVien.Length + r.BienSoXe.Length > 0).ToList();

            SetBusy(true, $"Đang kiểm tra {rows.Count} dòng của sheet {table.TableName}...");
            try
            {
                await Task.Run(() => Validate(rows));
            }
            catch (Exception ex)
            {
                if (!IsAlive || version != _checkVersion) return;
                SetBusy(false, null);
                ShowError("Không kiểm tra được dữ liệu với hệ thống DAT", ex);
                return;
            }
            if (!IsAlive || version != _checkVersion) return;

            _rows = rows;
            grdCtrl_Assign.DataSource = _rows;
            SetBusy(false, null);
            UpdateSummary();
        }

        /// <summary>Đối chiếu từng dòng với học viên, giáo viên, xe trong DAT (chạy nền).</summary>
        private void Validate(List<DongPhanCong> rows)
        {
            var hocVien = _phanCongDao.GetNguoiLxByMaDK(rows.Where(r => r.MaDK.Length > 0).Select(r => r.MaDK))
                .GroupBy(h => h.MaDK, StringComparer.OrdinalIgnoreCase).ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
            var giaoVien = _phanCongDao.GetGiaoVienTen().GroupBy(g => Norm(g.HoTen)).ToDictionary(g => g.Key, g => g.ToList());
            var xe = new Dictionary<string, XeTapLaiMa>();
            foreach (var x in _phanCongDao.GetXeTapLaiMa())
            {
                xe[NormXe(x.MaXe)] = x;
                if (!string.IsNullOrEmpty(x.BienSoXe)) xe[NormXe(x.BienSoXe)] = x;
            }
            var dongTheoMa = rows.Where(r => r.MaDK.Length > 0)
                .GroupBy(r => r.MaDK, StringComparer.OrdinalIgnoreCase).Where(g => g.Count() > 1)
                .ToDictionary(g => g.Key, g => g.Select(r => r.Dong).ToList(), StringComparer.OrdinalIgnoreCase);

            foreach (var r in rows)
            {
                var loi = new List<string>();
                hocVien.TryGetValue(r.MaDK, out var hv);
                if (r.MaDK.Length == 0) loi.Add("Thiếu mã ĐK");
                else if (hv == null) loi.Add("Không có học viên này trong DAT");
                else
                {
                    r.HocVien = hv.HoVaTen;
                    r.KhoaHoc = hv.TenKH;
                }
                if (dongTheoMa.TryGetValue(r.MaDK, out var trung))
                    loi.Add("Mã ĐK lặp ở dòng " + string.Join(", ", trung.Where(d => d != r.Dong)));

                // Giáo viên: khớp đúng cả họ tên (trước đây LIKE '%họ đệm%' + TOP 1 nên dễ khớp nhầm người)
                if (r.GiaoVien.Length == 0) loi.Add("Thiếu tên giáo viên");
                else if (!giaoVien.TryGetValue(Norm(r.GiaoVien), out var gv)) loi.Add($"Không tìm thấy giáo viên \"{r.GiaoVien}\"");
                else if (gv.Count > 1) loi.Add($"Có {gv.Count} giáo viên trùng tên \"{r.GiaoVien}\"");
                else r.GiaoVienID = gv[0].GiaoVienID;

                // Xe: để trống thì giữ xe hiện tại
                if (r.BienSoXe.Length == 0) r.MaXe = hv?.MaXeTap;
                else if (xe.TryGetValue(NormXe(r.BienSoXe), out var x)) r.MaXe = x.MaXe;
                else loi.Add($"Không tìm thấy xe {r.BienSoXe}");

                if (loi.Count > 0)
                {
                    r.Loai = LoaiDong.Loi;
                    r.TrangThai = string.Join("; ", loi);
                    continue;
                }

                var doi = new List<string>();
                if (hv.GiaoVienID != r.GiaoVienID)
                    doi.Add($"GV: {(string.IsNullOrEmpty(hv.TenGiaoVien) ? "(chưa có)" : hv.TenGiaoVien)} → {r.GiaoVien}");
                if (!string.Equals(NormXe(hv.MaXeTap), NormXe(r.MaXe), StringComparison.Ordinal))
                    doi.Add($"Xe: {(string.IsNullOrEmpty(hv.MaXeTap) ? "(chưa có)" : hv.MaXeTap)} → {r.MaXe}");
                r.ThayDoi = string.Join("   ·   ", doi);
                r.Loai = doi.Count > 0 ? LoaiDong.HopLe : LoaiDong.KhongDoi;
                r.TrangThai = doi.Count > 0 ? "Sẵn sàng áp dụng" : "Không đổi";
            }
        }

        #endregion

        #region Áp dụng

        private async void btn_Assign_Click(object sender, EventArgs e)
        {
            var apDung = _rows.Where(r => r.Loai == LoaiDong.HopLe).ToList();
            if (apDung.Count == 0) return;
            int loi = _rows.Count(r => r.Loai == LoaiDong.Loi);
            int khongDoi = _rows.Count(r => r.Loai == LoaiDong.KhongDoi);

            string msg = $"Áp dụng phân công cho {apDung.Count} học viên của sheet \"{cbb_KhoaHoc.SelectedItem}\"?";
            if (khongDoi > 0) msg += $"\n\n• {khongDoi} dòng không đổi sẽ được bỏ qua.";
            if (loi > 0) msg += $"\n• {loi} dòng lỗi sẽ KHÔNG được áp dụng (bật \"Chỉ hiện dòng cần xem lại\" để xem).";
            if (XtraMessageBox.Show(msg, "Xác nhận phân công", MessageBoxButtons.YesNo,
                    loi > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            SetBusy(true, $"Đang phân công {apDung.Count} học viên...");
            int ok = 0, fail = 0;
            await Task.Run(() =>
            {
                foreach (var r in apDung)
                {
                    try
                    {
                        _phanCongDao.AssignGiaoVienXe(r.MaDK, r.GiaoVienID.Value, r.MaXe);
                        r.Loai = LoaiDong.DaApDung;
                        r.TrangThai = "Đã áp dụng";
                        ok++;
                    }
                    catch (Exception ex)
                    {
                        r.Loai = LoaiDong.Loi;
                        r.TrangThai = "Lỗi khi lưu: " + ex.Message;
                        fail++;
                    }
                }
            });
            if (!IsAlive) return;

            SetBusy(false, null);
            gridView_Assign.RefreshData();
            UpdateSummary();
            XtraMessageBox.Show($"Đã phân công {ok} học viên." + (fail > 0 ? $"\n{fail} học viên lưu lỗi, xem cột Kiểm tra." : ""),
                "Hoàn tất", MessageBoxButtons.OK, fail > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        #endregion

        #region Giao diện

        private void UpdateSummary()
        {
            int hopLe = _rows.Count(r => r.Loai == LoaiDong.HopLe);
            int loi = _rows.Count(r => r.Loai == LoaiDong.Loi);
            int khongDoi = _rows.Count(r => r.Loai == LoaiDong.KhongDoi);
            int daApDung = _rows.Count(r => r.Loai == LoaiDong.DaApDung);

            lbl_Summary.Text = $"<b>{_fileName}</b>  ·  sheet <b>{cbb_KhoaHoc.SelectedItem}</b>  ·  {_rows.Count} dòng:  "
                + $"<color=21,128,61><b>{hopLe}</b> sẵn sàng</color>"
                + (daApDung > 0 ? $"  ·  <color=78,109,156><b>{daApDung}</b> đã áp dụng</color>" : "")
                + $"  ·  {khongDoi} không đổi"
                + (loi > 0 ? $"  ·  <color=185,28,28><b>{loi}</b> lỗi</color>" : "");
            btn_Assign.Enabled = hopLe > 0;
            btn_Assign.Text = hopLe > 0 ? $"Áp dụng ({hopLe})" : "Áp dụng phân công";
        }

        private void SetBusy(bool busy, string message)
        {
            if (busy) gridView_Assign.ShowLoadingPanel(); else gridView_Assign.HideLoadingPanel();
            btn_Open.Enabled = !busy;
            cbb_KhoaHoc.Enabled = !busy && _sheets != null;
            if (busy) btn_Assign.Enabled = false;
            if (message != null) lbl_Summary.Text = message;
        }

        private void chk_ChiLoi_CheckedChanged(object sender, EventArgs e) => gridView_Assign.RefreshData();

        private void gridView_Assign_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if (!chk_ChiLoi.Checked || e.ListSourceRow < 0 || e.ListSourceRow >= _rows.Count) return;
            e.Visible = _rows[e.ListSourceRow].Loai == LoaiDong.Loi;
            e.Handled = true;
        }

        private void gridView_Assign_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column != colTrangThai || !(gridView_Assign.GetRow(e.RowHandle) is DongPhanCong r)) return;
            switch (r.Loai)
            {
                case LoaiDong.HopLe: e.Appearance.ForeColor = UiTheme.Success; break;
                case LoaiDong.Loi: e.Appearance.ForeColor = UiTheme.DangerDark; break;
                case LoaiDong.DaApDung: e.Appearance.ForeColor = UiTheme.Brand; break;
                default: e.Appearance.ForeColor = UiTheme.TextMuted; e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Regular; break;
            }
        }

        private void gridView_Assign_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridView_Assign.RowCount > 0) return;
            if (_sheets == null)
                UiTheme.DrawEmptyState(e, "Chưa mở file phân công", "Bấm \"Mở file Excel...\" (Ctrl+O). Mỗi sheet là một khóa với cột GiaoVien, MaDK, BienSoXe.");
            else if (chk_ChiLoi.Checked)
                UiTheme.DrawEmptyState(e, "Không có dòng lỗi", "Tất cả các dòng của sheet đều hợp lệ.");
            else
                UiTheme.DrawEmptyState(e, "Sheet không có dữ liệu", "Chọn sheet khác hoặc kiểm tra lại file.");
        }

        private void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show($"{title}.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
