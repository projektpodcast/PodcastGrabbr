using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccessLayer
{
    public class FileDataSource
    {
        public List<Show> GetAllSeries()
        {
            FileDataGeneral fileMethods = new FileDataGeneral();
            DirectoryInfo folderPath = fileMethods.GetFilePath();
            FileInfo[] allFiles = GetFileNames(folderPath);

            List<Show> allSeries = Deserialize(folderPath, allFiles);

            return allSeries;
        }

        public FileInfo[] GetFileNames(DirectoryInfo folderPath)
        {
            FileInfo[] allFiles = folderPath.GetFiles();
            return allFiles;
        }

        public List<Show> Deserialize(DirectoryInfo folderPath, FileInfo[] allFiles)
        {
            //List<Podcast> podcastList = new List<Podcast>();


            List<Show> seriesList = new List<Show>();


            foreach (FileInfo item in allFiles)
            {
                string fileName = item.FullName;
                XmlSerializer serializer = new XmlSerializer(typeof(Podcast));
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                Podcast podcast = (Podcast)serializer.Deserialize(fileStream);

                seriesList.Add(podcast.ShowInfo);
            }
            return seriesList;
        }
    }
}
