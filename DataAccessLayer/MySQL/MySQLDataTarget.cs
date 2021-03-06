﻿using CommonTypes;
using Mk.DBConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: RK
    /// </summary>
    public class MySQLDataTarget : IDataTarget
    {

        //Hier implementier ich meine Connection

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

            var unbekannteZahl = MySQLDataTarget.Adapter.Adapter.InsertParameters(CreatePodacstInsert(), GetShowParameters(newPodcast));

        }
        public void InsertEpisodes(Podcast newPodcast)
        {

            throw new NotImplementedException();
        }

        public void UpdatePodcast(Podcast oldPodcast, Podcast newPodcast)
        {
            throw new NotImplementedException();
        }

        private List<Parameter> GetEpisondeParameters(Podcast newPodcast)
        {
            List<Parameter> paramList = new List<Parameter>();


            return paramList;
        }

        private List<Parameter> GetShowParameters(Podcast newPodcast)
        {
            List<Parameter> paramList = new List<Parameter>();
            Parameter showName = new Parameter("@showName", newPodcast.ShowInfo.PodcastTitle);
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

        public void InsertDownloadPath(Show show, Episode episode, string path)
        {
            throw new NotImplementedException();
        }

        void IDataTarget.InsertDownloadPath(Show show, Episode episode, string path)
        {
            throw new NotImplementedException();
        }

        public void SavePodcast(string rssUri)
        {
            RssFeedProcessor.DeserializingManager deserializer = new RssFeedProcessor.DeserializingManager();
            Podcast p = deserializer.DeserializeRssXml(rssUri);
            SavePodcast(p);
        }

        public void BulkCopyPodcasts(List<string> rssUriList)
        {
            throw new NotImplementedException();
        }
    }
}