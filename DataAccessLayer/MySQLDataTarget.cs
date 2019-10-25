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
            MySQLDataTarget.Adapter = new DBAdapter(DatabaseType.MySql,Instance.NewInstance,"10.194.9.130",3306,"pdocastmanager","root","iec9bei0weex7baShieg","logfile.txt");

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

            throw new NotImplementedException();
        }

        public void UpdatePodcast(Podcast oldPodcast, Podcast newPodcast)
        {
            throw new NotImplementedException();
        }
        private List<Parameter> getEpisondeParameters(Podcast newPodcast)
        {
            List<Parameter> paramList = new List<Parameter>();
            

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
            return "";
        }
    }
}
