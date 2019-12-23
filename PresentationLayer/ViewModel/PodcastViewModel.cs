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
using PresentationLayer.Models;

namespace PresentationLayer.ViewModel
{
    /// AUTHOR DER KLASSE: PG
    /// 
    public class PodcastViewModel : BaseViewModel
    {
        #region Services
        /// <summary>
        /// Instanz, die Zugriff auf Get und Save-Methoden des BusinessLayers ermöglicht.
        /// </summary>
        private IBusinessAccessService _businessAccess { get; set; }
        #endregion

        #region Suchmenü Properties
        /// <summary>
        /// Wechselt die Sichtbarkeit des Suchmenüs
        /// </summary>
        private Visibility _visibilitySearchBar = Visibility.Collapsed;
        public Visibility VisibilitySearchBar
        {
            get { return _visibilitySearchBar; }
            set { _visibilitySearchBar = value; OnPropertyChanged("VisibilitySearchBar"); }
        }

        /// <summary>
        /// Wahlmöglichkeiten/ItemSource der ComboBox im Suchmenü
        /// </summary>
        private List<string> _filterOptions { get; set; }
        public List<string> FilterOptions
        {
            get { return _filterOptions; }
            set { _filterOptions = value; OnPropertyChanged("FilterOptions"); }
        }

        /// <summary>
        /// Gebunden an das ausgewählte Item der ComboBox im Suchmenü
        /// </summary>
        private string _typeFilter { get; set; }
        public string TypeFilter
        {
            get { return _typeFilter; }
            set { _typeFilter = value; OnPropertyChanged("TypeFilter"); }
        }

        /// <summary>
        /// Gebunden an die TextBox im Suchmenü. Synchronisiert mit der Nutzereingabe.
        /// </summary>
        private string _textFilter { get; set; }
        public string TextFilter
        {
            get { return _textFilter; }
            set { _textFilter = value; OnPropertyChanged("TextFilter"); }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Property ist eine in der View ausgewählte Show.
        /// Wenn eine Show ausgewählt wird, werden die darunter gruppierten Episoden geladen (EpisodesCollection) und dargestellt.
        /// </summary>
        private Show _selectedShow { get; set; }
        public Show SelectedShow
        {
            get { return _selectedShow; }
            set { _selectedShow = value; OnPropertyChanged("SelectedShow"); GetEpisodes(); }
        }

        /// <summary>
        /// Implementiert ICollectionChanged um änderung der View mitzuteilen.
        /// Ist eine Sammlung aller Episoden, die zu einer ausgewählten Episode gehören.
        /// </summary>
        public ObservableCollection<EpisodeModel> EpisodesCollection { get; set; }

        /// <summary>
        /// Implementiert ICollectionChanged um änderung der View mitzuteilen.
        /// Ist eine Sammlung aller in der Datenquelle verfügbarer Podcasts.
        /// </summary>
        public ObservableCollection<Show> AllShows { get; set; }

        /// <summary>
        /// Wenn IsBusy = true ist, wird ein asynchroner Task ausgeführt.
        /// </summary>
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        /// <summary>
        /// Diese Property ist an die "IsEnabled"-Eigenschaft des Episoden-DownloadButtons in der Ui gebunden.
        /// Zeigt der Ui ob es momentan einen aktiven Download-Prozess gibt.
        /// Wenn true = DownloadButtons sind enabled, wenn false = DownloadButtons sind disabled
        /// </summary>
        private bool _isNotDownloading;
        public bool IsNotDownloading
        {
            get { return _isNotDownloading; }
            set { _isNotDownloading = value; OnPropertyChanged("IsNotDownloading"); }
        }

        /// <summary>
        /// Diese Properts ist an die "Value"-Eigenschaft der Episoden-ProgressBar in der Ui gebunden.
        /// Der Wert (0-100) zeigt der Ui den Fortschritt des Downloads an.
        /// </summary>
        private int _episodeDownloadProgress;
        public int EpisodeDownloadProgress
        {
            get { return _episodeDownloadProgress; }
            set { _episodeDownloadProgress = value; OnPropertyChanged("EpisodeDownloadProgress"); }
        }
        #endregion

        #region Ctor
        /// <summary>
        /// Initialisiert non-nullable Properties, setzt die Filteroptionen in der Such-ComboBox
        /// und fragt den BusinessLayer (->DataAccessLayer) ab um die ObservableCollection<Show> mit Shows des Datenziels zu füllen.
        /// </summary>
        /// <param name="businessAccess">Service, der Zugriff auf Get und Save-Methoden des BusinessLayers ermöglicht.</param>
        public PodcastViewModel(IBusinessAccessService businessAccess)
        {
            _businessAccess = businessAccess;
            IsNotDownloading = true;
            AllShows = new ObservableCollection<Show>();
            EpisodesCollection = new ObservableCollection<EpisodeModel>();
            FilterOptions = new List<string>
            {
                "Show",
                "Episode"
            };

            GetShows();
        }
        #endregion

        #region ICommand Properties und Plausenprüfuing
        /// <summary>
        /// ICommand: löscht den ausgewählten Podcast aus dem Datenziel
        /// </summary>
        private ICommand _deleteSelectedPodcast;
        public ICommand DeleteSelectedPodcast
        {
            get
            {
                if (_deleteSelectedPodcast == null)
                {
                    _deleteSelectedPodcast = new RelayCommand(
                        p => this.IsShowSelected(),
                        p => this.ExecuteDeleteSelectedShow());
                }
                return _deleteSelectedPodcast;
            }
        }

        /// <summary>
        /// Überprüft ob eine Show vom Nutzer ausgewählt wurde.
        /// </summary>
        /// <returns></returns>
        private bool IsShowSelected()
        {
            return SelectedShow != null ? true : false;
        }

        /// <summary>
        /// ICommand: Aktiviert den im Suchmenü gesetzten Suchfilter
        /// </summary>
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

        /// <summary>
        /// Überprüft, ob die ComboBox und das Textfeld im Suchmenü gefüllt sind.
        /// Wenn nicht, lässt sich der ICommand nicht ausführen.
        /// </summary>
        /// <returns></returns>
        private bool AreFiltersSet()
        {
            if (TypeFilter != null && !string.IsNullOrWhiteSpace(TextFilter))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ICommand: Zeigt / Blendet das Suchmenü auf Knopfdruck aus.
        /// </summary>
        private ICommand _toggleSearchBarVisibility;
        public ICommand ToggleSearchBarVisibility
        {
            get
            {
                if (_toggleSearchBarVisibility == null)
                {
                    _toggleSearchBarVisibility = new RelayCommand(
                        p => true,
                        p => this.DecideVisibilityProperty());
                }
                return _toggleSearchBarVisibility;
            }
        }

        /// <summary>
        /// ICommand: Aktualisiert die ausgewählte Show im Datenziel und lädt die Daten erneut aus der Datenquelle.
        /// </summary>
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

        /// <summary>
        /// ICommand: Play Button - spielt die heruntergeladene Mediendatei der ausgewählten Episode ab.
        /// </summary>
        private ICommand _playMedia;
        public ICommand PlayMedia
        {
            get
            {
                if (_playMedia == null)
                {
                    _playMedia = new RelayCommand(
                        param => true,
                        param => this.ExecutePlayMedia(param));
                }
                return _playMedia;
            }
        }

        /// <summary>
        /// ICommand: Befehl um die ausgewählte Episode lokal herunterzuladen.
        /// </summary>
        private ICommand _downloadMedia;
        public ICommand DownloadMedia
        {
            get
            {
                if (_downloadMedia == null)
                {
                    _downloadMedia = new AsyncRelayCommand<EpisodeModel>(ExecuteMediaDownloadAsync, CanExecuteSubmit);
                }
                return _downloadMedia;
            }
        }

        private bool CanExecuteSubmit(Episode episode)
        {
            return !IsBusy;
        }
        #endregion ICommand Properties

        #region Methoden
        /// <summary>
        /// Spricht den BusinessLayer an um eine Liste aller Shows aus der Datenquelle zu erhalten.
        /// </summary>
        public void GetShows()
        {
            try
            {
                List<Show> showList = _businessAccess.Get.GetShowList();
                AllShows = new ObservableCollection<Show>(showList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}");
                //throw;
            }

        }

        /// <summary>
        /// ICommand-Methode... 
        /// </summary>
        private void ExecuteDeleteSelectedShow()
        {
            throw new NotImplementedException();
            //BusinessLayer-Zugriff um Show(SelectedShow) + alle Episoden zu löschen.
        }

        /// <summary>
        /// ICommand Methode... Filtert in der UI angezeigten Shows oder Episoden in Abhängigkeit des gesetzten Suchfilters
        /// </summary>
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

        /// <summary>
        /// ICommand Methode... Toggled/wechselt die Sichtbarkeit des Suchmenüs auf Collapsed/Visible
        /// </summary>
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

        /// <summary>
        /// ICommand Methode... Aktualisiert die ausgewählte Show im Datenziel und lädt die Daten erneut aus der Datenquelle.
        /// </summary>
        private void ExecuteRefreshSelectedShow()
        {
            throw new NotImplementedException();
            //BusinessLayer-Zugriff um XML der Show(SelectedShow) neu zu laden, zu deserialisieren und neue Episoden in die DB zu speichern.
        }

        /// <summary>
        /// Steuert den BusinessLayer an um heruntergeladene Mediendatei der ausgewählten Episode (mit dem hinterlegten Standardprogramm) abzuspielen.
        /// </summary>
        /// <param name="episode"></param>
        private void ExecutePlayMedia(object episode)
        {
            _businessAccess.Get.PlayMediaFile((Episode)episode);
        }




        /// <summary>
        /// Asynchrone Methode um den Mediendownload auszuführen.
        /// Wenn eine Datei heruntergeladen wird, wird IsBusy = true gesetzt um anzuzeigen, dass bereits ein asynchroner Task ausgeführt wird.
        /// Nachdem der Download erfolgt ist wird die Episodenliste der Show erneut geladen und IsBusy = false gesetzt
        /// um anzuzeigen, dass der asynchrone Task abgeschlossen ist.
        /// </summary>
        /// <param name="episode">ausgewählte Episode, die heruntergeladen werden soll</param>
        /// <returns></returns>
        private async Task ExecuteMediaDownloadAsync(EpisodeModel episode)
        {
            IsNotDownloading = false;
            IsBusy = true;
            try
            {
                //Variable, die den DownloadProgress kennt, an die Property EpisodeDownloadProgress synchronisieren.
                IProgress<int> downloadProgress = new Progress<int>((result) =>
                {
                    EpisodeDownloadProgress = result;
                });
                //Downloadstart der Ui mitteilen
                episode.IsDownloading = true;
                //Download über BL starten
                await Task.Run(() => _businessAccess.Save.SaveEpisodeAsLocalMedia(SelectedShow, episode, downloadProgress));
                //Ui über fertiggestellten Download der Episode informieren
                episode.IsDownloaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Herunterladen" +"\n" + ex.ToString(), "Fehler beim Download der Episode " + episode.Title);
                throw;
            }
            finally
            {
                IsNotDownloading = true;
                IsBusy = false;
                episode.IsDownloading = false;
                GetEpisodes();
            }


            //NACHTRAGEN:::::::::::::::::
            //INotifyPropertyChanged implementieren für Episode IsDownloaded??
            //Progressbar mit IProgress<T>?

        }
        #endregion

        #region Mockdata
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

        private void GetEpisodes()
        {
            List<Episode> episodes = _businessAccess.Get.GetEpisodes(SelectedShow);
            EpisodesCollection.Clear();

            foreach (Episode item in episodes)
            {
                EpisodeModel convertedEpisode = new EpisodeModel(item);
                EpisodesCollection.Add(convertedEpisode);
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