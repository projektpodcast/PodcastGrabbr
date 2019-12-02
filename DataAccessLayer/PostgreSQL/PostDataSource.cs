using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using Npgsql;

using CommonTypes;





namespace DataAccessLayer.PostgreSQL

{

    public class PostDataSource : LocalDataSource, IDataSource

    {

        PostConnect myConecction = new PostConnect();

        NpgsqlConnection conexionOpen;







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



        public List<Episode> getAllEpisodes()

        {

            conexionOpen = new NpgsqlConnection();

            conexionOpen = myConecction.DBConnectionOpen();



            string csql_create = "select * from episodes";

            NpgsqlCommand Command = new NpgsqlCommand(csql_create, conexionOpen);

            var reader = Command.ExecuteReader();

            var list = new List<Episode>();



            while (reader.Read())

                list.Add(new Episode { IsDownloaded = false, Summary = reader.GetString(4), ImageUri = reader.GetString(10), Title = reader.GetString(2), PublishDate = reader.GetDateTime(5) });

            list.ToArray();

            myConecction.DBDesConnect();

            return list;

        }



        public List<Show> GetAllShows()

        {

            conexionOpen = new NpgsqlConnection();

            conexionOpen = myConecction.DBConnectionOpen();



            string csql_create = "select * from shows";

            NpgsqlCommand Command = new NpgsqlCommand(csql_create, conexionOpen);

            var reader = Command.ExecuteReader();

            var list = new List<Show>();



            while (reader.Read())

                list.Add(new Show { Description = reader.GetString(2), Keywords = "null", Language = "null", PodcastTitle = reader.GetString(1), PublisherName = reader.GetString(3), ImageUri = "" });



            list.ToArray();

            myConecction.DBDesConnect();

            return list;

        }



        internal override string GetFolderName()

        {

            throw new NotImplementedException();

        }

    }

}