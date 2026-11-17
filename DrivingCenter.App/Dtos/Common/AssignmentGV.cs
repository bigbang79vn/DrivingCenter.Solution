namespace DrivingCenter.App.DTO.Common
{
    public class AssignmentGV
    {
        public string MaDK { get; set; }
        public int GiaoVienId { get; set; }
        public string GiaoVien { get; set; }
        public string HoTenDem { get; set; }
        public string TenGV { get; set; }
        public string MaXeTap { get; set; }
        public string BienSoXe { get; set; }
    }

    /// <summary>Học viên DAT kèm giáo viên / xe đang gán (màn hình Phân công DAT).</summary>
    public class NguoiLxPhanCong
    {
        public string MaDK { get; set; }
        public string HoVaTen { get; set; }
        public string TenKH { get; set; }
        public int? GiaoVienID { get; set; }
        public string TenGiaoVien { get; set; }
        public string MaXeTap { get; set; }
    }

    public class GiaoVienTen
    {
        public int GiaoVienID { get; set; }
        public string HoTen { get; set; }
    }

    /// <summary>Xe số tự động (B.01) kèm mức đang sử dụng (màn hình Bố trí xe).</summary>
    public class XeSTDSuDung
    {
        public string MaXe { get; set; }
        public string BienSoXe { get; set; }
        public bool? SuDung { get; set; }
        public System.DateTime? SuDungDen { get; set; }
        public int SoKhoaDangDung { get; set; }
        public int SoHVDangDung { get; set; }
    }

    public class XeTapLaiMa
    {
        public string MaXe { get; set; }
        public string BienSoXe { get; set; }
    }
}
