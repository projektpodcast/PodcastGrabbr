using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Der Parent einer Episode ist eine Show.
    /// Zu einer Show können eine Vielzahl an Episoden gehören.
    /// </summary>
    public class Episode
    {
        public string EpisodeId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Summary { get; set; }
        public string Keywords { get; set; }
        public string ImageUri { get; set; }
        public FileInformation FileDetails { get; set; }
        public string DownloadPath { get; set; }
        public bool IsDownloaded { get; set; }

    }
}
