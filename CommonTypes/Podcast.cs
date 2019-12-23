using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Die Klasse "Podcast" gruppiert die Daten einer Show UND alle verfügbaren Episoden dieser Show.
    /// Die Properties des Typs "Show" und "List<Episode>" werden über Vererbung eingebunden.
    /// </summary>
    public class Podcast
    {
        public Show ShowInfo { get; set; }
        public List<Episode> EpisodeList { get; set; }
        public Podcast()
        {
        }

        /// <summary>
        /// Parametrisierter Konstruktor gruppiert eine Show mit einer Sammlung an Episoden.
        /// </summary>
        /// <param name="_show">Show, welche der Parent aller Episoden der Episodensammlung ist.</param>
        /// <param name="_episodeList">Episodensammlung, welche zu einer Show gehört</param>
        public Podcast(Show _show, List<Episode> _episodeList)
        {
            this.ShowInfo = _show;
            this.EpisodeList = _episodeList;
        }
    }
}
