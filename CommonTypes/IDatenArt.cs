using System.Collections.Generic;

namespace CommonTypes
{
    public interface IDatenArt
    {
        string DataBaseName { get; set; }
        KeyValuePair<int, string> DataType { get; set; }
        string EncryptedPassword { get; set; }
        string Ip { get; set; }
        int Port { get; set; }
        string UserName { get; set; }
    }
}