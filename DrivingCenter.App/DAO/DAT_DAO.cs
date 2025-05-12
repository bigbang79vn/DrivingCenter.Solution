
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.DTO.LyThuyet;
using GMap.NET;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.DAO
{
    public class DAT_DAO
    {
        private static DAT_DAO instance;

        public static DAT_DAO Instance
        {
            get { if (instance == null) instance = new DAT_DAO(); return DAT_DAO.instance; }
            private set => DAT_DAO.instance = value;
        }

        public DAT_DAO()
        {
        }

        #region Trang Home

        public List<int> LstYear()
        {
            return DataProviderDAT.Instance.ExecuteQuery("SELECT DISTINCT DATEPART(year, CreateDate) as Nam FROM NguoiLX ORDER BY Nam DESC")
                .AsEnumerable()
                .Select(row => row.Field<int>("Nam"))
                .ToList();
        }
        public int LoadSoHocVienByYear(int year)
        {
            int SoHvByYear = (int)DataProviderDAT.Instance.ExecuteScalar($"SELECT COUNT(*) from NguoiLX WHERE KhoaHocID IN (SELECT KhoaHocID FROM KhoaHoc WHERE YEAR(NgayKG) = {year})");
            return SoHvByYear;
        }

        public int LoadSoKhoaHocByYear(int year)
        {
            int SoKhByYear = (int)DataProviderDAT.Instance.ExecuteScalar($"SELECT COUNT(*) FROM KhoaHoc where DATEPART(year, NgayKG) = {year}");
            return SoKhByYear;
        }

        public int LoadSoHocVien()
        {
            int SoHv = (int)DataProviderDAT.Instance.ExecuteScalar("SELECT COUNT(*) FROM NguoiLX");
            return SoHv;
        }

        public int LoadSoKhoaHoc()
        {
            int SoKh = (int)DataProviderDAT.Instance.ExecuteScalar("SELECT COUNT(*) FROM KhoaHoc ");
            return SoKh;
        }

        public int LoadSoGiaoVien()
        {
            int SoGv = (int)DataProviderDAT.Instance.ExecuteScalar("SELECT COUNT(*) FROM GiaoVien ");
            return SoGv;
        }

        public int LoadSoXeTapLai()
        {
            int SoXetl = (int)DataProviderDAT.Instance.ExecuteScalar("SELECT COUNT(*) FROM Vehicles");
            return SoXetl;
        }

        public void UpdateKhoaHocDangDT()
        {
            string query = string.Format("SELECT kh.KhoaHocID, kh.TenKH, kh.HangDaoTao, kh.SoHVDuDK ,COUNT(hv.HoVaTen) as TongHocVien " +
                "FROM NguoiLX as hv, KhoaHocDangDT as kh " +
                "WHERE kh.KhoaHocID = hv.KhoaHocID " +
                "GROUP by kh.KhoaHocID, kh.TenKH, kh.HangDaoTao, kh.SoHVDuDK " +
                "ORDER by HangDaoTao");

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<KhoaHocDangDT> lst = new List<KhoaHocDangDT>();

            foreach (DataRow item in dt.Rows)
            {
                KhoaHocDangDT pr = new KhoaHocDangDT(item);
                lst.Add(pr);
                string qrUpdate = string.Format("UPDATE [KhoaHocDangDT] set [TongHocVien] = {0} where [KhoaHocID] = {1}", pr.TongHocVien, pr.KhoaHocID);
                DataProviderDAT.Instance.ExecuteNonQuery(qrUpdate);
            }
        }

        public void UpdateHvDuDK(int sohvdudk, int khoahocID)
        {
            string query = string.Format("UPDATE KhoaHocDangDT SET SoHVDuDK = '{0}' where KhoaHocID = {1}", sohvdudk, khoahocID);
            DataProviderDAT.Instance.ExecuteNonQuery(query);
        }

        public int CountPhien(int day, int month, int year)
        {
            string query = string.Format("SELECT COUNT(MaCSDT) as SoPhien " +
                "FROM DiemDanhNguoiLx " +
                "WHERE ActivePhien = 1 and TongQuangDuongLD_Phien >= 5 AND convert(varchar, ThoiDiem, 103) = '{0:00}/{1:00}/{2:0000}' " +
                "GROUP BY MaCSDT", day, month, year);

            var getValue = DataProviderDAT.Instance.ExecuteScalar(query);
            return getValue == null ? 0 : (int)getValue;
        }

        //public float SumKmByDay(int day, int month, int year)
        //{
        //    string query = string.Format("SELECT ROUND(SUM(TongQuangDuongLD_Phien),0) AS SoKm " +
        //        "FROM DiemDanhNguoiLx " +
        //        "WHERE ActivePhien = 1 AND ThoiDiem IS NOT NULL AND TongQuangDuongLD_Phien > 5 and convert(varchar, ThoiDiem, 103) = '{0:00}/{1:00}/{2:0000}'", day, month, year);

        //    var getValue = DataProviderDAT.Instance.ExecuteScalar(query);
        //    return getValue == null ? 0 : (float)getValue;
        //}

        #endregion Trang Home

        #region Sync GV for Cabin

        public List<GvCabin> GetListGvByNameKH(string TenKH)
        {
            string query = string.Format("select MaDK, GiaoVienID from NguoiLX where TenKH like '%{0}%' ORDER BY GiaoVienID", TenKH);
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<GvCabin> lst = new List<GvCabin>();

            lst = dt.AsEnumerable().Select(item => new GvCabin
            {
                MaDK = (string)(item)["MaDK"],
                GiaoVienID = (int)(item)["GiaoVienID"]
            }).ToList();

            return lst;
        }

        public List<GiaoVien> GetAllGV()
        {
            List<GiaoVien> lstGiaoVien = new List<GiaoVien>();
            string query = $@"SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX, AnhChup, MaCNDT, hv.Ten , SuDung, DateUse 
                        FROM GiaoVien AS gv
                        INNER JOIN TrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma";
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

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
                DateUse = item["DateUse"] == DBNull.Value ? DateTime.Now : (DateTime)item["DateUse"]
            }).ToList();

            return lstGiaoVien;
        }

        public void UpdateGiaoVien(GiaoVien gv, int GiaoVienId)
        {
            string query = $@"
                 UPDATE [dbo].[GiaoVien]
                    SET 
                        [MaGV] = @MaGV, [HoTenDem] = @HoTenDem, [TenGV] = @TenGV, [NgaySinh] = @NgaySinh, [SoCMT] = @SoCMT, [HangGPLX] = @HangGPLX, 
                        [SoDienThoai] = @SoDienThoai, [DiaChi] = @DiaChi, [MaCNDT] = @MaCNDT, [MaTrinhDoHocVan] = @MaTrinhDoHocVan, [SuDung] = @SuDung
                    WHERE GiaoVienID = @GiaoVienID";
            DataProviderDAT.Instance.ExecuteNonQuery(query, new
            {
                gv.MaGV,
                gv.HoTenDem,
                gv.TenGV,
                gv.NgaySinh,
                gv.SoCMT,
                SoDienThoai = "",
                DiaChi = "",
                gv.HangGPLX,
                gv.MaCNDT,
                MaTrinhDoHocVan = gv.TrinhDoHocVan,
                gv.SuDung,
                GiaoVienID = GiaoVienId
            });
        }
        public DataTable GetlstHocVan()
        {
            string query = $"SELECT * FROM TrinhDoHocVan";
            return DataProviderDAT.Instance.ExecuteQuery(query);
        }
        public DataTable GetlstHangGPLX()
        {
            string query = $"SELECT [ID], [MaHang], [TenHang] FROM [I2EDAT2023].[dbo].[HangGPLX]";
            return DataProviderDAT.Instance.ExecuteQuery(query);
        }

        #endregion Sync GV for Cabin

        #region Tổng hợp xe tập lái

        public List<XeTapLai> GetLstXeTapLai()
        {
            string query = $"SELECT * FROM XeTapLai ORDER BY BienSoXe ASC";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

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
                SuDungDen = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"],
            }).ToList();

            return lstXeTapLai;
        }

        public List<XeTapLai> GetLstXeTapLaiOnline()
        {
            string query = string.Format("select VehicleID, BienSoXe, MaXe, Version, HangGPLX, Imei, VanToc, UpdateTime from Vehicles where online = 1 order by BienSoXe ASC");

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<XeTapLai> lstXeOnline = dt.AsEnumerable().Select(item => new XeTapLai
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

            return lstXeOnline;
        }

        public List<XeTapLai> GetAllVehicles()
        {
            string query = string.Format("SELECT VehicleID, Imei, MaXe, BienSoXe, HangGPLX, Version, TrangThai, CreateDate, UpdateTime  FROM Vehicles ORDER BY BienSoXe ASC");

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<XeTapLai> lst = dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["VehicleID"],
                MaXe = item["MaXe"] == DBNull.Value ? "Null" : (string)item["MaXe"],
                Imei = item["Imei"] == DBNull.Value ? "Null" : (string)item["Imei"],
                BienSoXe = (string)item["BienSoXe"],
                HangGPLX = (string)item["HangGPLX"],
                Version = item["Version"] == DBNull.Value ? "Null" : (string)item["Version"],
                TrangThai = (bool)item["TrangThai"],
                CreateDate = (DateTime)item["CreateDate"],
                ThoiDiem = (DateTime)item["UpdateTime"]
            }).ToList();

            return lst;
        }

        //public List<PhienHoc> LstPhienHocTheoXe(string biensoxe, string from, string to)
        //{
        //    string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen,kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong , ActivePhien, Note " +
        //        "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
        //        "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ph.BienSo = '{0}' and CONVERT(VARCHAR, ph.ThoiDiem, 112) >= '{1}' AND CONVERT(VARCHAR, ph.ThoiDiem, 112) <= '{2}' " +
        //        "ORDER BY ThoiDiem", biensoxe, from, to);

        //    DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

        //    List<PhienHoc> lst = new List<PhienHoc>();
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        PhienHoc hv = new PhienHoc(item);
        //        lst.Add(hv);
        //    }
        //    return lst;
        //}

        public int UpdateXeTapLai(int vehicleId, string imei, XeTapLai xeTap)
        {
            string ngayCapGPXTL = xeTap.NgayCapGPXTL.ToString("yyyy-MM-dd 00:00:00");
            string ngayHHGPXTL = xeTap.NgayHHXTL.ToString("yyyy-MM-dd 00:00:00");
            string query = string.Format("EXEC [dbo].[spUpdateXeTapLai] N'66015' , {0} , N'{1}' , N'{2}' ,N'{3}', N'{4}', 1 , {5} , N'{6}' , '{7}' , '{8}'",
                vehicleId, xeTap.MaXe, xeTap.BienSoXe, xeTap.HangGPLX, imei, xeTap.NamSX, xeTap.SoGPXTL, ngayCapGPXTL, ngayHHGPXTL);
            int result = DataProviderDAT.Instance.ExecuteNonQuery(query);
            return result;
        }

        public int DeleteXeTapLai(int vehicleId, string imei)
        {
            string query = string.Format("DELETE FROM [dbo].[Vehicles] WHERE VehicleID = {0} AND Imei = '{1}' ", vehicleId, imei);
            int result = DataProviderDAT.Instance.ExecuteNonQuery(query);
            return result;
        }

        #endregion Tổng hợp xe tập lái

        #region DAT

        public void LockSession(string diemdanhnguoilxId, string note)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx set Note = N'{0}', ActivePhien = 0 WHERE DiemDanhNguoiLxID = '{1}'", note, diemdanhnguoilxId);
            DataProviderDAT.Instance.ExecuteNonQuery(query);
        }

        public void UnlockSession(string diemdanhnguoilxId)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx set Note = NULL, ActivePhien = 1 WHERE DiemDanhNguoiLxID = '{0}'", diemdanhnguoilxId);
            DataProviderDAT.Instance.ExecuteNonQuery(query);
        }

        public void ReLoad(string date)
        {
            string query1 = string.Format("Exec spGetDiemDanhNguoiLxByDay '{0}',0,0", date);
            string query2 = string.Format("Exec spGetDiemDanhNguoiLxByDay '{0}',1,0", date);

            DataProviderDAT.Instance.ExecuteNonQuery(query1);
            DataProviderDAT.Instance.ExecuteNonQuery(query2);
        }

        public List<PhienHoc> GetAllPhienHocToDay(string day)
        {
            string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen, kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong, ph.ActivePhien, ph.Note " +
                "FROM DiemDanhNguoiLx as ph, GiaoVien as gv, NguoiLX as hv,  KhoaHoc as kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and hv.NguoiLXID = ph.NguoiLxID AND hv.KhoaHocID = kh.KhoaHocID and CONVERT(VARCHAR, ThoiDiem, 103) = '{0}' " +
                "ORDER by ph.Logout, ph.SendTongCuc ASC", day);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

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
                Note = item.Field<string>("Note") ?? ""
            }).ToList();
        }

        public List<PhienHoc> GetLstPhienByDateTrue(string day)
        {
            List<PhienHoc> lstPhienHoc = GetAllPhienHocToDay(day).Where(x => x.QuangDuongPhien >= 1).ToList();
            return lstPhienHoc;
        }

        public List<KhoaHocDangDT> GetAllKhoaHocDDT()
        {
            string query = "SELECT KhoaHocID, MaKH, TenKH, HangDaoTao, SoHVDuDK, TongHocVien FROM KhoaHocDangDT";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<KhoaHocDangDT> lst = new List<KhoaHocDangDT>();

            lst = dt.AsEnumerable().Select(item => new KhoaHocDangDT
            {
                KhoaHocID = (int)(item["KhoaHocID"]),
                MaKH = (string)(item)["MaKH"],
                TenKH = (string)(item)["TenKH"],
                HangDaoTao = (string)(item)["HangDaoTao"],
                SoHVDuDK = item["SoHVDuDK"] == DBNull.Value ? 0 : (int)item["SoHVDuDK"],
                TongHocVien = item["TongHocVien"] == DBNull.Value ? 0 : (int)(item["TongHocVien"])
            }).OrderBy(x => x.TenKH).ToList();

            return lst;
        }

        public List<KhoaHoc> GetAllKhoaHoc()
        {
            string query = "SELECT KhoaHocID, MaKH, TenKH, NgayKG, NgayBG, MaBC1, HangDaoTao FROM KhoaHoc";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<KhoaHoc> lst = new List<KhoaHoc>();

            lst = dt.AsEnumerable().Select(item => new KhoaHoc
            {
                KhoaHocID = (int)(item["KhoaHocID"]),
                MaKhoaHoc = (string)(item)["MaKH"],
                TenKhoaHoc = (string)(item)["TenKH"],
                HangDaoTao = (string)(item)["HangDaoTao"],
                NgayKhaiGiang = (DateTime)(item)["NgayKG"],
                NgayBeGiang = (DateTime)(item)["NgayBG"],
                MaBC1 = (string)item["MABC1"]
            }).OrderBy(x => x.TenKhoaHoc).ToList();

            return lst;
        }

        public List<TongHopHVByKhoa> TongHopHvByKhoa(int khoahocId)
        {
            string query = $@"SELECT NguoiLXID, KhoaHocID, MaDK, HoTenGV, HoVaTen, HangDaoTao,  COUNT(NgayHoc) AS SoNgayHoc , ROUND(SUM(TongQuangDuongLD_Phien),3) as TongQuangDuongLD_Phien , ROUND(SUM(TongThoiGianLD_Phien),1) as TongThoiGianLD_Phien , ROUND(SUM(QuangDuongPhienGuiTC),3) as QuangDuongPhienGuiTC, ROUND(SUM(ThoiGianPhienGuiTC),3) as ThoiGianPhienGuiTC 
                FROM KQHvGroup
                WHERE KhoaHocID = '{khoahocId}'
                GROUP BY 
                    NguoiLXID,
                    KhoaHocID,
                    MaDK,
                    HoTenGV,
                    HoVaTen,
                    HangDaoTao
                ORDER BY 
                    HoTenGV";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TongHopHVByKhoa> lstResult = new List<TongHopHVByKhoa>();

            lstResult = dt.AsEnumerable().Select(item => new TongHopHVByKhoa
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

            return lstResult;
        }

        public List<TongHopHVByKhoa> TongHopPhB11(int khoahocId)
        {
            string query = string.Format("SELECT hv.MaDK, ph.HangXe , ROUND(SUM(ph.TongQuangDuongLD_Phien),1) AS TongQuangDuong_B11 , SUM(ph.TongThoiGianLD_Phien) AS TongThoiGian_B11 , ROUND(SUM(TongQuangDuongLD_BanDem),1) AS TongQuangDuongB11_PhienDem, " +
                "ROUND( SUM(ph.TongThoiGianLD_BanDem), 1) AS TongThoiGianB11_PhienDem  " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHocDangDT AS kh " +
                "where ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ActivePhien = 1 and HangXe = 'B11' and kh.KhoaHocID = '{0}' " +
                "GROUP BY hv.MaDK, hv.HangDaoTao, kh.KhoaHocID, ph.HangXe", khoahocId);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TongHopHVByKhoa> lstResultB11 = new List<TongHopHVByKhoa>();

            lstResultB11 = dt.AsEnumerable().Select(item => new TongHopHVByKhoa
            {
                MaDK = (string)(item["MaDK"]),
                HangXe = (string)(item)["HangXe"],
                TongQuangDuong_B1 = (double)(item)["TongQuangDuong_B11"],
                TongThoiGian_B1 = (double)(item)["TongThoiGian_B11"],
                TongQuangDuong_PhienDemB1 = item["TongQuangDuongB11_PhienDem"] == DBNull.Value ? 0 : (double)(item)["TongQuangDuongB11_PhienDem"],
                TongThoiGian_PhienDemB1 = item["TongThoiGianB11_PhienDem"] == DBNull.Value ? 0 : (double)(item)["TongThoiGianB11_PhienDem"]
            }).ToList();

            return lstResultB11;
        }

        public List<TongHopHVByKhoa> TongHopPhBD(int khoahocId, string hangdaotao)
        {
            string query;
            if (hangdaotao == "B.01" || hangdaotao == "B11")
            {
                query = $@"
                    SELECT MaDK, TongQuangDuongLD_Phien, TongThoiGianLD_Phien 
                    FROM vGioBanDemGroup 
                    WHERE KhoaHocID = '{khoahocId}'";
            }
            else
            {
                query = $@"
                    SELECT MaDK, TongQuangDuongLD_Phien, TongThoiGianLD_Phien 
                    FROM vGioBanDemGroup 
                    WHERE KhoaHocID = '{khoahocId}' AND HangXe <> 'B11'";
            }

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TongHopHVByKhoa> lstBD = new List<TongHopHVByKhoa>();

            lstBD = dt.AsEnumerable().Select(item => new TongHopHVByKhoa
            {
                MaDK = (string)(item["MaDK"]),
                TongQuangDuong_PhienDem = item["TongQuangDuongLD_Phien"] == DBNull.Value ? 0 : (double)(item)["TongQuangDuongLD_Phien"],
                TongThoiGian_PhienDem = item["TongThoiGianLD_Phien"] == DBNull.Value ? 0 : (double)(item)["TongThoiGianLD_Phien"]
            }).ToList();

            return lstBD;
        }


        #region Menu Context Tổng hợp

        //---------Tổng hợp giờ dạy GV by khóa học-------
        public List<TongHopGV> TongHopGVsByKh(string khoahoc)
        {
            string query = string.Format("SELECT MaGV , HoTenGV, ThoiDiem, COUNT(HoVaTen) AS SoHocVien, SUM(SoPhien) AS SoPhien, TenKH, BienSoXe, ROUND(SUM(TongQuangDuongLD_Phiens),1) as SQuangDuong, SUM(TongThoiGianLD_Phiens) as TongThoiGian, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianLD_Phiens),0),108) as SThoiGian " +
                "FROM TongHopGvFn " +
                "WHERE TenKH = '{0}' " +
                "GROUP BY MaGV, HoTenGV, ThoiDiem, BienSoXe, TenKH " +
                "ORDER BY HoTenGV", khoahoc);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TongHopGV> lstGV = dt.AsEnumerable().Select(item => new TongHopGV
            {
                MaGv = item["MaGV"].ToString(),
                SHoTenGV = item["HoTenGV"].ToString(),
                NgayTap = item["ThoiDiem"].ToString(),
                SoPhien = (int)item["SoPhien"],
                TenKhoaHoc = item["TenKH"].ToString(),
                BienSoXeGv = item["BienSoXe"].ToString(),
                SoHocVienByDay = (int)item["SoHocVien"],
                SQuangDuong = (double)item["SQuangDuong"],
                TongThoiGian = (double)item["TongThoiGian"],
                SThoiGian = item["SThoiGian"].ToString()
            }).ToList();

            return lstGV;
        }

        public List<TienDo> TongHopTienDoDatByKH(int khoahocid, string makh)
        {
            string query = $"SELECT * FROM TongHop_TienDo WHERE KhoaHocID = {khoahocid} and MaKH = '{makh}' ORDER BY MaGV ASC, Ngay ASC ";
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TienDo> lst = dt.AsEnumerable().Select(item => new TienDo {
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

            return lst;
        }

        public DataTable BaoCaoKetQuaHocVien(int KhoaHocID, int KieuKetQua)
        {
            string query = $"EXEC [dbo].[spQuaTrinhDaoTaoKhoaHoc] @MaCSDT = N'66015', @KhoaHocID = {KhoaHocID}, @TieuDe = N'BÁO CÁO KẾT QUẢ THỰC HÀNH LÁI XE CỦA KHÓA HỌC', @Result = {KieuKetQua}";

            return DataProviderDAT.Instance.ExecuteQuery(query);
        }

        #endregion Menu Context Tổng hợp

        public int CheckImgNullNew(string diemdanhnguoilxId)
        {
            string query = $"SELECT COUNT(AnhChup_Logout) FROM DiemDanhNguoiLx WHERE DiemDanhNguoiLxId = '{diemdanhnguoilxId}'";

            int count = (int)DataProviderIMG.Instance.ExecuteScalar(query);

            return count;
        }

        public void KetThucPhien(Int64 nguoiLxID, string diemdanhnguoilxId)
        {
            DataProviderDAT.Instance.ExecuteNonQuery(string.Format(string.Format("EXEC [dbo].[spKetThucPhienDaoTao] @NguoiLxID = {0}, @DiemDanhNguoiLxID = N'{1}' ", nguoiLxID, diemdanhnguoilxId)));
        }

        public List<TongHopHVByDay> TongHopGioHVByDay(string day)
        {
            string query = string.Format("SELECT ph.MaDK, CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, hv.HoVaTen, kh.TenKH, pH.BienSo as BienSoXe, ROUND(SUM(ph.TongQuangDuongLD_Phien), 1) AS TongQuangDuongLD_Phien, CONVERT(varchar, DATEADD(second, SUM(ph.TongThoiGianLD_Phien), 0), 108) AS TongThoiGianLD_Phien " +
                "FROM DiemDanhNguoiLx as ph, GiaoVien as gv, NguoiLX as hv, KhoaHocDangDT as kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and CONVERT(VARCHAR, ThoiDiem, 103) = '{0}' " +
                "GROUP BY CONCAT(gv.HoTenDem, ' ', gv.TenGV), ph.MaDK, hv.HoVaTen, kh.TenKH, PH.BienSo " +
                "ORDER BY HoTenGV", day);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TongHopHVByDay> LstSum = new List<TongHopHVByDay>();
            foreach (DataRow item in dt.Rows)
            {
                TongHopHVByDay sumTime = new TongHopHVByDay(item);
                LstSum.Add(sumTime);
            }

            return LstSum;
        }

        public List<TongHopGV> TongHopGVsByDay(string day, int flag)
        {
            string query;
            if (flag == 0)
            {
                query = $"EXEC [dbo].[spTongHopGioDay] @NgayDay = N'{day}'";
            }
            else
            {
                query = $@"SELECT MaGV , HoTenGV, ThoiDiem, COUNT(HoVaTen) AS SoHocVien, SUM(SoPhien) AS SoPhien, TenKH, BienSoXe, ROUND(SUM(TongQuangDuongLD_Phiens),1) as SQuangDuong, SUM(TongThoiGianLD_Phiens) as TongThoiGianLD_Phiens, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianLD_Phiens),0),108) as SThoiGian
                FROM TongHopGvFn 
                WHERE ThoiDiem = '{day}'
                GROUP by MaGV, HoTenGV, ThoiDiem, BienSoXe, TenKH
                ORDER by HoTenGV";
            }

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<TongHopGV> lstGV = dt.AsEnumerable().Select(item => new TongHopGV{
                MaGv = item["MaGV"].ToString(),
                SHoTenGV = item["HoTenGV"].ToString(),
                NgayTap = item["ThoiDiem"].ToString(),
                SoPhien = (int)item["SoPhien"],
                TenKhoaHoc = item["TenKH"].ToString(),
                BienSoXeGv = item["BienSoXe"].ToString(),
                SoHocVienByDay = (int)item["SoHocVien"],
                SQuangDuong = (double)item["SQuangDuong"],
                TongThoiGian = (double)item["TongThoiGianLD_Phiens"],
                SThoiGian = item["SThoiGian"].ToString(),

            }).ToList();
            return lstGV;
        }

        public List<string> GetLstGvTrue(string day)
        {
            string query = $@"SELECT DISTINCT MaGV FROM DiemDanhNguoiLx WHERE 
                (ROUND(Lat,3,1) = '12.67' AND ROUND(Lng,3,1) = '108.096' AND CONVERT(VARCHAR, ThoiDiem, 103) = '{day}') OR 
                (ROUND(Lat_Logout,3,1) = '12.67' AND ROUND(Lng_Logout,3,1) = '108.096' AND CONVERT(VARCHAR, ThoiDiem, 103) = '{day}') AND TongQuangDuongLD_Phien > 5 AND ActivePhien = 1 Order by MaGV asc ";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<string> lst = dt.AsEnumerable()
                           .Select(r => r.Field<string>("MaGV"))
                           .ToList();
            return lst;
        }

        public DateTime GetMinNgayKG()
        {
            string query = "SELECT MIN(NgayKG) FROM KhoaHocDangDT";
            DateTime result = (DateTime)DataProviderDAT.Instance.ExecuteScalar(query);
            return result;
        }

        public DateTime GetMaxNgayBG()
        {
            string query = "SELECT MAX(NgayBG) FROM KhoaHocDangDT";
            DateTime result = (DateTime)DataProviderDAT.Instance.ExecuteScalar(query);
            return result;
        }

        public List<PhienHoc> GetLstPhienLoiByDate(string day)
        {
            List<PhienHoc> lstError = GetAllPhienHocToDay(day).Where(x => x.QuangDuongPhien <= 5).ToList();

            return lstError;
        }

        public void DeletePhByID(string diemdanhnguoilxID)
        {
            string query = string.Format("EXEC [dbo].[spDeletePhienHoc] @diemdanhnguoilxID = '{0}'", diemdanhnguoilxID);
            DataProviderDAT.Instance.ExecuteNonQuery(query);
            DataProviderIMG.Instance.ExecuteNonQuery(query);
        }

        public List<PhienHoc> TongHopPhienHocLoi(string from, string to)
        {
            string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, hv.HoVaTen, ph.NguoiLxID, ph.DiemDanhNguoiLxID, ph.TongThoiGianLD_Phien as TongThoiGian_Double, hv.HangDaoTao, ph.ThoiDiem, ph.ThoiDiem_Logout,  kh.TenKH, ph.LogOut, ph.SendTongCuc, ROUND(ph.TongQuangDuongLD_Phien,1) AS TongQuangDuongLD_Phien, CONVERT(varchar, DATEADD(second, ph.TongThoiGianLD_Phien, 0), 108) AS TongThoiGianLD_Phien, ph.BienSo, ph.Note " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
                "where ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ActivePhien = 0 and CONVERT(VARCHAR, ph.ThoiDiem, 112) >= '{0}' AND CONVERT(VARCHAR, ph.ThoiDiem, 112) <= '{1}' " +
                "ORDER BY HoTenGV", from, to);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<PhienHoc> lst = dt.AsEnumerable().Select(item => new PhienHoc
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
                Note = item["Note"] == DBNull.Value ? string.Empty : (string)item["Note"]
            }).ToList();
            return lst;
        }

        public List<PhienHoc> LstPhienHocTheoXe(string biensoxe, string from, string to)
        {
            string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen,kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien / 3600 as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong , ActivePhien, Note " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ph.BienSo = '{0}' and CONVERT(VARCHAR, ph.ThoiDiem, 112) >= '{1}' AND CONVERT(VARCHAR, ph.ThoiDiem, 112) <= '{2}' " +
                "ORDER BY ThoiDiem", biensoxe, from, to);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<PhienHoc> lst = new List<PhienHoc>();
            foreach (DataRow item in dt.Rows)
            {
                PhienHoc hv = new PhienHoc(item);
                lst.Add(hv);
            }
            return lst;
        }

        public List<Guid> GetlstPhienHocLoginIsNull(int khoahocid)
        {
            string query = string.Format("SELECT kq.DiemDanhNguoiLxID " +
                "FROM DiemDanhNguoiLx kq, NguoiLX hv, KhoaHoc kh " +
                "WHERE kq.NguoiLxID = hv.NguoiLXID and kh.KhoaHocID = hv.KhoaHocID and kq.ThoiDiem is null and kh.KhoaHocID = '{0}'", khoahocid);
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<Guid> lstDiemDanhNguoiLxID = dt.AsEnumerable()
                .Select(r => r.Field<Guid>("DiemDanhNguoiLxID"))
                .ToList();
            return lstDiemDanhNguoiLxID;
        }

        public void DeletePhienHocLoginIsNull(List<Guid> lstDiemDanhNguoiLxID)
        {
            foreach (Guid item in lstDiemDanhNguoiLxID)
            {
                string query = string.Format("EXEC [dbo].[spDeletePhienHoc] @diemdanhnguoilxID = '{0}'", item);
                DataProviderDAT.Instance.ExecuteNonQuery(query);
                DataProviderIMG.Instance.ExecuteNonQuery(query);
            }
        }

        public List<PointLatLng> GetlstPoint(string diemdanhnguoilxId)
        {
            List<PointLatLng> lstPoint = new List<PointLatLng>();
            string query = string.Format("SELECT Lat, Lng  FROM VehicleHistory WHERE DiemDanhNguoiLxId = '{0}' ORDER BY TongQuangDuongPhien asc", diemdanhnguoilxId);
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            lstPoint = dt.AsEnumerable().Select(item => new PointLatLng
            {
                Lat = (double)item["Lat"],
                Lng = (double)item["Lng"]
            }).ToList();

            return lstPoint;
        }

        public DataTable GetDetailSession(string diemdanhnguoilxId)
        {
            string query = string.Format("SELECT TenGV, dd.NguoiLxID, hv.HoVaTen, hv.TenKH, dd.DiemDanhNguoiLxID, dd.BienSo, dd.HangXe, dd.ThoiDiem, dd.ThoiDiem_Logout, TongQuangDuongLD_Phien, TongThoiGianLD_Phien, Lat, Lng, Lat_Logout, Lng_Logout  " +
                "FROM DiemDanhNguoiLx AS dd, NguoiLX AS hv " +
                "WHERE DiemDanhNguoiLxId = '{0}' and dd.NguoiLxID = hv.NguoiLXID", diemdanhnguoilxId);

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);
            return dt;
        }

        public List<ChiTietPhienHoc> GetHistoryData(string diemdanhnguoilxId)
        {
            string query = $@"SELECT dt.HistoryID, dt.ThoiDiem, dt.VanToc, TongQuangDuongLD_Phien, TongThoiGianLD_Phien, Lat, Lng, FaceStatus, img.AnhChup 
                FROM HistoryData as dt, I2EDAT_IMAGE.dbo.HistoryData as img 
                WHERE dt.DiemDanhNguoiLxId = '{diemdanhnguoilxId}' AND dt.HistoryID = img.HistoryID";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<ChiTietPhienHoc> lst = dt.AsEnumerable().Select(item => new ChiTietPhienHoc
            {
                Id = (Guid)item["HistoryID"],
                ThoiDiem = (DateTime)item["ThoiDiem"],
                VanToc = Math.Round((double)item["VanToc"], 3),
                QuangDuong = Math.Round((double)item["TongQuangDuongLD_Phien"], 3),
                ThoiGian = (double)item["TongThoiGianLD_Phien"],
                Lat = (double)item["Lat"],
                Lng = (double)item["Lng"],
                XacThuc = (bool)item["FaceStatus"],
                AnhXacThuc = (byte[])item["AnhChup"]
            }).ToList();

            return lst;
        }

        public List<ChiTietPhienHoc> GetVehicleHistory(string diemdanhnguoilxId)
        {
            string query = string.Format("SELECT CreateDate, Lat, Lng, VanToc, TongQuangDuongPhien, TongThoiGianPhien, FaceStatus " +
                "FROM VehicleHistory " +
                "WHERE DiemDanhNguoiLxId = '{0}' ORDER BY CreateDate ASC", diemdanhnguoilxId);
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<ChiTietPhienHoc> lst = dt.AsEnumerable().Select(item => new ChiTietPhienHoc
            {
                ThoiDiem = (DateTime)item["CreateDate"],
                VanToc = Math.Round((double)item["VanToc"], 3),
                QuangDuong = Math.Round((double)item["TongQuangDuongPhien"], 3),
                ThoiGian = (double)item["TongThoiGianPhien"],
                Lat = (double)item["Lat"],
                Lng = (double)item["Lng"],
                XacThuc = (bool)item["FaceStatus"]
            }).ToList();

            return lst;
        }

        public void UpdateImageLogout(string diemdanhnguoilxId, Guid historyId)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx " +
                "SET AnhChup_Logout = (select AnhChup from HistoryData WHERE DiemDanhNguoiLxId = '{0}' AND HistoryID = '{1}') " +
                "WHERE DiemDanhNguoiLxId = '{0}'", diemdanhnguoilxId, historyId);

            DataProviderIMG.Instance.ExecuteNonQuery(query);
        }

        public void UpdateImageLogin(string diemdanhnguoilxId, Guid historyId)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx " +
                "SET AnhChup = (select AnhChup from HistoryData WHERE DiemDanhNguoiLxId = '{0}' AND HistoryID = '{1}') " +
                "WHERE DiemDanhNguoiLxId = '{0}'", diemdanhnguoilxId, historyId);

            DataProviderIMG.Instance.ExecuteNonQuery(query);
        }

        #endregion DAT

        #region In thẻ học viên

        public List<HocVien> GetlstHocVienByKhoaHoc(int idKhoaHoc)
        {
            List<HocVien> lst = new List<HocVien>();
            string query = string.Format("SELECT HV.GiaoVienID, gv.HoTenDem, GV.TenGV, NguoiLXID, MaDK, HoVaTen, HV.NgaySinh, GioiTinh, NoiCT, HV.SoCMT, NgayCapCMT " +
                "FROM NguoiLX AS HV, GiaoVien AS GV WHERE KhoaHocID = '{0}' AND HV.GiaoVienID = GV.GiaoVienID ORDER BY GiaoVienID ASC", idKhoaHoc);
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            lst = dt.AsEnumerable().Select(item => new HocVien
            {
                NguoiLxID = Convert.ToInt64((item)["NguoiLXID"]),
                MaDK = (string)(item)["MaDK"],
                HoTenHV = (string)(item)["HoVaTen"],
                NgaySinh = (DateTime)(item)["NgaySinh"],
                GioiTinh = (string)(item)["GioiTinh"],
                Address = (string)(item)["NoiCT"],
                SoCMT = (string)(item)["SoCMT"],
                NgayCapCMT = (DateTime)(item)["NgayCapCMT"],
                TenGV = (string)(item)["HoTenDem"] + " " + (string)(item)["TenGV"]
            }).ToList();

            return lst;
        }

        public string GetLstNguoiLxID(string khoahocID)
        {
            string query = string.Format("EXEC spGetLstNguoiLxIDByKhoaHoc @KhoaHocID = '{0}'", khoahocID);
            string LstNguoiLxID = DataProviderDAT.Instance.ExecuteScalar(query).ToString();
            return LstNguoiLxID;
        }

        #endregion In thẻ học viên

        #region Lịch học Cabin + STD

        public DataTable GetCategory()
        {
            string query = "EXEC [dbo].[spGetTreeKhoaHoc] @MaCSDT = N'66015'";
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);
            return dt;
        }

        public List<NguoiLxInDAT> GetAllNguoiLxByKhoaHocID(int khoahocId)
        {
            string query = $"EXEC [dbo].[spGetALLNguoiLxByKhoaHocCSDT] @MaCSDT = N'66015', @KhoaHocID = {khoahocId}";

            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<NguoiLxInDAT> lst = new List<NguoiLxInDAT>();

            if (dt != null && dt.Rows.Count > 0)
            {
                lst = dt.AsEnumerable().Select(item => new NguoiLxInDAT
                {
                    KhoaHocID = (int)item["KhoaHocID"],
                    TenKH = item["TenKH"].ToString(),
                    HangDT = item["HangDaoTao"].ToString(),
                    NguoiLXID = (Int64)item["NguoiLXID"],
                    MaDK = item["MaDK"].ToString(),
                    HoVaTen = item["HoVaTen"].ToString(),
                    NgaySinh = (DateTime)item["NgaySinh"],
                    GioiTinh = item["GioiTinh"].ToString(),
                    CCCD = item["SoCMT"].ToString(),
                    NgayCapCMT = (DateTime)item["NgayCapCMT"],
                    GiaoVienID = item["GiaoVienID"] == DBNull.Value ? 0 : (int)item["GiaoVienID"],
                    MaXeTap = item["MaXeTap"].ToString(),
                    MaXeTapB11 = item["XeB11"] == DBNull.Value ? "" : item["XeB11"].ToString(),
                    TongQuangDuongTichLuy = Convert.ToDouble(item["TongQuangDuongLD"]),
                    TongThoiGianTichLuy = (int)item["TongThoiGianLD"],
                    TongThoiGianText = TimeSpan.FromSeconds((int)item["TongThoiGianLD"]).ToString(@"hh\:mm\:ss"),
                    ThoiDiemChayDAT = item["ThoiDiemChayDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemChayDAT"],
                    ThoiDiemKetThucDAT = item["ThoiDiemKetThucDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemKetThucDAT"]
                }).ToList();
            }

            return lst;
        }

        public List<Schedule> GetAllSchedule(int monthId)
        {
            string query = $@"SELECT DISTINCT kh.KhoaHocId, kh.MaKH, kh.TenKH, HV.GiaoVienID, ThoiDiemChayDAT, ThoiDiemKetThucDAT, gv.HoTenDem, gv.TenGV, MaXeTap, XeB11, kh.HangDaoTao
                FROM NguoiLX HV, GiaoVien gv, KhoaHoc kh 
                WHERE gv.GiaoVienID = hv.GiaoVienID and hv.KhoaHocId = kh.KhoaHocID and kh.MonthID = {monthId}";
            
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<Schedule> list = dt.AsEnumerable().Select(item => new Schedule
            {
                KhoaHocId = (int)item["KhoaHocId"],
                MaKH = item["MaKH"].ToString(),
                TenKH = item["TenKH"].ToString(),
                GiaoVienId = (int)item["GiaoVienID"],
                GiaoVien = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString(),
                ThoiDiemChayDAT = item["ThoiDiemChayDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemChayDAT"],
                ThoiDiemKetThucDAT = item["ThoiDiemKetThucDAT"] == DBNull.Value ? (DateTime?)null : (DateTime)item["ThoiDiemKetThucDAT"],
                MaXeTap = item["MaXeTap"] == DBNull.Value ? "" : item["MaXeTap"].ToString(),
                MaXeTapB11 = item["XeB11"] == DBNull.Value ? "" : item["XeB11"].ToString(),
                HangDT = item["HangDaoTao"].ToString()
            }).ToList();

            return list;
        }

        public void UpdateXeTapB11(Schedule rowdata)
        {
            string query = $"UPDATE NguoiLX SET XeB11 = '{rowdata.MaXeTapB11}' WHERE KhoaHocID = {rowdata.KhoaHocId} AND GiaoVienID = {rowdata.GiaoVienId}";
            DataProviderDAT.Instance.ExecuteNonQuery(query);
        }

        public void UpdatePhanCongDAT(int khoahocid, string timestart, string timeend)
        {
            string query = $"UPDATE NguoiLX SET ThoiDiemChayDAT = '{timestart}', ThoiDiemKetThucDAT = '{timeend}' WHERE KhoaHocID = {khoahocid}";
            DataProviderDAT.Instance.ExecuteNonQuery(query);
        }

        #endregion

        #region Phân Công đào tạo
        public void Assignment(AssignmentGV ass)
        {
            string query = string.Format("UPDATE NguoiLX SET GiaoVienID = (SELECT TOP 1 GiaoVienID FROM GiaoVien WHERE HoTenDem  LIKE N'%{1}%' and TenGV like N'{2}'), MaXeTap = '{3}' WHERE MaDK = '{0}'", ass.MaDK, ass.HoTenDem, ass.TenGV, ass.MaXeTap);
            DataProviderDAT.Instance.ExecuteNonQuery(query);
        }

        #endregion

        #region Kết Quả học lý thuyết
        public List<NguoiLxInLT> GetAllNguoiLxByKhoaHoc(int khoahocId)
        {
            string query = $"SELECT KhoaHocID, TenKH, MaDK, NguoiLXID, HoVaTen, NgaySinh, GioiTinh, SoCMT, HangDaoTao, GiaoVienID FROM NguoiLX WHERE KhoaHocID =  {khoahocId}";
            DataTable dt = DataProviderDAT.Instance.ExecuteQuery(query);

            List<NguoiLxInLT> lst = dt.AsEnumerable().Select(item => new NguoiLxInLT
            {
                MaDK = item["MaDK"].ToString(),
                TenKH = item["TenKH"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = (DateTime)item["NgaySinh"],
                GioiTinh = item["GioiTinh"].ToString(),
                CCCD = item["SoCMT"].ToString(),
                HangDT = item["HangDaoTao"].ToString(),
                GiaoVienID = (int)item["GiaoVienID"],
            }).ToList();
            return lst;
        }

        #endregion

        #region Thông tin đào tạo chi tiết

        public DataTable GetResultNguoiLxByID(Int64 NguoiLxId)
        {
            string query = $@"SELECT IMG.AnhChup, IMG.AnhChup_Logout, DAT.DiemDanhNguoiLxID, DAT.ThoiDiem, ThoiDiem_Logout, BienSo, HangXe, ROUND(TongQuangDuongLD_Phien,3) AS TongQuangDuongLD_Phien, ROUND(TongThoiGianLD_Phien / 3600,3) AS TongThoiGianLD_Phien 
            FROM DiemDanhNguoiLx AS DAT, [I2EDAT_IMAGE].[dbo].[DiemDanhNguoiLx] AS IMG 
            WHERE DAT.DiemDanhNguoiLxID = IMG.DiemDanhNguoiLxID AND DAT.NguoiLxID = {NguoiLxId} 
            ORDER BY DAT.ThoiDiem DESC";
            return DataProviderDAT.Instance.ExecuteQuery(query);

        }

        public DataTable GetBaoCaoHocVienByNguoilxID(Int64 NguoilxId)
        {
            string query = $"EXEC [dbo].[spTongHopDiemDanhLDByNguoiLx] @MaCSDT = N'66015', @NguoiLxID = {NguoilxId}";
            return DataProviderDAT.Instance.ExecuteQuery(query);
        }

        public byte[] GetAnhChupNguoiLx(Int64 NguoiLxID)
        {
            string query = $"EXEC [dbo].[spGetAnhChupNguoiLx] @MaCSDT = N'66015', @NguoiLxID = {NguoiLxID}";
            return (byte[])DataProviderDAT.Instance.ExecuteScalar(query);
        }

        #endregion
    }
}