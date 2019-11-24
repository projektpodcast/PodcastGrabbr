using CommonTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class AppConfigManager /*: ISettingsManager*/
    {


        //private readonly string Key = "TargetType";
        //public int Value { get; set; }

        //public AppConfigManager()
        //{
        //    CreateSection();
        //}

        //public event EventHandler<OnValueChanged> ValueChanged;

        //public void ValueHasChanged()
        //{
        //    if (ValueChanged != null)
        //    {
        //        ValueChanged(this, new OnValueChanged());
        //    }
        //}

        //public void CreateSection()
        //{
        //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    KeyValueConfigurationCollection cfgSections = config.AppSettings.Settings;

        //    if (cfgSections[Key] == null)
        //    {
        //        cfgSections.Add(Key, "NotSet");
        //        config.Save(ConfigurationSaveMode.Modified);
        //        ConfigurationManager.RefreshSection("appSettings");
        //    }
        //}

        //public void SaveSectionValue(int type)
        //{
        //    string convertedType = type.ToString();

        //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    KeyValueConfigurationCollection cfgSections = config.AppSettings.Settings;

        //    if (cfgSections[Key].Value != convertedType)
        //    {
        //        cfgSections[Key].Value = convertedType;
        //    }
        //    config.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection("appSettings");
        //}

        //public string GetSectionValue()
        //{
        //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    KeyValueConfigurationCollection cfgSections = config.AppSettings.Settings;

        //    string currentValue = "NotSet";

        //    //Prüfung ob Section existiert: nein (hinzufügen) ja(prüfung ob Value exisitert und schreiben)
        //    if (cfgSections[Key] == null)
        //    {
        //    }
        //    else
        //    {
        //        currentValue = cfgSections[Key].Value;
        //    }
        //    config.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection("appSettings");

        //    return currentValue;
        }



        //public static int GetCurrentTarget(int type)
        //{
        //    int currentTarget = GetCurrentTargetType();
        //    return currentTarget;
        //}

        //public static void SetNewTargetType(int type)
        //{
        //    string key = "TargetType";
        //    string convertedType = type.ToString();

        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //    var settings = config.AppSettings.Settings;

        //    //Prüfung ob Section existiert: nein (hinzufügen) ja(prüfung ob Value exisitert und schreiben)
        //    if (settings[key] == null)
        //    {
        //        settings.Add(key, convertedType);
        //    }
        //    else
        //    {
        //        if (settings[key].Value != convertedType)
        //        {
        //            settings[key].Value = convertedType;
        //        }

        //    }
        //    config.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection("appSettings");
        //}

        //public static int GetCurrentTargetType()
        //{
        //    string key = "TargetType";
        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    var settings = config.AppSettings.Settings;

        //    string result = settings[key].Value;
        //    return int.Parse(result);
        //}

    }
    //static void AppConfigWriteToXml()
    //{


    //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    //    if (config.AppSettings.Settings.AllKeys.Contains("xmlAllowed"))
    //    {

    //    }
    //    else
    //    {
    //        config.AppSettings.Settings.Add("xmlAllowed", "1");
    //        config.Save(ConfigurationSaveMode.Modified);

    //        ConfigurationManager.RefreshSection("appSettings");
    //    }

    //}


    ///// <summary>
    ///// METHODE NUR FÜR TESTZWECKE
    ///// </summary>
    ///// <returns></returns>
    //static int GetXmlAllowance()
    //{
    //    var appSettings = ConfigurationManager.AppSettings;

    //    List<string> b = new List<string>();
    //    foreach (var key in appSettings.AllKeys)
    //    {
    //        if (key == "xmlAllowed")
    //        {
    //            b = appSettings.GetValues(0).ToList();
    //        }
    //    }

    //    int abc = Int32.Parse(b[0]);

    //    return abc;
    //}

