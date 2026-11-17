using System;

namespace DrivingCenter.App.Dtos.DAT
{
    public class KhoaHocNode
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        /// <summary>KhoaHocID thật (chỉ có ở node khoá học, cấp 3).</summary>
        public int KhoaHocID { get; set; }
        public string TenKH { get; set; }
        public string MaKH { get; set; }
        public DateTime? NgayKG { get; set; }
        public string HangDT { get; set; }
        public DateTime? NgayBatDauDT { get; set; }
        public DateTime? NgayBG { get; set; }
        /// <summary>"Chưa khai giảng" / "Đang học" / "Chờ hết hạn" / "Đã kết thúc" (chỉ node khoá học).</summary>
        public string TrangThai { get; set; }
        public int? Nam { get; set; }
        public int? Thang { get; set; }
    }
}
