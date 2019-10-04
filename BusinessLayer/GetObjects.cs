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
        public List<Show> GetSeriesList(int sourceType)
        {
            IDataSource fileSource = Factory.CreateFileSource(sourceType);
            List<Show> allShowList = fileSource.GetAllSeries();
            return allShowList;
        }

        public List<Show> GetLocalMedia()
        {
            MediaDataSource dal = new MediaDataSource();

            dal.GetAllSeries();
            return null;
            
        }
    }
}
