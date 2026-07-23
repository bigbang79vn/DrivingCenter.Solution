using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_GPLX : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly GPLX_DAO _gplx_Dao = new GPLX_DAO();
        private readonly GPLX_CDB_CSDT_DAO _DAO = new GPLX_CDB_CSDT_DAO();
        public UcChild_GPLX()
        {
            InitializeComponent();
        }

        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private KhoaHoc kh;
        public List<string> lstMaDK;
        private DataTable dt;
        public DataTableCollection tableCollection;

        private void Uc_GPLX_Load(object sender, EventArgs e)
        {
            tblLayout_ImportData.Dock = DockStyle.Fill;
            tblLayout_UpdateResult.Dock = DockStyle.Fill;
            grd_Certificate.Dock = DockStyle.Fill;

            dtp_NgayQD.DateTime = DateTime.Now;

            LoadKH();
            LoadlstStudentInReportCNTN();
        }

        #region Import Data

        private void LoadKH()
        {
            List<KhoaHoc> lst = _DAO.GetlstKh();
            cbx_KhoaHoc.Properties.DataSource = lst;
            cbx_KhoaHoc.Properties.DisplayMember = "TenKH";
            cbx_KhoaHoc.Properties.ValueMember = "MaKH";

            if (lst.Count > 0)
            {
                cbx_KhoaHoc.EditValue = lst[0].MaKH;
            }
        }

        public string MaDK()
        {
            //Delay(0.5);
            string prefix = _DAO.GenerateMaDK();
            return prefix;
        }
        public string ConvertNgaySinh(string birthday)
        {
            if (DateTime.TryParse(birthday, CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out DateTime ngaysinh))
            {
                return ngaysinh.ToString("yyyyMMdd");
            }
            return string.Empty;
        }
        private void Delay(double delay)
        {
            double delayTime = 0;
            while (delayTime < delay)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delayTime++;
            }
        }
        private void btn_ReadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;

                    try
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                                cb_SheetDataImport.Properties.Items.Clear();

                                //XtraMessageBox.Show("Chọn Sheet chứa thông tin học viên cần import", "Thông báo", MessageBoxButtons.OK);

                                foreach (DataTable table in tableCollection)
                                {
                                    cb_SheetDataImport.Properties.Items.Add(table.TableName);
                                }

                                cb_SheetDataImport.EditValue = tableCollection[0].TableName;
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        XtraMessageBox.Show($"Không thể đọc file vì đang được sử dụng bởi ứng dụng khác.\nVui lòng đóng file Excel và thử lại.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cb_SheetDataImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[cb_SheetDataImport.SelectedItem.ToString()];
            grd_ImportData.DataSource = dt;
            GridHelper.EnableRowNumbering(gridView_ImportData);
        }

        private void cbx_KhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMaKH = cbx_KhoaHoc.EditValue.ToString();
            kh = _DAO.GetlstKh().FirstOrDefault(x => x.MaKH == selectedMaKH);

            txtHangDaoTao.Text = kh.HangDT;
        }

        private void btn_Import_Click_1(object sender, EventArgs e)
        {
            if (cb_SheetDataImport.SelectedItem == null)
            {
                XtraMessageBox.Show(string.Format("Bạn chưa chọn Sheet chứa Data!"));
                cb_SheetDataImport.Focus();
            }
            else
            {
                lbStatus.Text = "Running...";
                DataTable dt = tableCollection[cb_SheetDataImport.SelectedItem.ToString()];

                List<NguoiLxDto> lstNguoiLX = new List<NguoiLxDto>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Tạo mới NguoiLX
                        NguoiLxDto nlx = new NguoiLxDto();

                        nlx.MaKH = kh.MaKH;
                        nlx.MaHangGPLX = kh.HangGPLX;
                        nlx.HangDaoTao = kh.HangDT;
                        nlx.SoHoSo = string.Format("{0:000}", (int)nbmSoHS.Value + i).ToString();
                        nlx.NguoiNhanHSo = "";
                        nlx.HoVaTen = dt.Rows[i]["HoVaTen"].ToString().Trim().ToUpper();
                        nlx.HoVaTenIn = nlx.HoVaTen.ToUpper();
                        nlx.NgaySinh = ConvertNgaySinh(dt.Rows[i]["NgaySinh"].ToString());
                        nlx.GioiTinh = dt.Rows[i]["GioiTinh"].ToString() == "Nam" ? "M" : "F";
                        nlx.MaQuocTich = "VNM";
                        nlx.SoCMT = dt.Rows[i]["SoCMT"].ToString();
                        nlx.NgayCap = ConvertNgaySinh(dt.Rows[i]["NgayCapCMT"].ToString());
                        nlx.NoiCap = string.Format("{0:00}", dt.Rows[i]["NoiCapCMT"]).ToString();
                        nlx.MaDVHC_TT = dt.Rows[i]["NoiTT_MaDVHC"].ToString();
                        nlx.ChiTiet_TT = "";
                        nlx.MaDVHC_CT = dt.Rows[i]["NoiCT_MaDVHC"].ToString();
                        nlx.ChiTiet_CT = "";
                        nlx.SoNamLX = 0;
                        nlx.SoKmLX = 0;
                        nlx.GiayTo = "1,2,3";
                        nlx.SoGPLXDaCo = dt.Rows[i]["SoGPLXDaCo"].ToString();
                        nlx.HangGPLXDaCo = dt.Rows[i]["HangGPLXDaCo"].ToString();
                        nlx.NgayTTGPLXDaCo = dt.Rows[i]["NgayTTGPLXDaCo"].ToString();
                        nlx.NgayCapGPLXDaCo = dt.Rows[i]["NgayCapGPLXDaCo"].ToString();
                        nlx.NgayHHGPLXDaCo = "";
                        nlx.DVCapGPLXDaCo = dt.Rows[i]["DonViCapGPLXDaCo"].ToString();
                        nlx.NoiCapGPLXDaCo = "";
                        nlx.GhiChu = "";
                        nlx.SO_CMND_CU = "";
                        nlx.CHON_IN_GPLX = 1;

                        lstNguoiLX.Add(nlx);
                    }
                    #endregion

                    try
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn đúng Hạng Đào Tạo?", "Thông Báo", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (lstNguoiLX != null)
                            {

                                var rs = _DAO.InsertHocVien(lstNguoiLX);
                                if (rs)
                                {
                                    XtraMessageBox.Show($"Import thành công {lstNguoiLX.Count} học viên cho khóa {kh.TenKH}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    XtraMessageBox.Show($"Import thất bại! Vui lòng kiểm tra lại dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                lbStatus.Text = "Done";
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                lbStatus.Text = "Try Again";
                                txtHangDaoTao.Focus();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #region Update Result Graduate

        private void btn_ReadDataUpdate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePathExcel.Text = openFileDialog.FileName;

                    //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    try
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                                cbb_SheetDataUpdate.Properties.Items.Clear();

                                foreach (DataTable table in tableCollection)
                                {
                                    cbb_SheetDataUpdate.Properties.Items.Add(table.TableName);
                                }
                                XtraMessageBox.Show("Chọn Sheet chứa thông tin học viên cần import", "Thông báo", MessageBoxButtons.OK);

                                cbb_SheetDataUpdate.EditValue = tableCollection[0].TableName;
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        XtraMessageBox.Show($"Không thể đọc file vì đang được sử dụng bởi ứng dụng khác.\nVui lòng đóng file Excel và thử lại.\n\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cbb_SheetDataUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = tableCollection[cbb_SheetDataUpdate.SelectedItem.ToString()];
            gridControl_UpdateResult.DataSource = dt;
        }

        private void dtp_NgayQD_EditValueChanged(object sender, EventArgs e)
        {
            dtp_NgayVaoSo.DateTime = dtp_NgayQD.DateTime;
            dtp_NgayIn.DateTime = dtp_NgayQD.DateTime;
        }

        private void btn_UpdateResult_Click(object sender, EventArgs e)
        {
            if (cbb_SheetDataUpdate.SelectedItem is null)
            {
                MessageBox.Show(string.Format("Bạn chưa chọn Sheet chứa Data!"));
                cbb_SheetDataUpdate.Focus();
            }
            else
            {
                //Khởi tạo danh sách
                List<NguoiLX_HoSo_Update> lstNguoiLxUpdates = new List<NguoiLX_HoSo_Update>();
                List<HvTotNghiep> lstNguoiLxCCTN = new List<HvTotNghiep>();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Cập nhật kết quả tốt nghiệp

                        NguoiLX_HoSo_Update nguoiLx = new NguoiLX_HoSo_Update();
                        nguoiLx.MaDK = dt.Rows[i]["MaDK"].ToString().ToUpper().Trim();
                        string hang = dt.Rows[i]["Lop"].ToString().ToUpper().Trim().Substring(0, 2);
                        nguoiLx.SoGiayCNTN = txt_SoCNTN.Text + (hang == "C1" ? "C1" : "B") + "|" + dt.Rows[i]["SoCNTN"].ToString().ToUpper().Trim();
                        nguoiLx.TT_XuLy = "09";
                        nguoiLx.SoQuyetDinhTN = txt_SoQD.Text;
                        nguoiLx.NgayRaQDTN = dtp_NgayQD.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                        nguoiLx.SoSoTN = txt_SoSoCNTN.Text;
                        nguoiLx.NgayVaoSoTN = dtp_NgayVaoSo.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                        nguoiLx.NgayInGiayTN = dtp_NgayIn.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                        lstNguoiLxUpdates.Add(nguoiLx);

                        //Cập nhật dữ liệu in chứng chỉ tốt nghiệp

                        HvTotNghiep nguoiLxCCTN = new HvTotNghiep();
                        nguoiLxCCTN.MaDK = dt.Rows[i]["MaDK"].ToString().ToUpper();
                        nguoiLxCCTN.NgayQDTN = dtp_NgayQD.DateTime.ToString("dd/MM/yyyy");

                        lstNguoiLxCCTN.Add(nguoiLxCCTN);
                    }
                }

                try
                {
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn chắc chắn đúng cập nhật kết quả?", "Thông Báo", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (toggleSwitch_UpdateResult.IsOn)
                        {
                            if (lstNguoiLxUpdates != null)
                            {
                                foreach (NguoiLX_HoSo_Update item in lstNguoiLxUpdates)
                                {
                                    _DAO.UpdateResultGraduate(item);
                                }
                            }
                            XtraMessageBox.Show(string.Format("Cập nhật kết quả tốt nghiệp thành công {0} học viên!", lstNguoiLxUpdates.Count));
                        }

                        if (lstNguoiLxCCTN != null)
                        {
                            int status = _DAO.DeleteDataReportOld();
                            foreach (HvTotNghiep item in lstNguoiLxCCTN)
                            {
                                _DAO.InsertDataReportCNTN(item);
                            }
                            XtraMessageBox.Show(string.Format("Cập nhật kết quả thành công dữ liệu in CCTN {0} học viên!", lstNguoiLxCCTN.Count));
                        }

                        lbStatusUpdate.Text = "Done";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        lbStatusUpdate.Text = "Try Again";
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion Update Result Graduate

        #region In chứng chỉ tốt nghiệp

        public void LoadlstStudentInReportCNTN()
        {
            List<HvTotNghiep> lst = _gplx_Dao.GetHvTotNghiep();

            grd_Certificate.DataSource = lst;
            GridHelper.EnableRowNumbering(gridView_Certificate);
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            // Lấy các chỉ số của các hàng được chọn
            int[] selectedRowHandles = gridView_Certificate.GetSelectedRows();

            string lstId = "";
            int HangDT = 1;

            foreach (int rowHandle in selectedRowHandles)
            {
                var cellValue = gridView_Certificate.GetRowCellValue(rowHandle, "MaDK");

                lstId = lstId + "," + cellValue;
            }

            if (button.Caption == "Refresh")
            {
                LoadlstStudentInReportCNTN();
            }
            else if (button.Caption == "Print B2")
            {
                HangDT = 2;
                if (!string.IsNullOrEmpty(lstId))
                {
                    DataTable dt = _DAO.GetDataPrintCCTN(lstId);
                    rptPrintReportCNTN f = new rptPrintReportCNTN(dt, HangDT);
                    f.Show();
                }
                else
                {
                    XtraMessageBox.Show("Bạn phải chọn học viên cần in chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (button.Caption == "Print B11")
            {
                HangDT = 1;
                if (lstId != "")
                {
                    DataTable dt = _DAO.GetDataPrintCCTN(lstId);
                    rptPrintReportCNTN f = new rptPrintReportCNTN(dt, HangDT);
                    f.Show();
                }
                else
                {
                    XtraMessageBox.Show("Bạn phải chọn học viên cần in chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (button.Caption == "Print")
            {
                if (!string.IsNullOrEmpty(lstId))
                {
                    DataTable dt = _gplx_Dao.GetDataPrintCCTN(lstId);

                    new ReportPrintTool(new rptCertificate { DataSource = dt }).ShowPreview();
                }
                else
                {
                    XtraMessageBox.Show("Bạn phải chọn học viên cần in chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (button.Caption == "Save")
            {
                // Get the focused row handle
                int focusedRowHandle = gridView_Certificate.FocusedRowHandle;

                if (focusedRowHandle != -1)
                {
                    string HoVaTen = gridView_Certificate.GetRowCellValue(focusedRowHandle, "HoVaTen").ToString();
                    string NgaySinh = gridView_Certificate.GetRowCellValue(focusedRowHandle, "NgaySinh").ToString();
                    string GioiTinh = gridView_Certificate.GetRowCellValue(focusedRowHandle, "GioiTinh").ToString();
                    string SoVaoSo = gridView_Certificate.GetRowCellValue(focusedRowHandle, "SoVaoSo").ToString();

                    string MaDK = gridView_Certificate.GetRowCellValue(focusedRowHandle, "MaDK").ToString();

                    string HoVaTenEng = StringUtil.RemoveVietnameseSign(HoVaTen);
                    string GioiTinhEng;
                    if (GioiTinh == "Nam")
                        GioiTinhEng = "Mr";
                    else
                        GioiTinhEng = "Ms";
                    var ngaysinh = Convert.ToDateTime(NgaySinh);
                    string NgaySinhEng = ngaysinh.ToString("dd MMMM yyyy");

                    var result = XtraMessageBox.Show(string.Format("Cập nhật thông tin học viên {0}?", HoVaTen), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        _DAO.UpdateInfo(MaDK, HoVaTen, HoVaTenEng, NgaySinh, NgaySinhEng, GioiTinh, GioiTinhEng, SoVaoSo);
                    }
                    LoadlstStudentInReportCNTN();
                }
            }
        }
        #endregion In chứng chỉ tốt nghiệp
    }
}