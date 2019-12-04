using CommonTypes;
using DataAccessLayer;
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
            IDataSource dataSource = Factory.CreateDataSource();
            return dataSource.GetAllShows();

            //MockDataSource dal = new MockDataSource();

            //List<Show> a = dal.GetAllShows();
            //return a;

        }

        public void Test()
        {
            IDataTarget fileTarget = Factory.CreateDataTarget();
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
            IDataSource dataSource = Factory.CreateDataSource();
            return dataSource.GetAllEpisodes(selectedShow);
        }
    }
}
