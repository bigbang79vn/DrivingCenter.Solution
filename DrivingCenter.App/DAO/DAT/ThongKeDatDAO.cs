using System.Collections.Generic;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    /// <summary>Số liệu tổng quan cho Trang chủ (học viên, khóa học, giáo viên, xe, số phiên trong ngày).</summary>
    public class ThongKeDatDAO : DatDaoBase
    {
        public List<int> LstYear()
        {
            return _db.Query<int>("SELECT DISTINCT DATEPART(year, CreateDate) AS Nam FROM NguoiLX ORDER BY Nam DESC").ToList();
        }

        public int LoadSoHocVienByYear(int year)
        {
            return _db.Scalar<int>("SELECT COUNT(*) FROM NguoiLX WHERE KhoaHocID IN (SELECT KhoaHocID FROM KhoaHoc WHERE YEAR(NgayKG) = @year)", new { year });
        }

        public int LoadSoKhoaHocByYear(int year)
        {
            return _db.Scalar<int>("SELECT COUNT(*) FROM KhoaHoc WHERE DATEPART(year, NgayKG) = @year", new { year });
        }

        public int LoadSoHocVien() => _db.Scalar<int>("SELECT COUNT(*) FROM NguoiLX");

        public int LoadSoKhoaHoc() => _db.Scalar<int>("SELECT COUNT(*) FROM KhoaHoc");

        public int LoadSoGiaoVien() => _db.Scalar<int>("SELECT COUNT(*) FROM GiaoVien");

        public int LoadSoXeTapLai() => _db.Scalar<int>("SELECT COUNT(*) FROM Vehicles");

        /// <summary>Số phiên hợp lệ (từ 5 km) trong một ngày.</summary>
        public int CountPhien(int day, int month, int year)
        {
            const string query = "SELECT COUNT(MaCSDT) FROM DiemDanhNguoiLx " +
                "WHERE ActivePhien = 1 AND TongQuangDuongLD_Phien >= 5 AND CONVERT(varchar, ThoiDiem, 103) = @ngay " +
                "GROUP BY MaCSDT";
            return _db.Scalar<int>(query, new { ngay = $"{day:00}/{month:00}/{year:0000}" });
        }
    }
}
