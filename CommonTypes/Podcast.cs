using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Die Klasse "Podcast" gruppiert die Daten einer Show UND alle verfügbaren Episoden dieser Show.
    /// Die Properties des Typs "Show" und "List<Episode>" werden über inerhitance eingebunden.
    /// </summary>
    public class Podcast
    {
        public Show ShowInfo { get; set; }
        public List<Episode> EpisodeList { get; set; }
        public Podcast()
        {
        }
        public Podcast(Show _show, List<Episode> _episodeList)
        {
            this.ShowInfo = _show;
            this.EpisodeList = _episodeList;
        }
    }
}
