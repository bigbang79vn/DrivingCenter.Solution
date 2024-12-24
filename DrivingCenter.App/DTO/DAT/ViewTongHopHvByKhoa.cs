using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class ViewTongHopHvByKhoa
    {
        public int STT { get; set; }
        public string View_MaDK { get; set; }
        public string View_HoTenGV { get; set; }
        public string View_HoVaTen { get; set; }
        public string View_HangDaoTao { get; set; }
        public double View_TongQuangDuong { get; set; }
        public string View_TongThoiGian { get; set; }
        public double TongThoiGian { get; set; }
        public double View_QuangDuongPhienGuiTC { get; set; }
        public string View_ThoiGianPhienGuiTC { get; set; }
        public double View_TongQuangDuong_B11 { get; set; }
        public string View_TongThoiGian_B11 { get; set; }
        public double TongThoiGianSTD { get; set; }
        public double View_TongQuangDuong_PhienDem { get; set; }
        public string View_TongThoiGian_PhienDem { get; set; }
        public double TongThoiGianBD { get; set; }
        public int View_SoNgayHoc { get; set; }
        public string View_Status { get; set; }
    }
}
