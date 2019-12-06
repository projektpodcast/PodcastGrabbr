using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;
using CommonTypes;

namespace RssFeedProcessor
{
    public class LocalRssTest
    {
        public static XDocument DbXml { get; set; }
        public LocalRssTest()
        {
            DbXml = XDocument.Load(".\\Test10.xml");
        }
        public void Test()
        {
            //string path = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\TransformRssToLocalXml.xslt";

            //var xsltFile = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("TransformRssToLocalXml.xslt"));
            //Stream xsltStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(xsltFile.First());
            //string url2 = "http://podcast.wdr.de/quarks.xml";
            //string url2 = "http://joeroganexp.joerogan.libsynpro.com/rss";
            //string url2 = "http://www1.swr.de/podcast/xml/swr2/forum.xml";

            //string url2 = "http://web.ard.de/radiotatort/rss/podcast.xml";
            //XslCompiledTransform xslt = new XslCompiledTransform();
            //xslt.Load(path);
            //xslt.Transform(url2, ".\\Test2.xml");


            //XsltSettings settings = new XsltSettings(true, false);

            //string path2 = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\XmlMerger.xslt";
            //XslCompiledTransform xslt2 = new XslCompiledTransform();

            //xslt2.Load(path2, settings, new XmlUrlResolver());
            //xslt2.Transform(".\\Test2.xml", ".\\Test10.xml");




            XDocument doc = XDocument.Load(".\\Test10.xml");
            //XmlDocument doc = new XmlDocument();

            //Load xml
            //XmlTextReader reader = new XmlTextReader(".\\Test10.xml");
            //reader.WhitespaceHandling = WhitespaceHandling.None;
            ////doc.Load(reader);
            //doc = XDocument.Parse(reader.ToString());

            //var query = from s in doc.Descendants("show")
            //            select new Show
            //            {
            //                PodcastTitle = (string)s.Element("title"),
            //                RssLink = (string)s.Element("link"),
            //                ImageUri = (string)s.Element("imageuri"),
            //                ShowId = (string)s.Attribute("sid")
            //            };

            //var episodes = doc.Descendants("allepisodes")
            //    .Select(element => element.Parent)
            //    .Attributes()
            //    .Where(attribute => attribute.Name == "sid")
            //    .Select(attribute => attribute.Value);


            //var episodes2 = doc.Descendants("show")
            //    .Attributes()
            //    .Where(attribute => attribute.Name == "sid" && attribute.Value == "ID0WG");

            //get all episodes to show id
            var episodes3 = doc.Descendants("episode")
                .Where(x => x.Parent.Parent.Attribute("sid").Value == "ID0WG");

            List<Episode> allEpisodes = new List<Episode>();
            foreach (var item in episodes3)
            {
                Episode epToAdd = new Episode();
                epToAdd.FileDetails = new FileInformation();
                //epToAdd.EpisodeId = item.Attribute("eid").Value
                epToAdd.Summary = item.Element("summary").Value;
                epToAdd.FileDetails.SourceUri = item.Element("url").Value;
                epToAdd.Title = item.Element("title").Value;
                epToAdd.EpisodeId = item.Attribute("eid").Value;

                allEpisodes.Add(epToAdd);
            }


            //var query = from t in doc.SelectNodes("podcasts/show")
            //            select t;

        }

        public List<Show> GetShows()
        {
            var query = from s in DbXml.Descendants("show")
                        select new Show
                        {
                            PodcastTitle = (string)s.Element("title"),
                            RssLink = (string)s.Element("link"),
                            ImageUri = (string)s.Element("imageuri") != null ? s.Element("imageuri").Value : "https://lh3.googleusercontent.com/BQUYd1Th9Z_XI5wtklPQDHmiNkSOzBakOnpk-Ni8CBTyHb0E7UM5LpyjRW9BWs4fUuVD",
                            ShowId = (string)s.Attribute("sid")
                        };
            List<Show> a = query.ToList();
            return a;
        }

        public List<Episode> GetEpisodes(Show show)
        {
            var episodes3 = DbXml.Descendants("episode")
                .Where(x => x.Parent.Parent.Attribute("sid").Value == show.ShowId);

            var episodesFilter = episodes3.Take(30);
            //var episodesFilter = episodes3;

            List<Episode> allEpisodes = new List<Episode>();
            foreach (var item in episodesFilter)
            {
                Episode epToAdd = new Episode();
                epToAdd.FileDetails = new FileInformation();
                //epToAdd.EpisodeId = item.Attribute("eid").Value
                epToAdd.Summary = item.Element("summary").Value;
                epToAdd.FileDetails.SourceUri = item.Element("url").Value;
                epToAdd.Title = item.Element("title").Value;
                epToAdd.EpisodeId = item.Attribute("eid").Value;

                allEpisodes.Add(epToAdd);
            }
            return allEpisodes;
        }

        public List<Episode> GetNext(Show selectedShow, Episode lastEpisode)
        {
            
            var episodes3 = DbXml.Descendants("episode")
                .Where(x => x.Parent.Parent.Attribute("sid").Value == selectedShow.ShowId && int.Parse(x.Attribute("eid").Value) < int.Parse(lastEpisode.EpisodeId))
                //.OrderBy(x => int.Parse(x.Attribute("eid").Value))
                //orderby int.Parse(x.Attribute("eid").Value)
                ;

            var episodesFilter = episodes3.Take(30);
            //var episodesFilter = episodes3;

            List<Episode> allEpisodes = new List<Episode>();
            foreach (var item in episodesFilter)
            {
                Episode epToAdd = new Episode();
                epToAdd.FileDetails = new FileInformation();
                //epToAdd.EpisodeId = item.Attribute("eid").Value
                epToAdd.Summary = item.Element("summary").Value;
                epToAdd.FileDetails.SourceUri = item.Element("url").Value;
                epToAdd.Title = item.Element("title").Value;
                epToAdd.EpisodeId = item.Attribute("eid").Value;

                allEpisodes.Add(epToAdd);
            }
            return allEpisodes;
        }

    }
}

//string path3 = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\ShowCounter.xslt";
//XslCompiledTransform xslt3 = new XslCompiledTransform();
//xslt.Load(path3);
//xslt2.Transform(".\\Test10.xml", ".\\Final.xml");
