using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Dtos.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    /// <summary>Học viên DAT: danh sách theo khóa (in thẻ, quá trình đào tạo) và tra cứu theo mã đăng ký.</summary>
    public class HocVienDatDAO : DatDaoBase
    {
        public List<HocVien> GetlstHocVienByKhoaHoc(int idKhoaHoc)
        {
            const string query = "SELECT HV.GiaoVienID, gv.HoTenDem, GV.TenGV, NguoiLXID, MaDK, HoVaTen, HV.NgaySinh, HV.GioiTinh, NoiCT, HV.SoCMT, NgayCapCMT " +
                "FROM NguoiLX AS HV, GiaoVien AS GV WHERE KhoaHocID = @idKhoaHoc AND HV.GiaoVienID = GV.GiaoVienID ORDER BY GiaoVienID ASC";
            DataTable dt = _db.QueryToDataTable(query, new { idKhoaHoc });

            return dt.AsEnumerable().Select(item => new HocVien
            {
                NguoiLxID = Convert.ToInt64((item)["NguoiLXID"]),
                MaDK = (string)(item)["MaDK"],
                HoTenHV = (string)(item)["HoVaTen"],
                NgaySinh = (DateTime)(item)["NgaySinh"],
                GioiTinh = (string)(item)["GioiTinh"],
                Address = item["NoiCT"] == DBNull.Value ? "" : (string)(item)["NoiCT"],
                SoCMT = (string)(item)["SoCMT"],
                NgayCapCMT = (DateTime)(item)["NgayCapCMT"],
                TenGV = (string)(item)["HoTenDem"] + " " + (string)(item)["TenGV"]
            }).ToList();
        }

        public List<NguoiLxInDAT> GetAllNguoiLxByKhoaHocID(int khoahocId)
        {
            DataTable dt = _db.QueryToDataTable("EXEC [dbo].[spGetALLNguoiLxByKhoaHocCSDT] @MaCSDT = @MaCSDT, @KhoaHocID = @khoahocId",
                new { MaCSDT, khoahocId });

            if (dt == null || dt.Rows.Count == 0)
                return new List<NguoiLxInDAT>();

            return dt.AsEnumerable().Select(item => new NguoiLxInDAT
            {
                KhoaHocID = (int)item["KhoaHocID"],
                TenKH = item["TenKH"].ToString(),
                HangDT = item["HangDaoTao"].ToString(),
                NguoiLXID = (Int64)item["NguoiLXID"],
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = (DateTime)item["NgaySinh"],
                GioiTinh = item["GioiTinh"].ToString(),
                CCCD = item["SoCMT"].ToString(),
                NgayCapCMT = (DateTime)item["NgayCapCMT"],
                GiaoVienID = item["GiaoVienID"] == DBNull.Value ? 0 : (int)item["GiaoVienID"],
                MaXeTap = item["MaXeTap"].ToString(),
                MaXeTapB11 = item["XeB11"] == DBNull.Value ? "" : item["XeB11"].ToString(),
                TongQuangDuongTichLuy = Convert.ToDouble(item["TongQuangDuongLD"]),
                // spGetALLNguoiLxByKhoaHocCSDT trả TongThoiGianLD là SỐ GIỜ kiểu decimal (VD 11.548):
                // ép (int) trực tiếp ném InvalidCastException. Quy đổi ra giây, hiển thị tổng giờ thực (vượt 24h).
                TongThoiGianTichLuy = GioToGiay(item["TongThoiGianLD"]),
                TongThoiGianText = FormatGioPhut(GioToGiay(item["TongThoiGianLD"])),
                ThoiDiemChayDAT = item["ThoiDiemChayDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemChayDAT"],
                ThoiDiemKetThucDAT = item["ThoiDiemKetThucDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemKetThucDAT"]
            }).ToList();
        }

        public NguoiLxInDAT GetInfoHocVienByMaDK(string maDK)
        {
            const string query = "SELECT MaDK, HoVaTen, NgaySinh, TenKH, HangGPLX FROM NguoiLX WHERE MaDK = @MaDK";
            DataTable dt = _db.QueryToDataTable(query, new { MaDK = maDK });
            if (dt.Rows.Count == 0) return null;

            DataRow item = dt.Rows[0];
            return new NguoiLxInDAT
            {
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = (DateTime)item["NgaySinh"],
                TenKH = item["TenKH"].ToString(),
                HangGPLX = item["HangGPLX"].ToString()
            };
        }

        private static int GioToGiay(object gio)
        {
            return gio == null || gio == DBNull.Value ? 0 : (int)Math.Round(Convert.ToDouble(gio) * 3600);
        }

        private static string FormatGioPhut(int seconds)
        {
            var ts = TimeSpan.FromSeconds(seconds);
            return $"{(int)ts.TotalHours:00}:{ts.Minutes:00}";
        }
    }
}
