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
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);

            //Lưu hình vào path của mình ví dụ D:\\anh.jpg, png

            //if (byteArrayIn != null && byteArrayIn.Length > 0)
            //{
            //    string savePath = @"D:\New\a.jpg";
            //    // Lưu dữ liệu ảnh vào file
            //    File.WriteAllBytes(savePath, byteArrayIn);
            //}

            return returnImage;
        }
    }
}