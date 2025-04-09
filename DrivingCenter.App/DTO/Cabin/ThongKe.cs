using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.Cabin
{
    public class ThongKe
    {
        public Int64 NguoiLxID { get; set; }
        public string TenGV { get; set; }
        public string TenKhoaHoc { get; set; }
        public string CabinName { get; set; }
        public string MaDK { get; set; }
        public string HoVaTen { get; set; }
        public string NgaySinh { get; set; }
        public string SoCMT { get; set; }
        public string TongThoiGian { get; set; }
        public bool Result { get; set; }
        public double TotalHour { get; set; }
    }
}