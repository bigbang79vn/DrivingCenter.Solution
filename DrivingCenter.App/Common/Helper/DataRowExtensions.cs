using System;
using System.Data;

namespace DrivingCenter.App.Helper
{
    /// <summary>
    /// Đọc cột DataRow an toàn với NULL. Dùng thay cho ép kiểu thẳng (int)row["x"], (DateTime)row["x"]...
    /// vốn văng lỗi khi cột NULL (lỗi đã gặp ở nhiều màn hình: phiên chưa đăng xuất, thiếu ảnh, thiếu ngày sinh...).
    /// </summary>
    public static class DataRowExtensions
    {
        public static bool IsNull(this DataRow r, string col) => r[col] == DBNull.Value || r[col] == null;

        /// <summary>Chuỗi đã Trim; NULL → "" (hoặc giá trị mặc định).</summary>
        public static string Str(this DataRow r, string col, string def = "") =>
            r.IsNull(col) ? def : Convert.ToString(r[col]).Trim();

        public static int Int(this DataRow r, string col, int def = 0) => r.IsNull(col) ? def : Convert.ToInt32(r[col]);

        public static int? IntN(this DataRow r, string col) => r.IsNull(col) ? (int?)null : Convert.ToInt32(r[col]);

        public static long Long(this DataRow r, string col, long def = 0) => r.IsNull(col) ? def : Convert.ToInt64(r[col]);

        public static double Dbl(this DataRow r, string col, double def = 0) => r.IsNull(col) ? def : Convert.ToDouble(r[col]);

        public static double? DblN(this DataRow r, string col) => r.IsNull(col) ? (double?)null : Convert.ToDouble(r[col]);

        public static bool Bool(this DataRow r, string col, bool def = false) => r.IsNull(col) ? def : Convert.ToBoolean(r[col]);

        public static DateTime? Date(this DataRow r, string col) => r.IsNull(col) ? (DateTime?)null : Convert.ToDateTime(r[col]);

        public static DateTime DateOr(this DataRow r, string col, DateTime def) => r.IsNull(col) ? def : Convert.ToDateTime(r[col]);

        public static Guid? GuidN(this DataRow r, string col) => r.IsNull(col) ? (Guid?)null : (Guid)r[col];

        public static byte[] Bytes(this DataRow r, string col) => r.IsNull(col) ? null : r[col] as byte[];
    }
}
