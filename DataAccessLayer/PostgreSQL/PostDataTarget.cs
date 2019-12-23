using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using CommonTypes;
using RssFeedProcessor;

namespace DataAccessLayer.PostgreSQL
{    
    /// <summary>
     /// AUTHOR: MA
     /// </summary>
    public class PostDataTarget : IDataTarget
    {
        PostConnect myConecction;
        NpgsqlConnection conexionOpen;



        public PostDataTarget(IDataStorageType DatenHaltung)
        {
            //1. verbindung öffnen -> postgressconn das onjekt storagedata übergeben
            myConecction = new PostConnect(DatenHaltung);

            try
            {
                if (!CheckDatenBank(PostDataSource.dbData.DataBaseName, myConecction.DBConnectCheck()))
                {
                    Createdb(PostDataSource.dbData.DataBaseName, myConecction.DBConnectCheck());
                    CreateTable(PostDataSource.dbData.DataBaseName, myConecction.DBConnect());
                }
                else
                {
                    myConecction.DBDesConnect();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        #region check db, create db and create tables



        // check if the db exist
        public Boolean CheckDatenBank(string db, NpgsqlConnection myConnection)

        {

            Boolean checkDb = false;
            string csql = "SELECT * FROM pg_catalog.pg_database where lower(datname) = lower('" + db + "') ";
            try
            {
                NpgsqlCommand Command = new NpgsqlCommand(csql, myConnection);

                NpgsqlDataReader reader = Command.ExecuteReader();

                checkDb = reader.HasRows;
                myConecction.DBDesConnect();
                return checkDb;

            }

            catch (Exception)

            {
                return checkDb;
            }
        }



        //Create the db if do not exist
        public Boolean Createdb(string db, NpgsqlConnection myConnection)

        {

            Boolean resp = false;

            // first connection without db

            try

            {
                string csql_create = "CREATE DATABASE " + db + "";
                NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConnection);
                Command.ExecuteNonQuery();



                myConecction.DBDesConnect();
                resp = true;

                return resp;

            }
            catch (Exception e)

            {
                Console.WriteLine(e);
                return resp;
            }
        }



        // create both tables (shows and episodes)
        public void CreateTable(string db, NpgsqlConnection myConnection)

        {


            string csql = @"CREATE TABLE shows (showid integer PRIMARY KEY,
                publishername VARCHAR(255) NOT NULL,
				podcasttitle VARCHAR(255) NOT NULL,
                categorie VARCHAR(100),
                Keywords VARCHAR(255),
				subtitle VARCHAR(255),
				language VARCHAR(30),
				description VARCHAR(2000),
				lastupdated DATE,
				lastbuilddate DATE,
				imageuri VARCHAR(5000),
				rsslink VARCHAR(5000)
				);


                create table episodes (episodeid serial PRIMARY KEY,
				showid integer REFERENCES shows(showid) ON DELETE CASCADE ON UPDATE CASCADE,
				title VARCHAR(255) NOT NULL,
				publishdate DATE,
				summary VARCHAR(2000),
				keywords VARCHAR(255),
				imageuri VARCHAR(5000),
				duration VARCHAR(10), 
				filetyp VARCHAR(255),
				fileurl VARCHAR(500) NOT NULL,
				imageurl VARCHAR(500),
                isDownload BOOLEAN,
                path VARCHAR(500)
				)";

            NpgsqlCommand Command = new NpgsqlCommand(csql, myConnection);

            Command.ExecuteNonQuery();
            myConecction.DBDesConnect();

        }

        #endregion




        #region insert values in show
        public bool InsertValuesShows(Show newShow, NpgsqlConnection myConnection)
        {

            bool resp = false;

            string sql = @"INSERT INTO shows (showid, publishername, podcasttitle,
            categorie, Keywords, subtitle, language, description, lastupdated, lastbuilddate, imageuri, rsslink) 
            VALUES (@showid, @publishername, @podcasttitle, @categorie, @Keywords, 
            @subtitle, @language, @description, @lastupdated, @lastbuilddate, @imageuri, @rsslink)";

            NpgsqlCommand Command = new NpgsqlCommand(sql, myConnection);

            Command.Parameters.AddWithValue("showid", Convert.ToInt32(newShow.ShowId));
            Command.Parameters.AddWithValue("publishername", newShow.PublisherName);
            Command.Parameters.AddWithValue("podcasttitle", newShow.PodcastTitle);
            Command.Parameters.AddWithValue("categorie", "newCategorie");
            //TODO new table for categorie
            //Command.Parameters.AddWithValue("categorie", newShow.Category);
            Command.Parameters.AddWithValue("Keywords", newShow.Keywords);
            Command.Parameters.AddWithValue("subtitle", "Subtitle");
            //Command.Parameters.AddWithValue("subtitle", newShow.Subtitle);
            Command.Parameters.AddWithValue("language", newShow.Language);
            Command.Parameters.AddWithValue("description", newShow.Description);
            Command.Parameters.AddWithValue("lastupdated", newShow.LastUpdated);
            Command.Parameters.AddWithValue("lastbuilddate", newShow.LastBuildDate);
            Command.Parameters.AddWithValue("imageuri", newShow.ImageUri);
            Command.Parameters.AddWithValue("rsslink", "RssLink");
            //Command.Parameters.AddWithValue("rsslink", newShow.RssLink);

            Command.ExecuteNonQuery();


            return resp;
        }

        #endregion

        #region insert values episodes



        public bool InsertValuesEpisodes(Episode newEpisode, String showId, NpgsqlConnection myConnection)

        {
            bool resp = false;

            string sql = @"INSERT INTO episodes (episodeid, showid , title, publishdate, 
            summary, keywords, imageuri, duration, filetyp, fileurl, imageurl, 
            isDownload, path)
            VALUES (default, @showid, @title, @publishdate, 
            @summary, @keywords, @imageuri, @duration, @filetyp, @fileurl, @imageurl, 
            @isDownload, @path)";


            NpgsqlCommand Command = new NpgsqlCommand(sql, myConnection);

            Command.Parameters.AddWithValue("showid", Convert.ToInt32(showId));
            Command.Parameters.AddWithValue("title", newEpisode.Title);
            Command.Parameters.AddWithValue("publishdate", newEpisode.PublishDate.Date);
            Command.Parameters.AddWithValue("summary", newEpisode.Summary);
            Command.Parameters.AddWithValue("keywords", newEpisode.Keywords);
            Command.Parameters.AddWithValue("imageuri", newEpisode.ImageUri);
            Command.Parameters.AddWithValue("duration", newEpisode.FileDetails.Length);
            Command.Parameters.AddWithValue("filetyp", newEpisode.FileDetails.FileType);
            Command.Parameters.AddWithValue("fileurl", newEpisode.FileDetails.SourceUri);
            Command.Parameters.AddWithValue("imageurl", newEpisode.ImageUri);
            Command.Parameters.AddWithValue("isDownload", newEpisode.IsDownloaded);
            //Command.Parameters.AddWithValue("path", newEpisode.LocalMediaPath);
            Command.Parameters.AddWithValue("path", newEpisode.DownloadPath);
            Command.ExecuteNonQuery();


            return resp;

        }

        public void SavePodcast(string rssUri)
        {
            DeserializingManager rssFeed = new DeserializingManager();
            Podcast podcastToSave = rssFeed.DeserializeRssXml(rssUri);
            SavePodcast(podcastToSave);
        }

        //save the show whit the episodes
        public void SavePodcast(Podcast podcastToSave)
        {
            // collect show id
            string csql_create = "select count(showid) from shows";

            NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect());
            var reader = Command.ExecuteScalar();


            int id = Convert.ToInt32(reader);

            podcastToSave.ShowInfo.ShowId = (id + 1).ToString();
            myConecction.DBDesConnect();

            NpgsqlConnection con = myConecction.DBConnect();

            InsertValuesShows(podcastToSave.ShowInfo, con);

            int countId = 1;
            // it will save each episde from a show
            foreach (Episode thisEpi in podcastToSave.EpisodeList)
            {
                thisEpi.EpisodeId = countId.ToString();
                Console.WriteLine("Insert episode: " + thisEpi.Title);
                InsertValuesEpisodes(thisEpi, podcastToSave.ShowInfo.ShowId, con);
                countId++;
            }
            myConecction.DBDesConnect();

        }
        #endregion

        #region delete DB
        // this function will erase the db
        private bool DeleteShow(Show myShow)

        {
            bool resp = false;

            conexionOpen = new NpgsqlConnection();

            conexionOpen = myConecction.DBConnect();

            string sql = "delete from shows where shows.showID =" + myShow.ShowId + "";

            NpgsqlCommand Command = new NpgsqlCommand(sql, conexionOpen);


            var reader = Command.ExecuteReader();

            sql = "delete from episodes where episodes.showID = " + myShow.ShowId + "";

            myConecction.DBDesConnect();

            return resp;
        }



        #endregion

        #region update DB
        // to update the db, first wil be erased and then will be generated a new db
        private bool UpdateShow(Podcast podcastToSave)

        {

            bool resp = false;

            // delte the show and episodes and then reload

            DeleteShow(podcastToSave.ShowInfo);

            // generate the shows and episodes
            SavePodcast(podcastToSave);

            return resp;

        }



        public void InsertDownloadPath(Show show, Episode episode, string path)
        {

        }

        public void BulkCopyPodcasts(List<string> rssUriList)
        {
            throw new NotImplementedException();
        }

        #endregion

    }



}