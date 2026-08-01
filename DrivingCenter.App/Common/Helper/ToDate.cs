using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.Common.Helper
{
    public class ToDate
    {
        public static DateTime ConvertToDate (string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DateTime.MinValue;

            if (DateTime.TryParseExact(
                    value.Trim(),
                    new[] { "ddMMyyyy", "yyyyMMdd" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
            {
                return date;
            }

            return DateTime.MinValue;
        }
    }
}
