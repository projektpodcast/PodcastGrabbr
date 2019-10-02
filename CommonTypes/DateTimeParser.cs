using System;
using System.Collections.Generic;
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
            DateTime parsedDate;
            if (DateTime.TryParse(dateInput, out parsedDate))
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
