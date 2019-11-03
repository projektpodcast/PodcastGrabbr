using BusinessLayer;
using CommonTypes;
using DataAccessLayer;
using RssFeedProcessor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPresentationLayer
{
    /// <summary>
    /// Dieses Projekt besteht nur für Testzwecke. Hier können Methodenaufrufe und -abläufe getestet werden.
    /// Dieses Projekt muss dafür als "Startup Project" in den Properties der Solution eingestellt werden.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DeserializingManager deserializer = new DeserializingManager();

            ////Podcast a = deserializer.DeserializeRssXml("http://joeroganexp.joerogan.libsynpro.com/rss");
            ////Podcast a = deserializer.DeserializeRssXml("http://podcast.wdr.de/quarks.xml");
            ////Podcast a = deserializer.DeserializeRssXml("http://web.ard.de/radiotatort/rss/podcast.xml");
            Podcast a = deserializer.DeserializeRssXml("https://www1.wdr.de/radio/podcasts/wdr2/kabarett132.podcast");
            ////Podcast a = deserializer.DeserializeRssXml("http://www1.swr.de/podcast/xml/swr2/forum.xml");

            PostgresData PostDB = new PostgresData();
            //call postdb
            //PostDB.(a);
        }
    }
}