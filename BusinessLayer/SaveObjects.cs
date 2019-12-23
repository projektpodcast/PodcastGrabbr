using CommonTypes;
using DataAccessLayer;
using RssFeedProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    /// <summary>
    /// SaveObjects verwaltet alle Methodenaufrufe um Daten in ein Datenziel des DataAccessLayers zu persistieren.
    /// Fungiert als Zwischenstück zwischen PresentationLayer und DataAccessLayer
    /// </summary>
    public class SaveObjects
    {
        /// <summary>
        /// Erhält eine Instanz in den DataAccessLayer von der Klasse Factory.
        /// Das implementierte Interface der Instanz legt die angesprochene Methode offen.
        /// </summary>
        /// <param name="rssUri">Rss Link eines Podcasts. Enthält eine Show mit vielen Episoden in Xml-Form</param>
        public void SavePodcast(string rssUri)
        {
            try
            {
                IDataTarget fileTarget = Factory.Instance.CreateDataTarget();
                fileTarget.SavePodcast(rssUri);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BulkSavePodcast(List<string> rssLinks)
        {
            try
            {
                IDataTarget fileTarget = Factory.Instance.CreateDataTarget();
                fileTarget.BulkCopyPodcasts(rssLinks);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Um das Absperren der UI zu verhindern, ist der Methodenaufruf target.DownloadEpisode asyncron.
        /// Nachdem der asnychrone Task abgeschlossen ist, wird der zurückerhaltene Pfad in einem Datenziel implementiert.
        /// </summary>
        /// <param name="show">Show, welche die Episode beinhalten. Benötigt um die Episode im Datenziel schneller zu finden</param>
        /// <param name="episode">Episode, die heruntergeladen werden soll. Enthält die Download-Uri und den zu speichernden Downloadpfad</param>
        /// <returns></returns>
        public async Task SaveEpisodeAsLocalMedia(Show show, Episode episode, IProgress<int> progress)
        {
            ILocalMediaTarget target = Factory.Instance.CreateLocalMediaTarget();
            try
            {
                InsertDownloadPathInEpisode(show, episode, await target.DownloadEpisode(show, episode, progress));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Reicht die benötigten Parameter an den DataAccessLayer weiter, um den Download-Pfad einer Episode zu aktualisieren.
        /// </summary>
        /// <param name="show">Benötigt um die Episode im Datenziel schneller zu finden</param>
        /// <param name="episode">Benötigt um die richtige Episode anhand des Schlüssels im Datenziel zu aktualisieren</param>
        /// <param name="path">Lokaler Pfad, welcher zur gedownloadeten Episode führt. Soll im EpisodenObjekt hinterlegt werden</param>
        public void InsertDownloadPathInEpisode(Show show, Episode episode, string path)
        {
            IDataTarget target = Factory.Instance.CreateDataTarget();
            target.InsertDownloadPath(show, episode, path);
        }

        /// <summary>
        /// Eine Liste, die Links zu RSS-Feeds erhält, wird an den DataAccessLayer zur Weiterverarbeitung geleitet.
        /// </summary>
        /// <param name="rssLinksToProcess"></param>
        public void ProcessRssList(List<string> rssLinksToProcess)
        {
            IDataTarget target = Factory.Instance.CreateDataTarget();

            foreach (string uri in rssLinksToProcess)
            {
                try
                {
                    target.SavePodcast(uri);
                }
                catch (Exception)
                {
                    //throw;
                }

            }
        }

    }
}
