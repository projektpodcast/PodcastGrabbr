using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class UserConfigService : IUserConfigService
    {
        private readonly string _configProperty = "DataType";

        private int _configValue { get; set; }
        public int ConfigValue
        {
            get { return _configValue; }
            set
            {
                UpdateSettings(value);
                _configValue = value;
                ConfigSync();
            }
        }

        public UserConfigService()
        {
            ConfigValue = (int)Properties.Settings.Default[_configProperty];
        }

        public bool IsPropertySet()
        {
            if (ConfigValue == 0)
            {
                return false;
            }
            return true;
        }

        private void UpdateSettings(int newValue)
        {
            Properties.Settings.Default.DataType = newValue;
            Properties.Settings.Default.Save();
        }

        //public event EventHandler<OnConfigChanged> OnTestChanged;

        //public void OnTest(string property)
        //{
        //    if (OnTestChanged != null)
        //    {
        //        OnTestChanged(this, new OnConfigChanged() { SettingProperty = _configProperty, SettingValue = ConfigValue });
        //    }
        //}

        private void ConfigSync()
        {
            Factory.TargetType = ConfigValue;
        }
    }
}
