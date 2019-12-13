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



            PostDataTarget myConnectionTarget = new PostDataTarget();







            // dadaBase Name



            string dbName = "test3";







            // check if the DB exist



            Boolean dbExist = myConnectionTarget.CheckDatenBank(dbName);



            if (dbExist == false)



            {



                // create db if not exist.



                bool ifcreated = myConnectionTarget.Createdb(dbName);



                if (ifcreated == false)



                {



                    Console.WriteLine("ERROR:");



                    Console.WriteLine("make sure you have pgAdmin open");



                    System.Threading.Thread.Sleep(5000);



                    System.Environment.Exit(1);



                }



                else



                {



                    myConnectionTarget.CreateTable(dbName);



                    Console.WriteLine("The db cas created");



                }







            }



            else



            {



                Console.WriteLine("The db ist already to use");



            }















            Show podcastShow = new Show();







            // TODO read   SHOW id



            podcastShow.PublisherName = a.ShowInfo.PublisherName;

            podcastShow.PodcastTitle = a.ShowInfo.PodcastTitle;

            podcastShow.Subtitle = a.ShowInfo.Subtitle;

            podcastShow.Keywords = a.ShowInfo.Keywords;

            podcastShow.Subtitle = a.ShowInfo.Subtitle;

            podcastShow.Language = a.ShowInfo.Language;

            podcastShow.Description = a.ShowInfo.Description;

            podcastShow.LastUpdated = a.ShowInfo.LastUpdated;

            podcastShow.LastBuildDate = a.ShowInfo.LastBuildDate;

            podcastShow.ImageUri = a.ShowInfo.ImageUri;



            // category is a list, we need another table for this



            //podcastShow.Category = "new";











            PostDataSource myConnectionRead = new PostDataSource();







            // if the episode exist dont make nothing











            // inster values in shows



            myConnectionTarget.InsertValuesShows(podcastShow);







            // read episodios



            Episode myEpisode = new Episode();



            foreach (var Episode in a.EpisodeList)



            {



                myEpisode.Title = Episode.Title;



                myEpisode.Summary = Episode.Summary;



                myEpisode.Keywords = Episode.Keywords;



                myEpisode.PublishDate = Episode.PublishDate;



                myEpisode.ImageUri = Episode.ImageUri;







                Console.WriteLine(Episode.FileDetails.FileType);







                myConnectionTarget.InsertValuesEpisodes(myEpisode);







            }



















        }



    }



}