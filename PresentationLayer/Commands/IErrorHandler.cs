using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    /// <summary>
    /// ToDo: In Klassen, die asynchrone Command-Methoden ausführen, einen implementierten ErrorHandler injecten und Fehler anzeigen.
    /// </summary>
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }

}
