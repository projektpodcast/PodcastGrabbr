using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using LocalStorage;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Definiert Methoden, welche benötigt werden um Medien-Dateien zu persistieren.
    /// </summary>
    public class MediaDataTarget : ILocalMediaTarget
    {
        /// <summary>
        /// Methode ist asynchron, da die Dauer des Downloads unbekannt ist und ansonsten die Anwendung auf unbestimmte Zeit absperrt.
        /// </summary>
        /// <param name="show">Benötigt um den Dateipfad zu bestimmen</param>
        /// <param name="episode">Episode enthält Downloadlink, welcher heruntergeladen werden soll.</param>
        public async Task<string> DownloadEpisode(Show show, Episode episode, IProgress<int> progress)
        {
            try
            {
                MediaStorage mediaDl = new MediaStorage();
                return await mediaDl.InitializeMediaDownload(show, episode, progress);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
