using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.UserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class fReplication : DevExpress.XtraEditors.XtraForm
    {
        private Int64 _nguoiLxId;
        public fReplication()
        {
            InitializeComponent();
        }

        public fReplication(Int64 nguoiLxId) : this()
        {
            _nguoiLxId = nguoiLxId;
        }

        private void fReplication_Load(object sender, EventArgs e)
        {
            GetLstKhoaHoc();
            LoadThietBi();
            LoadlstKQ(_nguoiLxId);
            dt_TimeStart.DateTime = DateTime.Now;
        }

        private Int64 NguoiLxId;
        private int KhoaHocID;
        private void GetLstKhoaHoc()
        {
            List<KhoaHoc> lstKhoaHoc = CabinI2E_DAO.Instance.GetlstKhoaHocDDT();
            cbb_KhoaHoc.Properties.DataSource = lstKhoaHoc;
            cbb_KhoaHoc.Properties.DisplayMember = "TenKhoaHoc";
            cbb_KhoaHoc.Properties.ValueMember = "KhoaHocID";
            if (lstKhoaHoc.Count > 0)
            {
                cbb_KhoaHoc.EditValue = lstKhoaHoc[0].KhoaHocID;
            }
        }

        private void LoadThietBi()
        {
            List<Cabin> lst = CabinI2E_DAO.Instance.GetLstCabin();
            cbb_Cabin.Properties.DataSource = lst;
            cbb_Cabin.Properties.DisplayMember = "NameCabin";
            cbb_Cabin.Properties.ValueMember = "IdThietBi";
            if (lst.Count > 0)
            {
                cbb_Cabin.EditValue = lst[0].IdThietBi;
            }
        }

        private void LoadLstHvByKH(int IdKhoaHoc)
        {
            List<HocVien> lstStudent = CabinI2E_DAO.Instance.GetlstHocVienByIdKhoaHoc(IdKhoaHoc);

            Uc_CabinEMC uCResult_I2E = new Uc_CabinEMC();
            uCResult_I2E.CheckResult(lstStudent);

            grid_HocVien.DataSource = lstStudent;
        }

        private void LoadlstKQ(Int64 NguoiLxID)
        {
            List<Result> lst = CabinI2E_DAO.Instance.GetResultByNguoiLxID(NguoiLxID);

            grid_BaiTapNguon.DataSource = lst;
        }

        private void LoadResultByNguoiLxID(Int64 NguoiLxID)
        {
            List<Result> lstKetQuaTap = CabinI2E_DAO.Instance.GetResultByNguoiLxID(NguoiLxID);

            grid_Result.DataSource = lstKetQuaTap;

            int totalsecond = (int)CabinI2E_DAO.Instance.GetTongThoiGianByNguoiLxID(NguoiLxId);
            TimeSpan t = TimeSpan.FromSeconds(totalsecond);
            lb_time.Text = t.ToString(@"hh\:mm\:ss");

            string ketluan = CabinI2E_DAO.Instance.CheckDK(NguoiLxId);
            lb_KetQua.Text = ketluan;

        }

        private void cbb_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            KhoaHocID = (int)cbb_KhoaHoc.EditValue;
            LoadLstHvByKH((int)KhoaHocID);

        }

        private void gridView_HocVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            HocVien rowData = gridView_HocVien.GetFocusedRow() as HocVien;
            NguoiLxId = Convert.ToInt64(rowData.NguoiLxID);

            LoadResultByNguoiLxID(NguoiLxId);
        }

        private void btn_InserResult_Click(object sender, EventArgs e)
        {
            HocVien rowData = gridView_HocVien.GetFocusedRow() as HocVien;
            Int64 NguoiLxID = rowData.NguoiLxID;
            string MaDK = rowData.MaDK;

            // Lấy các chỉ số của các hàng được chọn
            int[] selectedRowHandles = gridView_BaiTapNguon.GetSelectedRows();
            if (!selectedRowHandles.Any())
            {
                XtraMessageBox.Show("Không có kết quả cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                foreach (int rowHandle in selectedRowHandles)
                {
                    Result re = new Result();
                    re.BaiHocCaBinID = (int)gridView_BaiTapNguon.GetRowCellValue(rowHandle, "BaiHocCaBinID");
                    Int64 diemdanhnguoilxId = CabinI2E_DAO.Instance.GetMaxDiemdanhnguoilxByNguoiLxID(NguoiLxID);
                    int dayOld = CabinI2E_DAO.Instance.GetDayDiemdanhnguoilxByNguoiLxID(NguoiLxID);
                    int dayNew = (dt_TimeStart.DateTime).Day;
                    if (diemdanhnguoilxId == 0)
                    {
                        re.ThoiDiemBatDau = dt_TimeStart.DateTime;
                    }
                    else if (dayOld != dayNew)
                    {
                        re.ThoiDiemBatDau = dt_TimeStart.DateTime;
                    }
                    else
                    {
                        re.ThoiDiemBatDau = CabinI2E_DAO.Instance.GetTimeOut(diemdanhnguoilxId, NguoiLxID);
                    }
                    re.TotalSecond = (double)gridView_BaiTapNguon.GetRowCellValue(rowHandle, "TotalSecond");
                    re.QuangDuong = (double)gridView_BaiTapNguon.GetRowCellValue(rowHandle, "QuangDuong");
                    re.PathVideo = gridView_BaiTapNguon.GetRowCellValue(rowHandle, "PathVideo").ToString();
                    int IdThietBi = (int)cbb_Cabin.EditValue;

                    CabinI2E_DAO.Instance.InsertData(re, NguoiLxID, IdThietBi, KhoaHocID);

                    CabinI2E_DAO.Instance.UpdateTimeExerToNguoiLx(NguoiLxId);
                    LoadResultByNguoiLxID(NguoiLxId);
                }
                XtraMessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}