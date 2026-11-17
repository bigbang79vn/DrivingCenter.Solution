using Newtonsoft.Json;

namespace DrivingCenter.App.Network
{
    /// <summary>
    /// Gói tin trao đổi giữa Uc_CabinManager (máy chủ) và frmCabinClient (máy cabin).
    /// Mỗi gói là một dòng JSON.
    /// </summary>
    public class CabinMessage
    {
        // Máy cabin -> máy chủ
        public const string Hello = "HELLO";
        public const string Ping = "PING";

        // Máy chủ -> máy cabin
        public const string Assign = "ASSIGN";
        public const string Clear = "CLEAR";

        public string Type { get; set; }
        public int CabinIndex { get; set; }
        public string MachineName { get; set; }
        public string HoTen { get; set; }
        public string HangGPLX { get; set; }

        public string ToLine() => JsonConvert.SerializeObject(this, Formatting.None);

        public static CabinMessage FromLine(string line)
        {
            try { return JsonConvert.DeserializeObject<CabinMessage>(line); }
            catch (JsonException) { return null; }
        }
    }
}
