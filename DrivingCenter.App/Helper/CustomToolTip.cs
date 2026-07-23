using GMap.NET.WindowsForms;
using System.Drawing;

namespace DrivingCenter.App.Helper
{
    public class CustomToolTip : GMapToolTip
    {
        public CustomToolTip(GMapMarker marker) : base(marker)
        {
            Font = new Font("Tahoma", 8, FontStyle.Bold);
            Foreground = Brushes.Blue;
            Fill = Brushes.White;
            Stroke = Pens.Blue;
        }
    }
}
