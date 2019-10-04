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
    public class XmlDataTarget : LocalDataTarget, IDataTarget 
    {
        public void SavePodcast(Podcast podcastToSave)
        {
            string fileName = GetFileName(podcastToSave);
            string folderName = GetFolderName();

            DirectoryInfo dirInfo = base.GetDirectoryInfo(folderName);

            SerializePodcast(podcastToSave, dirInfo, fileName);
        }

        internal override string GetFileName(Podcast podcast)
        {
            string fileExtension = ".xml";
            string fileName = podcast.ShowInfo.PodcastTitle.Split('/').Last() + fileExtension;
            return fileName;
        }

        internal override string GetFolderName()
        {
            string xmlFolderName = "Xml\\";
            return xmlFolderName;
        }

        private void SerializePodcast(Podcast podcast, DirectoryInfo folderPath, string fileName)
        {
            string folderName = $"{ folderPath.FullName }{ fileName }";
            XmlSerializer serializer = new XmlSerializer(typeof(Podcast));

            FileStream fileStream = new FileStream(folderName, FileMode.Create, FileAccess.ReadWrite);
            serializer.Serialize(fileStream, podcast);
            fileStream.Close();
        }


    }
}
