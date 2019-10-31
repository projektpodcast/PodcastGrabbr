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
        public List<Show> GetShowList(/*int sourceType*/)
        {
            //IDataSource fileSource = Factory.CreateFileSource(sourceType);
            //List<Show> allShowList = fileSource.GetAllShows();
            //return allShowList;
            
            MockDataSource dal = new MockDataSource();

            List<Show> a = dal.GetAllShows();
            return a;
            
        }

        public List<Show> GetLocalMedia()
        {
            MediaDataSource dal = new MediaDataSource();

            dal.GetAllShows();
            return null;
            
        }
    }
}
