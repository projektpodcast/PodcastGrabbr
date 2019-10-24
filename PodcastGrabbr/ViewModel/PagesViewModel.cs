using PodcastGrabbr.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class PagesViewModel
    {
        public AllShowsView AllShowsPage { get; set; }

        public PagesViewModel()
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                AllShowsPage = new AllShowsView();
            });


        }

    }
}
