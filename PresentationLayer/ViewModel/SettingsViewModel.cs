using CommonTypes;
using PresentationLayer.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        //private IUserConfigService _configService { get; set; }
        #region Ui Properties
        private IConfigurationService _configurationService { get; set; }
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

        private Visibility _dbDetailVisibility { get; set; }
        public Visibility DbDetailVisibility { get { return _dbDetailVisibility; } set { _dbDetailVisibility = value; OnPropertyChanged("DbDetailVisibility"); } }


        private string _dbPassword { get; set; }
        public string DbPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; OnPropertyChanged("DbPassword"); }
        }

        #endregion Ui Properties

        public SettingsViewModel(IConfigurationService configService)
        {
            SelectedDataType = new KeyValuePair<int, string>();

            PossibleTypes = new Dictionary<int, string>
            {
                { 1, "Lokal: Xml" },
                { 2, "Datenbank: MySQL" },
                { 3, "Datenbank: PostgreSQL" }
            };

            _configurationService = configService;
            MapConfigData();
            IsConfigurationSet();
        }

        #region ICommand Properties
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
        #endregion ICommand Properties


        private bool IsDataTypeSelected()
        {
            return SelectedDataType.Key != 0 ? true : false;
        }

        private void ExecuteFileImport()
        {
            IDialogService fileServe = new FileDialogService();
            fileServe.StartFileDialog();
        }

        private void ExecuteDeleteAllDownloads()
        {
            //bl..deletemedia
        }

        private void ExecuteDeleteAllPodcasts()
        {
            //bl..delete
        }

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



        public void ExecutePersistDbData()
        {
            MessageBox.Show("Datenziel wird geändert"); //HIER KEINE MBOX ANZEIGEN, AN ANDERER STELLE
            _configurationService.UpdateUserConfiguration(ConfigData);
            ConfigurationHasChanged();
        }

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



        public event System.EventHandler<OnConfigChanged> OnTestChanged;

        public void ConfigurationHasChanged()
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnConfigChanged() { SettingValue = ConfigData.DataType.Key, SettingProperty = ConfigData.DataType.Value });
            }
        }
    }

}



//public event System.EventHandler<OnConfigChanged> OnTestChanged;

//public void OnTest()
//{
//    if (OnTestChanged != null)
//    {
//        OnTestChanged(this, new OnConfigChanged());
//    }
//}


//private ICommand _toggleVisibility { get; set; }
//public ICommand ToggleVisibility
//{
//    get
//    {
//        if (_toggleVisibility == null)
//        {
//            _toggleVisibility = new RelayCommand(
//                p => this.IsDataTypeSelected(),
//                p => this.ExecuteVisibilityToggle());
//        }
//        return _toggleVisibility;
//    }
//}
//public void SetConnectionType()
//{
//    if (SelectedDataType.Value != null)
//    {
//        GlobalUserCfgService.TestValue = SelectedDataType.Key;
//        ConfigDataType = SelectedDataType;
//    }
//}