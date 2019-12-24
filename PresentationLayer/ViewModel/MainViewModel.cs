using BusinessLayer;
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
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Das MainViewModel initialisiert verschiedene Views mit ihren ViewModels.
    /// Es wird benötigt um multiple Views in einem Window (MainView) anzuzeigen.
    /// To Do: Castle Windsow/MVVM Light etc. implementieren um eine saubere Dependency Injection zu erreichen.
    /// </summary>
    public class MainViewModel : BaseViewModel, IViewModel
    {
        #region Services
        private IDependencyService _initializerService { get; set; }
        private IConfigurationService _configurationService { get; set; }
        private IBusinessAccessService _businessAccessService { get; set; }
        #endregion Services

        /// <summary>
        /// Properties stellen mit ihren ViewModels initialisierten Views dar.
        /// Dienen dazu, um der MainView bindbare Properties zu geben, um mehrere Views in einem Window anzuzeigen.
        /// </summary>
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

        /// <summary>
        /// Der angezeigte Content im Mainframe der MainView
        /// </summary>
        private IView _currentContent { get; set; }
        public IView CurrentContent
        {
            get { return _currentContent; }
            private set { _currentContent = value; OnPropertyChanged("CurrentContent"); OnViewChanged(_currentContent.ToString()); }
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
            DecideCurrentContent();
        }

        /// <summary>
        /// Initialisiert die PodcastView mit dem zugehörigen ViewModel und stellt das Objekt als Property zur Verfügung.
        /// </summary>
        private void InitializePodcastUi()
        {
            IViewModel viewModel = new PodcastViewModel(_businessAccessService);
            _podcastUi = _initializerService.InitializeView(viewModel);
        }

        /// <summary>
        /// Initialisiert die SettingsView mit dem zugehörigen ViewModel und stellt das Objekt als Property zur Verfügung.
        /// </summary>
        private void InitializeSettingsUi()
        {
            IViewModel viewModel = new SettingsViewModel(_configurationService, _businessAccessService);
            _settingsUi = _initializerService.InitializeView(viewModel);
            SetUpSubscriber(viewModel);
        }

        /// <summary>
        /// Initialisiert die UserNavigationView mit dem zugehörigen ViewModel und stellt das Objekt als Property zur Verfügung.
        /// </summary>
        private void InitializeUserNavigationUi()
        {
            UserNavigationViewModel viewModel = new UserNavigationViewModel();
            UserNavigationUi = _initializerService.InitializeView(viewModel);
            SetUpSubscriber(viewModel);
        }

        /// <summary>
        /// Initialisiert die DownloadsView mit dem zugehörigen ViewModel und stellt das Objekt als Property zur Verfügung.
        /// </summary>
        private void InitializeDownloadsUi()
        {
            IViewModel viewModel = new DownloadsViewModel(_businessAccessService);
            _downloadsUi = new DownloadsView(viewModel);
        }

        /// <summary>
        /// Plausenprüfung: Wenn in der UserConfig Datenbank-Werte hinterlegt sind,
        /// dann kann erst von dieser View gewechselt werden. Setzt anschließend die angezeigte View.
        /// </summary>
        private void DecideCurrentContent()
        {
            if (CanSwitchOffSettings())
            {
                if (_podcastUi == null)
                {
                    InitializePodcastUi();
                }
                CurrentContent = _podcastUi;
            }
            else
            {
                InitializeSettingsUi();
                CurrentContent = _settingsUi;
            }
        }

        /// <summary>
        /// Überprüft ob in der UserConfig Werte definiert sind, wenn ja wird true, wenn nein false returned.
        /// </summary>
        /// <returns></returns>
        private bool CanSwitchOffSettings()
        {
            return _configurationService.IsPropertySet();
        }

        /// <summary>
        /// Wechselt den momentan angezeigten Window-Inhalt (CurrentContent) zur PodcastView.
        /// Falls keine Einstellung in der UserConfig gesetzt sind, dann wird die PodcastView nicht initialisiert.
        /// </summary>
        private void SwitchToPodcastUi()
        {
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
                MessageBox.Show("Bitte Datenziel konfigurieren und speichern", "Fehlende Einstellungen", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Initialisiert die SettingsView, wenn diese nicht initialisiert ist und setzt sie als CurrentContent (angezeigter Frame im MainView)
        /// </summary>
        private void SwitchToSettingsUi()
        {
            if (CurrentContent != _settingsUi)
            {
                InitializeSettingsUi();
                CurrentContent = _settingsUi;
            }
        }

        /// <summary>
        /// Initialisiert die DownloadsView, wenn diese nicht initialisiert ist und setzt sie als CurrentContent (angezeigter Frame im MainView)
        /// </summary>
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

        /// <summary>
        /// Lädt die PodcastView mit dem PodcastViewModel erneut um die darin enthaltenen Daten neuzuladen.
        /// Wichtig ein erneutes Laden durchzufühen, wenn sich in der UserConfig die Datenbank-Daten geändert haben.
        /// </summary>
        private void ReloadPodcastUi()
        {
            if (_podcastUi != null)
            {
                _podcastUi.ViewModelType = null;
                _podcastUi = null;
            }
            InitializePodcastUi();
        }

        #region Events und Event-Subscriptions
        /// <summary>
        /// Subscribed zu einem Event des UserNavigationViewModels.
        /// Anhand der übertragenen veröffentlichen EventProperty kann entschieden werden, welche View als CurrentContent angezeigt werden soll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                case "ToImport":
                    OpenImportWindow();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Published ein Event, dass ausgelöst werden soll, wenn sich der CurrentContent ändert.
        /// </summary>
        public event EventHandler<OnCurrentContentChanged> ViewChanged;

        /// <summary>
        /// Prüft ob es einen EventSubscriber gibt.
        /// Wenn ja, wird daer im CurrentContent angezeigte ViewModel-Name übertragen.
        /// </summary>
        /// <param name="property"></param>
        public void OnViewChanged(string property)
        {
            if (ViewChanged != null)
            {
                ViewChanged(this, new OnCurrentContentChanged() { ViewModelName = property });
            }
        }

        /// <summary>
        /// Setzt dynamisch beim Initialisieren der ViewModels die publisher und subscriber von Events.
        /// </summary>
        /// <param name="viewModel"></param>
        private void SetUpSubscriber(IViewModel viewModel)
        {
            if (viewModel.GetType().Equals(typeof(UserNavigationViewModel)))
            {
                UserNavigationViewModel userVm = viewModel as UserNavigationViewModel;
                this.ViewChanged += userVm.MainViewModel_OnTestChanged;
                userVm.OnNavigationChanged += this.UserNavigationUi_OnTestChanged;
            }
            else if (viewModel.GetType().Equals(typeof(SettingsViewModel)))
            {
                SettingsViewModel settingsVm = viewModel as SettingsViewModel;
                settingsVm.OnUserConfigChanged += SettingsViewModel_OnUserConfigChanged;
                settingsVm.OnPodcastsUpdated += SettingsVm_OnPodcastsUpdated;
            }
            else if (viewModel.GetType().Equals(typeof(SingleRssImportViewModel)))
            {
                SingleRssImportViewModel importVm = viewModel as SingleRssImportViewModel;
                importVm.OnPodcastsUpdated += ImportVm_OnPodcastsUpdated;
            }
        }

        /// <summary>
        /// Subscriber eines Events, dass im SingleRssImportViewMOdel gepublished wird.
        /// Wird ausgelöst, wenn die Podcasts im Datenziel über das SingleRssImportViewMOdel verändert werden.
        /// Um die Änderungen im Ui darzustellen, muss die PodcastView neugeladen werden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportVm_OnPodcastsUpdated(object sender, OnPodcastsManipulated e)
        {
            this._singleRssImportUi.Close();
            if (CurrentContent == _podcastUi)
            {
                ReloadPodcastUi();
                SwitchToPodcastUi();
            }
            ReloadPodcastUi();
        }

        /// <summary>
        /// Subscriber eines Events, dass im SettingsViewModel gepublished wird.
        /// Wird ausgelöst, wenn die Podcasts im Datenziel über das SettingsViewModel verändert werden.
        /// Um die Änderungen im Ui darzustellen, muss die PodcastView neugeladen werden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsVm_OnPodcastsUpdated(object sender, OnPodcastsManipulated e)
        {
            //if (CurrentContent == _podcastUi)
            //{
                ReloadPodcastUi();
                SwitchToPodcastUi();
            //}
            //ReloadPodcastUi();
        }

        /// <summary>
        /// Wenn sich die Datenbank-Informationen in UserConfiguration durch Nutzereingabe geändert hat,
        /// dann wird das MainViewModel über dieses Event informiert.
        /// Dadurch wird die PodcastView neu geladen und die korrekte Datenbank verwendet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsViewModel_OnUserConfigChanged(object sender, OnConfigChanged e)
        {
            ReloadPodcastUi();
        }
        #endregion



        private SingleRssImportView _singleRssImportUi { get; set; }

        public void OpenImportWindow()
        {
            IViewModel viewModel = new SingleRssImportViewModel(_businessAccessService);
            _singleRssImportUi = new SingleRssImportView(viewModel);

            SetUpSubscriber(viewModel);

            _singleRssImportUi.ShowInTaskbar = false;
            _singleRssImportUi.ShowActivated = true;
            _singleRssImportUi.ShowDialog();
        }

        

    }
}



