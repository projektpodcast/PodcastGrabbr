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
    public class XmlDataTarget : IDataTarget
    {
        public void SavePodcast(Podcast podcastToSave)
        {
            string fileName = CreateFileName(podcastToSave.ShowInfo);
            DirectoryInfo filePath = CreateFilePath();
            string fullFilePath = filePath.FullName + fileName;
            CheckIfFileExists(fullFilePath);

            SerializePodcast(podcastToSave, fullFilePath);
        }

        private string CreateFileName(Show series)
        {
            string fileExtension = ".xml";
            string fileName = series.PodcastTitle.Split('/').Last() + fileExtension;
            return fileName;
        }

        private DirectoryInfo CreateFilePath()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string xmlFolderName = "Xml\\";
            string folderPathToCreate = filePath + xmlFolderName;
            DirectoryInfo fileDirectory = Directory.CreateDirectory(folderPathToCreate);
            return fileDirectory;
        }

        private bool CheckIfFileExists(string fullFilePath)
        {
            bool fileExists = false;
            if (File.Exists(fullFilePath))
            {
                fileExists = true;
            }
            return fileExists;
        }

        private void SerializePodcast(Podcast podcast, string filePath)
        {
            //Podcast transformedPodcast = new Podcast();

            XmlSerializer serializer = new XmlSerializer(typeof(Podcast));

            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            serializer.Serialize(fileStream, podcast);
            fileStream.Close();
        }
    }
}
