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

        private readonly DbProvider _db = new DbProvider(Enums.DatabaseType.db_CabinEco);

        public List<KhoaHocCabin> GetAllKhoaHoc()
        {
            string query = @"SELECT * FROM tblKhoaHoc ORDER BY MaKhoaHoc ASC";

            DataTable dt = _db.QueryToDataTable(query);

            List<KhoaHocCabin> lstKhoaHoc = dt.AsEnumerable()
                .Select(item => new KhoaHocCabin
                {
                    KhoaHocID = item.Field<int>("ID"),
                    MaKhoaHoc = item.Field<string>("MaKhoaHoc"),
                    TenKhoaHoc = item.Field<string>("Ten"),
                    NgayKhaiGiang = item.Field<DateTime>("NgayKhaiGiang"),
                    NgayBeGiang = item.Field<DateTime>("NgayBeGiang"),
                    HangDaoTao = item.Field<string>("HangDaoTao")?.Trim(),
                    Status = item.Field<bool>("Type"),
                    Year = item.Field<int>("Year")
                }).ToList();

            return lstKhoaHoc;
        }

        public List<Cabin> GetLstCabin()
        {
            string query = "SELECT ID, Name, GhiChu FROM tblThietBi";
            DataTable dt = _db.QueryToDataTable(query);

            List<Cabin> lstCabin = dt.AsEnumerable()
                .Select(item => new Cabin
                {
                    IdThietBi = item.Field<int>("ID"),
                    NameCabin = item.Field<string>("Name"),
                    GhiChu = item.Field<string>("GhiChu")
                }).ToList();

            return lstCabin;
        }

        public List<HocVien> LoadLstHocVienById(int IdKhoaHoc)
        {
            const string query = "EXEC [dbo].[spGetLstHvByKhoaHoc] @ID_KhoaHoc = @IdKhoaHoc";

            var dt = _db.QueryToDataTable(query, new { IdKhoaHoc });

            List<HocVien> lstStudent = dt.AsEnumerable().Select(item => new HocVien
            {
                MaDK = item.Field<string>("ID"),
                HoTenHV = item.Field<string>("HoTen"),
                SoCMT = item.Field<string>("SoCMT"),
                ToTalSecond = item.Field<string>("TongThoiGianText"),
                TenGV = item["GiaoVienID"] == DBNull.Value ? "" : $"{Convert.ToString(item["HoGiaoVien"])} {Convert.ToString(item["TenGiaoVien"])}".Trim(),
                HangGPLX = item.Field<string>("HangGPLX"),
                Result = item.Field<bool>("KetLuan")
            }).ToList();

            return lstStudent;
        }

        public List<Result> lstResultById(string idHocVien)
        {
            // Đọc thẳng tblKetQuaTap thay vì v_ThongTinTap: view nối INNER JOIN theo bài tập đã giải mã nên bỏ mất
            // các phiên do phần mềm cabin mới ghi (từ 21/08/2026, mã hóa phía phần mềm, khóa cũ trả NULL).
            // Phiên dạng mới vẫn hiện với cabin + giờ bắt đầu (Time_In_Plain); các cột còn lại không giải mã được.
            const string query = @"
                SELECT kqt.ID AS ID_KetQuaTap, TB.Name AS Cabin, x.ID_BaiTap, e.Name,
                       ISNULL(x.Time_In, kqt.Time_In_Plain) AS Time_In, x.Time_Out, x.TongThoiGian,
                       CASE WHEN x.KeyLegacy IS NULL THEN 1 ELSE 0 END AS MaHoaMoi
                FROM tblKetQuaTap kqt
                CROSS APPLY (SELECT dbo.GetEcoString(N'edsecotek@2023', kqt.Mikiway) AS KeyLegacy) k
                CROSS APPLY (SELECT k.KeyLegacy,
                                    TRY_CONVERT(int, dbo.GetEcoString(k.KeyLegacy, kqt.ID_BaiTap)) AS ID_BaiTap,
                                    TRY_CONVERT(datetime, dbo.GetEcoString(k.KeyLegacy, kqt.Time_In)) AS Time_In,
                                    TRY_CONVERT(datetime, dbo.GetEcoString(k.KeyLegacy, kqt.Time_Out)) AS Time_Out,
                                    TRY_CONVERT(int, dbo.GetEcoString(k.KeyLegacy, kqt.TotalSecond)) AS TongThoiGian) x
                LEFT JOIN tblThietBi TB ON TB.ID = kqt.ID_ThietBi
                LEFT JOIN tblExam e ON e.ID_BaiTap = x.ID_BaiTap
                WHERE kqt.ID_HocVien = @idHocVien";

            DataTable dt = _db.QueryToDataTable(query, new { idHocVien });

            List<Result> lstKetQuaTap =  dt.AsEnumerable().Select(item =>
            {
                double giay = item["TongThoiGian"] == DBNull.Value ? 0 : Convert.ToDouble(item["TongThoiGian"]);
                return new Result
                {
                    NameCabin = Convert.ToString(item["Cabin"]),
                    DiemDanhNguoiLxID = (int)item["ID_KetQuaTap"],
                    BaiHocCaBinID = item["ID_BaiTap"] == DBNull.Value ? 0 : (int)item["ID_BaiTap"],
                    TenBaiTapCaBin = Convert.ToString(item["Name"]),
                    ThoiDiemBatDau = item["Time_In"] == DBNull.Value ? DateTime.MinValue : (DateTime)item["Time_In"],
                    ThoiDiemKetThuc = item["Time_Out"] == DBNull.Value ? DateTime.MinValue : (DateTime)item["Time_Out"],
                    TongThoiGianPhien = item["TongThoiGian"] == DBNull.Value ? "" : TimeSpan.FromSeconds(giay).ToString(@"hh\:mm\:ss"),
                    TotalSecond = giay,
                    MaHoaMoi = Convert.ToInt32(item["MaHoaMoi"]) == 1
                };
            }).ToList();

            return lstKetQuaTap;
        }

        public List<Exercise> GetListBaiTap()
        {
            string query = "SELECT ID_BaiTap, Name from tblExam";
            DataTable data = _db.QueryToDataTable(query);

            List<Exercise> lst = data.AsEnumerable().Select(item => new Exercise
            {
                ID = item.Field<int>("ID_BaiTap"),
                TenBaiTap = item.Field<string>("Name") ?? string.Empty
            }).ToList();

            return lst;
        }

        public DataTable LoadInfoHVByMaDK(string MaDK)
        {
            const string query = "SELECT hv.ID, hv.HoTen, hv.SoCMT, hv.HangDaoTao, hv.GioiTinh, hv.NgaySinh, hv.Images, kh.Ten " +
                "FROM tblHocVien AS hv INNER JOIN tblKhoaHoc AS kh ON hv.IDKhoaHoc = kh.ID " +
                "WHERE hv.ID = @MaDK";

            return _db.QueryToDataTable(query, new { MaDK });
        }

        public string GetMaDKNew(int Id_KhoaHoc, string SoCMT)
        {
            const string query = "SELECT TOP 1 ID FROM tblHocVien WHERE IDKhoaHoc = @Id_KhoaHoc AND SoCMT = @SoCMT";
            var getValue = _db.QuerySingle<string>(query, new { Id_KhoaHoc, SoCMT });
            return getValue ?? string.Empty;
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

            return _db.Execute(query, parameters);
        }

        public int GetIdMaxByIdHocVien(string IdHocVien)
        {
            const string query = "select MAX(ID) from tblKetQuaTap where ID_HocVien = @IdHocVien";
            return _db.Scalar<int>(query, new { IdHocVien });
        }

        public DateTime GetTimeOut(int id)
        {
            const string query = "SELECT Convert (datetime, dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), Time_Out)) AS Time_Out FROM tblKetQuaTap WHERE ID = @id";
            return _db.QuerySingle<DateTime>(query, new { id });
        }

        public void DeleteResultExcer(int ID_KetQua)
        {
            _db.Execute("DELETE tblKetQuaTap WHERE ID = @ID_KetQua", new { ID_KetQua });
            _db.Execute("DELETE tblThiSinhTap WHERE ID_KetQua = @ID_KetQua", new { ID_KetQua });
        }
        public DataTable GetDataReportByMaDK(string lstMaDK)
        {
            const string query = "EXEC [dbo].[spLoadDataReport] @lstMaDK = @lstMaDK";
            return _db.QueryToDataTable(query, new { lstMaDK });
        }

        public DataTable GetBaoCaoByKhoaHocID(int IdKhoaHoc, int flag)
        {
            const string query = "EXEC [dbo].[spTongHop] @flag = @flag, @ID_KhoaHoc = @IdKhoaHoc, @NguongHang = 10800";
            return _db.QueryToDataTable(query, new { flag, IdKhoaHoc });
        }
        public void UpdateGv(int GiaoVienID, string MaDK)
        {
            _db.Execute("UPDATE tblHocVien SET GiaoVienID = @GiaoVienID WHERE ID = @MaDK", new { GiaoVienID, MaDK });
        }

        public void ChangeCourse(int idKhoaHoc, string MaDK, int ID)
        {
            _db.Execute("UPDATE tblKetQuaTap SET ID_KhoaHoc = @idKhoaHoc, ID_HocVien = @MaDK WHERE ID = @ID", new { idKhoaHoc, MaDK, ID });
        }

        public void ChangeCabin(Cabin cabin, int ID)
        {
            _db.Execute("UPDATE tblKetQuaTap SET ID_ThietBi = @IdThietBi, Cabin = @GhiChu WHERE ID = @ID", new { cabin.IdThietBi, cabin.GhiChu, ID });
        }

        public int GetMaxLanTap(string IdHocVien)
        {
            try
            {
                const string query = "EXEC dbo.spGetLstMaDK @KhoaHocID = @IdHocVien";
                return _db.QuerySingle<int>(query, new { IdHocVien });
            }
            catch
            {
                return 0;
            }
        }

        public int CountLanTapByDay(string day, string IdHocVien)
        {
            int count;
            const string query = "SELECT COUNT(*) FROM tblKetQuaTap WHERE CONVERT(VARCHAR, Convert (datetime, dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), Time_In)), 103) = @day AND ID_HocVien = @IdHocVien";
            count = _db.Scalar<int>(query, new { day, IdHocVien });

            return count > 0 ? count : 0;
        }

        public int GetMaxIdBaiTap(string IdHocVien)
        {
            try
            {
                const string query = "SELECT MAX(dbo.GetEcoString(dbo.GetEcoString(N'edsecotek@2023',Mikiway), ID_BaiTap)) AS IdBaiTapMax FROM tblKetQuaTap WHERE Id_HocVien = @IdHocVien";
                return _db.Scalar<int>(query, new { IdHocVien });
            }
            catch
            {
                return 0;
            }
        }

        public void UpdateBaiTap(Int64 ID, int IDBaiTap)
        {
            // ID_BaiTap được mã hóa bằng SetEcoString; tham số vẫn truyền dạng chuỗi như bản cũ
            _db.Execute("UPDATE tblKetQuaTap SET ID_BaiTap = dbo.SetEcoString(N'csdt@66015', @BaiTap) WHERE ID = @ID",
                new { BaiTap = IDBaiTap.ToString(), ID });
        }

        public List<GiaoVien> GetAllGV()
        {
            string query = "SELECT * FROM tblGiaoVien";
            DataTable dt = _db.QueryToDataTable(query);

            List<GiaoVien> lstGiaoVien = dt.AsEnumerable().Select(item => new GiaoVien
            {
                GiaoVienID = (int)item["GiaoVienID"],
                MaGV = Convert.ToString(item["MaGV"]),
                HoTenDem = Convert.ToString(item["HoTenDem"]),
                TenGV = Convert.ToString(item["TenGV"]),
                HoTenGV = $"{Convert.ToString(item["HoTenDem"])} {Convert.ToString(item["TenGV"])}",
                NgaySinh = item["NgaySinh"] == DBNull.Value ? DateTime.MinValue : (DateTime)item["NgaySinh"],
                SoCMT = Convert.ToString(item["SoCMT"]),
                HangGPLX = Convert.ToString(item["HangGPLX"]),
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

            _db.Execute(query, parameters);
        }

        public void UpdateKhoaHoc(int id, string Ten, string hangdaotao, int status)
        {
            // Trước đây ghép chuỗi: tên khóa có dấu nháy đơn làm hỏng câu lệnh
            _db.Execute("UPDATE tblKhoaHoc SET Ten = @Ten, HangDaoTao = @hangdaotao, HangGPLX = @hangdaotao, Type = @status WHERE ID = @id",
                new { Ten, hangdaotao, status, id });
        }
        public List<ThongKe> LstThongKe(string day)
        {
            const string query = "EXEC [dbo].[spTongHopByDay] @Day = @day";

            DataTable dt = _db.QueryToDataTable(query, new { day });

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

        public int InsertKhoaHoc(KhoaHocCabin kh)
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

            return _db.Execute(query, parameters);
        }

        public int GetKhoaHocIdByMaKhoaHoc(string MaKH)
        {
            try
            {
                const string query = "SELECT TOP 1 ID FROM tblKhoaHoc WHERE MaKhoaHoc = @MaKH";
                return _db.QuerySingle<int>(query, new { MaKH });
            }
            catch
            {
                return 0;
            }
        }

        public int InsertNguoiLxBase(int khoahocId, int stt, HocVien hv, byte[] bytesImg)
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

            // Execute: thủ tục trả về 0, 1 hay nhiều dòng đều không lỗi (QuerySingle lỗi khi nhiều dòng)
            return _db.Execute(query, parameters);
        }

        #endregion

    }
}
