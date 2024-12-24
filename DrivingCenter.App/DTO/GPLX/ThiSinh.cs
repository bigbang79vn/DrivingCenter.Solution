using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.GPLX
{
    public class ThiSinh
    {
        public int STT { get; set; }
        public int IDThiSinh { get; set; }
        public string MaDK { get; set; }
        public int SoBaoDanh { get; set; }
        public string HoVaTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMT { get; set; }
        public string HangGPLX { get; set; }
        public int DiemLT { get; set; }
        public string Result_LT { get; set; }
        public int DiemMP { get; set; }
        public string Result_MP { get; set; }
    }
}
