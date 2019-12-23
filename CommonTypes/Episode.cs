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
    /// Der Parent einer Episode ist eine Show.
    /// Zu einer Show können eine Vielzahl an Episoden gehören.
    /// </summary>
    public class Episode
    {
        public virtual string EpisodeId { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string ImageUri { get; set; }
        public virtual FileInformation FileDetails { get; set; }
        public virtual string DownloadPath { get; set; }
        public virtual bool IsDownloaded { get; set; }

    }
}
