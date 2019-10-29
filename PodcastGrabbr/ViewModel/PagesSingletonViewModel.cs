using PodcastGrabbr.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public sealed class PagesSingletonViewModel : BaseViewModel
    {
        private static readonly PagesSingletonViewModel instance = new PagesSingletonViewModel();

        static PagesSingletonViewModel()
        {

        }

        private PagesSingletonViewModel()
        {
            InstantiateStandardView();
        }

        public static PagesSingletonViewModel Instance
        {
            get
            {
                return instance;
            }
        }

        private UserNavigationView _userNavigation { get; set; }
        public UserNavigationView UserNavigation
        {
            get { return _userNavigation; }
            set { _userNavigation = value; OnPropertyChanged("UserNavigation"); }
        }

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

        private SettingsView _settingsPage { get; set; }
        public SettingsView SettingsPage
        {
            get { return _settingsPage; }
            set { _settingsPage = value; OnPropertyChanged("SettingsPage"); }
        }

        private object _currentTopPage { get; set; }
        public object CurrentTopPage
        {
            get { return _currentTopPage; }
            set { _currentTopPage = value; OnPropertyChanged("CurrentTopPage"); }
        }

        private object _currentBottomPage { get; set; }
        public object CurrentBottomPage
        {
            get { return _currentBottomPage; }
            set { _currentBottomPage = value; OnPropertyChanged("CurrentBottomPage"); }
        }


        public void LoadSettingsView()
        {
            SettingsPage = new SettingsView();
            CurrentTopPage = SettingsPage;
        }

        public void LoadHomeView()
        {
            SettingsPage = null;
            CurrentTopPage = AllShowsPage;
            CurrentBottomPage = EpisodesPage;
        }


        private void InstantiateStandardView()
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                AllShowsPage = UiDependencies.AllShowsViewInstance();
                EpisodesPage = UiDependencies.EpisodesViewInstance();

                AllShowsViewModel showsVm = UiDependencies.AllShowsViewModelInstance();
                EpisodesViewModel episodesVm = UiDependencies.EpisodesViewModelInstance();

                UiDependencies.SetSubcriptionToShowSelectedEvent(showsVm, episodesVm);

                AllShowsPage.DataContext = showsVm;
                EpisodesPage.DataContext = episodesVm;

                CurrentTopPage = AllShowsPage;

                UserNavigation = new UserNavigationView();
                UserNavigationViewModel userNaviVm = new UserNavigationViewModel();
                UserNavigation.DataContext = userNaviVm;
            });
        }
    }
}
