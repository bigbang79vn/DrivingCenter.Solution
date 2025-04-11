using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.FReport;
using DrivingCenter.App.Report;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DrivingCenter.App.UserControl
{
    public partial class Uc_CabinEco : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_CabinEco()
        {
            InitializeComponent();
        }

        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private void Uc_CabinEco_Load(object sender, EventArgs e)
        {
            tblLayout_Result.Dock = DockStyle.Fill;
            tblLayout_ThongKe.Dock = DockStyle.Fill;
            tblLayout_KhoaHoc.Dock = DockStyle.Fill;

            dtp_DayTHCabin.DateTime = DateTime.Now;
            dt_TimeStart.DateTime = DateTime.Now;

            Task taskThongKe = taskTongHop();

            GetLstKhoaHocDDT();
            LoadBaiTapCabin();
            GetLstCabin();
            GetAllKhoaHoc();
        }

        private List<KhoaHoc> _lstKhoaHoc;
        private List<HocVien> _lstStudent;
        private string SoCCCD;
        private int IdKhoaHoc;
        private string TenKH;
        private string IdHocVien;
        private byte[] Jpegbytes;
        private int Idtemp;

        public void GetLstKhoaHocDDT()
        {
            List<KhoaHoc> lstKhoaHoc = CabinEco_DAO.Instance.GetKHDangDT();
            cbb_KhoaHocEco.Properties.DataSource = lstKhoaHoc;
            cbb_KhoaHocEco.Properties.DisplayMember = "TenKhoaHoc";
            cbb_KhoaHocEco.Properties.ValueMember = "KhoaHocID";
            if (lstKhoaHoc.Count > 0)
            {
                cbb_KhoaHocEco.EditValue = lstKhoaHoc[0].KhoaHocID;
            }

            mn_ChangeCourse.DropDownItems.Clear();
            //----- Add subitem chuyển khóa học
            foreach (var item in lstKhoaHoc)
            {
                ToolStripItem subItem = new ToolStripMenuItem(item.TenKhoaHoc);
                mn_ChangeCourse.DropDownItems.Add(subItem);
                subItem.Click += MenuItem_Click;
                subItem.Tag = item;
            }
        }


        private void GetLstCabin()
        {
            List<Cabin> lst = CabinEco_DAO.Instance.GetLstCabin();
            cbb_Cabin.Properties.DataSource = lst;
            cbb_Cabin.Properties.DisplayMember = "NameCabin";
            cbb_Cabin.Properties.ValueMember = "IdThietBi";
            if (lst.Count > 0)
            {
                cbb_Cabin.EditValue = lst[0].IdThietBi;
            }

            mn_TransferCabin.DropDownItems.Clear();
            //----- Add subitem Cabin
            foreach (var item in lst)
            {
                ToolStripItem subItem = new ToolStripMenuItem(item.NameCabin);
                mn_TransferCabin.DropDownItems.Add(subItem);
                subItem.Click += MenuItemChangeCabin_Click;
                subItem.Tag = item;
            }
        }

        private void LoadLstHvByKH(int IdKhoaHoc)
        {
            _lstStudent = CabinEco_DAO.Instance.LoadLstHocVienById(IdKhoaHoc);

            grd_HocVien.DataSource = _lstStudent;
        }

        private void cbb_KhoaHocEco_EditValueChanged(object sender, EventArgs e)
        {
            IdKhoaHoc = (int)cbb_KhoaHocEco.EditValue;
            TenKH = cbb_KhoaHocEco.Text;

            if (IdKhoaHoc == 0)
            {
                XtraMessageBox.Show("Không có khóa học đang đào tạo");
            }
            else
            {
                LoadLstHvByKH(IdKhoaHoc);
            }
        }

        private void gridview_HocVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;
            if (rowData != null)
            {
                IdHocVien = rowData.MaDK;
                LoadInfoHocVienById(IdHocVien);
            }
        }

        private void LoadBaiTapCabin()
        {
            List<Exercise> lstBaiTap = CabinEco_DAO.Instance.GetListBaiTap();

            lkDanhMucBaiTap.DataSource = lstBaiTap;
            lkDanhMucBaiTap.ValueMember = "ID";
            lkDanhMucBaiTap.DisplayMember = "TenBaiTap";
        }

        private void LoadInfoHocVienById(string MaDK)
        {
            DataTable data = CabinEco_DAO.Instance.LoadInfoHVByMaDK(MaDK);

            if (data.Rows.Count > 0)
            {
                txt_MaDK.Text = data.Rows[0]["ID"].ToString();
                lbl_HoTen.Text = data.Rows[0]["HoTen"].ToString();
                SoCCCD = data.Rows[0]["SoCMT"].ToString();
                lbl_CCCD.Text = SoCCCD;
                lbl_GioiTinh.Text = data.Rows[0]["GioiTinh"].ToString();
                DateTime ngaysinh = DateTime.Parse(data.Rows[0]["NgaySinh"].ToString());
                string NgaySinh = ngaysinh.ToString("dd/MM/yyyy");
                lbl_NgaySinh.Text = NgaySinh;
                lbl_KhoaHoc.Text = data.Rows[0]["Ten"].ToString();
                lbl_HangDaoTao.Text = data.Rows[0]["HangDaoTao"].ToString();
                byte[] imageBytes = (byte[])data.Rows[0]["Images"];
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    pictureBox1.Image = Image.FromStream(ms, true);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

            List<Result> lstKetQuaTap = CabinEco_DAO.Instance.lstResultById(MaDK);
            grd_Result.DataSource = lstKetQuaTap;
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure delete this result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    int[] selectedRowHandles = gridView.GetSelectedRows();

                    foreach (int rowHandle in selectedRowHandles)
                    {
                        var selectedRow = gridView.GetRow(rowHandle) as Result;
                        if (selectedRow != null)
                        {
                            int ID = (int)selectedRow.DiemDanhNguoiLxID;
                            CabinEco_DAO.Instance.DeleteResultExcer(ID);
                        }
                    }

                    LoadInfoHocVienById(IdHocVien);
                    LoadLstHvByKH(IdKhoaHoc);

                    for (int i = 0; i < gridview_HocVien.RowCount; i++)
                    {
                        if (gridview_HocVien.GetRowCellValue(i, "MaDK").ToString() == IdHocVien)
                        {
                            gridview_HocVien.FocusedRowHandle = i;
                            break;
                        }
                    }
                }
            }
            else if (button.Caption == "Print")
            {
                HocVien hv = gridview_HocVien.GetFocusedRow() as HocVien;
                var resultBC = CabinEco_DAO.Instance.GetDataReportByMaDK(hv.MaDK);

                // Gán DataSource và hiển thị report
                new ReportPrintTool(new rptResultEco { DataSource = resultBC }).ShowPreview();
            }
            else if (button.Caption == "Save")
            {
                int[] selectedRowHandles = gridView.GetSelectedRows();

                foreach (int rowhandle in selectedRowHandles)
                {
                    var selecthandle = gridView.GetRow(rowhandle) as Result;
                    int IDBaiTap = selecthandle.BaiHocCaBinID;
                    Int64 DiemdanhnguoiLxID = selecthandle.DiemDanhNguoiLxID;

                    CabinEco_DAO.Instance.UpdateBaiTap(DiemdanhnguoiLxID, IDBaiTap);

                    LoadInfoHocVienById(IdHocVien);
                }
                for (int i = 0; i < gridview_HocVien.RowCount; i++)
                {
                    if (gridview_HocVien.GetRowCellValue(i, "MaDK").ToString() == IdHocVien)
                    {
                        gridview_HocVien.FocusedRowHandle = i;
                        break;
                    }
                }
            }
        }

        #region MenuContexTrip

        //-----------Chuyển khóa học--------
        private void MenuItem_Click(object sender, EventArgs e)
        {
            HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;

            int Id_KhoaHoc = ((sender as ToolStripMenuItem).Tag as KhoaHoc).KhoaHocID;
            string MaDKNewBySoCCD = CabinEco_DAO.Instance.GetMaDKNew(Id_KhoaHoc, SoCCCD);
            if (MaDKNewBySoCCD == string.Empty)
            {
                XtraMessageBox.Show(string.Format("Không có thông tin học viên trong khóa học {0}", ((sender as ToolStripMenuItem).Tag as KhoaHoc).TenKhoaHoc), "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                if (XtraMessageBox.Show("Are you sure change course this result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    List<Result> lstKetQuaTap = CabinEco_DAO.Instance.lstResultById(rowData.MaDK);

                    foreach (var item in lstKetQuaTap)
                    {
                        int ID = (int)item.DiemDanhNguoiLxID;
                        CabinEco_DAO.Instance.ChangeCourse(Id_KhoaHoc, MaDKNewBySoCCD, ID);
                    }
                    LoadInfoHocVienById(rowData.MaDK);
                    //Result(ID_HocVien);
                    LoadLstHvByKH(IdKhoaHoc);
                }
            }
        }

        //------------------------------------

        //-----------Chuyển Cabin-------------
        private void MenuItemChangeCabin_Click(object sender, EventArgs e)
        {
            HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;

            var cabin = (sender as ToolStripMenuItem).Tag as Cabin;

            if (XtraMessageBox.Show("Are you sure change Cabin this result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                List<Result> lstKetQuaTap = CabinEco_DAO.Instance.lstResultById(rowData.MaDK);

                foreach (var item in lstKetQuaTap)
                {
                    int ID = (int)item.DiemDanhNguoiLxID;
                    CabinEco_DAO.Instance.ChangeCabin(cabin, ID);
                }
                LoadInfoHocVienById(rowData.MaDK);
                //Result(ID_HocVien);
                LoadLstHvByKH(IdKhoaHoc);
            }

            for (int i = 0; i < gridview_HocVien.RowCount; i++)
            {
                if (gridview_HocVien.GetRowCellValue(i, "MaDK").ToString() == rowData.MaDK)
                {
                    gridview_HocVien.FocusedRowHandle = i;
                    break;
                }
            }
        }
        //-------------------------------------

        private void mn_refresh_Click(object sender, EventArgs e)
        {
            LoadLstHvByKH(IdKhoaHoc);
        }

        private void mn_CreateAll_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure add all result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;

                string IdHocVien = rowData.MaDK.ToString();
                int IdCabin = (int)cbb_Cabin.EditValue;

                Cabin selectedRow = cbb_Cabin.GetSelectedDataRow() as Cabin;
                string ghichu = selectedRow.GhiChu;

                string Datecreate = dt_TimeStart.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                List<int> lst = new List<int>() { 1, 3, 4, 5, 6, 7, 8 };
                int LanTap = 1;

                Random rd = new Random();
                bool isB11orB2 = rowData.HangGPLX == "B11" || rowData.HangGPLX == "B2";
                int minute = rd.Next(isB11orB2 ? 18 : 12, isB11orB2 ? 28 : 17);

                CabinEco_DAO.Instance.CreateResult(IdCabin, ghichu, IdHocVien, 2, Datecreate, IdKhoaHoc, LanTap, LanTap, minute);

                while (lst.Count != 0)
                {
                    int ID = CabinEco_DAO.Instance.GetIdMaxByIdHocVien(IdHocVien);
                    DateTime timeout = CabinEco_DAO.Instance.GetTimeOut(ID);

                    int index = rd.Next(lst.Count);
                    int IdBaiTap = lst[index];
                    Datecreate = timeout.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    LanTap++;

                    int minute2 = rd.Next(isB11orB2 ? 18 : 12, isB11orB2 ? 28 : 17);

                    CabinEco_DAO.Instance.CreateResult(IdCabin, ghichu, IdHocVien, IdBaiTap, Datecreate, IdKhoaHoc, LanTap, LanTap, minute2);

                    lst.Remove(IdBaiTap);
                }
                LoadInfoHocVienById(IdHocVien);
                //Result(IdHocVien);
                LoadLstHvByKH(IdKhoaHoc);

                for (int i = 0; i < gridview_HocVien.RowCount; i++)
                {
                    if (gridview_HocVien.GetRowCellValue(i, "MaDK").ToString() == rowData.MaDK)
                    {
                        gridview_HocVien.FocusedRowHandle = i;
                        break;
                    }
                }
            }
        }

        private void mn_Create01Result_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure add 01 result?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                HocVien rowData = gridview_HocVien.GetFocusedRow() as HocVien;

                string IdHocVien = rowData.MaDK.ToString();
                int IdCabin = (int)cbb_Cabin.EditValue;

                Cabin selectedRow = cbb_Cabin.GetSelectedDataRow() as Cabin;
                string ghichu = selectedRow.GhiChu;

                string Datecreate = dt_TimeStart.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                int LanTap = CabinEco_DAO.Instance.GetMaxLanTap(IdHocVien);

                string day = dt_TimeStart.DateTime.ToString("dd/MM/yyyy");
                int count = CabinEco_DAO.Instance.CountLanTapByDay(day, IdHocVien);

                if (count > 0)
                {
                    int MaxID_BaiTap = CabinEco_DAO.Instance.GetMaxIdBaiTap(IdHocVien);
                    int ID = CabinEco_DAO.Instance.GetIdMaxByIdHocVien(IdHocVien);
                    DateTime timeout = CabinEco_DAO.Instance.GetTimeOut(ID);
                    Datecreate = timeout.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    Random rd = new Random();
                    bool isB11orB2 = rowData.HangGPLX == "B11" || rowData.HangGPLX == "B2";
                    int minute = rd.Next(isB11orB2 ? 18 : 12, isB11orB2 ? 28 : 17);

                    if (MaxID_BaiTap < 8)
                    {
                        MaxID_BaiTap++;
                    }
                    LanTap++;
                    CabinEco_DAO.Instance.CreateResult(IdCabin, ghichu, IdHocVien, MaxID_BaiTap, Datecreate, IdKhoaHoc, LanTap, LanTap, minute);
                    LoadInfoHocVienById(IdHocVien);
                    //Result(IdHocVien);
                    LoadLstHvByKH(IdKhoaHoc);
                }
                else
                {
                    LanTap++;
                    CabinEco_DAO.Instance.CreateResult(IdCabin, ghichu, IdHocVien, 2, Datecreate, IdKhoaHoc, LanTap, LanTap, 0);
                    LoadInfoHocVienById(IdHocVien);
                    //Result(IdHocVien);
                    LoadLstHvByKH(IdKhoaHoc);
                }

                for (int i = 0; i < gridview_HocVien.RowCount; i++)
                {
                    if (gridview_HocVien.GetRowCellValue(i, "MaDK").ToString() == rowData.MaDK)
                    {
                        gridview_HocVien.FocusedRowHandle = i;
                        break;
                    }
                }
            }
        }

        private void mn_PrintAllReport_Click(object sender, EventArgs e)
        {
            StringBuilder lstMaDK = new StringBuilder();

            // Lấy các chỉ số của các hàng được chọn
            int[] selectedRowHandles = gridview_HocVien.GetSelectedRows();

            foreach (int rowHandle in selectedRowHandles)
            {
                var cellValue = gridview_HocVien.GetRowCellValue(rowHandle, "MaDK");

                if (cellValue != null)
                {
                    if (lstMaDK.Length > 0)
                        lstMaDK.Append(",");

                    lstMaDK.Append(cellValue.ToString());
                }
            }

            if (string.IsNullOrEmpty(lstMaDK?.ToString()))
            {
                XtraMessageBox.Show("Chọn học viên cần in kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var resultBC = CabinEco_DAO.Instance.GetDataReportByMaDK(lstMaDK.ToString());

            if (resultBC == null || resultBC.Rows.Count == 0)
            {
                XtraMessageBox.Show("Học viên chưa có kết quả học cabin điện tử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Gán DataSource và hiển thị report
            new ReportPrintTool(new rptResultEco { DataSource = resultBC }).ShowPreview();
        }
        private void mn_TongHopBaoCao_Click(object sender, EventArgs e)
        {
            var resultBC = CabinEco_DAO.Instance.GetBaoCaoByKhoaHocID(IdKhoaHoc, 0);

            new ReportPrintTool(new rptBaoCaoTongHopEco { DataSource = resultBC }).ShowPreview();
        }

        private void mn_HocVienDuDieuKien_Click(object sender, EventArgs e)
        {
            var resultBC = CabinEco_DAO.Instance.GetBaoCaoByKhoaHocID(IdKhoaHoc, 1);
            if (resultBC == null || resultBC.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không có học viên đủ điều kiện thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            new ReportPrintTool(new rptBaoCaoTongHopEco { DataSource = resultBC }).ShowPreview();
        }

        private void mn_HocVienKoDuDieuKien_Click(object sender, EventArgs e)
        {
            var resultBC = CabinEco_DAO.Instance.GetBaoCaoByKhoaHocID(IdKhoaHoc, 2);
            if (resultBC == null || resultBC.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không có học viên không đủ điều kiện thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            new ReportPrintTool(new rptBaoCaoTongHopEco { DataSource = resultBC }).ShowPreview();
        }

        private void mn_ThemKhoaHoc_Click(object sender, EventArgs e)
        {
            ThemKhoaHoc();
            GetLstKhoaHocDDT();
        }
        private void mn_SyncTeacher_Click(object sender, EventArgs e)
        {
            List<GiaoVien> lstGiaoVienDAT = DAT_DAO.Instance.GetAllGV();
            List<GiaoVien> lstGiaoVienCabin = CabinEco_DAO.Instance.GetAllGV();

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
                    CabinEco_DAO.Instance.InsertTeacherNew(item);
                }
            }

            List<GvCabin> _lstHvInDat = DAT_DAO.Instance.GetListGvByNameKH(TenKH);
            foreach (HocVien item in _lstStudent)
            {
                foreach (GvCabin item1 in _lstHvInDat)
                {
                    if (item.MaDK == item1.MaDK)
                    {
                        CabinEco_DAO.Instance.UpdateGv(item1.GiaoVienID, item.MaDK);
                    }
                }
            }
            XtraMessageBox.Show("Đồng bộ giáo viên thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadLstHvByKH(IdKhoaHoc);
        }

        #endregion MenuContexTrip

        #region Tổng hợp theo ngày

        private void LoadThCabin()
        {
            string day = dtp_DayTHCabin.DateTime.ToString("dd/MM/yyyy");

            lb_ThongKe.Text = "THỐNG KÊ KẾT QUẢ CABIN NGÀY " + day;

            List<ThongKe> lst = CabinEco_DAO.Instance.LstThongKe(day);

            if (grd_ThongKe.InvokeRequired)
            {
                grd_ThongKe.Invoke(new Action(() => {
                    grd_ThongKe.DataSource = lst;
                }));
            }
            else
            {
                grd_ThongKe.DataSource = lst;
            }
        }

        private async Task taskTongHop()
        {
            Task task = new Task(
                () => { LoadThCabin(); }
            );
            task.Start();
            await task;
        }

        private void btn_ViewTongHop_Click(object sender, EventArgs e)
        {
            LoadThCabin();
        }

        #endregion Tổng hợp theo ngày

        #region Quản lý khóa học

        private void GetAllKhoaHoc()
        {
            _lstKhoaHoc = CabinEco_DAO.Instance.GetAllKhoaHoc();
            grd_QuanLyKhoaHoc.DataSource = _lstKhoaHoc;
        }

        private void gridView_QLKhoaHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            KhoaHoc rowData = gridView_QLKhoaHoc.GetFocusedRow() as KhoaHoc;

            Idtemp = rowData.KhoaHocID;

            if (rowData != null)
            {
                txt_MaKH.Text = rowData.MaKhoaHoc;
                txt_TenKhoaHoc.Text = rowData.TenKhoaHoc;
                date_NgayKG.Text = rowData.NgayKhaiGiang.ToString("dd/MM/yyyy");
                date_NgayBG.Text = rowData.NgayBeGiang.ToString("dd/MM/yyyy");
                txt_MaBCI.Text = rowData.MaBC1;
                txt_HangDT.Text = rowData.HangDaoTao;
                check_Status.EditValue = rowData.Status;
            }
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Cập Nhật")
            {
                CapNhatKhoaHoc();
            }
            else if (button.Caption == "Thêm Khóa Học")
            {
                ThemKhoaHoc();
                GetLstKhoaHocDDT();
            }
        }

        private void CapNhatKhoaHoc()
        {
            string tenkhoahoc = txt_TenKhoaHoc.Text;
            string hangdaotao = txt_HangDT.Text;
            int status = check_Status.Checked ? status = 1 : status = 0;

            CabinEco_DAO.Instance.UpdateKhoaHoc(Idtemp, tenkhoahoc, hangdaotao, status);
            GetAllKhoaHoc();
            GetLstKhoaHocDDT();
        }

        private void ThemKhoaHoc()
        {
            string filePath = "";
            string MaKH = "";
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
                            kh.NgayKhaiGiang = Convert.ToDateTime(khoaHocNode.Element("NGAY_KHAI_GIANG")?.Value);
                            kh.MaBC1 = khoaHocNode.Element("MA_BCI")?.Value;
                            kh.NgayBeGiang = Convert.ToDateTime(khoaHocNode.Element("NGAY_BE_GIANG")?.Value);
                            kh.NgaySatHach = Convert.ToDateTime(khoaHocNode.Element("NGAY_SAT_HACH")?.Value);
                            kh.HangDaoTao = khoaHocNode.Element("HANG_GPLX")?.Value;
                            kh.NgayDaoTao = Convert.ToDateTime(khoaHocNode.Element("NGAY_BCI")?.Value);

                            CabinEco_DAO.Instance.InsertKhoaHoc(kh);
                        }

                        int khoahocId = CabinEco_DAO.Instance.GetKhoaHocIdByMaKhoaHoc(MaKH);

                        List<XElement> nguoiLxLst = xdoc.Descendants("NGUOI_LX").ToList();

                        int STT = 0;
                        foreach (var item in nguoiLxLst)
                        {
                            HocVien hv = new HocVien();
                            hv.MaDK = item.Element("MA_DK")?.Value;
                            hv.HoTenDem = item.Element("HO_TEN_DEM")?.Value;
                            hv.Ten = item.Element("TEN")?.Value;
                            hv.HoTenHV = item.Element("HO_VA_TEN")?.Value;
                            hv.NgaySinh = DateTime.ParseExact(item.Element("NGAY_SINH")?.Value, "yyyyMMdd", null);
                            hv.GioiTinh = item.Element("GIOI_TINH")?.Value == "F" ? "Nữ" : "Nam";
                            hv.SoCMT = item.Element("SO_CMT")?.Value;
                            hv.NgayCapCMT = Convert.ToDateTime(item.Element("NGAY_CAP_CMT")?.Value);
                            hv.HangGPLX = (string)item.Element("HO_SO").Element("HANG_GPLX");
                            hv.ImageBase64 = (string)item.Element("HO_SO").Element("ANH_CHAN_DUNG");
                            STT++;

                            CabinEco_DAO.Instance.InsertNguoiLxBase(khoahocId, STT, hv, ConvertBase64ToBytes(hv.ImageBase64), STT);
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

        #endregion Quản lý khóa học

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

        private void grd_HocVien_MouseDown(object sender, MouseEventArgs e)
        {
            // Kiểm tra chuột phải
            if (e.Button == MouseButtons.Right)
            {
                // Kiểm tra tổ hợp Shift + Ctrl
                if (Control.ModifierKeys == (Keys.Shift | Keys.Control))
                {
                    // Hiển thị các item ẩn
                    contextMenuStrip_DTT.Items["toolStripSeparator4"].Visible = true;
                    contextMenuStrip_DTT.Items["mn_CreateAll"].Visible = true;
                    contextMenuStrip_DTT.Items["mn_Create01Result"].Visible = true;
                }
                else
                {
                    // Ẩn các item ẩn khi không nhấn Shift + Ctrl
                    contextMenuStrip_DTT.Items["toolStripSeparator4"].Visible = false;
                    contextMenuStrip_DTT.Items["mn_CreateAll"].Visible = false;
                    contextMenuStrip_DTT.Items["mn_Create01Result"].Visible = false;
                }

                // Hiển thị ContextMenuStrip tại vị trí chuột
                contextMenuStrip_DTT.Show(grd_HocVien, e.Location);
            }
        }
    }
}