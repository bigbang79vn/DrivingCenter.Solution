using System;

namespace DrivingCenter.App.DTO.DAT
{
    public class XeTapLai
    {
        public int VehicleID { get; set; }
        public string BienSoXe { get; set; }
        public string Imei { get; set; }
        public string SerialNumber { get; set; }
        public string Version { get; set; }
        public string MaXe { get; set; }
        public string HangGPLX { get; set; }
        public int NamSX { get; set; }
        public string SoGPXTL { get; set; }
        public DateTime NgayCapGPXTL { get; set; }
        public DateTime NgayHHXTL { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ThoiDiem { get; set; }
        public double VanToc { get; set; }
        public bool TrangThai { get; set; }
        public bool Active { get; set; }
        public string ChuSoHuu { get; set; }
        public bool ThanhLy { get; set; }
        public DateTime? SuDungDen { get; set; }

        public bool LoginCCCD { get; set; }
        public bool LoginOline { get; set; }
        public bool ShowInfoClass { get; set; }
        public bool Accumulation { get; set; }
        public bool CheckTeacher { get; set; }
        public bool CheckCar { get; set; }
        public bool CheckTimeTeacher { get; set ; }
        public bool OptimizeFace { get; set; }
    }
}