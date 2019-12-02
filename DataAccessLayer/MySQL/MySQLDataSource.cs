using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace DataAccessLayer
{
    public class MySQLDataSource : IDataSource
    {
        public List<Episode> getAllEpisodes()
        {
            throw new NotImplementedException();
        }

        public List<Episode> getAllEpisodes(Show selectedShow)
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
