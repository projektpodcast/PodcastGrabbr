using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/// <summary>
/// AUTHOR DER KLASSE: PG
/// 
/// Die Klasse EpisodeDeserializer besitzt drei Aufgaben:
/// 1. Xml-Knoten, die Daten einer "Episode" enthalten, an Properties mappen.
/// 2. Eine Xml deserialisieren und die Werte an die Properties binden.
/// 3. Die Properties an ein "Episode"-Listenobjekt und zurückgeben.
/// Die Klasse wird mit Aufruf der Methode "XmlToDeserializedEpisode(MemoryStream xmlStream)" angesteuert. Der MemoryStream enthält eine Rss-Feed-Xml eines Podcasts.
/// Anhand den gemappten Properties kann die Xml deserialisiert werden.
/// </summary>
namespace RssFeedProcessor
{
    [XmlRoot("rss")]
    public class EpisodeDeserializer
    {
        //Interne Liste der Klasse "DeserializedEpisode". Nicht fähig für übergreifenen Datentransfer. Ist an eine Listenobjekt des Typs "Episode" gebunden werden.
        [XmlIgnore]
        private List<DeserializedEpisode> AllDeserializedEpisodes { get; set; }

        //DataTransferObjekt der Klasse "Episode" welches zurückgegeben werden soll.
        [XmlIgnore]
        public List<Episode> EpisodeListDTO { get; set; }

        //Map Properties
        #region MappedProperties
        //Hinweis: Um Attribute eines Xml-Elements zu deserialisieren muss eine nested-Class verwendet werden. 
        //(Beispiel: die Property "FileInfo" des Typs "FileData" zeigt das XmlElement an das gelesen werden muss. 
        //Die Definition der Klasse "FileData" zeigt das XmlAttribute an (url), das gelesen werden soll.
        [XmlElement("channel")]
        public ChannelNode Channel { get; set; }

        public class ChannelNode
        {
            [XmlElement("item")]
            public List<DeserializedEpisode> DeserializedEpisodeList { get; set; }
        }
        public class DeserializedEpisode
        {
            [XmlElement("title")]
            public string Title { get; set; }
            [XmlElement("pubDate")]
            public string PublishingDate { get; set; }
            [XmlElement("keywords", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string Keywords { get; set; }
            [XmlElement("description")]
            public string Summary { get; set; }
            [XmlElement("image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public ImageLink LinkToImage { get; set; }
            [XmlElement("enclosure")]
            public FileData FileInfo { get; set; }
        }

        public class ImageLink
        {
            [XmlAttribute("href")]
            public string Link { get; set; }
        }

        public class FileData
        {
            [XmlAttribute("url")]
            public string PodcastUri { get; set; }
            [XmlAttribute("length")]
            public int Length { get; set; }
            [XmlAttribute("type")]
            public string Type { get; set; }
        }
        #endregion MappedProperties

        /// <summary>
        /// Helfermethode regelt den Methodenfluss. Diese Methode wird von außen angesteuert und erhält einen MemoryStream mit geladener Xml.
        /// Sie ruft eine Methode auf, welche die vielzahl an "Episoden" aus der Xml deserialisiert.
        /// Das deserialisierte Objekt wird durch eine zweite Methode an ein DTO-Listenobjekt des Typs "Episode" gebunden und zurückgegeben.
        /// </summary>
        /// <param name="xmlStream">Stream: enthält Xml einer Show mit beliebig vielen Episoden</param>
        /// <returns></returns>
        public List<Episode> XmlToDeserializedEpisode(MemoryStream xmlStream)
        {
            XmlLoader deserializingProcessor = new XmlLoader();

            DeserializeXmlToMappedPodcastEpisode(xmlStream);

            SerializedShowToDataTransferObject(AllDeserializedEpisodes);
            return EpisodeListDTO;
        }

        /// <summary>
        /// Instanziert einen XmlSerializer. Der XmlSerializer wird mit den gemappten Properties der Klasse ShowDeserializer geladen.
        /// Anhand den gemappten Properties werden nun die Knotenwerte der Xml an die übereinstimmenden Properties gebunden.
        /// Jede einzelne deserialisierte Episode wird derselben klasseneigenen Property-Liste "AllDeserializedEpisodes" hinzugefügt.
        /// Für einen xmlStream enstehen so viele Listeneinträge wie die Xml-Datei Episodenknoten hat.
        /// </summary>
        /// <param name="xmlStream">Stream: enthält Xml einer Show mit beliebig vielen Episoden</param>
        private void DeserializeXmlToMappedPodcastEpisode(MemoryStream xmlStream)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(EpisodeDeserializer));
            DeserializedEpisode serializedShow = new DeserializedEpisode();

            EpisodeDeserializer episodesCollection = new EpisodeDeserializer();
            episodesCollection = (EpisodeDeserializer)deserializer.Deserialize(xmlStream);

            AllDeserializedEpisodes = episodesCollection.Channel.DeserializedEpisodeList;
        }

        /// <summary>
        /// Bindet die klasseneigenen Properties an eine Instanz der allgemeinen "Show"-Klasse.
        /// Mit dem konditionellen Operator "?:" 
        /// wird a) ein default-Wert an eine non-nullable Property zugewiesen.
        /// oder b) eine alternativer Property-Wert zugewiesen.
        /// Es werden so viele Listeneinträge initialisiert wie es Listeneinträge im übergebenen Parameter gibt.
        /// </summary>
        /// <param name="deserializedShow">Deserialisierte Liste mit Episodeneinträgen. 
        /// Nicht fähig für übergreifenen Datentransfer. Muss an eine Listenobjekt des Typs "Episode" gebunden werden.</param>
        private void SerializedShowToDataTransferObject(List<DeserializedEpisode> deserializedShow)
        {
            EpisodeListDTO = new List<Episode>();
            foreach (DeserializedEpisode item in deserializedShow)
            {
                Episode newEpisode = new Episode
                {
                    Title = item.Title,
                    PublishDate = ConvertDateTime(item.PublishingDate),
                    Keywords = item.Keywords,
                    Summary = item.Summary,
                    ImageUri = item.LinkToImage != null ? item.LinkToImage.Link : "",
                    FileDetails = new FileInformation(item.FileInfo.PodcastUri, item.FileInfo.Length, item.FileInfo.Type)
                };
                EpisodeListDTO.Add(newEpisode);
            }
            AllDeserializedEpisodes = null;
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
    }
}
