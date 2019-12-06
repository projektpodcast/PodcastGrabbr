using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using RssFeedProcessor;

namespace DataAccessLayer.XmlAsDb
{
    public class XmlAsDataSource : IDataSource
    {
        public List<Episode> GetAllEpisodes(Show selectedShow)
        {
            LocalRssTest local = new LocalRssTest();
            return local.GetEpisodes(selectedShow);
        }

        public List<Show> GetAllShows()
        {
            LocalRssTest local = new LocalRssTest();
            return local.GetShows();
        }

        public List<Episode> GetNext(Show selectedShow, Episode lastEpisode)
        {
            LocalRssTest local = new LocalRssTest();
            return local.GetNext(selectedShow, lastEpisode);
        }
    }
}
