﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: MA
    /// </summary>
    public class PostgresData
    {
        public static NpgsqlConnection con = new NpgsqlConnection();
        #region Connect and disconnect

        public Boolean Connect(string db)
        {
            con.ConnectionString = @"User ID = postgres; password=soloyo;host=localhost;database=" + db + ";port=5432;commandtimeout=900";

            Boolean state = true;
            try
            {
                con.Open();

            }
            catch (Exception)
            {
                state = false;
            }
            return state;

        }
        public void DesConnect()
        {
            con.Close();
        }
        #endregion

        #region check db, create db and create tables
        // check if the db exist
        public Boolean CheckDatenBank(string db)
        {
            Boolean checkDb = false;
            // check if the DB exist
            string csql = "SELECT * FROM pg_catalog.pg_database where lower(datname) = lower('" + db + "') ";
            try
            {
                Connect("");
                NpgsqlCommand Command = new NpgsqlCommand(csql, con);
                NpgsqlDataReader reader = Command.ExecuteReader();
                checkDb = reader.HasRows;
                con.Close();
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
                Connect("");

                string csql_create = "CREATE DATABASE " + db + "";
                NpgsqlCommand Command = new NpgsqlCommand(csql_create, con);
                Command.ExecuteNonQuery();
                DesConnect();
                // conect with the created db

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
            Connect(db);
            string csql = @"CREATE TABLE shows (sid serial PRIMARY KEY,
				podcastName VARCHAR(255) NOT NULL,
				description VARCHAR(2000),
				publisherName VARCHAR(100),
				category VARCHAR(100),
				language VARCHAR(30),
				imageUri VARCHAR(5000),
				lastUpDATEd DATE,
				lastBuild DATE
				);
                create table downloads(downID serial PRIMARY KEY,				
				filepath VARCHAR(255)
				);
                create table episodes (episodeID serial PRIMARY KEY,
				showID serial REFERENCES shows(sid),
				downloadID serial REFERENCES downloads(downID),
				name VARCHAR(255) NOT NULL,
				title VARCHAR(255) NOT NULL,
				description VARCHAR(2000),
				publishDATE DATE,
				duration VARCHAR(10) NOT NULL, 
				sizeOfFile VARCHAR(10),
				author VARCHAR(255) NOT NULL,
				subtitle VARCHAR(255),
				imageUrl VARCHAR(500),
				fileUrl VARCHAR(500) NOT NULL,
				copyright VARCHAR(255)
				);";

            NpgsqlCommand Command = new NpgsqlCommand(csql, con);
            Command.ExecuteNonQuery();
            DesConnect();
        }





        #endregion

        #region check values

        public DataTable DownloadValues(string db)
        {
            //DataSet values = new DataSet();


            Connect(db);
            string csql = "SELECT * FROM shows where sid > 45 ";

            NpgsqlCommand comando = new NpgsqlCommand(csql, con);
            comando.ExecuteNonQuery();
            //NpgsqlDataReader reader = comando.ExecuteReader();
            //reader.Read();
            //var x = reader["sid"];

            //myTable.Load(comando.ExecuteReader());

            NpgsqlDataAdapter dataAdap = new NpgsqlDataAdapter(comando);
            DataTable myTable = new DataTable("shows");

            dataAdap.Fill(myTable);
            Console.WriteLine("comentar");
            DesConnect();
            return myTable;
        }
        #endregion

        #region insert values
        public Boolean InsertValues(string db)
        {
            Boolean resp = false;
            //string= "";

            Connect(db);
            string csql = @"INSERT INTO shows (sid, podcastname,description,publishername,category,language,imageuri,lastupdated,lastbuild)
            VALUES (default, 'other', 'la descripcion','casapub', 'gebeude','spanish', 'imagen','1971-7-13', '1992-5-13'); ";

            NpgsqlCommand Command = new NpgsqlCommand(csql, con);
            Command.ExecuteNonQuery();

            DesConnect();

            return resp;
        }

        #endregion

        #region insert values shows
        public Boolean InsertValuesShows(string db, string pName, string pDescr, string pPublName, string pCateg, string pLang, string pImage, DateTime pLUp, DateTime pLBuild)
        {
            Boolean resp = false;

            Connect(db);
            string csql = @"INSERT INTO shows (sid, podcastname,description,publishername,category,language,imageuri,lastupdated,lastbuild)
            VALUES (default, '" + pName + "', '" + pDescr + "','" + pPublName + "', '" + pCateg + "','" + pLang + "', '" + pImage + "','" + pLUp + "', '" + pLBuild + "'); ";

            var insertCommand = new NpgsqlCommand(@"INSERT INTO shows VALUES (@podcastname, @publishername)");
            insertCommand.Parameters.AddWithValue("podcastname", pName);



            NpgsqlCommand Command = new NpgsqlCommand(csql, con);
            Command.ExecuteNonQuery();

            DesConnect();

            return resp;
        }

        #endregion

        #region insert values episodes

        public Boolean InsertValuesEpisodes(string db, string eTitle, DateTime ePubDate, string eDesc, string eKeyword, string eImage, string eDeta)
        {
            Boolean resp = false;
            int showid = 1;
            int downloadid = 1;
            string eAuthor = "author";
            string eSub = "subtitle";
            string eCopyright = "copyright";

            Connect(db);
            string csql = @"INSERT INTO episodes (episodeid, showid, downloadid, name, title,description,publishdate,duration,sizeoffile,author,subtitle,imageurl,fileurl,copyright)
            VALUES (default, '" + showid + "', '" + downloadid + "','" + eTitle + "', '" + eTitle + "','" + eDesc + "', '" + ePubDate + "',12,12,'" + eAuthor + "','" + eSub + "', '" + eImage + "', '" + eDeta + "', '" + eCopyright + "'); ";

            NpgsqlCommand Command = new NpgsqlCommand(csql, con);
            Command.ExecuteNonQuery();

            DesConnect();

            return resp;
        }

        #endregion

        #region insert values Downlad

        public Boolean InsertValuesDownload(string db, string path)
        {
            Boolean resp = false;

            Connect(db);
            string csql = @"INSERT INTO downloads (downid, filepath)
            VALUES (default, '" + path + "');";

            NpgsqlCommand Command = new NpgsqlCommand(csql, con);
            Command.ExecuteNonQuery();

            DesConnect();

            return resp;
        }

        #endregion
    }
}