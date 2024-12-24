using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public KhoaHocDangDT(int khoahocID, string makh, string tenkh, string hangdt, int sohvdudk, int tonghocvien)
        {
            this.KhoaHocID = khoahocID;
            this.MaKH = makh;
            this.TenKH = tenkh;
            this.HangDaoTao = hangdt;
            this.SoHVDuDK = sohvdudk;
            this.TongHocVien = tonghocvien;
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
