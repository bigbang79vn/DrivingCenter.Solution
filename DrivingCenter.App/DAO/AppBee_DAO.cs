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
                SuDungDen = item["DateUse"] == DBNull.Value ? null : (DateTime?)item["DateUse"],
            }).ToList();

            return lstXeTapLai;
        }
    }
}
