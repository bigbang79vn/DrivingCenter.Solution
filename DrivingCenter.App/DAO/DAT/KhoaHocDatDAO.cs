using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Dtos.DAT;
using DrivingCenter.App.Entities.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    /// <summary>Khóa học DAT và danh sách "khóa học đang đào tạo" (KhoaHocDangDT).</summary>
    public class KhoaHocDatDAO : DatDaoBase
    {
        public List<KhoaHocDat> GetAllKhoaHoc()
        {
            return _db.Query<KhoaHocDat>("SELECT * FROM KhoaHoc").OrderBy(x => x.TenKH).ToList();
        }

        public DataTable GetCategory()
        {
            return _db.QueryToDataTable("EXEC [dbo].[spGetTreeKhoaHoc] @MaCSDT = @MaCSDT", new { MaCSDT });
        }

        public List<KhoaHocDangDT> GetAllKhoaHocDDT()
        {
            DataTable dt = _db.QueryToDataTable("SELECT KhoaHocID, MaKH, TenKH, HangDaoTao, SoHVDuDK, TongHocVien FROM KhoaHocDangDT");

            return dt.AsEnumerable()
                .Select(item => new KhoaHocDangDT
                {
                    KhoaHocID = (int)(item["KhoaHocID"]),
                    MaKH = (string)(item)["MaKH"],
                    TenKH = (string)(item)["TenKH"],
                    HangDaoTao = (string)(item)["HangDaoTao"],
                    SoHVDuDK = item["SoHVDuDK"] == DBNull.Value ? 0 : (int)item["SoHVDuDK"],
                    TongHocVien = item["TongHocVien"] == DBNull.Value ? 0 : (int)(item["TongHocVien"])
                }).OrderBy(x => x.TenKH).ToList();
        }

        public HashSet<int> GetKhoaHocDangDTIds()
        {
            return new HashSet<int>(_db.Query<int>("SELECT KhoaHocID FROM KhoaHocDangDT"));
        }

        /// <summary>Cập nhật cột TongHocVien của các khóa đang đào tạo theo số học viên thực tế.</summary>
        public void UpdateKhoaHocDangDT()
        {
            const string query = "SELECT kh.KhoaHocID, COUNT(hv.HoVaTen) AS TongHocVien " +
                "FROM NguoiLX AS hv, KhoaHocDangDT AS kh " +
                "WHERE kh.KhoaHocID = hv.KhoaHocID " +
                "GROUP BY kh.KhoaHocID";

            var rows = _db.QueryToDataTable(query).AsEnumerable()
                .Select(item => new
                {
                    Id = Convert.ToInt32(item["KhoaHocID"]),
                    Tong = item["TongHocVien"] == DBNull.Value ? 0 : Convert.ToInt32(item["TongHocVien"])
                })
                .ToList();

            if (rows.Count > 0)
                _db.Execute("UPDATE [KhoaHocDangDT] SET [TongHocVien] = @Tong WHERE [KhoaHocID] = @Id", rows);
        }

        public void SaveKhoaHocDangDT(ICollection<int> khoaHocIds)
        {
            string list = string.Join(",", khoaHocIds);
            if (list.Length > 2000)   // @ListKhoaHocID VARCHAR(2000)
                throw new InvalidOperationException("Chọn quá nhiều khóa học (danh sách vượt 2000 ký tự). Vui lòng bỏ bớt các khóa đã kết thúc.");

            var soHvDuDk = _db.Query<(int KhoaHocID, int? SoHVDuDK)>("SELECT KhoaHocID, SoHVDuDK FROM KhoaHocDangDT")
                .Where(x => x.SoHVDuDK.HasValue && khoaHocIds.Contains(x.KhoaHocID))
                .Select(x => new { Id = x.KhoaHocID, SoHV = x.SoHVDuDK.Value })
                .ToList();

            _db.ExecuteTransaction((conn, tran) =>
            {
                Dapper.SqlMapper.Execute(conn, "spUpdateKhoaHocDangDT", new { MaCSDT, ListKhoaHocID = list },
                    tran, commandType: CommandType.StoredProcedure);
                if (soHvDuDk.Count > 0)
                    Dapper.SqlMapper.Execute(conn, "UPDATE KhoaHocDangDT SET SoHVDuDK = @SoHV WHERE KhoaHocID = @Id", soHvDuDk, tran);
            });

            UpdateKhoaHocDangDT();
        }

        public void UpdateHvDuDK(int sohvdudk, int khoahocID)
        {
            _db.Execute("UPDATE KhoaHocDangDT SET SoHVDuDK = @sohvdudk WHERE KhoaHocID = @khoahocID", new { sohvdudk, khoahocID });
        }

        public DateTime GetMinNgayKG() => _db.QuerySingle<DateTime>("SELECT MIN(NgayKG) FROM KhoaHocDangDT");

        public DateTime GetMaxNgayBG() => _db.QuerySingle<DateTime>("SELECT MAX(NgayBG) FROM KhoaHocDangDT");
    }
}
