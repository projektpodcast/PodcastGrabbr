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
            throw new NotImplementedException();
        }
        public void DeletePodcast(Podcast podcastToDelete)
        {

            throw new NotImplementedException();
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
            Parameter fileSize = new Parameter("@sizeOfFile", e.FileDetails.FileSize);
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

            paramList.Add(showName);
            paramList.Add(description);
            paramList.Add(publisher);
            paramList.Add(category);
            paramList.Add(language);
            paramList.Add(imageUri);
            paramList.Add(lastUpdated);
            paramList.Add(lastBuild);

            return paramList;
        }
        public string CreatePodacstInsert()
        {
            String sql = "INSERT INTO shows VALUES(@sid, @showName, @description, @publisher, @category, @language, @imageUri, @lastUpdated, @lastBuild)";

            return sql;
        }
        public string CreateEpisodeInsert()
        {
            String sql = "";
            sql = "INSERT INTO episodes VALUES(@episodeID, @showID, @downloadID, @title, @description, @publishDate, @duration, @sizeOfFile, @author, @imageUrl, @fileUrl, @copyright)";

            return sql;
        }






        public void SavePodcast(Podcast podcastToSave)
        {
            throw new NotImplementedException();
        }
    }
}
