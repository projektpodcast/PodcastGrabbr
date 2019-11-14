using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using CommonTypes;
using System.Windows.Input;
using System.Windows;
using System.Configuration;
using System.Windows.Media;

namespace PresentationLayer.ViewModel
{
    public class PodcastViewModel : BaseViewModel
    {
        private Visibility _visibilitySearchBar = Visibility.Collapsed;
        public Visibility VisibilitySearchBar
        {
            get { return _visibilitySearchBar; }
            set { _visibilitySearchBar = value; OnPropertyChanged("VisibilitySearchBar"); }
        }

        private List<string> _filterOptions { get; set; }
        public List<string> FilterOptions
        {
            get { return _filterOptions; }
            set { _filterOptions = value; OnPropertyChanged("FilterOptions"); }
        }

        private string _typeFilter { get; set; }
        public string TypeFilter
        {
            get { return _typeFilter; }
            set { _typeFilter = value; OnPropertyChanged("TypeFilter"); }
        }

        private string _textFilter { get; set; }
        public string TextFilter
        {
            get { return _textFilter; }
            set { _textFilter = value; OnPropertyChanged("TextFilter"); }
        }





        private IBusinessAccessService _businessAccess { get; set; }
        private Show _selectedShow { get; set; }
        public Show SelectedShow
        {
            get { return _selectedShow; }
            set { _selectedShow = value; OnPropertyChanged("SelectedShow"); AddNewEpisode(); }
        }

        public ObservableCollection<Episode> EpisodesCollection { get; set; }

        public ObservableCollection<Show> AllShows { get; set; }

        public PodcastViewModel(IBusinessAccessService businessAccess)
        {
            _businessAccess = businessAccess;

            AllShows = new ObservableCollection<Show>();
            EpisodesCollection = new ObservableCollection<Episode>();
            FilterOptions = new List<string>();





            SetList();
            FillEpisodeListWithMockData();




            FilterOptions.Add("Show");
            FilterOptions.Add("Episode");
        }


        #region ICommand Properties
        private ICommand _deleteAllPodcasts;
        public ICommand DeleteSelectedPodcast
        {
            get
            {
                if (_deleteAllPodcasts == null)
                {
                    _deleteAllPodcasts = new RelayCommand(
                        p => this.IsShowSelected(),
                        p => this.ExecuteDeleteSelectedShow());
                }
                return _deleteAllPodcasts;
            }
        }

        private ICommand _searchFilter;
        public ICommand SearchFilter
        {
            get
            {
                if (_searchFilter == null)
                {
                    _searchFilter = new RelayCommand(
                        p => this.AreFiltersSet(),
                        p => this.ExecuteSearchFilter());
                }
                return _searchFilter;
            }
        }

        private bool AreFiltersSet()
        {
            if (TypeFilter != null && !string.IsNullOrWhiteSpace(TextFilter))
            {
                return true;
            }
            return false;
        }

        private void ExecuteSearchFilter()
        {
            if (TypeFilter == "Show")
            {
                var showsToKeep = AllShows.Where(x => !x.PodcastTitle.ToLower().Contains(TextFilter.ToLower())).ToList();
                foreach (var item in showsToKeep)
                {
                    AllShows.Remove(item);
                }
            }
            else
            {
                var showsToKeep = EpisodesCollection.Where(x => !x.Title.ToLower().Contains(TextFilter.ToLower())).ToList();
                foreach (var item in showsToKeep)
                {
                    EpisodesCollection.Remove(item);
                }
            }
        }

        private ICommand _toggleSearchBarVisibility;
        public ICommand ToggleSearchBarVisibility
        {
            get
            {
                if (_toggleSearchBarVisibility == null)
                {
                    _toggleSearchBarVisibility = new RelayCommand(
                        p => this.CanToggle(),
                        p => this.DecideVisibilityProperty());
                }
                return _toggleSearchBarVisibility;
            }
        }

        private bool CanToggle()
        {
            return true;
        }

        public void DecideVisibilityProperty()
        {
            switch (VisibilitySearchBar)
            {
                case Visibility.Visible:
                    this.VisibilitySearchBar = Visibility.Collapsed;
                    break;
                case Visibility.Collapsed:
                    this.VisibilitySearchBar = Visibility.Visible;
                    break;
                default:
                    this.VisibilitySearchBar = Visibility.Collapsed;
                    break;
            }
        }



        private ICommand _refreshSelectedPodcast;
        public ICommand RefreshSelectedPodcast
        {
            get
            {
                if (_refreshSelectedPodcast == null)
                {
                    _refreshSelectedPodcast = new RelayCommand(
                        p => this.IsShowSelected(),
                        p => this.ExecuteRefreshSelectedShow());
                }
                return _refreshSelectedPodcast;
            }
        }

        private ICommand _playMedia;
        public ICommand PlayMedia
        {
            get
            {
                if (_playMedia == null)
                {
                    _playMedia = new RelayCommand(
                        p => this.IsShowSelected(),
                        p => this.ExecutePlayMedia());
                }
                return _playMedia;
            }
        }

        private ICommand _downloadMedia;
        public ICommand DownloadMedia
        {
            get
            {
                if (_downloadMedia == null)
                {
                    _downloadMedia = new RelayCommand(
                        p => this.IsShowSelected(),
                        p => this.ExecuteDownloadMedia());
                }
                return _deleteAllPodcasts;
            }
        }

        #endregion ICommand Properties

        private bool IsShowSelected()
        {
            if (SelectedShow != null)
            {
                return true;
            }
            return false;
        }

        private void ExecutePlayMedia()
        {
            throw new NotImplementedException();
            //BusinessLayer-Zugriff um (Property) DownloadPath der Episode aufzulösen und abzuspielen.
        }

        private void ExecuteDownloadMedia()
        {
            throw new NotImplementedException();
            //BusinessLayer-Zugriff um LocalMedia anzusteuern und Episode anhand des DownloadPath runterzuladen.
        }

        private void ExecuteDeleteSelectedShow()
        {
            throw new NotImplementedException();
            //BusinessLayer-Zugriff um Show(SelectedShow) + alle Episoden zu löschen.
        }

        private void ExecuteRefreshSelectedShow()
        {
            throw new NotImplementedException();
            //BusinessLayer-Zugriff um XML der Show(SelectedShow) neu zu laden, zu deserialisieren und neue Episoden in die DB zu speichern.
        }

        #region Mockdata
        public void SetList()
        {
            List<Show> showList = _businessAccess.Get.GetShowList();
            AllShows = new ObservableCollection<Show>(showList);


            //List<Show> test = new List<Show>();
            //AllShows = new ObservableCollection<Show>();

            //GetObjects bl = new GetObjects();

            ////List<Show> showList = bl.GetShowList();

            //foreach (var item in showList)
            //{
            //    test.Add(item);
            //}

            //AllShows = new ObservableCollection<Show>(test);
            Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => { AddMoreMockData(); });
        }

        private void AddMoreMockData()
        {
            for (int i = 0; i < 10; i++)
            {
                string bild = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-1x1@2dXL_-1f32c27c4978132dd0854e53b5ed30e10facc189.jpg";
                App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    AllShows.Add(new Show
                    {
                        PodcastTitle = "Addedfun",
                        PublisherName = "WDR",
                        Description = "enttäuschende sachen",
                        ImageUri = bild
                    });
                });
            }
        }

        private void AddNewEpisode()
        {
            DateTime now = DateTime.Now;
            this.EpisodesCollection.Add(new Episode()
            {
                Title = "Neue Show Selected",
                PublishDate = now,
                ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg",
                Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364",
                Summary = "Pete Dominick is a stand up comic, speaker, news commentator, host, and moderator. Look for his podcast called 'StandUP!with Pete Dominick' available on Apple Podcasts.",
            });
        }

        private void FillEpisodeListWithMockData()
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < 4; i++)
            {
                EpisodesCollection.Add(new Episode() { Title = "#1364 - Brian RedbanRedbanRedb anRedbanRedbanRedbanRedba nRedbanRedbanRed banRedbanRedban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg", IsDownloaded = false });
                EpisodesCollection.Add(new Episode() { Title = "#1364 - Brian Redban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg", IsDownloaded = true });
            }
        }
        #endregion Mockdata
    }
}

#region out-commented Visibility ICommand
//private Visibility _visibility = Visibility.Hidden;
//public Visibility Visible { get { return _visibility; } set { _visibility = value; OnPropertyChanged("Visible"); } }

//private ICommand _toggleVisibility;
//public ICommand ToggleVisibility
//{
//    get
//    {
//        if (_toggleVisibility == null)
//        {
//            _toggleVisibility = new RelayCommand(
//                p => this.IsShowSelected(),
//                p => this.DecideVisibilityProperty());
//        }
//        return _deleteAllPodcasts;
//    }
//}

//public void MakeVisible()
//{
//    if (Visible == Visibility.Hidden)
//    {
//        Visible = Visibility.Visible;
//    }
//}
#endregion out-commented Visibility ICommand