using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace LocalStorage
{
    public sealed class XmlStorage
    {
        public Guid Id = Guid.NewGuid();
        #region Singleton
        private static readonly XmlStorage instance = new XmlStorage();

        static XmlStorage()
        {

        }

        private XmlStorage()
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

        public static XmlStorage Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion Singleton

        private static XDocument _dbXDoc { get; set; }

        private readonly string _newPodcastPath;
        private readonly string _dbXmlPath;
        private readonly string _dbXmlTempPath;

        //public XmlStorage()
        //{
        //    var config = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //    var localDir = Directory.CreateDirectory($"{config}\\podcastgrabbr_podcasts");
        //    this._dbXmlPath = $"{localDir.FullName}\\dbXml.xml";
        //    this._dbXmlTempPath = $"{localDir.FullName}\\dbXml.tmp";
        //    this._newPodcastPath = $"{localDir.FullName}\\latestPodcast.xml";

        //    if (File.Exists(_dbXmlPath))
        //    {
        //        _dbXDoc = XDocument.Load(_dbXmlPath);
        //    }
        //}

        public void ProcessNewPodcast(string rssUri)
        {
            ////string rssUri = "http://podcast.wdr.de/quarks.xml";
            //string rssUri = "http://joeroganexp.joerogan.libsynpro.com/rss";
            ////string rssUri = "http://www1.swr.de/podcast/xml/swr2/forum.xml";
            ////string rssUri = "http://web.ard.de/radiotatort/rss/podcast.xml";

            RssToNewPodcastXml(rssUri);
            if (_dbXDoc != null)
            {
                ReplaceOrInsertShow();
                DbXmlReload();
            }
            else
            {
                MergeNewPodcastWithAllPodcasts();
            }
        }

        private void RssToNewPodcastXml(string rssUri)
        {
            XslCompiledTransform xslt = MergeTest();
            xslt.Transform(rssUri, _newPodcastPath);
        }

        private void UpdateShow()
        {

        }

        private void MergeNewPodcastWithAllPodcasts()
        {
            XsltSettings settings = new XsltSettings(true, false);
            XslCompiledTransform xslt = MergeTest(settings);

            if (!File.Exists(_dbXmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<podcasts></podcasts>");
                doc.Save(_dbXmlPath);
            }

            File.Copy(_dbXmlPath, _dbXmlTempPath, true);
            XsltArgumentList argList = new XsltArgumentList();
            argList.AddParam("fileName", "", _dbXmlTempPath);
            XmlWriter writer = XmlWriter.Create(_dbXmlPath, xslt.OutputSettings);
            xslt.Transform(_newPodcastPath, argList, writer);
            writer.Close();
            File.Delete(_dbXmlTempPath);
        }

        private XslCompiledTransform MergeTest(XsltSettings settings = null)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            IEnumerable<string> xsltStreamPath;
            var a = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            if (settings != null)
            {
                xsltStreamPath = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(r => r.Contains("XmlMerger.xslt"));
            }
            else
            {
                xsltStreamPath = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(r => r.Contains("TransformRssToXml.xslt"));
            }
            XmlReader xmlReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream(xsltStreamPath.First()));
            xslt.Load(xmlReader, settings, new XmlUrlResolver());
            return xslt;
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

            string encodedXml = newShowElement.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("&#34;", "&quot;").Replace("'", "&apos;");

            string matchingShowElement = _dbXDoc.Descendants("show")
                .Where(x => x.Element("title").Value.Equals(encodedXml) || x.Element("title").Value.Equals(newShowElement))
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
                                PublisherName = s.Element("publisher").Value
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

            List<Episode> allEpisodes = new List<Episode>();
            foreach (var item in episodes3)
            {
                Episode epToAdd = new Episode();
                epToAdd.Summary = item.Element("description") != null ? item.Element("description").Value : "";
                epToAdd.Title = item.Element("title").Value;
                epToAdd.EpisodeId = item.Attribute("eid").Value;
                epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
                epToAdd.PublishDate = dateParser.ConvertStringToDateTime(item.Element("pubdate").Value);
                epToAdd.FileDetails = new FileInformation();
                epToAdd.FileDetails.SourceUri = item.Element("url").Value;
                allEpisodes.Add(epToAdd);
            }
            return allEpisodes;
        }

        //public List<Episode> GetNext(Show selectedShow, Episode lastEpisode)
        //{

        //    var episodes3 = _dbXDoc.Descendants("episode")
        //        .Where(x => x.Parent.Parent.Attribute("sid").Value == selectedShow.ShowId && int.Parse(x.Attribute("eid").Value) < int.Parse(lastEpisode.EpisodeId));

        //    List<Episode> allEpisodes = new List<Episode>();
        //    foreach (var item in episodes3)
        //    {
        //        Episode epToAdd = new Episode();
        //        epToAdd.FileDetails = new FileInformation();
        //        epToAdd.Summary = item.Element("summary").Value;
        //        epToAdd.FileDetails.SourceUri = item.Element("url").Value;
        //        epToAdd.Title = item.Element("title").Value;
        //        epToAdd.EpisodeId = item.Attribute("eid").Value;

        //        allEpisodes.Add(epToAdd);
        //    }
        //    return allEpisodes;
        //}

    }
}
