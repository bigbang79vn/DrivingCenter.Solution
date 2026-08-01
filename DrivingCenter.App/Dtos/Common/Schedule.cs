using System;

namespace DrivingCenter.App.DTO.Cabin
{
    public class Schedule
    {
        public int Id { get; set; }
        public int KhoaHocId { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public DateTime? NgayKG { get; set; }
        public DateTime? NgayBG { get; set; }
        public int GiaoVienId { get; set; }
        public string GiaoVien { get; set; }
        public int? SoHV { get; set; }
        public DateTime? ThoiDiemChayDAT { get; set; }
        public DateTime? ThoiDiemKetThucDAT { get; set; }
        public int? IdXeTap { get; set; }
        public string MaXeTap { get; set; }
        public int? IdXeB11 { get; set; }
        public string MaXeTapB11 { get; set; }
        public DateTime? TimeInB11 { get; set; }
        public DateTime? TimeOutB11 { get; set ; }
        public int? ThietBiID { get; set; }
        public string Cabin { get; set; }
        public DateTime? TimeInCabin { get; set; }
        public DateTime? TimeOutCabin { get; set; }
        public bool? Status { get; set; }
        public string HangDT { get; set; }
        public DateTime NgayHL { get; set; }
        public DateTime NgayHetHL { get; set; }

    }
}
