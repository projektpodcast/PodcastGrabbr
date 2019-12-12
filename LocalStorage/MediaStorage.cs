using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LocalStorage
{
    public class MediaStorage
    {
        private readonly string _downloadPath;
        private Uri DownloadUri { get; set; }

        public MediaStorage()
        {
            string localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DirectoryInfo localDir = Directory.CreateDirectory($"{localAppPath}\\podcastgrabbr_podcasts\\media");
            _downloadPath = localDir.FullName;
        }

        public void InitializeMediaDownload(Show show, Episode episode)
        {
            DownloadUri = new Uri(episode.FileDetails.SourceUri);
            string fileName = CreateFileName(episode);
            DirectoryInfo fullDir = CreateFullDirectory(show, fileName);
            ExecuteEpisodeDownload(fullDir, fileName);
        }

        private string CreateFileName(Episode episode)
        {
            string episodeTitle = episode.Title;
            string fileExtension = episode.FileDetails.SourceUri.Split('.').Last();

            if (fileExtension.Contains('?'))
            {
                string fileExtension2 = fileExtension.Split('?').First();
                string unsanitizedPath = $"{episodeTitle}.{fileExtension2}";
                string sanitizedPath = string.Join(" ", unsanitizedPath.Split(Path.GetInvalidFileNameChars()));
                return sanitizedPath;
            }
            else
            {
                string unsanitizedPath = $"{episodeTitle}.{fileExtension}";
                string sanitizedPath = string.Join(" ", unsanitizedPath.Split(Path.GetInvalidFileNameChars()));
                return sanitizedPath;
            }

        }

        private DirectoryInfo CreateFullDirectory(Show show, string fileName)
        {
            string sanitizedFolderName = string.Join("_", show.PodcastTitle.Split(Path.GetInvalidFileNameChars()));
            return Directory.CreateDirectory($"{_downloadPath}\\{sanitizedFolderName}\\");
        }

        private void ExecuteEpisodeDownload(DirectoryInfo dir, string fileName)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(DownloadUri, $"{dir.FullName}{fileName}");
            }
            catch (Exception ex)
            {

                ex.ToString();
            }

        }

    }
}
