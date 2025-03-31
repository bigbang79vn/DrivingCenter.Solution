using DevExpress.Export;
using DevExpress.XtraEditors;
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

            int stt = 0;

            double TongQuangDuong = 0;
            double TongThoiGian = 0;

            foreach (PhienHoc item in lstPhienHocTheoXe)
            {
                item.STT = stt + 1;
                stt++;
                TongQuangDuong = TongQuangDuong + item.QuangDuongPhien;
                TongThoiGian = TongThoiGian + item.dbTime;
            }

            ConvertListToDataTable convert = new ConvertListToDataTable();

            dt = convert.ToDataTable(lstPhienHocTheoXe);

            gridControl_BaoCaoTheoXe.DataSource = dt;

            lb_TongThoiGian.Text = Math.Round(TongThoiGian / 3600, 1).ToString() + " Giờ";
            lb_TongQuangDuong.Text = TongQuangDuong.ToString() + " Km";

            foreach (DataRow row in dt.Rows)
            {
                row["Time_LogIn"] = DateTime.Parse((row["Time_LogIn"].ToString())).ToString("HH:mm:ss dd/MM/yyyy");
                row["Time_LogOut"] = DateTime.Parse((row["Time_LogOut"].ToString())).ToString("HH:mm:ss dd/MM/yyyy");
            }
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
    }
}
