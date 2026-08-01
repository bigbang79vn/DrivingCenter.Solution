using DevExpress.XtraLayout.Filtering.Templates;
using System;

namespace DrivingCenter.App.Dtos.DAT
{
    public class GiaoVienDto
    {
        public string MaGV { get; set; }
        public string MaCSDT { get; set; }
        public string HoTenDem { get; set; }
        public string TenGV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMT { get; set; }
        public string HangGPLX { get; set; }
        public string HangGPLXDuocDT { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string MaCNDT { get; set; }
        public int MaTrinhDoHocVan { get; set; }
        public string MonGiangDay { get; set; }
        public byte[] AnhChup { get; set; }
    }
}
