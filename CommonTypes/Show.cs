using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class Show
    {
        public virtual string PublisherName { get; set; }
        public virtual string PodcastTitle { get; set; }
        public virtual List<string> Category { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string Language { get; set; }
        public virtual string Description { get; set; }

        public virtual DateTime LastUpdated { get; set; }
        public virtual DateTime LastBuildDate { get; set; }
        public virtual string ImageUri { get; set; }
        public Show()
        {
        }
    }
}
