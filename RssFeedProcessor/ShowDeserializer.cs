using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/// <summary>
/// Die Klasse ShowDeserializer besitzt drei Aufgaben:
/// 1. Xml-Knoten, die Daten einer "Show" enthalten, an Properties mappen.
/// 2. Eine Xml deserialisieren und die Werte an die Properties binden.
/// 3. Die Properties an ein "Show"-Objekt binden und zurückgeben.
/// Die Klasse wird mit Aufruf der Methode "XmlToDeserializedShow(MemoryStream xmlStream)" angesteuert. Der MemoryStream enthält eine Rss-Feed-Xml eines Podcasts.
/// Anhand den gemappten Properties kann die Xml deserialisiert werden.
/// </summary>
namespace RssFeedProcessor
{
    [XmlRoot("rss")]
    public class ShowDeserializer
    {
        //DataTransferObjekt der Klasse "Show" welches zurückgegeben werden soll.
        [XmlIgnore]
        public Show ShowDTO { get; set; }

        //Die verknotete Xml Struktur wird mit Klassen-Wrappern dargestellt.
        //Map Properties
        #region MappedProperties
        //Hinweis: Um Attribute eines Xml-Elements zu deserialisieren muss eine Wrapper-Klasse verwendet werden. 
        //(Beispiel: die Property "ImageUri" des Typs "ImageValue" zeigt den Xml-Knoten an, welcher angesteuert werden soll.
        //Die Definition der Klasse "ImageValue" zeigt das XmlAttribute an (href), das gelesen werden soll.
        [XmlElement("channel")]
        public DeserializedShow DeserializedShowData { get; set; }
        public class DeserializedShow
        {
            [XmlElement("managingEditor")]
            public string PublisherName { get; set; }
            [XmlElement("author", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string ITunesPublisherName { get; set; }
            [XmlElement("title")]
            public string PodcastTitle { get; set; }
            [XmlElement("keywords", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string Keywords { get; set; }
            [XmlElement("category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public List<Categories> CategoryList { get; set; }
            [XmlElement("subtitle", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string Subtitle { get; set; }
            [XmlElement("language")]
            public string Language { get; set; }
            [XmlElement("description")]
            public string Description { get; set; }
            [XmlElement("pubDate")]
            public string LastUpdated { get; set; }
            [XmlElement("lastBuildDate")]
            public string LastBuildDate { get; set; }
            [XmlElement("image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public ImageValue ImageUri { get; set; }
        }
        //Wrapper-Klasse um Attribute anzusteuern.
        public class Categories
        {
            [XmlAttribute("text")]
            public string CategoryName { get; set; }
        }
        //Wrapper-Klasse um Attribute anzusteuern.
        public class ImageValue
        {
            [XmlAttribute("href")]
            public string ImageLink { get; set; }
        }
        #endregion MappedProperties

        /// <summary>
        /// Helfermethode regelt den Methodenfluss. Diese Methode wird von außen angesteuert und erhält einen MemoryStream mit geladener Xml.
        /// Sie ruft eine Methode auf, welche die einzige "Show" aus der Xml deserialisiert. 
        /// Das deserialisierte Objekt wird durch eine zweite Methode an ein DTO-Objekt des Typs "Show" gebunden und zurückgegeben.
        /// </summary>
        /// <param name="xmlStream">Stream: enthält Xml einer Show mit beliebig vielen Episoden</param>
        /// <returns>Eine Show die aus dem xmlStream deserialisiert wurde</returns>
        public Show XmlToDeserializedShow(MemoryStream xmlStream)
        {
            DeserializeXmlToMappedPodcastShow(xmlStream);
            SerializedShowToDataTransferObject(DeserializedShowData);
            return ShowDTO;
        }

        /// <summary>
        /// Instanziert einen XmlSerializer. Der XmlSerializer wird mit den gemappten Properties der Klasse ShowDeserializer geladen.
        /// Anhand den gemappten Properties werden nun die Knotenwerte der Xml an die übereinstimmenden Properties gebunden.
        /// Für einen xmlStream entsteht ein Serien-Objekt. Das Objekt wird an die klasseneigene Property "DeserializedShowData" gebunden. 
        /// </summary>
        /// <param name="xmlStream">Stream: enthält Xml einer Show mit beliebig vielen Episoden</param>
        private void DeserializeXmlToMappedPodcastShow(MemoryStream xmlStream)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ShowDeserializer));

            ShowDeserializer deserializedShow = new ShowDeserializer();
            deserializedShow = (ShowDeserializer)deserializer.Deserialize(xmlStream);
            DeserializedShowData = deserializedShow.DeserializedShowData;
        }

        /// <summary>
        /// Bindet die klasseneigenen Properties an eine Instanz der allgemeinen "Show"-Klasse.
        /// Mit dem konditionellen Operator "?:" 
        /// wird a) ein default-Wert an eine non-nullable Property zugewiesen.
        /// oder b) eine alternativer Property-Wert zugewiesen.
        /// </summary>
        /// <param name="deserializedShow">Deserialisiertes Show-Objekt. Nicht fähig für übergreifenden Datentransfer. Muss an ein "Show"-Objekt gebunden werden.</param>
        private void SerializedShowToDataTransferObject(DeserializedShow deserializedShow)
        {
            ShowDTO = new Show
            {
                Description = deserializedShow.Description,
                Language = deserializedShow.Language,
                PodcastTitle = deserializedShow.PodcastTitle,
                Keywords = deserializedShow.Keywords,
                PublisherName = deserializedShow.PublisherName != null ? deserializedShow.PublisherName : deserializedShow.ITunesPublisherName,
                Subtitle = deserializedShow.Subtitle,
                LastUpdated = ConvertDateTime(deserializedShow.LastUpdated),
                LastBuildDate = ConvertDateTime(deserializedShow.LastBuildDate),
                Category = IterateCategoriesAndAddToShow(deserializedShow),
                ImageUri = deserializedShow.ImageUri != null ? deserializedShow.ImageUri.ImageLink : ""
            };
        }

        /// <summary>
        /// Initialisiert den Zugriff auf einen DateTimeParser.
        /// </summary>
        /// <param name="dateTimeForParsing">string, der zu DateTime geparsed werden soll</param>
        /// <returns>DateTime Objekt</returns>
        private DateTime ConvertDateTime(string dateTimeForParsing)
        {
            DateTimeParser dateParser = new DateTimeParser();
            return dateParser.ConvertStringToDateTime(dateTimeForParsing);
        }

        /// <summary>
        /// Die Unterklasse "Categories" des übergebenen "DeserializedShow"-Objekts wird iteriert.
        /// Jeder gefundene Eintrag dieser List<Categories> wird einer Liste<string> hinzugefügt.
        /// </summary>
        /// <param name="_neueSerie"></param>
        /// <returns>Liste an strings, enthält alle Kategorien einer Serie</returns>
        private List<string> IterateCategoriesAndAddToShow(DeserializedShow _neueSerie)
        {
            List<string> categoryList = new List<string>();
            foreach (Categories item in _neueSerie.CategoryList)
            {
                categoryList.Add(item.CategoryName);
            }
            return categoryList;
        }
    }
}
