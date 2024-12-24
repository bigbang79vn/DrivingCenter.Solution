using DrivingCenter.App.DTO.GPLX;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class MySQL_DAO
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static DataTable GetlistKhoaHoc()
        {
            string query = "SELECT IDKhoaHoc, TenKhoaHoc FROM esmartn.khoahoc where TrangThai = 2 OR TrangThai = 0;";

            //string query = "SELECT IDKhoaHoc, TenKhoaHoc FROM esmartn.khoahoc;";
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            return dt;
        }

        public static List<ThiSinh> LoadlstThiSinhByKHTN(string IDKhoaHoc)
        {
            List<ThiSinh> lstIDThiSinh = new List<ThiSinh>();
            string query = string.Format("CALL esmartn.VNDCResultStudent('{0}');", IDKhoaHoc);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            lstIDThiSinh = dt.AsEnumerable()
                           .Select(item => new ThiSinh
                           {
                               IDThiSinh = (int)(item["IDThiSinh"]),
                               MaDK = (string)(item["Ma_Dang_Ky"]),
                               SoBaoDanh = (int)(item["SoBaoDanh"]),
                               HoVaTen = (string)(item["HoTen"]),
                               NgaySinh = (DateTime)(item["NgaySinh"]),
                               SoCMT = (string)(item["SoCMT"]),
                               HangGPLX = (string)(item["LoaiBang"]),
                               DiemLT = (int)(item["TraLoiDung"]),
                               Result_LT = (string)(item["KetQua"]),
                               DiemMP = (int)(item["DiemMoPhong"]) == -1 ? 0 : (int)(item["DiemMoPhong"]),
                               Result_MP = (item["KetQuaMoPhong"]) == DBNull.Value ? "" : (string)(item["KetQuaMoPhong"])
                           })
                           .ToList();
            return lstIDThiSinh;
        }

        public static List<ThiSinh> LoadlstThiSinhSH(string IDKhoaHoc)
        {
            List<ThiSinh> lstThiSinhSH = new List<ThiSinh>();
            string query = string.Format("CALL esmartn.VNDCLstThiSinh('{0}');", IDKhoaHoc);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            lstThiSinhSH = dt.AsEnumerable()
                           .Select(item => new ThiSinh
                           {
                               IDThiSinh = (int)(item["IDThiSinh"]),
                               MaDK = (string)(item["Ma_Dang_Ky"]),
                               HoVaTen = (string)(item["HoTen"]),
                               NgaySinh = (DateTime)(item["NgaySinh"]),
                               SoCMT = (string)(item["SoCMT"])
                           })
                           .ToList();
            return lstThiSinhSH;
        }

        public static string GetHangGplxByMaDK(string MaDK)
        {
            string query = string.Format("SELECT LoaiBang FROM esmartn.khoahoc_thisinh \r\n" +
                "WHERE IDThiSinh = (SELECT MIN(IDThiSinh) AS IDThiSinh FROM esmartn.thisinh WHERE Ma_Dang_Ky = '{0}');", MaDK);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            string HangGplxByMaDK = dt.Rows[0][0].ToString();

            return HangGplxByMaDK;
        }

        public static void InsertThiSinh(string IDKhoaHoc, string MaDK)
        {
            string query = string.Format("CALL esmartn.VNDCInsertThiSinh('{0}', '{1}');", IDKhoaHoc, MaDK);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
        }

        public static void InsertKhoaHocThiSinh(string IDKhoaHoc, int IDThiSinh, string HangGPLX)
        {
            string query = string.Format("CALL esmartn.VNDCInsertKhoaHocThiSinh('{0}', {1}, '{2}');", IDKhoaHoc, IDThiSinh, HangGPLX);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
        }

        public static List<int> GetlstIDThiSinhDuplicate(string IDKhoaHoc)
        {
            string query = string.Format("SELECT IDThiSinh FROM esmartn.thisinh " +
                "WHERE IDKhoaHoc = '{0}' AND IDThiSinh NOT IN " +
                "(SELECT MAX(IDThiSinh) AS MaxID " +
                "FROM esmartn.thisinh " +
                "WHERE IDKhoaHoc = '{0}' " +
                "GROUP BY IDKhoaHoc, Ma_Dang_Ky);", IDKhoaHoc);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);

            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            List<int> lst = dt.AsEnumerable()
                           .Select(r => r.Field<int>("IDThiSinh"))
                           .ToList();
            return lst;
        }

        public static void DeleteThiSinh(int IDThiSinh, string IDKhoaHoc)
        {
            string query = string.Format("DELETE FROM esmartn.thisinh WHERE IDThiSinh = {0} AND IDKhoaHoc = '{1}';  ('{0}';", IDThiSinh, IDKhoaHoc);
            cmd = Helper.ConnectMySQL.ExcuteNonQuery(query);
        }
    }
}
