using BusinessLayer;
using CommonTypes;
using DataAccessLayer;
using RssFeedProcessor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MockPresentationLayer
{
    /// <summary>
    /// Dieses Projekt besteht nur für Testzwecke. Hier können Methodenaufrufe und -abläufe getestet werden.
    /// Dieses Projekt muss dafür als "Startup Project" in den Properties der Solution eingestellt werden.
    /// </summary>
    [Obsolete]
    class Program
    {
        static void Main(string[] args)
        {

            //string password = "#H1a!l@l^oMeinPasswortIstSuperLang";
            ////string encryptionString = "asd123asd123";
            //string encryptedPassword = SecurityLibrary.StringCipher.Encrypt(password);

            //string decryptedPassword = SecurityLibrary.StringCipher.Decrypt(encryptedPassword);

            //DeserializingManager deserializer = new DeserializingManager();

            //Podcast a = deserializer.DeserializeRssXml("http://joeroganexp.joerogan.libsynpro.com/rss");

            ////deserializer.SerializeIt(a);
            ///
            //string url2 = "http://podcast.wdr.de/quarks.xml";
            ////string url2 = "http://joeroganexp.joerogan.libsynpro.com/rss";
            ////string url2 = "http://www1.swr.de/podcast/xml/swr2/forum.xml";
            ////string url2 = "http://web.ard.de/radiotatort/rss/podcast.xml";
            //LocalRssTest test = new LocalRssTest();
            //test.ProcessNewPodcast(url2);

            ////Podcast a = deserializer.DeserializeRssXml("http://podcast.wdr.de/quarks.xml");
            ////Podcast a = deserializer.DeserializeRssXml("http://web.ard.de/radiotatort/rss/podcast.xml");
            //Podcast a = deserializer.DeserializeRssXml("https://www1.wdr.de/radio/podcasts/wdr2/kabarett132.podcast");
            ////Podcast a = deserializer.DeserializeRssXml("http://www1.swr.de/podcast/xml/swr2/forum.xml");

            //PostgresData PostDbInstance = new PostgresData();
            //call postdb
            //PostDB(a);

        }

        //static void PostDB(Podcast a)
        //{

        //    // new Object to connect with Postgres
        //    PostgresData myConnection = new PostgresData();

        //    // dadaBase Name
        //    string dbName = "test_podcast1";

        //    // check if the DB exist
        //    Boolean dbExist = myConnection.CheckDatenBank(dbName);
        //    if (dbExist == false)
        //    {
        //        // create db if not exist.
        //        bool ifcreated = myConnection.Createdb(dbName);
        //        if (ifcreated == false)
        //        {
        //            Console.WriteLine("ERROR:");
        //            Console.WriteLine("make sure you have pgAdmin open");
        //            System.Threading.Thread.Sleep(5000);
        //            System.Environment.Exit(1);
        //        }
        //        else
        //        {
        //            myConnection.CreateTable(dbName);
        //            Console.WriteLine("The db cas created");
        //        }

        //    }
        //    else
        //    {
        //        Console.WriteLine("The db ist already to use");
        //    }

        //    //read values from Shows
        //    string _PublisherName = a.ShowInfo.PublisherName;
        //    string _PodcastTitle = a.ShowInfo.PodcastTitle;
        //    string Keywords = a.ShowInfo.Keywords;
        //    string _Subtitle = a.ShowInfo.Subtitle;
        //    string _Language = a.ShowInfo.Language;
        //    string _Description = a.ShowInfo.Description;
        //    DateTime LastUpdated = a.ShowInfo.LastUpdated;
        //    DateTime LastBuildDate = a.ShowInfo.LastBuildDate;
        //    string ImageUri = a.ShowInfo.ImageUri;
        //    string _Categorie = "new";

        //    // inster values in shows
        //    myConnection.InsertValuesShows(dbName, _PublisherName, _Description, _PublisherName, _Categorie, _Language, ImageUri, LastUpdated, LastBuildDate);



        //    // read episodios
        //    foreach (var Episode in a.EpisodeList)
        //    {
        //        string _Title = Episode.Title;
        //        _Title = _Title.Replace("'", "''");
        //        DateTime _PublishDate = Episode.PublishDate;
        //        string _Summary = Episode.Summary;
        //        _Summary = _Summary.Replace("'", "''");
        //        string _Keywords = Episode.Keywords;
        //        string _ImageUri = Episode.ImageUri;
        //        string _FileDetails = Episode.FileDetails.SourceUri;

        //        // inser values in each episode
        //        myConnection.InsertValuesDownload(dbName, _FileDetails);
        //        myConnection.InsertValuesEpisodes(dbName, _Title, _PublishDate, _Summary, _Keywords, _ImageUri, _FileDetails);
        //    }


    }
    }
