using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RssFeedProcessor
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Diese Klasse regelt den Ablauf verschiedener, an der Xml-Deserialisierung teilhabenden, Klassen.
    /// Mit dem externen Aufruf der Methode "DeserializeRssXml(string xmlUri)" wird eine online gehostete Uri eines Rss-Feeds übergeben.
    /// Die Xml des Rss-Feeds enthält Daten eines Podcasts.
    /// Die Xml wird in einen Stream geladen und zur Deserialisierung weitergeleitet. Daraus entstehen zwei Objekte mit Relation zueinander.
    /// Die Liste enthält alle "Episoden" einer spezifischen "Show".
    /// Diese beiden Objekte werden gruppiert und als "Podcast" zurückgegeben.
    /// </summary>
    public class DeserializingManager
    {
        /// <summary>
        /// Steuert den Fluss als Methodenhelfer.
        /// Lädt Xml in einen MemoryStream. Die MemoryStreams werden an zwei Klassen verteilt.
        /// Der MemoryStream enthält eine einzige "Show" mit allen "Episoden". Diese Objekte werden aus dem Stream serialisiert und in einem "Podcast"-Objekt gruppiert.
        /// </summary>
        /// <param name="xmlUri">Uri einer online gehosteten Xml Datei</param>
        /// <returns>Gruppierung aller Episoden einer Show</returns>
        public Podcast DeserializeRssXml(string xmlUri)
        {
            XmlLoader xmlLoader = new XmlLoader();
            XmlDocument loadedXml = xmlLoader.CreateXmlDocument(xmlUri);

            using (MemoryStream memoryStreamWithXml = xmlLoader.LoadXmlDocumentIntoMemoryStream(loadedXml))
            {
                Show show = CreateShowObject(memoryStreamWithXml);
                xmlLoader.SetMemoryStreamPositionToStart(memoryStreamWithXml);
                List<Episode> episodeList = CreateEpisodeListObject(memoryStreamWithXml);

                Podcast newPodcast = CreatePodcast(show, episodeList);
                return newPodcast;
            }
        }

        /// <summary>
        /// Erstellt ein Klassenobjekt (Podcast) aus den beiden Parametern.
        /// Gruppiert alle Episoden einer Show in ein "Podcast"-Objekt.
        /// </summary>
        /// <param name="show">Deserialisiertes Objekt. Stellt eine einzelne Show dar.</param>
        /// <param name="episodeList">Deserialisiertes Objekt. Enthält alle Episoden einer Show</param>
        /// <returns>Erstelltes Klassenobjekt "Podcast"</returns>
        private Podcast CreatePodcast(Show show, List<Episode> episodeList)
        {
            Podcast newPodcast = new Podcast
            {
                ShowInfo = show,
                EpisodeList = episodeList
            };
            return newPodcast;
        }

        /// <summary>
        /// Instanziert die Klasse ShowDeserializer. Reicht einen, mit Xml geladenen, MemoryStream weiter.
        /// Erzeugt ein aus der Xml deserialisiertes "Show"-Objekt.
        /// </summary>
        /// <param name="memStream">Stream: enthält Xml einer Show mit beliebig vielen Episoden</param>
        /// <returns>Objekt einer "Show"</returns>
        private Show CreateShowObject(MemoryStream memStream)
        {
            ShowDeserializer showDeserializer = new ShowDeserializer();
            Show deserializedShow = showDeserializer.XmlToDeserializedShow(memStream);
            return deserializedShow;
        }

        /// <summary>
        /// Instanziert die Klasse "EpisodeDeserializer". Reicht einen, mit Xml geladenen, MemoryStream weiter.
        /// Erzeugt eine aus der Xml deserialisierte Listensammlung der Klasse "Episode".
        /// </summary>
        /// <param name="memoryStream">Stream: enthält Xml einer Show mit beliebig vielen Episoden</param>
        /// <returns>Sammlung aller "Episoden" in einem MemoryStream</returns>
        private List<Episode> CreateEpisodeListObject(MemoryStream memoryStream)
        {
            EpisodeDeserializer episodeDeserializer = new EpisodeDeserializer();
            List<Episode> deserializedEpisodeList = episodeDeserializer.XmlToDeserializedEpisode(memoryStream);
            return deserializedEpisodeList;
        }
    }
}
