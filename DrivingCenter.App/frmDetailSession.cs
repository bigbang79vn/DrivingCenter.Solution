using DrivingCenter.App.DAO;
using DrivingCenter.App.DTO.DAT;
using DrivingCenter.App.Helper;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmDetailSession : DevExpress.XtraEditors.XtraForm
    {
        private string _diemdanhnguoilxId;
        private List<PointLatLng> _points;

        public frmDetailSession(string diemdanhnguoilxId)
        {
            InitializeComponent();
            _diemdanhnguoilxId = diemdanhnguoilxId;
        }

        private GMapOverlay markersOverlay;
        private string timeStart;
        private string timeEnd;

        private void frmDetailSession_Load(object sender, EventArgs e)
        {
            InitializeMap(_diemdanhnguoilxId);
            GetHistoryData(_diemdanhnguoilxId);
            GetImgLogInLogOut(_diemdanhnguoilxId);
            GetVehicleHistory(_diemdanhnguoilxId);
        }

        private void GetImgLogInLogOut(string _diemdanhnguoilxId)
        {
            DataTable dt = IMG_DAO.Instance.GetImgById(_diemdanhnguoilxId);

            byte[] ImgLogin = (byte[])dt.Rows[0]["AnhChup"];
            using (var ms = new MemoryStream(ImgLogin, 0, ImgLogin.Length))
            {
                Img_Login.Image = Image.FromStream(ms, true);
                Img_Login.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            byte[] ImgLogout = dt.Rows[0]["AnhChup_Logout"] == DBNull.Value ? null : (byte[])dt.Rows[0]["AnhChup_Logout"];
            if (ImgLogout != null)
            {
                using (var ms = new MemoryStream(ImgLogout, 0, ImgLogout.Length))
                {
                    Img_Logout.Image = Image.FromStream(ms, true);
                    Img_Logout.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                Img_Logout.Image = Properties.Resources.Null;
                Img_Logout.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void GetHistoryData(string _diemdanhnguoilxId)
        {
            List<ChiTietPhienHoc> lst = DAT_DAO.Instance.GetHistoryData(_diemdanhnguoilxId);

            foreach (var item in lst)
            {
                double time = item.ThoiGian;
                TimeSpan t = TimeSpan.FromSeconds(time);
                item.ThoiGianText = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            }
            gridControl_HistoryData.DataSource = lst;
        }

        private void GetVehicleHistory(string _diemdanhnguoilxId)
        {
            List<ChiTietPhienHoc> lst = DAT_DAO.Instance.GetVehicleHistory(_diemdanhnguoilxId);

            foreach (var item in lst)
            {
                double time = item.ThoiGian;
                TimeSpan t = TimeSpan.FromSeconds(time);
                item.ThoiGianText = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            }

            gridControl_VehicleHistory.DataSource = lst;
        }

        private void InitializeMap(string _diemdanhnguoilxId)
        {
            _points = DAT_DAO.Instance.GetlstPoint(_diemdanhnguoilxId);
            DataTable dt = DAT_DAO.Instance.GetDetailSession(_diemdanhnguoilxId);

            var firstRow = dt.AsEnumerable().FirstOrDefault();
            if (firstRow != null)
            {
                txt_PhienHoc.Text = firstRow.Field<Guid>("DiemDanhNguoiLxID").ToString();
                txt_HocVien.Text = firstRow.Field<string>("HoVaTen").ToString();
                txt_KhoaHoc.Text = firstRow.Field<string>("TenKH").ToString();
                txt_GiaoVien.Text = firstRow.Field<string>("TenGV").ToString();
                txt_BienSo.Text = firstRow.Field<string>("BienSo").ToString();
                txt_QuangDuong.Text = Math.Round(firstRow.Field<double>("TongQuangDuongLD_Phien"), 3).ToString();
                TimeSpan t = TimeSpan.FromSeconds(firstRow.Field<double>("TongThoiGianLD_Phien"));
                txt_ThoiGian.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
                timeStart = firstRow.Field<DateTime>("ThoiDiem").ToString("HH:mm:ss dd/MM/yyyy");
                lb_TimeIn.Text = timeStart;
                timeEnd = firstRow.Field<DateTime>("ThoiDiem_Logout").ToString("HH:mm:ss dd/MM/yyyy");
                lb_TimeOut.Text = timeEnd;
            }

            var firstPoint = _points[0];
            var lastPoint = _points.Last();

            // Cấu hình GMapControl
            gmapControl.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmapControl.MinZoom = 1;
            gmapControl.MaxZoom = 20;
            gmapControl.Zoom = 15;

            // Tạo lớp overlay để chứa các marker và routes
            markersOverlay = new GMapOverlay("markers");

            // Thêm marker đầu
            GMarkerGoogle startMarker = new GMarkerGoogle(new PointLatLng(firstPoint.Lat, firstPoint.Lng), GMarkerGoogleType.blue_dot)
            {
                ToolTipText = $"Bắt đầu: {timeStart:HH:mm:ss}",
                ToolTipMode = MarkerTooltipMode.Always
            };
            startMarker.ToolTip = new CustomToolTip(startMarker);

            markersOverlay.Markers.Add(startMarker);

            // Thêm marker cuối
            GMarkerGoogle endMarker = new GMarkerGoogle(new PointLatLng(lastPoint.Lat, lastPoint.Lng), GMarkerGoogleType.red_dot)
            {
                ToolTipText = $"Kết thúc: {timeEnd:HH:mm:ss}",
                ToolTipMode = MarkerTooltipMode.Always
            };
            endMarker.ToolTip = new CustomToolTip(endMarker);
            markersOverlay.Markers.Add(endMarker);

            AddRoute(_points);

            // Thêm overlay vào bản đồ
            gmapControl.Overlays.Add(markersOverlay);

            gmapControl.Position = new PointLatLng(firstPoint.Lat, firstPoint.Lng); // Đặt vị trí ban đầu
        }

        private void AddRoute(List<PointLatLng> points)
        {
            GMapRoute r = new GMapRoute(points, "My routes")
            {
                Stroke = new Pen(Color.Blue, 3)
            };
            markersOverlay.Routes.Add(r);
        }

        private void gridView_VehicleHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int focusedRowHandle = gridView_VehicleHistory.FocusedRowHandle;

            double Lat = (double)gridView_VehicleHistory.GetRowCellValue(focusedRowHandle, "Lat");
            double Lng = (double)gridView_VehicleHistory.GetRowCellValue(focusedRowHandle, "Lng");

            gmapControl.Position = new PointLatLng(Lat, Lng);
        }

        private void update_ImageLoout_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_HistoryData.FocusedRowHandle;
            Guid _historyID = (Guid)gridView_HistoryData.GetRowCellValue(focusedRowHandle, "Id");
            DAT_DAO.Instance.UpdateImageLogout(_diemdanhnguoilxId, _historyID);
            GetImgLogInLogOut(_diemdanhnguoilxId);
        }

        private void update_ImageLogin_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridView_HistoryData.FocusedRowHandle;
            Guid _historyID = (Guid)gridView_HistoryData.GetRowCellValue(focusedRowHandle, "Id");
            DAT_DAO.Instance.UpdateImageLogin(_diemdanhnguoilxId, _historyID);
            GetImgLogInLogOut(_diemdanhnguoilxId);
        }
    }
}