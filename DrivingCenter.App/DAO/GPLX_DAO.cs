using Dapper;
using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.GPLX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.DAO
{
    public class GPLX_DAO
    {
        private readonly DbProvider _db = new DbProvider(Enums.DatabaseType.db_Gplx);

        public List<KhoaHocGplx> GetlstKh()
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
                    CONVERT(VARCHAR(10), NgayBG, 103) AS NgayBG,
                    TongSoHV
                FROM 
                    KhoaHoc
                WHERE 
                    MaKH NOT IN (SELECT MaKH FROM BaoCaoI) AND TongSoHV IS NOT NULL";

            DataTable dt = _db.QueryToDataTable(query);
            var lstKH = dt.AsEnumerable()
                           .Select(row => new KhoaHocGplx
                           {
                               KhoaHocs = row.Field<string>("KhoaHoc"),
                               MaKH = row.Field<string>("MaKH"),
                               TenKH = row.Field<string>("TenKH"),
                               HangGPLX = row.Field<string>("HangGPLX"),
                               HangDT = row.Field<string>("HangDT"),
                               NgayKG = DateTime.TryParseExact(row.Field<string>("NgayKG"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaykg) ? ngaykg : DateTime.MinValue,
                               NgayBG = DateTime.TryParseExact(row.Field<string>("NgayBG"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaybg) ? ngaybg : DateTime.MinValue,
                               TongSoHV = row["TongSoHV"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["TongSoHV"]),
                           }).ToList();
            return lstKH;
        }

        public List<KhoaHocGplx> GetAllKhoaHoc()
        {
            var dt1 = _db.QueryToDataTable($"SELECT MaKH, TenKH, HangGPLX, HangDT, NgayKG, NgayBG FROM KhoaHoc WHERE YEAR(NgayKG) >= 2025");
            var lstKH = dt1.AsEnumerable()
                           .Select(row => new KhoaHocGplx
                           {
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
            const string query = "SELECT * FROM KhoaHoc WHERE MaKH = @MaKH";
            DataTable dt = _db.QueryToDataTable(query, new { MaKH });
            return dt.Rows[0];
        }

        public string GenerateMaDK()
        {
            string query = "SELECT dbo.CreateNewMaDK('66015') AS NewMaHV;";
            object result = _db.Execute(query);
            return result.ToString().Trim();
        }

        //Giấy xác nhận hoàn thành khóa học

        //public DataTable GetDataPrintCCTN(string lstId)
        //{
        //    string query = string.Format("SELECT * FROM ReportCNTN WHERE Id IN (SELECT itemID FROM Split('{0}', ',')) ORDER BY SoCCTN ASC", lstId);
        //    return dt;
        //}

        public void UpdateResultGraduate(NguoiLX_HoSo_Update nguoiLXUpdate)
        {
            _db.Execute(CntnSql.UpdateHoSo, CntnSql.HoSoParams(nguoiLXUpdate));
        }
        public int DeleteDataReportOld()
        {
            int status = 0;
            try
            {
                const string query = "DELETE VNDC_ReportCNTN";
                return status = _db.Execute(query);
            }
            catch
            {
                return status;
            }
        }

        public List<HvTotNghiep> GetHvTotNghiep()
        {
            List<HvTotNghiep> lst = new List<HvTotNghiep>();
            DataTable dt = _db.QueryToDataTable("SELECT * FROM VNDC_ReportCNTN ORDER BY SoCCTN ASC");

            lst = dt.AsEnumerable().Select(item => new HvTotNghiep
            {
                Id = (int)item["Id"],
                MaDK = item["MaDK"].ToString(),
                HoVaTen = item["HoVaTen"].ToString(),
                NgaySinh = item["NgaySinh"].ToString(),
                GioiTinh = item["GioiTinh"].ToString(),
                HangGPLX = item["HangDT"].ToString(),
                ThoiGianDT = item["ThoiGianDT"] == DBNull.Value ? 0 : (int)item["ThoiGianDT"],
                NgayKG = item["NgayKG"].ToString(),
                NgayBG = item["NgayBG"].ToString(),
                SoCCTN = item["SoCCTN"].ToString(),
                SoVaoSo = item["SoVaoSo"].ToString()
            }).OrderBy(x => x.SoCCTN).ToList();
            return lst;
        }

        public void InsertDataReportCNTN(HvTotNghiep nguoiLx_CCTN)
        {
            _db.Execute(CntnSql.InsertReport, CntnSql.InsertParams(nguoiLx_CCTN));
        }

        public void UpdateInfo(string madk, string hoten, string hotenEng, string ngaysinh, string ngaysinhEng, string gioitinh, string gioitinhEng, string sovaoso)
        {
            _db.Execute(CntnSql.UpdateInfo, CntnSql.InfoParams(madk, hoten, hotenEng, ngaysinh, ngaysinhEng, gioitinh, gioitinhEng, sovaoso));
        }

        public DataTable GetDataPrintCCTN(string lstMaDK)
        {
            const string query = CntnSql.SelectPrint;
            DataTable dt = _db.QueryToDataTable(query, new { lstMaDK = lstMaDK ?? "" });
            return dt;
        }

        private void InsertHocVien(IDbConnection conn, IDbTransaction tran, NguoiLxDto nlx)
        {
            conn.Execute(
                "VNDC_spInsertHocVien",
                new
                {
                    p_out = 0,
                    p_MaKH = nlx.MaKH,
                    p_MaHangGPLX = nlx.MaHangGPLX,
                    p_HangDaoTao = nlx.HangDaoTao,
                    p_MaDK = (string)null,
                    p_SoHoSo = nlx.SoHoSo,
                    p_NguoiNhanHSo = nlx.NguoiNhanHSo,
                    p_HoVaTen = nlx.HoVaTen,
                    p_HoVaTenIn = nlx.HoVaTenIn,
                    p_NgaySinh = nlx.NgaySinh,
                    p_GioiTinh = nlx.GioiTinh,
                    p_MaQuocTich = nlx.MaQuocTich,
                    p_SoCMT = nlx.SoCMT,
                    p_NgayCap = nlx.NgayCap,
                    p_NoiCap = nlx.NoiCap,
                    p_MaDVHC_TT = nlx.MaDVHC_TT,
                    p_ChiTiet_TT = nlx.ChiTiet_TT,
                    p_MaDVHC_CT = nlx.MaDVHC_CT,
                    p_ChiTiet_CT = nlx.ChiTiet_CT,
                    p_SoNamLX = nlx.SoNamLX,
                    p_SoKmLX = nlx.SoKmLX,
                    p_GiayTo = nlx.GiayTo,
                    p_SoGPLXDaCo = nlx.SoGPLXDaCo,
                    p_HangGPLXDaCo = nlx.HangGPLXDaCo,
                    p_NgayTTGPLXDaCo = nlx.NgayTTGPLXDaCo,
                    p_NgayCapGPLXDaCo = nlx.NgayCapGPLXDaCo,
                    p_NgayHHGPLXDaCo = nlx.NgayHHGPLXDaCo,
                    p_DVCapGPLXDaCo = nlx.DVCapGPLXDaCo,
                    p_NoiCapGPLXDaCo = nlx.NoiCapGPLXDaCo,
                    p_GhiChu = nlx.GhiChu,
                    p_SO_CMND_CU = nlx.SO_CMND_CU,
                    p_CHON_IN_GPLX = nlx.CHON_IN_GPLX
                },
                transaction: tran,
                commandType: CommandType.StoredProcedure);

        }

        public bool InsertHocVien(List<NguoiLxDto> lstNguoiLX)
        {
            try
            {
                _db.ExecuteTransaction((conn, tran) =>
                {
                    foreach (var item in lstNguoiLX)
                    {
                        try
                        {
                            InsertHocVien(conn, tran, item);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(
                                $"Học viên '{item.HoVaTen}' \n " +
                                $"Ngày sinh: {item.NgaySinh} \n" +
                                $"(Số hồ sơ: {item.SoHoSo}) \n" +
                                $"lỗi: {ex.Message}",
                                ex);
                        }
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        #region Xếp Lớp

        /// <summary>Khóa dự bị: học viên chờ xếp lớp nằm ở khóa này.</summary>
        public const string MaKhoaDuBi = "66015K22DB001";

        public List<NguoiLX> Get_lstNguoiLxByMaKh(string MaKH)
        {
            // Liệt kê cột rõ ràng (SELECT * nối 3 bảng bị trùng tên cột GhiChu / TrangThai / NguoiTao...)
            const string query = @"SELECT nlx.MaDK, nlx.HoVaTen, nlx.HoVaTenIn, nlx.NgaySinh, nlx.GioiTinh, nlx.MaQuocTich,
                               nlx.SoCMT, nlx.NgayCapCMT, nlx.NoiCapCMT, nlx.NoiTT, nlx.NoiCT,
                               hs.SoHoSo, hs.HangGPLX, hs.DuongDanAnh, hs.SoNamLX, hs.SoKmLXAnToan, hs.GhiChu
                        FROM NguoiLX AS nlx
                        INNER JOIN NguoiLX_HoSo AS hs ON nlx.MaDK = hs.MaDK
                        WHERE hs.MaKhoaHoc = @MaKH";

            DataTable dt = _db.QueryToDataTable(query, new { MaKH });

            string S(DataRow r, string c) => r[c] == DBNull.Value ? "" : Convert.ToString(r[c]).Trim();

            return dt.AsEnumerable().Select(item =>
            {
                string ngaySinh = S(item, "NgaySinh");
                string ngayCap = S(item, "NgayCapCMT");
                return new NguoiLX
                {
                    MaDK = S(item, "MaDK"),
                    HoVaTen = S(item, "HoVaTen"),
                    HoVaTenIn = S(item, "HoVaTenIn"),
                    NgaySinh = ngaySinh,
                    // Ngày sinh lưu dạng yyyyMMdd; trước đây ParseExact lỗi cả danh sách khi có 1 hồ sơ thiếu ngày sinh
                    NgaySinhDateTime = DateTime.TryParseExact(ngaySinh, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ns) ? ns : DateTime.MinValue,
                    GioiTinh = S(item, "GioiTinh") == "F" ? "Nữ" : "Nam",
                    MaQuocTich = S(item, "MaQuocTich"),
                    SoCMT = S(item, "SoCMT"),
                    NgayCapCMT = ngayCap.Length >= 10 ? ngayCap.Substring(0, 10) : ngayCap,
                    NoiCapCMT = S(item, "NoiCapCMT"),
                    NoiTT = S(item, "NoiTT"),
                    NoiCT = S(item, "NoiCT"),
                    GhiChu = S(item, "GhiChu"),
                    SoHoSo = S(item, "SoHoSo"),
                    HangGPLX = S(item, "HangGPLX"),
                    DuongDanAnh = S(item, "DuongDanAnh"),
                    SoNamLX = S(item, "SoNamLX"),
                    SoKmLXAnToan = S(item, "SoKmLXAnToan")
                };
            }).OrderBy(x => x.MaDK).ToList();
        }

        // 3 hàm đọc 1 giá trị: trước đây dùng _db.Execute (trả về số dòng bị ảnh hưởng = -1 với SELECT),
        // nên số hồ sơ mới luôn là "000" và ảnh không bao giờ được chuyển sang thư mục của khóa mới.
        public string GetImgPath()
        {
            return _db.QuerySingle<string>("SELECT TOP 1 GiaTriTS FROM QTHT_ThamSoHT WHERE TenTS = 'IMG_PATH_CSDT'") ?? string.Empty;
        }

        public string GetImgPathByMaDK(string maDK)
        {
            return _db.QuerySingle<string>("SELECT TOP 1 DuongDanAnh FROM NguoiLX_HoSo WHERE MaDK = @maDK", new { maDK }) ?? string.Empty;
        }

        public string GetMaxSoHSByMaKKH(string makh)
        {
            return _db.QuerySingle<string>("SELECT MAX(SoHoSo) FROM NguoiLX_HoSo WHERE MaKhoaHoc = @makh", new { makh }) ?? "0";
        }

        /// <summary>
        /// Chuyển học viên sang khóa <paramref name="MaKH"/>: cấp số hồ sơ tiếp theo của khóa và chuyển ảnh chân dung
        /// sang thư mục của khóa. Trả về cảnh báo (vd. không chuyển được ảnh) hoặc null nếu trọn vẹn.
        /// </summary>
        public string UpdateNguoiLxToKhoaHoc(NguoiLX nguoiLx, string MaKH)
        {
            int maxSoHS = int.TryParse(GetMaxSoHSByMaKKH(MaKH), out int val) ? val : 0;
            string soHS = (maxSoHS + 1).ToString("D3");
            string canhBao = null;

            string oldImagePath = GetImgPathByMaDK(nguoiLx.MaDK);
            string newImagePath = null;
            if (!string.IsNullOrEmpty(oldImagePath) && File.Exists(oldImagePath))
            {
                string imgRoot = GetImgPath();
                if (string.IsNullOrEmpty(imgRoot))
                    canhBao = "chưa cấu hình thư mục ảnh (IMG_PATH_CSDT) nên ảnh giữ nguyên chỗ cũ";
                else
                {
                    string newFolder = Path.Combine(imgRoot, MaKH);
                    string target = Path.Combine(newFolder, Path.GetFileName(oldImagePath));
                    if (!string.Equals(Path.GetFullPath(target), Path.GetFullPath(oldImagePath), StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            Directory.CreateDirectory(newFolder);
                            if (File.Exists(target)) File.Delete(target);
                            File.Move(oldImagePath, target);
                            newImagePath = target;
                        }
                        catch (Exception ex)
                        {
                            canhBao = "không chuyển được ảnh: " + ex.Message;
                        }
                    }
                }
            }

            try
            {
                if (newImagePath != null)
                    _db.Execute("UPDATE NguoiLX_HoSo SET MaKhoaHoc = @MaKH, SoHoSo = @soHS, DuongDanAnh = @newImagePath WHERE MaDK = @MaDK",
                        new { MaKH, soHS, newImagePath, nguoiLx.MaDK });
                else
                    _db.Execute("UPDATE NguoiLX_HoSo SET MaKhoaHoc = @MaKH, SoHoSo = @soHS WHERE MaDK = @MaDK",
                        new { MaKH, soHS, nguoiLx.MaDK });
            }
            catch
            {
                // Lưu CSDL lỗi: trả ảnh về chỗ cũ để đường dẫn trong hồ sơ vẫn đúng
                if (newImagePath != null)
                    try { File.Move(newImagePath, oldImagePath); } catch { }
                throw;
            }
            return canhBao;
        }
        #endregion
    }
}