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
    /// <summary>Phân công đào tạo DAT: lịch theo tháng, xe số tự động, gán giáo viên / xe cho học viên.</summary>
    public class PhanCongDAO : DatDaoBase
    {
        #region Lịch đào tạo theo tháng (frmPhanCongDaoTao)

        public List<Schedule> GetAllSchedule(int monthId)
        {
            // Mỗi dòng = một giáo viên của một khóa trong tháng (kèm xe tập lái, xe STĐ và thời gian chạy DAT)
            const string query = @"SELECT DISTINCT kh.KhoaHocId, kh.MaKH, kh.TenKH, HV.GiaoVienID, ThoiDiemChayDAT, ThoiDiemKetThucDAT, gv.HoTenDem, gv.TenGV, MaXeTap, XeB11, kh.HangDaoTao
                FROM NguoiLX HV
                JOIN GiaoVien gv ON gv.GiaoVienID = hv.GiaoVienID
                JOIN KhoaHoc kh ON hv.KhoaHocId = kh.KhoaHocID
                WHERE kh.MonthID = @monthId";

            DataTable dt = _db.QueryToDataTable(query, new { monthId });

            return dt.AsEnumerable().Select(item => new Schedule
            {
                KhoaHocId = (int)item["KhoaHocId"],
                MaKH = S(item, "MaKH"),
                TenKH = S(item, "TenKH"),
                GiaoVienId = (int)item["GiaoVienID"],
                GiaoVien = $"{S(item, "HoTenDem")} {S(item, "TenGV")}".Trim(),
                ThoiDiemChayDAT = item["ThoiDiemChayDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemChayDAT"],
                ThoiDiemKetThucDAT = item["ThoiDiemKetThucDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemKetThucDAT"],
                MaXeTap = S(item, "MaXeTap"),
                MaXeTapB11 = S(item, "XeB11"),
                HangDT = S(item, "HangDaoTao")
            }).ToList();
        }

        /// <summary>
        /// Xe số tự động (hạng B.01) và mức đang được dùng: số khóa / học viên có XeB11 là xe này mà
        /// thời gian chạy DAT chưa kết thúc, và ngày kết thúc DAT xa nhất ("sử dụng đến").
        /// </summary>
        public List<XeSTDSuDung> GetXeSTDSuDung()
        {
            const string query = @"SELECT xe.MaXe, xe.BienSoXe, xe.SuDung,
                       MAX(hv.ThoiDiemKetThucDAT) AS SuDungDen,
                       COUNT(DISTINCT CASE WHEN hv.ThoiDiemKetThucDAT >= CAST(GETDATE() AS date) THEN hv.KhoaHocID END) AS SoKhoaDangDung,
                       SUM(CASE WHEN hv.ThoiDiemKetThucDAT >= CAST(GETDATE() AS date) THEN 1 ELSE 0 END) AS SoHVDangDung
                FROM XeTapLai xe
                LEFT JOIN NguoiLX hv ON hv.XeB11 = xe.MaXe
                WHERE REPLACE(xe.HangGPLX, '.', '') = 'B01'
                GROUP BY xe.MaXe, xe.BienSoXe, xe.SuDung
                ORDER BY xe.BienSoXe";
            return _db.Query<XeSTDSuDung>(query).ToList();
        }

        public int UpdateXeTapB11(Schedule rowdata)
        {
            return _db.Execute("UPDATE NguoiLX SET XeB11 = @xe WHERE KhoaHocID = @KhoaHocId AND GiaoVienID = @GiaoVienId",
                new { xe = string.IsNullOrEmpty(rowdata.MaXeTapB11) ? null : rowdata.MaXeTapB11, rowdata.KhoaHocId, rowdata.GiaoVienId });
        }

        public int UpdatePhanCongDAT(int khoahocid, DateTime tu, DateTime den)
        {
            // Từ 00:00:01 ngày bắt đầu đến 23:59:59 ngày kết thúc (giống cách ghi cũ)
            return _db.Execute("UPDATE NguoiLX SET ThoiDiemChayDAT = @tu, ThoiDiemKetThucDAT = @den WHERE KhoaHocID = @khoahocid",
                new { tu = tu.Date.AddSeconds(1), den = den.Date.AddDays(1).AddSeconds(-1), khoahocid });
        }

        #endregion

        #region Phân công từ Excel (Uc_PhanCongDAT)

        /// <summary>Học viên DAT theo danh sách mã đăng ký (kèm giáo viên, xe đang gán).</summary>
        public List<NguoiLxPhanCong> GetNguoiLxByMaDK(IEnumerable<string> maDK)
        {
            var result = new List<NguoiLxPhanCong>();
            const string query = @"SELECT nlx.MaDK, nlx.HoVaTen, nlx.TenKH, nlx.GiaoVienID, nlx.MaXeTap,
                                          LTRIM(RTRIM(ISNULL(gv.HoTenDem, '') + ' ' + ISNULL(gv.TenGV, ''))) AS TenGiaoVien
                                   FROM NguoiLX nlx LEFT JOIN GiaoVien gv ON gv.GiaoVienID = nlx.GiaoVienID
                                   WHERE nlx.MaDK IN @ma";
            // SQL Server giới hạn ~2100 tham số mỗi lệnh
            foreach (var chunk in maDK.Distinct().Select((m, i) => new { m, i }).GroupBy(x => x.i / 1000, x => x.m))
                result.AddRange(_db.Query<NguoiLxPhanCong>(query, new { ma = chunk.ToList() }));
            return result;
        }

        public List<GiaoVienTen> GetGiaoVienTen()
        {
            return _db.Query<GiaoVienTen>(
                "SELECT GiaoVienID, LTRIM(RTRIM(ISNULL(HoTenDem, '') + ' ' + ISNULL(TenGV, ''))) AS HoTen FROM GiaoVien").ToList();
        }

        public List<XeTapLaiMa> GetXeTapLaiMa()
        {
            return _db.Query<XeTapLaiMa>("SELECT MaXe, BienSoXe FROM XeTapLai").ToList();
        }

        public int AssignGiaoVienXe(string maDK, int giaoVienId, string maXe)
        {
            return _db.Execute("UPDATE NguoiLX SET GiaoVienID = @giaoVienId, MaXeTap = @maXe WHERE MaDK = @maDK",
                new { maDK, giaoVienId, maXe });
        }

        #endregion
    }
}
