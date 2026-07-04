using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.UserControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        private readonly CabinI2E_DAO _cabinI2E = new CabinI2E_DAO();
        private void fReplication_Load(object sender, EventArgs e)
        {
            lstKhoaHocDDT();
            LoadThietBi();
            LoadlstKQ(_nguoiLxId);
            dt_TimeStart.DateTime = DateTime.Now;
        }

        private int KhoaHocID;

        private void lstKhoaHocDDT()
        {
            List<KhoaHoc> lstKhoaHoc = _cabinI2E.GetAllKhoaHoc()
                .Where(x => x.LoaiDaoTao == "Đang Đào Tạo")
                .OrderBy(c => c.TenKhoaHoc)
                .ToList();
            
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
            List<Cabin> lst = _cabinI2E.GetLstCabin();
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
            List<HocVien> lstStudent = _cabinI2E.GetlstHocVienByIdKhoaHoc(IdKhoaHoc);

            Uc_CabinEMC uCResult_I2E = new Uc_CabinEMC();
            uCResult_I2E.CheckResult(lstStudent);

            grid_HocVien.DataSource = lstStudent;
        }

        private void LoadlstKQ(Int64 NguoiLxID)
        {
            List<Result> lst = _cabinI2E.GetResultByNguoiLxID(NguoiLxID);

            grid_BaiTapNguon.DataSource = lst;
        }

        private void LoadResultByNguoiLxID(HocVien hv)
        {
            List<Result> lstKetQuaTap = _cabinI2E.GetResultByNguoiLxID(hv.NguoiLxID);

            grid_Result.DataSource = lstKetQuaTap;

            int totalsecond = (int)_cabinI2E.GetTongThoiGianByNguoiLxID(hv.NguoiLxID);
            TimeSpan t = TimeSpan.FromSeconds(totalsecond);
            lb_time.Text = t.ToString(@"hh\:mm\:ss");

            lb_KetQua.Text = _cabinI2E.CheckDK(hv.NguoiLxID, hv.MaKH);
        }

        private void cbb_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            KhoaHocID = (int)cbb_KhoaHoc.EditValue;
            LoadLstHvByKH((int)KhoaHocID);

        }

        private void gridView_HocVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            HocVien rowData = gridView_HocVien.GetFocusedRow() as HocVien;

            LoadResultByNguoiLxID(rowData);
        }

        private void btn_InserResult_Click(object sender, EventArgs e)
        {
            HocVien hv = gridView_HocVien.GetFocusedRow() as HocVien;

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
                    Int64 diemdanhnguoilxId = _cabinI2E.GetMaxDiemdanhnguoilxByNguoiLxID(hv.NguoiLxID);
                    int dayOld = _cabinI2E.GetDayDiemdanhnguoilxByNguoiLxID(hv.NguoiLxID);
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
                        re.ThoiDiemBatDau = _cabinI2E.GetTimeOut(diemdanhnguoilxId, hv.NguoiLxID);
                    }
                    re.TotalSecond = (double)gridView_BaiTapNguon.GetRowCellValue(rowHandle, "TotalSecond");
                    re.QuangDuong = (double)gridView_BaiTapNguon.GetRowCellValue(rowHandle, "QuangDuong");
                    re.PathVideo = gridView_BaiTapNguon.GetRowCellValue(rowHandle, "PathVideo").ToString();
                    int IdThietBi = (int)cbb_Cabin.EditValue;

                    _cabinI2E.InsertData(re, hv.NguoiLxID, IdThietBi, KhoaHocID);

                    _cabinI2E.UpdateTimeExerToNguoiLx(hv.NguoiLxID);
                    LoadResultByNguoiLxID(hv);
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