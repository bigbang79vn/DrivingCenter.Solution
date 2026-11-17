using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Dtos.DAT;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    /// <summary>Phiên học DAT: giám sát trong ngày, khóa/mở/kết thúc/xóa phiên, chi tiết phiên (lộ trình, ảnh).</summary>
    public class PhienHocDAO : DatDaoBase
    {
        private const string SpDeletePhienHoc = "EXEC [dbo].[spDeletePhienHoc] @diemdanhnguoilxID = @id";

        #region Giám sát trong ngày

        /// <summary>Chạy lại thủ tục tổng hợp điểm danh của ngày (dd/MM/yyyy), thử lại khi gặp deadlock.</summary>
        public async Task ReLoad(string date)
        {
            await SafeExecuteAsync("EXEC spGetDiemDanhNguoiLxByDay @date, 0, 0", new { date });
            await SafeExecuteAsync("EXEC spGetDiemDanhNguoiLxByDay @date, 1, 0", new { date });
        }

        private async Task SafeExecuteAsync(string query, object param)
        {
            const int retryCount = 3;
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    await _db.ExecuteAsync(query, param);
                    break;
                }
                catch (SqlException ex) when (ex.Number == 1205) // Deadlock
                {
                    if (i == retryCount - 1) throw;
                    await Task.Delay(200);
                }
            }
        }

        /// <summary>Tất cả phiên bắt đầu trong ngày <paramref name="day"/> (dd/MM/yyyy).</summary>
        public List<PhienHoc> GetAllPhienHocToDay(string day)
        {
            const string query = "SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen, kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong, ph.ActivePhien, ph.GhiChu " +
                "FROM DiemDanhNguoiLx as ph, GiaoVien as gv, NguoiLX as hv,  KhoaHoc as kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and hv.NguoiLXID = ph.NguoiLxID AND hv.KhoaHocID = kh.KhoaHocID and CONVERT(VARCHAR, ThoiDiem, 103) = @day " +
                "ORDER by ph.Logout, ph.SendTongCuc ASC";

            DataTable dt = _db.QueryToDataTable(query, new { day });

            return dt.AsEnumerable().Select(item => new PhienHoc
            {
                HoTenGV = item.Field<string>("HoTenGV"),
                NguoiLxID = item.Field<long>("NguoiLxID"),
                HoTenHV = item.Field<string>("HoVaTen"),
                MaKH = item.Field<string>("MaKH"),
                TenKH = item.Field<string>("TenKH"),
                MaPhien = item.Field<Guid>("DiemDanhNguoiLxID"),
                BienSoXe = item.Field<string>("BienSo"),
                Time_LogIn = item.Field<DateTime>("ThoiDiem"),
                Time_LogOut = item.Field<DateTime?>("ThoiDiem_Logout") ?? DateTime.Now,
                QuangDuongPhien = item.Field<double?>("TongQuangDuongLD_Phien") ?? 0,
                ThoiGianPhien = item.Field<string>("TongThoiGianLD_Phien") ?? "",
                dbTime = item.Field<double?>("dbTime") ?? 0,
                LogOut = item.Field<bool>("Logout"),
                SendTongCuc = item.Field<bool>("SendTongCuc"),
                NhanDien = $"{item.Field<int?>("SoLanNhanDienThanhCong") ?? 0}/{item.Field<int?>("SoLanNhanDien") ?? 0}",
                SoLanNhanDien = item.Field<int?>("SoLanNhanDien") ?? 0,
                SoLanNhanDienThanhCong = item.Field<int?>("SoLanNhanDienThanhCong") ?? 0,
                Active = item.Field<bool>("ActivePhien"),
                Note = item.Field<string>("GhiChu") ?? ""
            }).ToList();
        }

        /// <summary>Phiên trong ngày từ 1 km.</summary>
        public List<PhienHoc> GetLstPhienByDateTrue(string day)
        {
            return GetAllPhienHocToDay(day).Where(x => x.QuangDuongPhien >= 1).ToList();
        }

        /// <summary>Phiên trong ngày dưới 5 km (nghi lỗi).</summary>
        public List<PhienHoc> GetLstPhienLoiByDate(string day)
        {
            return GetAllPhienHocToDay(day).Where(x => x.QuangDuongPhien <= 5).ToList();
        }

        /// <summary>Phiên đã bị khóa (ActivePhien = 0) từ 1 km trong khoảng ngày yyyyMMdd [from, to].</summary>
        public List<PhienHoc> TongHopPhienHocLoi(string from, string to)
        {
            const string query = "SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, hv.HoVaTen, ph.NguoiLxID, ph.DiemDanhNguoiLxID, ph.TongThoiGianLD_Phien as TongThoiGian_Double, hv.HangDaoTao, ph.ThoiDiem, ph.ThoiDiem_Logout,  kh.TenKH, ph.LogOut, ph.SendTongCuc, ROUND(ph.TongQuangDuongLD_Phien,1) AS TongQuangDuongLD_Phien, CONVERT(varchar, DATEADD(second, ph.TongThoiGianLD_Phien, 0), 108) AS TongThoiGianLD_Phien, ph.BienSo, ph.GhiChu " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
                "where ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ActivePhien = 0 and CONVERT(VARCHAR, ph.ThoiDiem, 112) >= @from AND CONVERT(VARCHAR, ph.ThoiDiem, 112) <= @to " +
                "ORDER BY HoTenGV";

            DataTable dt = _db.QueryToDataTable(query, new { from, to });

            return dt.AsEnumerable().Select(item => new PhienHoc
            {
                HoTenGV = (string)item["HoTenGV"],
                NguoiLxID = (Int64)item["NguoiLxID"],
                HoTenHV = (string)item["HoVaTen"],
                TenKH = (string)item["TenKH"],
                MaPhien = (Guid)item["DiemDanhNguoiLxID"],
                BienSoXe = (string)item["BienSo"],
                Time_LogIn = (DateTime)item["ThoiDiem"],
                Time_LogOut = item["ThoiDiem_Logout"] == DBNull.Value ? DateTime.Now : (DateTime)item["ThoiDiem_Logout"],
                QuangDuongPhien = item["TongQuangDuongLD_Phien"] == DBNull.Value ? 0 : (double)item["TongQuangDuongLD_Phien"],
                ThoiGianPhien = item["TongThoiGianLD_Phien"] == DBNull.Value ? "" : (string)item["TongThoiGianLD_Phien"],
                LogOut = (bool)item["LogOut"],
                SendTongCuc = (bool)item["SendTongCuc"],
                Note = item["GhiChu"] == DBNull.Value ? string.Empty : (string)item["GhiChu"]
            }).ToList();
        }

        /// <summary>
        /// Phiên học (từ 1 km) của một xe trong khoảng ngày [from, to] (tính cả ngày "to").
        /// maXe là biển số không dấu (VD 47B19703), trùng với cột BienSo của phiên.
        /// </summary>
        public List<PhienHoc> LstPhienHocTheoXe(string maXe, DateTime from, DateTime to)
        {
            const string query = "SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen,kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien / 3600 as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong , ActivePhien, GhiChu " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ph.BienSo = @BienSo and ph.ThoiDiem >= @From AND ph.ThoiDiem < @ToExclusive " +
                "ORDER BY ThoiDiem";

            DataTable dt = _db.QueryToDataTable(query, new { BienSo = maXe, From = from.Date, ToExclusive = to.Date.AddDays(1) });

            return dt.AsEnumerable().Select(item => new PhienHoc
            {
                HoTenGV = (string)item["HoTenGV"],
                NguoiLxID = (Int64)item["NguoiLxID"],
                HoTenHV = (string)item["HoVaTen"],
                TenKH = (string)item["TenKH"],
                MaPhien = (Guid)item["DiemDanhNguoiLxID"],
                BienSoXe = (string)item["BienSo"],
                Time_LogIn = (DateTime)item["ThoiDiem"],
                Time_LogOut = item["ThoiDiem_Logout"] == DBNull.Value ? DateTime.Now : (DateTime)item["ThoiDiem_Logout"],
                QuangDuongPhien = item["TongQuangDuongLD_Phien"] == DBNull.Value ? 0 : (double)item["TongQuangDuongLD_Phien"],
                ThoiGianPhien = item["TongThoiGianLD_Phien"] == DBNull.Value ? "" : (string)item["TongThoiGianLD_Phien"],
                dbTime = item.Field<double>("dbTime"),
                LogOut = (bool)item["LogOut"],
                SendTongCuc = (bool)item["SendTongCuc"],
                Note = item["GhiChu"] == DBNull.Value ? string.Empty : (string)item["GhiChu"]
            }).ToList();
        }

        /// <summary>Các phiên đi ra ngoài ranh giới tỉnh (ProvinceBoundaryUnion) trong khoảng thời gian.</summary>
        public List<Guid> CheckMapOut(DateTime fromDay, DateTime toDay)
        {
            const string query = @"DECLARE @Boundary GEOMETRY = (SELECT BoundaryUnion FROM ProvinceBoundaryUnion);

                            SELECT DISTINCT G.DiemDanhNguoiLxId
                            FROM VehicleHistory G
                            WHERE @Boundary.STContains(geometry::Point(G.Lng, G.Lat, 4326)) = 0
                              AND G.AddDated >= @FromDate
                              AND G.AddDated <  @ToDate;";
            var data = _db.QueryToDataTable(query, new { FromDate = fromDay, ToDate = toDay });

            return data.AsEnumerable()
                       .Select(r => Guid.Parse(Convert.ToString(r["DiemDanhNguoiLxId"])))
                       .ToList();
        }

        #endregion

        #region Thao tác trên phiên

        public void LockSession(string diemdanhnguoilxId, string note)
        {
            _db.Execute("UPDATE DiemDanhNguoiLx SET GhiChu = @GhiChu, ActivePhien = 0 WHERE DiemDanhNguoiLxID = @Id",
                new { GhiChu = note, Id = diemdanhnguoilxId });
        }

        public void UnlockSession(string diemdanhnguoilxId)
        {
            _db.Execute("UPDATE DiemDanhNguoiLx SET GhiChu = NULL, ActivePhien = 1 WHERE DiemDanhNguoiLxID = @Id",
                new { Id = diemdanhnguoilxId });
        }

        public bool OptimizeTime(Int64 nguoilxId, string diemdanhnguoilxId)
        {
            int result = _db.Execute("EXEC [dbo].[spOptimizeTimeSession] @NguoiLxID, @DiemDanhNguoiLxID",
                new { NguoiLxID = nguoilxId, DiemDanhNguoiLxID = diemdanhnguoilxId });
            return result > 0;
        }

        public void KetThucPhien(Int64 nguoiLxID, string diemdanhnguoilxId)
        {
            _db.Execute("EXEC [dbo].[spKetThucPhienDaoTao] @NguoiLxID = @NguoiLxID, @DiemDanhNguoiLxID = @DiemDanhNguoiLxID",
                new { NguoiLxID = nguoiLxID, DiemDanhNguoiLxID = diemdanhnguoilxId });
        }

        /// <summary>Xóa phiên ở cả CSDL dữ liệu và CSDL ảnh.</summary>
        public void DeletePhByID(string diemdanhnguoilxID)
        {
            var p = new { id = diemdanhnguoilxID };
            _db.Execute(SpDeletePhienHoc, p);
            _dbImg.Execute(SpDeletePhienHoc, p);
        }

        public List<Guid> GetlstPhienHocLoginIsNull(int khoahocid)
        {
            const string query = "SELECT kq.DiemDanhNguoiLxID " +
                "FROM DiemDanhNguoiLx kq, NguoiLX hv, KhoaHoc kh " +
                "WHERE kq.NguoiLxID = hv.NguoiLXID and kh.KhoaHocID = hv.KhoaHocID and kq.ThoiDiem is null and kh.KhoaHocID = @khoahocid";
            DataTable dt = _db.QueryToDataTable(query, new { khoahocid });

            return dt.AsEnumerable()
                .Select(r => r.Field<Guid>("DiemDanhNguoiLxID"))
                .ToList();
        }

        public void DeletePhienHocLoginIsNull(List<Guid> lstDiemDanhNguoiLxID)
        {
            foreach (Guid item in lstDiemDanhNguoiLxID)
                DeletePhByID(item.ToString());
        }

        /// <summary>Phiên đã có ảnh đăng xuất trong số các phiên cho trước.</summary>
        public HashSet<Guid> GetPhienCoAnhLogout(IEnumerable<Guid> diemdanhnguoilxIds)
        {
            const string query = "SELECT DiemDanhNguoiLxId FROM DiemDanhNguoiLx " +
                                 "WHERE DiemDanhNguoiLxId IN @Ids AND AnhChup_Logout IS NOT NULL";

            var result = new HashSet<Guid>();
            var ids = diemdanhnguoilxIds.Distinct().ToList();
            for (int i = 0; i < ids.Count; i += 1000)
            {
                // SQL Server giới hạn 2100 tham số mỗi câu lệnh
                var batch = ids.Skip(i).Take(1000).ToList();
                foreach (Guid id in _dbImg.Query<Guid>(query, new { Ids = batch }))
                    result.Add(id);
            }
            return result;
        }

        public void UpdateImeiSerial(string diemdanhnguoilxId)
        {
            _db.Execute("UPDATE DiemDanhNguoiLx SET serialNumber = 'C0166ECC81AA4C106456', imei = 'EMC865651046973859' WHERE DiemDanhNguoiLxID = @Id",
                new { Id = diemdanhnguoilxId });
        }

        #endregion

        #region Chi tiết phiên (frmDetailSession)

        public List<PointLatLng> GetlstPoint(string diemdanhnguoilxId)
        {
            const string query = "SELECT Lat, Lng FROM VehicleHistory WHERE DiemDanhNguoiLxId = @id AND Lat IS NOT NULL AND Lng IS NOT NULL ORDER BY TongQuangDuongPhien ASC, CreateDate ASC";
            DataTable dt = _db.QueryToDataTable(query, new { id = diemdanhnguoilxId });
            return dt.AsEnumerable()
                .Select(item => new PointLatLng(D(item, "Lat"), D(item, "Lng")))
                .Where(p => p.Lat != 0 || p.Lng != 0)
                .ToList();
        }

        public DataTable GetDetailSession(string diemdanhnguoilxId)
        {
            const string query = @"SELECT TenGV, dd.NguoiLxID, hv.HoVaTen, kh.KhoaHocID, hv.TenKH, dd.DiemDanhNguoiLxID, dd.BienSo, dd.HangXe, dd.ThoiDiem, dd.ThoiDiem_Logout, dd.Logout, TongQuangDuongLD_Phien, TongThoiGianLD_Phien, Lat, Lng, Lat_Logout, Lng_Logout
                    FROM DiemDanhNguoiLx AS dd
                    JOIN NguoiLX AS hv ON dd.NguoiLxID = hv.NguoiLXID
                    JOIN KhoaHoc as KH ON hv.KhoaHocID = kh.KhoaHocID
                    WHERE DiemDanhNguoiLxId = @id";
            return _db.QueryToDataTable(query, new { id = diemdanhnguoilxId });
        }

        public List<ChiTietPhienHoc> GetHistoryData(string diemdanhnguoilxId)
        {
            const string query = @"SELECT dt.HistoryID, dt.ThoiDiem, dt.VanToc, TongQuangDuongLD_Phien, TongThoiGianLD_Phien, Lat, Lng, FaceStatus, img.AnhChup
                FROM HistoryData as dt JOIN EMCDAT_IMAGE.dbo.HistoryData as img ON dt.HistoryID = img.HistoryID
                WHERE dt.DiemDanhNguoiLxId = @id
                ORDER BY ThoiDiem";
            DataTable dt = _db.QueryToDataTable(query, new { id = diemdanhnguoilxId });

            return dt.AsEnumerable().Select(item => new ChiTietPhienHoc
            {
                Id = (Guid)item["HistoryID"],
                ThoiDiem = (DateTime)item["ThoiDiem"],
                VanToc = Math.Round(D(item, "VanToc"), 3),
                QuangDuong = Math.Round(D(item, "TongQuangDuongLD_Phien"), 3),
                ThoiGian = D(item, "TongThoiGianLD_Phien"),
                Lat = D(item, "Lat"),
                Lng = D(item, "Lng"),
                XacThuc = item["FaceStatus"] != DBNull.Value && Convert.ToBoolean(item["FaceStatus"]),
                AnhXacThuc = item["AnhChup"] as byte[]
            }).ToList();
        }

        public List<ChiTietPhienHoc> GetVehicleHistory(string diemdanhnguoilxId)
        {
            const string query = "SELECT CreateDate, Lat, Lng, VanToc, TongQuangDuongPhien, TongThoiGianPhien, FaceStatus " +
                "FROM VehicleHistory WHERE DiemDanhNguoiLxId = @id ORDER BY CreateDate ASC";
            DataTable dt = _db.QueryToDataTable(query, new { id = diemdanhnguoilxId });

            return dt.AsEnumerable().Select(item => new ChiTietPhienHoc
            {
                ThoiDiem = (DateTime)item["CreateDate"],
                VanToc = Math.Round(D(item, "VanToc"), 3),
                QuangDuong = Math.Round(D(item, "TongQuangDuongPhien"), 3),
                ThoiGian = D(item, "TongThoiGianPhien"),
                Lat = D(item, "Lat"),
                Lng = D(item, "Lng"),
                XacThuc = item["FaceStatus"] != DBNull.Value && Convert.ToBoolean(item["FaceStatus"])
            }).ToList();
        }

        public void UpdateImageLogout(string diemdanhnguoilxId, Guid historyId)
        {
            _dbImg.Execute(@"UPDATE DiemDanhNguoiLx
                SET AnhChup_Logout = (SELECT AnhChup FROM HistoryData WHERE DiemDanhNguoiLxId = @id AND HistoryID = @historyId)
                WHERE DiemDanhNguoiLxId = @id", new { id = diemdanhnguoilxId, historyId });
        }

        public void UpdateImageLogin(string diemdanhnguoilxId, Guid historyId)
        {
            _dbImg.Execute(@"UPDATE DiemDanhNguoiLx
                SET AnhChup = (SELECT AnhChup FROM HistoryData WHERE DiemDanhNguoiLxId = @id AND HistoryID = @historyId)
                WHERE DiemDanhNguoiLxId = @id", new { id = diemdanhnguoilxId, historyId });
        }

        public void InsertHinhDem(int khoaHocID, long nguoiLxID, string diemdanhnguoilxId, Guid historyId)
        {
            const string query = @"
                DECLARE @Image VARBINARY(MAX)
                DECLARE @AddDated DATETIME = GETDATE()
                DECLARE @LengthImage INT

                SELECT @Image = AnhChup
                FROM EMCDAT_IMAGE.dbo.HistoryData
                WHERE DiemDanhNguoiLxId = @id AND HistoryID = @historyId

                SET @LengthImage = DATALENGTH(@Image)

                EXEC dbo.spAddFace @MaCSDT, @khoaHocID, @nguoiLxID, 11, @AddDated, @Image, @LengthImage";
            _db.Execute(query, new { id = diemdanhnguoilxId, historyId, khoaHocID, nguoiLxID, MaCSDT });
        }

        #endregion
    }
}
