using DrivingCenter.App.DataProvider;
using System.Data;

namespace DrivingCenter.App.DAO
{
    public class IMG_DAO
    {

        private readonly DbProvider _db = new DbProvider(Enums.DatabaseType.db_DatImg);
        public DataTable GetImgById(string diemdanhnguoilxId)
        {
            string query = string.Format("SELECT DiemDanhNguoiLxID, AnhChup, AnhChup_Logout FROM DiemDanhNguoiLx WHERE DiemDanhNguoiLxID = '{0}'", diemdanhnguoilxId);
            DataTable dt = _db.QueryToDataTable(query);
            return dt;   
        }

        public DataTable GetImgByMaDK(string maDK)
        {
            string query = $"SELECT Anh FROM NguoiLX WHERE MaDK = '{maDK}'";
            return _db.QueryToDataTable(query);
        }

        public DataTable GetDataTheHocVien(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDaTaHV] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return _db.QueryToDataTable(query);
        }

        public DataTable GetDataPhieuTheoDoi(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDataPhieuTheoDoi] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return _db.QueryToDataTable(query);
        }

        public DataTable GetDataTemTheDAT(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDaTaHvInTemDat] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return _db.QueryToDataTable(query);
        }
    }
}