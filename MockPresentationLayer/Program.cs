using BusinessLayer;
using CommonTypes;
using RssFeedProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DeserializingManager deserializer = new DeserializingManager();

            //Podcast a = deserializer.DeserializeRssXml("http://joeroganexp.joerogan.libsynpro.com/rss");
            //Podcast a = deserializer.DeserializeRssXml("http://podcast.wdr.de/quarks.xml");
            //Podcast a = deserializer.DeserializeRssXml("http://web.ard.de/radiotatort/rss/podcast.xml");
            //Podcast a = deserializer.DeserializeRssXml("https://www1.wdr.de/radio/podcasts/wdr2/kabarett132.podcast");

            Podcast a = deserializer.DeserializeRssXml("http://www1.swr.de/podcast/xml/swr2/forum.xml");

            SaveObjects blSave = new SaveObjects();
            blSave.SavePodcastAsXml(a);

            GetObjects blGet = new GetObjects();
            List<Show> seriesList = blGet.GetSeriesList();
        }
    }
}
