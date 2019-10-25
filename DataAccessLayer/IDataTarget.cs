using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataTarget
    {
        void SavePodcast(Podcast podcastToSave);
        void DeletePodcast(Podcast podcastToDelete);
        void UpdatePodcast(Podcast oldPodcast, Podcast newPodcast);

        ///weitere operations wie update, create, insert, select(suchparameter), ...
    }
}
