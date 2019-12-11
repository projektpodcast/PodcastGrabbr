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
using System.Xml.XPath;
using System.Configuration;

namespace RssFeedProcessor
{
    public class LocalRssTest
    {
        private static XDocument _dbXDoc { get; set; }

        private readonly string _newPodcastPath;
        //private readonly string _rssToXmlXsltPath = "..\\..\\..\\RssFeedProcessor\\TransformRssToLocalXml.xslt";
        //private readonly string _xmlMergeXsltPath = "..\\..\\..\\RssFeedProcessor\\XmlMerger.xslt";
        //private readonly string _localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private readonly string _dbXmlPath;
        private readonly string _dbXmlTempPath;
        //private string _idToReplace { get; set; }

        public LocalRssTest()
        {
            var config = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var localDir = Directory.CreateDirectory($"{config}\\podcastgrabbr_podcasts");
            this._dbXmlPath = $"{localDir.FullName}\\dbXml.xml";
            this._dbXmlTempPath = $"{localDir.FullName}\\dbXml.tmp";
            this._newPodcastPath = $"{localDir.FullName}\\latestPodcast.xml";

            if (File.Exists(_dbXmlPath))
            {
                _dbXDoc = XDocument.Load(_dbXmlPath);
            }
        }

        public void ProcessNewPodcast(/*string rssUri*/)
        {
            //string rssUri = "http://podcast.wdr.de/quarks.xml";
            string rssUri = "http://joeroganexp.joerogan.libsynpro.com/rss";
            //string rssUri = "http://www1.swr.de/podcast/xml/swr2/forum.xml";
            //string rssUri = "http://web.ard.de/radiotatort/rss/podcast.xml";

            RssToNewPodcastXml(rssUri);
            if (_dbXDoc != null)
            {
                ReplaceOrInsertShow();
            }
            else
            {
                MergeNewPodcastWithAllPodcasts();
            }
        }

        public string SetFilePaths()
        {
            return null;
        }

        private void RssToNewPodcastXml(string rssUri)
        {
            XslCompiledTransform xslt = MergeTest();
            //XslCompiledTransform xslt = new XslCompiledTransform();
            //xslt.Load(_rssToXmlXsltPath);
            xslt.Transform(rssUri, _newPodcastPath);
        }

        private void UpdateShow()
        {

        }

        private void MergeNewPodcastWithAllPodcasts()
        {
            //XslCompiledTransform xslt2 = new XslCompiledTransform();
            //XsltSettings settings = new XsltSettings(true, false);
            //xslt2.Load(_xmlMergeXsltPath, settings, new XmlUrlResolver());
            XsltSettings settings = new XsltSettings(true, false);
            XslCompiledTransform xslt2 = MergeTest(settings);

            if (!File.Exists(_dbXmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<podcasts></podcasts>");
                doc.Save(_dbXmlPath);
            }

            File.Copy(_dbXmlPath, _dbXmlTempPath, true);
            XsltArgumentList argList = new XsltArgumentList();
            argList.AddParam("fileName", "", _dbXmlTempPath);
            XmlWriter writer = XmlWriter.Create(_dbXmlPath, xslt2.OutputSettings);
            xslt2.Transform(_newPodcastPath, argList, writer);
            writer.Close();
            File.Delete(_dbXmlTempPath);
        }

        private XslCompiledTransform MergeTest(XsltSettings settings = null)
        {
            XslCompiledTransform loadedXslt = new XslCompiledTransform();
            IEnumerable<string> xsltStreamPath;
            if (settings != null)
            {
                xsltStreamPath = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(r => r.Contains("XmlMerger.xslt"));
            }
            else
            {
                xsltStreamPath = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(r => r.Contains("TransformRssToLocalXml.xslt"));
            }
            XmlReader xmlReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream(xsltStreamPath.First()));
            loadedXslt.Load(xmlReader, settings, new XmlUrlResolver());
            return loadedXslt;
        }

        public void DbXmlReload()
        {
            try
            {
                _dbXDoc = null;
                _dbXDoc = XDocument.Load(_dbXmlPath);
            }
            catch (Exception)
            {


            }
        }

        private void ReplaceOrInsertShow()
        {
            XDocument newPodcast = XDocument.Load(_newPodcastPath);

            string newShowElement = newPodcast.Element("podcasts")
                .Elements("show")
                .Elements("title")
                .FirstOrDefault().Value;

            string matchingShowElement = _dbXDoc.Descendants("show")
                .Where(x => x.Element("title").Value.Equals(newShowElement))
                .Select(y => (string)y.Attribute("sid").Value).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(matchingShowElement))
            {
                string _idToReplace = matchingShowElement;

                XElement updatedPodcast = newPodcast.Descendants("show").FirstOrDefault();
                updatedPodcast.SetAttributeValue("sid", _idToReplace);
                XElement dBXmlShow = _dbXDoc.Descendants("show")
                    .Where(x => x.Attribute("sid").Value == _idToReplace).First();

                dBXmlShow.ReplaceWith(updatedPodcast);
                _dbXDoc.Save(_dbXmlPath);            
            }
            else
            {
                MergeNewPodcastWithAllPodcasts();
            }
        }

        public List<Show> GetShows()
        {
            DateTimeParser dateParser = new DateTimeParser();
            List<Show> a = new List<Show>();
            if (_dbXDoc != null)
            {
                var query = from s in _dbXDoc.Descendants("show")
                            select new Show
                            {
                                PodcastTitle = s.Element("title").Value,
                                RssLink = s.Element("link").Value,
                                ImageUri = s.Element("imageuri").Value != null ? s.Element("imageuri").Value : "https://lh3.googleusercontent.com/BQUYd1Th9Z_XI5wtklPQDHmiNkSOzBakOnpk-Ni8CBTyHb0E7UM5LpyjRW9BWs4fUuVD",
                                ShowId = s.Attribute("sid").Value,
                                Description = s.Element("description").Value,
                                LastBuildDate = dateParser.ConvertStringToDateTime(s.Element("lastbuilddate").Value),
                                LastUpdated = dateParser.ConvertStringToDateTime(s.Element("lastupdated").Value),
                            };
                a = query.ToList();
            }
            return a;
        }

        public List<Episode> GetEpisodes(Show show)
        {
            DateTimeParser dateParser = new DateTimeParser();
            var episodes3 = _dbXDoc.Descendants("episode")
                .Where(x => x.Parent.Parent.Attribute("sid").Value == show.ShowId);

            //var episodesFilter = episodes3.Take(30);
            //var episodesFilter = episodes3;

            //int count = (episodes3.Count() + 30 - 1) / 30;

            List<Episode> allEpisodes = new List<Episode>();
            foreach (var item in episodes3)
            {
                Episode epToAdd = new Episode();
                epToAdd.FileDetails = new FileInformation();
                epToAdd.Summary = item.Element("description") != null ? item.Element("description").Value : "";
                epToAdd.FileDetails.SourceUri = item.Element("url").Value;
                epToAdd.Title = item.Element("title").Value;
                epToAdd.EpisodeId = item.Attribute("eid").Value;
                epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
                epToAdd.PublishDate = dateParser.ConvertStringToDateTime(item.Element("pubdate").Value);
                allEpisodes.Add(epToAdd);
            }
            return allEpisodes;
        }



        public List<Episode> GetNext(Show selectedShow, Episode lastEpisode)
        {

            var episodes3 = _dbXDoc.Descendants("episode")
                .Where(x => x.Parent.Parent.Attribute("sid").Value == selectedShow.ShowId && int.Parse(x.Attribute("eid").Value) < int.Parse(lastEpisode.EpisodeId))
                //.OrderBy(x => int.Parse(x.Attribute("eid").Value))
                //orderby int.Parse(x.Attribute("eid").Value)
                ;

            //var episodesFilter = episodes3.Take(30);
            //var episodesFilter = episodes3;

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
            return allEpisodes;
        }

    }
}

//string path3 = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\ShowCounter.xslt";
//XslCompiledTransform xslt3 = new XslCompiledTransform();
//xslt.Load(path3);
//xslt2.Transform(".\\Test10.xml", ".\\Final.xml");

//string path = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\TransformRssToLocalXml.xslt";
//   XslCompiledTransform xslt = new XslCompiledTransform();
//   xslt.Load(path);
//   //Rss-Uri in Transformer laden sowie Zielpfad zur Verfügung stellen

//   //Fragestellung: Stream als Output möglich?
//   xslt.Transform(url2, ".\\NewPodcast.xml"); //
//   //MemoryStream ms = new MemoryStream();
//   //xslt.Transform(url2, null, ms);
//   /////////////////////////////////////////////////////

//   //////Podcast-Xml und bestehende "db xml" in eine neue "db Xml" mergen/////
//   //Fragestellung: Wie Xml1 und Xml2 nicht in Xml3 mergen, sondern Xml1 an Xml2 beifügen
//   XsltSettings settings = new XsltSettings(true, false);
//   string path2 = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\XmlMerger.xslt";
//   XslCompiledTransform xslt2 = new XslCompiledTransform();

//   xslt2.Load(path2, settings, new XmlUrlResolver());
//   //xslt2.Transform(".\\Test2.xml", ".\\Test10.xml");
//   //ms.Seek(0,0);
//   //XmlReader readerxml = XmlReader.Create(ms);
//   //XmlTextWriter writer = new XmlTextWriter(@"C:\\Users\\pgrundmann\\source\\repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\sampleData.xml", null);

//   //copy AllPodcasts.xml, open copy and overwrite original, delete copy
//   //File.Copy(".\\AllPodcasts.xml", ".\\AllPodcasts.tmp");
//   string samplePath1 = "..\\..\\..\\RssFeedProcessor\\sampleData.xml";
//   string samplePath2 = "..\\..\\..\\RssFeedProcessor\\sampleDataTemp.xml";
//   //string path1 = "C:\\Users\\pgrundmann\\Source\\Repos\\projektpodcast\\PodcastGrabbr\\RssFeedProcessor\\sampleData.xml";
//   File.Copy(samplePath1, samplePath2, true);

//   //writer.Formatting = Formatting.Indented;
//   //MemoryStream ms2 = new MemoryStream();
//   xslt2.Transform(".\\NewPodcast.Xml", samplePath1);



//   File.Delete(samplePath2);



//XDocument doc = XDocument.Load(".\\Test10.xml");
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

///////////////////"DATABASE LIKE METHODS TO PARSE XML////////////////////////
//get all episodes to show id
//var episodes3 = doc.Descendants("episode")
//    .Where(x => x.Parent.Parent.Attribute("sid").Value == "ID0WG");

//List<Episode> allEpisodes = new List<Episode>();
//foreach (var item in episodes3)
//{
//    Episode epToAdd = new Episode();
//    epToAdd.FileDetails = new FileInformation();
//    //epToAdd.EpisodeId = item.Attribute("eid").Value
//    epToAdd.Summary = item.Element("summary").Value;
//    epToAdd.FileDetails.SourceUri = item.Element("url").Value;
//    epToAdd.Title = item.Element("title").Value;
//    epToAdd.EpisodeId = item.Attribute("eid").Value;

//    allEpisodes.Add(epToAdd);
//}