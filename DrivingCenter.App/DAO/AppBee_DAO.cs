using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    public class AppBee_DAO
    {
        private static DbProvider _db = new DbProvider(Enums.DatabaseType.db_BeeSolution);

        #region Giáo Viên
        public List<GiaoVien> GetAllGV()
        {
            List<GiaoVien> lstGiaoVien = new List<GiaoVien>();
            string query = $@"SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX, AnhChup, MaCNDT, hv.Ten , SuDung, DateUse 
                        FROM tblGiaoVien AS gv
                        INNER JOIN tblTrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma";
            DataTable dt = _db.QueryToDataTable(query);

            lstGiaoVien = dt.AsEnumerable().Select(item => new GiaoVien
            {
                GiaoVienID = (int)item["GiaoVienID"],
                MaGV = (string)item["MaGV"],
                HoTenDem = (string)item["HoTenDem"],
                TenGV = (string)item["TenGV"],
                HoTenGV = (string)item["HoTenDem"] + " " + (string)item["TenGV"],
                NgaySinh = (DateTime)item["NgaySinh"],
                SoCMT = (string)item["SoCMT"],
                HangGPLX = (string)item["HangGPLX"],
                AnhChup = (byte[])item["AnhChup"],
                MaCNDT = (string)item["MaCNDT"],
                TrinhDoHocVan = (string)item["Ten"],
                SuDung = (bool)item["SuDung"],
                DateUse = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"]
            }).ToList();

            return lstGiaoVien;
        }

        public List<GiaoVien> Get_GvTrong(string loailop)
        {
            const string query = @"SELECT *
                            FROM tblGiaoVien AS gv
                            INNER JOIN tblTrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma
                            WHERE gv.GiaoVienID NOT IN (
                                SELECT GiaoVienID
                                FROM tblKhoaHoc_GiaoVien
                                WHERE LoaiLop = @loailop AND HetHieuLuc > GETDATE() AND GiaoVienID IS NOT NULL) AND SuDung = 1";
            DataTable dt = _db.QueryToDataTable(query, new { loailop });

            return dt.AsEnumerable().Select(MapGiaoVien).ToList();
        }

        public List<GiaoVien> Get_GiaoVienByMaKH(string makh)
        {
            const string query = @"SELECT GV.GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX, AnhChup, MaCNDT, hv.Ten , SuDung, DateUse
                        FROM tblKhoaHoc_GiaoVien AS kh
						INNER JOIN tblGiaoVien AS gv ON KH.GiaoVienID = GV.GiaoVienID
                        INNER JOIN tblTrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma
						WHERE MaKH = @makh";
            DataTable dt = _db.QueryToDataTable(query, new { makh });

            return dt.AsEnumerable().Select(MapGiaoVien).ToList();
        }

        /// <summary>Đọc giáo viên an toàn với cột NULL (ảnh, mã CNĐT, ngày sinh... trước đây ép kiểu thẳng nên lỗi khi trống).</summary>
        private static GiaoVien MapGiaoVien(DataRow item)
        {
            string hoTenDem = Convert.ToString(item["HoTenDem"]);
            string tenGV = Convert.ToString(item["TenGV"]);
            return new GiaoVien
            {
                GiaoVienID = (int)item["GiaoVienID"],
                MaGV = Convert.ToString(item["MaGV"]),
                HoTenDem = hoTenDem,
                TenGV = tenGV,
                HoTenGV = $"{hoTenDem} {tenGV}".Trim(),
                NgaySinh = item["NgaySinh"] == DBNull.Value ? DateTime.MinValue : (DateTime)item["NgaySinh"],
                SoCMT = Convert.ToString(item["SoCMT"]),
                HangGPLX = Convert.ToString(item["HangGPLX"]).Trim(),
                AnhChup = item["AnhChup"] as byte[],
                MaCNDT = Convert.ToString(item["MaCNDT"]),
                TrinhDoHocVan = Convert.ToString(item["Ten"]),
                SuDung = item["SuDung"] != DBNull.Value && (bool)item["SuDung"],
                DateUse = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"]
            };
        }

        public DataTable GetlstHangGPLX()
        {
            string query = $"SELECT [ID], [MaHang], [TenHang] FROM HangGPLX";
            return _db.QueryToDataTable(query);
        }

        public DataTable GetlstHocVan()
        {
            string query = $"SELECT * FROM tblTrinhDoHocVan";
            return _db.QueryToDataTable(query);
        }

        public int SynsGiaoVien()
        {
            string query = $@"EXEC dbo.usp_SyncGiaoVien";
            return _db.Execute(query);
        }

        public int UpdateGiaoVien(GiaoVien gv, int idgiaovien)
        {
            var sudung = gv.SuDung ? 1 : 0;
            const string query = "Update tblGiaoVien set SuDung = @sudung Where GiaoVienID = @idgiaovien";
            return _db.Execute(query, new { sudung, idgiaovien });
        }

        public List<Schedule> GetKhoaHocDaPhanCong(string maKh)
        {
            // Trước đây đọc cột không tồn tại (MaXeTapB11, ThietBiID) nên lỗi ngay khi khóa đã có xe STĐ / cabin,
            // và ép kiểu thẳng GiaoVienID / SoHV / HieuLuc / HetHieuLuc (cho phép NULL trong bảng)
            const string query = "SELECT * FROM tblKhoaHoc_GiaoVien WHERE MaKH = @maKh AND GiaoVienID IS NOT NULL ORDER BY ID";
            DataTable dt = _db.QueryToDataTable(query, new { maKh });

            T? Get<T>(DataRow r, string col) where T : struct => r[col] == DBNull.Value ? (T?)null : (T)r[col];
            string Str(DataRow r, string col) => r[col] == DBNull.Value ? null : Convert.ToString(r[col]).Trim();

            List<Schedule> lst = dt.AsEnumerable().Select(item => new Schedule
            {
                Id = (int)item["ID"],
                MaKH = Str(item, "MaKH"),
                GiaoVienId = (int)item["GiaoVienID"],
                GiaoVien = Str(item, "HoTenGV"),
                IdXeTap = Get<int>(item, "IdXeTap"),
                MaXeTap = Str(item, "MaXeTap"),
                IdXeB11 = Get<int>(item, "IDXeB11"),
                MaXeTapB11 = Str(item, "MaXeB11"),
                TimeInB11 = Get<DateTime>(item, "DateStartB11"),
                TimeOutB11 = Get<DateTime>(item, "DateEndB11"),
                SoHV = Get<int>(item, "SoHV"),
                Cabin = Str(item, "Cabin"),
                TimeInCabin = Get<DateTime>(item, "DateStartCabin"),
                TimeOutCabin = Get<DateTime>(item, "DateEndCabin"),
                Status = Get<bool>(item, "TrangThai"),
                NgayHL = Get<DateTime>(item, "HieuLuc") ?? DateTime.MinValue,
                NgayHetHL = Get<DateTime>(item, "HetHieuLuc") ?? DateTime.MinValue,
            }).ToList();
            return lst;
        }

        public string GetMaXeTapById(int idxetap)
        {
            const string query = "SELECT MaXe FROM tblXeTapLai WHERE ID = @idxetap";
            return _db.Scalar<string>(query, new { idxetap });
        }

        public int InsertKhoaHocGiaoVien(string maKhoaHoc, int? giaoVienId, int? xeTapId, int soHocVien, DateTime ngayHieuLuc, DateTime ngayHetHieuLuc)
        {
            string query = @"EXEC dbo.usp_KhoaHoc_GiaoVien_Insert 
                        @MaKH, 
                        @GiaoVienID, 
                        @IdXeTap,
                        @SoHV, 
                        @NgayHL, 
                        @NgayHetHL";

            var parameters = new
            {
                MaKH = maKhoaHoc,
                GiaoVienID = giaoVienId,
                IdXeTap = xeTapId,
                SoHV = soHocVien,
                NgayHL = ngayHieuLuc,
                NgayHetHL = ngayHetHieuLuc
            };

            return _db.Execute(query, parameters);
        }

        public int UpdateKhoaHocGiaoVien(int Id, string maKhoaHoc, int? giaoVienId, int? xeTapId, int soHocVien, DateTime ngayHieuLuc, DateTime ngayHetHieuLuc)
        {
            string query = @"EXEC dbo.usp_KhoaHoc_GiaoVien_Update
                        @Id,
                        @MaKH, 
                        @GiaoVienID, 
                        @IdXeTap,
                        @SoHV, 
                        @NgayHL, 
                        @NgayHetHL";

            var parameters = new
            {
                Id = Id,
                MaKH = maKhoaHoc,
                GiaoVienID = giaoVienId,
                IdXeTap = xeTapId,
                SoHV = soHocVien,
                NgayHL = ngayHieuLuc,
                NgayHetHL = ngayHetHieuLuc
            };

            return _db.Execute(query, parameters);
        }

        public int DeleteKhoaHocGiaoVien(Schedule item)
        {
            return _db.Execute("DELETE FROM tblKhoaHoc_GiaoVien WHERE ID = @Id", new { item.Id });
        }

        #endregion

        #region Xe Tập Lái
        public List<XeTapLai> Get_XeTapLaiTrong(string loailop)
        {
            string query = $@"SELECT * 
                            FROM tblXeTapLai AS xe
                            WHERE xe.ID NOT IN (
                                SELECT IdXeTap 
                                FROM tblKhoaHoc_GiaoVien 
                                WHERE LoaiLop = N'{loailop}' AND HetHieuLuc > GETDATE()) AND ThanhLy = 0";

            DataTable dt = _db.QueryToDataTable(query);

            List<XeTapLai> lstXeTapLai = dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["ID"],
                BienSoXe = (string)item["BienSoXe"],
                MaXe = (string)item["MaXe"],
                HangGPLX = (string)item["HangGPLX"],
                NamSX = (int)item["NamSX"],
                SoGPXTL = (string)item["SoGPXTL"],
                NgayCapGPXTL = (DateTime)item["NgayCapGPXTL"],
                NgayHHXTL = (DateTime)item["NgayHHGPXTL"],
                ThanhLy = (bool)item["ThanhLy"],
                ChuSoHuu = (string)item["ChuSoHuu"],
                SuDungDen = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"],
            }).ToList();

            return lstXeTapLai;
        }

        public List<XeTapLai> Get_XeB11ConTrong(string loailop, DateTime ngayTH)
        {
            string ngaySql = ngayTH.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $@"
                    SELECT * 
                    FROM tblXeTapLai AS xe
                    WHERE xe.ID NOT IN (
                        SELECT IdXeTap 
                        FROM tblKhoaHoc_GiaoVien 
                        WHERE LoaiLop = N'{loailop}' 
                            AND HetHieuLuc > GETDATE() 
                            AND (DateEndB11 IS NULL OR DateEndB11 > '{ngaySql}')
                    ) 
                    AND xe.HangGPLX = 'B11'";

            DataTable dt = _db.QueryToDataTable(query);

            List<XeTapLai> lstXeTapLai = dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["ID"],
                BienSoXe = (string)item["BienSoXe"],
                MaXe = (string)item["MaXe"],
                HangGPLX = (string)item["HangGPLX"],
                NamSX = (int)item["NamSX"],
                SoGPXTL = (string)item["SoGPXTL"],
                NgayCapGPXTL = (DateTime)item["NgayCapGPXTL"],
                NgayHHXTL = (DateTime)item["NgayHHGPXTL"],
                ThanhLy = (bool)item["ThanhLy"],
                ChuSoHuu = (string)item["ChuSoHuu"],
                SuDungDen = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"],
            }).Where(x => x.ThanhLy == false)
            .ToList();

            return lstXeTapLai;
        }

        public List<XeTapLai> GetAllXeTapLai()
        {
            string query = $@"select * from tblXeTapLai";

            DataTable dt = _db.QueryToDataTable(query);

            List<XeTapLai> lstXeTapLai = dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["ID"],
                BienSoXe = (string)item["BienSoXe"],
                MaXe = (string)item["MaXe"],
                HangGPLX = (string)item["HangGPLX"],
                NamSX = (int)item["NamSX"],
                SoGPXTL = (string)item["SoGPXTL"],
                NgayCapGPXTL = (DateTime)item["NgayCapGPXTL"],
                NgayHHXTL = (DateTime)item["NgayHHGPXTL"],
                ThanhLy = item["ThanhLy"] == DBNull.Value ? true : (bool)item["ThanhLy"],
                ChuSoHuu = (string)item["ChuSoHuu"],
                SuDungDen = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"],
            }).ToList();

            return lstXeTapLai;
        }

        public int UpdateXeTapLai(XeTapLai xe, int idxetap)
        {
            var thanhly = xe.ThanhLy ? 1 : 0;
            const string query = "Update tblXeTapLai set ChuSoHuu = @ChuSoHuu, ThanhLy = @thanhly Where ID = @idxetap";
            return _db.Execute(query, new { xe.ChuSoHuu, thanhly, idxetap });
        }

        public int SyncXeTapLai()
        {
            string query = $@"EXEC dbo.usp_SyncXeTapLai";
            return _db.Execute(query);
        }
        #endregion
    }
}
