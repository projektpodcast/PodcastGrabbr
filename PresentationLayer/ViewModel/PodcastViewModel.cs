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

            SetList();
            FillEpisodeListWithMockData();

            MergeMockData();
        }

        public ObservableCollection<Podcast> Podcasts { get; set; }

        private void MergeMockData()
        {
            Podcasts = new ObservableCollection<Podcast>();
            string bild = "https://img.br.de/6f808e9b-beb7-464c-a912-f8cc752ffe8d.png?w=1800";
            Podcast p1 = new Podcast();
            p1.EpisodeList = new List<Episode>();
            p1.EpisodeList = EpisodesCollection.ToList();
            p1.ShowInfo = new Show();

            p1.ShowInfo.PodcastTitle = "Titel des PodcastsTitel des PodcastsTitel des Podcasts";
            p1.ShowInfo.PublisherName = "Ich bin der Publisher";
            p1.ShowInfo.Description = "The podcast of Comedian Joe Rogan.";
            p1.ShowInfo.ImageUri = bild;
            p1.ShowInfo.LastUpdated = DateTime.UtcNow;

            Podcasts.Add(p1);
            Podcasts.Add(p1);
            Podcasts.Add(p1);
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
                Summary = SelectedShow.Description,
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