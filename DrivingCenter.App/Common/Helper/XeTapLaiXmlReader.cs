using DrivingCenter.App.DTO.DAT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DrivingCenter.App.Helper
{
    /// <summary>
    /// Đọc file XML danh sách xe tập lái (DSXTL) do Sở gửi và chuẩn hóa biển số / hạng xe.
    /// </summary>
    public static class XeTapLaiXmlReader
    {
        /// <summary>Đọc danh sách xe trong file; biển số lặp thì lấy dòng cuối.</summary>
        public static List<XeTapLaiXml> Read(string path)
        {
            XElement root = XDocument.Load(path).Root;
            if (root == null || root.Name.LocalName != "DANH_SACH_XE_TAP")
                throw new FormatException("File không phải danh sách xe tập lái (thiếu thẻ DANH_SACH_XE_TAP).");

            var list = new List<XeTapLaiXml>();
            foreach (XElement x in root.Element("XETAPS")?.Elements("XETAP") ?? Enumerable.Empty<XElement>())
            {
                string ma = NormalizeMaXe(Str(x, "BIENSOXE"));
                if (string.IsNullOrEmpty(ma)) continue;
                string hangGplx = Str(x, "HANGGPLXXE");
                list.Add(new XeTapLaiXml
                {
                    MaXe = ma,
                    BienSoXe = FormatBienSo(ma),
                    MaCSDT = Str(x, "MACSDT"),
                    SoDK = Str(x, "SODK"),
                    HangGPLXXe = hangGplx,
                    HangDaoTao = HangDaoTaoTuGplx(hangGplx),
                    NamSX = int.TryParse(Str(x, "NAMSX"), out int nam) ? nam : (int?)null,
                    SoGPXTL = Str(x, "SOGPXTL"),
                    CoQuanCapGPXTL = Str(x, "COQUANCAPGPXTL"),
                    NgayCapGPXTL = ParseNgay(Str(x, "NGAYCAPGPXTL")),
                    NgayHHGPXTL = ParseNgay(Str(x, "NGAYHHGPXTL")),
                    NhanHieu = Str(x, "NHANHIEU"),
                    MauXe = Str(x, "MAUXE"),
                    TrangThai = int.TryParse(Str(x, "TRANGTHAI"), out int tt) ? tt : (int?)null
                });
            }
            return list.GroupBy(x => x.MaXe).Select(g => g.Last()).ToList();
        }

        /// <summary>Ngày trong file dạng yyyyMMdd; sai định dạng → null.</summary>
        public static DateTime? ParseNgay(string yyyyMMdd) =>
            DateTime.TryParseExact(yyyyMMdd, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d)
                ? d : (DateTime?)null;

        /// <summary>Biển số không dấu, in hoa: "47A-042.27" → "47A04227".</summary>
        public static string NormalizeMaXe(string bienSo) =>
            Regex.Replace(bienSo ?? "", @"[^0-9A-Za-z]", "").ToUpperInvariant();

        /// <summary>"47A04227" → "47A-042.27"; "47LD1234" → "47LD-1234" (giống cách ghi BienSoXe trên DAT).</summary>
        public static string FormatBienSo(string maXe)
        {
            // Ưu tiên đuôi 5 số (47A + 04227); không có mới thử đuôi 4 số
            var m = Regex.Match(maXe ?? "", @"^(\d{2}[A-Z]{1,2})(\d{5})$");
            if (m.Success)
                return m.Groups[1].Value + "-" + m.Groups[2].Value.Substring(0, 3) + "." + m.Groups[2].Value.Substring(3);
            m = Regex.Match(maXe ?? "", @"^(\d{2}[A-Z]{1,2}\d?)(\d{4})$");
            return m.Success ? m.Groups[1].Value + "-" + m.Groups[2].Value : maXe;
        }

        /// <summary>Hạng GPLX của Sở → hạng đào tạo trên DAT: B11 (số tự động) → B.01, C1 → C1, còn lại → B.</summary>
        public static string HangDaoTaoTuGplx(string hangGplx)
        {
            switch ((hangGplx ?? "").Trim().ToUpperInvariant())
            {
                case "B11": return "B.01";
                case "C1": return "C1";
                default: return "B";
            }
        }

        private static string Str(XElement x, string name) => ((string)x.Element(name) ?? "").Trim();
    }
}
