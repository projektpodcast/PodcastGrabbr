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
    public class XmlAsDataSource : IDataSource
    {
        public XmlAsDataSource()
        {
            ////string rssUri = "http://podcast.wdr.de/quarks.xml";
            //string rssUri = "http://joeroganexp.joerogan.libsynpro.com/rss";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/forum.xml";
            string rssUri = "http://web.ard.de/radiotatort/rss/podcast.xml";

            //string rssUri = "https://feeds.br.de/das-computermagazin/feed.xml";
            //string rssUri = "http://www.deutschlandfunk.de/podcast-feature.1383.de.podcast.xml";
            //string rssUri = "http://podcast.wdr.de/quarks.xml";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/wissen.xml";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/feature.xml";
            //string rssUri = "https://www.zdf.de/ZDFmediathek/podcast/222766?view=podcast";
            //string rssUri = "http://www.3sat.de/scobel/podcast/scobel_feed.xml";
            XmlStorage.Instance.ProcessNewPodcast(rssUri);
        }
        public List<Episode> GetAllEpisodes(Show selectedShow)
        {
            return XmlStorage.Instance.GetEpisodes(selectedShow);
            //LocalRssTest local = new LocalRssTest();
            //return local.GetEpisodes(selectedShow);
        }

        public List<Show> GetAllShows()
        {
            return XmlStorage.Instance.GetShows(); 
            //LocalRssTest local = new LocalRssTest();
            //return local.GetShows();
        }

    }
}
