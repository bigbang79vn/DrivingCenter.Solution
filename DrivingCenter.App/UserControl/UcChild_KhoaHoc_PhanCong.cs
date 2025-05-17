using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.CSDT;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DTO.GPLX;
using NPOI.HSSF.Record.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.UserControl
{
    public partial class UcChild_KhoaHoc_PhanCong : DevExpress.XtraEditors.XtraUserControl
    {
        public UcChild_KhoaHoc_PhanCong()
        {
            InitializeComponent();
        }

        private void UcChild_KhoaHoc_PhanCong_Load(object sender, EventArgs e)
        {
            xeTapLais = AppBee_DAO.Instance.GetAllXeTapLai();
            GetAllKhoaHoc();

            cbb_KhoaHocXeDaoTao.EditValueChanged += cbb_KhoaHocXeDaoTao_EditValueChanged;
            gridViewRight.RowCellStyle += gridViewRight_RowCellStyle;

        }

        List<XeTapLai> xeTapLais;
        List<XeTapLai> lslistXeConTrong;
        List<XeTapLai> xeB11ConTrong;
        Dictionary<int, int?> giaoVienXe = new Dictionary<int, int?>();
        List<Schedule> data;
        DataRow row;
        private string MaKhDuKien;
        private int? currentGiaoVienId = null; // Biến lưu giáo viên đang focus

        //private void GetKhoaHocDuKien()
        //{
        //    List<KhoaHocGPLX> lst = GPLX_DAO.Instance.GetlstKH();
        //    cbb_KhoaHocDuKien.Properties.DataSource = lst;
        //    cbb_KhoaHocDuKien.Properties.DisplayMember = "TenKHGplx";
        //    cbb_KhoaHocDuKien.Properties.ValueMember = "MaKHGplx";

        //    if (lst.Count > 0) cbb_KhoaHocDuKien.EditValue = lst[0].MaKHGplx;
        //}

        private void GetAllKhoaHoc()
        {
            List<KhoaHocGPLX> lst = GPLX_DAO.Instance.GetAllKhoaHoc();
            cbb_KhoaHocDuKien.Properties.DataSource = lst;
            cbb_KhoaHocDuKien.Properties.DisplayMember = "TenKHGplx";
            cbb_KhoaHocDuKien.Properties.ValueMember = "MaKHGplx";

            if (lst.Count > 0)
            {
                cbb_KhoaHocDuKien.EditValue = lst[0].MaKHGplx;
            }
        }

        private void Get_DmGvTrong(string loailop)
        {
            var data = AppBee_DAO.Instance.Get_GvTrong(loailop).Where(x => x.SuDung == true).ToList();
            listLeft = new BindingList<GiaoVien>(data);
            gridControlLeft.DataSource = listLeft;

            listRight = new BindingList<GiaoVien>();
            gridControlRight.DataSource = listRight;
        }

        private void Get_DmXeDaoTaoConTrong(string loailop)
        {
            lslistXeConTrong = AppBee_DAO.Instance.Get_XeTapLaiTrong(loailop);
            cbb_KhoaHocXeDaoTao.Properties.DataSource = lslistXeConTrong;
            cbb_KhoaHocXeDaoTao.Properties.DisplayMember = "BienSoXe";
            cbb_KhoaHocXeDaoTao.Properties.ValueMember = "VehicleID";
            cbb_KhoaHocXeDaoTao.Properties.NullText = "-- Chọn giá trị --";
        }

        private void Get_DmXeB11ConTrong(string loailop, DateTime ngayTH)
        {
            xeB11ConTrong = AppBee_DAO.Instance.Get_XeB11ConTrong(loailop, ngayTH);
            cbb_KhoaHocXeB11.Properties.DataSource = xeB11ConTrong;
            cbb_KhoaHocXeB11.Properties.DisplayMember = "BienSoXe";
            cbb_KhoaHocXeB11.Properties.ValueMember = "VehicleID";
            cbb_KhoaHocXeB11.Properties.NullText = "-- Chọn giá trị --";
        }

        private void cbb_KhoaHocDuKien_EditValueChanged(object sender, EventArgs e)
        {
            var obj = cbb_KhoaHocDuKien.GetSelectedDataRow() as KhoaHocGPLX;
            MaKhDuKien = obj.MaKHGplx;

            groupControl_XeSTD.Enabled = obj.HangDT != "B.01";

            string loailop = !string.IsNullOrEmpty(MaKhDuKien) && MaKhDuKien.Length >= 3 && int.TryParse(MaKhDuKien.Substring(MaKhDuKien.Length - 3), out int num) ? (num % 2 == 0 ? "Ngoài giờ" : "Trong giờ") : "Không xác định";
            Get_DmGvTrong(loailop);
            Get_DmXeDaoTaoConTrong(loailop);
            
            lbl_ThongTinPhanCong.Text = $"THÔNG TIN PHÂN CÔNG ({obj.TenKHGplx})";
            Renew(MaKhDuKien);

            DateTime ngayTH = ((DateTime)row["NgayKG"]).AddDays(25);
            dateEdit_NgayThucHanh.DateTime = ngayTH;
            Get_DmXeB11ConTrong(loailop, ngayTH);

        }

        private void Renew(string MaKhDuKien)
        {
            if (!string.IsNullOrEmpty(MaKhDuKien))
            {
                row = GPLX_DAO.Instance.KhoaHocSelected(MaKhDuKien);
                txt_MaKH.Text = row["MaKH"].ToString();

                txt_TenKH.Text = row["TenKH"].ToString();
                txt_HangDT.Text = row["HangDT"].ToString();
                dateEdit_NgayKG.DateTime = (DateTime)row["NgayKG"];
                dateEdit_NgayBG.DateTime = (DateTime)row["NgayBG"];
                txt_SoHV.Text = row["TongSoHV"].ToString();
                txt_ThoiGianDT.Text = row["ThoiGianDT"].ToString();
                memoEdit_GhiChu.Text = row["GhiChu"].ToString();

                dateEdit_Begin.EditValue = ((DateTime)row["NgayKG"]).AddDays(25);
                dateEdit_End.EditValue = (DateTime)row["NgayBG"];

                data = AppBee_DAO.Instance.GetKhoaHocDaPhanCong(MaKhDuKien);
                gridCtrl_BoTri.DataSource = data;

                giaoVienXe = data.ToDictionary(x => x.GiaoVienId, x => x.IdXeTap);

                listRight.Clear();
                var giaoViens = AppBee_DAO.Instance.Get_GiaoVienByMaKH(MaKhDuKien);
                listRight = new BindingList<GiaoVien>(giaoViens);
                gridControlRight.DataSource = listRight;
            }
        }

        BindingList<GiaoVien> listLeft = new BindingList<GiaoVien>();
        BindingList<GiaoVien> listRight = new BindingList<GiaoVien>();

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewLeft.GetSelectedRows();
            foreach (var rowHandle in selectedRows.Reverse())
            {
                var giaoVien = gridViewLeft.GetRow(rowHandle) as GiaoVien;
                if (giaoVien != null)
                {
                    listRight.Add(giaoVien);
                    listLeft.Remove(giaoVien);
                }
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewRight.GetSelectedRows();
            foreach (var rowHandle in selectedRows.Reverse())
            {
                var giaoVien = gridViewRight.GetRow(rowHandle) as GiaoVien;
                if (giaoVien != null)
                {
                    listLeft.Add(giaoVien);
                    listRight.Remove(giaoVien);
                    giaoVienXe.Remove(giaoVien.GiaoVienID);
                }
            }
        }

        private void gridViewLeft_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.ImageIndex = -1;
            }
        }

        private void gridViewRight_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.ImageIndex = -1;
            }
        }

        private void gridViewRight_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GiaoVien selectedGiaoVien = gridViewRight.GetFocusedRow() as GiaoVien;

            if (selectedGiaoVien != null)
            {
                txt_MaGV.Text = selectedGiaoVien.MaGV;
                txt_HoTenGV.Text = selectedGiaoVien.HoTenGV;
                date_NgaySinh.EditValue = selectedGiaoVien.NgaySinh;
                spinEdit_SoHV.Value = spinEdit_SoHV.Value;

                currentGiaoVienId = selectedGiaoVien.GiaoVienID;
                OnFocusChange(selectedGiaoVien.GiaoVienID);
            }
        }

        private void cbb_KhoaHocXeDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            if (currentGiaoVienId != null && cbb_KhoaHocXeDaoTao.EditValue != null)
            {
                int xeId = (int)cbb_KhoaHocXeDaoTao.EditValue;
                OnSelectXe(currentGiaoVienId.Value, xeId);
                var xeInfo = xeTapLais.FirstOrDefault(x => x.VehicleID == xeId);
                txtBienSoXe.Text = xeInfo.BienSoXe;
                txt_MaXe.Text = xeInfo.MaXe;
            }
        }

        void OnSelectXe(int giaoVienId, int xeId)
        {
            // Nếu giáo viên này đã có xe cũ => Trả xe cũ về lại danh sách xe trống
            if (giaoVienXe.TryGetValue(giaoVienId, out int? xeCuId) && xeCuId != null)
            {
                // Nếu xe cũ khác xe mới thì trả lại xe cũ
                if (xeCuId != xeId)
                {
                    var xeCu = xeTapLais.FirstOrDefault(x => x.VehicleID == xeCuId);
                    if (xeCu != null && !lslistXeConTrong.Any(x => x.VehicleID == xeCuId))
                    {
                        lslistXeConTrong.Add(xeCu);
                    }
                }
            }

            // Kiểm tra nếu xe mới đã được chọn bởi giáo viên khác
            if (giaoVienXe.Values.Contains(xeId))
            {
                var giaoVienKhac = giaoVienXe.FirstOrDefault(x => x.Value == xeId);
                if (!giaoVienKhac.Equals(default(KeyValuePair<int, int>)))
                {
                    int? maGiaoVienDaChon = giaoVienKhac.Key;

                    var xeInfo = xeTapLais.FirstOrDefault(x => x.VehicleID == xeId);
                    txtBienSoXe.Text = xeInfo?.BienSoXe?.ToString();
                    txt_MaXe.Text = xeInfo?.MaXe?.ToString();
                }
                // Không cho chọn lại xe đã bị người khác lấy
                return;
            }

            // Gán xe mới
            giaoVienXe[giaoVienId] = xeId;

            // Xóa xe mới khỏi danh sách xe còn trống
            lslistXeConTrong.RemoveAll(x => x.VehicleID == xeId);

            // Cập nhật lại ComboBox datasource
            cbb_KhoaHocXeDaoTao.EditValueChanged -= cbb_KhoaHocXeDaoTao_EditValueChanged;

            cbb_KhoaHocXeDaoTao.EditValue = null;
            cbb_KhoaHocXeDaoTao.Properties.DataSource = null;
            cbb_KhoaHocXeDaoTao.Properties.DataSource = lslistXeConTrong;
            cbb_KhoaHocXeDaoTao.Properties.DisplayMember = "BienSoXe";
            cbb_KhoaHocXeDaoTao.Properties.ValueMember = "VehicleID";
            cbb_KhoaHocXeDaoTao.Properties.NullText = "-- Chọn xe --";

            cbb_KhoaHocXeDaoTao.EditValueChanged += cbb_KhoaHocXeDaoTao_EditValueChanged;

            // Cập nhật lại giao diện
            gridViewRight.RefreshData();

        }

        void OnFocusChange(int giaoVienId)
        {
            if (giaoVienXe.ContainsKey(giaoVienId))
            {
                cbb_KhoaHocXeDaoTao.EditValue = giaoVienXe[giaoVienId];
                txtBienSoXe.Text = xeTapLais.FirstOrDefault(x => x.VehicleID == giaoVienXe[giaoVienId]).BienSoXe.ToString();
                var result = data.FirstOrDefault(x => x.GiaoVienId == giaoVienId);

                dateEdit_Begin.EditValue = result?.NgayHL ?? (DateTime)row["NgayKG"];
                dateEdit_End.EditValue = result?.NgayHetHL ?? (DateTime)row["NgayBG"];
            }
            else
            {
                cbb_KhoaHocXeDaoTao.EditValue = null;
                txtBienSoXe.Text = "";
            }
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;

            if (button.Caption == "Save")
            {
                SaveButton(giaoVienXe, data);
                MaKhDuKien = cbb_KhoaHocDuKien.EditValue?.ToString();
                Renew(MaKhDuKien);
            }
            else if (button.Caption == "Refresh")
            {
                MaKhDuKien = cbb_KhoaHocDuKien.EditValue?.ToString();
                Renew(MaKhDuKien);
            }
            else if (button.Caption == "Export")
            {
                //----
            }
        }

        private void SaveButton(Dictionary<int, int?> giaoVienXe, List<Schedule> data)
        {
            int soHocVien = (int)spinEdit_SoHV.Value;
            DateTime ngayHieuLuc = dateEdit_Begin.DateTime;
            DateTime ngayHetHieuLuc = dateEdit_End.DateTime;

            // Cập nhật hoặc thêm mới
            foreach (var item in giaoVienXe)
            {
                var existing = data.FirstOrDefault(s => s.GiaoVienId == item.Key);

                int giaoVienId = item.Key;
                int? xeMoi = item.Value;

                if (existing != null)
                {
                    //int? xeCu = giaoVienXe[giaoVienId];
                    //if (existing.IdXeTap != xeMoi)
                        // Update
                        AppBee_DAO.Instance.UpdateKhoaHocGiaoVien(existing.Id, MaKhDuKien, giaoVienId, xeMoi, soHocVien, ngayHieuLuc, ngayHetHieuLuc);
                    
                }
                else
                {
                    // Insert
                    AppBee_DAO.Instance.InsertKhoaHocGiaoVien(MaKhDuKien, giaoVienId, xeMoi, soHocVien, ngayHieuLuc, ngayHetHieuLuc);
                }
            }

            // Xóa những phần tử trong data không có trong giaoVienXe
            data.Where(s => !giaoVienXe.ContainsKey(s.GiaoVienId))
                .ToList()
                .ForEach(item => AppBee_DAO.Instance.DeleteKhoaHocGiaoVien(item));

        }

        private void gridViewRight_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                var gv = gridViewRight.GetRow(e.RowHandle) as GiaoVien;
                if (gv != null && giaoVienXe.ContainsKey(gv.GiaoVienID))
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        private void gridView_BoTri_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.ImageIndex = -1;
            }
        }

        private void groupControl_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            XtraMessageBox.Show("Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
