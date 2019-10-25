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
        public void SavePodcastAsXml(Podcast podcast, int targetType)
        {
            IDataTarget fileTarget = Factory.CreateFileTarget(targetType);
            fileTarget.SavePodcast(podcast);
        }

        public void SavePodcastAsMediaFile(Podcast podcast)
        {
            MediaDataTarget mediaTarget = new MediaDataTarget();

            Episode selectedEpisode = podcast.EpisodeList[0];
            Show selectedShow = podcast.ShowInfo;
            Podcast selectedPodcastData = new Podcast() { ShowInfo = selectedShow, EpisodeList = new List<Episode> { selectedEpisode } };

            mediaTarget.SavePodcast(selectedPodcastData);
        }
    }
}
