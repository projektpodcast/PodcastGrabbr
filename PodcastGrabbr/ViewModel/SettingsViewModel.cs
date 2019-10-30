using CommonTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private KeyValuePair<int, string> _selectedDataType { get; set; }
        public KeyValuePair<int, string> SelectedDataType {
            get { return _selectedDataType; }
            set { _selectedDataType = value;
                OnPropertyChanged("SelectedDataType"); SetConnectionType(); }
        }

        public Dictionary<int, string> PossibleTypes { get; set; }
        public SettingsViewModel()
        {
            SelectedDataType = new KeyValuePair<int, string>();

            //Dictionary<int, string> types = SelectedDataType.GetPossibleDataTypes();

            PossibleTypes = new Dictionary<int, string>
            {
                { 0, "Lokal: Xml" },
                { 1, "Datenbank: MySQL" },
                { 2, "Datenbank: PostgreSQL" }
            };
        }

        public void SetConnectionType()
        {
            if (SelectedDataType.Value != null)
            {
                var a = SelectedDataType;
            }
        }

    }
}
