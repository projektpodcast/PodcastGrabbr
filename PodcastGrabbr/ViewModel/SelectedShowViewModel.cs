using PodcastGrabbr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class SelectedShowViewModel
    {
        public ShowModel SelectedShow { get; set; }

        SelectedShowViewModel()
        {
            SelectedShow = new ShowModel();
        }
    }
}
