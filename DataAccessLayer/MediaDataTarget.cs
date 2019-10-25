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
    public class MediaDataTarget : LocalDataTarget, IDataTarget
    {
        public void SavePodcast(Podcast podcastToSave)
        {
            string folderName = GetFolderName();
            string fileName = GetFileName(podcastToSave);
            string podcastFolderName = CreatePodcastSubFolder(podcastToSave.ShowInfo.ShowName, folderName);

            DirectoryInfo dirInfo = base.GetDirectoryInfo(podcastFolderName);

            DownloadPodcast(podcastToSave, dirInfo, fileName);
        }

        internal override string GetFileName(Podcast podcast)
        {
            string episodeName = podcast.EpisodeList[0].Title;
            string fileExtension = podcast.EpisodeList[0].FileDetails.SourceUri.Split('.').Last();
            string fileName = episodeName+"."+fileExtension;
            //string fileName = $"{ episodeName }.{ fileExtension }";
            return string.Join(" ", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        internal override string GetFolderName()
        {
            string mediaFolderName = "MediaFiles\\";
            return mediaFolderName;
        }

        private string CreatePodcastSubFolder(string podcastTitle, string mediaFolderName)
        {
            string sanitizedTitle = string.Join("_", podcastTitle.Split(Path.GetInvalidFileNameChars()));
            string subFolder = mediaFolderName+sanitizedTitle+"\\";
            //string subFolder = $"{ mediaFolderName }{ sanitizedTitle }\\";
            return subFolder;
        }

        private void DownloadPodcast(Podcast podcast, DirectoryInfo folderPath, string fileName)
        {
            string folderName = folderPath.FullName+fileName;
            //string folderName = $"{ folderPath.FullName }{ fileName }";
            Uri uri = new Uri(podcast.EpisodeList[0].FileDetails.SourceUri);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(uri, folderName);
        }

        public void DeletePodcast(Podcast podcastToDelete)
        {

        }
        public void UpdatePodcast(Podcast oldPodcast, Podcast newPodcast)
        {

        }

    }
}
