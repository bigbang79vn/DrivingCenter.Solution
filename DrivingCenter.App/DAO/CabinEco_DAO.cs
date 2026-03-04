using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            string query = @"SELECT ID, MaKhoaHoc, Ten, NgayKhaiGiang, NgayBeGiang, HangDaoTao, Status, Year
                FROM tblKhoaHoc 
                ORDER BY MaKhoaHoc ASC";

            DataTable dt = DataProviderCaBinEco.Instance.ExecuteQuery(query);

            List<KhoaHoc> lstKhoaHoc = dt.AsEnumerable().Select(item => new KhoaHoc
            {
                KhoaHocID = (int)item["ID"],
                MaKhoaHoc = (string)item["MaKhoaHoc"],
                TenKhoaHoc = (string)item["Ten"],
                NgayKhaiGiang = (DateTime)item["NgayKhaiGiang"],
                NgayBeGiang = (DateTime)item["NgayBeGiang"],
                HangDaoTao = (string)item["HangDaoTao"],
                Status = (bool)item["Status"],
                Year = (int)item["Year"]
            }).ToList();

            return lstKhoaHoc;
        }

        public List<KhoaHoc> GetKHDangDT()
        {
            List<KhoaHoc> lstKhoaHoc = GetAllKhoaHoc()
                .Where(x => x.Status == false)
                .OrderBy(C => C.TenKhoaHoc)
                .ToList();

            return lstKhoaHoc;
        }

        public List<Cabin> GetLstCabin()
        {
            string query = "SELECT ID, Name, GhiChu FROM tblThietBi";
            DataTable dt = DataProviderCaBinEco.Instance.ExecuteQuery(query);

            List<Cabin> lstCabin = dt.AsEnumerable().Select(item => new Cabin
            {
                IdThietBi = (int)(item["ID"]),
                NameCabin = (string)(item["Name"]),
                GhiChu = (string)(item["GhiChu"])
            }).ToList();

            return lstCabin;
        }

        public List<HocVien> LoadLstHocVienById(int IdKhoaHoc)
        {
            string query = $"EXEC [dbo].[spGetLstHvByKhoaHoc] @ID_KhoaHoc = {IdKhoaHoc}";

            DataTable dt = DataProviderCaBinEco.Instance.ExecuteQuery(query);

            List<HocVien> lstStudent = dt.AsEnumerable().Select(item => new HocVien
            {
                MaDK = (string)item["ID"],
                HoTenHV = (string)item["HoTen"],
                SoCMT = (string)item["SoCMT"],
                ToTalSecond = (string)item["TongThoiGianText"],
                TenGV = item["GiaoVienID"] == DBNull.Value ? "" : (string)item["HoGiaoVien"] + " " + (string)item["TenGiaoVien"],
                HangGPLX = item.Field<string>("HangGPLX"),
                Result = item.Field<bool>("KetLuan")
            }).ToList();

            return lstStudent;
        }

        public string CheckDK(string idhocvien)
        {
            string query = "spKTDieuKien @Id_HocVien = " + "'" + idhocvien + "'";
            var getValue = DataProviderCaBinEco.Instance.ExecuteScalar(query);
            return (getValue == null) ? string.Empty : getValue.ToString();
        }

        public List<Result> lstResultById(string idHocVien)
        {
            string query = string.Format("SELECT TB.Name AS Cabin, KQ.ID_BaiTap, ID_KetQuaTap, KQ.Name, Time_In, Time_Out, CONVERT(CHAR(8),DATEADD(second,TongThoiGian,0),108) AS TongThoiGianPhien, TongThoiGian AS TotalSecond, LanTap " +
                "FROM v_ThongTinTap KQ, tblThietBi TB " +
                "WHERE ID_HocVien = '{0}' AND KQ.ID_ThietBi = TB.ID", idHocVien);

            DataTable dt = DataProviderCaBinEco.Instance.ExecuteQuery(query);

            List<Result> lstKetQuaTap =  dt.AsEnumerable().Select(item => new Result
            {
                NameCabin = (string)item["Cabin"],
                DiemDanhNguoiLxID = (int)item["ID_KetQuaTap"],
                BaiHocCaBinID = (int)item["ID_BaiTap"],
                TenBaiTapCaBin = (string)item["Name"],
                ThoiDiemBatDau = (DateTime)item["Time_In"],
                ThoiDiemKetThuc = (DateTime)item["Time_Out"],
                TongThoiGianPhien = (string)item["TongThoiGianPhien"],
                TotalSecond = Convert.ToDouble(item["TotalSecond"])
            }).ToList();

            return lstKetQuaTap;
        }

        public List<Exercise> GetListBaiTap()
        {
            string query = "SELECT ID_BaiTap, Name from tblExam";
            DataTable data = DataProviderCaBinEco.Instance.ExecuteQuery(query);

            List<Exercise> lst = data.AsEnumerable().Select(item => new Exercise
            {
                ID = item.Field<int>("ID_BaiTap"),
                TenBaiTap = item.Field<string>("Name") ?? string.Empty
            }).ToList();

            return lst;
        }

        public DataTable LoadInfoHVByMaDK(string MaDK)
        {
            string query = string.Format("SELECT hv.ID, hv.HoTen, hv.SoCMT, hv.HangDaoTao, hv.GioiTinh, hv.NgaySinh, hv.Images, kh.Ten " +
                "FROM tblHocVien AS hv INNER JOIN tblKhoaHoc AS kh ON hv.IDKhoaHoc = kh.ID " +
                "WHERE hv.ID = '{0}'", MaDK);

            return DataProviderCaBinEco.Instance.ExecuteQuery(query);
        }

        public string GetMaDKNew(int Id_KhoaHoc, string SoCMT)
        {
            string query = $"SELECT ID FROM tblHocVien WHERE IDKhoaHoc = {Id_KhoaHoc} AND SoCMT = '{SoCMT}'";
            var getValue = DataProviderCaBinEco.Instance.ExecuteScalar(query);
            return (getValue == null) ? string.Empty : getValue.ToString();
        }

        public int CreateResult(int idthietbi, string ghichu, string idhocvien, int idbaitap, string datecreate, int idkhoahoc, int lantap, int lantapcabin, int minute)
        {
            string query = "spResult @IdThietBi , @Cabin ,  @IdHocVien , @IdBaiTap , @DateCreate , @IdKhoaHoc , @LanTap , @LanTapCaBin , @Minute ";
            
            var parameters = new
            {
                IdThietBi = idthietbi,
                Cabin = ghichu,
                IdHocVien = idhocvien,
                IdBaiTap = idbaitap,
                DateCreate = datecreate,
                IdKhoaHoc = idkhoahoc,
                LanTap = lantap,
                LanTapCaBin = lantapcabin,
                Minute = minute
            };

            return DataProviderCaBinEco.Instance.ExecuteNonQuery(query, parameters);
        }

        public int GetIdMaxByIdHocVien(string IdHocVien)
        {
            string query = "select MAX(ID) from tblKetQuaTap where ID_HocVien = '" + IdHocVien + "'";
            var getValue = DataProviderCaBinEco.Instance.ExecuteScalar(query);

            return getValue == DBNull.Value ? 0 : (int)getValue;
        }

        public DateTime GetTimeOut(int id)
        {
            string query = $"SELECT Convert (datetime, dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), Time_Out)) AS Time_Out FROM tblKetQuaTap WHERE ID = {id}";
            return (DateTime)DataProviderCaBinEco.Instance.ExecuteScalar(query);
        }

        public void DeleteResultExcer(int ID_KetQua)
        {
            string querykqt = "DELETE tblKetQuaTap where ID = '" + ID_KetQua + "'";
            DataProviderCaBinEco.Instance.ExecuteNonQuery(querykqt);

            string querytst = "DELETE tblThiSinhTap WHERE ID_KetQua = '" + ID_KetQua + "'";
            DataProviderCaBinEco.Instance.ExecuteNonQuery(querytst);
        }
        public DataTable GetDataReportByMaDK(string lstMaDK)
        {
            string query = $"EXEC [dbo].[spLoadDataReport] @lstMaDK = N'{lstMaDK}'";
            return DataProviderCaBinEco.Instance.ExecuteQuery(query);
        }

        public DataTable GetBaoCaoByKhoaHocID(int IdKhoaHoc, int flag)
        {
            string query = $"EXEC [dbo].[spTongHop] @flag = {flag}, @ID_KhoaHoc = {IdKhoaHoc}, @NguongHang = 10800";
            return DataProviderCaBinEco.Instance.ExecuteQuery(query);
        }
        public void UpdateGv(int GiaoVienID, string MaDK)
        {
            string query = $"UPDATE tblHocVien set GiaoVienID = '{GiaoVienID}' where ID = '{MaDK}'";
            DataProviderCaBinEco.Instance.ExecuteNonQuery(query);
        }

        public void ChangeCourse(int idKhoaHoc, string MaDK, int ID)
        {
            string query = $"UPDATE tblKetQuaTap SET ID_KhoaHoc = {idKhoaHoc} , ID_HocVien = '{MaDK}'  WHERE ID  = {ID}";
            DataProviderCaBinEco.Instance.ExecuteNonQuery(query);
        }

        public void ChangeCabin(Cabin cabin, int ID)
        {
            string query = $"UPDATE tblKetQuaTap SET ID_ThietBi = {cabin.IdThietBi} , Cabin = '{cabin.GhiChu}'  WHERE ID  = {ID}";
            DataProviderCaBinEco.Instance.ExecuteNonQuery(query);
        }

        public int GetMaxLanTap(string IdHocVien)
        {
            try
            {
                string query = $"EXEC dbo.spGetLstMaDK @KhoaHocID = {IdHocVien}";
                var getValue = DataProviderCaBinEco.Instance.ExecuteScalar(query);
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
            count = (int)DataProviderCaBinEco.Instance.ExecuteScalar(query);

            return count > 0 ? count : 0;
        }

        public int GetMaxIdBaiTap(string IdHocVien)
        {
            try
            {
                string query = $"SELECT MAX(dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), ID_BaiTap)) AS IdBaiTapMax FROM tblKetQuaTap WHERE Id_HocVien =  '{IdHocVien}' ";
                var getValue = DataProviderCaBinEco.Instance.ExecuteScalar(query);
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
            DataProviderCaBinEco.Instance.ExecuteNonQuery(query);
        }

        public List<GiaoVien> GetAllGV()
        {
            string query = "SELECT * FROM tblGiaoVien";
            DataTable dt = DataProviderCaBinEco.Instance.ExecuteQuery(query);

            List<GiaoVien> lstGiaoVien = dt.AsEnumerable().Select(item => new GiaoVien
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

        public void InsertTeacherNew(GiaoVien gv)
        {
            string query = @"
                        INSERT INTO [dbo].[tblGiaoVien] 
                        ([GiaoVienID], [MaGV], [HoTenDem], [TenGV], [NgaySinh], [SoCMT], [HangGPLX], [SuDung]) 
                        VALUES 
                        (@giaoVienID, @maGV, @hoTenDem, @tenGV, @ngaySinh, @soCMT, @hangGPLX, 1);";

            var parameters = new
            {
                giaoVienID = gv.GiaoVienID,
                maGV = gv.MaGV,
                hoTenDem = gv.HoTenDem,
                tenGV = gv.TenGV,
                ngaySinh = gv.NgaySinh,
                soCMT = gv.SoCMT,
                hangGPLX = gv.HangGPLX
            };

            DataProviderCaBinEco.Instance.ExecuteNonQuery(query, parameters);
        }

        public void UpdateKhoaHoc(int id, string Ten, string hangdaotao, int status)
        {
            string query = $"Update tblKhoaHoc set Ten = N'{Ten}', HangDaoTao = N'{hangdaotao}', HangGPLX = N'{hangdaotao}', Status = {status} where ID = {id}";
            DataProviderCaBinEco.Instance.ExecuteNonQuery(query);
        }
        public List<ThongKe> LstThongKe(string day)
        {
            string query = $"EXEC [dbo].[spTongHopByDay] @Day = '{day}'";

            DataTable dt = DataProviderCaBinEco.Instance.ExecuteQuery(query);

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
            string query = "EXEC spAddKhoaHoc @MaKH, @TenKH, @NgayKG, @NgayBG, @HangDaoTao";
            var parameters = new
            {
                MaKH = kh.MaKhoaHoc,
                TenKH = kh.TenKhoaHoc,
                NgayKG = kh.NgayKhaiGiang,
                NgayBG = kh.NgayBeGiang,
                HangDaoTao = kh.HangDaoTao
            };

            int result = DataProviderCaBinEco.Instance.ExecuteNonQuery(query, parameters);
            return result;
        }

        public int GetKhoaHocIdByMaKhoaHoc(string MaKH)
        {
            try
            {
                string query = $"SELECT ID FROM tblKhoaHoc WHERE MaKhoaHoc = '{MaKH}'";
                return (int)DataProviderCaBinEco.Instance.ExecuteScalar(query);
            }
            catch
            {
                return 0;
            }
        }

        public int InsertNguoiLxBase(int khoahocId, int stt, HocVien hv, byte[] bytesImg, int STT)
        {
            string query = "spAddBaseNguoiLx @ID , @STT , @HoTen , @SoCMT , @IDKhoaHoc , @NgaySinh , @HangGPLX , @HangDaoTao , @Images , @GioiTinh , @Ho , @Ten ";

            var parameters = new {
                ID = hv.MaDK,
                STT = stt,
                HoTen = hv.HoTenHV ,
                SoCMT = hv.SoCMT ,
                IDKhoaHoc = khoahocId ,
                NgaySinh = hv.NgaySinh ,
                HangGPLX = hv.HangGPLX ,
                HangDaoTao = hv.HangGPLX ,
                Images = bytesImg ,
                GioiTinh = hv.GioiTinh ,
                Ho = hv.HoTenDem ,
                Ten = hv.Ten 
            };

            int result = DataProviderCaBinEco.Instance.ExecuteNonQuery(query, parameters);

            return result;
        }

        #endregion

    }
}