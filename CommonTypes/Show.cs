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
    /// Das Child einer Show ist eine Episode.
    /// Zu einer Show können eine Vielzahl an Episoden gehören.
    /// </summary>
    public class Show
    {
        public string ShowId { get; set; }
        public string PublisherName { get; set; }
        public string PodcastTitle { get; set; }
        public List<string> Category { get; set; }
        public string Keywords { get; set; }
        public string Subtitle { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }

        public DateTime LastUpdated { get; set; }
        public DateTime LastBuildDate { get; set; }
        public string ImageUri { get; set; }
        public string RssLink { get; set; }

    }
}
