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
    /// <summary>
    /// Die Klasse XmlStorage wird als "lokale in memory" Datenbank genutzt.
    /// Eine Xml-Datei wird in den Speicher geladen und mit Linq to Xml durchsucht.
    /// Daten können persistiert werden, indem ein XmlWriter in die Datei schreibt und erneut in das XDocument geladen wird.
    /// Somit besteht eine lokale Alternative zu Datenbanken, die Fremd gehostet sind.
    /// </summary>
    public sealed class XmlStorage
    {

        public Guid Id = Guid.NewGuid();

        /// <summary>
        /// Singleton garantiert, dass alle fremden Klassen, die diese Klasse aufrufen, die gleiche Instanz erhalten.
        /// Das ist wichtig, da es sonst Zugriffsprobleme auf die lokale Xml-Datei geben könnte oder
        /// auf veraltete XDocument-Versionen zugegriffen wird.
        /// </summary>
        #region Singleton
        private static readonly XmlStorage instance = new XmlStorage();

        static XmlStorage()
        {

        }

        /// <summary>
        /// Bei Klasseninitialisierung wird der relative Pfad "..\AppData\Local" festgestellt.
        /// Und, bei Bestand, die "Datenbank"-Xml geladen.
        /// </summary>
        private XmlStorage()
        {
            string localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DirectoryInfo localDir = Directory.CreateDirectory($"{localAppPath}\\podcastgrabbr_podcasts");
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

        /// <summary>
        /// Das XDocument ist eine in-memory gehaltene Xml-Datei. Diese Xml Datei gruppiert, wie eine Datenbank, Episoden zu Shows.
        /// Dieses XDocument kann mit (Linq) Queries datenbankähnlich durchsucht werden.
        /// </summary>
        private static XDocument _dbXDoc { get; set; }

        /// <summary>
        /// Lokale Pfade
        /// </summary>
        private readonly string _newPodcastPath;
        private readonly string _dbXmlPath;
        private readonly string _dbXmlTempPath;

        /// <summary>
        /// Hilfsmethode leitet den Fluss der Methode.
        /// 1) Sie transformiert eine Rss-Xml in eine selbstdefinierte Xml Form
        /// 2) Prüft ob es bereits eine "Datenbank"-Xml gibt
        /// Fügt die neuerstellte Xml mit der "Datenbank"-Xml zusammen.
        /// </summary>
        /// <param name="rssUri">RSS-Feed Uri eines Podcasts</param>
        public void ProcessNewPodcast(string rssUri)
        {
            RssToNewPodcastXml(rssUri);
            if (_dbXDoc != null)
            {
                UpdateOrInsertNewShow();
            }
            else
            {
                MergeNewPodcastXmlWithDbXml();
            }
            DbXmlReload();
        }

        /// <summary>
        /// Transformiert eine RSS-Feed Xml mithilfe einer .xslt-Datei in eine unterschiedlich geformtes XML.
        /// Speichert die daraus resultierende XML lokal ab.
        /// </summary>
        /// <param name="rssUri">RSS-Feed Uri eines Podcasts</param>
        private void RssToNewPodcastXml(string rssUri)
        {
            try
            {
                XslCompiledTransform xslt = LoadXslt();
                xslt.Transform(rssUri, _newPodcastPath);
            }
            catch (Exception)
            {
                //throw;
            }

        }

        /// <summary>
        /// Fügt eine Podcast-Xml der "Datenbank"-Xml hinzu.
        /// Falls die "Datenbank"-Xml nicht existiert, wird diese angelegt und das Xml hinzugefügt.
        /// Falls die "Datenbank"-Xml existiert, wird in diese um die neue Show ergänzt und überschrieben.
        /// </summary>
        private void MergeNewPodcastXmlWithDbXml(string filePath = null)
        {
            //try
            //{
            XsltSettings settings = new XsltSettings(true, false);
            XslCompiledTransform xslt = LoadXslt(settings);

            if (!File.Exists(_dbXmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<podcasts></podcasts>");
                doc.Save(_dbXmlPath);
            }
            if (filePath == null)
            {
                File.Copy(_dbXmlPath, _dbXmlTempPath, true);
                XsltArgumentList argList = new XsltArgumentList();
                argList.AddParam("fileName", "", _dbXmlTempPath);
                XmlWriter writer = XmlWriter.Create(_dbXmlPath, xslt.OutputSettings);
                xslt.Transform(_newPodcastPath, argList, writer);
                writer.Close();
                File.Delete(_dbXmlTempPath);
            }
            else
            {
                File.Copy(_dbXmlPath, _dbXmlTempPath, true);
                XsltArgumentList argList = new XsltArgumentList();
                argList.AddParam("fileName", "", _dbXmlTempPath);
                XmlWriter writer = XmlWriter.Create(_dbXmlPath, xslt.OutputSettings);
                xslt.Transform(filePath, argList, writer);
                writer.Close();
                File.Delete(_dbXmlTempPath);
            }
        }

        /// <summary>
        /// Es werden 2 Xml Dateien benötigt: 
        /// 1) ist ein einzelner, aus einem rss-feed transformierter Podcast
        /// 2) ist die "Datenbank"-Xml
        /// Es wird geprüft ob die hinzufügende Show (1) bereits existiert,
        /// wenn ja wird der betroffene Knoten der "Datenbank"-Xml mit (1) aktualisiert,
        /// wenn nein, wird die Xml (1) der Datenbank-Xml als neuen Knoten hinzugefügt.
        /// </summary>
        private void UpdateOrInsertNewShow(string filePath = null)
        {

            XDocument newPodcast = new XDocument();

            if (filePath == null)
            {
                newPodcast = XDocument.Load(_newPodcastPath);
            }
            else
            {
                newPodcast = XDocument.Load(filePath);
            }

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
                MergeNewPodcastXmlWithDbXml();
            }
            newPodcast = null;
        }

        /// <summary>
        /// Lädt die verschieden benötigten .xslt-Dateien (eine benötigt um zwei Xml-Dateien zu vereinen, eine benötigt um aus einem RSS-Feed eine neue XML zu erstellen)
        /// in eine XslCompiledTransform-Klasse und gibt dieses mit der benötigten und geladenen .xslt-Datei zurück.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private XslCompiledTransform LoadXslt(XsltSettings settings = null)
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

        /// <summary>
        /// Kurzfristig einfügt... Kommentar Todo...
        /// </summary>
        /// <param name="links"></param>
        public void MassImport(List<string> links)
        {
            string localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DirectoryInfo localDir = Directory.CreateDirectory($"{localAppPath}\\podcastgrabbr_podcasts\\bulkcopy\\");
            var path = $"{localDir.FullName}";

            foreach (var item in links)
            {
                var name = string.Join("", item.Split(Path.GetInvalidFileNameChars()));
                name.Replace('/', ' ');
                string path2 = path + name + ".xml";
                string path3 = path + name + "copy.xml";

                try
                {
                    XslCompiledTransform xslt = LoadXslt();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    using (var xmlWriter = XmlWriter.Create(path2))
                    {
                        xslt.Transform(item, XmlWriter.Create(xmlWriter, settings));
                    }

                    if (new FileInfo(path2).Length >= 1024)
                    {
                        File.Copy(path2, path3, true);

                        if (_dbXDoc != null)
                        {
                            UpdateOrInsertNewShow(path3);
                        }
                        else
                        {
                            MergeNewPodcastXmlWithDbXml(path3);
                        }

                    }
                }
                catch (Exception)
                {
                }
            }
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            DbXmlReload();
        }

        /// <summary>
        /// Lädt die "Datenbank-Xml" erneut den Arbeitsspeicher.
        /// </summary>
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

        /// <summary>
        /// Feuert einen Query die in-memory geladene "Datenbank"-Xml.
        /// </summary>
        /// <returns>List-Collection aller vorhandenen Shows</returns>
        public List<Show> GetShows()
        {
            DateTimeParser dateParser = new DateTimeParser();
            List<Show> allShowsList = new List<Show>();
            try
            {
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
                    allShowsList = query.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return allShowsList;
        }

        /// <summary>
        /// Feuert einen Query die in-memory geladene "Datenbank"-Xml.
        /// </summary>
        /// <param name="show">Properties der Show werden genutzt um die Episoden-Children zu suchen</param>
        /// <returns>List-Collection aller, zur Show gehörenden, Episoden</returns>
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
                epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
                epToAdd.DownloadPath = item.Element("localpath").Value != "" ? item.Element("localpath").Value : "";
                allEpisodes.Add(epToAdd);
            }
            return allEpisodes;
        }

        /// <summary>
        /// Identifiziert welcher Knoten der "Datenbank"-Xml angesteuert werden soll (anhand Properties der Show und dessen Episode)
        /// Updated den Knoten "localpath", welcher den lokalen Downloadpfad beschreibt, der gefundenen Episode.
        /// Speichert die Datenbank-Xml danach lokal ab und lädt diese erneut.
        /// </summary>
        /// <param name="show">Parent der Episode, welche heruntergeladen wurde</param>
        /// <param name="episode">Episode, welche heruntergeladen wurde und Child der Show ist</param>
        /// <param name="path">Lokaler Downloadpfad der Episode</param>
        public void SetDownloadPath(Show show, Episode episode, string path)
        {
            XElement targetedEpisode = _dbXDoc.Descendants("episode")
                .Where(x => x.Parent.Parent.Attribute("sid").Value == show.ShowId && x.Attribute("eid").Value == episode.EpisodeId)
                .FirstOrDefault();

            targetedEpisode.Element("localpath").Value = path;
            _dbXDoc.Save(_dbXmlPath);
            DbXmlReload();
        }

        public List<Podcast> GetDownloadedPodcasts()
        {
            List<Podcast> downloadedPodcasts = new List<Podcast>();
            DateTimeParser dateParser = new DateTimeParser();
            var showsWithDownloadedEpisode = _dbXDoc.Descendants("show")
                .Where(x => x.Descendants("allepisodes").Descendants("episode").Elements("localpath").Any(y => !string.IsNullOrWhiteSpace(y.Value)));

            foreach (var s in showsWithDownloadedEpisode)
            {

                List<Episode> episodesOfShow = new List<Episode>();
                Show showToAdd = new Show()
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

                var c = s.Descendants("allepisodes").Descendants("episode")
                    .Where(x => x.Element("localpath").Value != "");

                foreach (var item in c)
                {
                    Episode epToAdd = new Episode();
                    epToAdd.Summary = item.Element("description") != null ? item.Element("description").Value : "";
                    epToAdd.Title = item.Element("title").Value;
                    epToAdd.EpisodeId = item.Attribute("eid").Value;
                    epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
                    epToAdd.PublishDate = dateParser.ConvertStringToDateTime(item.Element("pubdate").Value);
                    epToAdd.FileDetails = new FileInformation();
                    epToAdd.FileDetails.SourceUri = item.Element("url").Value;
                    epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
                    epToAdd.DownloadPath = item.Element("localpath").Value != "" ? item.Element("localpath").Value : "";
                    episodesOfShow.Add(epToAdd);
                }
                downloadedPodcasts.Add(new Podcast(showToAdd, episodesOfShow));
            }

            return downloadedPodcasts;
        }


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




//public List<Podcast> GetDownloadedPodcasts()
//{
//    var episodes = _dbXDoc.Descendants("episode").Where(x => x.Element("localpath").Value != "");

//    var shows = _dbXDoc.Descendants("show").Where(x => x.Element("allepisodes").Value.Contains("a"));

//    var showsWithDownloadedEpisode = _dbXDoc.Descendants("show")
//        .Where(x => x.Descendants("allepisodes").Descendants("episode").Elements("localpath").Any(y => !string.IsNullOrWhiteSpace(y.Value)));

//    List<Podcast> downloadedPodcasts = new List<Podcast>();

//    foreach (var s in showsWithDownloadedEpisode)
//    {
//        DateTimeParser dateParser = new DateTimeParser();
//        List<Episode> episodesOfShow = new List<Episode>();
//        Show showToAdd = new Show()
//        {
//            PodcastTitle = s.Element("title").Value,
//            RssLink = s.Element("link").Value,
//            ImageUri = s.Element("imageuri").Value != null ? s.Element("imageuri").Value : "https://lh3.googleusercontent.com/BQUYd1Th9Z_XI5wtklPQDHmiNkSOzBakOnpk-Ni8CBTyHb0E7UM5LpyjRW9BWs4fUuVD",
//            ShowId = s.Attribute("sid").Value,
//            Description = s.Element("description").Value,
//            LastBuildDate = dateParser.ConvertStringToDateTime(s.Element("lastbuilddate").Value),
//            LastUpdated = dateParser.ConvertStringToDateTime(s.Element("lastupdated").Value),
//            PublisherName = s.Element("publisher").Value
//        };
//        var c = s.Descendants("allepisodes").Descendants("episode")
//            .Where(x => x.Element("localpath").Value != "");
//        foreach (var item in c)
//        {
//            Episode epToAdd = new Episode();
//            epToAdd.Summary = item.Element("description") != null ? item.Element("description").Value : "";
//            epToAdd.Title = item.Element("title").Value;
//            epToAdd.EpisodeId = item.Attribute("eid").Value;
//            epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
//            epToAdd.PublishDate = dateParser.ConvertStringToDateTime(item.Element("pubdate").Value);
//            epToAdd.FileDetails = new FileInformation();
//            epToAdd.FileDetails.SourceUri = item.Element("url").Value;
//            epToAdd.IsDownloaded = item.Element("localpath").Value != "" ? true : false;
//            epToAdd.DownloadPath = item.Element("localpath").Value != "" ? item.Element("localpath").Value : "";
//            episodesOfShow.Add(epToAdd);
//        }
//        Podcast newPodcast = new Podcast(showToAdd, episodesOfShow);
//        downloadedPodcasts.Add(newPodcast);
//    }
//    var b = showsWithDownloadedEpisode.Descendants("allepisodes").Descendants("episode")
//        .Where(x => x.Element("localpath").Value != "");

//    var test = _dbXDoc.Descendants("localpath")
//        .Where(x => !string.IsNullOrWhiteSpace(x.Value))
//        .SelectMany(x => x.Ancestors("show"))
//        .ToList();

//    var test2 = _dbXDoc.Descendants("localpath")
//        .Where(x => x.Value.Contains("."))
//        .SelectMany(x => x.Ancestors("episode"));


//    //var query = from s in _dbXDoc.Descendants("show")
//    //            select new Show
//    return null;
//}