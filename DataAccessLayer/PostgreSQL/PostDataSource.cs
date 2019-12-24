using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using CommonTypes;
using RssFeedProcessor;
using Mk.DBConnector;

namespace DataAccessLayer.PostgreSQL
{
    /// <summary>
    /// AUTHOR: MA
    /// </summary>
    public class PostDataSource : IDataSource
    {
        //initialize prozess target and conenection
        PostConnect myConecction;
        PostDataTarget myTarget;

        DeserializingManager deserializer = new DeserializingManager();

        public static IDataStorageType dbData;

        public PostDataSource(IDataStorageType storageData)
        {
            dbData = storageData;
            myConecction = new PostConnect(storageData);
            //myTarget = new PostDataTarget(dbData);

            //String html = "http://podcast.wdr.de/quarks.xml";


            //Podcast podcast = deserializer.DeserializeRssXml(html);
            //myConecction = new PostConnect(storageData);



            ////check if the podcast exist
            //bool CheckPodcast = CheckInDB(podcast);

            ////if the podcast dont exist, it will be saved in the db
            //if (!CheckPodcast)
            //{
            //    myTarget.SavePodcast(podcast);
            //}
        }

        public PostDataSource(PostConnect connection)
        {
            myConecction = connection;
        }

        internal bool CheckInDB(Podcast thisPodcast)
        {
            bool check = false;



            string csql_create = "select * from shows where podcasttitle = '" + thisPodcast.ShowInfo.PodcastTitle + "'";

            NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect());
            var reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                check = true;
            }
            myConecction.DBDesConnect();
            return check;
        }

        // collect the episodes
        public List<Episode> GetAllEpisodes(Show selectedShow)
        {

            string csql_create = "select * from episodes WHERE showid= @showId";
            int id = int.Parse(selectedShow.ShowId);

            NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect());
            Command.Parameters.AddWithValue("@showId", id);
            var reader = Command.ExecuteReader();

            var list = new List<Episode>();

            while (reader.Read())

           
                // return a list with all episodes 

 
            list.Add(new Episode
            {
                EpisodeId = reader.GetInt32(0).ToString(),
                //showid = reader.GetString(1),
                Title = reader.GetString(2),
                PublishDate = reader.GetDateTime(3),
                Summary = reader.GetString(4),
                Keywords = reader.GetString(5),
                ImageUri = reader.GetString(6),
                //duration = reader.GetString(7),
                //filetyp = reader.GetString(8),
                FileDetails = new FileInformation() { FileType = reader.GetString(8), SourceUri = reader.GetString(9) },
                //fileurl = reader.GetString(9),
                //FileDetails = reader.GetString(10),
                IsDownloaded = reader.GetBoolean(11),
                DownloadPath = reader.GetString(12)
            });

            list.ToArray();

            myConecction.DBDesConnect();

            return list;
        }

        //Collect all shows 
        public List<Show> GetAllShows()
        {
            string csql_create = "select * from shows";
            NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect());

            var reader = Command.ExecuteReader();

            var list = new List<Show>();


            while (reader.Read())
                list.Add(new Show
                {
                    ShowId = reader.GetInt32(0).ToString(),
                    PublisherName = reader.GetString(1),
                    PodcastTitle = reader.GetString(2),
                    //Category = reader.GetString(3),
                    Keywords = reader.GetString(4),
                    Subtitle = reader.GetString(5),
                    Language = reader.GetString(6),
                    Description = reader.GetString(7),
                    LastUpdated = reader.GetDateTime(8),
                    LastBuildDate = reader.GetDateTime(9),
                    ImageUri = reader.GetString(10),
                    RssLink = reader.GetString(11)
                });

            list.ToArray();
            myConecction.DBDesConnect();

            return list;

        }

        public List<Podcast> GetAllDownloadedPodcasts()
        {
            throw new NotImplementedException();
        }
    }



}