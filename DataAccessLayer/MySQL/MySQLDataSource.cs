using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: RK
    /// </summary>
    public class MySQLDataSource : IDataSource
    {
        public List<Podcast> GetAllDownloadedPodcasts()
        {
            throw new NotImplementedException();
        }

        public List<Episode> getAllEpisodes()
        {
            throw new NotImplementedException();
        }

        public List<Episode> GetAllEpisodes(Show selectedShow)
        {
            throw new NotImplementedException();
        }

        public List<Show> GetAllShows(Show selectedShow)
        {
            throw new NotImplementedException();
        }

        public List<Show> GetAllShows()
        {
            throw new NotImplementedException();
        }
    }
}
