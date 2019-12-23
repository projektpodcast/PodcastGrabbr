using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;

namespace PresentationLayer.Services
{
    public class UserConfigurationService : IConfigurationService
    {

        private IDataStorageType _configDatenArt { get; set; }
        public IDataStorageType ConfigDatenArt {
            get { return _configDatenArt; }
            private set { _configDatenArt = value;/* OnDatenHaltungChange(ConfigDatenArt);*/ }
        }

        /// <summary>
        /// Konstruktor liest die lokale UserConfig und setzt diese als Property.
        /// </summary>
        public UserConfigurationService()
        {
            ConfigDatenArt = new DataStorageType();
            //guid = Guid.NewGuid();
            GetUserConfiguration();
        }

        /// <summary>
        /// Überprüft, ob der Benutzer über das SettingsViewModel/die SettingsView die UserConfig verändert hat.
        /// </summary>
        /// <returns></returns>
        public bool IsPropertySet()
        {
            bool isSet = false;
            if (ConfigDatenArt.DataType.Key != 0)
            {
                isSet = true;
            }
            return isSet;
        }

        /// <summary>
        /// Schreibt in die lokale UserConfig und synchronisiert die Property "ConfigDatenArt" mit den neuen Daten.
        /// </summary>
        /// <param name="datenHaltung"></param>
        public void UpdateUserConfiguration(IDataStorageType datenHaltung)
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
            SyncFactory();
        }

        /// <summary>
        /// Liest die lokale UserConfig und füllt die Property "ConfigDatenArt" mit dessen Werten.
        /// </summary>
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
            SyncFactory();
        }

        /// <summary>
        /// Synchronisiert die UserConfig mit der BusinessLayer.Factory.
        /// Aufgerufen, nachdem sich die UserConfig geändert hat oder sie neu geladen wurde.
        /// </summary>
        public void SyncFactory()
        {
            BusinessLayer.Factory.Instance.DatenHaltung = ConfigDatenArt;
        }


        //public event EventHandler<OnDatenHaltungChanged> DatenHaltungChanged;

        //public void OnDatenHaltungChange(IDataStorageType datenHaltungsTyp)
        //{
        //    if (DatenHaltungChanged != null)
        //    {
        //        DatenHaltungChanged(this, new OnDatenHaltungChanged() { DatenHaltungTyp = ConfigDatenArt });
        //    }
        //}


    }
}
