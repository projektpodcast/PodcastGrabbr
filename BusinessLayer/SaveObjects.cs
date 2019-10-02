using CommonTypes;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SaveObjects
    {
        public void SavePodcastAsXml(Podcast podcast)
        {
            IDataTarget fileTarget = Factory.CreateFileTarget();
            fileTarget.SavePodcast(podcast);
        }
    }
}
