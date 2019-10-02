using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace DataAccessLayer
{
    public class MediaDataTarget : IDataTarget
    {
        public void SavePodcast(Podcast podcastToSave)
        {
            string folderName = CreateMediaFolderName(podcastToSave.ShowInfo.PodcastTitle);
            string fileName = CreateMediaFileName(podcastToSave);

            FileDataGeneral fileMethods = new FileDataGeneral();
            DirectoryInfo dirInfo = fileMethods.GetFilePath(folderName);

            DownloadPodcast(podcastToSave, dirInfo, fileName);
        }

        public string CreateMediaFolderName(string podcastTitle)
        {
            string mediaFolderName = "MediaFiles";
            string subFolder = $"{ mediaFolderName }\\{ podcastTitle }\\";
            return subFolder;
        }

        public string CreateMediaFileName(Podcast podcast)
        {
            string episodeName = podcast.EpisodeList[0].Title;
            string fileExtension = podcast.EpisodeList[0].FileDetails.SourceUri.Split('.').Last();
            string fileName = $"{ episodeName }.{ fileExtension }";
            return fileName;
        }

        public void DownloadPodcast(Podcast podcast, DirectoryInfo folderPath, string fileName)
        {
            string folderName = $"{ folderPath.FullName }{ fileName }";
            Uri uri = new Uri(podcast.EpisodeList[0].FileDetails.SourceUri);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(uri, folderName);
        }
    }
}
