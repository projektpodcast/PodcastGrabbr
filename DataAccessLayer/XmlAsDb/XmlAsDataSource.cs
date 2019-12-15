using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using LocalStorage;
using RssFeedProcessor;

namespace DataAccessLayer
{
    /// <summary>
    /// Als Datenbankalternative, soll es eine offline Option geben.
    /// Lokal wird eine Xml-Datei gehalten die datenbankähnliche Funktionen erfüllen soll.
    /// Die Methoden rufen jeweils die implementierte Klasse XmlStorage auf und stellen Lesebefehle dar.
    /// </summary>
    public class XmlAsDataSource : IDataSource
    {
        public XmlAsDataSource()
        {
            ////string rssUri = "http://podcast.wdr.de/quarks.xml";
            //string rssUri = "http://joeroganexp.joerogan.libsynpro.com/rss";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/forum.xml";
            //string rssUri = "http://web.ard.de/radiotatort/rss/podcast.xml";

            //string rssUri = "https://feeds.br.de/das-computermagazin/feed.xml";
            //string rssUri = "http://www.deutschlandfunk.de/podcast-feature.1383.de.podcast.xml";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/wissen.xml";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/feature.xml";
            //string rssUri = "https://www.zdf.de/ZDFmediathek/podcast/222766?view=podcast";
            //string rssUri = "http://www.3sat.de/scobel/podcast/scobel_feed.xml";
            //XmlStorage.Instance.ProcessNewPodcast(rssUri);
        }
        /// <summary>
        /// Greift auf ein Datenziel zu um alle darin persistierten Shows zu erhalten
        /// </summary>
        /// <returns>Liste aller Shows</returns>
        public List<Show> GetAllShows()
        {
            return XmlStorage.Instance.GetShows();
        }

        /// <summary>
        /// Sucht alle zu einer übergebenen Show zugehörigen Episoden.
        /// </summary>
        /// <param name="selectedShow">Show, zu welcher die zugehörigen Episoden gesucht werden sollen.</param>
        /// <returns>Lisste aller Episoden einer spezifischen Show</returns>
        public List<Episode> GetAllEpisodes(Show selectedShow)
        {
            return XmlStorage.Instance.GetEpisodes(selectedShow);
        }

        /// <summary>
        /// Durchsucht das Datenziel nach Shows, die eine Episode enthalten, welche gedownloadet ist.
        /// </summary>
        /// <returns>Gruppiert alle gedownloadeten Episoden zu den zugehören Shows und schreibt sie in eine Liste</returns>
        public List<Podcast> GetAllDownloadedPodcasts()
        {
            return XmlStorage.Instance.GetDownloadedPodcasts();
        }
    }
}
