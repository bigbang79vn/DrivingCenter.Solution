using System;

namespace DrivingCenter.App.DTO.Cabin
{
    public class GiaoVien
    {
        public int GiaoVienID { get; set; }
        public string MaGV { get; set; }
        public string HoTenDem { get; set; }
        public string TenGV { get; set; }
        public string HoTenGV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMT { get; set; }
        public string HangGPLX { get; set; }
        public byte[] AnhChup { get; set; }
        public string MaCNDT { get; set; }
        public string TrinhDoHocVan { get; set; }
        public bool SuDung { get; set; }
        public DateTime DateUse { get; set; }

    }
}