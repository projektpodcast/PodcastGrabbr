using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class DataType
    {
        public KeyValuePair<int, string> TypeIdentifier { get; set; }

        public DataType()
        {
            Dictionary<int, string> possibleTypes = new Dictionary<int, string>
            {
                { 0, "Lokal: Xml" },
                { 1, "Datenbank: MySQL" },
                { 2, "Datenbank: PostgreSQL" }
            };
        }

        public Dictionary<int, string> GetPossibleDataTypes()
        {
            Dictionary<int, string> possibleTypes = new Dictionary<int, string>
            {
                { 0, "Lokal: Xml" },
                { 1, "Datenbank: MySQL" },
                { 2, "Datenbank: PostgreSQL" }
            };

            return possibleTypes;
        }
    }
}
