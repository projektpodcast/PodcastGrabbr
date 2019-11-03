using CommonTypes;
using PodcastGrabbr.View;
using PodcastGrabbr.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class PageInitializer : BaseViewModel
    {
        //public UserNavigationViewModel NavigationPage { get; set; }
        //public PodcastViewModel PodcastPage { get; set; }
        //public SettingsViewModel SettingsPage { get; set; }
        private PodcastViewModel _podcastVm { get; set; }
        private SettingsViewModel _settingsVm { get; set; }

        public PodcastView PodcastUi { get; set; }
        public SettingsView SettingsUi { get; set; }

        public UserNavigationView NavigationUi { get; set; }

        private object _currentPage { get; set; }
        public object CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        public PageInitializer()
        {
            Startup();
        }

        public void InitializePodcast()
        {
            _podcastVm = new PodcastViewModel();
            PodcastUi = new PodcastView(_podcastVm);
        }

        public void InitializeNavigation()
        {
            UserNavigationViewModel navigationView = new UserNavigationViewModel();
            navigationView.BlaEvent += NavigationView_BlaEvent;

            NavigationUi = new UserNavigationView(navigationView);

        }

        private void NavigationView_BlaEvent(object sender, OnValueChanged e)
        {
            switch (e.TestProperty)
            {
                case "Settings":
                    ChangeToSettingsView();
                    break;
                case "Home":
                    ChangeToPodcastView();
                    break;
                default:
                    break; ; //impl.
            }
        }

        public void InitializeSettings()
        {
            _settingsVm = new SettingsViewModel();
            SettingsUi = new SettingsView(_settingsVm);
        }

        //public PodcastView InitializePodcast()
        //{
        //    PodcastUi = new PodcastView(new PodcastViewModel());
        //    return PodcastUi;
        //}

        //public UserNavigationView InitializeNavigation()
        //{
        //    UserNavigationViewModel navigationView = new UserNavigationViewModel();
        //    navigationView.PropertyChanged += NavigationView_PropertyChanged;

        //    NavigationUi = new UserNavigationView(new UserNavigationViewModel());



        //    return NavigationUi;
        //}

        //public SettingsView InitializeSettings()
        //{
        //    SettingsUi = new SettingsView(new SettingsViewModel());
        //    return SettingsUi;
        //}


        private void ChangeToSettingsView()
        {
            if (CurrentPage == SettingsUi)
            {

            }
            else
            {
                if (SettingsUi == null)
                {
                    InitializeSettings();
                }
                else
                {
                    _settingsVm = null;
                    SettingsUi = null;
                    InitializeSettings();
                }
                CurrentPage = SettingsUi;
            }
        }

        private void ChangeToPodcastView()
        {
            CurrentPage = PodcastUi;
        }

        //public void InitializePodcast()
        //{
        //    PodcastUi = new PodcastView(new PodcastViewModel());
        //}

        //public void InitializeNavigation()
        //{
        //    NavigationUi = new UserNavigationView(new UserNavigationViewModel());
        //}

        //public void InitializeSettings()
        //{
        //    SettingsUi = new SettingsView(new SettingsViewModel());
        //}

        private void Startup()
        {
            InitializeNavigation();
            InitializePodcast();
            CurrentPage = PodcastUi;
        }

    }
}
