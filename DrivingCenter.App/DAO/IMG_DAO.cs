using DrivingCenter.App.DataProvider;
using System.Data;

namespace DrivingCenter.App.DAO
{
    public class IMG_DAO
    {

        private readonly DbProvider _db = new DbProvider(Enums.DatabaseType.db_DatImg);
        public DataTable GetImgById(string diemdanhnguoilxId)
        {
            const string query = "SELECT DiemDanhNguoiLxID, AnhChup, AnhChup_Logout FROM DiemDanhNguoiLx WHERE DiemDanhNguoiLxID = @id";
            DataTable dt = _db.QueryToDataTable(query, new { id = diemdanhnguoilxId });
            return dt;   
        }

        public DataTable GetImgByMaDK(string maDK)
        {
            const string query = "SELECT Anh FROM NguoiLX WHERE MaDK = @MaDK";
            return _db.QueryToDataTable(query, new { MaDK = maDK });
        }

        public DataTable GetDataTheHocVien(string lstNguoiLxID)
        {
            const string query = "EXEC [dbo].[usp_LoadDaTaHV] @lstNguoiLXID = @lstNguoiLxID";
            return _db.QueryToDataTable(query, new { lstNguoiLxID });
        }

        public DataTable GetDataPhieuTheoDoi(string lstNguoiLxID)
        {
            const string query = "EXEC [dbo].[usp_LoadDataPhieuTheoDoi] @lstNguoiLXID = @lstNguoiLxID";
            return _db.QueryToDataTable(query, new { lstNguoiLxID });
        }

        public DataTable GetDataTemTheDAT(string lstNguoiLxID)
        {
            const string query = "EXEC [dbo].[usp_LoadDaTaHvInTemDat] @lstNguoiLXID = @lstNguoiLxID";
            return _db.QueryToDataTable(query, new { lstNguoiLxID });
        }
    }
}