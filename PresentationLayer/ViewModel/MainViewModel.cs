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
    public class MainViewModel : BaseViewModel
    {
        #region Properties
        private IInitializerService _initializerService { get; set; }
        private IView PodcastUi { get; set; }
        private IView _settingsUi { get; set; }
        private IView SettingsUi { get; set; }
        private IView _userNavigationUi { get; set; }
        public IView UserNavigationUi
        {
            get { return _userNavigationUi; }
            set { _userNavigationUi = value; OnPropertyChanged("UserNavigationUi"); }
        }

        private object _currentContent { get; set; }
        public object CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; OnPropertyChanged("CurrentContent"); OnTest(_currentContent.ToString()); }
        }
        #endregion Properties
        public MainViewModel()
        {
            _initializerService = new ViewInitializer();
            InitializeUserNavigationUi();
            InitializeCurrentContent();
        }

        public void InitializeCurrentContent()
        {
            if (PodcastUi == null)
            {
                IViewModel viewModel = new PodcastViewModel();
                PodcastUi = _initializerService.InitializeView(viewModel);
            }
            DecideCurrentContent();
        }

        public void InitializeSettingsUi()
        {
            IViewModel viewModel = new SettingsViewModel();
            SettingsUi = _initializerService.InitializeView(viewModel);
        }

        public void InitializeUserNavigationUi()
        {
            UserNavigationViewModel viewModel = new UserNavigationViewModel();
            UserNavigationUi = _initializerService.InitializeView(viewModel);
            SetUpSubscriber(viewModel);
        }

        private void DecideCurrentContent()
        {
            bool dataTargetIsSet = UserSettingsManager.IsDataTypeSet();
            if (dataTargetIsSet == true)
            {
                CurrentContent = PodcastUi;
            }
            else
            {
                InitializeSettingsUi();
                CurrentContent = SettingsUi;
                //MessageBox.Show("Bitte Datenziel auswählen", "Fehlende Einstellung", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SwitchCurrentContentTo()
        {
            if (CurrentContent == PodcastUi)
            {
                InitializeSettingsUi();
                CurrentContent = SettingsUi;
            }
            else
            {
                SwitchToPodcastUi();
            }
        }

        private void SwitchToPodcastUi()
        {
            if (UserSettingsManager.IsDataTypeSet() == true)
            {
                SettingsUi.ViewModelType = null;
                SettingsUi = null;
                CurrentContent = PodcastUi;
            }
            else
            {
                //MessageBox.Show("Bitte Datenziel auswählen", "Fehlende Einstellung", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void UserNavigationUi_OnTestChanged(object sender, OnNavigationButtonClicked e)
        {
            SwitchCurrentContentTo();
        }

        public event EventHandler<OnCurrentContentChanged> OnTestChanged;

        public void OnTest(string property)
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnCurrentContentChanged() { ViewModelName = property });
            }
        }


        private void SetUpSubscriber(UserNavigationViewModel userVm)
        {
            this.OnTestChanged += userVm.MainViewModel_OnTestChanged;
            userVm.OnTestChanged += this.UserNavigationUi_OnTestChanged;
        }

    }
}



