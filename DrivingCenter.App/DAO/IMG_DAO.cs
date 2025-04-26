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
            DataTable dt = DataProviderIMG.Instance.ExecuteQuery(query);

            return dt;   
        }
        public int DeleteDataReportOld()
        {
            int status = 0;
            try
            {
                string query = string.Format("DELETE ReportCNTN");
                return status = DataProviderIMG.Instance.ExecuteNonQuery(query);
            }
            catch
            {
                return status;
            }
        }

        public List<HvTotNghiep> GetHvTotNghiep()
        {
            List<HvTotNghiep> lst = new List<HvTotNghiep>();
            DataTable dt = DataProviderIMG.Instance.ExecuteQuery("SELECT * FROM ReportCNTN ORDER BY SoCCTN ASC");

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
            string query = $@"EXEC [dbo].[usp_ReportCNTN_Insert]
                    @MaDK = '{nguoiLx_CCTN.MaDK}', 
                    @HoVaTen = N'{nguoiLx_CCTN.HoVaTen}', 
                    @NgaySinh = '{nguoiLx_CCTN.NgaySinh}',
                    @GioiTinh = N'{nguoiLx_CCTN.GioiTinh}', 
                    @HangGPLX = N'{nguoiLx_CCTN.HangGPLX}', 
                    @XepLoaiTN = N'{nguoiLx_CCTN.XepLoaiTN}', 
                    @NgayQDTN = '{nguoiLx_CCTN.NgayQDTN}',
                    @SoChungChi = '{nguoiLx_CCTN.SoChungChi}',
                    @SoCCTN = '{nguoiLx_CCTN.SoCCTN}', 
                    @SoVaoSo = '{nguoiLx_CCTN.SoVaoSo}',
                    @HoVaTenEng = '{nguoiLx_CCTN.HoVaTenEng}',
                    @GioiTinhEng = '{nguoiLx_CCTN.GioiTinhEng}', 
                    @NgaySinhEng = '{nguoiLx_CCTN.NgaySinhEng}',
                    @XepLoaiTNEng = '{nguoiLx_CCTN.XepLoaiTNEng}', 
                    @NgayQDTNEng = '{nguoiLx_CCTN.NgayQDTNEng}',
                    @HangGPLXEng = '{nguoiLx_CCTN.HangGPLXEng}'";

            DataProviderIMG.Instance.ExecuteNonQuery(query);
        }

        public void UpdateInfo(string madk, string hoten, string hotenEng, string ngaysinh, string ngaysinhEng, string gioitinh, string gioitinhEng, string sovaoso)
        {
            string query = $@"UPDATE ReportCNTN SET HoVaTen = N'{hoten}', 
                NgaySinh = '{ngaysinh}', 
                GioiTinh = N'{gioitinh}', 
                HoVaTenEng = '{hotenEng}', 
                NgaySinhEng = '{ngaysinhEng}',
                GioiTinhEng = '{gioitinhEng}',
                SoVaoSo = '{sovaoso}'
                WHERE MaDK = '{madk}'";
            DataProviderIMG.Instance.ExecuteNonQuery(query);
        }

        public DataTable GetDataPrintCCTN(string lstId)
        {
            string query = string.Format("SELECT * FROM ReportCNTN WHERE Id IN (SELECT itemID FROM Split('{0}', ',')) ORDER BY SoCCTN ASC", lstId);
            DataTable dt = DataProviderIMG.Instance.ExecuteQuery(query);
            return dt;
        }

        public DataTable GetDataTheHocVien(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDaTaHV] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return DataProviderIMG.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataPhieuTheoDoi(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDataPhieuTheoDoi] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return DataProviderIMG.Instance.ExecuteQuery(query);
        }

        public DataTable GetDataTemTheDAT(string lstNguoiLxID)
        {
            string query = $"EXEC [dbo].[usp_LoadDaTaHvInTemDat] @lstNguoiLXID = N'{lstNguoiLxID}'";
            return DataProviderIMG.Instance.ExecuteQuery(query);
        }
    }
}