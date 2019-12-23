using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Definiert Methoden, welche benötigt werden um Medien-Dateien abzuspielen 
    /// </summary>
    public class MediaDataSource : ILocalMediaSource
    {
        /// <summary>
        /// Methode ruft eine Methode auf um die übergebene Methode auf dem Gerät abzuspielen.
        /// </summary>
        /// <param name="episode">Enthält den zur Episode gehörigen Pfad, welcher abgespielt werden soll</param>
        public void PlayLocalMedia(Episode episode)
        {
            //File.Open(episode.DownloadPath, FileMode.Open);
            System.Diagnostics.Process.Start(episode.DownloadPath);
        }

    }
}
