using DevExpress.Data.Filtering.Helpers;
using DocumentFormat.OpenXml.Drawing;
using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.Common;
using DrivingCenter.App.DTO.DAT;
using GMap.NET;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<int> lstYear = new List<int>();
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery("SELECT DISTINCT DATEPART(year, CreateDate) as Nam FROM NguoiLX ORDER BY Nam DESC");

            lstYear = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<int>("Nam")).ToList();

            return lstYear;
        }

        public int LoadSoHocVienByYear(int year)
        {
            int SoHvByYear = (int)DataProviderDAT.Instance.ExcuteScalar(string.Format("SELECT COUNT(*) FROM NguoiLX where DATEPART(year, CreateDate) = {0}", year));
            return SoHvByYear;
        }

        public int LoadSoKhoaHocByYear(int year)
        {
            int SoKhByYear = (int)DataProviderDAT.Instance.ExcuteScalar(string.Format("SELECT COUNT(*) FROM KhoaHoc where DATEPART(year, NgayKG) = {0}", year));
            return SoKhByYear;
        }

        public int LoadSoHocVien()
        {
            int SoHv = (int)DataProviderDAT.Instance.ExcuteScalar("SELECT COUNT(*) FROM NguoiLX");
            return SoHv;
        }

        public int LoadSoKhoaHoc()
        {
            int SoKh = (int)DataProviderDAT.Instance.ExcuteScalar("SELECT COUNT(*) FROM KhoaHoc ");
            return SoKh;
        }

        public int LoadSoGiaoVien()
        {
            int SoGv = (int)DataProviderDAT.Instance.ExcuteScalar("SELECT COUNT(*) FROM GiaoVien ");
            return SoGv;
        }

        public int LoadSoXeTapLai()
        {
            int SoXetl = (int)DataProviderDAT.Instance.ExcuteScalar("SELECT COUNT(*) FROM Vehicles");
            return SoXetl;
        }

        public void UpdateKhoaHocDangDT()
        {
            string query = string.Format("SELECT kh.KhoaHocID, kh.TenKH, kh.HangDaoTao, kh.SoHVDuDK ,COUNT(hv.HoVaTen) as TongHocVien " +
                "FROM NguoiLX as hv, KhoaHocDangDT as kh " +
                "WHERE kh.KhoaHocID = hv.KhoaHocID " +
                "GROUP by kh.KhoaHocID, kh.TenKH, kh.HangDaoTao, kh.SoHVDuDK " +
                "ORDER by HangDaoTao");

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<KhoaHocDangDT> lst = new List<KhoaHocDangDT>();

            foreach (DataRow item in dt.Rows)
            {
                KhoaHocDangDT pr = new KhoaHocDangDT(item);
                lst.Add(pr);
                string qrUpdate = string.Format("UPDATE [KhoaHocDangDT] set [TongHocVien] = {0} where [KhoaHocID] = {1}", pr.TongHocVien, pr.KhoaHocID);
                DataProviderDAT.Instance.ExcuteNoneQuery(qrUpdate);
            }
        }

        public void UpdateHvDuDK(int sohvdudk, int khoahocID)
        {
            string query = string.Format("UPDATE KhoaHocDangDT SET SoHVDuDK = '{0}' where KhoaHocID = {1}", sohvdudk, khoahocID);
            DataProviderDAT.Instance.ExcuteNoneQuery(query);
        }

        public int CountPhien(int day, int month, int year)
        {
            string query = string.Format("SELECT COUNT(MaCSDT) as SoPhien " +
                "FROM DiemDanhNguoiLx " +
                "WHERE ActivePhien = 1 and TongQuangDuongLD_Phien >= 5 AND convert(varchar, ThoiDiem, 103) = '{0:00}/{1:00}/{2:0000}' " +
                "GROUP BY MaCSDT", day, month, year);

            var getValue = DataProviderDAT.Instance.ExcuteScalar(query);
            return getValue == null ? 0 : (int)getValue;
        }

        //public float SumKmByDay(int day, int month, int year)
        //{
        //    string query = string.Format("SELECT ROUND(SUM(TongQuangDuongLD_Phien),0) AS SoKm " +
        //        "FROM DiemDanhNguoiLx " +
        //        "WHERE ActivePhien = 1 AND ThoiDiem IS NOT NULL AND TongQuangDuongLD_Phien > 5 and convert(varchar, ThoiDiem, 103) = '{0:00}/{1:00}/{2:0000}'", day, month, year);

        //    var getValue = DataProviderDAT.Instance.ExcuteScalar(query);
        //    return getValue == null ? 0 : (float)getValue;
        //}

        #endregion Trang Home

        #region Sync GV for Cabin

        public List<GvCabin> GetListGvByNameKH(string TenKH)
        {
            string query = string.Format("select MaDK, GiaoVienID from NguoiLX where TenKH like '%{0}%' ORDER BY GiaoVienID", TenKH);
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            string query = "SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX FROM GiaoVien";
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            }).ToList();

            return lstGiaoVien;
        }

        #endregion Sync GV for Cabin

        #region Tổng hợp xe tập lái

        public List<XeTapLai> GetLstXeTapLai()
        {
            string query = string.Format("SELECT ID, MaXe, BienSoXe, HangGPLX, NamSX, SoGPXTL, NgayCapGPXTL, NgayHHGPXTL  FROM XeTapLai ORDER BY BienSoXe ASC");

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<XeTapLai> lstXeTapLai = dt.AsEnumerable().Select(item => new XeTapLai
            {
                VehicleID = (int)item["ID"],
                BienSoXe = (string)item["BienSoXe"],
                MaXe = (string)item["MaXe"],
                HangGPLX = (string)item["HangGPLX"],
                NamSX = (int)item["NamSX"],
                SoGPXTL = (string)item["SoGPXTL"],
                NgayCapGPXTL = (DateTime)item["NgayCapGPXTL"],
                NgayHHXTL = (DateTime)item["NgayHHGPXTL"]
            }).ToList();

            return lstXeTapLai;
        }

        public List<XeTapLai> GetLstXeTapLaiOnline()
        {
            string query = string.Format("select VehicleID, BienSoXe, MaXe, Version, HangGPLX, Imei, VanToc, UpdateTime from Vehicles where online = 1 order by BienSoXe ASC");

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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

        //    DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            int result = DataProviderDAT.Instance.ExcuteNoneQuery(query);
            return result;
        }

        public int DeleteXeTapLai(int vehicleId, string imei)
        {
            string query = string.Format("DELETE FROM [dbo].[Vehicles] WHERE VehicleID = {0} AND Imei = '{1}' ", vehicleId, imei);
            int result = DataProviderDAT.Instance.ExcuteNoneQuery(query);
            return result;
        }

        #endregion Tổng hợp xe tập lái

        #region DAT

        public void LockSession(string diemdanhnguoilxId, string note)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx set Note = N'{0}', ActivePhien = 0 WHERE DiemDanhNguoiLxID = '{1}'", note, diemdanhnguoilxId);
            DataProviderDAT.Instance.ExcuteNoneQuery(query);
        }

        public void UnlockSession(string diemdanhnguoilxId)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx set Note = NULL, ActivePhien = 1 WHERE DiemDanhNguoiLxID = '{0}'", diemdanhnguoilxId);
            DataProviderDAT.Instance.ExcuteNoneQuery(query);
        }

        public void ReLoad(string date)
        {
            string query1 = string.Format("Exec spGetDiemDanhNguoiLxByDay '{0}',0,0", date);
            string query2 = string.Format("Exec spGetDiemDanhNguoiLxByDay '{0}',1,0", date);

            DataProviderDAT.Instance.ExcuteNoneQuery(query1);
            DataProviderDAT.Instance.ExcuteNoneQuery(query2);
        }

        public List<PhienHoc> GetAllPhienHocToDay(string day)
        {
            string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen, kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong, ph.ActivePhien, ph.Note " +
                "FROM DiemDanhNguoiLx as ph, GiaoVien as gv, NguoiLX as hv,  KhoaHoc as kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and hv.NguoiLXID = ph.NguoiLxID AND hv.KhoaHocID = kh.KhoaHocID and CONVERT(VARCHAR, ThoiDiem, 103) = '{0}' " +
                "ORDER by ph.Logout, ph.SendTongCuc ASC", day);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);
            List<PhienHoc> LstPhienHoc = new List<PhienHoc>();

            LstPhienHoc = dt.AsEnumerable().Select(item => new PhienHoc
            {
                HoTenGV = (string)item["HoTenGV"],
                NguoiLxID = (Int64)item["NguoiLxID"],
                HoTenHV = (string)item["HoVaTen"],
                MaKH = (string)item["MaKH"],
                TenKH = (string)item["TenKH"],
                MaPhien = (Guid)item["DiemDanhNguoiLxID"],
                BienSoXe = (string)item["BienSo"],
                Time_LogIn = (DateTime)item["ThoiDiem"],
                Time_LogOut = item["ThoiDiem_Logout"] == DBNull.Value ? DateTime.Now : (DateTime)item["ThoiDiem_Logout"],
                QuangDuongPhien = item["TongQuangDuongLD_Phien"] == DBNull.Value ? 0 : (double)item["TongQuangDuongLD_Phien"],
                ThoiGianPhien = item["TongThoiGianLD_Phien"] == DBNull.Value ? "" : (string)item["TongThoiGianLD_Phien"],
                dbTime = item["dbTime"] == DBNull.Value ? 0 : (double)item["dbTime"],
                LogOut = (bool)item["Logout"],
                SendTongCuc = (bool)item["SendTongCuc"],
                NhanDien = item["SoLanNhanDienThanhCong"].ToString() + "/" + item["SoLanNhanDien"].ToString(),
                SoLanNhanDien = item["SoLanNhanDien"] == DBNull.Value ? 0 : (int)item["SoLanNhanDien"],
                SoLanNhanDienThanhCong = item["SoLanNhanDienThanhCong"] == DBNull.Value ? 0 : (int)item["SoLanNhanDienThanhCong"],
                Active = (bool)item["ActivePhien"],
                Note = item["Note"] == DBNull.Value ? string.Empty : (string)item["Note"]
            }).ToList();

            return LstPhienHoc;
        }

        public List<PhienHoc> GetLstPhienByDateTrue(string day)
        {
            List<PhienHoc> lstPhienHoc = GetAllPhienHocToDay(day).Where(x => x.QuangDuongPhien >= 1).ToList();
            return lstPhienHoc;
        }

        public List<KhoaHocDangDT> GetAllKhoaHocDDT()
        {
            string query = "SELECT KhoaHocID, MaKH, TenKH, HangDaoTao, SoHVDuDK, TongHocVien FROM KhoaHocDangDT";

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            string query = string.Format("SELECT KhoaHocID, MaDK, HoTenGV, HoVaTen, HangDaoTao,  COUNT(NgayHoc) AS SoNgayHoc , ROUND(SUM(TongQuangDuongLD_Phien),3) as TongQuangDuongLD_Phien , ROUND(SUM(TongThoiGianLD_Phien),1) as TongThoiGianLD_Phien , ROUND(SUM(QuangDuongPhienGuiTC),3) as QuangDuongPhienGuiTC, ROUND(SUM(ThoiGianPhienGuiTC),3) as ThoiGianPhienGuiTC " +
                "FROM KQHvGroup " +
                "WHERE KhoaHocID = '{0}' " +
                "GROUP BY MaDK, HoTenGV, HoVaTen, HangDaoTao, KhoaHocID " +
                "ORDER BY HoTenGV", khoahocId);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<TongHopHVByKhoa> lstResult = new List<TongHopHVByKhoa>();

            lstResult = dt.AsEnumerable().Select(item => new TongHopHVByKhoa
            {
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

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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

        public List<TongHopHVByKhoa> TongHopPhBD(int khoahocId)
        {
            string query = string.Format("SELECT MaDK, TongQuangDuongLD_Phien, TongThoiGianLD_Phien FROM vGioBanDemGroup WHERE KhoaHocID = '{0}' ", khoahocId);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            string query = string.Format("SELECT MaGV , HoTenGV, ThoiDiem, COUNT(HoVaTen) AS SoHocVien, SUM(SoPhien) AS SoPhien, TenKH, BienSoXe, ROUND(SUM(TongQuangDuongLD_Phiens),1) as SQuangDuong, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianLD_Phiens),0),108) as SThoiGian " +
                "FROM TongHopGvFn " +
                "WHERE TenKH = '{0}' " +
                "GROUP BY MaGV, HoTenGV, ThoiDiem, BienSoXe, TenKH " +
                "ORDER BY HoTenGV", khoahoc);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<TongHopGV> lstGV = new List<TongHopGV>();
            foreach (DataRow item in dt.Rows)
            {
                TongHopGV gv = new TongHopGV(item);
                lstGV.Add(gv);
            }

            return lstGV;
        }

        #endregion Menu Context Tổng hợp

        public int CheckImgNull(string day)
        {
            int CountPhImgNull = 0;
            string query = string.Format("SELECT COUNT(*) AS NumImgIsNull FROM DiemDanhNguoiLx WHERE CONVERT(VARCHAR, ThoiDiem, 112) = '{0}' AND AnhChup_Logout IS NULL", day);

            CountPhImgNull = (int)DataProviderIMG.Instance.ExcuteScalar(query);

            return CountPhImgNull;
        }

        public int CheckImgNullNew(string diemdanhnguoilxId)
        {
            string query = string.Format("SELECT COUNT(AnhChup_Logout) FROM DiemDanhNguoiLx WHERE DiemDanhNguoiLxId = '{0}'", diemdanhnguoilxId);

            int count = (int)DataProviderIMG.Instance.ExcuteScalar(query);

            return count;
        }

        public void KetThucPhien(Int64 nguoiLxID, string diemdanhnguoilxId)
        {
            DataProviderDAT.Instance.ExcuteNoneQuery(string.Format(string.Format("EXEC [dbo].[spKetThucPhienDaoTao] @NguoiLxID = {0}, @DiemDanhNguoiLxID = N'{1}' ", nguoiLxID, diemdanhnguoilxId)));
        }

        #endregion DAT

        public List<TongHopHVByDay> TongHopGioHVByDay(string day)
        {
            string query = string.Format("SELECT ph.MaDK, CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, hv.HoVaTen, kh.TenKH, pH.BienSo as BienSoXe, ROUND(SUM(ph.TongQuangDuongLD_Phien), 1) AS TongQuangDuongLD_Phien, CONVERT(varchar, DATEADD(second, SUM(ph.TongThoiGianLD_Phien), 0), 108) AS TongThoiGianLD_Phien " +
                "FROM DiemDanhNguoiLx as ph, GiaoVien as gv, NguoiLX as hv, KhoaHocDangDT as kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and CONVERT(VARCHAR, ThoiDiem, 103) = '{0}' " +
                "GROUP BY CONCAT(gv.HoTenDem, ' ', gv.TenGV), ph.MaDK, hv.HoVaTen, kh.TenKH, PH.BienSo " +
                "ORDER BY HoTenGV", day);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<TongHopHVByDay> LstSum = new List<TongHopHVByDay>();
            foreach (DataRow item in dt.Rows)
            {
                TongHopHVByDay sumTime = new TongHopHVByDay(item);
                LstSum.Add(sumTime);
            }

            return LstSum;
        }

        public List<TongHopGV> TongHopGVsByDay(string day)
        {
            string query = string.Format("SELECT MaGV , HoTenGV, ThoiDiem, COUNT(HoVaTen) AS SoHocVien, SUM(SoPhien) AS SoPhien, TenKH, BienSoXe, ROUND(SUM(TongQuangDuongLD_Phiens),1) as SQuangDuong, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianLD_Phiens),0),108) as SThoiGian " +
                "FROM TongHopGvFn " +
                "WHERE ThoiDiem = '{0}' " +
                "GROUP by MaGV, HoTenGV, ThoiDiem, BienSoXe, TenKH " +
                "ORDER by HoTenGV", day);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<TongHopGV> lstGV = new List<TongHopGV>();
            foreach (DataRow item in dt.Rows)
            {
                TongHopGV gv = new TongHopGV(item);
                lstGV.Add(gv);
            }

            return lstGV;
        }

        public List<string> GetLstGvTrue(string day)
        {
            string query = string.Format("SELECT DISTINCT MaGV FROM DiemDanhNguoiLx " +
                "WHERE (ROUND(Lat,3,1) = '12.67' AND CONVERT(VARCHAR, ThoiDiem, 103) = '{0}') " +
                "OR (ROUND(Lat_Logout,3,1) = '12.67' AND CONVERT(VARCHAR, ThoiDiem, 103) = '{0}') " +
                "OR (ROUND(Lng,3,1) = '108.096' AND CONVERT(VARCHAR, ThoiDiem, 103) = '{0}') " +
                "OR (ROUND(Lng_Logout,3,1) = '108.096' AND CONVERT(VARCHAR, ThoiDiem, 103) = '{0}') " +
                "AND TongQuangDuongLD_Phien > 5 AND ActivePhien = 1 " +
                "Order by MaGV asc ", day);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<string> lst = dt.AsEnumerable()
                           .Select(r => r.Field<string>("MaGV"))
                           .ToList();
            return lst;
        }

        public List<PhienHoc> GetLstPhienLoiByDate(string day)
        {
            List<PhienHoc> lstError = GetAllPhienHocToDay(day).Where(x => x.QuangDuongPhien <= 5).ToList();

            return lstError;
        }

        public void DeletePhByID(string diemdanhnguoilxID)
        {
            string query = string.Format("EXEC [dbo].[spDeletePhienHoc] @diemdanhnguoilxID = '{0}'", diemdanhnguoilxID);
            DataProviderDAT.Instance.ExcuteNoneQuery(query);
            DataProviderIMG.Instance.ExcuteNoneQuery(query);
        }

        public List<PhienHoc> TongHopPhienHocLoi(string from, string to)
        {
            string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, hv.HoVaTen, ph.NguoiLxID, ph.DiemDanhNguoiLxID, ph.TongThoiGianLD_Phien as TongThoiGian_Double, hv.HangDaoTao, ph.ThoiDiem, ph.ThoiDiem_Logout,  kh.TenKH, ph.LogOut, ph.SendTongCuc, ROUND(ph.TongQuangDuongLD_Phien,1) AS TongQuangDuongLD_Phien, CONVERT(varchar, DATEADD(second, ph.TongThoiGianLD_Phien, 0), 108) AS TongThoiGianLD_Phien, ph.BienSo, ph.Note " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
                "where ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ActivePhien = 0 and CONVERT(VARCHAR, ph.ThoiDiem, 112) >= '{0}' AND CONVERT(VARCHAR, ph.ThoiDiem, 112) <= '{1}' " +
                "ORDER BY HoTenGV", from, to);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            string query = string.Format("SELECT CONCAT(gv.HoTenDem, ' ', gv.TenGV) AS HoTenGV, ph.NguoiLxID, hv.HoVaTen,kh.MaKH, kh.TenKH, ph.DiemDanhNguoiLxID, ph.BienSo, ph.Logout, ph.SendTongCuc, ph.ThoiDiem, ph.ThoiDiem_Logout, ROUND( ph.TongQuangDuongLD_Phien, 1) AS TongQuangDuongLD_Phien,  CONVERT(varchar,DATEADD(second,ph.TongThoiGianLD_Phien,0),108) AS TongThoiGianLD_Phien, TongThoiGianLD_Phien as dbTime, ph.SoLanNhanDien, ph.SoLanNhanDienThanhCong , ActivePhien, Note " +
                "FROM DiemDanhNguoiLx AS ph, GiaoVien AS gv, NguoiLX AS hv, KhoaHoc AS kh " +
                "WHERE ph.GiaoVienID = gv.GiaoVienID and ph.NguoiLxID = hv.NguoiLXID and hv.KhoaHocID = kh.KhoaHocID and ph.TongQuangDuongLD_Phien >= 1 and ph.BienSo = '{0}' and CONVERT(VARCHAR, ph.ThoiDiem, 112) >= '{1}' AND CONVERT(VARCHAR, ph.ThoiDiem, 112) <= '{2}' " +
                "ORDER BY ThoiDiem", biensoxe, from, to);

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
                DataProviderDAT.Instance.ExcuteNoneQuery(query);
                DataProviderIMG.Instance.ExcuteNoneQuery(query);
            }
        }

        public List<PointLatLng> GetlstPoint(string diemdanhnguoilxId)
        {
            List<PointLatLng> lstPoint = new List<PointLatLng>();
            string query = string.Format("SELECT Lat, Lng  FROM VehicleHistory WHERE DiemDanhNguoiLxId = '{0}' ORDER BY TongQuangDuongPhien asc", diemdanhnguoilxId);
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            lstPoint = dt.AsEnumerable().Select(item => new PointLatLng {
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

            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);
            return dt;
        }

        public List<ChiTietPhienHoc> GetHistoryData(string diemdanhnguoilxId)
        {
            string query = string.Format("SELECT dt.HistoryID, dt.ThoiDiem, dt.VanToc, TongQuangDuongLD_Phien, TongThoiGianLD_Phien, Lat, Lng, FaceStatus, img.AnhChup " +
                "FROM HistoryData as dt, I2EDAT_IMAGE.dbo.HistoryData as img " +
                "WHERE dt.DiemDanhNguoiLxId = '{0}' AND dt.HistoryID = img.HistoryID", diemdanhnguoilxId);
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<ChiTietPhienHoc> lst = dt.AsEnumerable().Select(item => new ChiTietPhienHoc {
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
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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

            DataProviderIMG.Instance.ExcuteNoneQuery(query);
        }

        public void UpdateImageLogin(string diemdanhnguoilxId, Guid historyId)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx " +
                "SET AnhChup = (select AnhChup from HistoryData WHERE DiemDanhNguoiLxId = '{0}' AND HistoryID = '{1}') " +
                "WHERE DiemDanhNguoiLxId = '{0}'", diemdanhnguoilxId, historyId);

            DataProviderIMG.Instance.ExcuteNoneQuery(query);
        }

        #region In thẻ học viên

        public List<HocVien> GetlstHocVienByKhoaHoc(int idKhoaHoc)
        {
            List<HocVien> lst = new List<HocVien>();
            string query = string.Format("SELECT HV.GiaoVienID, gv.HoTenDem, GV.TenGV, NguoiLXID, MaDK, HoVaTen, HV.NgaySinh, GioiTinh, NoiCT, HV.SoCMT, NgayCapCMT " +
                "FROM NguoiLX AS HV, GiaoVien AS GV WHERE KhoaHocID = '{0}' AND HV.GiaoVienID = GV.GiaoVienID ORDER BY GiaoVienID ASC", idKhoaHoc);
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

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
            string LstNguoiLxID = DataProviderDAT.Instance.ExcuteScalar(query).ToString();
            return LstNguoiLxID;
        }

        #endregion In thẻ học viên

        public DateTime GetMinNgayKG()
        {
            string query = "SELECT MIN(NgayKG) FROM KhoaHocDangDT";
            DateTime result = (DateTime)DataProviderDAT.Instance.ExcuteScalar(query);
            return result;
        }

        public DateTime GetMaxNgayBG()
        {
            string query = "SELECT MAX(NgayBG) FROM KhoaHocDangDT";
            DateTime result = (DateTime)DataProviderDAT.Instance.ExcuteScalar(query);
            return result;
        }

        #region Lịch học Cabin + STD

        public DataTable GetCategory()
        {
            DataTable dt = new DataTable();
            string query = "SELECT MonthID, ParentMonthID, Name FROM Categories";
            dt = DataProviderDAT.Instance.ExcuteQuery(query);
            return dt;
        }

        public List<Schedule> GetAllSchedule(int monthId)
        {
            string query = $"SELECT Id, KhoaHocId, MaKH, TenKH, lh.GiaoVienID, gv.HoTenDem, gv.TenGV, MaXeTap, MaXeTapB11, TimeInB11, TimeOutB11, ThietBiID, TimeInCB, TimeOutCB, Status, HangDT " +
                $"FROM LichHoc lh, GiaoVien gv " +
                $"WHERE gv.GiaoVienID = lh.GiaoVienID and MonthId = {monthId}";
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            List<Schedule> list = dt.AsEnumerable().Select(item => new Schedule { 
                Id = (Guid)item["Id"],
                KhoaHocId = (int)item["KhoaHocId"],
                MaKH = item["MaKH"].ToString(),
                TenKH = item["TenKH"].ToString(),
                GiaoVienId = (int)item["GiaoVienID"],
                GiaoVien = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString(),
                MaXeTap = item["MaXeTap"] == DBNull.Value ? "" : item["MaXeTap"].ToString(),
                MaXeTapB11 = item["MaXeTapB11"] == DBNull.Value? "" : item["MaXeTapB11"].ToString(),
                //TimeInB11 = (DateTime)item["TimeInB11"],
                //TimeOutB11 = (DateTime)item["TimeOutB11"],
                //ThietBiID = (int)item["ThietBiID"],
                //TimeInCB = (DateTime)item["TimeInCB"],
                //TimeOutCB = (DateTime)item["TimeOutCB"],
                //Status =  (bool)item["Status"],
                HangDT = item["HangDT"].ToString()
            }).ToList();

            return list;
        }

        #endregion

        #region Phân Công đào tạo
        public void Assignment(Assignment ass)
        {
            string query = string.Format("UPDATE NguoiLX SET GiaoVienID = (SELECT GiaoVienID FROM GiaoVien WHERE HoTenDem  LIKE N'%{1}%' and TenGV like N'%{2}%'), MaXeTap = '{3}' WHERE MaDK = '{0}'", ass.MaDK, ass.HoTenDem, ass.TenGV, ass.MaXeTap);
            DataProviderDAT.Instance.ExcuteNoneQuery(query);
        }
        #endregion
    }
}