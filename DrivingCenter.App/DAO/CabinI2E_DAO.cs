using DevExpress.CodeParser;
using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class CabinI2E_DAO
    {
        private static CabinI2E_DAO instance;

        public static CabinI2E_DAO Instance
        {
            get { if (instance == null) instance = new CabinI2E_DAO(); return CabinI2E_DAO.instance; }
            private set => CabinI2E_DAO.instance = value;
        }

        public CabinI2E_DAO()
        {
        }

        public List<KhoaHoc> GetAllKhoaHoc()
        {
            List<KhoaHoc> lstKhoaHoc = new List<KhoaHoc>();
            string query = "SELECT KhoaHocID, MaKH, TenKH, NgayKG, NgayBG, MaBC1, HangDaoTao, LoaiDaoTao FROM KhoaHoc";
            DataTable dt = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            lstKhoaHoc = dt.AsEnumerable().Select(item => new KhoaHoc
            {
                KhoaHocID = (int)item["KhoaHocID"],
                MaKhoaHoc = (string)item["MaKH"],
                TenKhoaHoc = (string)item["TenKH"],
                NgayKhaiGiang = (DateTime)item["NgayKG"],
                NgayBeGiang = (DateTime)item["NgayBG"],
                MaBC1 = (string)item["MaBC1"],
                HangDaoTao = (string)item["HangDaoTao"],
                LoaiDaoTao = (int)item["LoaiDaoTao"] == 1 ? "Chưa Đào Tạo" : (int)item["LoaiDaoTao"] == 2 ? "Đang Đào Tạo" : "Đã Đào Tạo"
            }).ToList();

            return lstKhoaHoc;
        }

        public List<KhoaHoc> GetlstKhoaHocDDT()
        {
            List<KhoaHoc> lstKhoaHoc = GetAllKhoaHoc()
                .Where(x => x.LoaiDaoTao == "Đang Đào Tạo")
                .OrderBy(c => c.TenKhoaHoc)
                .ToList();

            return lstKhoaHoc;
        }

        public List<Int64> GetlstNguoiLxID(int IdKhoaHoc)
        {
            List<Int64> lst = new List<Int64>();

            string query = string.Format("SELECT NguoiLXID FROM NguoiLX WHERE KhoaHocID = {0}", IdKhoaHoc);

            DataTable data = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Int64 NguoiLxID = Convert.ToInt64(item["NguoiLXID"]);
                lst.Add(NguoiLxID);
            }
            return lst;
        }

        public void UpdateTimeExerToNguoiLx(Int64 NguoilxId)
        {
            string query = "sp_UpdateTimeExer @ID_HocVien";
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query, new object[] { NguoilxId });
        }

        public List<HocVien> GetlstHocVienByIdKhoaHoc(int IdKhoaHoc)
        {
            string query = string.Format("SELECT * " +
                "FROM v_LstHocVienByKH " +
                "WHERE KhoaHocID = {0} " +
                "ORDER BY GiaoVienID ASC", IdKhoaHoc);

            DataTable data = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            List<HocVien> lstStudent = data.AsEnumerable().Select(item => new HocVien
            {
                MaDK = item["MaDK"].ToString(),
                NguoiLxID = (long)item["NguoiLXID"],
                HoTenHV = item["HoVaTen"].ToString(),
                NgaySinh = (DateTime)item["NgaySinh"],
                GioiTinh = item["GioiTinh"].ToString(),
                SoCMT = item["SoCMT"].ToString(),
                NgayCapCMT = (DateTime)item["NgayCapCMT"],
                HangGPLX = item["HangGPLX"].ToString(),
                TenKH = item["TenKH"].ToString(),
                ToTalSecond = item["TongThoiGianPhien"].ToString(),
                TenGV = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString()
            }).ToList();

            return lstStudent;
        }

        public string CheckDK(Int64? NguoiLxID)
        {
            string query = "sp_KTDieuKien @Id_HocVien = " + "'" + NguoiLxID + "'";
            string KetLuan = DataProviderCaBinI2E.Instance.ExcuteScalar(query).ToString();
            return KetLuan;
        }

        public List<ThongKe> LstThongKe(string day)
        {
            string query = $"EXEC [dbo].[spTongHopByDay] @Day = N'{day}'";

            DataTable dt = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            List<ThongKe> lst = new List<ThongKe>();
            lst = dt.AsEnumerable().Select(item => new ThongKe
            {
                NguoiLxID = Convert.ToInt64(item["NguoiLxID"]),
                TenGV = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString(),
                TenKhoaHoc = item["TenKH"].ToString(),
                CabinName = item["CabinName"].ToString(),
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = item["NgaySinh"].ToString(),
                SoCMT = item["SoCMT"].ToString(),
                TongThoiGian = item["TongThoiGian"].ToString(),
                TotalHour = Convert.ToInt64(item["TotalSecond"]),
                Result = Convert.ToBoolean(item["KetLuan"])
            }).ToList();

            return lst;
        }

        public HocVien GetInfoHVByNguoiLxID(Int64? NguoiLxID)
        {
            string query = string.Format("SELECT * FROM [I2ECABIN3D].[dbo].[v_LstHocVienByKH] WHERE NguoiLXID = '{0}'", NguoiLxID);

            DataTable dt = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            List<HocVien> lstStudent = dt.AsEnumerable().Select(item => new HocVien
            {
                MaDK = item["MaDK"].ToString(),
                NguoiLxID = (long)item["NguoiLXID"],
                HoTenHV = item["HoVaTen"].ToString(),
                NgaySinh = (DateTime)item["NgaySinh"],
                GioiTinh = item["GioiTinh"].ToString(),
                SoCMT = item["SoCMT"].ToString(),
                NgayCapCMT = (DateTime)item["NgayCapCMT"],
                HangGPLX = item["HangGPLX"].ToString(),

                Anh = (byte[])item["Anh"],
                ToTalSecond = item["TongThoiGianPhien"].ToString(),
                TongNoiDungDT = (int)item["TongNoiDungDT"],
                TenGV = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString()
            }).ToList();

            return lstStudent.FirstOrDefault();
        }

        public int CountBaiTap(Int64? NguoiLxID)
        {
            string query = string.Format("SELECT count(*) FROM " +
                "(SELECT MIN(BaiHocCaBinID) AS ID_BaiTap FROM DiemDanhNguoiLx where NguoiLxID = {0} and BaiHocCaBinID > 0 and Datediff(s, ThoiDiemBatDau, ThoiDiemKetThuc) > 60 GROUP BY BaiHocCaBinID) t", NguoiLxID);
            int TongBaiTap = (int)DataProviderCaBinI2E.Instance.ExcuteScalar(query);
            return TongBaiTap;
        }

        public List<Result> GetResultByNguoiLxID(Int64? NguoiLxID)
        {
            string query = string.Format("SELECT tb.TenThietBi, kq.DiemDanhNguoiLxID, kq.BaiHocCaBinID, bt.TenBaiTapCabin, kq.ThoiDiemBatDau, kq.ThoiDiemKetThuc, QuangDuong, PathVideo, CONVERT(varchar, DATEADD(second, kq.TongThoiGianPhien, 0), 108) as TongThoiGianPhien, kq.TongThoiGianPhien as TotalSecond, CreateDate " +
                "FROM DiemDanhNguoiLx as kq, BaiTapCaBin as bt, ThietBi as tb " +
                "WHERE kq.BaiHocCaBinID = bt.BaiTapCaBinID and tb.ThietBiID = kq.ThietBiID and NguoiLxID = {0} " +
                "ORDER BY ThoiDiemBatDau", NguoiLxID);

            DataTable dt = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            List<Result> lst = new List<Result>();

            lst = dt.AsEnumerable().Select(item => new Result
            {
                NameCabin = item["TenThietBi"].ToString(),
                DiemDanhNguoiLxID = (long)item["DiemDanhNguoiLxID"],
                BaiHocCaBinID = (int)item["BaiHocCaBinID"],
                TenBaiTapCaBin = item["TenBaiTapCaBin"].ToString(),
                ThoiDiemBatDau = (DateTime)item["ThoiDiemBatDau"],
                ThoiDiemKetThuc = (DateTime)item["ThoiDiemKetThuc"],
                TongThoiGianPhien = item["TongThoiGianPhien"].ToString(),
                TotalSecond = (double)item["TotalSecond"],
                QuangDuong = (double)item["QuangDuong"],
                PathVideo = item["PathVideo"].ToString(),
                CreateDate = (DateTime)item["CreateDate"]
            }).ToList();

            return lst;
        }

        public List<Exercise> GetListBaiTap()
        {
            string query = "SELECT BaiTapCaBinID, TenBaiTapCaBin FROM BaiTapCaBin";
            DataTable data = DataProviderCaBinI2E.Instance.ExcuteQuery(query);

            List<Exercise> lst = new List<Exercise>();

            lst = data.AsEnumerable().Select(item => new Exercise
            {
                ID = (int)item["BaiTapCaBinID"],
                TenBaiTap = (string)item["TenBaiTapCaBin"],
            }).ToList();

            return lst;
        }

        public void UpdateBaiTap(int IdBaiTap, Int64 diemdanhnguoilxId)
        {
            string query = string.Format("UPDATE DiemDanhNguoiLx SET BaiHocCaBinID = {0} WHERE DiemDanhNguoiLxID = {1}", IdBaiTap, diemdanhnguoilxId);
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query);
        }

        public void DeleteResultExcer(Int64 DiemdanhnguoilxID)
        {
            string querydeltediemdanhnguoilx = string.Format("DELETE DiemDanhNguoiLx WHERE DiemDanhNguoiLxID =  {0}", DiemdanhnguoilxID);
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(querydeltediemdanhnguoilx);

            string querydeleteSK = string.Format("DELETE SuKienHoc WHERE DiemDanhNguoiLxID = {0}", DiemdanhnguoilxID);
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(querydeleteSK);
        }

        public void InsertTeacherNew(GiaoVien GV)
        {
            string query = string.Format("INSERT INTO [dbo].[GiaoVien] " +
                "([GiaoVienID], [MaGV],[HoTenDem],[TenGV],[NgaySinh],[SoCMT],[HangGPLX],[SuDung]) " +
                "VALUES " +
                "('{0}' , '{1}' , N'{2}' , N'{3}', '{4}', '{5}', '{6}' ,'1')", GV.GiaoVienID, GV.MaGV, GV.HoTenDem, GV.TenGV, GV.NgaySinh.ToString("yyyy-MM-dd HH:mm:ss.fff"), GV.SoCMT, GV.HangGPLX);
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query);
        }

        public void UpdateGv(int GiaoVienID, string MaDK)
        {
            string query = string.Format("UPDATE NguoiLX set GiaoVienID = '{0}' where MaDK = '{1}'", GiaoVienID, MaDK);
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query);
        }

        public string GetLstNguoiLxID(int khoahocID)
        {
            string query = string.Format("EXEC VNDC_spGetLstNguoiLxID @KhoaHocID = {0}", khoahocID);
            string LstNguoiLxID = DataProviderCaBinI2E.Instance.ExcuteScalar(query).ToString();
            return LstNguoiLxID;
        }

        public void UpdateStatusKhoaHoc(string tenkhoahoc, int loaidaotao, string makhoahoc)
        {
            string query = string.Format("UPDATE KhoaHoc SET TenKH = '{0}', LoaiDaoTao = '{1}' WHERE MaKH = '{2}'", tenkhoahoc, loaidaotao, makhoahoc);
            DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query);
        }

        #region Sync GV for Cabin

        public List<GvCabin> GetListGvByNameKH(string TenKH)
        {
            string query = string.Format("select MaDK, GiaoVienID from NguoiLX where TenKH like '%{0}%' ORDER BY GiaoVienID", TenKH);
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);
            List<GvCabin> lst = new List<GvCabin>();

            lst = dt.AsEnumerable().Select(item => new GvCabin
            {
                MaDK = (string)(item)["MaDK"],
                GiaoVienID = (int)(item)["GiaoVienID"]
            }).ToList();

            return lst;
        }

        public List<GiaoVien> GetAllGV()
        {
            List<GiaoVien> lstGiaoVien = new List<GiaoVien>();
            string query = "SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX FROM GiaoVien";
            DataTable dt = DataProviderDAT.Instance.ExcuteQuery(query);

            lstGiaoVien = dt.AsEnumerable().Select(item => new GiaoVien
            {
                GiaoVienID = (int)item["GiaoVienID"],
                MaGV = (string)item["MaGV"],
                HoTenDem = (string)item["HoTenDem"],
                TenGV = (string)item["TenGV"],
                HoTenGV = (string)item["HoTenDem"] + " " + (string)item["TenGV"],
                NgaySinh = (DateTime)item["NgaySinh"],
                SoCMT = (string)item["SoCMT"],
                HangGPLX = (string)item["HangGPLX"],
            }).ToList();

            return lstGiaoVien;
        }

        #endregion Sync GV for Cabin

        public List<Cabin> GetLstCabin()
        {
            DataTable dt = new DataTable();
            string query = "SELECT ThietBiID, TenThietBi FROM ThietBi";
            dt = DataProviderCaBinI2E.Instance.ExcuteQuery(query);
            List<Cabin> lstCabin = dt.AsEnumerable().Select(item => new Cabin
            {
                IdThietBi = (int)item["ThietBiID"],
                NameCabin = (string)item["TenThietBi"]
            }).ToList();
            return lstCabin;
        }

        public double GetTongThoiGianByNguoiLxID(Int64 NguoiLxID)
        {
            try
            {
                string query = string.Format("SELECT SUM(TongThoiGianPhien) as  TongThoiGianPhien FROM DiemDanhNguoiLx  WHERE NguoiLxID = {0}", NguoiLxID);
                return (double)DataProviderCaBinI2E.Instance.ExcuteScalar(query);
            }
            catch
            {
                return 0;
            }
        }

        public Int64 GetMaxDiemdanhnguoilxByNguoiLxID(Int64 NguoiLxID)
        {
            try
            {
                string query = string.Format("SELECT MAX(DiemDanhNguoiLxID) FROM DiemDanhNguoiLx WHERE NguoiLxID = {0}", NguoiLxID);
                return Convert.ToInt64(DataProviderCaBinI2E.Instance.ExcuteScalar(query));
            }
            catch
            {
                return 0;
            }
        }

        public int GetDayDiemdanhnguoilxByNguoiLxID(Int64 NguoiLxID)
        {
            try
            {
                string query = string.Format("SELECT DISTINCT MAX(day(ThoiDiemBatDau)) as NgayTap FROM DiemDanhNguoiLx WHERE NguoiLxID = {0}", NguoiLxID);
                return (int)DataProviderCaBinI2E.Instance.ExcuteScalar(query);
            }
            catch
            {
                return 0;
            }
        }

        public DateTime GetTimeOut(Int64 diemdanhnguoilxId, Int64 NguoiLxID)
        {
            string query = string.Format("select ThoiDiemKetThuc from DiemDanhNguoiLx where DiemDanhNguoiLxID = {0} AND NguoiLxID = {1}", diemdanhnguoilxId, NguoiLxID);

            DateTime timeout = (DateTime)DataProviderCaBinI2E.Instance.ExcuteScalar(query);
            return timeout;
        }

        public int InsertData(Result r, Int64 nguoiLxId, int IdThietBi, int IDKhoaHoc)
        {
            string query = "VNDC_Insert @IdBaiTap , @NguoiLxID , @IdThietBi , @TimeIn , @ThoiGianPhien , @QuangDuong , @PathVideo , @KhoaHocId";
            int result = DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query, new object[] { r.BaiHocCaBinID, nguoiLxId, IdThietBi, r.ThoiDiemBatDau, r.TotalSecond, r.QuangDuong, r.PathVideo, IDKhoaHoc });
            return result;
        }

        #region Thêm Khóa Học

        public int InsertKhoaHoc(KhoaHoc kh)
        {
            string query = "spAddKhoaHoc @MaKH , @TenKH , @NgayKG , @MaBC1 , @NgayBG , @NgaySatHach , @HangDaoTao , @NgayDaoTao";
            int result = DataProviderCaBinI2E.Instance.ExcuteNoneQuery(query, new object[] { kh.MaKhoaHoc, kh.TenKhoaHoc, kh.NgayKhaiGiang, kh.MaBC1, kh.NgayBeGiang, kh.NgaySatHach, kh.HangDaoTao, kh.NgayDaoTao });
            return result;
        }

        public int GetKhoaHocIdByMaKhoaHoc(string MaKH)
        {
            try
            {
                string query = string.Format("SELECT KhoaHocID FROM KhoaHoc WHERE MaKH = '{0}'", MaKH);
                return (int)DataProviderCaBinI2E.Instance.ExcuteScalar(query);
            }
            catch
            {
                return 0;
            }
        }

        public int InsertNguoiLxBase(int khoahocId, string tenkhoahoc, HocVien hv, byte[] bytesImg) 
        {
            string query = "spAddBaseNguoiLx @KhoaHocID , @TenKH , @MaDK , @HoDemNLX , @TenNLX , @HoVaTen , @NgaySinh , @GioiTinh , @QuocTich , @NoiCT , @SoCMT ,  @NgayCapCMT , @HangDaoTao , @HangGPLX , @Anh , @RFID , @SoTheRFID , @IsRFID ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@KhoaHocID", SqlDbType.NVarChar) { Value = khoahocId},
                new SqlParameter("@TenKH", SqlDbType.NVarChar) { Value = tenkhoahoc },
                new SqlParameter("@MaDK", SqlDbType.NVarChar) { Value = hv.MaDK },
                new SqlParameter("@HoDemNLX", SqlDbType.NVarChar) { Value = hv.HoTenDem },
                new SqlParameter("@TenNLX", SqlDbType.NVarChar) { Value = hv.Ten },
                new SqlParameter("@HoVaTen", SqlDbType.NVarChar) { Value = hv.HoTenHV },
                new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = hv.NgaySinh },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar) { Value = hv.GioiTinh },
                new SqlParameter("@QuocTich", SqlDbType.NVarChar) { Value = hv.QuocTich },
                new SqlParameter("@NoiCT", SqlDbType.NVarChar) { Value = ""},
                new SqlParameter("@SoCMT" , SqlDbType.NVarChar) { Value = hv.SoCMT },
                new SqlParameter("@NgayCapCMT" , SqlDbType.DateTime) { Value = hv.NgayCapCMT },
                new SqlParameter("@HangDaoTao" , SqlDbType.NVarChar) { Value = hv.HangGPLX },
                new SqlParameter("@HangGPLX" , SqlDbType.NVarChar) { Value = hv.HangGPLX },
                new SqlParameter("@Anh" , SqlDbType.VarBinary) { Value = bytesImg },
                new SqlParameter("@RFID" , SqlDbType.VarChar) { Value = "" },
                new SqlParameter("@SoTheRFID" , SqlDbType.VarChar) { Value = "" },
                new SqlParameter("@IsRFID" , SqlDbType.Bit) { Value = 0 }
            };

            int result = DataProviderCaBinI2E.Instance.ExecuteNonQueryNew(query, parameters);

            return result;
        }

        #endregion Thêm Khóa Học
    }
}