using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.Cabin
{
    public class HocVien
    {
        public int STT { get; set; }
        public string MaDK { get; set; }
        public long NguoiLxID { get; set; }
        public string MaKH { get; set; }
        public DateTime NgayKG { get; set; }
        public string HoTenDem { get; set; }
        public string Ten { get; set; }
        public string HoTenHV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string QuocTich { get; set; }
        public string SoCMT { get; set; }
        public DateTime NgayCapCMT { get; set; }
        public string HangGPLX { get; set; }
        public string TenKH { get; set; }
        public string Address { get; set; }
        public byte[] Anh { get; set; }
        public string ImageBase64 { get; set; }
        public string ToTalSecond { get; set; }
        public int TongNoiDungDT { get; set; }
        public bool Result { get; set; }
        public string TenGV { get; set; }
    }
}