using System;
using System.Data;

namespace DrivingCenter.App.DTO.DAT
{
    public class KhoaHocDangDT
    {
        public int KhoaHocID { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string HangDaoTao { get; set; }
        public int SoHVDuDK { get; set; }
        public int TongHocVien { get; set; }

        public KhoaHocDangDT()
        {
        }

        public KhoaHocDangDT(DataRow row)
        {
            this.KhoaHocID = (int)row["KhoaHocID"];
            this.TenKH = row["TenKH"].ToString();
            this.HangDaoTao = row["HangDaoTao"].ToString();
            this.SoHVDuDK = row["SoHVDuDK"] == DBNull.Value ? 0 : (int)row["SoHVDuDK"];
            this.TongHocVien = (int)row["TongHocVien"];
        }
    }
}
