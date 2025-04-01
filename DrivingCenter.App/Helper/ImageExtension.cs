using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.Helper
{
    public class ImageExtension
    {
        public Image Base64ToImage(string base64Image)
        {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64Image)))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null || byteArrayIn.Length == 0)
            {
                throw new ArgumentException("⚠ Dữ liệu ảnh không hợp lệ: Mảng byte rỗng hoặc null.");
            }

            try
            {
                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("⚠ Dữ liệu không phải là hình ảnh hợp lệ hoặc bị hỏng.");
            }
            catch (OutOfMemoryException)
            {
                throw new OutOfMemoryException("⚠ Hình ảnh quá lớn hoặc bị lỗi, không thể tải vào bộ nhớ.");
            }
        }
    }
}