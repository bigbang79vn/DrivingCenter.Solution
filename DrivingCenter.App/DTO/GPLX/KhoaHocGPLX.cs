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
        public string NgayKG { get; set; }

        public KhoaHocGPLX(DataRow row)
        {
            this.MaKHGplx = row["MaKH"].ToString();
            this.TenKHGplx = row["TenKH"].ToString();
            this.NgayKG = row["NgayKG"].ToString();
        }

    }
}
