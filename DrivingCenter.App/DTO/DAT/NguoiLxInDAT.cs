using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class NguoiLxInDAT
    {
        public int KhoaHocID { get; set; }
        public string TenKH { get; set; }
        public string HangDT { get; set; }
        public Int64 NguoiLXID { get; set; }
        public string MaDK { get; set; }
        public string HoVaTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayCapCMT { get; set; }
        public int GiaoVienID { get; set; }
        public string TenGV { get; set; }
        public string MaXeTap { get; set; }
        public string MaXeTapB11 { get; set; }
        public double? TongQuangDuongTichLuy { get; set; }
        public int TongThoiGianTichLuy { get; set; }
        public string TongThoiGianText { get; set; }
        public DateTime? ThoiDiemChayDAT { get; set; }
        public DateTime? ThoiDiemKetThucDAT { get; set; }
        public byte[] Anh { get; set; }
        public string ImageBase64 { get; set; }
    }
}
