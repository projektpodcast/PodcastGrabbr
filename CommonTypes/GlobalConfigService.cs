using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class GlobalConfigService
    {
        protected ISettingsManager _settings;

        public GlobalConfigService(ISettingsManager settingsManager)
        {
            _settings = settingsManager;
        }

        public string GetTypeValue()
        {
            var a = _settings.GetSectionValue();
            return a;
        }

    }
}
