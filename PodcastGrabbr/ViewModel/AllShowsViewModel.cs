using PodcastGrabbr.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class AllShowsViewModel : BaseViewModel
    {
        private ShowModel _selectedShow { get; set; }
        public ShowModel SelectedShow { get { return _selectedShow; } set { _selectedShow = value; OnPropertyChanged("SelectedShow"); NewShowSelected(); } }
        //{
        //    get { return _selectedShow; }
        //    set { _selectedShow = value; OnPropertyChanged(_selectedShow); }
        //}
        public ObservableCollection<ShowModel> AllShows { get; set; }

        public AllShowsViewModel()
        {
            List<ShowModel> test = new List<ShowModel>();
            AllShows = new ObservableCollection<ShowModel>();
            BusinessLayer.GetObjects bl = new BusinessLayer.GetObjects();
            var showList = bl.GetShowList(0);

            foreach (var item in showList)
            {
                test.Add(new ShowModel(item));
                //AllShows.Add(new ShowModel(item));
            }

            AllShows = new ObservableCollection<ShowModel>(test);
            Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => { AddMoreMockData(); });


            //AddMockData();
        }

        public event EventHandler<OnShowSelected> ShowSelected;

        public void NewShowSelected()
        {
            if (ShowSelected != null)
            {
                ShowSelected(this, new OnShowSelected() { ShowSelection = SelectedShow });
            }
        }

        private void AddMockData()
        {
            string bild3 = "http://static.libsyn.com/p/assets/7/1/f/3/71f3014e14ef2722/JREiTunesImage2.jpg";
            string bild2 = "https://www1.wdr.de/mediathek/video/sendungen/quarks-und-co/logo-quarks100~_v-Podcast.jpg";
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 >= 1)
                {
                    AllShows.Add(new ShowModel
                    {
                        PodcastTitle = "Titel des Podcasts",
                        PublisherName = "Ich bin der Publisher",
                        Description = "The podcast of Comedian Joe Rogan.",
                        ImageUri = bild3,
                        LastUpdated = DateTime.UtcNow
                    });
                }
                else
                {
                    AllShows.Add(new ShowModel
                    {
                        PodcastTitle = "WowTitel",
                        PublisherName = "ARD & ZDF",
                        Description = "Wow Podcast",
                        ImageUri = bild2,
                        LastUpdated = DateTime.UtcNow
                    });
                }
            }
            Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => { AddMoreMockData(); });
        }

        private void AddMoreMockData()
        {
            for (int i = 0; i < 10; i++)
            {
                string bild = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-1x1@2dXL_-1f32c27c4978132dd0854e53b5ed30e10facc189.jpg";
                App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    AllShows.Add(new ShowModel
                    {
                        PodcastTitle = "Addedfun",
                        PublisherName = "WDR",
                        Description = "enttäuschende sachen",
                        ImageUri = bild,
                        LastUpdated = DateTime.UtcNow
                    });
                });        
            }
        }

    }
}
