﻿using BusinessLayer;
using PresentationLayer.Services;
using PresentationLayer.View;
using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class MainViewModel : BaseViewModel, IViewModel
    {
        #region Services
        //private IUserConfigService _configService { get; set; }
        private IDependencyService _initializerService { get; set; }
        private IConfigurationService _configurationService { get; set; }
        private IBusinessAccessService _businessAccessService { get; set; }
        #endregion Services

        #region Properties
        private IView _downloadsUi { get; set; }
        private IView _podcastUi { get; set; }
        private IView _settingsUi { get; set; }
        private IView _userNavigationUi { get; set; }
        public IView UserNavigationUi
        {
            get { return _userNavigationUi; }
            private set { _userNavigationUi = value; OnPropertyChanged("UserNavigationUi"); }
        }

        private object _currentContent { get; set; }
        public object CurrentContent
        {
            get { return _currentContent; }
            private set { _currentContent = value; OnPropertyChanged("CurrentContent"); OnTest(_currentContent.ToString()); }
        }

        #endregion Properties
        public MainViewModel(/*IInitializerService initializerService*/)
        {
            //Services initialisieren
            _initializerService = new DependencyService();
            _configurationService = _initializerService.InitializeConfigService();
            _businessAccessService = _initializerService.InitializeBusinessLayer();

            //Benutzeroberfläche initialisieren
            InitializeUserNavigationUi();
            InitializeCurrentContent();
        }

        private void InitializeCurrentContent()
        {
            if (_podcastUi == null)
            {
                InitializePodcastUi();
            }
            DecideCurrentContent();
        }

        private void InitializePodcastUi()
        {
            IViewModel viewModel = new PodcastViewModel(_businessAccessService);
            _podcastUi = _initializerService.InitializeView(viewModel);
        }

        private void InitializeSettingsUi()
        {
            IViewModel viewModel = new SettingsViewModel(_configurationService);
            _settingsUi = _initializerService.InitializeView(viewModel);
            SetUpSubscriber(viewModel);
        }

        private void InitializeUserNavigationUi()
        {
            UserNavigationViewModel viewModel = new UserNavigationViewModel();
            UserNavigationUi = _initializerService.InitializeView(viewModel);
            SetUpSubscriber(viewModel);
        }

        //private void InitializeUserNavigationUi()
        //{
        //    UserNavigationViewModel viewModel = new UserNavigationViewModel();
        //    UserNavigationUi = new TestView(viewModel);
        //    SetUpSubscriber(viewModel);
        //}

        private void InitializeDownloadsUi()
        {
            IViewModel viewModel = new DownloadsViewModel(_businessAccessService);
            _downloadsUi = new DownloadsView(viewModel);
        }

        private void DecideCurrentContent()
        {
            //bool dataTargetIsSet = _configService.IsPropertySet();


            //bool dataTargetIsSet = _configurationService.IsPropertySet();

            if (CanSwitchOffSettings())
            {
                CurrentContent = _podcastUi;
            }
            else
            {
                InitializeSettingsUi();
                CurrentContent = _settingsUi;
                //MessageBox.Show("Bitte Datenziel auswählen", "Fehlende Einstellung", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //private void SwitchCurrentContentTo()
        //{
        //    if (CurrentContent == _podcastUi)
        //    {
        //        InitializeSettingsUi();
        //        CurrentContent = _settingsUi;
        //    }
        //    else
        //    {
        //        SwitchToPodcastUi();
        //    }
        //}

        private bool CanSwitchOffSettings()
        {
            return _configurationService.IsPropertySet();
        }

        private void SwitchToPodcastUi()
        {
            //CanSwitchOffSettings() == true ? CurrentContent = _settingsUi : CurrentContent = _podcastUi;
            //CurrentContent = CanSwitchOffSettings() == true ? _settingsUi : _podcastUi;

            if (CanSwitchOffSettings())
            {
                if (_settingsUi != null)
                {
                    _settingsUi.ViewModelType = null;
                    _settingsUi = null;
                }
                CurrentContent = _podcastUi;
            }

            else
            {
                //MessageBox.Show("Bitte Datenziel auswählen", "Fehlende Einstellung", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
                MessageBox.Show("Bitte Datenziel konfigurieren und speichern", "Fehlende Einstellungen", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }



            //if (_configService.IsPropertySet() == true)
            //{
            //    if (_settingsUi != null)
            //    {
            //        _settingsUi.ViewModelType = null;
            //        _settingsUi = null;
            //    }
            //    CurrentContent = _podcastUi;
            //}

            //else
            //{
            //    //MessageBox.Show("Bitte Datenziel auswählen", "Fehlende Einstellung", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void SwitchToSettingsUi()
        {
            if (CurrentContent != _settingsUi)
            {
                InitializeSettingsUi();
                CurrentContent = _settingsUi;
            }
        }

        private void SwitchToDownloads()
        {
            if (CanSwitchOffSettings())
            {
                if (_settingsUi != null)
                {
                    _settingsUi.ViewModelType = null;
                    _settingsUi = null;
                }
                InitializeDownloadsUi();
                CurrentContent = _downloadsUi;
            }

            else
            {
                MessageBox.Show("Bitte Datenziel konfigurieren und speichern", "Fehlende Einstellungen", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void ReloadPodcastUi()
        {
            _podcastUi.ViewModelType = null;
            _podcastUi = null;
            InitializePodcastUi();
        }

        public void UserNavigationUi_OnTestChanged(object sender, OnNavigationButtonClicked e)
        {
            //SwitchCurrentContentTo();
            switch (e.ChangeTo)
            {
                case "ToSettings":
                    SwitchToSettingsUi();
                    break;
                case "ToPodcast":
                    SwitchToPodcastUi();
                    break;
                case "ToDownloads":
                    SwitchToDownloads();
                    break;
                default:
                    break;
            }
        }

        public event EventHandler<OnCurrentContentChanged> OnTestChanged;

        public void OnTest(string property)
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnCurrentContentChanged() { ViewModelName = property });
            }
        }

        private void SetUpSubscriber(IViewModel viewModel)
        {
            if (viewModel.GetType().Equals(typeof(UserNavigationViewModel)))
            {
                UserNavigationViewModel userVm = viewModel as UserNavigationViewModel;
                this.OnTestChanged += userVm.MainViewModel_OnTestChanged;
                userVm.OnTestChanged += this.UserNavigationUi_OnTestChanged;
            }
            else if (viewModel.GetType().Equals(typeof(SettingsViewModel)))
            {
                SettingsViewModel settingsVm = viewModel as SettingsViewModel;
                settingsVm.OnTestChanged += ViewModel_OnTestChanged;
            }
        }

        private void ViewModel_OnTestChanged(object sender, OnConfigChanged e)
        {
            ReloadPodcastUi();
        }
    }
}



