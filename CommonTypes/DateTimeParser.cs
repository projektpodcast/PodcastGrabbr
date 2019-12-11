using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class DateTimeParser
    {
        public DateTime ConvertStringToDateTime(string dateTimeForParsing)
        {
            string dateInput = dateTimeForParsing;
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                   "MM/d/yyyy HH:mm:ss.ffffff", "ddd, dd MMM yyyy HH:mm:ss zzz" };
            DateTime parsedDate;
            DateTimeFormatInfo dtfi = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            //if (DateTime.TryParseExact(dateInput, "ddd, dd MMM yyyy HH':'mm':'ss zzz", dtfi, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out parsedDate))
            //{
            //    return parsedDate;
            //}
            if (DateTime.TryParseExact(dateInput, "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            if (DateTime.TryParseExact(dateInput, formats, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return DateTime.Parse("1999-01-01 00:00:00");
            }
        }
    }
}
