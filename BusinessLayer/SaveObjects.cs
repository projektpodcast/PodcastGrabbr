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
        public SaveObjects()
        { }
        public void SavePodcastAsXml(Podcast podcast)
        {
            IDataTarget fileTarget = Factory.CreateDataTarget();
            fileTarget.SavePodcast(podcast);
        }

        public void Test()
        {
            IDataTarget fileTarget = Factory.CreateDataTarget();
        }

        public void SavePodcastAsMediaFile(Podcast podcast)//rework (interface, umschreiben(episode list hier nicht festlegen, kommt aus interface)
        {
            MediaDataTarget mediaTarget = new MediaDataTarget();

            Episode selectedEpisode = podcast.EpisodeList[0];
            Show selectedShow = podcast.ShowInfo;
            Podcast selectedPodcastData = new Podcast() { ShowInfo = selectedShow, EpisodeList = new List<Episode> { selectedEpisode } };

            mediaTarget.SavePodcast(selectedPodcastData);
        }
    }
}
