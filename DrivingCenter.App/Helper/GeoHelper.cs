using System;
using System.IO;
using NetTopologySuite.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace DrivingCenter.App.Helper
{
    public class GeoHelper
    {
        // Trả về Geometry (Polygon / MultiPolygon) của tỉnh nếu tìm thấy, ngược lại null
        public static Geometry GetProvinceGeometry(string geoJsonFilePath, string provinceName)
        {
            if (!File.Exists(geoJsonFilePath))
                throw new FileNotFoundException("GeoJSON file not found.", geoJsonFilePath);

            // Tạo GeoJson serializer từ NetTopologySuite
            var serializer = GeoJsonSerializer.Create();

            FeatureCollection featureCollection;
            using (var sr = new StreamReader(geoJsonFilePath, System.Text.Encoding.UTF8))
            using (var jtr = new JsonTextReader(sr))
            {
                featureCollection = serializer.Deserialize<FeatureCollection>(jtr);
            }

            if (featureCollection == null)
                throw new Exception("Không đọc được FeatureCollection từ file GeoJSON.");

            foreach (var feature in featureCollection)
            {
                // Tên thuộc tính có thể khác: 'ten_tinh', 'NAME', 'NAME_EN', v.v.
                string[] possibleKeys = new[] { "ten_tinh", "NAME", "name", "TenTinh", "ten" };

                foreach (var key in possibleKeys)
                {
                    if (feature.Attributes.Exists(key))
                    {
                        var val = feature.Attributes[key]?.ToString();
                        if (!string.IsNullOrEmpty(val) &&
                            string.Equals(val.Trim(), provinceName.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            // Đặt SRID nếu cần (GeoJSON thường WGS84)
                            var geom = feature.Geometry;
                            if (geom != null)
                            {
                                geom.SRID = 4326;
                            }
                            return geom;
                        }
                    }
                }
            }
            return null; // không tìm thấy
        }

        public static string GeometryToWkt(Geometry g)
        {
            if (g == null) return null;
            var writer = new WKTWriter();
            return writer.Write(g);
        }
    }
}
