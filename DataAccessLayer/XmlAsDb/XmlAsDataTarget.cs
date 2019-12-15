using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using LocalStorage;

namespace DataAccessLayer
{
    /// <summary>
    /// Als Datenbankalternative, soll es eine offline Option geben.
    /// Lokal wird eine Xml-Datei gehalten die datenbankähnliche Funktionen erfüllen soll.
    /// Die Methoden rufen jeweils die implementierte Klasse XmlStorage auf und stellen Schreibbefehle dar.
    /// </summary>
    public class XmlAsDataTarget : IDataTarget
    {
        /// <summary>
        /// Greift auf ein Datenziel zu, um den übergebenen Podcast zu persistieren.
        /// </summary>
        /// <param name="rssUri">Rss Link eines Podcasts. Enthält eine Show mit vielen Episoden in Xml-Form</param>
        public void SavePodcast(string rssUri)
        {
            XmlStorage.Instance.ProcessNewPodcast(rssUri);
        }

        /// <summary>
        /// Greift auf ein Datenziel zu, um den Downloadpfad einer bestehenden Episode zu setzen.
        /// </summary>
        /// <param name="show">Parent der Episode, um die Episode zuzuordnen</param>
        /// <param name="episode">Episode, welche geupdated werden soll</param>
        /// <param name="path">Downloadpfad der Episode, der geschrieben werden soll</param>
        public void InsertDownloadPath(Show show, Episode episode, string path)
        {
            XmlStorage.Instance.SetDownloadPath(show, episode, path);
        }


    }
}
