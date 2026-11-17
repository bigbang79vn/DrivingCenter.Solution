using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DrivingCenter.App.Helper
{
    /// <summary>
    /// Ghi lỗi ra %AppData%\DrivingCenter\logs\error_yyyyMMdd.log kèm số handle GDI/USER và bộ nhớ,
    /// để chẩn đoán lỗi chỉ xảy ra trên máy thật sau thời gian chạy dài. Lỗi ghi log được bỏ qua.
    /// </summary>
    public static class ErrorLog
    {
        private static readonly string LogDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrivingCenter", "logs");

        private static readonly object _lock = new object();

        [DllImport("user32.dll")]
        private static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

        /// <summary>
        /// Lỗi thật bên trong: Activator / new T() / reflection bọc lỗi trong TargetInvocationException
        /// ("Exception has been thrown by the target of an invocation").
        /// </summary>
        public static Exception Unwrap(Exception ex)
        {
            while (ex is TargetInvocationException && ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

        public static string Write(string context, Exception ex)
        {
            try
            {
                string file = Path.Combine(LogDir, $"error_{DateTime.Now:yyyyMMdd}.log");
                string text = $"===== {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {context}{Environment.NewLine}"
                    + $"{ResourceInfo()}{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}";
                lock (_lock)
                {
                    Directory.CreateDirectory(LogDir);
                    File.AppendAllText(file, text);
                }
                return file;
            }
            catch
            {
                return null;
            }
        }

        public static string ResourceInfo()
        {
            try
            {
                using (var p = Process.GetCurrentProcess())
                {
                    return $"GDI={GetGuiResources(p.Handle, 0)}  USER={GetGuiResources(p.Handle, 1)}  Handles={p.HandleCount}  "
                         + $"PrivateMem={p.PrivateMemorySize64 >> 20}MB  Managed={GC.GetTotalMemory(false) >> 20}MB  "
                         + $"Uptime={(DateTime.Now - p.StartTime):hh\\:mm\\:ss}  {(Environment.Is64BitProcess ? "x64" : "x86")}";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
