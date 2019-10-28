using BusinessLayer;
using CommonTypes;
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
    class Program
    {
        static void Main(string[] args)
        {
            DeserializingManager deserializer = new DeserializingManager();

            ////Podcast a = deserializer.DeserializeRssXml("http://joeroganexp.joerogan.libsynpro.com/rss");
            ////Podcast a = deserializer.DeserializeRssXml("http://podcast.wdr.de/quarks.xml");
            ////Podcast a = deserializer.DeserializeRssXml("http://web.ard.de/radiotatort/rss/podcast.xml");
            Podcast a = deserializer.DeserializeRssXml("https://www1.wdr.de/radio/podcasts/wdr2/kabarett132.podcast");

            ////Podcast a = deserializer.DeserializeRssXml("http://www1.swr.de/podcast/xml/swr2/forum.xml");



            //SaveObjects blSave = new SaveObjects();
            //blSave.SavePodcastAsXml(a, 0);

            //GetObjects blGet = new GetObjects();
            //List<Show> showList = blGet.GetShowList();

            //blSave.SavePodcastAsMediaFile(a);

            //blGet.GetLocalMedia();


            ////AppConfigWriteToXml();

            //ConnectionStringSetter();
            //Test2();
            //int xmlOrDb = GetXmlAllowance();

        }

        //static void ConnectionStringSetter()
        //{
        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
        //    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings() { ConnectionString = "asdasdasd", ProviderName = "bbb", Name = "connblu" });
        //    config.Save();
        //    ConfigurationManager.RefreshSection("connectionStrings");
        //}

        //static void AppConfigWriteToXml()
        //{


        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    if (config.AppSettings.Settings.AllKeys.Contains("xmlAllowed"))
        //    {

        //    }
        //    else
        //    {
        //        config.AppSettings.Settings.Add("xmlAllowed", "1");
        //        config.Save(ConfigurationSaveMode.Modified);

        //        ConfigurationManager.RefreshSection("appSettings");
        //    }

        //}


        ///// <summary>
        ///// METHODE NUR FÜR TESTZWECKE
        ///// </summary>
        ///// <returns></returns>
        //static int GetXmlAllowance()
        //{
        //    var appSettings = ConfigurationManager.AppSettings;

        //    List<string> b = new List<string>();
        //    foreach (var key in appSettings.AllKeys)
        //    {
        //        if (key == "xmlAllowed")
        //        {
        //            b = appSettings.GetValues(0).ToList();
        //        }
        //    }

        //    int abc = Int32.Parse(b[0]);

        //    return abc;
        //}



        //static void Test2()
        //{
        //    System.Configuration.Configuration config =
        //ConfigurationManager.OpenExeConfiguration(
        //ConfigurationUserLevel.None);

        //    string conStringname = "zzzzdb";
        //    string conString = @"data source=connectionstringstringstringconnection";
        //    string providerName = "oofProvider";

        //    ConnectionStringSettings connStrSettings = new ConnectionStringSettings();
        //    connStrSettings.Name = conStringname;
        //    connStrSettings.ConnectionString = conString;
        //    connStrSettings.ProviderName = providerName;

        //    config.ConnectionStrings.ConnectionStrings.Add(connStrSettings);

        //    config.Save(ConfigurationSaveMode.Full);

        //    ConfigurationManager.RefreshSection("connectionStrings");




        //    Console.WriteLine("Created configuration file: {0}", config.FilePath);
        //}
    }
}
