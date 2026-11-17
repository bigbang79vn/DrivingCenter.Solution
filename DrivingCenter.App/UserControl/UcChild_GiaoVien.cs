using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_GiaoVien : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly GiaoVienDatDAO _giaoVienDao = new GiaoVienDatDAO();
        private readonly GPLX_CDB_CSDT_DAO _gplx = new GPLX_CDB_CSDT_DAO();

        public UcChild_GiaoVien()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pnl_Header.Paint += UiTheme.PaintHeader;

            UiTheme.StylePrimary(btn_Sync);
            UiTheme.SetIcon(btn_Sync, "svgimages/icon%20builder/actions_reload.svg");
            UiTheme.SetIcon(btn_Export, "svgimages/export/exporttoxlsx.svg");
            UiTheme.SetIcon(btn_Refresh, "svgimages/icon%20builder/actions_refresh.svg");

            UiTheme.StyleGrid(gridView_GiaoVien, "Tìm theo mã, họ tên, số CMT/CCCD...");
            gridView_GiaoVien.RowHeight = 42;
        }

        private void UcChild_GiaoVien_Load(object sender, EventArgs e)
        {
            Get_DmGiaoVienDAT();
        }

        private void Get_DmGiaoVienDAT()
        {
            List<GiaoVien> data = _giaoVienDao.GetAllGV();
            gridControl_GiaoVien.DataSource = data;
            GridHelper.EnableRowNumbering(gridView_GiaoVien);

            int soHang = data.Select(x => x.HangGPLX).Distinct().Count();
            grp_List.Text = $"DANH SÁCH GIÁO VIÊN  ·  {data.Count} giáo viên  ·  {soHang} hạng GPLX";

            ShowDetail(gridView_GiaoVien.GetFocusedRow() as GiaoVien);
        }

        private void gridView_GiaoVien_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ShowDetail(gridView_GiaoVien.GetFocusedRow() as GiaoVien);
        }

        private void ShowDetail(GiaoVien gv)
        {
            if (gv == null)
            {
                lbl_HoTen.Text = "Chọn một giáo viên";
                lbl_MaGV.Text = "Mã GV: —";
                pic_AnhChup.EditValue = null;
                foreach (var label in new[] { val_NgaySinh, val_CCCD, val_HangGPLX, val_MaCNDT, val_HocVan })
                    label.Text = "—";
                return;
            }

            lbl_HoTen.Text = string.IsNullOrWhiteSpace(gv.HoTenGV) ? $"{gv.HoTenDem} {gv.TenGV}" : gv.HoTenGV;
            lbl_MaGV.Text = "Mã GV: " + gv.MaGV;
            pic_AnhChup.EditValue = gv.AnhChup;

            val_NgaySinh.Text = gv.NgaySinh == DateTime.MinValue ? "—" : gv.NgaySinh.ToString("dd/MM/yyyy");
            val_CCCD.Text = ValueOrDash(gv.SoCMT);
            val_HangGPLX.Text = ValueOrDash(gv.HangGPLX);
            val_MaCNDT.Text = ValueOrDash(gv.MaCNDT);
            val_HocVan.Text = ValueOrDash(gv.TrinhDoHocVan);
        }

        private static string ValueOrDash(string value) => string.IsNullOrWhiteSpace(value) ? "—" : value;

        private void grp_Detail_Paint(object sender, PaintEventArgs e)
        {
            UiTheme.DrawSection(e.Graphics, "THÔNG TIN CÁ NHÂN", 20, 202, 340);
            UiTheme.DrawSection(e.Graphics, "LƯU Ý", 20, 404, 340);
        }

        private void gridView_GiaoVien_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            if (gridView_GiaoVien.RowCount > 0) return;
            UiTheme.DrawEmptyState(e, "Chưa có giáo viên", "Bấm \"Đồng bộ từ GPLX\" để lấy danh sách giáo viên.");
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Get_DmGiaoVienDAT();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"DanhSachGiaoVien_{DateTime.Now:yyyyMMdd}.xlsx"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    gridView_GiaoVien.ExportToXlsx(dialog.FileName);
                    if (XtraMessageBox.Show("Xuất Excel thành công. Mở file ngay?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Không xuất được file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btn_Sync_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Đồng bộ lại danh sách giáo viên từ hệ thống GPLX?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btn_Sync.Enabled = false;
            string caption = btn_Sync.Text;
            btn_Sync.Text = "Đang đồng bộ...";
            try
            {
                int count = await Task.Run(() => Synchronize());
                Get_DmGiaoVienDAT();
                XtraMessageBox.Show($"Đã đồng bộ {count} giáo viên từ hệ thống GPLX.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Đồng bộ thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_Sync.Text = caption;
                btn_Sync.Enabled = true;
            }
        }

        private int Synchronize()
        {
            var giaoViens = _gplx.GetAllGv();

            int count = 0;
            foreach (var item in giaoViens)
            {
                if (_giaoVienDao.AddGiaoVien(item))
                    count++;
            }
            return count;
        }
    }
}
