using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.Cabin
{
    public class KhoaHoc
    {
        public int MonthID { get; set; }
        public int KhoaHocID { get; set; }
        public string MaKhoaHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public DateTime NgayKhaiGiang { get; set; }
        public DateTime NgayBeGiang { get; set; }
        public DateTime NgaySatHach { get; set; }
        public DateTime CreateDate { get; set; }
        public string MaBC1 { get; set; }
        public string HangDaoTao { get; set; }
        public DateTime NgayDaoTao { get; set; }
        public string LoaiDaoTao { get; set; }
        public bool Status { get; set; }
    }
}