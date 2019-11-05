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

        public ObservableCollection<ButtonModel> MenueOptions { get; set; }

        public UserNavigationViewModel()
        {
            InitializeButtonCollection();
        }

        public void InitializeButtonCollection()
        {
            ButtonModel firstButton = new ButtonModel("Startseite", SwitchPageToHome);
            ButtonModel secondButton = new ButtonModel("Einstellungen", SwitchPageToSettings);
            ButtonModel thirdButton = new ButtonModel("Meine Downloads", NotImplemented);
            ButtonModel fourthButton = new ButtonModel("Show importieren", NotImplemented);
            ButtonModel fifthButton = new ButtonModel("Shows aktualisieren", NotImplemented);
            MenueOptions = new ObservableCollection<ButtonModel>
            {
                firstButton,
                secondButton,
                thirdButton,
                fourthButton,
                fifthButton
            };
        }


        private bool _canSwitchToSettings { get; set; }
        private bool _canSwitchToPodcast { get; set; }

        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visible { get { return _visibility; } set { _visibility = value; OnPropertyChanged("Visible") ; } }

        private ICommand _notImplemented;
        public ICommand NotImplemented
        {
            get
            {
                if (_notImplemented == null)
                {
                    _notImplemented = new RelayCommand(
                        p => true,
                        p => this.Implement());
                }
                return _notImplemented;
            }
        }
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

        private void Implement()
        {

        }

        public event EventHandler<OnNavigationButtonClicked> OnTestChanged;

        public void OnTest(string property)
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnNavigationButtonClicked());
            }
        }

        public void MainViewModel_OnTestChanged(object sender, OnCurrentContentChanged e)
        {
            if (e.ViewModelName.Contains("PodcastView"))
            {
                this._canSwitchToPodcast = false;
                this._canSwitchToSettings = true;
            }
            else if (e.ViewModelName.Contains("SettingsView"))
            {
                this._canSwitchToPodcast = true;
                this._canSwitchToSettings = false;
            }
        }


    }
}
