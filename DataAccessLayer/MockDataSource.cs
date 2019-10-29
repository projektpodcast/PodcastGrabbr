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
        public List<Show> GetAllShows()
        {
            List<Show> mockList = new List<Show>();
            string bild3 = "http://static.libsyn.com/p/assets/7/1/f/3/71f3014e14ef2722/JREiTunesImage2.jpg";
            string bild2 = "https://www1.wdr.de/mediathek/video/sendungen/quarks-und-co/logo-quarks100~_v-Podcast.jpg";
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 >= 1)
                {
                    mockList.Add(new Show
                    {
                        PodcastTitle = "Titel des Podcasts",
                        PublisherName = "Ich bin der Publisher",
                        Description = "The podcast of Comedian Joe Rogan.",
                        ImageUri = bild3,
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
                        ImageUri = bild2,
                        LastUpdated = DateTime.UtcNow
                    });
                }
            }
            return mockList;
        }








}
}
