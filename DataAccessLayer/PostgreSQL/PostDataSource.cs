using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using CommonTypes;


namespace DataAccessLayer.PostgreSQL
{
    public class PostDataSource
    {
        PostConnect myConecction = new PostConnect();
        public Boolean CheckShow(string id, string db)
        {
            Boolean check = false;
            try
            {
                string csql_create = "select sid from shows WHERE sid = " + id + "";
                NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect(db));
                Command.ExecuteNonQuery();
                myConecction.DBDesConnect();
                // conect with the created db
                check = true;
                return check;
            }
            catch (Exception)
            {

                return check;
            }
            

        }
        
        public List<Show> getShows(string db)
        {
                string csql_create = "select * from shows";
                NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect(db));
                var reader = Command.ExecuteReader();
                var list = new List<Show>();

                while (reader.Read())
                    list.Add(new Show { PodcastTitle = reader.GetString(1), PublisherName = reader.GetString(3)});
                list.ToArray();
                myConecction.DBDesConnect();
                return list;
        }

        public List<Episode> getAllEpisodes(string db)
        {
        string csql_create = "select * from episodes";
            NpgsqlCommand Command = new NpgsqlCommand(csql_create, myConecction.DBConnect(db));
            var reader = Command.ExecuteReader();
            var list = new List<Episode>();

            while (reader.Read())
                list.Add(new Episode { Title = reader.GetString(2), PublishDate = reader.GetDateTime(5) });
            list.ToArray();
            myConecction.DBDesConnect();
            return list;
        }


    }
}
