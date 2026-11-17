using DrivingCenter.App.DataProvider;
using DrivingCenter.App.Enums;
using System;
using System.Data;

namespace DrivingCenter.App.DAO
{
    /// <summary>
    /// Nền chung cho các DAO của CSDL DAT (EMCDAT + EMCDAT_IMAGE), tách ra từ DAT_DAO cũ.
    /// Mọi câu lệnh đều dùng tham số (@ten) — không ghép chuỗi giá trị vào SQL.
    /// </summary>
    public abstract class DatDaoBase
    {
        /// <summary>Mã cơ sở đào tạo dùng trong các stored procedure DAT.</summary>
        protected const string MaCSDT = "66015";

        protected readonly DbProvider _db = new DbProvider(DatabaseType.db_DAT);
        protected readonly DbProvider _dbImg = new DbProvider(DatabaseType.db_DatImg);

        /// <summary>Đọc số thực, cột NULL → 0.</summary>
        protected static double D(DataRow r, string c) => r[c] == DBNull.Value ? 0d : Convert.ToDouble(r[c]);

        /// <summary>Đọc chuỗi đã cắt khoảng trắng, cột NULL → "".</summary>
        protected static string S(DataRow r, string c) => r[c] == DBNull.Value ? "" : Convert.ToString(r[c]).Trim();
    }
}
