using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class TongHopHVByKhoa
    {
        public string MaDK { get; set; }
        public string HoTenGV { get; set; }
        public string HoVaTenHV { get; set; }
        public string HangDaoTao { get; set; }
        public double TongQuangDuongTichLuy { get; set; }
        public double TongThoiGianTichLuy { get; set; }
        public double QuangDuongPhienGuiTC { get; set; }
        public double ThoiGianPhienGuiTC { get; set; }
        public double TongQuangDuong_B1 { get; set; }
        public double TongThoiGian_B1 { get; set; }
        public double TongQuangDuong_PhienDemB1 { get; set; }
        public double TongThoiGian_PhienDemB1 { get; set; }
        public double TongQuangDuong_PhienDem { get; set; }
        public double TongThoiGian_PhienDem { get; set; }
        public string HangXe { get; set; }
        public int SoNgayHocKQ { get; set; }
        public string StatusKQ { get; set; }

    }
}
