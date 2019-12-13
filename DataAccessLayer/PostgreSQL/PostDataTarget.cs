using System;

using System.Collections.Generic;

using System.Data;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using Npgsql;

using CommonTypes;



namespace DataAccessLayer.PostgreSQL

{

    public class PostDataTarget : LocalDataTarget, IDataTarget

    {
        PostConnect myConecction;

        NpgsqlConnection conexionOpen;
        public PostDataTarget()
        {
            myConecction = new PostConnect();
        }

        public void PodcastLoad(Podcast myPodcast)
        {
            myConecction = new PostConnect();

            InsertValuesShows(myPodcast.ShowInfo);

            foreach (Episode thisEpi in myPodcast.EpisodeList)
            {
                Console.WriteLine( "Insert episode: " + thisEpi.Title);
                InsertValuesEpisodes(thisEpi);
            }

            Console.ReadKey();
        }


        #region check db, create db and create tables

        // check if the db exist

        public Boolean CheckDatenBank(string db)
        {
            Boolean checkDb = false;

            // check if the DB exist

            string csql = "SELECT * FROM pg_catalog.pg_database where lower(datname) = lower('" + db + "') ";

            try

            {

                NpgsqlCommand Command = new NpgsqlCommand(csql, myConecction.DBConnect(""));

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

        public Boolean Createdb(string db)

        {

            Boolean resp = false;

            // first connection without db

            try

            {
                string csql_create = "CREATE DATABASE " + db + "";

                NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect(""));

                Command.ExecuteNonQuery();

                myConecction.DBDesConnect();

                // conect with the created db

                resp = true;

                return resp;

            }

            catch (Exception)

            {
                return resp;

            }

        }

        // create tables

        public void CreateTable(string db)

        {

            string csql = @"CREATE TABLE shows (sid serial PRIMARY KEY,

                publishername VARCHAR(255) NOT NULL,

				PodcastTitle VARCHAR(255) NOT NULL,

				Subtitle VARCHAR(255),

				description VARCHAR(2000),

				Keywords VARCHAR(255),

				category VARCHAR(100),

				language VARCHAR(30),

				imageUri VARCHAR(5000),

				lastUpDATEd DATE,

				lastBuild DATE

				);

                     

                create table episodes (episodeID serial PRIMARY KEY,

				showID serial REFERENCES shows(sid) ON DELETE CASCADE ON UPDATE CASCADE,

				title VARCHAR(255) NOT NULL,

				Summary VARCHAR(2000),

				Keywords VARCHAR(255),

				publishdate DATE,

				duration VARCHAR(10), 

				sizeOfFile VARCHAR(10),

				filetyp VARCHAR(255),

				author VARCHAR(255) NOT NULL,

				subtitle VARCHAR(255),

				imageUrl VARCHAR(500),

				fileUrl VARCHAR(500) NOT NULL,

				copyright VARCHAR(255),

                isDownload BOOLEAN

				)";



            NpgsqlCommand Command = new NpgsqlCommand(csql, myConecction.DBConnect(db));

            Command.ExecuteNonQuery();

            myConecction.DBDesConnect();

        }



        #endregion



        #region check values



        public DataTable DownloadValues(string db)

        {

            //DataSet values = new DataSet();



            string csql = "SELECT * FROM shows where sid > 45 ";



            NpgsqlCommand comando = new NpgsqlCommand(csql, myConecction.DBConnect(db));

            comando.ExecuteNonQuery();

            //NpgsqlDataReader reader = comando.ExecuteReader();

            //reader.Read();

            //var x = reader["sid"];



            //myTable.Load(comando.ExecuteReader());



            NpgsqlDataAdapter dataAdap = new NpgsqlDataAdapter(comando);

            DataTable myTable = new DataTable("shows");



            dataAdap.Fill(myTable);

            Console.WriteLine("comentar");

            myConecction.DBDesConnect();

            return myTable;

        }

        #endregion



        #region insert values

        public Boolean InsertValues(string db)

        {

            Boolean resp = false;

            //string= "";



            string csql = @"INSERT INTO shows (sid, podcastname,description,publishername,category,language,imageuri,lastupdated,lastbuild)

            VALUES (default, 'other', 'la descripcion','casapub', 'gebeude','spanish', 'imagen','1971-7-13', '1992-5-13'); ";



            NpgsqlCommand Command = new NpgsqlCommand(csql, myConecction.DBConnect(db));

            Command.ExecuteNonQuery();



            myConecction.DBDesConnect();



            return resp;

        }



        #endregion



        #region insert values in show



        public bool InsertValuesShows(Show newShow)

        {

            conexionOpen = new NpgsqlConnection();

            conexionOpen = myConecction.DBConnectionOpen();



            bool resp = false;

            string category = "category";


            string sql = @"INSERT INTO shows (sid, publishername, PodcastTitle, Subtitle, description, Keywords, category, language, imageuri, lastupdated, lastbuild) 

                     VALUES (@sid, @publishername, @PodcastTitle, @Subtitle, @description, @Keywords, @category, @language, @imageuri, @lastupdated, @lastbuild)";

            NpgsqlCommand Command = new NpgsqlCommand(sql, conexionOpen);

            Command.Parameters.AddWithValue("sid", newShow.ShowId);

            Command.Parameters.AddWithValue("publishername", newShow.PublisherName);

            Command.Parameters.AddWithValue("PodcastTitle", newShow.PodcastTitle);

            Command.Parameters.AddWithValue("Subtitle", "xxx");

            Command.Parameters.AddWithValue("description", newShow.Description);

            Command.Parameters.AddWithValue("Keywords", newShow.Keywords);

            Command.Parameters.AddWithValue("category", category);

            Command.Parameters.AddWithValue("language", newShow.Language);

            Command.Parameters.AddWithValue("imageuri", newShow.ImageUri);

            Command.Parameters.AddWithValue("lastupdated", newShow.LastUpdated);

            Command.Parameters.AddWithValue("lastbuild", newShow.LastBuildDate);


            Command.ExecuteNonQuery();



            myConecction.DBDesConnect();



            return resp;

        }



        #endregion



        #region insert values episodes

        public bool InsertValuesEpisodes(Episode newEpisode)

        {

            conexionOpen = new NpgsqlConnection();

            conexionOpen = myConecction.DBConnectionOpen();



            bool resp = false;



            string sql = @"INSERT INTO episodes (episodeid, showid , title, Summary, Keywords, publishdate, duration, sizeoffile, filetyp, author, subtitle, imageurl, fileurl, copyright, isDownload)

            VALUES (default, 1 , @title, @Summary, @Keywords, @publishdate, @duration, @sizeoffile, @filetyp,  @author, @subtitle, @imageurl, @fileurl, @copyright, @isDownload)";





            NpgsqlCommand Command = new NpgsqlCommand(sql, conexionOpen);

            //Command.Parameters.AddWithValue("showid", "aaaa");

            Command.Parameters.AddWithValue("title", newEpisode.Title);

            Command.Parameters.AddWithValue("Summary", newEpisode.Summary);

            Command.Parameters.AddWithValue("Keywords", newEpisode.Keywords);

            Command.Parameters.AddWithValue("publishdate", newEpisode.PublishDate);

            Command.Parameters.AddWithValue("duration", newEpisode.FileDetails.Length);

            Command.Parameters.AddWithValue("sizeoffile", "2222222");

            Command.Parameters.AddWithValue("filetyp", newEpisode.FileDetails.FileType);

            Command.Parameters.AddWithValue("author", "xxxx");

            Command.Parameters.AddWithValue("subtitle", "yyyy");

            Command.Parameters.AddWithValue("imageurl", newEpisode.ImageUri);

            Command.Parameters.AddWithValue("fileurl", newEpisode.FileDetails.SourceUri);

            Command.Parameters.AddWithValue("copyright", "cccc");

            Command.Parameters.AddWithValue("isDownload", newEpisode.IsDownloaded);

            Command.ExecuteNonQuery();



            myConecction.DBDesConnect();



            return resp;

        }

        public void SavePodcast(Podcast podcastToSave)

        {

            throw new NotImplementedException();

        }



        internal override string GetFileName(Podcast podcast)

        {

            throw new NotImplementedException();

        }



        internal override string GetFolderName()

        {

            throw new NotImplementedException();

        }



        #endregion





        #region delete DB

        private bool DeleteShow(Show myShow)
        {
            bool resp = false;

            conexionOpen = new NpgsqlConnection();

            conexionOpen = myConecction.DBConnectionOpen();

            string sql = "delete from shows where shows.showID =" + myShow.ShowId+"";

            NpgsqlCommand Command = new NpgsqlCommand(sql, conexionOpen);

            var reader = Command.ExecuteReader();

            sql = "delete from episodes where episodes.showID = " + myShow.ShowId + "";

            myConecction.DBDesConnect();

            return resp;
        }

        #endregion

        #region update DB
        private bool UpdateShow(Show myShow)
        {
            bool resp = false;

            // delte the show and episodes and then reload
            DeleteShow(myShow);

            // insert the show
            InsertValuesShows(myShow);

            
            //InsertValuesEpisodes();

            return resp;
        }

        #endregion


    }

}