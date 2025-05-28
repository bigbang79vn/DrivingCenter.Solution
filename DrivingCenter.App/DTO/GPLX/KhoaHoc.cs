using DevExpress.Pdf.Native.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.GPLX
{
    public class KhoaHoc
    {
        public string MaKH { get; set; }
        public string KhoaHocs { get; set; }
        public string TenKH { get; set; }
        public string HangGPLX { get; set; }
        public string HangDT { get; set; }
        public DateTime NgayKG { get; set; }
        public DateTime NgayBG { get; set; }
    }
}
