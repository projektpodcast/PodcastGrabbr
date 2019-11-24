using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class OnConfigChanged : EventArgs
    {
        public string SettingProperty { get; set; }
        public int SettingValue { get; set; }
    }
}
