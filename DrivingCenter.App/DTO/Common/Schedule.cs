using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.Cabin
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public int KhoaHocId { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public int? GiaoVienId { get; set; }
        public string GiaoVien { get; set; }
        public DateTime? ThoiDiemChayDAT { get; set; }
        public DateTime? ThoiDiemKetThucDAT { get; set; }
        public string MaXeTap { get; set; }
        public string MaXeTapB11 { get; set; }
        public DateTime? TimeInB11 { get; set; }
        public DateTime? TimeOutB11 { get; set ; }
        public int? ThietBiID { get; set; }
        public string Cabin { get; set; }
        public DateTime? TimeInCabin { get; set; }
        public DateTime? TimeOutCabin { get; set; }
        public bool? Status { get; set; }
        public string HangDT { get; set; }

    }
}
