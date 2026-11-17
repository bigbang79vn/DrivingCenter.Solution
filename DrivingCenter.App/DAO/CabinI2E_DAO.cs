using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.Cabin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DrivingCenter.App.DAO
{
    public class CabinI2E_DAO
    {
        private readonly DbProvider _db = new DbProvider(Enums.DatabaseType.db_CaBinEMC);

        public List<KhoaHocCabin> GetAllKhoaHoc()
        {
            string query = "SELECT KhoaHocID, MaKH, TenKH, NgayKG, NgayBG, MaBC1, HangDaoTao, LoaiDaoTao FROM KhoaHoc";
            var result = _db.QueryToDataTable(query);
            List<KhoaHocCabin> lstKhoaHoc = result.AsEnumerable().Select(item => new KhoaHocCabin
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

        public List<Int64> GetlstNguoiLxID(int IdKhoaHoc)
        {
            const string query = "SELECT NguoiLXID FROM NguoiLX WHERE KhoaHocID = @IdKhoaHoc";
            return _db.Query<Int64>(query, new { IdKhoaHoc }).ToList();
        }

        public void UpdateTimeExerToNguoiLx(Int64 NguoilxId)
        {
            string query = "sp_UpdateTimeExer @ID_HocVien";
            var para = new {
                ID_HocVien = NguoilxId
            };
            _db.Execute(query, para);
        }

        public List<HocVien> GetlstHocVienByIdKhoaHoc(int IdKhoaHoc)
        {
            const string query = "SELECT * FROM v_LstHocVienByKH WHERE KhoaHocID = @IdKhoaHoc ORDER BY GiaoVienID ASC";

            DataTable data = _db.QueryToDataTable(query, new { IdKhoaHoc });

            List<HocVien> lstStudent = data.AsEnumerable().Select(item => new HocVien
            {
                MaDK = item["MaDK"].ToString(),
                NguoiLxID = (long)item["NguoiLXID"],
                MaKH = item["MaKH"].ToString(),
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

        public string CheckDK(Int64? NguoiLxID, string makh)
        {
            const string query = "EXEC sp_KTDieuKien @Id_HocVien = @Id_HocVien, @MaKH = @MaKH";
            string KetLuan = _db.QuerySingle<string>(query, new { Id_HocVien = NguoiLxID, MaKH = makh }) ?? string.Empty;
            return KetLuan;
        }

        public List<ThongKe> LstThongKe(string day)
        {
            string query = $@"SELECT hv.NguoiLXID, gv.HoTenDem, gv.TenGV, hv.TenKH, hv.MaDK, hv.HoVaTen, CONVERT(varchar, hv.NgaySinh, 103) as NgaySinh, hv.SoCMT, CONVERT(varchar,DATEADD(second,SUM(TongThoiGianPhien),0),108) AS TongThoiGian , SUM(TongThoiGianPhien) / 3600 AS TotalSecond 
                FROM DiemDanhNguoiLx as kqt, NguoiLX as hv, GiaoVien AS gv  
                WHERE hv.NguoiLXID = kqt.NguoiLxID and hv.GiaoVienID = gv.GiaoVienID and CONVERT(VARCHAR, ThoiDiemBatDau, 103) = @Day
                GROUP BY hv.NguoiLXID, hv.TenKH, hv.MaDK, hv.NgaySinh, hv.HoVaTen, hv.SoCMT, gv.TenGV, gv.HoTenDem";

            var dt = _db.QueryToDataTable(query, new { Day = day });

            List<ThongKe> lst = new List<ThongKe>();
            lst = dt.AsEnumerable().Select(item => new ThongKe
            {
                NguoiLxID = Convert.ToInt64(item["NguoiLxID"]),
                TenGV = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString(),
                TenKhoaHoc = item["TenKH"].ToString(),
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = item["NgaySinh"].ToString(),
                SoCMT = item["SoCMT"].ToString(),
                TongThoiGian = item["TongThoiGian"].ToString(),
                TotalHour = Convert.ToInt64(item["TotalSecond"]),
            }).ToList();

            return lst;
        }

        public HocVien GetInfoHVByNguoiLxID(Int64? NguoiLxID)
        {
            const string query = "SELECT * FROM [I2ECABIN3D].[dbo].[v_LstHocVienByKH] WHERE NguoiLXID = @NguoiLxID";

            var dt = _db.QueryToDataTable(query, new { NguoiLxID });

            List<HocVien> lstStudent = dt.AsEnumerable().Select(item => new HocVien
            {
                MaDK = item["MaDK"].ToString(),
                NguoiLxID = (long)item["NguoiLXID"],
                // Thiếu MaKH thì sp_KTDieuKien trả "Không tìm thấy thông tin khóa học" dù học viên đã đạt
                MaKH = item["MaKH"].ToString(),
                TenKH = item["TenKH"].ToString(),
                HoTenHV = item["HoVaTen"].ToString(),
                NgaySinh = (DateTime)item["NgaySinh"],
                GioiTinh = item["GioiTinh"].ToString(),
                SoCMT = item["SoCMT"].ToString(),
                NgayCapCMT = (DateTime)item["NgayCapCMT"],
                HangGPLX = item["HangGPLX"].ToString(),

                Anh = item["Anh"] as byte[],   // học viên chưa có ảnh: DBNull → null (trước đây ném lỗi ép kiểu)
                ToTalSecond = item["TongThoiGianPhien"].ToString(),
                TongNoiDungDT = (int)item["TongNoiDungDT"],
                TenGV = item["HoTenDem"].ToString() + " " + item["TenGV"].ToString()
            }).ToList();

            return lstStudent.FirstOrDefault();
        }

        public int CountBaiTap(Int64? NguoiLxID)
        {
            const string query = "SELECT count(*) FROM " +
                "(SELECT MIN(BaiHocCaBinID) AS ID_BaiTap FROM DiemDanhNguoiLx where NguoiLxID = @NguoiLxID and BaiHocCaBinID > 0 and Datediff(s, ThoiDiemBatDau, ThoiDiemKetThuc) > 60 GROUP BY BaiHocCaBinID) t";
            int TongBaiTap = _db.Scalar<int>(query, new { NguoiLxID });
            return TongBaiTap;
        }

        public List<Result> GetResultByNguoiLxID(Int64? NguoiLxID)
        {
            const string query = "SELECT tb.TenThietBi, kq.DiemDanhNguoiLxID, kq.BaiHocCaBinID, bt.TenBaiTapCabin, kq.ThoiDiemBatDau, kq.ThoiDiemKetThuc, QuangDuong, PathVideo, CONVERT(varchar, DATEADD(second, kq.TongThoiGianPhien, 0), 108) as TongThoiGianPhien, kq.TongThoiGianPhien as TotalSecond, CreateDate " +
                "FROM DiemDanhNguoiLx as kq, BaiTapCaBin as bt, ThietBi as tb " +
                "WHERE kq.BaiHocCaBinID = bt.BaiTapCaBinID and tb.ThietBiID = kq.ThietBiID and NguoiLxID = @NguoiLxID " +
                "ORDER BY ThoiDiemBatDau";

            var dt = _db.QueryToDataTable(query, new { NguoiLxID });

            List<Result> lst = dt.AsEnumerable().Select(item => new Result
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
            var data = _db.QueryToDataTable(query);

            List<Exercise> lst = data.AsEnumerable().Select(item => new Exercise
            {
                ID = (int)item["BaiTapCaBinID"],
                TenBaiTap = (string)item["TenBaiTapCaBin"],
            }).ToList();

            return lst;
        }

        public void UpdateBaiTap(int IdBaiTap, Int64 diemdanhnguoilxId)
        {
            const string query = "UPDATE DiemDanhNguoiLx SET BaiHocCaBinID = @IdBaiTap WHERE DiemDanhNguoiLxID = @Id";
            _db.Execute(query, new { IdBaiTap, Id = diemdanhnguoilxId });
        }

        public void DeleteResultExcer(Int64 DiemdanhnguoilxID)
        {
            var p = new { Id = DiemdanhnguoilxID };
            _db.Execute("DELETE DiemDanhNguoiLx WHERE DiemDanhNguoiLxID = @Id", p);
            _db.Execute("DELETE SuKienHoc WHERE DiemDanhNguoiLxID = @Id", p);
        }

        public void InsertTeacherNew(GiaoVien GV)
        {
            const string query = "INSERT INTO [dbo].[GiaoVien] " +
                "([GiaoVienID], [MaGV],[HoTenDem],[TenGV],[NgaySinh],[SoCMT],[HangGPLX],[SuDung]) " +
                "VALUES (@GiaoVienID, @MaGV, @HoTenDem, @TenGV, @NgaySinh, @SoCMT, @HangGPLX, 1)";
            _db.Execute(query, new { GV.GiaoVienID, GV.MaGV, GV.HoTenDem, GV.TenGV, GV.NgaySinh, GV.SoCMT, GV.HangGPLX });
        }

        public void UpdateGv(int GiaoVienID, string MaDK)
        {
            const string query = "UPDATE NguoiLX set GiaoVienID = @GiaoVienID where MaDK = @MaDK";
            _db.Execute(query, new { GiaoVienID, MaDK });
        }

        public string GetLstNguoiLxID(int khoahocID)
        {
            const string query = "EXEC VNDC_spGetLstNguoiLxID @KhoaHocID = @KhoaHocID";
            return _db.QuerySingle<string>(query, new { KhoaHocID = khoahocID });
        }

        public void UpdateStatusKhoaHoc(string tenkhoahoc, int loaidaotao, string makhoahoc)
        {
            const string query = "UPDATE KhoaHoc SET TenKH = @TenKH, LoaiDaoTao = @LoaiDaoTao WHERE MaKH = @MaKH";
            _db.Execute(query, new { TenKH = tenkhoahoc, LoaiDaoTao = loaidaotao, MaKH = makhoahoc });
        }

        #region Sync GV for Cabin

        public List<GvCabin> GetListGvByNameKH(string TenKH)
        {
            const string query = "select MaDK, GiaoVienID from NguoiLX where TenKH like '%' + @TenKH + '%' ORDER BY GiaoVienID";
            DataTable dt = _db.QueryToDataTable(query, new { TenKH });

            List<GvCabin> lst = dt.AsEnumerable().Select(item => new GvCabin
            {
                MaDK = (string)(item)["MaDK"],
                GiaoVienID = (int)(item)["GiaoVienID"]
            }).ToList();

            return lst;
        }

        public List<GiaoVien> GetAllGV()
        {
            string query = "SELECT GiaoVienID, MaGV, HoTenDem, TenGV, NgaySinh, SoCMT, HangGPLX FROM GiaoVien";
            DataTable dt = _db.QueryToDataTable(query);

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

        #endregion Sync GV for Cabin

        public List<Cabin> GetLstCabin()
        {
            string query = "SELECT ThietBiID, TenThietBi FROM ThietBi";
            var dt = _db.QueryToDataTable(query);
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
                const string query = "SELECT SUM(TongThoiGianPhien) FROM DiemDanhNguoiLx WHERE NguoiLxID = @NguoiLxID";
                return _db.Scalar<double>(query, new { NguoiLxID });
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
                // Trước đây gọi _db.Equals(query) (so sánh object, không truy vấn) nên luôn trả 0:
                // mọi bài sao chép đều bắt đầu cùng một thời điểm thay vì nối tiếp nhau
                const string query = "SELECT MAX(DiemDanhNguoiLxID) FROM DiemDanhNguoiLx WHERE NguoiLxID = @NguoiLxID";
                return _db.QuerySingle<long?>(query, new { NguoiLxID }) ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>Thời điểm kết thúc của phiên cabin muộn nhất của học viên (null nếu chưa có phiên nào).</summary>
        public DateTime? GetLastSessionEnd(Int64 NguoiLxID)
        {
            const string query = "SELECT MAX(ThoiDiemKetThuc) FROM DiemDanhNguoiLx WHERE NguoiLxID = @NguoiLxID";
            return _db.QuerySingle<DateTime?>(query, new { NguoiLxID });
        }

        public int GetDayDiemdanhnguoilxByNguoiLxID(Int64 NguoiLxID)
        {
            try
            {
                const string query = "SELECT MAX(day(ThoiDiemBatDau)) FROM DiemDanhNguoiLx WHERE NguoiLxID = @NguoiLxID";
                return _db.Scalar<int>(query, new { NguoiLxID });
            }
            catch
            {
                return 0;
            }
        }

        public DateTime GetTimeOut(Int64 diemdanhnguoilxId, Int64 NguoiLxID)
        {
            const string query = "select ThoiDiemKetThuc from DiemDanhNguoiLx where DiemDanhNguoiLxID = @Id AND NguoiLxID = @NguoiLxID";

            return _db.QuerySingle<DateTime>(query, new { Id = diemdanhnguoilxId, NguoiLxID });
        }

        public int InsertData(Result r, Int64 nguoiLxId, int IdThietBi, int IDKhoaHoc)
        {
            // Dapper cần tham số có tên: truyền object[] (kiểu cũ của DataProvider) làm các @tham số không được gán
            string query = "VNDC_Insert @IdBaiTap , @NguoiLxID , @IdThietBi , @TimeIn , @ThoiGianPhien , @QuangDuong , @PathVideo , @KhoaHocId";
            int result = _db.Execute(query, new
            {
                IdBaiTap = r.BaiHocCaBinID,
                NguoiLxID = nguoiLxId,
                IdThietBi,
                TimeIn = r.ThoiDiemBatDau,
                ThoiGianPhien = r.TotalSecond,
                r.QuangDuong,
                r.PathVideo,
                KhoaHocId = IDKhoaHoc
            });
            return result;
        }

        #region Thêm Khóa Học

        public int InsertKhoaHoc(KhoaHocCabin kh)
        {
            string query = "spAddKhoaHoc @MaKH , @TenKH , @NgayKG , @MaBC1 , @NgayBG , @NgaySatHach , @HangDaoTao , @NgayDaoTao";
            int result = _db.Execute(query, new
            {
                MaKH = kh.MaKhoaHoc,
                TenKH = kh.TenKhoaHoc,
                NgayKG = kh.NgayKhaiGiang,
                kh.MaBC1,
                NgayBG = kh.NgayBeGiang,
                kh.NgaySatHach,
                kh.HangDaoTao,
                kh.NgayDaoTao
            });
            return result;
        }

        public int GetKhoaHocIdByMaKhoaHoc(string MaKH)
        {
            try
            {
                const string query = "SELECT KhoaHocID FROM KhoaHoc WHERE MaKH = @MaKH";
                return _db.QuerySingle<int>(query, new { MaKH });
            }
            catch
            {
                return 0;
            }
        }

        public int InsertNguoiLxBase(int khoahocId, string tenkhoahoc, HocVien hv, byte[] bytesImg) 
        {
            string query = "spAddBaseNguoiLx @KhoaHocID , @TenKH , @MaDK , @HoDemNLX , @TenNLX , @HoVaTen , @NgaySinh , @GioiTinh , @QuocTich , @NoiCT , @SoCMT ,  @NgayCapCMT , @HangDaoTao , @HangGPLX , @Anh , @RFID , @SoTheRFID , @IsRFID ";

            // Dapper không đọc SqlParameter[]: dùng DynamicParameters (giữ kiểu varbinary cho ảnh, kể cả khi null)
            var parameters = new Dapper.DynamicParameters();
            parameters.Add("KhoaHocID", khoahocId.ToString(), DbType.String);
            parameters.Add("TenKH", tenkhoahoc, DbType.String);
            parameters.Add("MaDK", hv.MaDK, DbType.String);
            parameters.Add("HoDemNLX", hv.HoTenDem, DbType.String);
            parameters.Add("TenNLX", hv.Ten, DbType.String);
            parameters.Add("HoVaTen", hv.HoTenHV, DbType.String);
            parameters.Add("NgaySinh", hv.NgaySinh, DbType.DateTime);
            parameters.Add("GioiTinh", hv.GioiTinh, DbType.String);
            parameters.Add("QuocTich", hv.QuocTich, DbType.String);
            parameters.Add("NoiCT", "", DbType.String);
            parameters.Add("SoCMT", hv.SoCMT, DbType.String);
            parameters.Add("NgayCapCMT", hv.NgayCapCMT, DbType.DateTime);
            parameters.Add("HangDaoTao", hv.HangGPLX, DbType.String);
            parameters.Add("HangGPLX", hv.HangGPLX, DbType.String);
            parameters.Add("Anh", bytesImg, DbType.Binary);
            parameters.Add("RFID", "", DbType.AnsiString);
            parameters.Add("SoTheRFID", "", DbType.AnsiString);
            parameters.Add("IsRFID", false, DbType.Boolean);

            int result = _db.Execute(query, parameters);

            return result;
        }

        #endregion Thêm Khóa Học
    }
}