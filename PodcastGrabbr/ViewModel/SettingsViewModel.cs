using PodcastGrabbr.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PodcastGrabbr.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
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
        #endregion ICommand Properties

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
            int currentValue = UserSettingsManager.TestValue;
            if (currentValue != 0)
            {
                var configValue = PossibleTypes.First(p => p.Key == currentValue);
                ConfigDataType = configValue;
            }
            else
            {
                KeyValuePair<int, string> a = new KeyValuePair<int, string>(currentValue, "Bitte wählen");
                ConfigDataType = a;
            }
        }

        public Dictionary<int, string> PossibleTypes { get; set; }
        public SettingsViewModel()
        {
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
                MessageBox.Show("Datenziel wird geändert"); //HIER KEINE MBOX ANZEIGEN, AN ANDERER STELLE
                SetConnectionType();
            }
        }

        public void SetConnectionType()
        {
            if (SelectedDataType.Value != null)
            {
                UserSettingsManager.TestValue = SelectedDataType.Key;
                ConfigDataType = SelectedDataType;
            }
        }

    }
}
