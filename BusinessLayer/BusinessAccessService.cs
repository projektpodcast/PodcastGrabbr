using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    /// <summary>
    /// Der BusinessAccessService vereint zwei Klassen:
    /// Klasse a) Methoden um Daten aus einer Datenquelle zu erhalten
    /// Klasse b) Methoden um Daten in ein Datenziel zu speichern
    /// in ein Klassenobjekt.
    /// Zentriert den Zugriff für den Presentation auf nur ein Objekt.
    /// </summary>
    public class BusinessAccessService : IBusinessAccessService
    {
        /// <summary>
        /// Enthält Methoden um Daten aus einer Datenquelle zu erhalten.
        /// </summary>
        public GetObjects Get { get; set; }
        /// <summary>
        /// Enthält Methoden um Daten in eine Datenquelle zu speichern.
        /// </summary>
        public SaveObjects Save { get; set; }

        /// <summary>
        /// Konstruktor initialisiert die Property-Objekte selbständig.
        /// </summary>
        public BusinessAccessService()
        {
            Get = new GetObjects();
            Save = new SaveObjects();
        }
    }
}
