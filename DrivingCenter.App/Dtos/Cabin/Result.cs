using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DTO.Cabin
{
    public class Result
    {
        public string NameCabin { get; set; }
        public long DiemDanhNguoiLxID { get; set; }
        public int BaiHocCaBinID { get; set; }
        public string TenBaiTapCaBin { get; set; }
        public DateTime ThoiDiemBatDau { get; set; }
        public DateTime ThoiDiemKetThuc { get; set; }
        public string TongThoiGianPhien { get; set; }
        public double TotalSecond { get; set; }
        public double QuangDuong { get; set; }
        public string PathVideo { get; set; }
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Cabin Eco: phiên ghi bằng phần mềm cabin mới (từ 21/08/2026) — bài tập, giờ kết thúc, thời lượng
        /// được mã hóa phía phần mềm cabin nên CSDL không giải mã được; chỉ đọc được cabin và giờ bắt đầu.
        /// </summary>
        public bool MaHoaMoi { get; set; }
    }
}