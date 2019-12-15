using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    /// <summary>
    /// Event, dass im UserNavigationModel implementiert wird.
    /// Soll ausgelöst werden, wenn der angezeigten Inhalt mit einer anderen View/ViewModel ersetzt wird.
    /// </summary>
    public class OnNavigationButtonClicked : EventArgs
    {
        /// <summary>
        /// Property soll der Name des angewählten Menüpunktes sein.
        /// Anhand dieser Property, kann das MainViewModel entscheiden, zu welcher View/ViewModel gewechselt werden soll.
        /// </summary>
        public string ChangeTo { get; set; }
    }
}
