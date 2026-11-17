using Dapper;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    /// <summary>Xe tập lái (danh mục XeTapLai) và thiết bị DAT gắn trên xe (Vehicles).</summary>
    public class XeTapLaiDAO : DatDaoBase
    {
        public List<XeTapLai> GetLstXeTapLai()
        {
            // SuDungDen = ngày kết thúc chạy DAT xa nhất của các học viên được gán xe này (xe tập lái hoặc xe STĐ)
            const string query = @"SELECT xe.ID, xe.BienSoXe, xe.MaXe, xe.HangGPLX, xe.NamSX, xe.SoGPXTL,
                                          xe.NgayCapGPXTL, xe.NgayHHGPXTL, xe.ChuSoHuu, xe.SuDung, dung.SuDungDen
                                   FROM XeTapLai xe
                                   OUTER APPLY (SELECT MAX(hv.ThoiDiemKetThucDAT) AS SuDungDen
                                                FROM NguoiLX hv
                                                WHERE hv.MaXeTap = xe.MaXe OR hv.XeB11 = xe.MaXe) dung
                                   ORDER BY xe.BienSoXe ASC";
            DataTable dt = _db.QueryToDataTable(query);

            return dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["ID"],
                BienSoXe = S(item, "BienSoXe"),
                MaXe = S(item, "MaXe"),
                HangGPLX = S(item, "HangGPLX"),
                NamSX = item["NamSX"] == DBNull.Value ? 0 : Convert.ToInt32(item["NamSX"]),
                SoGPXTL = S(item, "SoGPXTL"),
                NgayCapGPXTL = item["NgayCapGPXTL"] == DBNull.Value ? DateTime.MinValue : (DateTime)item["NgayCapGPXTL"],
                NgayHHXTL = item["NgayHHGPXTL"] == DBNull.Value ? DateTime.MinValue : (DateTime)item["NgayHHGPXTL"],
                ChuSoHuu = S(item, "ChuSoHuu"),
                ThanhLy = item["SuDung"] != DBNull.Value && !(bool)item["SuDung"],
                SuDungDen = item["SuDungDen"] == DBNull.Value ? (DateTime?)null : (DateTime)item["SuDungDen"]
            }).ToList();
        }

        /// <summary>Cập nhật chủ sở hữu và tình trạng (SuDung = 0 khi thanh lý) của xe tập lái.</summary>
        public int UpdateXeDaoTao(XeTapLai xeTap)
        {
            if (string.IsNullOrWhiteSpace(xeTap.MaXe))
                throw new ArgumentException("Thiếu mã xe (MaXe) — không xác định được xe cần cập nhật.", nameof(xeTap));

            const string query = "UPDATE XeTapLai SET ChuSoHuu = @ChuSoHuu, SuDung = @SuDung WHERE MaXe = @MaXe and ID = @ID ";
            return _db.Execute(query, new { xeTap.ChuSoHuu, SuDung = !xeTap.ThanhLy, xeTap.MaXe, ID = xeTap.VehicleID });
        }

        public List<XeTapLai> GetLstXeTapLaiOnline()
        {
            const string query = "SELECT VehicleID, BienSoXe, MaXe, Version, HangGPLX, Imei, VanToc, UpdateTime FROM Vehicles WHERE online = 1 ORDER BY BienSoXe ASC";
            DataTable dt = _db.QueryToDataTable(query);

            return dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["VehicleID"],
                BienSoXe = (string)item["BienSoXe"],
                MaXe = item["MaXe"] == DBNull.Value ? "Null" : (string)item["MaXe"],
                Imei = item["Imei"] == DBNull.Value ? "Null" : (string)item["Imei"],
                Version = item["Version"] == DBNull.Value ? "Null" : (string)item["Version"],
                HangGPLX = (string)item["HangGPLX"],
                ThoiDiem = (DateTime)item["UpdateTime"],
                VanToc = item["VanToc"] == DBNull.Value ? 0 : Math.Round((double)item["VanToc"], 1)
            }).ToList();
        }

        public List<XeTapLai> GetAllVehicles()
        {
            DataTable dt = _db.QueryToDataTable("SELECT * FROM Vehicles ORDER BY BienSoXe ASC");

            return dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = item.Field<int>("VehicleID"),
                MaXe = item["MaXe"] == DBNull.Value ? "Null" : item.Field<string>("MaXe"),
                SerialNumber = item["SerialNumber"] == DBNull.Value ? "Null" : item.Field<string>("SerialNumber"),
                Imei = item["Imei"] == DBNull.Value ? "Null" : (string)item["Imei"],
                BienSoXe = (string)item["BienSoXe"],
                HangGPLX = (string)item["HangGPLX"],
                Version = item["Version"] == DBNull.Value ? "Null" : (string)item["Version"],
                TrangThai = (bool)item["Active"],
                CreateDate = (DateTime)item["CreateDate"],
                ThoiDiem = (DateTime)item["UpdateTime"],

                LoginCCCD = item.Field<bool>("allowLoginWithID"),
                LoginOline = item.Field<bool>("bLoginOnline"),
                ShowInfoClass = item.Field<bool>("bShowReportTraining"),
                Accumulation = item.Field<bool>("bTichLuyPhienDangTap"),
                CheckTeacher = item.Field<bool>("bCheckGiaoVienHuongDan"),
                CheckCar = item.Field<bool>("bCheckXeDaoTao"),
                CheckTimeTeacher = item.Field<bool>("bCheckTotalTimeTeacher24h"),
                OptimizeFace = item.Field<bool>("bToiUuNhanDien")
            }).ToList();
        }

        public int UpdateXeTapLai(int vehicleId, string imei, XeTapLai xeTap, bool status, bool loginCCCD)
        {
            const string query = "EXEC [dbo].[spAlterVehicle] @MaCSDT, @VehicleID, @MaXe, @BienSoXe, @HangGPLX, @Imei, @Active, @NamSX, @SoGPXTL, @NgayCapGPXTL, @NgayHHGPXTL, @LoginCCCD";
            return _db.Execute(query, new
            {
                MaCSDT,
                VehicleID = vehicleId,
                xeTap.MaXe,
                xeTap.BienSoXe,
                xeTap.HangGPLX,
                Imei = imei,
                Active = status,
                xeTap.NamSX,
                xeTap.SoGPXTL,
                // giữ định dạng chuỗi như bản cũ (stored procedure nhận ngày dạng chuỗi)
                NgayCapGPXTL = xeTap.NgayCapGPXTL.ToString("yyyy-MM-dd 00:00:00"),
                NgayHHGPXTL = xeTap.NgayHHXTL.ToString("yyyy-MM-dd 00:00:00"),
                LoginCCCD = loginCCCD
            });
        }

        public int DeleteXeTapLai(int vehicleId, string imei)
        {
            return _db.Execute("DELETE FROM [dbo].[Vehicles] WHERE VehicleID = @vehicleId AND Imei = @imei", new { vehicleId, imei });
        }

        #region Import file XML danh sách xe tập lái (DSXTL)

        /// <summary>Dữ liệu hiện có của một xe trong bảng XeTapLai, chỉ dùng để so sánh khi import.</summary>
        private class XeHienTai
        {
            public int ID { get; set; }
            public string MaXe { get; set; }
            public string SoGPXTL { get; set; }
            public DateTime? NgayCapGPXTL { get; set; }
            public DateTime? NgayHHGPXTL { get; set; }
            public bool? SuDung { get; set; }
        }

        /// <summary>
        /// So sánh xe trong file với bảng XeTapLai theo MaXe (= BIENSOXE) — CHỈ ĐỌC CSDL.
        /// Ghi kết quả vào HanhDong / IdHienTai / ThayDoi của từng xe và trả về các xe thuộc cơ sở đào tạo này.
        /// Đã có và không đổi → GiuNguyen; đổi Số GPXTL / ngày cấp / ngày hết hạn / tình trạng sử dụng → CapNhat; chưa có → ThemMoi.
        /// </summary>
        public List<XeTapLaiXml> BuildImportPlan(List<XeTapLaiXml> xeTrongFile)
        {
            var hienTai = _db.Query<XeHienTai>("SELECT ID, MaXe, SoGPXTL, NgayCapGPXTL, NgayHHGPXTL, SuDung FROM XeTapLai")
                             .Where(r => !string.IsNullOrWhiteSpace(r.MaXe))
                             .GroupBy(r => Helper.XeTapLaiXmlReader.NormalizeMaXe(r.MaXe))
                             .ToDictionary(g => g.Key, g => g.OrderByDescending(r => r.ID).First());

            var ketQua = xeTrongFile.Where(x => string.IsNullOrEmpty(x.MaCSDT) || x.MaCSDT == MaCSDT).ToList();
            foreach (var x in ketQua)
            {
                x.ThayDoi.Clear();
                if (!hienTai.TryGetValue(x.MaXe, out XeHienTai cu))
                {
                    x.HanhDong = ImportAction.ThemMoi;
                    x.IdHienTai = null;
                    continue;
                }

                x.IdHienTai = cu.ID;

                if (!string.Equals((cu.SoGPXTL ?? "").Trim(), x.SoGPXTL, StringComparison.OrdinalIgnoreCase))
                    x.ThayDoi.Add($"Số GPXTL: {Show(cu.SoGPXTL)} → {Show(x.SoGPXTL)}");
                if (cu.NgayCapGPXTL?.Date != x.NgayCapGPXTL?.Date)
                    x.ThayDoi.Add($"Ngày cấp: {Show(cu.NgayCapGPXTL)} → {Show(x.NgayCapGPXTL)}");
                if (cu.NgayHHGPXTL?.Date != x.NgayHHGPXTL?.Date)
                    x.ThayDoi.Add($"Hết hạn: {Show(cu.NgayHHGPXTL)} → {Show(x.NgayHHGPXTL)}");
                if (cu.SuDung != x.DangHoatDong)   // ApplyImport ghi SuDung = 1 nếu TRANGTHAI = 1, ngược lại 0
                    x.ThayDoi.Add($"Sử dụng: {ShowSuDung(cu.SuDung)} → {ShowSuDung(x.DangHoatDong)}");

                x.HanhDong = x.ThayDoi.Count > 0 ? ImportAction.CapNhat : ImportAction.GiuNguyen;
            }
            return ketQua;
        }

        /// <summary>Ghi các xe ThemMoi / CapNhat trong một transaction (lỗi giữa chừng thì không ghi gì). Trả về (số thêm, số cập nhật).</summary>
        public (int Them, int CapNhat) ApplyImport(List<XeTapLaiXml> danhSach)
        {
            int nThem = 0, nSua = 0;
            _db.ExecuteTransaction((conn, tran) =>
            {
                foreach (var x in danhSach.Where(x => x.HanhDong == ImportAction.CapNhat))
                {
                    // Chỉ cập nhật 3 trường GPXTL và tình trạng sử dụng; thông tin khác của xe giữ nguyên
                    nSua += conn.Execute(
                        @"UPDATE XeTapLai
                          SET SoGPXTL = @SoGPXTL, NgayCapGPXTL = @NgayCapGPXTL, NgayHHGPXTL = @NgayHHGPXTL,
                              SuDung = CASE WHEN @DangHoatDong = 1 THEN 1 ELSE 0 END
                          WHERE ID = @ID",
                        new { x.SoGPXTL, x.NgayCapGPXTL, x.NgayHHGPXTL, x.DangHoatDong, ID = x.IdHienTai.Value }, tran);
                }

                foreach (var x in danhSach.Where(x => x.HanhDong == ImportAction.ThemMoi))
                {
                    nThem += conn.Execute(
                        @"INSERT INTO XeTapLai (MaCSDT, BienSoXe, HangGPLX, SoDK, NamSX, SoGPXTL, CoQuanCapGPXTL,
                                                NgayCapGPXTL, NgayHHGPXTL, ChuSoHuu, MaXe, SendTongCuc, MauXe, NhanHieuXe, SuDung)
                          VALUES (@MaCSDT, @BienSoXe, @HangGPLX, @SoDK, @NamSX, @SoGPXTL, @CoQuanCapGPXTL,
                                  @NgayCapGPXTL, @NgayHHGPXTL, N'', @MaXe, 0, @MauXe, @NhanHieuXe, @DangHoatDong)",
                        new
                        {
                            MaCSDT = string.IsNullOrEmpty(x.MaCSDT) ? MaCSDT : x.MaCSDT,
                            x.BienSoXe,
                            HangGPLX = x.HangDaoTao,
                            SoDK = NullIfEmpty(x.SoDK),
                            x.NamSX,
                            x.SoGPXTL,
                            CoQuanCapGPXTL = NullIfEmpty(x.CoQuanCapGPXTL),
                            x.NgayCapGPXTL,
                            x.NgayHHGPXTL,
                            x.MaXe,
                            MauXe = NullIfEmpty(x.MauXe),
                            NhanHieuXe = NullIfEmpty(x.NhanHieu),
                            x.DangHoatDong
                        }, tran);
                }
            });
            return (nThem, nSua);
        }

        private static string Show(string s) => string.IsNullOrWhiteSpace(s) ? "(trống)" : s.Trim();
        private static string Show(DateTime? d) => d?.ToString("dd/MM/yyyy") ?? "(trống)";
        private static string ShowSuDung(bool? v) => v == null ? "(trống)" : v.Value ? "Đang sử dụng" : "Đã thanh lý";
        private static string NullIfEmpty(string s) => string.IsNullOrWhiteSpace(s) ? null : s;

        #endregion
    }
}
