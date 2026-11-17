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
    /// <summary>Giáo viên trên DAT (danh sách, thêm mới, đồng bộ giáo viên sang Cabin).</summary>
    public class GiaoVienDatDAO : DatDaoBase
    {
        public List<GvCabin> GetListGvByNameKH(string TenKH)
        {
            const string query = "SELECT MaDK, GiaoVienID FROM NguoiLX WHERE TenKH LIKE '%' + @TenKH + '%' ORDER BY GiaoVienID";
            DataTable dt = _db.QueryToDataTable(query, new { TenKH });

            return dt.AsEnumerable().Select(item => new GvCabin
            {
                MaDK = (string)(item)["MaDK"],
                GiaoVienID = (int)(item)["GiaoVienID"]
            }).ToList();
        }

        public List<GiaoVien> GetAllGV()
        {
            const string query = @"SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX, AnhChup, MaCNDT, hv.Ten
                        FROM GiaoVien AS gv
                        INNER JOIN TrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma";
            DataTable dt = _db.QueryToDataTable(query);

            return dt.AsEnumerable().Select(item => new GiaoVien
            {
                GiaoVienID = (int)item["GiaoVienID"],
                MaGV = (string)item["MaGV"],
                HoTenDem = (string)item["HoTenDem"],
                TenGV = (string)item["TenGV"],
                HoTenGV = (string)item["HoTenDem"] + " " + (string)item["TenGV"],
                NgaySinh = (DateTime)item["NgaySinh"],
                SoCMT = (string)item["SoCMT"],
                HangGPLX = (string)item["HangGPLX"],
                AnhChup = item["AnhChup"] as byte[],
                MaCNDT = item["MaCNDT"] == DBNull.Value ? "" : (string)item["MaCNDT"],
                TrinhDoHocVan = (string)item["Ten"]
            }).ToList();
        }

        public bool AddGiaoVien(GiaoVienDto giaoVien)
        {
            const string query = "EXEC [dbo].[spAddGiaoVien] @MaGV, @MaCSDT, @HoTenDem, @TenGV, @NgaySinh, @SoCMT, @HangGPLX, @SoDienThoai, @DiaChi, @MaCNDT, @MaTrinhDoHocVan, @MonGiangDay, @AnhChup";
            var rs = _db.Execute(query, new
            {
                giaoVien.MaGV,
                giaoVien.MaCSDT,
                giaoVien.HoTenDem,
                giaoVien.TenGV,
                giaoVien.NgaySinh,
                giaoVien.SoCMT,
                HangGPLX = giaoVien.HangGPLXDuocDT,
                giaoVien.SoDienThoai,
                giaoVien.DiaChi,
                giaoVien.MaCNDT,
                giaoVien.MaTrinhDoHocVan,
                MonGiangDay = "",
                AnhChup = (byte[])null
            });

            return rs > 0;
        }
    }
}
