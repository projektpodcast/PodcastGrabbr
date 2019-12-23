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
    /// Definiert Methoden, welche benötigt werden um ein datenbankähnliche Schreibbefehle an einem Datenziel durchzuführen.
    /// </summary>
    public interface IDataTarget
    {
        /// <summary>
        /// Greift auf ein Datenziel zu, um den übergebenen Podcast zu persistieren.
        /// </summary>
        /// <param name="rssUri">Rss Link eines Podcasts. Enthält eine Show mit vielen Episoden in Xml-Form</param>
        void SavePodcast(string rssUri);

        /// <summary>
        /// Greift auf ein Datenziel zu, um den Downloadpfad einer bestehenden Episode zu setzen.
        /// </summary>
        /// <param name="show">Parent der Episode, um die Episode zuzuordnen</param>
        /// <param name="episode">Episode, welche geupdated werden soll</param>
        /// <param name="path">Downloadpfad der Episode, der geschrieben werden soll</param>
        void InsertDownloadPath(Show show, Episode episode, string path);
        ///weitere operations wie update, create, insert, select(suchparameter), ...
        
        void BulkCopyPodcasts(List<string> rssUriList);
    }
}
