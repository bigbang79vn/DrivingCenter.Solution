using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.Helper
{
    public class ImageToByteArray
    {
        // Hàm chuyển ảnh sang byte[]
        private byte[] ConvertImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
