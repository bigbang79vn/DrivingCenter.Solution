using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    /// <summary>
    /// Truy vấn CSDL sát hạch esmartn (MySQL). Mọi giá trị đều truyền qua tham số
    /// (trước đây ghép chuỗi và mỗi câu SELECT/CALL bị chạy hai lần: ExecuteNonQuery rồi Fill).
    /// </summary>
    public class MySQL_DAO
    {
        public static DataTable GetlistKhoaHoc()
        {
            const string query = "SELECT IDKhoaHoc, TenKhoaHoc FROM esmartn.khoahoc WHERE TrangThai = 2 OR TrangThai = 0;";
            return ConnectMySQL.Query(query);
        }

        public static List<ThiSinh> LoadlstThiSinhByKHTN(string IDKhoaHoc)
        {
            DataTable dt = ConnectMySQL.Query("CALL esmartn.VNDCResultStudent(@IDKhoaHoc);", ("@IDKhoaHoc", IDKhoaHoc));

            return dt.AsEnumerable()
                     .Select(item => new ThiSinh
                     {
                         IDThiSinh = (int)(item["IDThiSinh"]),
                         MaDK = (string)(item["Ma_Dang_Ky"]),
                         SoBaoDanh = (int)(item["SoBaoDanh"]),
                         HoVaTen = (string)(item["HoTen"]),
                         NgaySinh = (DateTime)(item["NgaySinh"]),
                         SoCMT = (string)(item["SoCMT"]),
                         TenKhoaHoc = (string)(item["KhoaThi"]),
                         NgayThi = (DateTime)(item["NgayThi"]),
                         HangGPLX = (string)(item["LoaiBang"]),
                         DiemLT = (int)(item["TraLoiDung"]),
                         Result_LT = (string)(item["KetQua"]),
                         DiemMP = (item["DiemMoPhong"]) == DBNull.Value || (int)(item["DiemMoPhong"]) == -1 ? 0 : (int)(item["DiemMoPhong"]),
                         Result_MP = (item["KetQuaMoPhong"]) == DBNull.Value ? "" : (string)(item["KetQuaMoPhong"])
                     })
                     .ToList();
        }

        public static List<ThiSinh> LoadlstThiSinhSH(string IDKhoaHoc)
        {
            DataTable dt = ConnectMySQL.Query("CALL esmartn.VNDCLstThiSinh(@IDKhoaHoc);", ("@IDKhoaHoc", IDKhoaHoc));

            return dt.AsEnumerable()
                     .Select(item => new ThiSinh
                     {
                         IDThiSinh = (int)(item["IDThiSinh"]),
                         MaDK = (string)(item["Ma_Dang_Ky"]),
                         HoVaTen = (string)(item["HoTen"]),
                         NgaySinh = (DateTime)(item["NgaySinh"]),
                         SoCMT = (string)(item["SoCMT"])
                     })
                     .ToList();
        }

        public static string GetHangGplxByMaDK(string MaDK)
        {
            const string query = "SELECT LoaiBang FROM esmartn.khoahoc_thisinh " +
                "WHERE IDThiSinh = (SELECT MIN(IDThiSinh) FROM esmartn.thisinh WHERE Ma_Dang_Ky = @MaDK);";
            DataTable dt = ConnectMySQL.Query(query, ("@MaDK", MaDK));

            return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
        }

        public static void InsertThiSinh(string IDKhoaHoc, string MaDK)
        {
            ConnectMySQL.Execute("CALL esmartn.VNDCInsertThiSinh(@IDKhoaHoc, @MaDK);",
                ("@IDKhoaHoc", IDKhoaHoc), ("@MaDK", MaDK));
        }

        public static void InsertKhoaHocThiSinh(string IDKhoaHoc, int IDThiSinh, string HangGPLX)
        {
            ConnectMySQL.Execute("CALL esmartn.VNDCInsertKhoaHocThiSinh(@IDKhoaHoc, @IDThiSinh, @HangGPLX);",
                ("@IDKhoaHoc", IDKhoaHoc), ("@IDThiSinh", IDThiSinh), ("@HangGPLX", HangGPLX));
        }

        public static List<int> GetlstIDThiSinhDuplicate(string IDKhoaHoc)
        {
            const string query = "SELECT IDThiSinh FROM esmartn.thisinh " +
                "WHERE IDKhoaHoc = @IDKhoaHoc AND IDThiSinh NOT IN " +
                "(SELECT MAX(IDThiSinh) FROM esmartn.thisinh WHERE IDKhoaHoc = @IDKhoaHoc GROUP BY IDKhoaHoc, Ma_Dang_Ky);";
            DataTable dt = ConnectMySQL.Query(query, ("@IDKhoaHoc", IDKhoaHoc));

            return dt.AsEnumerable().Select(r => Convert.ToInt32(r["IDThiSinh"])).ToList();
        }

        public static void DeleteThiSinh(int IDThiSinh, string IDKhoaHoc)
        {
            // Bản cũ có đoạn thừa "  ('{0}';" sau câu DELETE (lỗi cú pháp ở câu thứ hai, bị nuốt lỗi)
            ConnectMySQL.Execute("DELETE FROM esmartn.thisinh WHERE IDThiSinh = @IDThiSinh AND IDKhoaHoc = @IDKhoaHoc;",
                ("@IDThiSinh", IDThiSinh), ("@IDKhoaHoc", IDKhoaHoc));
        }
    }
}
