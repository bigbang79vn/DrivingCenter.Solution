using System;

namespace DrivingCenter.App.DTO.Cabin
{
    public enum CabinClientStatus
    {
        Offline,
        Online
    }

    /// <summary>
    /// Máy con (cabin) hiển thị trên Uc_CabinManager.
    /// </summary>
    public class CabinClientInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string MachineName { get; set; }
        public CabinClientStatus Status { get; set; }
        public DateTime? LastSeen { get; set; }
        public HocVien HocVien { get; set; }
    }

    /// <summary>
    /// Một dòng lịch sử hiển thị dưới mỗi cabin.
    /// </summary>
    public class CabinRunLog
    {
        public DateTime ThoiGian { get; set; }
        public string HocVien { get; set; }
        public string SuKien { get; set; }
    }
}
