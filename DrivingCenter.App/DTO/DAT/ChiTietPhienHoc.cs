using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class ChiTietPhienHoc
    {
        public Guid Id { get; set; }
        public DateTime ThoiDiem { get; set; }
        public double VanToc { get; set; }
        public double QuangDuong { get; set; }
        public double ThoiGian { get; set; }
        public string ThoiGianText { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool XacThuc { get; set; }
        public byte[] AnhXacThuc { get; set; }

    }
}
