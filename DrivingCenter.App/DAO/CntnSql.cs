using DrivingCenter.App.DTO.GPLX;

namespace DrivingCenter.App.DAO
{
    /// <summary>
    /// Câu lệnh chứng chỉ tốt nghiệp (CNTN) dùng chung cho GPLX_DAO và GPLX_CDB_CSDT_DAO (hai CSDL cùng cấu trúc).
    /// Tham số null được đổi thành chuỗi rỗng để giữ nguyên hành vi của bản ghép chuỗi cũ.
    /// </summary>
    internal static class CntnSql
    {
        public const string UpdateHoSo =
            "UPDATE NguoiLX_HoSo SET SoGiayCNTN = @SoGiayCNTN, TT_XuLy = @TT_XuLy, SoQuyetDinhTN = @SoQuyetDinhTN, " +
            "NgayRaQDTN = @NgayRaQDTN, SoSoTN = @SoSoTN, NgayVaoSoTN = @NgayVaoSoTN, NgayInGiayTN = @NgayInGiayTN WHERE MaDK = @MaDK";

        public const string InsertReport = "EXEC [dbo].[VNDC_ReportCNTN_Insert] @MaDK = @MaDK, @NgayQDTN = @NgayQDTN";

        public const string UpdateInfo =
            "UPDATE VNDC_ReportCNTN SET HoVaTen = @HoVaTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, " +
            "HoVaTenEng = @HoVaTenEng, NgaySinhEng = @NgaySinhEng, GioiTinhEng = @GioiTinhEng, SoVaoSo = @SoVaoSo WHERE MaDK = @MaDK";

        public const string SelectPrint =
            "SELECT * FROM VNDC_ReportCNTN WHERE MaDK IN (SELECT itemID FROM dbo.VNDC_Split(@lstMaDK, ',')) ORDER BY Id ASC";

        private static string S(string v) => v ?? string.Empty;

        public static object HoSoParams(NguoiLX_HoSo_Update u) => new
        {
            SoGiayCNTN = S(u.SoGiayCNTN),
            TT_XuLy = S(u.TT_XuLy),
            SoQuyetDinhTN = S(u.SoQuyetDinhTN),
            NgayRaQDTN = S(u.NgayRaQDTN),
            SoSoTN = S(u.SoSoTN),
            NgayVaoSoTN = S(u.NgayVaoSoTN),
            NgayInGiayTN = S(u.NgayInGiayTN),
            MaDK = S(u.MaDK)
        };

        public static object InsertParams(HvTotNghiep hv) => new { MaDK = S(hv.MaDK), NgayQDTN = S(hv.NgayQDTN) };

        public static object InfoParams(string madk, string hoten, string hotenEng, string ngaysinh, string ngaysinhEng,
                                        string gioitinh, string gioitinhEng, string sovaoso) => new
        {
            HoVaTen = S(hoten),
            NgaySinh = S(ngaysinh),
            GioiTinh = S(gioitinh),
            HoVaTenEng = S(hotenEng),
            NgaySinhEng = S(ngaysinhEng),
            GioiTinhEng = S(gioitinhEng),
            SoVaoSo = S(sovaoso),
            MaDK = S(madk)
        };
    }
}
