using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.GPLX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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