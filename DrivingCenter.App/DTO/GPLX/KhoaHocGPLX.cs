using DevExpress.Pdf.Native.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.GPLX
{
    public class KhoaHocGPLX
    {
        public string MaKHGplx { get; set; }
        public string TenKHGplx { get; set; }
        public string HangGPLX { get; set; }
        public string HangDT { get; set; }
        public string NgayKG { get; set; }
        public string NgayBG { get; set; }


        public KhoaHocGPLX(DataRow row)
        {
            this.MaKHGplx = row["MaKH"].ToString();
            this.TenKHGplx = row["TenKH"].ToString();
            this.HangGPLX = row["HangGPLX"].ToString();
            this.HangDT = row["HangDT"].ToString();
            this.NgayKG = row["NgayKG"].ToString();
            this.NgayBG = row["NgayBG"].ToString();
        }

    }
}
