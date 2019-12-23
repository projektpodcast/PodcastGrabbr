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
    public static class TaskUtilities
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }

}
