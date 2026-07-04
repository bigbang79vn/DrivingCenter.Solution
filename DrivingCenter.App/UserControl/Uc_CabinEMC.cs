using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.FReport;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static DevExpress.Office.Drawing.LazyGroupBrush;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_CabinEMC : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly CabinI2E_DAO _DAO = new CabinI2E_DAO();
        public Uc_CabinEMC()
        {
            InitializeComponent();
        }

        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private string TenKH;
        private List<KhoaHoc> lstKhoaHoc;
        private List<ThongKe> _lstThongKe;
        private int IdKhoaHoc;
        private byte[] Jpegbytes;

        private void Uc_CabinEMC_Load(object sender, EventArgs e)
        {
            Initialize();
            tblLayout_Result.Dock = DockStyle.Fill;
            tblLayout_ThongKe.Dock = DockStyle.Fill;
            tblLayout_KhoaHoc.Dock = DockStyle.Fill;

            dtp_DayTHCabin.DateTime = DateTime.Now;
        }

        public void Initialize()
        {
            List<KhoaHoc> lst = _DAO.GetAllKhoaHoc();
            grd_QuanLyKhoaHoc.DataSource = lst;

            lstKhoaHoc = lst.Where(x => x.LoaiDaoTao == "Đang Đào Tạo")
                .OrderBy(x => x.TenKhoaHoc)
                .ToList();
            cbb_KhoaHocEMC.Properties.DataSource = lstKhoaHoc;
            cbb_KhoaHocEMC.Properties.DisplayMember = "TenKhoaHoc";
            cbb_KhoaHocEMC.Properties.ValueMember = "KhoaHocID";
            if (lstKhoaHoc.Count > 0)
            {
                cbb_KhoaHocEMC.EditValue = lstKhoaHoc[0].KhoaHocID;
            }
        }

        public List<HocVien> CheckResult(List<HocVien> lst)
        {
            foreach (HocVien item in lst)
            {
                string kl = _DAO.CheckDK(item.NguoiLxID, item.MaKH);
                if (kl == "Đáp ứng quy định")
                {
                    item.Result = true;
                }
                else
                {
                    item.Result = false;
                }
            }
            return lst;
        }

        public void LoadLstHvByKH(int IdKhoaHoc)
        {
            List<Int64> lstNguoiLxID = _DAO.GetlstNguoiLxID(IdKhoaHoc);

            //---------Update thoi gian tap, bai tap to NguoiLx-----

            foreach (var item in lstNguoiLxID)
            {
                _DAO.UpdateTimeExerToNguoiLx(item);
            }

            List<HocVien> _lstStudent = _DAO.GetlstHocVienByIdKhoaHoc(IdKhoaHoc);

            CheckResult(_lstStudent);

            grd_HocVien.DataSource = _lstStudent;
        }

        public void LoadTongHopKQ()
        {
            string day = dtp_DayTHCabin.DateTime.ToString("dd/MM/yyyy");

            lb_ThongKe.Text = "THỐNG KÊ KẾT QUẢ CABIN NGÀY " + day;

            _lstThongKe = _DAO.LstThongKe(day);

            grd_ThongKe.DataSource = _lstThongKe;
        }

        #region Event

        private void cbb_KhoaHocEMC_EditValueChanged(object sender, EventArgs e)
        {
            IdKhoaHoc = (int)cbb_KhoaHocEMC.EditValue;
            TenKH = cbb_KhoaHocEMC.Text;
            LoadLstHvByKH(IdKhoaHoc);
        }

        private void dtp_DayTHCabin_EditValueChanged(object sender, EventArgs e)
        {
            LoadTongHopKQ();
        }

        private void gridView_QLKhoaHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            KhoaHoc rowData = gridView_QLKhoaHoc.GetFocusedRow() as KhoaHoc;

            if (rowData != null)
            {
                txt_MaKH.Text = rowData.MaKhoaHoc;
                txt_TenKhoaHoc.Text = rowData.TenKhoaHoc;
                date_NgayKG.Text = rowData.NgayKhaiGiang.ToString("dd/MM/yyyy");
                date_NgayBG.Text = rowData.NgayBeGiang.ToString("dd/MM/yyyy");
                txt_MaBCI.Text = rowData.MaBC1;
                txt_HangDT.Text = rowData.HangDaoTao;
                cbb_TrangThai.Text = rowData.LoaiDaoTao;
            }
        }

        private void gridview_HocVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;

            if (LayoutPanel.Controls.Count < 0)
            {
                LayoutPanel.Controls.Clear();
            }
            else
            {
                LayoutPanel.Controls.Clear();
                UC_Detail uc = new UC_Detail(rowData.NguoiLxID);
                uc.Dock = DockStyle.Fill;
                LayoutPanel.Controls.Add(uc);
            }
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Thêm Khóa Học")
            {
                ThemKhoaHoc();
            }
            else if (button.Caption == "Cập Nhật")
            {
                CapNhatKhoaHoc();
            }
        }

        private void gridview_HocVien_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0) // Chỉ xử lý các dòng dữ liệu
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Hiển thị STT bắt đầu từ 1
                e.Info.ImageIndex = -1; // Tắt biểu tượng mặc định (nếu không muốn hiển thị hình)
            }
        }

        #endregion Event

        #region MenuContext

        private void mn_refresh_Click(object sender, EventArgs e)
        {
            LoadLstHvByKH(IdKhoaHoc);
        }

        private void thêmKhóaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemKhoaHoc();
        }

        private void mn_ReplicationResult_Click(object sender, EventArgs e)
        {
            HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;
            Int64 _nguoiLxID = rowData.NguoiLxID;

            fReplication f = new fReplication(_nguoiLxID);
            f.Show();
        }

        private void mn_PrintAllReport_Click(object sender, EventArgs e)
        {
            string lstNguoiLxID = _DAO.GetLstNguoiLxID(IdKhoaHoc);
            int? khoahocId = null;
            rptPrintResult_EMC f = new rptPrintResult_EMC(lstNguoiLxID, khoahocId, 1);
            f.Show();
        }

        private void mn_TongHopBaoCao_Click(object sender, EventArgs e)
        {
            rptPrintResult_EMC f = new rptPrintResult_EMC(null, IdKhoaHoc, 2);
            f.Show();
        }

        private void mn_HocVienDuDieuKien_Click(object sender, EventArgs e)
        {
        }

        private void mn_SyncTeacher_Click(object sender, EventArgs e)
        {
            List<GiaoVien> lstGiaoVienDAT = _DAO.GetAllGV();
            List<GiaoVien> lstGiaoVienCabin = _DAO.GetAllGV();

            foreach (GiaoVien item in lstGiaoVienDAT.ToList())
            {
                foreach (GiaoVien itemCB in lstGiaoVienCabin)
                {
                    if (item.GiaoVienID == itemCB.GiaoVienID)
                        lstGiaoVienDAT.Remove(item);
                }
            }

            if (lstGiaoVienDAT.Count != 0)
            {
                foreach (GiaoVien item in lstGiaoVienDAT)
                {
                    _DAO.InsertTeacherNew(item);
                }
            }
            List<GvCabin> _lstGvInDat = _DAO.GetListGvByNameKH(TenKH);

            List<HocVien> _lstStudent = _DAO.GetlstHocVienByIdKhoaHoc(IdKhoaHoc);

            foreach (HocVien item in _lstStudent)
            {
                foreach (GvCabin item1 in _lstGvInDat)
                {
                    if (item.MaDK == item1.MaDK)
                    {
                        _DAO.UpdateGv(item1.GiaoVienID, item.MaDK);
                    }
                }
            }
            XtraMessageBox.Show("Đồng bộ giáo viên thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadLstHvByKH((int)cbb_KhoaHocEMC.EditValue);
        }

        #endregion MenuContext

        private void CapNhatKhoaHoc()
        {
            string tenkhoahoc = txt_TenKhoaHoc.Text;
            int loaidaotao;
            string makhoahoc = txt_MaKH.Text;
            if (cbb_TrangThai.Text == "Chưa Đào Tạo")
            {
                loaidaotao = 1;
            }
            else if (cbb_TrangThai.Text == "Đang Đào Tạo")
            {
                loaidaotao = 2;
            }
            else
            {
                loaidaotao = 3;
            }
            _DAO.UpdateStatusKhoaHoc(tenkhoahoc, loaidaotao, makhoahoc);
            Initialize();
            XtraMessageBox.Show("Cập nhật khóa học thành công!", "Thông báo", MessageBoxButtons.OK);
        }

        private void ThemKhoaHoc()
        {
            string filePath = "";
            string MaKH = "";
            string tenkhoahoc = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Xml |*.xml", ValidateNames = true, Multiselect = false })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    try
                    {
                        XDocument xdoc = XDocument.Load(filePath);

                        XElement khoaHocNode = xdoc.Descendants("KHOA_HOC").FirstOrDefault();

                        if (khoaHocNode != null)
                        {
                            KhoaHoc kh = new KhoaHoc();
                            kh.MaKhoaHoc = khoaHocNode.Element("MA_KHOA_HOC")?.Value;
                            MaKH = kh.MaKhoaHoc;
                            kh.TenKhoaHoc = khoaHocNode.Element("TEN_KHOA_HOC")?.Value;
                            //---- gán tên khóa học cho biến
                            tenkhoahoc = kh.TenKhoaHoc;
                            kh.NgayKhaiGiang = Convert.ToDateTime(khoaHocNode.Element("NGAY_KHAI_GIANG")?.Value);
                            kh.MaBC1 = khoaHocNode.Element("MA_BCI")?.Value;
                            kh.NgayBeGiang = Convert.ToDateTime(khoaHocNode.Element("NGAY_BE_GIANG")?.Value);
                            kh.NgaySatHach = Convert.ToDateTime(khoaHocNode.Element("NGAY_SAT_HACH")?.Value);
                            kh.HangDaoTao = khoaHocNode.Element("HANG_GPLX")?.Value;
                            kh.NgayDaoTao = Convert.ToDateTime(khoaHocNode.Element("NGAY_BCI")?.Value);

                            _DAO.InsertKhoaHoc(kh);
                        }

                        int khoahocId = _DAO.GetKhoaHocIdByMaKhoaHoc(MaKH);

                        List<XElement> nguoiLxLst = xdoc.Descendants("NGUOI_LX").ToList();

                        foreach (var item in nguoiLxLst)
                        {
                            HocVien hv = new HocVien();
                            hv.MaDK = item.Element("MA_DK")?.Value;
                            hv.HoTenDem = item.Element("HO_TEN_DEM")?.Value;
                            hv.Ten = item.Element("TEN")?.Value;
                            hv.HoTenHV = item.Element("HO_VA_TEN")?.Value;
                            hv.NgaySinh = DateTime.ParseExact(item.Element("NGAY_SINH")?.Value, "yyyyMMdd", null);
                            hv.GioiTinh = item.Element("GIOI_TINH")?.Value == "F" ? "Nữ" : "Nam";
                            hv.QuocTich = item.Element("MA_QUOC_TICH")?.Value;
                            hv.SoCMT = item.Element("SO_CMT")?.Value;
                            hv.NgayCapCMT = Convert.ToDateTime(item.Element("NGAY_CAP_CMT")?.Value);
                            hv.HangGPLX = (string)item.Element("HO_SO").Element("HANG_GPLX");
                            hv.ImageBase64 = (string)item.Element("HO_SO").Element("ANH_CHAN_DUNG");

                            _DAO.InsertNguoiLxBase(khoahocId, tenkhoahoc, hv, ConvertBase64ToBytes(hv.ImageBase64));
                        }
                        XtraMessageBox.Show("Thêm khóa học thành công!");
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Error: " + ex.Message);
                    }
                }
            } 
        }

        private byte[] ConvertBase64ToBytes(string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (MagickImage image = new MagickImage(imageBytes))
                {
                    image.Format = MagickFormat.Jpeg;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Write(ms);
                        Jpegbytes = ms.ToArray();
                    }
                }
            }
            return Jpegbytes;
        }
    } 
}