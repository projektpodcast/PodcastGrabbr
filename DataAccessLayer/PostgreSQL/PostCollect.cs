using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace DataAccessLayer.PostgreSQL
{
    public class PostCollect
    {

        public void PodcastRead(Podcast a)
        {
            // new Object to connect with Postgres
            PostDataTarget myConnection = new PostDataTarget();

            PostDataSource myConnectionRead = new PostDataSource();


            // dadaBase Name
            string dbName = "test";

            // check if the DB exist
            Boolean dbExist = myConnection.CheckDatenBank(dbName);
            if (dbExist == false)
            {
                // create db if not exist.
                bool ifcreated = myConnection.Createdb(dbName);
                if (ifcreated == false)
                {
                    Console.WriteLine("ERROR:");
                    Console.WriteLine("make sure you have pgAdmin open");
                    System.Threading.Thread.Sleep(5000);
                    System.Environment.Exit(1);
                }
                else
                {
                    myConnection.CreateTable(dbName);
                    Console.WriteLine("The db cas created");
                }

            }
            else
            {
                Console.WriteLine("The db ist already to use");
            }


            //read values from Shows
            string _PublisherName = a.ShowInfo.PublisherName;
            string _PodcastTitle = a.ShowInfo.PodcastTitle;
            string Keywords = a.ShowInfo.Keywords;
            string _Subtitle = a.ShowInfo.Subtitle;
            string _Language = a.ShowInfo.Language;
            string _Description = a.ShowInfo.Description;
            DateTime LastUpdated = a.ShowInfo.LastUpdated;
            DateTime LastBuildDate = a.ShowInfo.LastBuildDate;
            string ImageUri = a.ShowInfo.ImageUri;
            string _Categorie = "new";

            // if the episode exist dont make nothing
            // TODO read   SHOW id
            if ( myConnectionRead.CheckShow("1", dbName) == false)
            {
                // inster values in shows
                myConnection.InsertValuesShows(dbName, _PublisherName, _Description, _PublisherName, _Categorie, _Language, ImageUri, LastUpdated, LastBuildDate);
                
                // read episodios
                foreach (var Episode in a.EpisodeList)
                {
                    string _Title = Episode.Title;
                    _Title = _Title.Replace("'", "''");
                    DateTime _PublishDate = Episode.PublishDate;
                    string _Summary = Episode.Summary;
                    _Summary = _Summary.Replace("'", "''");
                    string _Keywords = Episode.Keywords;
                    string _ImageUri = Episode.ImageUri;
                    string _FileDetails = Episode.FileDetails.SourceUri;

                    // inser values in each episode
                    myConnection.InsertValuesEpisodes(dbName, _Title, _PublishDate, _Summary, _Keywords, _ImageUri, _FileDetails);
                }
            }


            myConnectionRead.getShows(dbName);
            myConnectionRead.getAllEpisodes(dbName);
            





        }
    }
}
