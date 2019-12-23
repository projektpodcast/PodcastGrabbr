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
        IDataStorageType ConfigDatenArt { get; }
        void UpdateUserConfiguration(IDataStorageType datenHaltung);
        bool IsPropertySet();
    }
}
