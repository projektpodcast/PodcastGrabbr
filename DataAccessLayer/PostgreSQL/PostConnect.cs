﻿using System;
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


        public static NpgsqlConnection con = new NpgsqlConnection();


        public PostConnect(IDataStorageType storageData)
        {
            //dbName = PostDataSource.dbData.DataBaseName;
            //myPort = PostDataSource.dbData.Port;
            //myIp = PostDataSource.dbData.Ip;
            dbName = storageData.DataBaseName;
            myPort = storageData.Port;
            myIp = storageData.Ip;
        }


        public NpgsqlConnection DBConnect()

        {

            con.ConnectionString = @"User ID = postgres; password=" + StringCipher.Decrypt(PostDataSource.dbData.EncryptedPassword) + "; host= " + myIp + ";database=" + dbName + ";port=" + myPort + ";commandtimeout=900";
            //con.ConnectionString = @"User ID = postgres; password=" + myPass + "; host= " + myIp +";database=" + dbName + ";port="+ myPort +";commandtimeout=900";
            con.Open();
            return con;
        }
        public NpgsqlConnection DBConnectCheck()

        {

            con.ConnectionString = @"User ID = postgres; password=" + StringCipher.Decrypt(PostDataSource.dbData.EncryptedPassword) + "; host= " + myIp + ";database=;port=" + myPort + ";commandtimeout=900";
            //con.ConnectionString = @"User ID = postgres; password=" + myPass + "; host= " + myIp +";database=" + dbName + ";port="+ myPort +";commandtimeout=900";
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