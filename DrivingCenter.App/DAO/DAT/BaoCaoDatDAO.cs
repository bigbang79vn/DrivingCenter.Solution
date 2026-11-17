using DrivingCenter.App.Common;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Dtos.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    /// <summary>Báo cáo / tổng hợp DAT: theo khóa, theo ngày, theo giáo viên, theo học viên.</summary>
    public class BaoCaoDatDAO : DatDaoBase
    {
        #region Theo khóa học

        public List<TongHopHVByKhoa> TongHopHvByKhoa(int khoahocId)
        {
            const string query = @"SELECT NguoiLXID, KhoaHocID, MaDK, HoTenGV, HoVaTen, HangDaoTao, COUNT(NgayHoc) AS SoNgayHoc, ROUND(SUM(TongQuangDuongLD_Phien),3) as TongQuangDuongLD_Phien, ROUND(SUM(TongThoiGianLD_Phien),1) as TongThoiGianLD_Phien, ROUND(SUM(QuangDuongPhienGuiTC),3) as QuangDuongPhienGuiTC, ROUND(SUM(ThoiGianPhienGuiTC),3) as ThoiGianPhienGuiTC
                FROM KQHvGroup
                WHERE KhoaHocID = @khoahocId
                GROUP BY NguoiLXID, KhoaHocID, MaDK, HoTenGV, HoVaTen, HangDaoTao
                ORDER BY HoTenGV";

            DataTable dt = _db.QueryToDataTable(query, new { khoahocId });

            return dt.AsEnumerable().Select(item => new TongHopHVByKhoa
            {
                NguoiLxID = (Int64)(item["NguoiLXID"]),
                MaDK = (string)(item["MaDK"]),
                HoTenGV = (string)(item)["HoTenGV"],
                HoVaTenHV = (string)(item)["HoVaTen"],
                HangDaoTao = (string)(item)["HangDaoTao"],
                TongQuangDuongTichLuy = (double)(item)["TongQuangDuongLD_Phien"],
                TongThoiGianTichLuy = (double)(item)["TongThoiGianLD_Phien"],
                QuangDuongPhienGuiTC = (item)["QuangDuongPhienGuiTC"] == DBNull.Value ? 0 : (double)(item)["QuangDuongPhienGuiTC"],
                ThoiGianPhienGuiTC = (item)["ThoiGianPhienGuiTC"] == DBNull.Value ? 0 : (double)(item)["ThoiGianPhienGuiTC"],
                SoNgayHocKQ = (int)(item["SoNgayHoc"])
            }).ToList();
        }

        /// <summary>Quãng đường / thời gian trên xe số tự động (HangXe 'B.01') của từng học viên trong khóa.</summary>
        public List<TongHopHVByKhoa> TongHopPhB11(int khoahocId)
        {
            const string query = "SELECT hv.MaDK, ph.HangXe , ROUND(SUM(ph.TongQuangDuongLD_Phien),1) AS TongQuangDuong_B11 , SUM(ph.TongThoiGianLD_Phien) AS TongThoiGian_B11 , ROUND(SUM(TongQuangDuongLD_BanDem),1) AS TongQuangDuongB11_PhienDem, " +
                "ROUND( SUM(ph.TongThoiGianLD_BanDem), 1) AS TongThoiGianB11_PhienDem  " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHocDangDT AS kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ActivePhien = 1 and HangXe = @HangXe and kh.KhoaHocID = @khoahocId " +
                "GROUP BY hv.MaDK, hv.HangDaoTao, kh.KhoaHocID, ph.HangXe";

            DataTable dt = _db.QueryToDataTable(query, new { khoahocId, HangXe = HangGplx.B01 });

            return dt.AsEnumerable().Select(item => new TongHopHVByKhoa
            {
                MaDK = (string)(item["MaDK"]),
                HangXe = (string)(item)["HangXe"],
                TongQuangDuong_B1 = (double)(item)["TongQuangDuong_B11"],
                TongThoiGian_B1 = (double)(item)["TongThoiGian_B11"],
                TongQuangDuong_PhienDemB1 = item["TongQuangDuongB11_PhienDem"] == DBNull.Value ? 0 : (double)(item)["TongQuangDuongB11_PhienDem"],
                TongThoiGian_PhienDemB1 = item["TongThoiGianB11_PhienDem"] == DBNull.Value ? 0 : (double)(item)["TongThoiGianB11_PhienDem"]
            }).ToList();
        }

        /// <summary>Giờ ban đêm; học viên số sàn không tính phiên trên xe số tự động.</summary>
        public List<TongHopHVByKhoa> TongHopPhBD(int khoahocId, string hangdaotao)
        {
            string query = "SELECT MaDK, TongQuangDuongLD_Phien, TongThoiGianLD_Phien FROM vGioBanDemGroup WHERE KhoaHocID = @khoahocId";
            if (!HangGplx.IsSoTuDong(hangdaotao))
                query += " AND HangXe <> @HangXe";

            DataTable dt = _db.QueryToDataTable(query, new { khoahocId, HangXe = HangGplx.B01 });

            return dt.AsEnumerable().Select(item => new TongHopHVByKhoa
            {
                MaDK = (string)(item["MaDK"]),
                TongQuangDuong_PhienDem = item["TongQuangDuongLD_Phien"] == DBNull.Value ? 0 : (double)(item)["TongQuangDuongLD_Phien"],
                TongThoiGian_PhienDem = item["TongThoiGianLD_Phien"] == DBNull.Value ? 0 : (double)(item)["TongThoiGianLD_Phien"]
            }).ToList();
        }

        /// <summary>Tổng hợp giờ dạy của giáo viên theo tên khóa học.</summary>
        public List<TongHopGV> TongHopGVsByKh(string khoahoc)
        {
            const string query = "SELECT MaGV , HoTenGV, ThoiDiem, COUNT(HoVaTen) AS SoHocVien, SUM(SoPhien) AS SoPhien, TenKH, BienSoXe, ROUND(SUM(TongQuangDuongLD_Phiens),1) as SQuangDuong, SUM(TongThoiGianLD_Phiens) as TongThoiGian, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianLD_Phiens),0),108) as SThoiGian " +
                "FROM TongHopGvFn " +
                "WHERE TenKH = @khoahoc " +
                "GROUP BY MaGV, HoTenGV, ThoiDiem, BienSoXe, TenKH " +
                "ORDER BY HoTenGV";

            DataTable dt = _db.QueryToDataTable(query, new { khoahoc });
            return MapTongHopGV(dt, "TongThoiGian");
        }

        public List<TienDo> TongHopTienDoDatByKH(int khoahocid, string makh)
        {
            const string query = "SELECT * FROM TongHop_TienDo WHERE KhoaHocID = @khoahocid AND MaKH = @makh ORDER BY MaGV ASC, Ngay ASC";
            DataTable dt = _db.QueryToDataTable(query, new { khoahocid, makh });

            return dt.AsEnumerable().Select(item => new TienDo
            {
                ThuOfWeek = item["Thu"].ToString(),
                Ngay = item["Ngay"].ToString(),
                MaGV = item["MaGV"].ToString(),
                TenGV = item["HoTenGV"].ToString(),
                MaKH = item["MaKH"].ToString(),
                TenKH = item["TenKH"].ToString(),
                HangDaoTao = item["HangDaoTao"].ToString(),
                XeTap = item["BienSoXe"].ToString(),
                TenBaiTap = item["BaiTap"].ToString(),
                SoKmTH = (double)item["TongQuangDuong"],
                SoGioTH = (double)item["TongThoiGian"]
            }).ToList();
        }

        public DataTable BaoCaoKetQuaHocVien(int KhoaHocID, int KieuKetQua)
        {
            const string query = "EXEC [dbo].[spQuaTrinhDaoTaoKhoaHoc] @MaCSDT = @MaCSDT, @KhoaHocID = @KhoaHocID, @TieuDe = @TieuDe, @Result = @Result";
            return _db.QueryToDataTable(query, new
            {
                MaCSDT,
                KhoaHocID,
                TieuDe = "BÁO CÁO KẾT QUẢ THỰC HÀNH LÁI XE CỦA KHÓA HỌC",
                Result = KieuKetQua
            });
        }

        #endregion

        #region Theo ngày

        /// <summary>Tổng giờ học của từng học viên trong ngày (dd/MM/yyyy).</summary>
        public List<TongHopHVByDay> TongHopGioHVByDay(string day)
        {
            const string query = "SELECT ph.MaDK, CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, hv.HoVaTen, kh.TenKH, pH.BienSo as BienSoXe, ROUND(SUM(ph.TongQuangDuongLD_Phien), 1) AS TongQuangDuongLD_Phien, CONVERT(varchar, DATEADD(second, SUM(ph.TongThoiGianLD_Phien), 0), 108) AS TongThoiGianLD_Phien " +
                "FROM DiemDanhNguoiLx as ph, GiaoVien as gv, NguoiLX as hv, KhoaHocDangDT as kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and CONVERT(VARCHAR, ThoiDiem, 103) = @day " +
                "GROUP BY CONCAT(gv.HoTenDem, ' ', gv.TenGV), ph.MaDK, hv.HoVaTen, kh.TenKH, PH.BienSo " +
                "ORDER BY HoTenGV";

            DataTable dt = _db.QueryToDataTable(query, new { day });
            return dt.Rows.Cast<DataRow>().Select(item => new TongHopHVByDay(item)).ToList();
        }

        /// <summary>Tổng hợp giờ dạy giáo viên trong ngày: flag = 0 dùng thủ tục spTongHopGioDay, ngược lại đọc TongHopGvFn.</summary>
        public List<TongHopGV> TongHopGVsByDay(string day, int flag)
        {
            DataTable dt;
            if (flag == 0)
            {
                dt = _db.QueryToDataTable("EXEC [dbo].[spTongHopGioDay] @NgayDay = @day", new { day });
            }
            else
            {
                const string query = @"SELECT MaGV , HoTenGV, ThoiDiem, COUNT(HoVaTen) AS SoHocVien, SUM(SoPhien) AS SoPhien, TenKH, BienSoXe, ROUND(SUM(TongQuangDuongLD_Phiens),1) as SQuangDuong, SUM(TongThoiGianLD_Phiens) as TongThoiGianLD_Phiens, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianLD_Phiens),0),108) as SThoiGian
                FROM TongHopGvFn
                WHERE ThoiDiem = @day
                GROUP by MaGV, HoTenGV, ThoiDiem, BienSoXe, TenKH
                ORDER by HoTenGV";
                dt = _db.QueryToDataTable(query, new { day });
            }

            return MapTongHopGV(dt, "TongThoiGianLD_Phiens");
        }

        private static List<TongHopGV> MapTongHopGV(DataTable dt, string cotTongThoiGian)
        {
            return dt.AsEnumerable().Select(item => new TongHopGV
            {
                MaGv = item["MaGV"].ToString(),
                SHoTenGV = item["HoTenGV"].ToString(),
                NgayTap = item["ThoiDiem"].ToString(),
                SoPhien = (int)item["SoPhien"],
                TenKhoaHoc = item["TenKH"].ToString(),
                BienSoXeGv = item["BienSoXe"].ToString(),
                SoHocVienByDay = (int)item["SoHocVien"],
                SQuangDuong = (double)item["SQuangDuong"],
                TongThoiGian = (double)item[cotTongThoiGian],
                SThoiGian = item["SThoiGian"].ToString()
            }).ToList();
        }

        #endregion

        #region Theo học viên

        public string GetMaDKByNguoiLxId(Int64 nguoiLxId)
        {
            return _db.Scalar<string>("SELECT MaDK FROM NguoiLX WHERE NguoiLXID = @nguoiLxId", new { nguoiLxId });
        }

        public DataTable GetKetQuaDaoTaoByMaDK(string maDK)
        {
            const string query = @"SELECT img.AnhChup, img.AnhChup_Logout, ph.DiemDanhNguoiLxID, ph.BienSo, ph.HangXe, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND(ph.TongQuangDuongLD_Phien,4) AS TongQuangDuongLD_Phien, ph.TongThoiGianLD_Phien as Time, CONVERT(VARCHAR, DATEADD(SECOND, ph.TongThoiGianLD_Phien, 0), 108) AS TongThoiGianLD_Phien, ph.SendTongCuc
                FROM DiemDanhNguoiLx ph
                JOIN EMCDAT_IMAGE.dbo.DiemDanhNguoiLx img ON ph.DiemDanhNguoiLxID = img.DiemDanhNguoiLxID and ph.PhienDaoTao = img.PhienDaoTao
                WHERE ph.NguoiLxID = (SELECT NguoiLxID from NguoiLX WHERE MaDK = @MaDK) AND ph.ActivePhien = 1 AND ph.TongThoiGianLD_Phien >= 300
                ORDER BY ph.ThoiDiem DESC";
            return _db.QueryToDataTable(query, new { MaDK = maDK });
        }

        public DataTable GetBaoCaoHocVienByNguoilxID(Int64 NguoilxId)
        {
            return _db.QueryToDataTable("EXEC [dbo].[spTongHopDiemDanhLDByNguoiLx] @MaCSDT = @MaCSDT, @NguoiLxID = @NguoiLxID",
                new { MaCSDT, NguoiLxID = NguoilxId });
        }

        #endregion
    }
}
