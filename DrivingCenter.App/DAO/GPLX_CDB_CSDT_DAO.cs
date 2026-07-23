using Dapper;
using DrivingCenter.App.DataProvider;
using DrivingCenter.App.DTO.GPLX;
using DrivingCenter.App.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App.DAO
{
    public class GPLX_CDB_CSDT_DAO
    {
        private readonly DbProvider _db = new DbProvider(DatabaseType.db_Gplx);

        public List<KhoaHoc> GetlstKh()
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
                    CONVERT(VARCHAR(10), NgayBG, 103) AS NgayBG
                FROM 
                    KhoaHoc
                WHERE 
                    MaKH NOT IN (SELECT MaKH FROM BaoCaoI) AND TongSoHV IS NOT NULL";

            DataTable dt = _db.QueryToDataTable(query);
            var lstKH = dt.AsEnumerable()
                           .Select(row => new KhoaHoc
                           {
                               KhoaHocs = row.Field<string>("KhoaHoc"),
                               MaKH = row.Field<string>("MaKH"),
                               TenKH = row.Field<string>("TenKH"),
                               HangGPLX = row.Field<string>("HangGPLX"),
                               HangDT = row.Field<string>("HangDT"),
                               NgayKG = DateTime.TryParse(row.Field<string>("NgayKG"), out DateTime ngaykg) ? ngaykg : DateTime.MinValue,
                               NgayBG = DateTime.TryParse(row.Field<string>("NgayBG"), out DateTime ngaybg) ? ngaybg : DateTime.MinValue,
                           }).ToList();
            return lstKH;
        }

        public string GenerateMaDK()
        {
            string query = "SELECT dbo.CreateNewMaDK('66015') AS NewMaHV;";
            object result = _db.Execute(query);
            return result.ToString().Trim();
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
        public void UpdateResultGraduate(NguoiLX_HoSo_Update nguoiLXUpdate)
        {
            string query = string.Format("UPDATE NguoiLX_HoSo set " +
                "SoGiayCNTN = '{0}', TT_XuLy = '{1}', SoQuyetDinhTN = '{2}', NgayRaQDTN = '{3}', SoSoTN = '{4}', NgayVaoSoTN = '{5}', NgayInGiayTN = '{6}' where MaDK = '{7}'",
                nguoiLXUpdate.SoGiayCNTN, nguoiLXUpdate.TT_XuLy, nguoiLXUpdate.SoQuyetDinhTN, nguoiLXUpdate.NgayRaQDTN, nguoiLXUpdate.SoSoTN, nguoiLXUpdate.NgayVaoSoTN, nguoiLXUpdate.NgayInGiayTN, nguoiLXUpdate.MaDK);
            _db.Execute(query);
        }
        public int DeleteDataReportOld()
        {
            int status = 0;
            try
            {
                string query = string.Format("DELETE VNDC_ReportCNTN");
                return status = _db.Execute(query);
            }
            catch
            {
                return status;
            }
        }
        public void InsertDataReportCNTN(HvTotNghiep nguoiLx_CCTN)
        {
            string query = $@"EXEC [dbo].[VNDC_ReportCNTN_Insert]
                    @MaDK = '{nguoiLx_CCTN.MaDK}',
                    @NgayQDTN = '{nguoiLx_CCTN.NgayQDTN}'";

            _db.Execute(query);
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

        public DataTable GetDataPrintCCTN(string lstMaDK)
        {
            string query = $"SELECT * FROM VNDC_ReportCNTN WHERE MaDK IN (SELECT itemID FROM dbo.VNDC_Split('{@lstMaDK}', ',')) ORDER BY Id ASC";
            DataTable dt = _db.QueryToDataTable(query);
            return dt;
        }

        public void UpdateInfo(string madk, string hoten, string hotenEng, string ngaysinh, string ngaysinhEng, string gioitinh, string gioitinhEng, string sovaoso)
        {
            string query = $@"UPDATE VNDC_ReportCNTN SET HoVaTen = N'{hoten}', 
                NgaySinh = '{ngaysinh}', 
                GioiTinh = N'{gioitinh}', 
                HoVaTenEng = '{hotenEng}', 
                NgaySinhEng = '{ngaysinhEng}',
                GioiTinhEng = '{gioitinhEng}',
                SoVaoSo = '{sovaoso}'
                WHERE MaDK = '{madk}'";
            _db.Execute(query);
        }
    }
}
