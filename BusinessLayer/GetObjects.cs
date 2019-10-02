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
        public List<Show> GetSeriesList()
        {
            FileDataSource fileSource = Factory.CreateFileSource();
            List<Show> allShowList = fileSource.GetAllSeries();
            return allShowList;
        }
    }
}
