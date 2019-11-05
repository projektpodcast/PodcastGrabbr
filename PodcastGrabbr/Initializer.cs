using PodcastGrabbr.View;
using PodcastGrabbr.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PodcastGrabbr.ViewModel
{
    public class Initializer : BaseViewModel
    {
        private PodcastView PodcastUi { get; set; }

        private SettingsView _settingsUi { get; set; }
        private SettingsView SettingsUi { get; set; }
        private UserNavigationView _userNavigationUi { get; set; }
        public UserNavigationView UserNavigationUi
        {
            get { return _userNavigationUi; }
            set { _userNavigationUi = value; OnPropertyChanged("UserNavigationUi"); }
        }

        private InitializerService _initializerService { get; set; }

        private object _currentContent { get; set; }
        public object CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; OnPropertyChanged("CurrentContent"); OnTest(_currentContent.ToString()); }
        }

        public Initializer()
        {
            InitializeUserNavigationUi();
            InitializePodcastUi();
        }

        private void SwitchToPodcastUi()
        {
            if (SettingsManager.IsDataTypeSet() == true)
            {
                SettingsUi.DataContext = null;
                SettingsUi = null;
                CurrentContent = PodcastUi;
            }
            else
            {
                //MessageBox.Show("Bitte Datenziel auswählen", "Fehlende Einstellung", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void InitializePodcastUi()
        {
            if (PodcastUi == null)
            {
                PodcastViewModel viewModel = new PodcastViewModel();
                PodcastView view = new PodcastView(viewModel);
                //return view;
                PodcastUi = view;


                bool dataTargetIsSet = SettingsManager.IsDataTypeSet();
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
        }

        public void InitializeSettingsUi()
        {
            SettingsViewModel viewModel = new SettingsViewModel();
            SettingsView view = new SettingsView(viewModel);
            SettingsUi = view;
        }

        public void InitializeUserNavigationUi()
        {
            UserNavigationViewModel viewModel = new UserNavigationViewModel();
            UserNavigationView view = new UserNavigationView(viewModel);
            UserNavigationUi = view;


            SetUpSubscriber(viewModel);
            //InitializerService service = new InitializerService(viewModel, this);
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

        public void UserNavigationUi_OnTestChanged(object sender, OnTestEventChanged e)
        {
            SwitchCurrentContentTo();
        }


        public event EventHandler<OnTestEventChanged> OnTestChanged;

        public void OnTest(string property)
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnTestEventChanged() { PropertyName = property });
            }
        }


        private void SetUpSubscriber(UserNavigationViewModel userVm)
        {
            this.OnTestChanged += userVm.MainViewModel_OnTestChanged;
            userVm.OnTestChanged += this.UserNavigationUi_OnTestChanged;
        }

    }
}



