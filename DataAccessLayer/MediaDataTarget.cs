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
            string folderName = GetMediaFolderName(podcastToSave.ShowInfo.PodcastTitle);
            FileDataGeneral fileMethods = new FileDataGeneral();


            //string abc = GetFileType(podcastToSave);
            //string test = folderName + podcastToSave.ShowInfo.PodcastTitle + "\\";





            DirectoryInfo dirInfo = fileMethods.GetFilePath(folderName);
            string fileName = CreateFileName(podcastToSave);



            DownloadPodcast(podcastToSave, dirInfo, fileName);
        }

        public string GetMediaFolderName(string podcastTitle)
        {
            string mediaFolderName = "MediaFiles";
            string subFolder = $"{ mediaFolderName }\\{ podcastTitle }\\";
            return subFolder;
        }

        //public string GetFileType(Podcast podcast)
        //{
        //    string fileType = podcast.EpisodeList[0].FileDetails.SourceUri.Split('.').Last();
        //    return fileType;
        //}

        public void DownloadPodcast(Podcast podcast, DirectoryInfo folderPath, string fileName)
        {
            //string folderTest = folderPath.FullName+ podcast.EpisodeList[0].FileDetails.SourceUri.Split('/').Last();

            string folderName = $"{ folderPath.FullName }{ fileName }";

            Uri uri = new Uri(podcast.EpisodeList[0].FileDetails.SourceUri);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(uri, folderName);
        }

        public string CreateFileName(Podcast podcast)
        {
            string episodeName = podcast.EpisodeList[0].Title;
            string fileExtension = podcast.EpisodeList[0].FileDetails.SourceUri.Split('.').Last();
            string fileName = $"{ episodeName }.{ fileExtension }";
            return fileName;
        }

        //public string CreateFileName(Podcast podcast)
        //{
        //    string author = podcast.ShowInfo.PodcastTitle.Split('/').Last();
        //    string episode = podcast.EpisodeList[0].Title.Split('/').Last();
        //    //episode.Replace('?', 'b');
        //    string fileName = $"{ author }_{ episode.Replace('?', 'b') }";
        //    return fileName;
        //}

    }
}
