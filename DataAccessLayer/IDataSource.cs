using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Definiert Methoden, welche benötigt werden um ein datenbankähnliche Lesebefehle an einem Datenziel durchzuführen.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Greift auf ein Datenziel zu um alle darin persistierten Shows zu erhalten
        /// </summary>
        /// <returns>Liste aller Shows</returns>
        List<Show> GetAllShows();

        /// <summary>
        /// Sucht alle zu einer übergebenen Show zugehörigen Episoden.
        /// </summary>
        /// <param name="selectedShow">Show, zu welcher die zugehörigen Episoden gesucht werden sollen.</param>
        /// <returns>Lisste aller Episoden einer spezifischen Show</returns>
        List<Episode> GetAllEpisodes(Show selectedShow);

        /// <summary>
        /// Durchsucht das Datenziel nach Shows, die eine Episode enthalten, welche gedownloadet ist.
        /// </summary>
        /// <returns>Gruppiert alle gedownloadeten Episoden zu den zugehören Shows und schreibt sie in eine Liste</returns>
        List<Podcast> GetAllDownloadedPodcasts();
    }
}
