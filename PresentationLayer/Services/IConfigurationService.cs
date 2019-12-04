using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public interface IConfigurationService
    {
        IDatenArt ConfigDatenArt { get; }
        void UpdateUserConfiguration(IDatenArt datenHaltung);
        bool IsPropertySet();
    }
}
