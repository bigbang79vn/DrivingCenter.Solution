using System;
using System.Linq;

namespace DrivingCenter.App.Common
{
    /// <summary>
    /// Mã hạng đào tạo / hạng GPLX. Mỗi CSDL ghi khác nhau nên luôn so bằng <see cref="Same"/>:
    /// <list type="bullet">
    /// <item>DAT (KhoaHoc.HangDaoTao, XeTapLai.HangGPLX): "B", "B.01", "C1"</item>
    /// <item>GPLX (KhoaHoc.HangDT): "B", "B.01"…; hồ sơ / mã GPLX (HangGPLX): "B1", "B11", "C1"</item>
    /// <item>Bee (tblXeTapLai.HangGPLX): "B11" cho xe số tự động</item>
    /// </list>
    /// Đã từng gây lỗi: lọc xe STĐ bằng "B01" trong khi DAT lưu "B.01" (danh sách luôn trống),
    /// so hạng khóa "B" với hạng hồ sơ "B1".
    /// </summary>
    public static class HangGplx
    {
        public const string B = "B";
        public const string B01 = "B.01";   // hạng đào tạo số tự động (DAT, GPLX)
        public const string B1 = "B1";      // mã hạng GPLX tương ứng hạng B
        public const string B11 = "B11";    // mã hạng GPLX tương ứng hạng B.01
        public const string C1 = "C1";

        /// <summary>Bỏ dấu chấm, khoảng trắng, không phân biệt hoa thường ("B.01" → "B01").</summary>
        public static string Normalize(string hang) =>
            new string((hang ?? "").Where(c => c != '.' && !char.IsWhiteSpace(c)).ToArray()).ToUpperInvariant();

        /// <summary>Cùng một mã (sau khi chuẩn hóa). Chuỗi rỗng chỉ bằng chuỗi rỗng.</summary>
        public static bool Same(string a, string b) => Normalize(a) == Normalize(b);

        /// <summary>Hạng số tự động: "B.01" / "B01" (hạng đào tạo) hoặc "B11" (mã GPLX).</summary>
        public static bool IsSoTuDong(string hang)
        {
            string n = Normalize(hang);
            return n == "B01" || n == "B11";
        }
    }
}
