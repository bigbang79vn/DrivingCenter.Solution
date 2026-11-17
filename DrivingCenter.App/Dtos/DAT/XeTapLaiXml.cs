using DrivingCenter.App.Enums;
using System;
using System.Collections.Generic;

namespace DrivingCenter.App.DTO.DAT
{
    /// <summary>
    /// Một xe (thẻ XETAP) trong file XML danh sách xe tập lái (DSXTL) do Sở gửi,
    /// kèm kết quả so sánh với bảng XeTapLai sau khi gọi XeTapLaiDAO.BuildImportPlan.
    /// </summary>
    public class XeTapLaiXml
    {
        #region Dữ liệu trong file

        public string MaXe { get; set; }          // BIENSOXE không dấu, VD 47A04227
        public string BienSoXe { get; set; }      // dạng hiển thị, VD 47A-042.27
        public string MaCSDT { get; set; }
        public string SoDK { get; set; }
        public string HangGPLXXe { get; set; }    // B1 / B11 / C1 (hạng GPLX theo Sở)
        public string HangDaoTao { get; set; }    // B / B.01 / C1 (hạng dùng trên DAT)
        public int? NamSX { get; set; }
        public string SoGPXTL { get; set; }
        public string CoQuanCapGPXTL { get; set; }
        public DateTime? NgayCapGPXTL { get; set; }
        public DateTime? NgayHHGPXTL { get; set; }
        public string NhanHieu { get; set; }
        public string MauXe { get; set; }

        /// <summary>TRANGTHAI trong file: 1 = đang hoạt động, 0 = ngừng; null nếu thẻ trống / không hợp lệ.</summary>
        public int? TrangThai { get; set; }

        public bool DangHoatDong => TrangThai == 1;

        #endregion

        #region Kết quả so sánh với CSDL

        public ImportAction HanhDong { get; set; }

        /// <summary>ID của xe trong bảng XeTapLai (null nếu xe chưa có).</summary>
        public int? IdHienTai { get; set; }

        /// <summary>Các trường thay đổi, VD "Số GPXTL: 66015/25114 → 66015/26050".</summary>
        public List<string> ThayDoi { get; } = new List<string>();

        #endregion
    }
}
