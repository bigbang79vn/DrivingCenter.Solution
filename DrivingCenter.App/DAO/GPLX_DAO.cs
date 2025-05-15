using DrivingCenter.App.DTO.GPLX;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class GPLX_DAO
    {
        private static GPLX_DAO instance;

        public static GPLX_DAO Instance
        {
            get { if (instance == null) instance = new GPLX_DAO(); return GPLX_DAO.instance; }
            private set => GPLX_DAO.instance = value;
        }

        public GPLX_DAO()
        {
        }

        public List<KhoaHocGPLX> GetlstKH()
        {
            // Lấy danh sách khóa học
            var dt1 = DataProviderGPLX.Instance.ExecuteQuery("SELECT MaKH, TenKH, HangGPLX, HangDT, NgayKG, NgayBG FROM KhoaHoc WHERE MaKH NOT IN (SELECT MaKH FROM BaoCaoI)");
            var lstKH = dt1.AsEnumerable()
                           .Select(row => new KhoaHocGPLX(row))
                           .ToList();
            return lstKH;
        }

        public List<KhoaHocGPLX> GetAllKhoaHoc()
        {
            // Lấy danh sách khóa học
            var dt1 = DataProviderGPLX.Instance.ExecuteQuery($"SELECT MaKH, TenKH, HangDT, NgayKG, NgayBG FROM KhoaHoc WHERE YEAR(NgayKG) >= 2025");
            var lstKH = dt1.AsEnumerable()
                           .Select(row => new KhoaHocGPLX(row))
                           .ToList();
            return lstKH;
        }
        public DataRow KhoaHocSelected(string MaKH)
        {
            // Lấy thông tin khóa học đã chọn
            string query = $"SELECT * FROM KhoaHoc WHERE MaKH = '{MaKH}'";
            DataTable dt = DataProviderGPLX.Instance.ExecuteQuery(query);
            return dt.Rows[0];
        }

        //public List<HvTotNghiep> GetHvTotNghiep()
        //{
        //    List<HvTotNghiep> lst = new List<HvTotNghiep>();
        //    DataTable dt = DataProviderGPLX.Instance.ExecuteQuery("SELECT * FROM ReportCNTN ORDER BY SoCCTN ASC");

        //    lst = dt.AsEnumerable().Select(item => new HvTotNghiep
        //    {
        //        Id = (int)item["Id"],
        //        MaDK = item["MaDK"].ToString(),
        //        HoVaTen = item["HoVaTen"].ToString(),
        //        NgaySinh = item["NgaySinh"].ToString(),
        //        GioiTinh = item["GioiTinh"].ToString(),
        //        HangGPLX = item["HangGPLX"].ToString(),
        //        XepLoai = item["XepLoaiTN"].ToString(),
        //        SoCCTN = item["SoCCTN"].ToString(),
        //        SoVaoSo = item["SoVaoSo"].ToString()
        //    }).OrderBy(x => x.SoCCTN).ToList();
        //    return lst;
        //}

        //public DataTable GetDataPrintCCTN(string lstId)
        //{
        //    string query = string.Format("SELECT * FROM ReportCNTN WHERE Id IN (SELECT itemID FROM Split('{0}', ',')) ORDER BY SoCCTN ASC", lstId);
        //    DataTable dt = DataProviderGPLX.Instance.ExecuteQuery(query);
        //    return dt;
        //}

        public void UpdateResultGraduate(NguoiLX_HoSo_Update nguoiLXUpdate)
        {
            string query = string.Format("UPDATE NguoiLX_HoSo set " +
                "SoGiayCNTN = '{0}', TT_XuLy = '{1}', SoQuyetDinhTN = '{2}', NgayRaQDTN = '{3}', SoSoTN = '{4}', NgayVaoSoTN = '{5}', NgayInGiayTN = '{6}' where MaDK = '{7}'",
                nguoiLXUpdate.SoGiayCNTN, nguoiLXUpdate.TT_XuLy_Old, nguoiLXUpdate.SoQuyetDinhTN, nguoiLXUpdate.NgayRaQDTN, nguoiLXUpdate.SoSoTN, nguoiLXUpdate.NgayVaoSoTN, nguoiLXUpdate.NgayInGiayTN, nguoiLXUpdate.MaDK);
            DataProviderGPLX.Instance.ExecuteNonQuery(query);
        }
    }
}