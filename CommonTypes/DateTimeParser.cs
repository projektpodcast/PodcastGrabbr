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
        /// <summary>
        /// Versucht einen String in ein DateTime-Objekt zu parsen.
        /// Bei Misserfolg wird ein Default-Wert verwendet.
        /// </summary>
        /// <param name="dateTimeForParsing">Zeichenkette spiegelt ein Datum wieder. Dieser String geparsed werden</param>
        /// <returns>Valid geparstes DateTime Objekt</returns>
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

            // Versucht das in einem RSS-Feed meistverbreitete Datumformat zu parsen.
            if (DateTime.TryParseExact(dateInput, "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            // Versucht anhand einem Array mit Datumsformaten die DateTime zu parsen.
            if (DateTime.TryParseExact(dateInput, formats, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            // Bei gescheitertem Parsing wird ein Default-Wert zurückgegeben.
            else
            {
                return DateTime.Parse("1999-01-01 00:00:00");
            }
        }
    }
}
