﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace PresentationLayer.Services
{
    public class UserConfigurationService : IConfigurationService
    {
        //public Guid guid { get; set; }


        private IDatenArt _configDatenArt { get; set; }
        public IDatenArt ConfigDatenArt {
            get { return _configDatenArt; }
            private set { _configDatenArt = value; SyncFactory(); OnDatenHaltungChange(ConfigDatenArt); }
        }

        public UserConfigurationService()
        {
            ConfigDatenArt = new DatenArt();
            //guid = Guid.NewGuid();
            GetUserConfiguration();
        }

        public bool IsPropertySet()
        {
            bool isSet = false;
            if (ConfigDatenArt.DataType.Key != 0)
            {
                isSet = true;
            }
            return isSet;
        }

        public void UpdateUserConfiguration(IDatenArt datenHaltung)
        {
            ConfigDatenArt = datenHaltung;
            Properties.Settings.Default.DataType = ConfigDatenArt.DataType.Key;
            if (ConfigDatenArt.DataType.Key != 1)
            {
                Properties.Settings.Default.DataBaseName = ConfigDatenArt.DataBaseName;
                Properties.Settings.Default.Ip = ConfigDatenArt.Ip;
                Properties.Settings.Default.Port = ConfigDatenArt.Port;
                Properties.Settings.Default.UserName = ConfigDatenArt.UserName;
                Properties.Settings.Default.EncryptedPassword = ConfigDatenArt.EncryptedPassword;
            }
            else
            {
                Properties.Settings.Default.DataBaseName = null;
                Properties.Settings.Default.Ip = null;
                Properties.Settings.Default.Port = 0;
                Properties.Settings.Default.UserName = null;
                Properties.Settings.Default.EncryptedPassword = null;
            }
            Properties.Settings.Default.Save();
        }

        public void GetUserConfiguration()
        {
            KeyValuePair<int, string> keyValuePair = new KeyValuePair<int, string>(Properties.Settings.Default.DataType, "");
            ConfigDatenArt.DataType = keyValuePair;

            if (ConfigDatenArt.DataType.Key > 1)
            {
                ConfigDatenArt.DataBaseName = Properties.Settings.Default.DataBaseName;
                ConfigDatenArt.Ip = Properties.Settings.Default.Ip;
                ConfigDatenArt.Port = Properties.Settings.Default.Port;
                ConfigDatenArt.UserName = Properties.Settings.Default.UserName;
                ConfigDatenArt.EncryptedPassword = Properties.Settings.Default.EncryptedPassword;
            }

        }

        public void SyncFactory()
        {
            BusinessLayer.Factory.DatenHaltung = ConfigDatenArt;
        }


        public event EventHandler<OnDatenHaltungChanged> DatenHaltungChanged;

        public void OnDatenHaltungChange(IDatenArt datenHaltungsTyp)
        {
            if (DatenHaltungChanged != null)
            {
                DatenHaltungChanged(this, new OnDatenHaltungChanged() { DatenHaltungTyp = ConfigDatenArt });
            }
        }


    }
}
