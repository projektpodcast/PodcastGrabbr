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
        private IUserConfigService _configService { get; set; }
        #region Ui Properties
        private KeyValuePair<int, string> _selectedDataType { get; set; }
        public KeyValuePair<int, string> SelectedDataType
        {
            get { return _selectedDataType; }
            set
            {
                _selectedDataType = value;
                OnPropertyChanged("SelectedDataType"); CheckEqualitySelectedAndConfigDataType();
            }
        }

        private KeyValuePair<int, string> _configDataType { get; set; }
        public KeyValuePair<int, string> ConfigDataType
        {
            get { return _configDataType; }
            set
            {
                _configDataType = value;
                OnPropertyChanged("ConfigDataType");
            }
        }
        #endregion Ui Properties

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
                        p => CheckDbData(),
                        p => this.ExecutePersistDbData());
                }
                return _persistDbData;
            }
        }
        #endregion ICommand Properties

        private bool CheckDbData()
        {
            if (SelectedDataType.Key != ConfigDataType.Key)
            {
                return true;
            }
            return false;
        }

        public void ExecutePersistDbData()
        {
            MessageBox.Show("Datenziel wird geändert"); //HIER KEINE MBOX ANZEIGEN, AN ANDERER STELLE
            SetConnectionType2();
        }

        #region ICommand Methods
        private bool IsDataTypeSelected()
        {
            if (ConfigDataType.Key != 0)
            {
                return true;
            }
            return false;
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
        #endregion ICommand Methods

        public void DoesItExist()
        {
            int currentValue = _configService.ConfigValue;
            if (currentValue != 0)
            {
                var configValue = PossibleTypes.First(p => p.Key == currentValue);
                ConfigDataType = configValue;
                if (currentValue == 1)
                {
                    Visible = Visibility.Collapsed;
                }
                else
                {
                    Visible = Visibility.Visible;
                }
            }
            else
            {
                KeyValuePair<int, string> a = new KeyValuePair<int, string>(currentValue, "Bitte wählen");
                ConfigDataType = a;
                Visible = Visibility.Collapsed;

            }
        }

        public Dictionary<int, string> PossibleTypes { get; set; }
        public SettingsViewModel(IUserConfigService configService)
        {
            _configService = configService;
            SelectedDataType = new KeyValuePair<int, string>();

            PossibleTypes = new Dictionary<int, string>
            {
                { 1, "Lokal: Xml" },
                { 2, "Datenbank: MySQL" },
                { 3, "Datenbank: PostgreSQL" }
            };
            DoesItExist();
        }

        private void CheckEqualitySelectedAndConfigDataType()
        {
            if (SelectedDataType.Key != ConfigDataType.Key)
            {


                if (SelectedDataType.Key == 1)
                {
                    Visible = Visibility.Collapsed;
                }
                else
                {
                    Visible = Visibility.Visible;
                }
            }
        }

        public void SetConnectionType2()
        {
            if (SelectedDataType.Value != null)
            {
                _configService.ConfigValue = SelectedDataType.Key;
                ConfigDataType = SelectedDataType;
                OnTest();
            }
        }

        public event System.EventHandler<OnConfigChanged> OnTestChanged;

        public void OnTest()
        {
            if (OnTestChanged != null)
            {
                OnTestChanged(this, new OnConfigChanged());
            }
        }

        //public void SetConnectionType()
        //{
        //    if (SelectedDataType.Value != null)
        //    {
        //        GlobalUserCfgService.TestValue = SelectedDataType.Key;
        //        ConfigDataType = SelectedDataType;
        //    }
        //}

        private Visibility _visibility { get; set; }
        public Visibility Visible { get { return _visibility; } set { _visibility = value; OnPropertyChanged("Visible"); } }

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

        private void ExecuteVisibilityToggle()
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

        private void GetConfig()
        {

        }

        private string _dbPassword { get; set; }
        public string DbPassword
        { get { return _dbPassword; }
          set { _dbPassword = value; OnPropertyChanged("DbPassword"); }
        }










        private DatenArt _selectedData { get; set; }
        public DatenArt SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged("SelectedData");
            }
        }

        private DatenArt _configData { get; set; }
        public DatenArt ConfigData
        {
            get { return _configData; }
            set
            {
                _configData = value;
                OnPropertyChanged("SelectedData");
            }
        }

        private void GetConfigData()
        {
            //erweitern: service
        }

        private void GetSelectedData()
        {
            //passiert im DataBinding, überflüssig
        }

        private void SetConfigData()
        {
            //erweitern: service
        }

        private void SyncConfigAndSelectedData()
        {
            //prinzip: selecteddata = configdata
        }







    }
}
