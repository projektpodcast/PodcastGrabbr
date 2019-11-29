using CommonTypes;
using Mk.DBConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MySQLDataTarget : IDataTarget
    {
        /**
         * Hier implementier ich meine Connection
         * 
         */

        public static DBAdapter Adapter;

        static void Connection()
        {
            MySQLDataTarget.Adapter = new DBAdapter(DatabaseType.MySql, Instance.NewInstance, "10.194.9.130", 3306, "podcastmanager", "datauser", "password123", "logfile.txt");

            MySQLDataTarget.Adapter.Adapter.LogFile = true;
            MySQLDataTarget.Adapter.Adapter.CheckConnectionState();
        }
        public void InsertPodcast(Podcast newPodcast)
        {
            Connection();
            var unbekannteZahl = MySQLDataTarget.Adapter.Adapter.InsertParameters(CreatePodacstInsert(), getShowParameters(newPodcast));

        }
        public void UpdatePodcast(Podcast oldPodcast, Podcast newPodcast)
        {
            string stuff = oldPodcast.ShowInfo.ShowID;
            var unbekannteZahl = MySQLDataTarget.Adapter.Adapter.InsertParameters(CreatePodcastUpdate(), getShowParameters(newPodcast));
            throw new NotImplementedException();
        }
        public void DeletePodcast(Podcast podcastToDelete)
        {
            Connection();
            var unbekannteZahl = MySQLDataTarget.Adapter.Adapter.InsertParameters(CreatePodcastDelete(), getShowParameters(podcastToDelete));
        }
        public void InsertEpisodes(Podcast newPodcast)
        {
            foreach (Episode e in newPodcast.EpisodeList)
            {
                var unbekannteZahl = MySQLDataTarget.Adapter.Adapter.InsertParameters(CreateEpisodeInsert(), getEpisodeParameters(newPodcast, e));
            }

            throw new NotImplementedException();
        }
        public void UpdateEpisode(Podcast podcastToDelete)
        {

            throw new NotImplementedException();
        }
        public void DeleteEpisode(Podcast podcastToDelete)
        {

            throw new NotImplementedException();
        }

        private List<Parameter> getEpisodeParameters(Podcast newPodcast, Episode e)
        {
            List<Parameter> paramList = new List<Parameter>();

            Parameter showId = new Parameter("@showID", newPodcast.ShowInfo.ShowID);
            Parameter title = new Parameter("@title", e.Title);
            Parameter description = new Parameter("@description", e.Description);
            Parameter publishDate = new Parameter("@publishDate", e.PublishDate);
            Parameter duration = new Parameter("@duration", e.FileDetails.Duration);
            Parameter fileSize = new Parameter("@fileSize", e.FileDetails.FileSize);
            Parameter author = new Parameter("@author", newPodcast.ShowInfo.PublisherName);
            Parameter imageUrl = new Parameter("@imageUrl", e.ImageUri);
            Parameter fileUrl = new Parameter("@fileUrl", e.FileDetails.SourceUri);
            Parameter copyright = new Parameter("@copyright", e.FileDetails.Copyright);

            paramList.Add(showId);
            paramList.Add(title);
            paramList.Add(description);
            paramList.Add(publishDate);
            paramList.Add(fileSize);
            paramList.Add(author);
            paramList.Add(imageUrl);
            paramList.Add(fileUrl);
            paramList.Add(copyright);

            return paramList;
        }
        private List<Parameter> getShowParameters(Podcast newPodcast)
        {
            List<Parameter> paramList = new List<Parameter>();
            Parameter showName = new Parameter("@showName", newPodcast.ShowInfo.ShowName);
            Parameter description = new Parameter("@description", newPodcast.ShowInfo.Description);
            Parameter publisher = new Parameter("@publisher", newPodcast.ShowInfo.PublisherName);
            Parameter category = new Parameter("@category", newPodcast.ShowInfo.Category);
            Parameter language = new Parameter("@language", newPodcast.ShowInfo.Language);
            Parameter imageUri = new Parameter("@imageUri", newPodcast.ShowInfo.ImageUri);
            Parameter lastUpdated = new Parameter("@lastUpdated", newPodcast.ShowInfo.LastUpdated);
            Parameter lastBuild = new Parameter("@lastBuild", newPodcast.ShowInfo.LastBuildDate);
            Parameter feedUrl = new Parameter("@feedUrl", newPodcast.ShowInfo.FeedUrl);

            paramList.Add(showName);
            paramList.Add(description);
            paramList.Add(publisher);
            paramList.Add(category);
            paramList.Add(language);
            paramList.Add(imageUri);
            paramList.Add(lastUpdated);
            paramList.Add(lastBuild);
            paramList.Add(feedUrl);

            return paramList;
        }
        public string CreatePodacstInsert()
        {
            String sql = "INSERT INTO shows VALUES(@sid, @showName, @description, @publisher, @category, @language, @imageUri, @lastUpdated, @lastBuild, @feedUrl)";

            return sql;
        }

        public string CreatePodcastUpdate()
        {
            String sql = "";
            sql = "UPDATE shows SET showName = @showName, description = @description, publisher = @publisher, category = @category, language = @language, imageUri = @imageUri, lastUpdated = @lastUpdated, lastBuild = @lastBuild, feedUrl = @feedUrl";
            sql += " WHERE showID = @sid;";

            return sql;
        }
        public string CreatePodcastDelete()
        {
            String sql = ""; // sid = @sid

            sql = "DELETE FROM shows WHERE showName = '@showName'";
            sql += " AND description = '@description'";
            sql += " AND publisherName = '@publisher'";
            sql += " AND category = '@category'";
            sql += " AND language = '@language'";
            sql += " AND imageUri = '@imgaeUri'";
            sql += " AND lastUpdate = '@lastUpdated'";
            sql += " AND lastBuild = '@lastBuild'";

            return sql;
        }
        public string CreateEpisodeInsert()
        {
            String sql = "";
            sql = "INSERT INTO episodes VALUES(@episodeID, @showID, @downloadID, @title, @description, @publishDate, @duration, @fileSize, @author, @imageUrl, @fileUrl, @copyright)";

            return sql;
        }
        public string CreateEpisodeUpdate()
        {
            String sql = "";
            sql = "";

            return sql;
        }
        public string CreateEpisodeDelete()
        {
            String sql = "";
            sql = "DELETE FROM episodes  WHERE title = '@title'";
            sql += " AND description = '@description'";
            sql += " AND publishDate = '@publishDate'";
            sql += " AND duration = '@duration'";
            sql += " AND fileSize = '@fileSize'";
            sql += " AND author = '@author'";
            sql += " AND lastBuild = '@lastBuild'";
            sql += " AND imageUrl = '@imageUrl'";
            sql += " AND fileUrl = '@fileUrl'";
            sql += " AND copyright = '@copyright'";

            return sql;
        }

        public string DeleteAllShowEpisodes()
        {
            String sql = "";
            sql = "DELETE FROM episodes WHERE showID = '@showID'";

            return sql;
        }
    }
}
