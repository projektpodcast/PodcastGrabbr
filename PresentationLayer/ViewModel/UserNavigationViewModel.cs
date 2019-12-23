using CommonTypes;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{

    public class UserNavigationViewModel : BaseViewModel
    {
        #region Ui Properties
        private bool _canSwitchToSettings { get; set; }
        private bool _canSwitchToPodcast { get; set; }
        private bool _canSwitchToDownloads { get; set; }

        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visible { get { return _visibility; } set { _visibility = value; OnPropertyChanged("Visible"); } }
        #endregion

        /// <summary>
        /// Gebunden an die Oberfläche. Jeder Eintrag ist ein Navigationspunkt, der in der View angezeigt wird.
        /// </summary>
        public ObservableCollection<ButtonModel> MenueOptions { get; set; }

        /// <summary>
        /// Initialisiert die Menüpunkte, indem die Property MenueOptions gefüllt wird.
        /// </summary>
        public UserNavigationViewModel()
        {
            InitializeButtonCollection();
        }

        /// <summary>
        /// Ein ButtonModel besteht aus einer Zeichenkette (wird in Menü angezeigt) und einem ICommand (wird an den Menüpunkt gebunden)
        /// </summary>
        public void InitializeButtonCollection()
        {
            ButtonModel firstButton = new ButtonModel("Startseite", SwitchPageToHome);
            ButtonModel secondButton = new ButtonModel("Einstellungen", SwitchPageToSettings);
            ButtonModel thirdButton = new ButtonModel("Meine Downloads", SwitchPageToDownloads);
            ButtonModel fourthButton = new ButtonModel("Show importieren", OpenWindowSingleRssImport);
            ButtonModel fifthButton = new ButtonModel("Shows aktualisieren", RefreshEpisodesOfAllShows);
            MenueOptions = new ObservableCollection<ButtonModel>
            {
                firstButton,
                secondButton,
                thirdButton,
                fourthButton,
                fifthButton
            };
        }

        #region ICommands und Plausenprüfung


        private ICommand _toggleMenueVisibility;
        public ICommand ToggleMenueVisibility
        {
            get
            {
                if (_toggleMenueVisibility == null)
                {
                    _toggleMenueVisibility = new RelayCommand(
                        p => this.CanClickButton(),
                        p => this.DecideVisibilityProperty());
                }
                return _toggleMenueVisibility;
            }
        }

        private ICommand _switchPageToSettings;
        public ICommand SwitchPageToSettings
        {
            get
            {
                if (_switchPageToSettings == null)
                {
                    _switchPageToSettings = new RelayCommand(
                        p => this._canSwitchToSettings,
                        p => this.NavigationChanged("ToSettings"));
                }
                return _switchPageToSettings;
            }
        }

        private ICommand _switchPageToHome;
        public ICommand SwitchPageToHome
        {
            get
            {
                if (_switchPageToHome == null)
                {
                    _switchPageToHome = new RelayCommand(
                        p => this._canSwitchToPodcast,
                        p => this.NavigationChanged("ToPodcast"));
                }
                return _switchPageToHome;
            }
        }

        private ICommand _switchPageToDownloads;
        public ICommand SwitchPageToDownloads
        {
            get
            {
                if (_switchPageToDownloads == null)
                {
                    _switchPageToDownloads = new RelayCommand(
                        p => this._canSwitchToDownloads,
                        p => this.NavigationChanged("ToDownloads"));
                }
                return _switchPageToDownloads;
            }
        }

        private ICommand _openWindowSingleRssImport;
        public ICommand OpenWindowSingleRssImport
        {
            get
            {
                if (_openWindowSingleRssImport == null)
                {
                    _openWindowSingleRssImport = new RelayCommand(
                        p => this.CanClickButton(),
                        p => this.NavigationChanged("ToImport"));
                }
                return _openWindowSingleRssImport;
            }
        }

        private ICommand _openShowImport;
        public ICommand OpenShowImport
        {
            get
            {
                if (_openShowImport == null)
                {
                    _openShowImport = new RelayCommand(
                        p => this.CanClickButton(),
                        p => this.DecideVisibilityProperty());
                }
                return _openShowImport;
            }
        }

        private ICommand _refreshEpisodesOfAllShows;
        public ICommand RefreshEpisodesOfAllShows
        {
            get
            {
                if (_refreshEpisodesOfAllShows == null)
                {
                    _refreshEpisodesOfAllShows = new RelayCommand(
                        p => this.CanClickButton(),
                        p => this.RefreshEpisodes());
                }
                return _refreshEpisodesOfAllShows;
            }
        }

        private bool CanClickButton()
        {
            return true;
        }
        #endregion

        /// <summary>
        /// Toggled die Sichtbarkeit zwischen Collapsed und Visible einer Ui-Property umher.
        /// </summary>
        public void DecideVisibilityProperty()
        {
            this.Visible = this.Visible != Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;
            //switch (Visible)
            //{
            //    case Visibility.Visible:
            //        this.Visible = Visibility.Collapsed;
            //        break;
            //    case Visibility.Collapsed:
            //        this.Visible = Visibility.Visible;
            //        break;
            //    default:
            //        this.Visible = Visibility.Collapsed;
            //        break;
            //}
        }

        private void Implement()
        {

        }

        public void RefreshEpisodes()
        {
            throw new NotImplementedException();
            //BL Zugriff: Show-RSS Feed erneut laden, deserialisieren, prüfen ob neue Episoden verfügbar sind, in Db schieben, Db abruf: ShowList
        }

        #region Events
        /// <summary>
        /// Teilt den Subscribern mit, dass ein Menünavigationspunkt ausgewählt wurde.
        /// Empfänger ist das MainViewModel, welchen den angezeigten Inhalt (CurrentContent) an ausgewählten Menünavigationspunkt angleicht.
        /// </summary>
        /// <param name="property"></param>
        public event EventHandler<OnNavigationButtonClicked> OnNavigationChanged;

        public void NavigationChanged(string property)
        {
            if (OnNavigationChanged != null)
            {
                OnNavigationChanged(this, new OnNavigationButtonClicked() { ChangeTo = property });
            }
        }

        /// <summary>
        /// Subscribed einem Event aus dem MainViewModel.
        /// Empfängt eine Änderung, wenn sich der angezeigte "CurrentContent" im MainViewModel ändert.
        /// Dies ist wichtig, da der aktuell ausgewählte Menüpunkt inaktiv gesetzt werden soll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Name des ViewModels, welches in der MainView angezeigt wird.</param>
        public void MainViewModel_OnTestChanged(object sender, OnCurrentContentChanged e)
        {
            if (e.ViewModelName.Contains("PodcastView"))
            {
                this._canSwitchToPodcast = false;
                this._canSwitchToSettings = true;
                this._canSwitchToDownloads = true;
            }
            else if (e.ViewModelName.Contains("SettingsView"))
            {
                this._canSwitchToPodcast = true;
                this._canSwitchToSettings = false;
                this._canSwitchToDownloads = true;
            }
            else if (e.ViewModelName.Contains("DownloadsView"))
            {
                this._canSwitchToPodcast = true;
                this._canSwitchToSettings = true;
                this._canSwitchToDownloads = false;
            }
        }
        #endregion

    }
}
