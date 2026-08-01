using System;

namespace DrivingCenter.App.DTO.DAT
{
    public class PhienHoc
    {
        public byte[] AnhChup { get; set; }
        public byte[] AnhChup_Logout { get; set; }
        public string HoTenGV { get; set; }
        public Int64 NguoiLxID { get; set; }
        public string HoTenHV { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public Guid MaPhien { get; set; }
        public string BienSoXe { get; set; }
        public DateTime Time_LogIn { get; set; }
        public DateTime Time_LogOut { get; set; }
        public double QuangDuongPhien { get; set; }
        public string ThoiGianPhien { get; set; }
        public double dbTime { get; set; }
        public bool LogOut { get; set; }
        public bool SendTongCuc { get; set; }
        public int SoLanNhanDien { get; set; }
        public int SoLanNhanDienThanhCong { get; set; }
        public string NhanDien { get; set; }
        public double TyLe { get; set; }
        public string Error { get; set; }
        public bool Active { get; set; }
        public string Note { get; set; }
    }
}
