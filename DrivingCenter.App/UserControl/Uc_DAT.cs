using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using DrivingCenter.App.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_DAT : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_DAT()
        {
            InitializeComponent();
        }

        private void Uc_DAT_Load(object sender, EventArgs e)
        {
            cbb_TongHopGv.Properties.Items.AddRange(new string[] { "Theo Giáo Viên", "Theo Xe" });
            cbb_TongHopGv.EditValue = "Theo Giáo Viên";

            dtp_PhByDay.DateTime = DateTime.Now;
            dtp_TongHopHvByDay.DateTime = DateTime.Now;
            dtp_TongHopGioDay.DateTime = DateTime.Now;
            dtp_PhienHocLoiByDay.DateTime = DateTime.Now;

            dtp_From.DateTime = DAT_DAO.Instance.GetMinNgayKG();
            dtp_To.DateTime = DAT_DAO.Instance.GetMaxNgayBG();

            //DateTime now = DateTime.Now;
            //DateTime lastDayOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            //dtp_To.DateTime = lastDayOfMonth;

            LoadKhDangDaoTao();
        }

        //-----------Panel Left------------
        private bool isCollapsed;

        //---------------------------------
        private DataTable dt;

        private List<TongHopHVByDay> lstSum;
        private List<TongHopGV> lstSumGV;
        private List<KhoaHocDangDT> lstKhoaHoc;
        private List<PhienHoc> lstPH;
        private string HangDaoTao;
        private int khoahocid;
        private string MaKH;
        private string TenKhoaHoc;
        private int flag;

        #region Phiên Học

        private void LoadPhienHoc()
        {
            string day = dtp_PhByDay.DateTime.ToString("dd/MM/yyyy");
            label11.Text = "DANH SÁCH PHIÊN HỌC NGÀY " + day;
            lstPH = DAT_DAO.Instance.GetLstPhienByDateTrue(day);
            CheckPhInList(lstPH);
        }

        private void btn_ViewPhByDay_Click(object sender, EventArgs e)
        {
            LoadPhienHoc();
        }

        private void dtp_PhByDay_EditValueChanged(object sender, EventArgs e)
        {
            LoadPhienHoc();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            string datetime = dtp_PhByDay.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
            DAT_DAO.Instance.ReLoad(datetime);
            LoadPhienHoc();
        }

        public int ProcessPhienHoc(List<PhienHoc> lstPH)
        {
            int SendTC = 0;
            // Sắp xếp theo Time_LogOut để kiểm tra nghỉ thiếu hiệu quả hơn
            var sortedList = lstPH.OrderBy(x => x.Time_LogOut).ToList();

            for (int i = 0; i < sortedList.Count; i++)
            {
                var item = sortedList[i];

                var sb = new StringBuilder(); // Dùng StringBuilder để tích lũy lỗi

                // Tỷ lệ nhận diện
                item.TyLe = item.SoLanNhanDien > 0 ? Math.Round((double)item.SoLanNhanDienThanhCong / item.SoLanNhanDien * 100, 1) : 0;
                if (item.TyLe < 90) sb.Append("Nhận Diện! ");

                // Quãng đường
                if (item.QuangDuongPhien < 5 && item.LogOut) sb.Append("Quãng đường <5! ");

                // Vận tốc
                double vantoc = item.dbTime > 0 ? item.QuangDuongPhien / (item.dbTime / 3600) : 0;
                if (vantoc < 20) sb.Append("Vận Tốc! ");

                // Đăng nhập sớm
                if (item.Time_LogIn.Hour < 7) sb.Append("ĐN Sớm! ");

                // Chạy quá giờ
                var logout = item.Time_LogOut;
                if (logout.Hour > 22 || (logout.Hour == 22 && logout.Minute > 30)) sb.Append("Quá Giờ! ");

                // Kiểm tra ngày giờ theo khóa học
                int khoahoc = int.Parse(item.TenKH.Substring(item.TenKH.IndexOf("K") + 1));
                if (IsInvalidCourseTime(item, khoahoc)) sb.Append("Sai ngày giờ! ");

                // Gửi Tổng cục
                if (item.LogOut && !item.SendTongCuc)
                {
                    sb.Append("Chưa gửi TC! ");
                    SendTC++;
                }

                // Kiểm tra trùng lặp phiên
                for (int j = 0; j < sortedList.Count; j++)
                {
                    if (i == j) continue;
                    var other = sortedList[j];
                    if (item.NguoiLxID == other.NguoiLxID && item.TenKH == other.TenKH)
                    {
                        bool isOverlap = item.Time_LogIn < other.Time_LogOut && other.Time_LogIn < item.Time_LogOut;
                        if (isOverlap)
                        {
                            sb.Append("Trùng phiên! ");
                            break;
                        }
                    }
                }

                // Gán chuỗi lỗi
                item.Error = sb.ToString();

                // Kiểm tra nghỉ thiếu với phần tử tiếp theo (nếu có)
                if (i < sortedList.Count - 1)
                {
                    var nextItem = sortedList[i + 1];
                    if (nextItem.NguoiLxID == item.NguoiLxID && nextItem.TenKH == item.TenKH)
                    {
                        TimeSpan tp = nextItem.Time_LogIn - item.Time_LogOut;
                        if (tp.TotalSeconds > 0 && tp.TotalSeconds < 900)
                        {
                            nextItem.Error += "Nghỉ Thiếu! ";
                        }
                    }
                }
            }
            return SendTC;
        }

        private bool IsInvalidCourseTime(PhienHoc item, int khoahoc)
        {
            DayOfWeek wk = item.Time_LogIn.DayOfWeek;
            var loginHour = item.Time_LogIn.Hour;
            var logout = item.Time_LogOut;
            bool isEven = khoahoc % 2 == 0;

            return isEven
                ? wk == DayOfWeek.Tuesday || wk == DayOfWeek.Wednesday || wk == DayOfWeek.Thursday ||
                  (wk == DayOfWeek.Monday && (loginHour < 16 || logout.Hour > 22 || (logout.Hour == 22 && logout.Minute > 30)))
                : wk == DayOfWeek.Friday || wk == DayOfWeek.Saturday || wk == DayOfWeek.Sunday ||
                  (wk == DayOfWeek.Monday && (loginHour < 7 || logout.Hour > 12 || (logout.Hour == 12 && logout.Minute > 30)));
        }
        private void CheckPhInList(List<PhienHoc> lstPH)
        {
            ProcessPhienHoc(lstPH);

            lb_CountUnSendTC.Text = ProcessPhienHoc(lstPH).ToString();
            ConvertListToDataTable convert = new ConvertListToDataTable();
            dt = convert.ToDataTable(lstPH);

            gridControl_PhienHoc.DataSource = lstPH;
        }

        private void btn_ExportPH_Click(object sender, EventArgs e)
        {
            string day = dtp_PhByDay.DateTime.ToString("dd-MM-yyyy");

            string fileName = $"Phien DAT - {day}.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridControl_PhienHoc.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            var result = XtraMessageBox.Show("Xuất Excel thành công! Mở file", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        private void gridView_PhienHoc_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }

        #region Context Menustrip Phiên học

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPhienHoc();
        }

        private void DetailSession_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHoc.FocusedRowHandle;
            var diemdanhnguoilxId = gridView_PhienHoc.GetRowCellValue(focusedRowHandle, "MaPhien");

            frmDetailSession f = new frmDetailSession(diemdanhnguoilxId.ToString());
            f.ShowDialog();
        }

        private void CheckImgNull_Click(object sender, EventArgs e)
        {
            List<PhienHoc> lstNull = new List<PhienHoc>();

            gridControl_PhienHoc.DataSource = lstNull;

            foreach (PhienHoc item in lstPH.ToList())
            {
                int check = DAT_DAO.Instance.CheckImgNullNew(item.MaPhien.ToString());
                if (check == 1) lstPH.Remove(item);
            }

            gridControl_PhienHoc.DataSource = lstPH;
        }

        private void mn_SessionLock_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHoc.FocusedRowHandle;

            var diemdanhnguoilxId = gridView_PhienHoc.GetRowCellValue(focusedRowHandle, "MaPhien");

            fGhiChu f = new fGhiChu(diemdanhnguoilxId.ToString());
            f.Show();
        }

        private void deleteSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHoc.FocusedRowHandle;

            var diemdanhnguoilxId = gridView_PhienHoc.GetRowCellValue(focusedRowHandle, "MaPhien");

            string day = dtp_PhByDay.DateTime.ToString("dd/MM/yyyy");
            if (XtraMessageBox.Show(string.Format("Bạn có chắc chắn xóa phiên học \n{0}", diemdanhnguoilxId), "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                DAT_DAO.Instance.DeletePhByID(diemdanhnguoilxId.ToString());
                List<PhienHoc> lstPH = DAT_DAO.Instance.GetLstPhienByDateTrue(day);
                CheckPhInList(lstPH);
            }
        }

        private void EndSession_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHoc.FocusedRowHandle;

            var diemdanhnguoilxId = gridView_PhienHoc.GetRowCellValue(focusedRowHandle, "MaPhien");
            var NguoiLxID = Convert.ToInt64(gridView_PhienHoc.GetRowCellValue(focusedRowHandle, "NguoiLxID"));

            var checkLogOut = gridView_PhienHoc.GetRowCellValue(focusedRowHandle, "LogOut");

            if ((bool)checkLogOut == false)
            {
                var result = XtraMessageBox.Show(string.Format("Phiên học chưa kết thúc. Bạn muốn kết thúc phiên học?"), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                if (result == DialogResult.OK)
                {
                    DAT_DAO.Instance.KetThucPhien(Convert.ToInt64(NguoiLxID), diemdanhnguoilxId.ToString());
                    LoadPhienHoc();
                }
            }
        }

        #endregion Context Menustrip Phiên học

        #endregion Phiên Học

        #region Kết Quả

        private void btn_Extend_Click(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                PanelLeft.Width = PanelLeft.Width + 210;
                isCollapsed = false;
                this.Refresh();
            }
            else
            {
                PanelLeft.Width = PanelLeft.Width - 210;
                isCollapsed = true;
                this.Refresh();
            }
        }

        private void LoadKhDangDaoTao()
        {
            lstKhoaHoc = DAT_DAO.Instance.GetAllKhoaHocDDT();
            gridControl_KhoaHoc.DataSource = lstKhoaHoc;
        }

        private void btn_LoadKhoaHoc_Click(object sender, EventArgs e)
        {
            LoadKhDangDaoTao();
        }

        public List<TongHopHVByKhoa> TongHopKQByKhoa(int khoahocid)
        {
            List<TongHopHVByKhoa> lst = DAT_DAO.Instance.TongHopHvByKhoa(khoahocid);

            List<TongHopHVByKhoa> lstB1 = DAT_DAO.Instance.TongHopPhB11(khoahocid);

            List<TongHopHVByKhoa> lstBD = DAT_DAO.Instance.TongHopPhBD(khoahocid, HangDaoTao);

            if (HangDaoTao == "B2" || HangDaoTao == "B")
            {
                foreach (TongHopHVByKhoa itemB1 in lstB1)
                {
                    foreach (TongHopHVByKhoa item in lst)
                    {
                        if (itemB1.MaDK == item.MaDK)
                        {
                            item.TongQuangDuong_B1 = itemB1.TongQuangDuong_B1;
                            item.TongThoiGian_B1 = itemB1.TongThoiGian_B1;
                        }
                    }
                }
            }
            foreach (TongHopHVByKhoa item in lst)
            {
                foreach (TongHopHVByKhoa itemBD in lstBD)
                {
                    if (item.MaDK == itemBD.MaDK)
                    {
                        item.TongQuangDuong_PhienDem = itemBD.TongQuangDuong_PhienDem;
                        item.TongThoiGian_PhienDem = itemBD.TongThoiGian_PhienDem;
                    }
                }
            }
            return lst;
        }

        public int HvDuDK { get; private set; } = 0;

        public List<ViewTongHopHvByKhoa> ProcessTongHop(List<TongHopHVByKhoa> lstKQ, string HangDaoTao)
        {
            return lstKQ.Select((item, index) =>
            {
                var view = new ViewTongHopHvByKhoa
                {
                    STT = index + 1,
                    NguoiLxID = item.NguoiLxID,
                    View_MaDK = item.MaDK,
                    View_HoTenGV = item.HoTenGV,
                    View_HoVaTen = item.HoVaTenHV,
                    View_HangDaoTao = item.HangDaoTao,
                    View_TongQuangDuong = item.TongQuangDuongTichLuy,
                    TongThoiGian = Math.Round(item.TongThoiGianTichLuy / 3600, 4),
                    View_TongThoiGian = FormatTimeSpan(TimeSpan.FromSeconds(item.TongThoiGianTichLuy)),
                    View_QuangDuongPhienGuiTC = item.QuangDuongPhienGuiTC,
                    View_ThoiGianPhienGuiTC = FormatTimeSpan(TimeSpan.FromSeconds(item.ThoiGianPhienGuiTC)),
                    View_TongQuangDuong_B11 = item.TongQuangDuong_B1,
                    View_TongThoiGian_B11 = FormatTimeSpan(TimeSpan.FromSeconds(item.TongThoiGian_B1)),
                    TongThoiGianSTD = Math.Round(item.TongThoiGian_B1 / 3600, 4),
                    View_TongQuangDuong_PhienDem = item.TongQuangDuong_PhienDem,
                    View_TongThoiGian_PhienDem = FormatTimeSpan(TimeSpan.FromSeconds(item.TongThoiGian_PhienDem)),
                    TongThoiGianBD = Math.Round(item.TongThoiGian_PhienDem / 3600, 4),
                    View_SoNgayHoc = item.SoNgayHocKQ
                };

                // Check điều kiện DAT New
                view.View_Status = CheckDatNewCondition(item, HangDaoTao) ? "Đủ điều kiện!" : "";
                if (!string.IsNullOrEmpty(view.View_Status)) HvDuDK++;

                return view;
            }).ToList();
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            int hours = ts.Days * 24 + ts.Hours;
            return $"{hours:D2} giờ {ts.Minutes:D2} phút";
        }

        private bool CheckDatNewCondition(TongHopHVByKhoa item, string hangDaoTao)
        {
            bool baseCondition = item.TongQuangDuongTichLuy > 710 && item.TongThoiGianTichLuy > 43200;
            switch (hangDaoTao)
            {
                case "B11":
                    return baseCondition && item.TongThoiGian_PhienDem > 11100 && item.SoNgayHocKQ >= 6;
                case "B":
                    return item.TongQuangDuongTichLuy > 810 && item.TongThoiGianTichLuy > 72480 && 
                           item.TongThoiGian_PhienDem > 7500 && item.SoNgayHocKQ >= 6;
                case "B.01":
                    return baseCondition && item.TongThoiGian_PhienDem > 7500 && item.SoNgayHocKQ >= 4;
                default:
                    return item.TongQuangDuongTichLuy > 810 && item.TongThoiGianTichLuy > 72480 &&
                           item.TongThoiGian_B1 > 7500 && item.TongThoiGian_PhienDem > 11100 && item.SoNgayHocKQ >= 8;
            }
        }

        public void ViewTongHopKQ(List<TongHopHVByKhoa> lstKQ, string HangDaoTao)
        {
            List<ViewTongHopHvByKhoa> lst = ProcessTongHop(lstKQ, HangDaoTao);
            ConvertListToDataTable convert = new ConvertListToDataTable();
            dt = convert.ToDataTable(lst);

            grid_Result.DataSource = lst;
        }

        private void gridView_KhoaHocDDT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            KhoaHocDangDT rowData = gridView_KhoaHocDDT.GetFocusedRow() as KhoaHocDangDT;

            khoahocid = Convert.ToInt32(rowData.KhoaHocID);
            TenKhoaHoc = rowData.TenKH;
            MaKH = rowData.MaKH.ToString();
            HangDaoTao = rowData.HangDaoTao.ToString();
            lb_KetquaKh.Text = "KẾT QUẢ HỌC VIÊN KHÓA " + TenKhoaHoc;
            HvDuDK = 0;
            ViewTongHopKQ(TongHopKQByKhoa(khoahocid), HangDaoTao);
            DAT_DAO.Instance.UpdateHvDuDK(HvDuDK, khoahocid);
        }

        private void cb_ShowSendTC_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_ShowSendTC.Checked)
            {
                View_QuangDuongPhienGuiTC.Visible = true;
                View_ThoiGianPhienGuiTC.Visible = true;
            }
            else
            {
                View_QuangDuongPhienGuiTC.Visible = false;
                View_ThoiGianPhienGuiTC.Visible = false;
            }
        }
        private void mn_ContextThongKeGiaoVien_Click(object sender, EventArgs e)
        {
            // Lấy giá trị của cột "ID" từ dòng hiện tại
            int focusedRowHandle = gridView_KhoaHocDDT.FocusedRowHandle;
            string khoahoc = gridView_KhoaHocDDT.GetRowCellValue(focusedRowHandle, "TenKH").ToString();

            lstSumGV = DAT_DAO.Instance.TongHopGVsByKh(khoahoc);

            foreach (var item in lstSumGV)
            {
                if (DateTime.TryParseExact(item.NgayTap, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    DayOfWeek dayOfWeek = date.DayOfWeek;
                    if (item.TongThoiGian > 36000 && dayOfWeek != DayOfWeek.Monday)
                    {
                        item.SGhiChu = "Quá giờ dạy!";
                    }
                    else if (item.TongThoiGian > 18000 && dayOfWeek.Equals(DayOfWeek.Monday))
                    {
                        item.SGhiChu = "Quá giờ dạy Thứ 2!";
                    }
                }
                else
                {
                    Console.WriteLine("Ngày không hợp lệ!");
                }
            }

            ConvertListToDataTable convert = new ConvertListToDataTable();
            DataTable dt = convert.ToDataTable(lstSumGV);

            ExportExcelWithName exportExcelWithName = new ExportExcelWithName();
            exportExcelWithName.ExportToExcel(dt, "Giờ dạy", TenKhoaHoc);
        }

        private void mn_TongHopTienDo_Click(object sender, EventArgs e)
        {
            List<TienDo> lst = DAT_DAO.Instance.TongHopTienDoDatByKH(khoahocid, MaKH);

            ConvertListToDataTable convert = new ConvertListToDataTable();
            DataTable dt = convert.ToDataTable(lst);

            ExportExcelWithName exportExcelWithName = new ExportExcelWithName();
            exportExcelWithName.ExportToExcel(dt, "Giờ dạy", TenKhoaHoc);
        }
        private void mn_ThongKeKetQuaHocVien_Click(object sender, EventArgs e)
        {
            DataTable resultBC = DAT_DAO.Instance.BaoCaoKetQuaHocVien(khoahocid, 0);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;


            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }
        private void mn_ThongKeHocVienHoanThanh_Click(object sender, EventArgs e)
        {
            DataTable resultBC = DAT_DAO.Instance.BaoCaoKetQuaHocVien(khoahocid, 1);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;

            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        private void mn_ThongKeHocVienKhongHoanThanh_Click(object sender, EventArgs e)
        {
            DataTable resultBC = DAT_DAO.Instance.BaoCaoKetQuaHocVien(khoahocid, 2);

            // Gán DataSource cho Report
            rptBaoCaoKetQuaHocDAT rpt = new rptBaoCaoKetQuaHocDAT();
            rpt.DataSource = resultBC;


            // Hiển thị report
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreview();
        }

        private void mn_XoaPhienHocLoginIsNull_Click(object sender, EventArgs e)
        {
            List<Guid> list = DAT_DAO.Instance.GetlstPhienHocLoginIsNull(khoahocid);
            int sophienhocLoginIsNull = list.Count();
            if (sophienhocLoginIsNull == 0)
            {
                XtraMessageBox.Show(string.Format("Không có phiên học Login Is Null !"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (XtraMessageBox.Show(string.Format("Có {0} phiên học Login Is Null ! \n Bạn có chắc muốn xóa {0} phiên học Login Is Null? ", sophienhocLoginIsNull), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    DAT_DAO.Instance.DeletePhienHocLoginIsNull(list);
                }
            }
        }

        private void btn_ExportResultToExcel_Click(object sender, EventArgs e)
        {
            if (cb_StyleExport.Checked)
            {
                string fileName = $"{TenKhoaHoc}.xlsx";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                grid_Result.ExportToXlsx(filePath, new XlsxExportOptionsEx()
                {
                    ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
                });

                var result = XtraMessageBox.Show("Xuất Excel thành công! Mở file", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            else
            {
                ExportExcelWithName exportExcelWithName = new ExportExcelWithName();
                exportExcelWithName.ExportToExcel(dt, TenKhoaHoc, TenKhoaHoc);
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var NguoiLXID = Convert.ToInt64(gridView_Result.GetFocusedRowCellValue("NguoiLxID"));
            var resultBC = DAT_DAO.Instance.GetBaoCaoHocVienByNguoilxID(NguoiLXID);
            new ReportPrintTool(new rptResultDAT { DataSource = resultBC }).ShowPreview();
        }

        #endregion Kết Quả

        #region Tổng hợp giờ Học Viên trong ngày

        private void LoadSumTimeHV(string day)
        {
            lstSum = DAT_DAO.Instance.TongHopGioHVByDay(day)
                .Select(item =>
                {
                    int time = Convert.ToInt32((item.TongThoiGianLD_Phien.Trim()).Remove(2, 6));
                    item.GhiChu = time >= 5 ? "Quá giờ!" : "";
                    return item;
                })
                .ToList();

            gridControl_TongHopHvByDay.DataSource = lstSum;
        }

        private void btn_ViewTongHopHv_Click(object sender, EventArgs e)
        {
            string day = dtp_TongHopHvByDay.DateTime.ToString("dd/MM/yyyy");
            LoadSumTimeHV(day);
            lb_Tonghophocvienbyday.Text = "TỔNG HỢP GIỜ HỌC CỦA HỌC VIÊN NGÀY " + day;
        }

        private void dtp_TongHopHvByDay_EditValueChanged(object sender, EventArgs e)
        {
            string day = dtp_TongHopHvByDay.DateTime.ToString("dd/MM/yyyy");
            LoadSumTimeHV(day);
            lb_Tonghophocvienbyday.Text = "TỔNG HỢP GIỜ HỌC CỦA HỌC VIÊN NGÀY " + day;
        }

        private void gridView_TongHopHvByDay_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }

        #endregion Tổng hợp giờ Học Viên trong ngày

        #region Tổng hợp giờ dạy của giáo viên trong ngày

        private void dtp_TongHopGioDay_EditValueChanged(object sender, EventArgs e)
        {
            TongHopGvTrongNgay();
        }

        private void btn_TongHopGioGv_Click(object sender, EventArgs e)
        {
            TongHopGvTrongNgay();
        }
        private void cbb_TongHopGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            TongHopGvTrongNgay();
        }

        private void TongHopGvTrongNgay()
        {
            flag = cbb_TongHopGv.EditValue.ToString() == "Theo Giáo Viên" ? 0 : 1;
            string day = dtp_TongHopGioDay.DateTime.ToString("dd/MM/yyyy");
            if (DateTime.TryParseExact(day, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                LoadTongHopGV(day, flag);
            }
            else
            {
                Console.WriteLine("Ngày không hợp lệ!");
            }
            lb_TongHopGvByDay.Text = "TỔNG HỢP GIỜ DẠY GV NGÀY " + day;
        }
        private void LoadTongHopGV(string day,  int flag)
        {
            lstSumGV = DAT_DAO.Instance.TongHopGVsByDay(day, flag);
            List<string> lstMaGVs = DAT_DAO.Instance.GetLstGvTrue(day);

            foreach (TongHopGV item in lstSumGV)
            {
                if (lstMaGVs.Contains(item.MaGv) == false)
                {
                    item.SGhiChu += "Không ĐN/ĐX! ";
                }

                int hour = Convert.ToInt32(item.SThoiGian.Trim().Remove(2, 6));
                int minute = Convert.ToInt32((item.SThoiGian.Trim().Substring(3)).Remove(2, 3));
                DayOfWeek wk = dtp_PhByDay.DateTime.DayOfWeek;
                if (wk == DayOfWeek.Monday && hour == 5)
                {
                    item.SGhiChu = "Quá Giờ! ";
                }
                if (hour == 10 && minute > 0 || hour > 10)
                {
                    item.SGhiChu = "Quá Giờ! ";
                }

                if (item.SoHocVienByDay == 1)
                {
                    item.SGhiChu += "Dạy 1 Hv/Ngày! ";
                }
            }

            gridControl_TongHopGioDay.DataSource = lstSumGV;
        }

        private void gridView_TongHopGioDay_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }
        #endregion Tổng hợp giờ dạy của giáo viên trong ngày

        #region Phiên học lỗi trong ngày

        private void btn_ViewPhLoi_Click(object sender, EventArgs e)
        {
            string day = dtp_PhienHocLoiByDay.DateTime.ToString("dd/MM/yyyy");
            lbl_PhienHocLoi.Text = "DANH SÁCH PHIÊN HỌC LỖI " + day;
            LoadAllPhLoi(day);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHocLoi.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Không có phiên học cần xóa!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var diemdanhnguoilxId = gridView_PhienHocLoi.GetRowCellValue(focusedRowHandle, "MaPhien");
                string day = dtp_PhienHocLoiByDay.DateTime.ToString("dd/MM/yyyy");
                if (XtraMessageBox.Show(string.Format("Bạn có chắc chắn xóa phiên học \n{0}", diemdanhnguoilxId), "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    DAT_DAO.Instance.DeletePhByID(diemdanhnguoilxId.ToString());
                    LoadAllPhLoi(day);
                }
            }
        }

        private void LoadAllPhLoi(string day)
        {
            List<PhienHoc> lstPhLoi = DAT_DAO.Instance.GetLstPhienLoiByDate(day);
            CheckPhInList(lstPhLoi);
            gridControl_PhienHocLoi.DataSource = lstPhLoi;
        }

        private void gridView_PhienHocLoi_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }
        #endregion Phiên học lỗi trong ngày

        #region Phiên học sai

        private void btn_ViewPhienHocSai_Click(object sender, EventArgs e)
        {
            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
        }

        private void LoadLstPhienHocBo(string from, string to)
        {
            List<PhienHoc> lst = DAT_DAO.Instance.TongHopPhienHocLoi(from, to);

            ConvertListToDataTable convert = new ConvertListToDataTable();
            dt = convert.ToDataTable(lst);

            gridControl_PhienHocBo.DataSource = lst;
        }

        private void mn_refresh_Click(object sender, EventArgs e)
        {
            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
        }

        private void mn_UnlockSession_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHocBo.FocusedRowHandle;

            var diemdanhnguoilxId = gridView_PhienHocBo.GetRowCellValue(focusedRowHandle, "MaPhien");

            var result = XtraMessageBox.Show(string.Format("Sử dụng lại phiên học {0}?", diemdanhnguoilxId), "Thông báo", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                DAT_DAO.Instance.UnlockSession(diemdanhnguoilxId.ToString());
            }

            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
        }

        private void mn_DeletePhienHoc_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_PhienHocBo.FocusedRowHandle;

            var diemdanhnguoilxId = gridView_PhienHocBo.GetRowCellValue(focusedRowHandle, "MaPhien");

            var result = XtraMessageBox.Show(string.Format("Xóa phiên học {0}?", diemdanhnguoilxId), "Thông báo", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                DAT_DAO.Instance.DeletePhByID(diemdanhnguoilxId.ToString());
            }

            string from = dtp_From.DateTime.ToString("yyyyMMdd");
            string to = dtp_To.DateTime.ToString("yyyyMMdd");

            LoadLstPhienHocBo(from, to);
        }
        private void btn_ExportPhSai_Click(object sender, EventArgs e)
        {
            string day = dtp_PhByDay.DateTime.ToString("dd-MM-yyyy");

            string fileName = $"Phien_DAT_bo.xlsx";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            gridControl_PhienHocBo.ExportToXlsx(filePath, new XlsxExportOptionsEx()
            {
                ExportType = ExportType.WYSIWYG  // Giữ nguyên định dạng
            });

            var result = XtraMessageBox.Show("Xuất Excel thành công! Mở file", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        #endregion Phiên học sai

        #region Kiểm tra chồng phiên


        #endregion

    }
}