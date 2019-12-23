namespace BusinessLayer
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Das Interface IBusinessAccessService vereint zwei Klassen:
    /// Klasse a) Methoden um Daten aus einer Datenquelle zu erhalten
    /// Klasse b) Methoden um Daten in ein Datenziel zu speichern
    /// in ein Klassenobjekt.
    /// Zentriert den Zugriff für den Presentation auf nur ein Objekt.
    /// </summary>
    public interface IBusinessAccessService
    {
        GetObjects Get { get; set; }
        SaveObjects Save { get; set; }
    }
}