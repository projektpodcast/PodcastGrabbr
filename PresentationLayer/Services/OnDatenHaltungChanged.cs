using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class OnDatenHaltungChanged : EventArgs
    {
        public IDatenArt DatenHaltungTyp {get;set;}
    }
}
