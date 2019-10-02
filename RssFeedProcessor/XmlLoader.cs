using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/// <summary>
/// Die Klasse stellt Methoden bereit um:
/// -ein XmlDocument aus einer uri zu erstellen.
/// -Einen MemoryStream aus einem XmlDocument zu erstellen.
/// -Die Position eines MemoryStreams auf 0 zu stellen.
/// Alle Methoden können von außen angesteuert werden.
/// </summary>
namespace RssFeedProcessor
{
    public class XmlLoader
    {
        /// <summary>
        /// Lädt den Inhalt einer im Internet gehosteten Xml-Datei in handlebares XmlDocument.
        /// </summary>
        /// <param name="xmlUri">Uri eines Rss-Feeds</param>
        /// <returns>Ein XmlDocument, dass mit dem Inhalt der übergebenen Uri geladen ist</returns>
        public XmlDocument CreateXmlDocument(string xmlUri)
        {
            XmlDocument sourceXml = new XmlDocument();
            sourceXml.Load(xmlUri);
            return sourceXml;
        }

        /// <summary>
        /// Lädt den Inhalt eines XmlDocuments in einen MemoryStream. 
        /// Setzt die Positon des instanzierten MemoryStreams auf 0 diesen zu lesen.
        /// </summary>
        /// <param name="loadedXml">ein XmlDocument, welches geladene und lesbare Daten enthält</param>
        /// <returns>Ein MemoryStream, der mit dem Inhalt des übergebenen XmlDocuments geladen ist</returns>
        public MemoryStream LoadXmlDocumentIntoMemoryStream(XmlDocument loadedXml)
        {
            MemoryStream memStream = new MemoryStream();
            loadedXml.Save(memStream);
            memStream.Position = 0;
            return memStream;
        }

        /// <summary>
        /// Setzt die Positon des übergebenen MemoryStreams auf 0 um diesen erneut zu lesen.
        /// </summary>
        /// <param name="memStream">ein nicht-leerer MemoryStream</param>
        /// <returns>unveränderter MemoryStream mit Positon = 0</returns>
        public MemoryStream SetMemoryStreamPositionToStart(MemoryStream memStream)
        {
            memStream.Position = 0;
            return memStream;
        }
    }
}
