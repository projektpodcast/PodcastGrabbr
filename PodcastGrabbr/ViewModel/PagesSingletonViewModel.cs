using CommonTypes;
using PodcastGrabbr.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PodcastGrabbr.ViewModel
{
    public sealed class PagesSingletonViewModel : BaseViewModel
    {
        #region Singleton
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
        #endregion Singleton
        #region Properties (Instanzen der Views)
        private UserNavigationView _userNavigation { get; set; }
        public UserNavigationView UserNavigation
        {
            get { return _userNavigation; }
            set { _userNavigation = value; OnPropertyChanged("UserNavigation"); }
        }

        private PodcastView _podcastPage { get; set; }
        public PodcastView PodcastPage
        {
            get { return _podcastPage; }
            set { _podcastPage = value; OnPropertyChanged("AllShowsPage"); }
        }


        private SettingsView _settingsPage { get; set; }
        public SettingsView SettingsPage
        {
            get { return _settingsPage; }
            set { _settingsPage = value; OnPropertyChanged("SettingsPage"); }
        }
        #endregion SettingsViewModel  

        private object _currentContent { get; set; }
        public object CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; OnPropertyChanged("CurrentContent"); }
        }

        public void LoadSettingsView()
        {
            SettingsPage = new SettingsView(new SettingsViewModel());
            CurrentContent = SettingsPage;
        }

        public void LoadPodcastView()
        {
            
            if (CheckIfDataTargetIsSet() == true)
            {
                SettingsPage = null;
            }
            else
            {
                LoadSettingsView();
                MessageBox.Show("Bitte Datenziel auswählen");
            }

        }

        private void InstantiateStandardView()
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                UserNavigation = new UserNavigationView(new UserNavigationViewModel());
                PodcastPage = new PodcastView(new PodcastViewModel());
                CurrentContent = PodcastPage;

                CheckIfDataTargetIsSet();
            });
        }

        private bool CheckIfDataTargetIsSet()
        {
            if (SettingsManager.IsDataTypeSet() == false)
            {
                LoadSettingsView();
                return false;
            }
            return true;
        }

    }
}
