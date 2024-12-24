using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class XeTapLai
    {
        public int STT { get; set; }
        public int VehicleID { get; set; }
        public string BienSoXe { get; set; }
        public string Imei { get; set; }
        public string Version { get; set; }
        public string MaXe { get; set; }
        public string HangGPLX { get; set; }
        public int NamSX { get; set; }
        public string SoGPXTL { get; set; }
        public DateTime NgayCapGPXTL { get; set; }
        public DateTime NgayHHXTL { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ThoiDiem { get; set; }
        public double VanToc { get; set; }
        public bool TrangThai { get; set; }
    }
}