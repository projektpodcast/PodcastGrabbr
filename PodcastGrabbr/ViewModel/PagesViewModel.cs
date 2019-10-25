using PodcastGrabbr.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class PagesViewModel : INotifyPropertyChanged
    {
        private AllShowsView _allShowsPage { get; set; }
        public AllShowsView AllShowsPage
        {
            get { return _allShowsPage; }
            set { _allShowsPage = value; OnPropertyChanged("AllShowsPage"); }
        }


        private EpisodesView _episodesPage { get; set; }
        public EpisodesView EpisodesPage
        {
            get { return _episodesPage; }
            set { _episodesPage = value; OnPropertyChanged("EpisodesPage"); }
        }

        public PagesViewModel()
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                AllShowsPage = new AllShowsView();
                EpisodesPage = new EpisodesView();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
