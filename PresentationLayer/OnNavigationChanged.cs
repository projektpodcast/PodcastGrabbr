using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class OnNavigationButtonClicked : EventArgs
    {
        public string ChangeTo { get; set; }
    }
}
