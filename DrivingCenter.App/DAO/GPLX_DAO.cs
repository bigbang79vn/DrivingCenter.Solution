using DevExpress.Pdf.Native.BouncyCastle.Asn1.GM;
using DrivingCenter.App.DTO.GPLX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

        public List<KhoaHoc> GetlstKh()
        {
            string query = @"SELECT 
                    MaKH + ' - ' + TenKH + 
                        CASE 
                            WHEN NgayKG IS NOT NULL AND CHARINDEX('DB', MaKH) <= 0 THEN
                                N' (ngày KG: ' + CONVERT(VARCHAR(10), NgayKG, 103) + ')'
                            ELSE
                                ''
                        END AS KhoaHoc,
                    MaKH,
                    TenKH,
                    HangGPLX,
                    HangDT,
                    CONVERT(VARCHAR(10), NgayKG, 103) AS NgayKG,
                    CONVERT(VARCHAR(10), NgayBG, 103) AS NgayBG
                FROM 
                    KhoaHoc
                WHERE 
                    MaKH NOT IN (SELECT MaKH FROM BaoCaoI) AND TongSoHV IS NOT NULL";

            DataTable dt = DataProviderGPLX.Instance.ExecuteQuery(query);
            var lstKH = dt.AsEnumerable()
                           .Select(row => new KhoaHoc
                           {
                               KhoaHocs = row.Field<string>("KhoaHoc"),
                               MaKH = row.Field<string>("MaKH"),
                               TenKH = row.Field<string>("TenKH"),
                               HangGPLX = row.Field<string>("HangGPLX"),
                               HangDT = row.Field<string>("HangDT"),
                               NgayKG = DateTime.TryParse(row.Field<string>("NgayKG"), out DateTime ngaykg) ? ngaykg : DateTime.MinValue,
                               NgayBG = DateTime.TryParse(row.Field<string>("NgayBG"), out DateTime ngaybg) ? ngaybg : DateTime.MinValue,
                           }).ToList();
            return lstKH;
        }

        public List<KhoaHoc> GetAllKhoaHoc()
        {
            // Lấy danh sách khóa học
            var dt1 = DataProviderGPLX.Instance.ExecuteQuery($"SELECT MaKH, TenKH, HangGPLX, HangDT, NgayKG, NgayBG FROM KhoaHoc WHERE YEAR(NgayKG) >= 2025");
            var lstKH = dt1.AsEnumerable()
                           .Select(row => new KhoaHoc{
                               MaKH = row.Field<string>("MaKH"),
                               TenKH = row.Field<string>("TenKH"),
                               HangGPLX = row.Field<string>("HangGPLX"),
                               HangDT = row.Field<string>("HangDT"),
                               NgayKG = row.Field<DateTime>("NgayKG"),
                               NgayBG = row.Field<DateTime>("NgayBG")
                           })
                           .ToList();
            return lstKH;
        }
        public DataRow KhoaHocSelected(string MaKH)
        {
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

        #region Xếp Lớp
        public List<NguoiLX> Get_lstNguoiLxByMaKh(string MaKH)
        {
            List<NguoiLX> lst = new List<NguoiLX>();
            string query = @"select * from NguoiLX AS nlx
                        INNER JOIN NguoiLX_HoSo AS hs ON nlx.MaDK = HS.MaDK
                        INNER JOIN KhoaHoc AS kh ON hs.MaKhoaHoc = kh.MaKH
                        where kh.MaKH = '66015K22DB001'";

            DataTable dt = DataProviderGPLX.Instance.ExecuteQuery(query);
            lst = dt.AsEnumerable().Select(item => new NguoiLX
            {
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = item["NgaySinh"].ToString(),
                NgaySinhDateTime = DateTime.ParseExact(item["NgaySinh"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture),
                GioiTinh = item["GioiTinh"].ToString() == "F" ? "Nữ" : "Nam",
                SoCMT = item["SoCMT"].ToString(),
                NgayCapCMT = item["NgayCapCMT"].ToString().Substring(0, 10)
            }).OrderBy(x => x.MaDK).ToList();

            return lst;
        }

        #endregion
    }
}