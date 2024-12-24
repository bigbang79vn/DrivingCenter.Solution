using DevExpress.Charts.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.DAT
{
    public class ChartSoPhien
    {
        public string Day { get; set; }
        public string SoPhien { get; set; }
        public string TongSoKm { get; set; }

        public ChartSoPhien(string day, string sophien, string Sokm)
        {
            this.Day = day;
            this.SoPhien = sophien;
            this.TongSoKm = Sokm;
        }
    }
}
