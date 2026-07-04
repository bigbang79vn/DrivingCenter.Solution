using DrivingCenter.App.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmQuaTrinhDaoTao : DevExpress.XtraEditors.XtraForm
    {
        private readonly DAT_DAO _DAO = new DAT_DAO();
        private readonly IMG_DAO _IMG_DAO = new IMG_DAO();
        private string _madk;
        public frmQuaTrinhDaoTao(string madk)
        {
            InitializeComponent();
            _madk = madk;
        }

        private async void frmQuaTrinhDaoTao_Load(object sender, EventArgs e)
        {
            await LoadThongTinHocVien(_madk);
            await LoadKetQuaDaoTao(_madk);

        }
        private async Task LoadThongTinHocVien(string madk)
        {
            if (madk == null)
            {
                MessageBox.Show("Mã đăng ký không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var result = _DAO.GetInfoHocVienByMaDK(madk);
                lb_HoTen.Text = result.HoVaTen;
                lb_NgaySinh.Text = result.NgaySinh.ToString("dd/MM/yyyy");
                txt_maDK.Text = result.MaDK;
                txt_KhoaHoc.Text = result.TenKH;
                txt_HangDT.Text = result.HangGPLX;

                var img = _IMG_DAO.GetImgByMaDK(madk);
                if (img.Rows.Count > 0 && img.Rows[0]["Anh"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])img.Rows[0]["Anh"];
                    using (var ms = new System.IO.MemoryStream(imageData))
                    {
                        pic_Avatar.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
            }
        }
        public async Task LoadKetQuaDaoTao(string madk)
        {
            if (madk == null)
            {
                MessageBox.Show("Mã đăng ký không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var result = _DAO.GetKetQuaDaoTaoByMaDK(madk);
                txt_QuangDuongTH.DataSource = result;
                GridHelper.EnableRowNumbering(grdView_KetQuaDaoTao);

                if (result == null || result.Rows.Count == 0)
                {
                    txt_QuangDuong.Text = "0 Km";
                    txt_ThoiGian.Text = "0 Giờ";
                    txt_GioDem.Text = "0 Giờ";
                    txt_KmDem.Text = "0 Km";
                    return;
                }

                var rows = result.AsEnumerable();

                double tongQuangDuong = rows.Sum(x => x.Field<double?>("TongQuangDuongLD_Phien") ?? 0);
                txt_QuangDuong.Text = tongQuangDuong.ToString("N2") + " Km";

                double tongThoiGianLaiXe = rows.Sum(x => x.Field<double?>("Time") ?? 0);
                txt_ThoiGian.Text = (tongThoiGianLaiXe / 3600).ToString("N2") + " Giờ";


                #region Điều kiện hạng xe

                List<string> hangXeHopLe;

                if (txt_HangDT.Text == "B.01")
                {
                    hangXeHopLe = new List<string> { "B.01" };
                }
                else
                {
                    hangXeHopLe = new List<string> { "B", "C1" };
                }

                #endregion

                var duLieuBanDem = rows.Where(x =>
                {
                    DateTime? thoiDiem = x.Field<DateTime?>("ThoiDiem");

                    if (!thoiDiem.HasValue)
                        return false;

                    string hangXe = x.Field<string>("HangXe");

                    return thoiDiem.Value.Hour >= 18
                           && hangXeHopLe.Contains(hangXe);
                });

                double tongThoiGianBanDem = duLieuBanDem.Sum(x => x.Field<double?>("Time") ?? 0);
                txt_GioDem.Text = (tongThoiGianBanDem / 3600).ToString("N2") + " Giờ";


                double tongQuangDuongBanDem = duLieuBanDem.Sum(x => x.Field<double?>("TongQuangDuongLD_Phien") ?? 0);
                txt_KmDem.Text = $"{tongQuangDuongBanDem:N2} Km";


                double tongThoiGianSTD = rows
                    .Where(x => x.Field<string>("HangXe") == "B.01")
                    .Sum(x => x.Field<double?>("Time") ?? 0);

                txt_GioSTD.Text = (tongThoiGianSTD / 3600).ToString("N2") + " Giờ";

                double tongQuangDuongSTD = rows
                    .Where(x => x.Field<string>("HangXe") == "B.01")
                    .Sum(x => x.Field<double?>("TongQuangDuongLD_Phien") ?? 0);
                txt_KmSTD.Text = tongQuangDuongSTD.ToString("N2") + " Km";
            }
        }

        private void txt_QuangDuongTH_DoubleClick(object sender, EventArgs e)
        {
            Guid maPhien = (Guid)grdView_KetQuaDaoTao.GetFocusedRowCellValue("DiemDanhNguoiLxID");
            if (maPhien == Guid.Empty)
            {
                return;
            }
            else
            {
                frmDetailSession chiTietDaoTao = new frmDetailSession(maPhien.ToString());
                chiTietDaoTao.ShowDialog();
            }
        }

        private void grp_KetQuaDaoTao_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption != "ReLoad")
                return;

            LoadKetQuaDaoTao(_madk);
        }
    }
}