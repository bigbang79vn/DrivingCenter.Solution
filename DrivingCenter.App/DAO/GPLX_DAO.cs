using DrivingCenter.App.DTO.GPLX;
using PinApplication.DAO;
using System;
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
            string querylstKH = "SELECT MaKH , TenKH, NgayKG FROM KhoaHoc";
            DataTable dt1 = DataProviderGPLX.Instance.ExcuteQuery(querylstKH);

            List<KhoaHocGPLX> lstKH = new List<KhoaHocGPLX>();

            foreach (DataRow dr in dt1.Rows)
            {
                KhoaHocGPLX kh = new KhoaHocGPLX(dr);
                lstKH.Add(kh);
            }

            string querylstKHBC1 = "SELECT MaKH FROM BaoCaoI";
            DataTable dt2 = DataProviderGPLX.Instance.ExcuteQuery(querylstKHBC1);

            List<string> lstKHBC1 = new List<string>();

            foreach (DataRow row in dt2.Rows)
            {
                lstKHBC1.Add(row["MaKH"].ToString());
            }

            foreach (KhoaHocGPLX item in lstKH.ToList())
            {
                foreach (var item1 in lstKHBC1)
                {
                    if (item.MaKHGplx == item1)
                        lstKH.Remove(item);
                }
            }

            return lstKH;
        }

        //public List<HvTotNghiep> GetHvTotNghiep()
        //{
        //    List<HvTotNghiep> lst = new List<HvTotNghiep>();
        //    DataTable dt = DataProviderGPLX.Instance.ExcuteQuery("SELECT * FROM ReportCNTN ORDER BY SoCCTN ASC");

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
        //    DataTable dt = DataProviderGPLX.Instance.ExcuteQuery(query);
        //    return dt;
        //}

        public void UpdateResultGraduate(NguoiLX_HoSo_Update nguoiLXUpdate)
        {
            string query = string.Format("UPDATE NguoiLX_HoSo set " +
                "SoGiayCNTN = '{0}', TT_XuLy = '{1}', SoQuyetDinhTN = '{2}', NgayRaQDTN = '{3}', SoSoTN = '{4}', NgayVaoSoTN = '{5}', NgayInGiayTN = '{6}' where MaDK = '{7}'",
                nguoiLXUpdate.SoGiayCNTN, nguoiLXUpdate.TT_XuLy_Old, nguoiLXUpdate.SoQuyetDinhTN, nguoiLXUpdate.NgayRaQDTN, nguoiLXUpdate.SoSoTN, nguoiLXUpdate.NgayVaoSoTN, nguoiLXUpdate.NgayInGiayTN, nguoiLXUpdate.MaDK);
            DataProviderGPLX.Instance.ExcuteNoneQuery(query);
        }
    }
}