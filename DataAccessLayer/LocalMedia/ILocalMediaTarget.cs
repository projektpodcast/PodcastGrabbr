using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Legt Methoden nach außen Frei, welche benötigt werden um Medien-Dateien zu persistieren.
    /// </summary>
    public interface ILocalMediaTarget
    {
        /// <summary>
        /// Methode ist asynchron, da die Dauer des Downloads unbekannt ist und ansonsten die Anwendung auf unbestimmte Zeit absperrt.
        /// </summary>
        /// <param name="show">Benötigt um den Dateipfad zu bestimmen</param>
        /// <param name="episode">Episode enthält Downloadlink, welcher heruntergeladen werden soll.</param>
        /// <returns></returns>
        Task<string> DownloadEpisode(Show show, Episode episode, IProgress<int> progress);
    }
}
