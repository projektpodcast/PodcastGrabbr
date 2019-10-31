using PodcastGrabbr.Model;
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

namespace PodcastGrabbr.ViewModel
{
    public class PodcastViewModel : BaseViewModel
    {

        private ShowModel _selectedShow { get; set; }
        public ShowModel SelectedShow {
            get { return _selectedShow; }
            set { _selectedShow = value; OnPropertyChanged("SelectedShow"); AddNewEpisode(); }
        }

        public ObservableCollection<EpisodeModel> EpisodesCollection { get; set; }

        public ObservableCollection<ShowModel> AllShows { get; set; }

        public PodcastViewModel()
        {

            AllShows = new ObservableCollection<ShowModel>();
            EpisodesCollection = new ObservableCollection<EpisodeModel>();

            SetList();
            FillEpisodeListWithMockData();
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
        #endregion ICommand Properties

        private bool IsShowSelected()
        {
            if (SelectedShow != null)
            {
                return true;
            }
            return false;
        }

        private void ExecuteDeleteSelectedShow()
        {
            //BusinessLayer-Zugriff um Show(SelectedShow) + alle Episoden zu löschen
        }

        private void ExecuteRefreshSelectedShow()
        {
            SaveObjects bl = new SaveObjects();
            bl.Test();

            //BusinessLayer-Zugriff um XML der Show(SelectedShow) neu zu laden, zu deserialisieren und neue Episoden in die DB zu speichern.
        }

        #region Mockdata
        public void SetList()
        {
            List<ShowModel> test = new List<ShowModel>();
            AllShows = new ObservableCollection<ShowModel>();

            GetObjects bl = new GetObjects();

            List<Show> showList = bl.GetShowList();

            foreach (var item in showList)
            {
                test.Add(new ShowModel(item));
            }

            AllShows = new ObservableCollection<ShowModel>(test);
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
        private void AddNewEpisode()
        {
            DateTime now = DateTime.Now;
            this.EpisodesCollection.Add(new EpisodeModel()
            {
                Title = "Neue Show Selected",
                PublishDate = now,
                ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg",
                Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364",
                Summary = SelectedShow.Description
            });
        }

        private void FillEpisodeListWithMockData()
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < 4; i++)
            {
                EpisodesCollection.Add(new EpisodeModel() { Title = "#1364 - Brian RedbanRedbanRedb anRedbanRedbanRedbanRedba nRedbanRedbanRed banRedbanRedban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg" });
                EpisodesCollection.Add(new EpisodeModel() { Title = "#1364 - Brian Redban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg" });
            }
        }
        #endregion Mockdata
    }
}

#region Uncommented Visibility ICommand
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
#endregion Uncommented Visibility ICommand