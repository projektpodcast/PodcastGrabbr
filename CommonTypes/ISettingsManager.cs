using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public interface ISettingsManager
    {
        int Value { get; set; }

        void CreateSection();
        string GetSectionValue();
        void SaveSectionValue(int type);
    }
}
