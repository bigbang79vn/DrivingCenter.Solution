using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Helper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_GPLX : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_GPLX()
        {
            InitializeComponent();
        }

        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private KhoaHocGPLX kh;
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
            List<KhoaHocGPLX> lst = new List<KhoaHocGPLX>();
            lst = GPLX_DAO.Instance.GetlstKH();

            cbx_KhoaHoc.DataSource = lst;
            cbx_KhoaHoc.DisplayMember = "TenKHGplx";
        }

        public string MaDK()
        {
            string MaDK = "66015-";
            var dt = DateTime.Now.ToString("yyyyMMddhhmmssfff").ToString();
            MaDK += dt;
            return MaDK;
        }

        public string ConvertNgaySinh(string birthday)
        {
            DateTime ngaysinh = DateTime.Parse(birthday, CultureInfo.GetCultureInfo("vi-VN"));
            string Dob;
            Dob = string.Format("{0:0000}", ngaysinh.Year.ToString());
            Dob = Dob + string.Format("{0:00}", (int)ngaysinh.Month);
            Dob = Dob + string.Format("{0:00}", (int)ngaysinh.Day);
            return Dob;
        }

        private void Delay(double delay)
        {
            double delayTime = 0;
            while (delayTime < delay)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.05));
                delayTime++;
            }
        }

        public string GetHangDaoTaoByMaKH()
        {
            string HangDaoTao = "";
            kh = cbx_KhoaHoc.SelectedItem as KhoaHocGPLX;
            if (kh.MaKHGplx.Length >= 6)
            {
                string newmakh = kh.MaKHGplx.Substring(0, 6);
                if (newmakh == "66015K")
                    HangDaoTao = "B2";
                else
                {
                    if (newmakh == "660152")
                        HangDaoTao = "B11";
                    else
                        MessageBox.Show("Mã khóa học không đúng!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            return HangDaoTao;
        }

        private void btn_ReadData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;

                    //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            cb_SheetDataImport.Items.Clear();

                            MessageBox.Show("Chọn Sheet chứa thông tin học viên cần import", "Thông báo", MessageBoxButtons.OK);

                            foreach (DataTable table in tableCollection)
                            {
                                cb_SheetDataImport.Items.Add(table.TableName);
                            }
                        }
                    }
                }
            }
        }

        private void cb_SheetDataImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[cb_SheetDataImport.SelectedItem.ToString()];
            grd_ImportData.DataSource = dt;
        }

        private void cbx_KhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHangDaoTao.Text = GetHangDaoTaoByMaKH();
        }

        private void btn_Import_Click_1(object sender, EventArgs e)
        {
            if (cb_SheetDataImport.SelectedItem == null)
            {
                MessageBox.Show(string.Format("Bạn chưa chọn Sheet chứa Data!"));
                cb_SheetDataImport.Focus();
            }
            else
            {
                lbStatus.Text = "Running...";
                DataTable dt = tableCollection[cb_SheetDataImport.SelectedItem.ToString()];

                List<NguoiLX> lstNguoiLX = new List<NguoiLX>();
                List<NguoiLX_HoSo> lstnguoiLX_HoSo = new List<NguoiLX_HoSo>();
                List<NguoiLXHS_GT> lstnguoiLXHS_GT = new List<NguoiLXHS_GT>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Tạo mới NguoiLX
                        NguoiLX nguoiLX = new NguoiLX();
                        nguoiLX.MaDK = MaDK();
                        nguoiLX.DonViNhanHSo = "66015";
                        nguoiLX.HoVaTen = dt.Rows[i]["HoVaTen"].ToString().ToUpper().Trim();
                        nguoiLX.TenNLX = (dt.Rows[i]["HoVaTen"].ToString()).Trim().Substring((dt.Rows[i]["HoVaTen"].ToString()).Trim().LastIndexOf(' ') + 1).ToUpper();
                        int lastIndex = nguoiLX.HoVaTen.LastIndexOf(' ');
                        nguoiLX.HoDemNLX = (nguoiLX.HoVaTen).Trim().Substring(0, lastIndex).ToUpper();
                        nguoiLX.MaQuocTich = "VNM";
                        nguoiLX.NgaySinh = ConvertNgaySinh((dt.Rows[i]["NgaySinh"].ToString()));
                        nguoiLX.NoiTT = "";
                        nguoiLX.NoiTT_MaDVHC = dt.Rows[i]["NoiTT_MaDVHC"].ToString();
                        nguoiLX.NoiTT_MaDVQL = dt.Rows[i]["NoiTT_MaDVQL"].ToString();
                        nguoiLX.NoiCT = "";
                        nguoiLX.NoiCT_MaDVHC = dt.Rows[i]["NoiCT_MaDVHC"].ToString();
                        nguoiLX.NoiCT_MaDVQL = dt.Rows[i]["NoiCT_MaDVQL"].ToString();
                        nguoiLX.SoCMT = dt.Rows[i]["SoCMT"].ToString(); ;
                        nguoiLX.NgayCapCMT = dt.Rows[i]["NgayCapCMT"].ToString();
                        nguoiLX.NoiCapCMT = string.Format("{0:00}", dt.Rows[i]["NoiCapCMT"]).ToString();
                        nguoiLX.GhiChu = null;
                        nguoiLX.TrangThai = true;
                        nguoiLX.NguoiTao = null;
                        nguoiLX.NguoiSua = null;
                        nguoiLX.NgayTao = DateTime.Now;
                        nguoiLX.NgaySua = DateTime.Now;
                        if (dt.Rows[i]["GioiTinh"].ToString() == "Nam")
                            nguoiLX.GioiTinh = "M";
                        else
                            nguoiLX.GioiTinh = "F";
                        nguoiLX.HoVaTenIn = nguoiLX.HoVaTen.ToUpper();
                        nguoiLX.SO_CMND_CU = "";
                        lstNguoiLX.Add(nguoiLX);

                        //Tạo mới NguoiLX_HS
                        int ids = 11691;
                        NguoiLX_HoSo nguoiLX_HoSo = new NguoiLX_HoSo();
                        nguoiLX_HoSo.MaDK = nguoiLX.MaDK;
                        int soHS = (int)nbmSoHS.Value;
                        nguoiLX_HoSo.SoHoSo = string.Format("{0:000}", soHS + i).ToString();
                        nguoiLX_HoSo.MaCSDT = "66015";
                        nguoiLX_HoSo.MaSoGTVT = "66";
                        nguoiLX_HoSo.MaDVNhanHSo = "66015";
                        nguoiLX_HoSo.NgayNhanHSo = DateTime.Now.ToString();
                        nguoiLX_HoSo.NguoiNhanHSo = "";
                        nguoiLX_HoSo.NgayHenTra = null;
                        nguoiLX_HoSo.MaLoaiHs = 2;
                        nguoiLX_HoSo.TT_XuLy = "01";
                        nguoiLX_HoSo.DuongDanAnh = null;
                        nguoiLX_HoSo.ChatLuongAnh = null;
                        nguoiLX_HoSo.NgayThuNhanAnh = null;
                        nguoiLX_HoSo.NguoiThuNhanAnh = null;
                        nguoiLX_HoSo.SoGPLXDaCo = dt.Rows[i]["SoGPLXDaCo"].ToString();
                        nguoiLX_HoSo.HangGPLXDaCo = dt.Rows[i]["HangGPLXDaCo"].ToString();
                        nguoiLX_HoSo.DonViCapGPLXDaCo = dt.Rows[i]["DonViCapGPLXDaCo"].ToString();
                        nguoiLX_HoSo.NoiCapGPLXDaCo = "";
                        nguoiLX_HoSo.NgayCapGPLXDaCo = dt.Rows[i]["NgayCapGPLXDaCo"].ToString();
                        nguoiLX_HoSo.NgayHHGPLXDaCo = "";
                        nguoiLX_HoSo.NgayTTGPLXDaCo = dt.Rows[i]["NgayTTGPLXDaCo"].ToString();
                        nguoiLX_HoSo.DonViHocLX = null;
                        nguoiLX_HoSo.HangGPLX = GetHangDaoTaoByMaKH();
                        nguoiLX_HoSo.SoNamLX = 0;
                        nguoiLX_HoSo.SoKmLXAnToan = 0;
                        nguoiLX_HoSo.GiayCNSK = true;
                        nguoiLX_HoSo.LyDoCapDoi = null;
                        nguoiLX_HoSo.MucDichCapDoi = null;
                        //nguoiLX_HoSo.NoiDungSH = null;
                        nguoiLX_HoSo.MaKhoaHoc = kh.MaKHGplx;
                        nguoiLX_HoSo.HangDaoTao = GetHangDaoTaoByMaKH();
                        nguoiLX_HoSo.SoGiayCNTN = null;
                        nguoiLX_HoSo.SoCCN = null;
                        nguoiLX_HoSo.MaBC1 = null;
                        nguoiLX_HoSo.BC1_TuoiTS = true;
                        nguoiLX_HoSo.BC1_ThamNien = true;
                        nguoiLX_HoSo.MaBC2 = null;
                        nguoiLX_HoSo.KetQuaBC2 = null;
                        nguoiLX_HoSo.MaLyDoTCBC2 = null;
                        nguoiLX_HoSo.MaKySH = null;
                        nguoiLX_HoSo.SoBD = null;
                        nguoiLX_HoSo.LanSH = 1;
                        nguoiLX_HoSo.SoQDSH = null;
                        nguoiLX_HoSo.NgayQDSH = null;
                        nguoiLX_HoSo.KetQua_LyThuyet = 0;
                        nguoiLX_HoSo.NhanXet_LyThuyet = null;
                        nguoiLX_HoSo.KetQua_Hinh = 0;
                        nguoiLX_HoSo.NhanXet_Hinh = null;
                        nguoiLX_HoSo.KetQua_Duong = 0;
                        nguoiLX_HoSo.NhanXet_Duong = null;
                        nguoiLX_HoSo.KetQuaSH = "1";
                        nguoiLX_HoSo.SoQDTT = null;
                        nguoiLX_HoSo.NguoiKy = null;
                        nguoiLX_HoSo.GhiChu = "";
                        nguoiLX_HoSo.NguoiTao = "";
                        nguoiLX_HoSo.NguoiSua = null;
                        nguoiLX_HoSo.NgayTao = DateTime.Now.ToString();
                        nguoiLX_HoSo.NgaySua = DateTime.Now.ToString();
                        nguoiLX_HoSo.SoGPLXTmp = null;
                        nguoiLX_HoSo.NgayKTBC1 = null;
                        nguoiLX_HoSo.NguoiKTBC1 = null;
                        nguoiLX_HoSo.NgayKTBC2 = null;
                        nguoiLX_HoSo.MaBC2 = null;
                        nguoiLX_HoSo.NguoiKTBC2 = null;
                        nguoiLX_HoSo.MaIn = null;
                        nguoiLX_HoSo.KetQuaDoiSanhTW = null;
                        nguoiLX_HoSo.GhiChuKQDSTW = null;
                        nguoiLX_HoSo.ChuKy = null;
                        nguoiLX_HoSo.TrangThai = true;
                        nguoiLX_HoSo.MaHTCap = "CM_VN";
                        nguoiLX_HoSo.IDs = (ids + 1).ToString();
                        nguoiLX_HoSo.TT_XuLy_Old = null;
                        nguoiLX_HoSo.KQ_BC1 = null;
                        nguoiLX_HoSo.KQ_BC1_GhiChu = null;
                        nguoiLX_HoSo.Transfer_flag = 0;
                        nguoiLX_HoSo.VaoSoCNNSo = null;
                        nguoiLX_HoSo.NgayVaoSoCNN = null;
                        nguoiLX_HoSo.XepLoaiTotNghiep = null;
                        nguoiLX_HoSo.NgayCapCCN = null;
                        nguoiLX_HoSo.SoQuyetDinhTN = null;
                        nguoiLX_HoSo.NgayRaQDTN = null;
                        nguoiLX_HoSo.SoSoTN = null;
                        nguoiLX_HoSo.NgayVaoSoTN = null;
                        nguoiLX_HoSo.NgayInGiayTN = null;
                        nguoiLX_HoSo.NamcapLandau = null;
                        nguoiLX_HoSo.MaTrichNgang = null;
                        nguoiLX_HoSo.CoQuanQuanLyGPLX = null;
                        nguoiLX_HoSo.CHON_IN_GPLX = "2";
                        nguoiLX_HoSo.KetQuaSHM = "0";
                        nguoiLX_HoSo.NhanXet_MoPhong = null;
                        lstnguoiLX_HoSo.Add(nguoiLX_HoSo);

                        Delay(1);
                    }

                    //Add NguoiLXHS_GT

                    foreach (var item in lstnguoiLX_HoSo)
                    {
                        NguoiLXHS_GT nguoiLXHS_GT1 = new NguoiLXHS_GT();
                        nguoiLXHS_GT1.MaGT = 1;
                        nguoiLXHS_GT1.MaDK = item.MaDK;
                        nguoiLXHS_GT1.SoHoSo = item.SoHoSo;
                        nguoiLXHS_GT1.TenGT = "Đơn đề nghị học, sát hạch để cấp giấy phép lái xe theo mẫu quy định tại Phụ lục 13";
                        nguoiLXHS_GT1.TrangThai = true;
                        lstnguoiLXHS_GT.Add(nguoiLXHS_GT1);

                        NguoiLXHS_GT nguoiLXHS_GT2 = new NguoiLXHS_GT();
                        nguoiLXHS_GT2.MaGT = 2;
                        nguoiLXHS_GT2.MaDK = item.MaDK;
                        nguoiLXHS_GT2.SoHoSo = item.SoHoSo;
                        nguoiLXHS_GT2.TenGT = "Bản sao chụp giấy chứng minh nhân dân hoặc hộ chiếu còn thời hạn";
                        nguoiLXHS_GT2.TrangThai = true;
                        lstnguoiLXHS_GT.Add(nguoiLXHS_GT2);

                        NguoiLXHS_GT nguoiLXHS_GT3 = new NguoiLXHS_GT();
                        nguoiLXHS_GT3.MaGT = 3;
                        nguoiLXHS_GT3.MaDK = item.MaDK;
                        nguoiLXHS_GT3.SoHoSo = item.SoHoSo;
                        nguoiLXHS_GT3.TenGT = "Bản sao chụp giấy chứng minh nhân dân hoặc hộ chiếu còn thời hạn";
                        nguoiLXHS_GT3.TrangThai = true;
                        lstnguoiLXHS_GT.Add(nguoiLXHS_GT3);
                    }
                }
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn đúng Hạng Đào Tạo?", "Thông Báo", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string connectionString = @"Data Source=192.168.50.254;Initial Catalog=gplx_csdt;User ID=sa;Password=vNdc@123";

                        DapperPlusManager.Entity<NguoiLX>().Table("NguoiLX");
                        DapperPlusManager.Entity<NguoiLX_HoSo>().Table("NguoiLX_HoSo");
                        DapperPlusManager.Entity<NguoiLXHS_GT>().Table("[NguoiLXHS_GiayTo]");

                        if (lstNguoiLX != null && lstnguoiLX_HoSo != null && lstnguoiLXHS_GT != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkInsert(lstNguoiLX);
                                db.BulkInsert(lstnguoiLX_HoSo);
                                db.BulkInsert(lstnguoiLXHS_GT);
                            }
                        }

                        lbStatus.Text = "Done";

                        string KhoaHoc = kh.TenKHGplx;

                        MessageBox.Show(string.Format("Import thành công {0} học viên cho khóa {1}!", lstNguoiLX.Count, KhoaHoc));
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        lbStatus.Text = "Try Again";
                        txtHangDaoTao.Focus();
                    }
            }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }

        #endregion Import Data

        #region Update Result Graduate

        private void btn_ReadDataUpdate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePathExcel.Text = openFileDialog.FileName;

                    //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            cbb_SheetDataUpdate.Items.Clear();

                            foreach (DataTable table in tableCollection)
                            {
                                cbb_SheetDataUpdate.Items.Add(table.TableName);
                            }
                            MessageBox.Show("Chọn Sheet chứa thông tin học viên cần import", "Thông báo", MessageBoxButtons.OK);
                        }
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
            if (cbb_SheetData.SelectedItem is null)
            {
                MessageBox.Show(string.Format("Bạn chưa chọn Sheet chứa Data!"));
                cbb_SheetData.Focus();
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
                        nguoiLx.SoGiayCNTN = txt_SoCNTN.Text + "|" + dt.Rows[i]["SoCNTN"].ToString().ToUpper().Trim();
                        nguoiLx.TT_XuLy_Old = "09";
                        nguoiLx.SoQuyetDinhTN = txt_SoQD.Text;
                        nguoiLx.NgayRaQDTN = dtp_NgayQD.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                        nguoiLx.SoSoTN = txt_SoSoCNTN.Text;
                        nguoiLx.NgayVaoSoTN = dtp_NgayVaoSo.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                        nguoiLx.NgayInGiayTN = dtp_NgayIn.DateTime.ToString("yyyy-MM-dd 00:00:00.000");
                        lstNguoiLxUpdates.Add(nguoiLx);

                        //Cập nhật dữ liệu in chứng chỉ tốt nghiệp

                        HvTotNghiep nguoiLxCCTN = new HvTotNghiep();
                        nguoiLxCCTN.MaDK = dt.Rows[i]["MaDK"].ToString().ToUpper();
                        nguoiLxCCTN.HoVaTen = dt.Rows[i]["Ho"].ToString() + " " + dt.Rows[i]["Ten"].ToString();
                        nguoiLxCCTN.NgaySinh = (dt.Rows[i]["NgaySinh"].ToString()).Substring(0, 10);
                        nguoiLxCCTN.GioiTinh = dt.Rows[i]["GioiTinh"].ToString();
                        nguoiLxCCTN.HangGPLX = dt.Rows[i]["Lop"].ToString().Substring(0, dt.Rows[i]["Lop"].ToString().IndexOf("-"));
                        nguoiLxCCTN.XepLoaiTN = dt.Rows[i]["XepLoai"].ToString();
                        nguoiLxCCTN.NgayQDTN = dtp_NgayQD.DateTime.ToString("dd/MM/yyyy");
                        nguoiLxCCTN.NgayQDTNEng = dtp_NgayQD.DateTime.ToString("dd MMMM yyyy");
                        nguoiLxCCTN.SoCCTN = dt.Rows[i]["SoCNTN"].ToString() + " " + "LX" + dt.Rows[i]["Lop"].ToString();
                        nguoiLxCCTN.SoVaoSo = "Q01/" + dt.Rows[i]["SoCNTN"].ToString();
                        nguoiLxCCTN.HoVaTenEng = StringUtil.RemoveVietnameseSign(dt.Rows[i]["Ho"].ToString() + " " + dt.Rows[i]["Ten"].ToString());
                        if (nguoiLxCCTN.GioiTinh == "Nam")
                            nguoiLxCCTN.GioiTinhEng = "Mr";
                        else
                            nguoiLxCCTN.GioiTinhEng = "Ms";
                        var ngaysinh = Convert.ToDateTime(dt.Rows[i]["NgaySinh"]);
                        nguoiLxCCTN.NgaySinhEng = ngaysinh.ToString("dd MMMM yyyy");
                        if (nguoiLxCCTN.XepLoaiTN == "Xuất sắc")
                            nguoiLxCCTN.XepLoaiTNEng = "Excellent";
                        else if (nguoiLxCCTN.XepLoaiTN == "Giỏi")
                            nguoiLxCCTN.XepLoaiTNEng = "Very good";
                        else if (nguoiLxCCTN.XepLoaiTN == "Khá")
                            nguoiLxCCTN.XepLoaiTNEng = "Good";
                        else if (nguoiLxCCTN.XepLoaiTN == "TB Khá")
                            nguoiLxCCTN.XepLoaiTNEng = "Average Good";
                        else
                            nguoiLxCCTN.XepLoaiTNEng = "Average";
                        nguoiLxCCTN.HangGPLXEng = "Class " + dt.Rows[i]["Lop"].ToString().Substring(0, dt.Rows[i]["Lop"].ToString().IndexOf("-")) + " car-driving";
                        nguoiLxCCTN.HinhAnh = "";

                        lstNguoiLxCCTN.Add(nguoiLxCCTN);
                    }
                }

                try
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn đúng cập nhật kết quả?", "Thông Báo", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (cb_CapNhatKQ.Checked)
                        {
                            if (lstNguoiLxUpdates != null)
                            {
                                foreach (NguoiLX_HoSo_Update item in lstNguoiLxUpdates)
                                {
                                    GPLX_DAO.Instance.UpdateResultGraduate(item);
                                }
                            }
                            MessageBox.Show(string.Format("Cập nhật kết quả tốt nghiệp thành công {0} học viên!", lstNguoiLxUpdates.Count));
                        }

                        if (lstNguoiLxCCTN != null)
                        {
                            int status = IMG_DAO.Instance.DeleteDataReportOld();
                            foreach (HvTotNghiep item in lstNguoiLxCCTN)
                            {
                                IMG_DAO.Instance.InsertDataReportCNTN(item);
                            }
                            MessageBox.Show(string.Format("Cập nhật kết quả thành công dữ liệu in CCTN {0} học viên!", lstNguoiLxCCTN.Count));
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
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion Update Result Graduate

        #region In chứng chỉ tốt nghiệp

        public void LoadlstStudentInReportCNTN()
        {
            List<HvTotNghiep> lst = IMG_DAO.Instance.GetHvTotNghiep();

            int stt = 0;
            foreach (HvTotNghiep item in lst)
            {
                item.STT = stt + 1;
                stt++;
            }

            grd_Certificate.DataSource = lst;
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
                var cellValue = gridView_Certificate.GetRowCellValue(rowHandle, "Id");

                lstId = lstId + "," + cellValue;
            }

            if (button.Caption == "Refresh")
            {
                LoadlstStudentInReportCNTN();
            }
            else if (button.Caption == "Print B2")
            {
                HangDT = 2;
                if (lstId != "")
                {
                    DataTable dt = IMG_DAO.Instance.GetDataPrintCCTN(lstId);
                    rptPrintReportCNTN f = new rptPrintReportCNTN(dt, HangDT);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn học viên cần in chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (button.Caption == "Print B11")
            {
                HangDT = 1;
                if (lstId != "")
                {
                    DataTable dt = IMG_DAO.Instance.GetDataPrintCCTN(lstId);
                    rptPrintReportCNTN f = new rptPrintReportCNTN(dt, HangDT);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn học viên cần in chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (button.Caption == "Save")
            {
                // Get the focused row handle
                int focusedRowHandle = gridView_Certificate.FocusedRowHandle;

                string HoVaTen = gridView_Certificate.GetRowCellValue(focusedRowHandle, "HoVaTen").ToString();
                string NgaySinh = gridView_Certificate.GetRowCellValue(focusedRowHandle, "NgaySinh").ToString();
                string GioiTinh = gridView_Certificate.GetRowCellValue(focusedRowHandle, "GioiTinh").ToString();

                string MaDK = gridView_Certificate.GetRowCellValue(focusedRowHandle, "MaDK").ToString();

                string HoVaTenEng = StringUtil.RemoveVietnameseSign(HoVaTen);
                string GioiTinhEng;
                if (GioiTinh == "Nam")
                    GioiTinhEng = "Mr";
                else
                    GioiTinhEng = "Ms";
                var ngaysinh = Convert.ToDateTime(NgaySinh);
                string NgaySinhEng = ngaysinh.ToString("dd MMMM yyyy");

                var result = MessageBox.Show(string.Format("Cập nhật thông tin học viên {0}?", HoVaTen), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    IMG_DAO.Instance.UpdateInfo(MaDK, HoVaTen, HoVaTenEng, NgaySinh, NgaySinhEng, GioiTinh, GioiTinhEng);
                }
                LoadlstStudentInReportCNTN();
            }
        }

        #endregion In chứng chỉ tốt nghiệp
    }
}