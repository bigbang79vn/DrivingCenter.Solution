using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.DAO
{
    public class CabinEco_DAO
    {
        private static CabinEco_DAO instance;

        public static CabinEco_DAO Instance
        {
            get { if (instance == null) instance = new CabinEco_DAO(); return CabinEco_DAO.instance; }
            private set => CabinEco_DAO.instance = value;
        }

        public CabinEco_DAO()
        {
        }

        public List<KhoaHoc> GetAllKhoaHoc()
        {
            List<KhoaHoc> lstKhoaHoc = new List<KhoaHoc>();
            string query = "SELECT ID, MaKhoaHoc, Ten, NgayKhaiGiang, NgayBeGiang, HangDaoTao, Status " +
                "FROM tblKhoaHoc " +
                "ORDER BY MaKhoaHoc ASC";
            DataTable dt = DataProviderCaBinEco.Instance.ExcuteQuery(query);

            lstKhoaHoc = dt.AsEnumerable().Select(item => new KhoaHoc
            {
                KhoaHocID = (int)item["ID"],
                MaKhoaHoc = (string)item["MaKhoaHoc"],
                TenKhoaHoc = (string)item["Ten"],
                NgayKhaiGiang = (DateTime)item["NgayKhaiGiang"],
                NgayBeGiang = (DateTime)item["NgayBeGiang"],
                HangDaoTao = (string)item["HangDaoTao"],
                Status = (bool)item["Status"],
            }).ToList();

            return lstKhoaHoc;
        }

        public List<KhoaHoc> GetKHDangDT()
        {
            List<KhoaHoc> lstKhoaHoc = new List<KhoaHoc>();

            lstKhoaHoc = GetAllKhoaHoc()
                .Where(x => x.Status == false)
                .OrderBy(C => C.TenKhoaHoc)
                .ToList();

            return lstKhoaHoc;
        }

        public List<Cabin> GetLstCabin()
        {
            string query = "SELECT ID, Name, GhiChu FROM tblThietBi";
            DataTable dt = DataProviderCaBinEco.Instance.ExcuteQuery(query);
            List<Cabin> lstCabin = new List<Cabin>();

            lstCabin = dt.AsEnumerable().Select(item => new Cabin
            {
                IdThietBi = (int)(item["ID"]),
                NameCabin = (string)(item["Name"]),
                GhiChu = (string)(item["GhiChu"])
            }).ToList();

            return lstCabin;
        }

        public List<HocVien> LoadLstHocVienById(int IdKhoaHoc)
        {
            List<HocVien> lstStudent = new List<HocVien>();

            string query = string.Format("EXEC [dbo].[spGetLstHvByKhoaHoc] @ID_KhoaHoc = {0}", IdKhoaHoc);

            DataTable dt = DataProviderCaBinEco.Instance.ExcuteQuery(query);

            lstStudent = dt.AsEnumerable().Select(item => new HocVien
            {
                MaDK = (string)item["ID"],
                HoTenHV = (string)item["HoTen"],
                SoCMT = (string)item["SoCMT"],
                ToTalSecond = (string)item["TongThoiGianText"],
                TenGV = item["GiaoVienID"] == DBNull.Value ? "" : (string)item["HoGiaoVien"] + " " + (string)item["TenGiaoVien"],
                HangGPLX = item.Field<string>("HangGPLX")
            }).ToList();

            return lstStudent;
        }

        public string CheckDK(string idhocvien)
        {
            string query = "spKTDieuKien @Id_HocVien = " + "'" + idhocvien + "'";
            var getValue = DataProviderCaBinEco.Instance.ExcuteScalar(query);
            return (getValue == null) ? string.Empty : getValue.ToString();
        }

        public List<Result> lstResultById(string idHocVien)
        {
            string query = string.Format("SELECT TB.Name AS Cabin, KQ.ID_BaiTap, ID_KetQuaTap, KQ.Name, Time_In, Time_Out, CONVERT(CHAR(8),DATEADD(second,TongThoiGian,0),108) AS ToTalSecond, LanTap " +
                "FROM v_ThongTinTap KQ, tblThietBi TB " +
                "WHERE ID_HocVien = '{0}' AND KQ.ID_ThietBi = TB.ID", idHocVien);

            DataTable dt = DataProviderCaBinEco.Instance.ExcuteQuery(query);

            List<Result> lstKetQuaTap = new List<Result>();

            lstKetQuaTap = dt.AsEnumerable().Select(item => new Result
            {
                NameCabin = (string)item["Cabin"],
                DiemDanhNguoiLxID = (int)item["ID_KetQuaTap"],
                BaiHocCaBinID = (int)item["ID_BaiTap"],
                TenBaiTapCaBin = (string)item["Name"],
                ThoiDiemBatDau = (DateTime)item["Time_In"],
                ThoiDiemKetThuc = (DateTime)item["Time_Out"],
                TongThoiGianPhien = (string)item["TotalSecond"],
            }).ToList();

            return lstKetQuaTap;
        }

        public List<Exercise> GetListBaiTap()
        {
            string query = "SELECT ID_BaiTap, Name from tblExam";
            DataTable data = DataProviderCaBinEco.Instance.ExcuteQuery(query);

            List<Exercise> lst = new List<Exercise>();

            lst = data.AsEnumerable().Select(item => new Exercise
            {
                ID = (int)item["ID_BaiTap"],
                TenBaiTap = (string)item["Name"],
            }).ToList();

            return lst;
        }

        public DataTable LoadInfoHVByMaDK(string MaDK)
        {
            string query = string.Format("SELECT hv.ID, hv.HoTen, hv.SoCMT, hv.HangDaoTao, hv.GioiTinh, hv.NgaySinh, hv.Images, kh.Ten " +
                "FROM tblHocVien AS hv INNER JOIN tblKhoaHoc AS kh ON hv.IDKhoaHoc = kh.ID " +
                "WHERE hv.ID = '{0}'", MaDK);

            DataTable data = DataProviderCaBinEco.Instance.ExcuteQuery(query);

            return data;
        }

        public string GetMaDKNew(int Id_KhoaHoc, string SoCMT)
        {
            string query = $"SELECT ID FROM tblHocVien WHERE IDKhoaHoc = {Id_KhoaHoc} AND SoCMT = '{SoCMT}'";
            var getValue = DataProviderCaBinEco.Instance.ExcuteScalar(query);
            return (getValue == null) ? string.Empty : getValue.ToString();
        }

        public int CreateResult(int idthietbi, string ghichu, string idhocvien, int idbaitap, string datecreate, int idkhoahoc, int lantap, int lantapcabin, int minute)
        {
            string query = "spResult @IdThietBi , @Cabin ,  @IdHocVien , @IdBaiTap , @DateCreate , @IdKhoaHoc , @LanTap , @LanTapCaBin , @Minute ";
            int result = DataProviderCaBinEco.Instance.ExcuteNoneQuery(query, new object[] { idthietbi, ghichu, idhocvien, idbaitap, datecreate, idkhoahoc, lantap, lantapcabin, minute });
            return result;
        }

        public int GetIdMaxByIdHocVien(string IdHocVien)
        {
            string query = "select MAX(ID) from tblKetQuaTap where ID_HocVien = '" + IdHocVien + "'";
            var getValue = DataProviderCaBinEco.Instance.ExcuteScalar(query);

            return getValue == DBNull.Value ? 0 : (int)getValue;
        }

        public DateTime GetTimeOut(int id)
        {
            string query = $"SELECT Convert (datetime, dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), Time_Out)) AS Time_Out FROM tblKetQuaTap WHERE ID = {id}";
            return (DateTime)DataProviderCaBinEco.Instance.ExcuteScalar(query);
        }

        public void DeleteResultExcer(int ID_KetQua)
        {
            string querykqt = "DELETE tblKetQuaTap where ID = '" + ID_KetQua + "'";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(querykqt);

            string querytst = "DELETE tblThiSinhTap WHERE ID_KetQua = '" + ID_KetQua + "'";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(querytst);
        }

        public string GetLstMaDK(int khoahocID)
        {
            string query = $"EXEC dbo.spGetLstMaDK @KhoaHocID = {khoahocID}";
            var getValue = DataProviderCaBinEco.Instance.ExcuteScalar(query);
            return (getValue == null) ? string.Empty : getValue.ToString();
        }


        public DataTable GetDataReportByMaDK(string lstMaDK)
        {
            string query = $"EXEC [dbo].[spLoadDataReport] @lstMaDK = N'{lstMaDK}'";
            return DataProviderCaBinEco.Instance.ExcuteQuery(query);
        }

        public DataTable GetBaoCaoByKhoaHocID(int IdKhoaHoc, int flag)
        {
            string query = $"EXEC [dbo].[spTongHop] @flag = {flag}, @ID_KhoaHoc = {IdKhoaHoc}, @NguongHang = 10800";
            return DataProviderCaBinEco.Instance.ExcuteQuery(query);
        }
        public void UpdateGv(int GiaoVienID, string MaDK)
        {
            string query = $"UPDATE tblHocVien set GiaoVienID = '{GiaoVienID}' where ID = '{MaDK}'";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(query);
        }

        public void ChangeCourse(int idKhoaHoc, string MaDK, int ID)
        {
            string query = $"UPDATE tblKetQuaTap SET ID_KhoaHoc = {idKhoaHoc} , ID_HocVien = '{MaDK}'  WHERE ID  = {ID}";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(query);
        }

        public void ChangeCabin(Cabin cabin, int ID)
        {
            string query = $"UPDATE tblKetQuaTap SET ID_ThietBi = {cabin.IdThietBi} , Cabin = '{cabin.GhiChu}'  WHERE ID  = {ID}";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(query);
        }

        public int GetMaxLanTap(string IdHocVien)
        {
            try
            {
                string query = $"EXEC dbo.spGetLstMaDK @KhoaHocID = {IdHocVien}";
                var getValue = DataProviderCaBinEco.Instance.ExcuteScalar(query);
                return Convert.ToInt32(getValue);
            }
            catch
            {
                return 0;
            }
        }

        public int CountLanTapByDay(string day, string IdHocVien)
        {
            int count;
            string query = string.Format("SELECT COUNT(*) FROM tblKetQuaTap WHERE CONVERT(VARCHAR, Convert (datetime, dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), Time_In)), 103) = '{0}' AND ID_HocVien = '{1}'", day, IdHocVien);
            count = (int)DataProviderCaBinEco.Instance.ExcuteScalar(query);

            return count > 0 ? count : 0;
        }

        public int GetMaxIdBaiTap(string IdHocVien)
        {
            try
            {
                string query = $"SELECT MAX(dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), ID_BaiTap)) AS IdBaiTapMax FROM tblKetQuaTap WHERE Id_HocVien =  '{IdHocVien}' ";
                var getValue = DataProviderCaBinEco.Instance.ExcuteScalar(query);
                return Convert.ToInt32(getValue);
            }
            catch
            {
                return 0;
            }
        }
        public void UpdateBaiTap(Int64 ID, int IDBaiTap)
        {
            string query = $"UPDATE tblKetQuaTap SET ID_BaiTap = dbo.SetEcoString(N'csdt@66015','{IDBaiTap}') WHERE ID = '{ID}'";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(query);
        }

        public List<GiaoVien> GetAllGV()
        {
            List<GiaoVien> lstGiaoVien = new List<GiaoVien>();
            string query = "SELECT * FROM tblGiaoVien";
            DataTable dt = DataProviderCaBinEco.Instance.ExcuteQuery(query);

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

        public void InsertTeacherNew(GiaoVien GV)
        {
            string query = string.Format("INSERT INTO [dbo].[tblGiaoVien] " +
                "([GiaoVienID], [MaGV],[HoTenDem],[TenGV],[NgaySinh],[SoCMT],[HangGPLX],[SuDung]) " +
                "VALUES " +
                "('{0}' , '{1}' , N'{2}' , N'{3}', '{4}', '{5}', '{6}' ,'1')", GV.GiaoVienID, GV.MaGV, GV.HoTenDem, GV.TenGV, GV.NgaySinh.ToString("yyyy-MM-dd HH:mm:ss.fff"), GV.SoCMT, GV.HangGPLX);
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(query);
        }

        public void UpdateKhoaHoc(int id, string Ten, string hangdaotao, int status)
        {
            string query = $"Update tblKhoaHoc set Ten = N'{Ten}', HangDaoTao = N'{hangdaotao}', HangGPLX = N'{hangdaotao}', Status = {status} where ID = {id}";
            DataProviderCaBinEco.Instance.ExcuteNoneQuery(query);
        }
        public List<ThongKe> LstThongKe(string day)
        {
            string query = $"EXEC [dbo].[spTongHopByDay] @Day = '{day}'";

            DataTable dt = DataProviderCaBinEco.Instance.ExcuteQuery(query);

            List<ThongKe> lst = dt.AsEnumerable().Select(item => new ThongKe
            {
                TenKhoaHoc = Convert.ToString(item["TenKH"]),
                TenGV = $"{Convert.ToString(item["HoTenDem"])} {Convert.ToString(item["TenGV"])}",
                CabinName = Convert.ToString(item["CabinName"]),
                MaDK = Convert.ToString(item["MaDK"]),
                HoVaTen = Convert.ToString(item["HoVaTen"]),
                NgaySinh = Convert.ToString(item["NgaySinh"]),
                SoCMT = Convert.ToString(item["SoCMT"]),
                TongThoiGian = Convert.ToString(item["ToTalSecond"]),
                TotalHour = Convert.ToDouble(item["TongGio"]),
                Result = Convert.ToBoolean(item["KetLuan"])
            }).ToList();

            return lst;
        }

        #region Thêm Khóa Học

        public int InsertKhoaHoc(KhoaHoc kh)
        {
            string query = "spAddKhoaHoc @MaKH , @TenKH , @NgayKG , @NgayBG , @HangDaoTao ";
            int result = DataProviderCaBinEco.Instance.ExcuteNoneQuery(query, new object[] { kh.MaKhoaHoc, kh.TenKhoaHoc, kh.NgayKhaiGiang, kh.NgayBeGiang, kh.HangDaoTao });
            return result;
        }

        public int GetKhoaHocIdByMaKhoaHoc(string MaKH)
        {
            try
            {
                string query = $"select ID from tblKhoaHoc WHERE MaKhoaHoc = '{MaKH}'";
                return (int)DataProviderCaBinEco.Instance.ExcuteScalar(query);
            }
            catch
            {
                return 0;
            }
        }

        public int InsertNguoiLxBase(int khoahocId, int stt, HocVien hv, byte[] bytesImg, int STT)
        {
            string query = "spAddBaseNguoiLx @ID , @STT , @HoTen , @SoCMT , @IDKhoaHoc , @NgaySinh , @HangGPLX , @HangDaoTao , @Images , @GioiTinh , @Ho , @Ten ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", SqlDbType.NVarChar) { Value = hv.MaDK},
                new SqlParameter("@STT", SqlDbType.Int) { Value = stt },
                new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = hv.HoTenHV },
                new SqlParameter("@SoCMT", SqlDbType.NVarChar) { Value = hv.SoCMT },
                new SqlParameter("@IDKhoaHoc", SqlDbType.NVarChar) { Value = khoahocId },
                new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = hv.NgaySinh },
                new SqlParameter("@HangGPLX", SqlDbType.VarChar) { Value = hv.HangGPLX },
                new SqlParameter("@HangDaoTao", SqlDbType.VarChar) { Value = hv.HangGPLX },
                new SqlParameter("@Images" , SqlDbType.VarBinary) { Value = bytesImg },
                new SqlParameter("@GioiTinh" , SqlDbType.NVarChar) { Value = hv.GioiTinh },
                new SqlParameter("@Ho" , SqlDbType.NVarChar) { Value = hv.HoTenDem },
                new SqlParameter("@Ten" , SqlDbType.NVarChar) { Value = hv.Ten }
            };

            int result = DataProviderCaBinEco.Instance.ExecuteNonQueryNew(query, parameters);

            return result;
        }

        #endregion

    }
}