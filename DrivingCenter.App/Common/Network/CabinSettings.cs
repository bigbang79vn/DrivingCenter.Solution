using System.Configuration;

namespace DrivingCenter.App.Network
{
    /// <summary>
    /// Cấu hình kết nối cabin trong App.config (appSettings).
    /// Máy quản lý chỉ cần CabinPort; mỗi máy cabin cần thêm CabinManagerHost và CabinIndex (1-4).
    /// </summary>
    public static class CabinSettings
    {
        public static string ManagerHost => Get("CabinManagerHost", "127.0.0.1");

        public static int Port => int.TryParse(Get("CabinPort", null), out int port) ? port : 9900;

        public static int CabinIndex => int.TryParse(Get("CabinIndex", null), out int index) && index > 0 ? index : 1;

        private static string Get(string key, string fallback)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        }
    }
}
