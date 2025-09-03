using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class PhienHoc
    {
        public string HoTenGV { get; set; }
        public Int64 NguoiLxID { get; set; }
        public string HoTenHV { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public Guid MaPhien { get; set; }
        public string BienSoXe { get; set; }
        public DateTime Time_LogIn { get; set; }
        public DateTime Time_LogOut { get; set; }
        public double QuangDuongPhien { get; set; }
        public string ThoiGianPhien { get; set; }
        public double dbTime { get; set; }
        public bool LogOut { get; set; }
        public bool SendTongCuc { get; set; }
        public int SoLanNhanDien { get; set; }
        public int SoLanNhanDienThanhCong { get; set; }
        public string NhanDien { get; set; }
        public double TyLe { get; set; }
        public string Error { get; set; }
        public bool Active { get; set; }
        public string Note { get; set; }
        public HashSet<string> ErrorList { get; set; } = new HashSet<string>();

        public PhienHoc()
        {

        }
        public PhienHoc(DataRow row)
        {
            this.HoTenGV = row["HoTenGV"].ToString();
            this.NguoiLxID = Convert.ToInt64(row["NguoiLxID"]);
            this.HoTenHV = row["HoVaTen"].ToString();
            this.MaKH = row["MaKH"].ToString();
            this.TenKH = row["TenKH"].ToString();
            this.MaPhien = (Guid)row["DiemDanhNguoiLxID"];
            this.BienSoXe = row["BienSo"].ToString();
            this.Time_LogIn = (DateTime)row["ThoiDiem"];
            this.Time_LogOut = row["ThoiDiem_Logout"] == DBNull.Value ? DateTime.Now : (DateTime)row["ThoiDiem_Logout"];
            this.QuangDuongPhien = (double)row["TongQuangDuongLD_Phien"];
            this.ThoiGianPhien = row["TongThoiGianLD_Phien"].ToString();
            this.dbTime = (double)row["dbTime"];
            this.LogOut = (bool)row["Logout"];
            this.SendTongCuc = (bool)row["SendTongCuc"];
            this.NhanDien = row["SoLanNhanDienThanhCong"].ToString() + "/" + row["SoLanNhanDien"].ToString();
            this.SoLanNhanDien = row["SoLanNhanDien"] == DBNull.Value ? 0 : (int)row["SoLanNhanDien"];
            this.SoLanNhanDienThanhCong = row["SoLanNhanDienThanhCong"] == DBNull.Value ? 0 : (int)row["SoLanNhanDienThanhCong"];
            this.Active = (bool)row["ActivePhien"];
            this.Note = row["Note"].ToString();
        }
    }
}
