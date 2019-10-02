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
    public class DeserializingManager
    {
        public Podcast DeserializeRssXml(string xmlUri)
        {

            XmlLoader deserializingProcessor = new XmlLoader();
            XmlDocument loadedXml = deserializingProcessor.CreateXmlDocument(xmlUri);

            using (MemoryStream memoryStreamWithXml = deserializingProcessor.LoadXmlDocumentIntoMemoryStream(loadedXml))
            {
                Show series = CreateSeriesObject(memoryStreamWithXml);
                deserializingProcessor.SetMemoryStreamPositionToStart(memoryStreamWithXml);
                List<Episode> episodeList = CreateEpisodeListObject(memoryStreamWithXml);

                Podcast newPodcast = CreatePodcast(series, episodeList);
                return newPodcast;
            }
        }

        public Podcast CreatePodcast(Show series, List<Episode> episodeList)
        {
            Podcast newPodcast = new Podcast
            {
                ShowInfo = series,
                EpisodeList = episodeList
            };
            return newPodcast;
        }

        public Show CreateSeriesObject(MemoryStream memStream)
        {
            ShowDeserializer showDeserializer = new ShowDeserializer();
            Show deserializedShow = showDeserializer.XmlToDeserializedShow(memStream);
            return deserializedShow;
        }

        public List<Episode> CreateEpisodeListObject(MemoryStream memoryStream)
        {
            EpisodeDeserializer episodeDeserializer = new EpisodeDeserializer();
            List<Episode> deserializedEpisodeList = episodeDeserializer.XmlToDeserializedEpisode(memoryStream);
            return deserializedEpisodeList;
        }
    }
}
