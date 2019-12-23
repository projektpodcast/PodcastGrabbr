using System.Collections.Generic;

namespace CommonTypes
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Enthält benötigte Informationen um eine Datenbank-Verbindung zu öffnen.
    /// Das KeyValuePair enthält um welche Art von Datenziel es sich handelt.
    /// </summary>
    public interface IDataStorageType
    {
        string DataBaseName { get; set; }
        KeyValuePair<int, string> DataType { get; set; }
        string EncryptedPassword { get; set; }
        string Ip { get; set; }
        int Port { get; set; }
        string UserName { get; set; }
    }
}