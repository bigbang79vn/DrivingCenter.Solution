using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using DrivingCenter.App.DTO.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class AppBee_DAO
    {
        private static AppBee_DAO instance;

        public static AppBee_DAO Instance
        {
            get { if (instance == null) instance = new AppBee_DAO(); return AppBee_DAO.instance; }
            private set => AppBee_DAO.instance = value;
        }

        public AppBee_DAO()
        {
        }

        #region Giáo Viên
        public List<GiaoVien> GetAllGV()
        {
            List<GiaoVien> lstGiaoVien = new List<GiaoVien>();
            string query = $@"SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX, AnhChup, MaCNDT, hv.Ten , SuDung, DateUse 
                        FROM tblGiaoVien AS gv
                        INNER JOIN tblTrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma";
            DataTable dt = DataProviderApp.Instance.ExecuteQuery(query);

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
        public List<GiaoVien> Get_GiaoVienByMaKH(string makh)
        {
            List<GiaoVien> lstGiaoVien = new List<GiaoVien>();
            string query = $@"SELECT GV.GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX, AnhChup, MaCNDT, hv.Ten , SuDung, DateUse
                        FROM tblKhoaHoc_GiaoVien AS kh
						INNER JOIN tblGiaoVien AS gv ON KH.GiaoVienID = GV.GiaoVienID
                        INNER JOIN tblTrinhDoHocVan AS hv ON gv.MaTrinhDoHocVan = hv.Ma
						WHERE MaKH = '{makh}'";
            DataTable dt = DataProviderApp.Instance.ExecuteQuery(query);

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
                DateUse = item["DateUse"] == DBNull.Value ? DateTime.Now : (DateTime)item["DateUse"],
                //NgayHL = item["NgayHL"] == DBNull.Value ? DateTime.Now : (DateTime)item["NgayHL"],
            }).ToList();

            return lstGiaoVien;
        }


        public DataTable GetlstHangGPLX()
        {
            string query = $"SELECT [ID], [MaHang], [TenHang] FROM HangGPLX";
            return DataProviderApp.Instance.ExecuteQuery(query);
        }

        public DataTable GetlstHocVan()
        {
            string query = $"SELECT * FROM tblTrinhDoHocVan";
            return DataProviderApp.Instance.ExecuteQuery(query);
        }

        public List<Schedule> GetKhoaHocDaPhanCong(string maKh)
        {
            string query = $"SELECT * FROM tblKhoaHoc_GiaoVien WHERE MaKH = '{maKh}'";

            DataTable dt = DataProviderApp.Instance.ExecuteQuery(query);

            List<Schedule> lst = dt.AsEnumerable().Select(item => new Schedule
            {
                Id = (int)item["ID"],
                MaKH = (string)item["MaKH"],
                GiaoVienId = (int)item["GiaoVienID"],
                GiaoVien = item["HoTenGV"] == DBNull.Value ? null : (string)item["HoTenGV"],
                IdXeTap = item["IdXeTap"] == DBNull.Value ? null : (int?)item["IdXeTap"],
                MaXeTap = item["MaXeTap"] == DBNull.Value ? null : (string)item["MaXeTap"],
                MaXeTapB11 = item["MaXeB11"] == DBNull.Value ? null : (string)item["MaXeTapB11"],
                TimeInB11 = item["DateStartB11"] == DBNull.Value ? null : (DateTime?)item["DateStartB11"],
                TimeOutB11 = item["DateEndB11"] == DBNull.Value ? null : (DateTime?)item["DateEndB11"],
                SoHV = (int)item["SoHV"],
                ThietBiID = item["Cabin"] == DBNull.Value ? null : (int?)item["ThietBiID"],
                Cabin = item["Cabin"] == DBNull.Value ? null : (string)item["Cabin"],
                TimeInCabin = item["DateStartCabin"] == DBNull.Value ? null : (DateTime?)item["DateStartCabin"],
                TimeOutCabin = item["DateEndCabin"] == DBNull.Value ? null : (DateTime?)item["DateEndCabin"],
                Status = item["TrangThai"] == DBNull.Value ? null : (bool?)item["TrangThai"],
                NgayHL = (DateTime)item["HieuLuc"],
                NgayHetHL = (DateTime)item["HetHieuLuc"],
            }).ToList();
            return lst;
        }

        public string GetMaXeTapById(int idxetap)
        {
            string query = $"SELECT MaXe FROM tblXeTapLai WHERE ID = {idxetap}";
            object result = DataProviderApp.Instance.ExecuteScalar(query);
            return result?.ToString();
        }


        public int InsertKhoaHocGiaoVien(string maKhoaHoc, int? giaoVienId, int? xeTapId, int soHocVien, DateTime ngayHieuLuc, DateTime ngayHetHieuLuc)
        {
            string query = @"EXEC dbo.usp_KhoaHoc_GiaoVien_Insert 
                        @MaKH, 
                        @GiaoVienID, 
                        @IdXeTap,
                        @SoHV, 
                        @NgayHL, 
                        @NgayHetHL";

            var parameters = new
            {
                MaKH = maKhoaHoc,
                GiaoVienID = giaoVienId,
                IdXeTap = xeTapId,
                SoHV = soHocVien,
                NgayHL = ngayHieuLuc,
                NgayHetHL = ngayHetHieuLuc
            };

            return DataProviderApp.Instance.ExecuteNonQuery(query, parameters);
        }

        public int UpdateKhoaHocGiaoVien(string maKhoaHoc, int? giaoVienId, int? xeTapId, int soHocVien, DateTime ngayHieuLuc, DateTime ngayHetHieuLuc)
        {
            string query = @"EXEC dbo.usp_KhoaHoc_GiaoVien_Insert 
                        @MaKH, 
                        @GiaoVienID, 
                        @IdXeTap,
                        @SoHV, 
                        @NgayHL, 
                        @NgayHetHL";

            var parameters = new
            {
                MaKH = maKhoaHoc,
                GiaoVienID = giaoVienId,
                IdXeTap = xeTapId,
                SoHV = soHocVien,
                NgayHL = ngayHieuLuc,
                NgayHetHL = ngayHetHieuLuc
            };

            return DataProviderApp.Instance.ExecuteNonQuery(query, parameters);
        }

        #endregion

        #region Xe Tập Lái
        public List<XeTapLai> GetLstXeTapLai()
        {
            string query = $"SELECT * FROM tblXeTapLai ORDER BY BienSoXe ASC";

            DataTable dt = DataProviderApp.Instance.ExecuteQuery(query);

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
                ThanhLy = (bool)item["ThanhLy"],
                ChuSoHuu = (string)item["ChuSoHuu"],
                SuDungDen = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"],
            }).ToList();

            return lstXeTapLai;
        }
        #endregion
    }
}
