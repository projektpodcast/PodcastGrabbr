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
            MySQLDataTarget.Adapter = new DBAdapter(DatabaseType.MySql, Instance.NewInstance, "10.194.9.130", 3306, "pdocastmanager", "root", "iec9bei0weex7baShieg", "logfile.txt");

            MySQLDataTarget.Adapter.Adapter.LogFile = true;
            MySQLDataTarget.Adapter.Adapter.CheckConnectionState();
        }

        public void SavePodcast(Podcast podcastToSave)
        {
            throw new NotImplementedException();
        }


        public void DeletePodcast(Podcast podcastToDelete)
        {
            throw new NotImplementedException();
        }

        public void InsertPodcast(Podcast newPodcast)
        {

            Connection();

            var unbekannteZahl = MySQLDataTarget.Adapter.Adapter.InsertParameters(CreatePodacstInsert(), getShowParameters(newPodcast));

        }
        public void InsertEpisodes(Podcast newPodcast)
        {
            var unbekannteEpisode = MySQLDataTarget.Adapter.Adapter.InsertParameters()
            throw new NotImplementedException();
        }

        public void UpdatePodcast(Podcast oldPodcast, Podcast newPodcast)
        {
            throw new NotImplementedException();
        }
        private List<Parameter> getEpisodeParameters(Podcast newPodcast)
        {
            List<Parameter> paramList = new List<Parameter>();
            Parameter episodeName = new Parameter("@name", newPodcast.EpisodeList)


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
            String sql = "INSERT INTO shows VALUES(@showName, @description, @publisher, @category, @language, @imageUri, @lastUpdated, @lastBuild)";

            return sql;
        }
        public string CreateEpisodeInsert(Podcast newPodcast)
        {
            String sql = "";
            List<Parameter> paramList = new List<Parameter>();
            int showID = newPodcast.ShowInfo.ShowID;


            foreach (Episode item in newPodcast.EpisodeList)
            {
                Episode e = item;
                sql = "INSERT INTO episodes VALUES(@showID, @title, @description, @publishDate, @duration, @sizeOfFile, @author, @imageUrl, @fileUrl, @copyright)";
                    //add parameter... e.podcasttitle
                Parameter showId = new Parameter("@showID", showID);
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
                    
            }




            return "";
        }
    }
}
