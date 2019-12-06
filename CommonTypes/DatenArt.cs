using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class DataStorageType : IDataStorageType
    {
        public KeyValuePair<int, string> DataType { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string DataBaseName { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
