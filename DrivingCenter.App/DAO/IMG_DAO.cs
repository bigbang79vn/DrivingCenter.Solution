using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.GPLX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class IMG_DAO
    {
        private static IMG_DAO instance;

        public static IMG_DAO Instance
        {
            get { if (instance == null) instance = new IMG_DAO(); return IMG_DAO.instance; }
            private set => IMG_DAO.instance = value;
        }

        public IMG_DAO()
        {
        }

        public DataTable GetImgById(string diemdanhnguoilxId)
        {
            string query = string.Format("SELECT DiemDanhNguoiLxID, AnhChup, AnhChup_Logout FROM DiemDanhNguoiLx WHERE DiemDanhNguoiLxID = '{0}'", diemdanhnguoilxId);
            DataTable dt = DataProviderIMG.Instance.ExcuteQuery(query);

            return dt;   
        }
        public int DeleteDataReportOld()
        {
            int status = 0;
            try
            {
                string query = string.Format("DELETE ReportCNTN");
                return status = DataProviderIMG.Instance.ExcuteNoneQuery(query);
            }
            catch
            {
                return status;
            }
        }

        public List<HvTotNghiep> GetHvTotNghiep()
        {
            List<HvTotNghiep> lst = new List<HvTotNghiep>();
            DataTable dt = DataProviderIMG.Instance.ExcuteQuery("SELECT * FROM ReportCNTN ORDER BY SoCCTN ASC");

            lst = dt.AsEnumerable().Select(item => new HvTotNghiep
            {
                Id = (int)item["Id"],
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = item["NgaySinh"].ToString(),
                GioiTinh = item["GioiTinh"].ToString(),
                HangGPLX = item["HangGPLX"].ToString(),
                ThoiGianDT = item["ThoiGianDT"] == DBNull.Value ? 0 : (int)item["ThoiGianDT"],
                NgayKG = item["NgayKG"].ToString(),
                NgayBG = item["NgayBG"].ToString(),
                XepLoaiTN = item["XepLoaiTN"].ToString(),
                SoCCTN = item["SoCCTN"].ToString(),
                SoVaoSo = item["SoVaoSo"].ToString()
            }).OrderBy(x => x.SoCCTN).ToList();
            return lst;
        }

        public void InsertDataReportCNTN(HvTotNghiep nguoiLx_CCTN)
        {
            string query = string.Format("EXEC [dbo].[usp_ReportCNTN_Insert] " +
                "@MaDK = '{0}' , @HoVaTen = N'{1}' , @NgaySinh = '{2}' , @GioiTinh = N'{3}' , @HangGPLX = N'{4}' , " +
                "@XepLoaiTN = N'{5}' , @NgayQDTN = '{6}' , @SoCCTN = '{7}' , @SoVaoSo = '{8}' , " +
                "@HoVaTenEng = '{9}' , @GioiTinhEng = '{10}' , @NgaySinhEng = '{11}' , @XepLoaiTNEng = '{12}' , @NgayQDTNEng = '{13}' , @HangGPLXEng = '{14}' ",
                nguoiLx_CCTN.MaDK, nguoiLx_CCTN.HoVaTen, nguoiLx_CCTN.NgaySinh, nguoiLx_CCTN.GioiTinh, nguoiLx_CCTN.HangGPLX,
                nguoiLx_CCTN.XepLoaiTN, nguoiLx_CCTN.NgayQDTN, nguoiLx_CCTN.SoCCTN, nguoiLx_CCTN.SoVaoSo,
                nguoiLx_CCTN.HoVaTenEng, nguoiLx_CCTN.GioiTinhEng, nguoiLx_CCTN.NgaySinhEng, nguoiLx_CCTN.XepLoaiTNEng, nguoiLx_CCTN.NgayQDTNEng, nguoiLx_CCTN.HangGPLXEng);

            DataProviderIMG.Instance.ExcuteNoneQuery(query);
        }

        public void UpdateInfo(string madk, string hoten, string hotenEng, string ngaysinh, string ngaysinhEng, string gioitinh, string gioitinhEng)
        {
            string query = string.Format("UPDATE ReportCNTN SET " +
                "HoVaTen = N'{0}', " +
                "NgaySinh = '{1}', " +
                "GioiTinh = N'{2}', " +
                "HoVaTenEng = '{3}', " +
                "NgaySinhEng = '{4}', " +
                "GioiTinhEng = '{5}' " +
                "WHERE MaDK = '{6}'",
                hoten, ngaysinh, gioitinh, hotenEng, ngaysinhEng, gioitinhEng, madk);
            DataProviderIMG.Instance.ExcuteNoneQuery(query);
        }

        public DataTable GetDataPrintCCTN(string lstId)
        {
            string query = string.Format("SELECT * FROM ReportCNTN WHERE Id IN (SELECT itemID FROM Split('{0}', ',')) ORDER BY SoCCTN ASC", lstId);
            DataTable dt = DataProviderIMG.Instance.ExcuteQuery(query);
            return dt;
        }

        public DataTable GetDataTheHocVien(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDaTaHV] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return DataProviderIMG.Instance.ExcuteQuery(query);
        }

        public DataTable GetDataPhieuTheoDoi(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDataPhieuTheoDoi] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return DataProviderIMG.Instance.ExcuteQuery(query);
        }

        public DataTable GetDataTemTheDAT(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDaTaHvInTemDat] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return DataProviderIMG.Instance.ExcuteQuery(query);
        }
    }
}