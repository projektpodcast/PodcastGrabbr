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
    /// AUTHOR DER KLASSE: PG
    /// 
    /// GetObjects verwaltet alle Methodenaufrufe um Daten aus einer Datenquelle des DataAccessLayers zu erhalten.
    /// Fungiert als Zwischenstück zwischen PresentationLayer und DataAccessLayer.
    /// </summary>
    public class GetObjects
    {
        /// <summary>
        /// Erhält eine Instanz in den DataAccessLayer von der Klasse Factory.
        /// Das implementierte Interface der Instanz legt die angesprochene Methode offen.
        /// </summary>
        /// <returns>Alle Show-Objekte im Datenziel</returns>
        public List<Show> GetShowList()
        {
            List<Show> allShows = new List<Show>();
            try
            {
                IDataSource dataSource = Factory.Instance.CreateDataSource();
                allShows = dataSource.GetAllShows();
            }
            catch (Exception)
            {
                //return null;
                //throw;
            }
            return allShows;
        }

        /// <summary>
        /// Erhält eine Instanz in den DataAccessLayer von der Klasse Factory.
        /// Das implementierte Interface der Instanz legt die angesprochene Methode offen.
        /// </summary>
        /// <param name="selectedShow">Für dieses Show-Objekt sollen die verwandten Episoden gesucht werden</param>
        /// <returns>Episoden, die zu einer spezifischen Show gehören</returns>
        public List<Episode> GetEpisodes(Show selectedShow)
        {
            IDataSource dataSource = Factory.Instance.CreateDataSource();
            return dataSource.GetAllEpisodes(selectedShow);
        }

        /// <summary>
        /// Erhält eine Instanz in den DataAccessLayer von der Klasse Factory.
        /// Das implementierte Interface der Instanz legt die angesprochene Methode offen.
        /// </summary>
        /// <param name="episode">Lokal persistierte Episode, die abgespielt werden soll</param>
        public void PlayMediaFile(Episode episode)
        {
            ILocalMediaSource dal = Factory.Instance.CreateLocalMediaSource();
            dal.PlayLocalMedia(episode);
        }

        /// <summary>
        /// Erhält eine Instanz in den DataAccessLayer von der Klasse Factory.
        /// Der DataAccessLayer durchsucht die Datenquelle nach alles Shows, die eine gedownloadete Episode enthalten.
        /// </summary>
        /// <returns>Liste aller Podcasts aus der Datenquelle, die eine gedownloadete Episode enthalten</returns>
        public List<Podcast> GetDownloadedPodcasts()
        {
            IDataSource dal = Factory.Instance.CreateDataSource();
            return dal.GetAllDownloadedPodcasts();
        }
    }
}
