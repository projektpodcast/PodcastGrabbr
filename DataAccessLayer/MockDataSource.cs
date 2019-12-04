using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace DataAccessLayer
{
    public class MockDataSource : IDataSource
    {
        public MockDataSource()
        {
        }
        //public List<Show> GetAllShows()
        //{
        //    List<Show> mockList = new List<Show>();
        //    string bild3 = "http://static.libsyn.com/p/assets/7/1/f/3/71f3014e14ef2722/JREiTunesImage2.jpg";
        //    string bild2 = "https://www1.wdr.de/mediathek/video/sendungen/quarks-und-co/logo-quarks100~_v-Podcast.jpg";
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (i % 2 >= 1)
        //        {
        //            mockList.Add(new Show
        //            {
        //                PodcastTitle = "Titel des PodcastsTitel des PodcastsTitel des Podcasts",
        //                PublisherName = "Ich bin der Publisher",
        //                Description = "The podcast of Comedian Joe Rogan.",
        //                ImageUri = bild3,
        //                LastUpdated = DateTime.UtcNow
        //            });
        //        }
        //        else
        //        {
        //            mockList.Add(new Show
        //            {
        //                PodcastTitle = "WowTitel",
        //                PublisherName = "ARD & ZDF",
        //                Description = "Wow Podcast",
        //                ImageUri = bild2,
        //                LastUpdated = DateTime.UtcNow
        //            });
        //        }
        //    }
        //    return mockList;
        //}

        public List<Podcast> GetDownloadedPodcasts()
        {
            List<Show> showList = new List<Show>();
            showList = GetAllShows();
            List<Episode> episodeList = new List<Episode>();

            DateTime now = DateTime.Now;
            Episode episode = new Episode
            {
                Title = "Neue Show Selected",
                PublishDate = now,
                Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364",
                Summary = "Pete Dominick is a stand up comic, speaker, news commentator, host, and moderator. Look for his podcast called " +
                "'StandUP!with Pete Dominick' available on Apple Podcasts.",
            };
            episodeList.Add(episode);
            episodeList.Add(episode);
            episodeList.Add(episode);
            episodeList.Add(episode);
            episodeList.Add(episode);
            episodeList.Add(episode);
            episodeList.Add(episode);

            List<Podcast> allDownloadedPodcasts = new List<Podcast>();

            foreach (var item in showList)
            {
                Podcast podcast = new Podcast();
                podcast.ShowInfo = item;
                podcast.EpisodeList = episodeList;
                allDownloadedPodcasts.Add(podcast);
            }
            return allDownloadedPodcasts;
        }



        public List<Show> GetAllShows()
        {
            List<Show> mockList = new List<Show>();
            string bild9 = "uri not configured in index document";
            string bild8 = "http://module.zdf.de/podcasts/anstalt_1400x1400.jpg";
            string bild7 = "https://www.swr.de/swr2/programm/Sendungsbild-SWR2-Feature,1564740500732,podcastbild-swr2-feature-100~_v-16x9@2dS_-6be50a9c75559ca1aaf1d0b25bae287afdcd877a.jpg";
            string bild6 = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Wissen,1565083398010,swr2-wissen-podcast-108~_v-16x9@2dS_-6be50a9c75559ca1aaf1d0b25bae287afdcd877a.jpg";
            string bild5 = "http://www.deutschlandfunk.de/index.media.f7ed9b9651ece4d4c1532fe33cd80201";
            string bild4 = "https://img.br.de/6f808e9b-beb7-464c-a912-f8cc752ffe8d.png?w=1800";
            string bild3 = "http://static.libsyn.com/p/assets/7/1/f/3/71f3014e14ef2722/JREiTunesImage2.jpg";
            string bild2 = "https://www1.wdr.de/mediathek/video/sendungen/quarks-und-co/logo-quarks100~_v-Podcast.jpg";
            string bild1 = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-16x9@2dS_-6be50a9c75559ca1aaf1d0b25bae287afdcd877a.jpg";

            List<string> podImages = new List<string>();
            podImages.Add(bild1);
            podImages.Add(bild2);
            podImages.Add(bild3);
            podImages.Add(bild4);
            podImages.Add(bild5);
            podImages.Add(bild6);
            podImages.Add(bild7);
            podImages.Add(bild8);
            podImages.Add(bild9);


            for (int i = 0; i < 9; i++)
            {
                int loop = i;
                if (i % 2 >= 1)
                {
                    mockList.Add(new Show
                    {
                        PodcastTitle = "Titel des PodcastsTitel des PodcastsTitel des Podcasts",
                        PublisherName = "Ich bin der Publisher",
                        Description = "The podcast of Comedian Joe Rogan.",
                        ImageUri = podImages[loop],
                        LastUpdated = DateTime.UtcNow
                    });
                }
                else
                {
                    mockList.Add(new Show
                    {
                        PodcastTitle = "WowTitel",
                        PublisherName = "ARD & ZDF",
                        Description = "Wow Podcast",
                        ImageUri = podImages[loop],
                        LastUpdated = DateTime.UtcNow
                    });
                }
            }
            return mockList;
        }

        public List<Episode> GetAllEpisodes(Show selectedShow)
        {
            List<Episode> episodesList = new List<Episode>();

            DateTime now = DateTime.Now;
            for (int i = 0; i < 4; i++)
            {
                episodesList.Add(new Episode() { Title = "#1364 - Brian RedbanRedbanRedb anRedbanRedbanRedbanRedba nRedbanRedbanRed banRedbanRedban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg", IsDownloaded = false });
                episodesList.Add(new Episode() { Title = "#1364 - Brian Redban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg", IsDownloaded = true });
            }

            return episodesList;
        }
    }
}
