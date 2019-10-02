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

        ///weitere operations wie update, create, insert, select(suchparameter), ...
    }
}
