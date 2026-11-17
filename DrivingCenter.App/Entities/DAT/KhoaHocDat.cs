using System;

namespace DrivingCenter.App.Entities.DAT
{
    public class KhoaHocDat
    {
        public int MonthID { get; set; }
        public int KhoaHocID { get; set; }
        public string MaKH { get; set; }
        public string MaCSDT { get; set; }
        public string TenKH { get; set; }
        public DateTime NgayKG { get; set; }
        public DateTime NgayBG { get; set; }
        public DateTime NgaySatHach { get; set; }
        public DateTime CreateDate { get; set; }
        public string MABC1 { get; set; }
        public bool SendTongCuc { get; set; }
        public string HangDaoTao { get; set; }
        public DateTime NgayDaoTao { get; set; }
        public bool DaoTaoNoiBo { get; set; }
        public bool Del { get; set; }
        public int SoHVDuDK { get; set; }
        public int TongHocVien { get; set; }

        public KhoaHocDat()
        {

        }
    }
}

namespace DrivingCenter.App.DTO.DAT
{
    /// <summary>
    /// Bảng KhoaHocDangDT có cùng cấu trúc với bảng KhoaHoc.
    /// Giữ lại tên cũ để các màn hình đang dùng (Uc_DAT, DAT_DAO...) không bị lỗi.
    /// </summary>
    public class KhoaHocDangDT : DrivingCenter.App.Entities.DAT.KhoaHocDat
    {
    }
}
