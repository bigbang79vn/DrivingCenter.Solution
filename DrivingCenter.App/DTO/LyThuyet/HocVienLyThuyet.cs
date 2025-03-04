using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.LyThuyet
{
    public class HocVienLyThuyet
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
        public int GiaoVienID { get; set; }
        public string DaoDuc { get; set; }
        public string KyThuat { get; set; }
        public string CauTao { get; set; }
        public string PL1 { get; set; }
        public string PL2 { get; set; }
        public string PL3 { get; set; }
        public string MoPhong { get; set; }
        public string KetLuan { get; set; }
    }
}
