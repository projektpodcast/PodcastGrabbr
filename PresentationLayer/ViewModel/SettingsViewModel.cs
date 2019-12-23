using BusinessLayer;
using CommonTypes;
using LocalStorage;
using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    /// AUTHOR DER KLASSE: PG
    /// 
    public class SettingsViewModel : BaseViewModel
    {
        #region Ui Properties
        private IConfigurationService _configurationService { get; set; }

        /// <summary>
        /// Gebunden an das SelectedItem einer ComboBox
        /// Wenn ein neues Item ausgewählt wird, wird geprüft ob das DetailGrid 
        /// in der Ui angezeigt werden soll oder nicht (Visibility Property an View gebunden)
        /// </summary>
        private KeyValuePair<int, string> _selectedDataType { get; set; }
        public KeyValuePair<int, string> SelectedDataType
        {
            get { return _selectedDataType; }
            set
            {
                _selectedDataType = value;
                OnPropertyChanged("SelectedDataType"); DecideVisibility();
                if (ConfigData != null)
                {
                    ConfigData.DataType = value;
                }
            }
        }

        public Dictionary<int, string> PossibleTypes { get; set; }

        /// <summary>
        /// Ui Property, welche die Sichtbarkeit eines Grids bestimmt.
        /// </summary>
        private Visibility _dbDetailVisibility { get; set; }
        public Visibility DbDetailVisibility { get { return _dbDetailVisibility; } set { _dbDetailVisibility = value; OnPropertyChanged("DbDetailVisibility"); } }

        /// <summary>
        /// An eine PasswordBox in der View gebunden. Inhalt ist ein Encrypted Passwort. Passwort wird automatisch durch eine Custom PasswordBox encrypted.
        /// </summary>
        private string _dbPassword { get; set; }
        public string DbPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; OnPropertyChanged("DbPassword"); }
        }

        private IBusinessAccessService _businessAccess { get; set; }

        #endregion Ui Properties

        /// <summary>
        /// Initialisiert non-nullable Properties, setzt das Dictionary für die Datenbank-Combobox
        /// und synchronisiert die UserConfiguration mit einer Klasseninternen Property.
        /// </summary>
        /// <param name="configService"></param>
        public SettingsViewModel(IConfigurationService configService, IBusinessAccessService businessAccessService)
        {
            SelectedDataType = new KeyValuePair<int, string>();

            PossibleTypes = new Dictionary<int, string>
            {
                { 1, "Lokal: Xml" },
                { 2, "Datenbank: MySQL" },
                { 3, "Datenbank: PostgreSQL" }
            };

            _configurationService = configService;
            _businessAccess = businessAccessService;

            MapConfigData();
            IsConfigurationSet();
        }

        #region ICommand Properties und CanExecute-Prüfungen
        private ICommand _fileImport { get; set; }
        public ICommand FileImport
        {
            get
            {
                if (_fileImport == null)
                {
                    _fileImport = new RelayCommand(
                        p => this.IsDataTypeSelected(),
                        p => this.ExecuteFileImport());
                }
                return _fileImport;
            }
        }

        private ICommand _deleteAllPodcasts { get; set; }
        public ICommand DeleteAllPodcasts
        {
            get
            {
                if (_deleteAllPodcasts == null)
                {
                    _deleteAllPodcasts = new RelayCommand(
                        p => this.IsDataTypeSelected(),
                        p => this.ExecuteDeleteAllPodcasts());
                }
                return _deleteAllPodcasts;
            }
        }

        private ICommand _deleteAllDownloads { get; set; }
        public ICommand DeleteAllDownloads
        {
            get
            {
                if (_deleteAllDownloads == null)
                {
                    _deleteAllDownloads = new RelayCommand(
                        p => this.IsDataTypeSelected(),
                        p => this.ExecuteDeleteAllDownloads());
                }
                return _deleteAllDownloads;
            }
        }

        private ICommand _persistDbData { get; set; }
        public ICommand PersistDbData
        {
            get
            {
                if (_persistDbData == null)
                {
                    _persistDbData = new RelayCommand(
                        p => IsDataTypeSelected(),
                        p => this.ExecutePersistDbData());
                }
                return _persistDbData;
            }
        }

        private bool IsDataTypeSelected()
        {
            return SelectedDataType.Key != 0 ? true : false;
        }
        #endregion ICommand Properties

        /// <summary>
        /// Öffnet einen FileDialog, der durch das Interface IDialogService angeboten wird.
        /// Bisher keine elegante Lösung gefunden, eine Wegnavigation zu verhindern (Nutzer soll während dem Import keinen Input tätigen dürfen)
        /// und ihn asynchron über den Zustand zu informieren.
        /// Bisherige Lösung ist bedürftig gebastelt und die .Save.ProcessRssList(uriListToProcess) ist noch synchron.
        /// </summary>
        private void ExecuteFileImport()
        {
            IDialogService fileServe = new FileDialogService();

            List<string> uriListToProcess = new List<string>();
            uriListToProcess = fileServe.StartFileDialog();

            Task.Run(() => { MessageBox.Show("Podcasts werden verarbeitet", "Bitte warten", MessageBoxButton.OK, MessageBoxImage.Information); });
            //try
            //{
            XmlStorage.Instance.MassImport(uriListToProcess);

                //for (int i = 0; i < uriListToProcess.Count; i++)
                //{

                //    _businessAccess.Save.ProcessRssList(uriListToProcess);

                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Bei der Verarbeitung ist ein Fehler aufgetreten." +
            //        $"Ab folgendem Link hat keine weitere Verarbeitung mehr stattgefunden: \n  {ex.ToString()}");
            //    //throw;
            //}
            OnPodcastsInserted();
        }



        private void ExecuteDeleteAllDownloads()
        {
            //bl..deletemedia
        }

        private void ExecuteDeleteAllPodcasts()
        {
            //bl..delete
        }

        /// <summary>
        /// Toggled die Ui-Property "Visibility" zwischen Collapsed und Visibility.
        /// Wenn der SelectedDataType.Key 1 ist, wird die Sichtbarkeit ausgeblendet.
        /// Ein Wert von 1 ist auf den DataType von Xml bezogen, dieser benötigt kein explizites setzen von Datenkbank-Informationen.
        /// </summary>
        private void DecideVisibility()
        {
            DbDetailVisibility = SelectedDataType.Key <= 1 ? Visibility.Collapsed : Visibility.Visible;
        }


        private IDataStorageType _userManipulatedData { get; set; }
        public IDataStorageType UserManipulatedData
        {
            get { return _userManipulatedData; }
            set
            {
                _userManipulatedData = value;
                OnPropertyChanged("UserManipulatedData");
            }
        }

        /// <summary>
        /// Enthält Werte der gesetzten UserConfig.
        /// Der Nutzer ändert diese Property mit seinen Nutzereingaben, 
        /// welche durch den ICommand PersistDbData wiederum in die UserConfig geschrieben wird.
        /// </summary>
        private IDataStorageType _configData { get; set; }
        public IDataStorageType ConfigData
        {
            get { return _configData; }
            set
            {
                _configData = value;
                OnPropertyChanged("ConfigData");
            }
        }

        /// <summary>
        /// Synchronisiert die Werte der UserConfiguration mit der Property ConfigDate.
        /// Dies muss explizit geschehen, da es sonst Referenzprobleme zwischen der ConfigData und der UserConfiguration gibt.
        /// </summary>
        private void MapConfigData()
        {
            ConfigData = new DataStorageType
            {
                DataType = _configurationService.ConfigDatenArt.DataType,
                Ip = _configurationService.ConfigDatenArt.Ip,
                Port = _configurationService.ConfigDatenArt.Port,
                DataBaseName = _configurationService.ConfigDatenArt.DataBaseName,
                UserName = _configurationService.ConfigDatenArt.UserName,
                EncryptedPassword = _configurationService.ConfigDatenArt.EncryptedPassword
            };
        }

        /// <summary>
        /// Speichert die vom Nutzer hinterlegten Datenbank-Daten in die lokale UserConfiguration.
        /// Löst ein Event aus um die Änderung anzuzeigen.
        /// </summary>
        public void ExecutePersistDbData()
        {
            MessageBox.Show("Datenziel wird geändert"); //HIER KEINE MBOX ANZEIGEN, AN ANDERER STELLE
            _configurationService.UpdateUserConfiguration(ConfigData);
            ConfigurationHasChanged();
        }

        /// <summary>
        /// Überprüft, ob die UserConfiguration vom Nutzer verändert wurde.
        /// Standardmäßig ist der Key in der UserConfig 0.
        /// </summary>
        public void IsConfigurationSet()
        {
            if (ConfigData.DataType.Key != 0)
            {
                KeyValuePair<int, string> parsedConfigValue = PossibleTypes.First(p => p.Key == ConfigData.DataType.Key);
                ConfigData.DataType = parsedConfigValue;
                SelectedDataType = parsedConfigValue;
            }

            else
            {
                KeyValuePair<int, string> noDataTypeSelected = new KeyValuePair<int, string>(ConfigData.DataType.Key, "Bitte wählen");
                ConfigData.DataType = noDataTypeSelected;
                SelectedDataType = noDataTypeSelected;
            }
        }


        #region Events
        public event System.EventHandler<OnConfigChanged> OnUserConfigChanged;

        /// <summary>
        /// Veröffentlicht das OnUserConfigChanged Event.
        /// Wenn es einen Event-Subscriber gibt, wird das in der UserConfig gesetzte KeyValuePair übertragen.
        /// Das Event soll ausgelöst werden, wenn die UserConfiguration durch den Nutzer bearbeitet und abgespeichert wurde.
        /// </summary>
        public void ConfigurationHasChanged()
        {
            if (OnUserConfigChanged != null)
            {
                OnUserConfigChanged(this, new OnConfigChanged() { SettingValue = ConfigData.DataType.Key, SettingProperty = ConfigData.DataType.Value });
            }
        }

        /// <summary>
        /// Das Event soll ausgelöst werden, wenn über die Einstellungen (z.B. import/löschen) Podcasts im Datenziel hinzugefügt oder entfernt werden.
        /// </summary>
        public event System.EventHandler<OnPodcastsManipulated> OnPodcastsUpdated;
        public void OnPodcastsInserted()
        {
            if (OnPodcastsUpdated != null)
            {
                OnPodcastsUpdated(this, new OnPodcastsManipulated());
            }
        }

        #endregion
    }

}