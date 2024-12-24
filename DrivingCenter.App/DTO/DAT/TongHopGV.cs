using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class TongHopGV
    {
        public int STT { get; set; }
        public string MaGv { get; set; }
        public string SHoTenGV { get; set; }
        public string NgayTap { get; set; }
        public int SoHocVienByDay { get; set; }
        public int SoPhien { get; set; }
        public string TenKhoaHoc { get; set; }
        public string BienSoXeGv { get; set; }
        public double SQuangDuong { get; set; }
        public string SThoiGian { get; set; }
        public string SGhiChu { get; set; }

        public TongHopGV(DataRow row)
        {
            this.MaGv = row["MaGV"].ToString();
            this.SHoTenGV = row["HoTenGV"].ToString();
            this.NgayTap = row["ThoiDiem"].ToString();
            this.SoPhien = (int)row["SoPhien"];
            this.TenKhoaHoc = row["TenKH"].ToString();
            this.BienSoXeGv = row["BienSoXe"].ToString();
            this.SoHocVienByDay = (int)row["SoHocVien"];
            this.SQuangDuong = (double)row["SQuangDuong"];
            this.SThoiGian = row["SThoiGian"].ToString();
        }
    }
}
