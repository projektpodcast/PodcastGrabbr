using CommonTypes;
using DataAccessLayer;
using DataAccessLayer.XmlAsDb;
using RssFeedProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class GetObjects
    {
        public GetObjects()
        { }
        public List<Show> GetShowList()
        {
            //IDataSource dataSource = Factory.CreateDataSource();
            //return dataSource.GetAllShows();

            LocalRssTest rss = new LocalRssTest();
            rss.ProcessNewPodcast();
            XmlAsDataSource dal = new XmlAsDataSource();
            
            return dal.GetAllShows();
        }

        public List<Show> GetLocalMedia() //umschreiben, gibt falschen Typ zurück
        {
            MediaDataSource dal = new MediaDataSource();

            dal.GetAllShows();
            return null;
            
        }

        public List<Podcast> GetMockDownloadedPodcasts()
        {
            MockDataSource dal = new MockDataSource();

            return dal.GetDownloadedPodcasts();
        }

        public List<Episode> GetEpisodes(Show selectedShow)
        {
            //IDataSource dataSource = Factory.CreateDataSource();
            //return dataSource.GetAllEpisodes(selectedShow);

            XmlAsDataSource dal = new XmlAsDataSource();
            return dal.GetAllEpisodes(selectedShow);
        }

        public List<Episode> GetNextEpisodes(Show selectedShow, Episode lastEpisode)
        {
            XmlAsDataSource dal = new XmlAsDataSource();
            return dal.GetNext(selectedShow, lastEpisode);
        }
    }
}
