using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class TongHopHVByDay
    {
        public string MaDkHv { get; set; }
        public string HoTenGVs { get; set; }
        public string HoVaTen { get; set; }
        public string TenKHs { get; set; }
        public string BienSoXe { get; set; }
        public double TongQuangDuongLD_Phien { get; set; }
        public string TongThoiGianLD_Phien { get; set; }
        public string GhiChu { get; set; }
        public TongHopHVByDay(DataRow row)
        {
            this.MaDkHv = row["MaDK"].ToString();
            this.HoTenGVs = row["HoTenGV"].ToString();
            this.HoVaTen = row["HoVaTen"].ToString();
            this.TenKHs = row["TenKH"].ToString();
            this.BienSoXe = row["BienSoXe"].ToString();
            this.TongQuangDuongLD_Phien = (double)row["TongQuangDuongLD_Phien"];
            this.TongThoiGianLD_Phien = row["TongThoiGianLD_Phien"].ToString();
        }
    }
}
