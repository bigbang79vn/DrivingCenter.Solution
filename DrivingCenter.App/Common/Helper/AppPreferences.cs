using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DrivingCenter.App.Helper
{
    /// <summary>
    /// Lưu các tùy chọn giao diện của người dùng (VD: chế độ tối) dạng key=value
    /// trong %AppData%\DrivingCenter\preferences.txt. Lỗi đọc/ghi được bỏ qua để không chặn app.
    /// </summary>
    public static class AppPreferences
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrivingCenter", "preferences.txt");

        public static bool GetBool(string key, bool defaultValue = false)
        {
            return Load().TryGetValue(key, out string value) && bool.TryParse(value, out bool result)
                ? result
                : defaultValue;
        }

        public static void SetBool(string key, bool value)
        {
            var values = Load();
            values[key] = value.ToString();
            Save(values);
        }

        public static string GetString(string key, string defaultValue = null)
        {
            return Load().TryGetValue(key, out string value) && !string.IsNullOrEmpty(value) ? value : defaultValue;
        }

        public static void SetString(string key, string value)
        {
            var values = Load();
            values[key] = (value ?? string.Empty).Replace("\r", " ").Replace("\n", " ");
            Save(values);
        }

        private static Dictionary<string, string> Load()
        {
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                if (!File.Exists(FilePath)) return values;
                foreach (string line in File.ReadAllLines(FilePath))
                {
                    int index = line.IndexOf('=');
                    if (index > 0)
                        values[line.Substring(0, index).Trim()] = line.Substring(index + 1).Trim();
                }
            }
            catch
            {
                // File hỏng hoặc không đọc được: dùng giá trị mặc định
            }
            return values;
        }

        private static void Save(Dictionary<string, string> values)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                File.WriteAllLines(FilePath, values.Select(kv => kv.Key + "=" + kv.Value));
            }
            catch
            {
                // Không lưu được tùy chọn cũng không ảnh hưởng chức năng chính
            }
        }
    }
}
