using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PodcastGrabbr.ViewModel
{
    public class UserNavigationViewModel : BaseViewModel
    {
        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visible { get { return _visibility; } set { _visibility = value; OnPropertyChanged("Visible") ; } }

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
                        p => this.CanSwitchToSettingsPage(),
                        p => this.ExecuteSwitchToSettingsPage());
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
                        p => this.CanSwitchToHomePage(),
                        p => this.ExecuteSwitchToHomePage());
                }
                return _switchPageToHome;
            }
        }
        public void ExecuteSwitchToSettingsPage()
        {
            PagesSingletonViewModel.Instance.LoadSettingsView();
        }

        public void ExecuteSwitchToHomePage()
        {
            PagesSingletonViewModel.Instance.LoadHomeView();
        }

        private bool CanSwitchToSettingsPage()
        {
            if (PagesSingletonViewModel.Instance.CurrentTopPage != PagesSingletonViewModel.Instance.SettingsPage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanSwitchToHomePage()
        {
            if (PagesSingletonViewModel.Instance.CurrentTopPage != PagesSingletonViewModel.Instance.AllShowsPage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanClickButton()
        {
            return true;
        }

        public void DecideVisibilityProperty()
        {
            switch (Visible)
            {
                case Visibility.Visible:
                    this.Visible = Visibility.Collapsed;
                    break;
                case Visibility.Collapsed:
                    this.Visible = Visibility.Visible;
                    break;
                default:
                    this.Visible = Visibility.Collapsed;
                    break;
            }
        }


    }
}
