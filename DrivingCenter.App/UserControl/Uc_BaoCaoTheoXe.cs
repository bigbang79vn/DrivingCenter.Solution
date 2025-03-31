using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_BaoCaoTheoXe : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_BaoCaoTheoXe()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();
        private string xetap;
        private void Uc_BaoCaoTheoXe_Load(object sender, EventArgs e)
        {
            LoadlstXe();
            dtk_From.DateTime = DateTime.Now;
            dtk_To.DateTime = DateTime.Now;
        }
        private void LoadlstXe()
        {
            List<XeTapLai> lst = DAT_DAO.Instance.GetLstXeTapLai();

            cbb_XeTapLai.Properties.DataSource = lst;
            cbb_XeTapLai.Properties.DisplayMember = "BienSoXe";
            cbb_XeTapLai.Properties.ValueMember = "MaXe";
            if (lst.Count > 0)
            {
                cbb_XeTapLai.EditValue = lst[0].MaXe;
            }
        }

        private void ViewInfo()
        {
            var selectedObject = cbb_XeTapLai.GetSelectedDataRow() as XeTapLai;

            xetap = selectedObject.BienSoXe;
            lb_BienSoXe.Text = selectedObject.BienSoXe;
            lb_HangXe.Text = selectedObject.HangGPLX;
            lb_ThoiGian.Text = "Từ " + dtk_From.DateTime.ToString("dd/MM/yyyy") + " Đến " + dtk_To.DateTime.ToString("dd/MM/yyyy");

            string from = dtk_From.DateTime.ToString("yyyyMMdd");
            string to = dtk_To.DateTime.ToString("yyyyMMdd");

            string bienso = selectedObject.MaXe;

            List<PhienHoc> lstPhienHocTheoXe = DAT_DAO.Instance.LstPhienHocTheoXe(bienso, from, to);

            gridControl_BaoCaoTheoXe.DataSource = lstPhienHocTheoXe;

        }

        private void btn_View_Click(object sender, EventArgs e)
        {
            ViewInfo();
        }


        private void btn_export_Click_1(object sender, EventArgs e)
        {
            string fileName = $"{xetap}.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridControl_BaoCaoTheoXe.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            var result = XtraMessageBox.Show("Xuất Excel thành công! Mở file", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private void gridView_BaoCaoTheoXe_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }
    }
}
