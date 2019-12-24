using System;
using CommonTypes;
using Npgsql;
using SecurityLibrary;

namespace DataAccessLayer.PostgreSQL

{
    /// <summary>
    /// AUTHOR: MA
    /// </summary>
    public class PostConnect
    {
        String dbName;
        int myPort;
        String myIp;

        private IDataStorageType ConnectionData { get; set; }
        internal NpgsqlConnection DbConnection { get; set; }
        public static NpgsqlConnection con = new NpgsqlConnection();


        public PostConnect(IDataStorageType storageData)
        {
            ConnectionData = storageData;
            //dbName = PostDataSource.dbData.DataBaseName;
            //myPort = PostDataSource.dbData.Port;
            //myIp = PostDataSource.dbData.Ip;
        }

        public NpgsqlConnection DBConnect()

        {
            con.ConnectionString =
            $"Server={ConnectionData.Ip}; Port={ConnectionData.Port}; DataBase={ConnectionData.DataBaseName}; User Id={ConnectionData.UserName}; Password={StringCipher.Decrypt(ConnectionData.EncryptedPassword)}";
            DbConnection = con;
            DbConnection.Open();
            return DbConnection;
        }

        //public NpgsqlConnection DBConnect()

        //{
        //    con.ConnectionString = @"User ID = postgres; password=" + StringCipher.Decrypt(PostDataSource.dbData.EncryptedPassword) + "; host= " + myIp + ";database=" + dbName + ";port=" + myPort + ";commandtimeout=900";
        //    //con.ConnectionString = @"User ID = postgres; password=" + myPass + "; host= " + myIp +";database=" + dbName + ";port="+ myPort +";commandtimeout=900";
        //    con.Open();
        //    return con;
        //}
        public NpgsqlConnection DBConnectCheck()

        {
            if (DbConnection.State != System.Data.ConnectionState.Closed)
            {
                return DbConnection;
            }
            else
            {
                con.ConnectionString =
                $"Server={ConnectionData.Ip}; Port={ConnectionData.Port}; DataBase={ConnectionData.DataBaseName}; User Id={ConnectionData.UserName}; " +
                $"Password={StringCipher.Decrypt(ConnectionData.EncryptedPassword)}";
                DbConnection = con;
                DbConnection.Open();
                return DbConnection;
            }
        }

        public NpgsqlConnection DBDesConnect()
        {
            con.Close();
            return con;
        }
    }

}