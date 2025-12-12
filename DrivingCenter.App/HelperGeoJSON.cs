using DevExpress.XtraEditors;
using DocumentFormat.OpenXml.Features;
using DrivingCenter.App.Helper;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class HelperGeoJSON : DevExpress.XtraEditors.XtraForm
    {
        public HelperGeoJSON()
        {
            InitializeComponent();
        }

        private void btn_GetProvinces_Click(object sender, EventArgs e)
        {
            string tentinh = txt_NameProvince.Text;
            var geom = GeoHelper.GetProvinceGeometry(@"C:\Users\TrungHieu\Downloads\diaphantinh.geojson", tentinh);

            if (geom != null)
                XtraMessageBox.Show($"Đã đọc polygon {tentinh} thành công!");
            else
                XtraMessageBox.Show("Không tìm thấy tỉnh!");

            InsertBoundary(tentinh, GeoHelper.GeometryToWkt(geom));
        }

        void InsertBoundary(string provinceName, string wkt)
        {
            using (var conn = new SqlConnection("Data Source=192.168.50.253;Initial Catalog=I2EDAT2023;User ID=it;Password=@97UiLYW0Fwkl3qQWK($4My^"))
            {
                conn.Open();
                string sql = @"
            INSERT INTO ProvinceBoundary(Name, Boundary)
            VALUES (@name, geometry::STGeomFromText(@wkt, 4326));";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", provinceName);
                    cmd.Parameters.AddWithValue("@wkt", wkt);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}