using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Event, dass im MainViewModel implementiert wird.
    /// Soll ausgelöst werden, wenn der angezeigten Inhalt mit einer anderen View/ViewModel ersetzt wird.
    /// Subscriber ist das NavigationsViewModel.
    /// </summary>
    public class OnCurrentContentChanged : EventArgs
    {
        /// <summary>
        /// Anhand dieser Property weiß das UserNavigationViewModel, welcher Navigationspunkt
        /// momentan angezeigt wird, um diesen Menüpunkt als inaktiv darzustellen.
        /// </summary>
        public string ViewModelName { get; set; }
    }
}
