using PodcastGrabbr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr
{
    public class OnShowSelected : EventArgs
    {
        public ShowModel ShowSelection { get; set; }
    }
}
