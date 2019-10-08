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
    public class XmlDataSource : LocalDataSource, IDataSource
    {

        public List<Show> GetAllShows()
        {
            string folderName = GetFolderName();
            DirectoryInfo folderPath = base.GetDirectoryInfo(folderName);
            FileInfo[] allFiles = GetFileInfo(folderPath);

            List<Show> allShows = DeserializePodcast(folderPath, allFiles);

            return allShows;
        }

        internal override string GetFolderName()
        {
            string xmlFolderName = "Xml\\";
            return xmlFolderName;
        }

        private FileInfo[] GetFileInfo(DirectoryInfo folderPath)
        {
            FileInfo[] allFiles = folderPath.GetFiles();
            return allFiles;
        }

        private List<Show> DeserializePodcast(DirectoryInfo folderPath, FileInfo[] allFiles)
        {
            List<Show> showList = new List<Show>();

            foreach (FileInfo item in allFiles)
            {
                string fileName = item.FullName;
                XmlSerializer serializer = new XmlSerializer(typeof(Podcast));
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                Podcast podcast = (Podcast)serializer.Deserialize(fileStream);

                showList.Add(podcast.ShowInfo);
            }
            return showList;
        }

    }
}
