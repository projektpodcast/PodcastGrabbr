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
        public IDatenArt ConfigDatenArt { get; set; }

        public bool IsPropertySet()
        {
            throw new NotImplementedException();
        }

        public void UpdateUserConfiguration(IDatenArt datatype)
        {

        }

        public void GetUserConfigurationService()
        {

        }
    }
}
