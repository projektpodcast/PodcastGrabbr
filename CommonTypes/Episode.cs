using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class Episode
    {

        public virtual string EpisodeId { get; set; }
        public virtual string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Summary { get; set; }
        public string Keywords { get; set; }
        public string ImageUri { get; set; }
        public FileInformation FileDetails { get; set; }
        public bool IsDownloaded { get; set; }

        public string LocalMediaPath { get; set; }

        public Episode()
        {
        }
    }
}
