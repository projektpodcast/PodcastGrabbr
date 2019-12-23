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
    /// <summary>
    /// Diese Klasse verwaltet das Herunterladen von Episoden aus dem Internet auf die lokale Festplatte.
    /// Sie erstellt relative Pfade und gibt diese zurück, um die Dateien herunterzuladen.
    /// Zusätzlich wird der Download an dieser Stelle initialisiert.
    /// </summary>
    public class MediaStorage
    {
        /// <summary>
        /// Zielpfad des vorzunehmenden Downloads
        /// </summary>
        private readonly string _downloadPath;
        private Uri DownloadUri { get; set; }
        public IProgress<int> Progress { get; set; }

        /// <summary>
        /// Bei Klasseninitialisierung wird der relative Pfad "..\AppData\Local" festgestellt.
        /// Zusäzlich wird ein Directory erstellt, welches das Ziel der Downloads festlegt.
        /// </summary>
        public MediaStorage()
        {
            string localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DirectoryInfo localDir = Directory.CreateDirectory($"{localAppPath}\\podcastgrabbr_podcasts\\media");
            _downloadPath = localDir.FullName;
        }

        /// <summary>
        /// Hilfsmethode, lässt den Dateinamen und den Pfad erstellen und initialisiert anschließend den Dateidownload.
        /// </summary>
        /// <param name="show">Show, zu welcher die Episode gehört. Benötigt um einen Sammelordner für alle Episoden dieser Show festzulegen</param>
        /// <param name="episode">Episode, welche heruntergeladen werden soll. Enthält den Downloadlink</param>
        /// <returns>Der erstellte Downloadpfad wird zurückgegeben um diesen später in das Datenziel zu schreiben</returns>
        public async Task<string> InitializeMediaDownload(Show show, Episode episode, IProgress<int> progress)
        {
            Progress = progress;
            DownloadUri = new Uri(episode.FileDetails.SourceUri);
            string fileName = CreateFileName(episode);

            try
            {
                DirectoryInfo fullDir = CreateFullDirectory(show);
                string downloadPath = $"{fullDir.FullName}{fileName}";
                await ExecuteEpisodeDownload(downloadPath);
                return downloadPath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Erstellt und bereinigt den Dateinamen sowie die Dateiendung anhand von Informationen der übergebenen Episode
        /// </summary>
        /// <param name="episode">Episode, für welche der Dateiname erstellt werden soll</param>
        /// <returns></returns>
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

        /// <summary>
        /// Erstellt einen Ordnerpfad, der anhand des übergebenen Shownames festgestellt wird.
        /// </summary>
        /// <param name="show">Show, welche der Parent der zu downloadenden Episode ist.</param>
        /// <returns></returns>
        private DirectoryInfo CreateFullDirectory(Show show)
        {
            string sanitizedFolderName = string.Join("_", show.PodcastTitle.Split(Path.GetInvalidFileNameChars()));
            return Directory.CreateDirectory($"{_downloadPath}\\{sanitizedFolderName}\\");
        }

        /// <summary>
        /// Asnychrone Methode um eine Datei mithilfe Ihrer Uri herunterzuladen.
        /// Muss asynchron sein, da die Dauer des downloads unbekannt ist 
        /// und somit die Benutzeroberfläche über die Dauer des Downloads sperren würde.
        /// </summary>
        /// <param name="downloadPath">DownloadPfad einer Episode</param>
        /// <returns>DownloadPfad, um in nach erfolgtem Download in einem Datenziel zu persistieren.</returns>
        private async Task ExecuteEpisodeDownload(string downloadPath)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                await webClient.DownloadFileTaskAsync(DownloadUri, downloadPath);
                webClient.DownloadProgressChanged -= WebClient_DownloadProgressChanged;
            }
            catch (Exception)
            {   
            }
        }

        /// <summary>
        /// Syncronisiert den WebClient-Downloadprogress mit der Property Progress.
        /// Da die Property Progress und eine Progress-Property im ViewModel auf dieselbe Referenz haben, synchronisieren sich diese Werte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress.Report(e.ProgressPercentage);
        }
    }
}
