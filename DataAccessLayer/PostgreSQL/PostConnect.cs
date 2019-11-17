using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DataAccessLayer.PostgreSQL
{
    

    class PostConnect
    {
        public static NpgsqlConnection con = new NpgsqlConnection();
        public NpgsqlConnection DBConnect(string db)
        {
            con.ConnectionString = @"User ID = postgres; password=soloyo;host=localhost;database=" + db + ";port=5432;commandtimeout=900";

            con.Open();

            return con;

        }
        public NpgsqlConnection DBDesConnect()
        {
            con.Close();
            return con;
        }

    }
}
