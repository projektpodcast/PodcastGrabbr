using CommonTypes;
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
        private bool _canSwitchToSettings { get; set; }
        private bool _canSwitchToPodcast { get; set; }

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
                        p => this._canSwitchToSettings,
                        p => this.OnTest("ToSettings"));
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
                        p => this.OnTest("ToPodcast"));
                }
                return _switchPageToHome;
            }
        }
        //public void ExecuteSwitchToSettingsPage()
        //{
        //    PagesSingletonViewModel.Instance.LoadSettingsView();
        //}

        //public void ExecuteSwitchToHomePage()
        //{
        //    PagesSingletonViewModel.Instance.LoadPodcastView();
        //}

        //private bool CanSwitchToSettingsPage()
        //{
        //    if (PagesSingletonViewModel.Instance.CurrentContent != PagesSingletonViewModel.Instance.SettingsPage)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private bool CanSwitchToHomePage()
        //{
        //    if (PagesSingletonViewModel.Instance.CurrentContent != PagesSingletonViewModel.Instance.PodcastPage)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

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


        public event EventHandler<OnTestEventChanged> OnTestChanged;

        public void OnTest(string property)
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnTestEventChanged() { PropertyName = property});
            }
        }



        public void MainViewModel_OnTestChanged(object sender, OnTestEventChanged e)
        {
            if (e.PropertyName.Contains("PodcastView"))
            {
                this._canSwitchToPodcast = false;
                this._canSwitchToSettings = true;
            }
            else if (e.PropertyName.Contains("SettingsView"))
            {
                this._canSwitchToPodcast = true;
                this._canSwitchToSettings = false;
            }
        }


    }
}
