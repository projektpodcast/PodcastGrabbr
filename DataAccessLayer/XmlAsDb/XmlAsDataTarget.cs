using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using LocalStorage;

namespace DataAccessLayer
{
    public class XmlAsDataTarget : IDataTarget
    {
        public void InsertDownloadPath(Show show, Episode episode, string path)
        {
            XmlStorage.Instance.SetDownloadPath(show, episode, path);
        }

        public void SavePodcast(Podcast podcastToSave)
        {
            throw new NotImplementedException();
        }


    }
}
