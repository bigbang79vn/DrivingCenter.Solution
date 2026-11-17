using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ghi log mọi lỗi không bắt được (lỗi trên luồng giao diện và luồng nền) để chẩn đoán trên máy thật
            Application.ThreadException += (s, e) =>
            {
                Exception real = Helper.ErrorLog.Unwrap(e.Exception);
                string file = Helper.ErrorLog.Write("Application.ThreadException", e.Exception);
                DevExpress.XtraEditors.XtraMessageBox.Show($"Đã xảy ra lỗi.\n\nChi tiết: {real.GetType().Name}: {real.Message}"
                    + (file == null ? "" : $"\n\nĐã ghi log: {file}"), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                Helper.ErrorLog.Write("AppDomain.UnhandledException", e.ExceptionObject as Exception ?? new Exception(Convert.ToString(e.ExceptionObject)));

            // Máy cabin chạy: DrivingCenter.App.exe --cabin-client
            if (args.Any(a => a.Equals("--cabin-client", StringComparison.OrdinalIgnoreCase)))
            {
                Application.Run(new frmCabinClient());
                return;
            }

            Application.Run(new frmLogin());
        }
    }
}
