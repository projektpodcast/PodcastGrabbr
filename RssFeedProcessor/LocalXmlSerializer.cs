using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RssFeedProcessor
{
    [XmlRoot("podcasts")]
    public class LocalXmlSerializer
    {
        [XmlElement("show")]
        public ShowNode Show { get; set; }


        public class ShowNode
        {
            [XmlAttribute("showId")]
            public int ShowId { get; set; }
            [XmlElement("rssLink")]
            public string RssLink { get; set; }
            [XmlElement("description")]
            public string Description { get; set; }
            [XmlElement("episodes")]
            public List<EpisodeNode> EpisodeList { get; set; }
        }


        public class EpisodeNode
        {
            //[XmlAttribute("showId")]
            //public int ShowId { get; set; }
            [XmlAttribute("episodeId")]
            public int EpisodeId { get; set; }
            [XmlElement("summary")]
            public string Summary { get; set; }
            [XmlElement("mediaPath")]
            public string MediaPath { get; set; }
            [XmlElement("downloadLink")]
            public string DownloadLink { get; set; }

        }

        public void PodcastToSerializedXml(Podcast podcast)
        {

        }

        public void SerializePodcast(Podcast podcast)
        {
            //string xmlUri = "";

            //XmlLoader xmlLoader = new XmlLoader();
            //XmlDocument loadedXml = xmlLoader.CreateXmlDocument(xmlUri);

            string filename = ".\\Test";
            TextWriter writer = new StreamWriter(filename);


            XmlSerializer serializer = new XmlSerializer(typeof(LocalXmlSerializer));





            LocalXmlSerializer serializedXml = new LocalXmlSerializer();
            serializedXml.Show = new ShowNode();
            serializedXml.Show.EpisodeList = new List<EpisodeNode>();


            //Map
            serializedXml.Show.Description = podcast.ShowInfo.Description;
            serializedXml.Show.RssLink = "mylink";
            serializedXml.Show.ShowId = 1;

            foreach (Episode item in podcast.EpisodeList)
            {
                EpisodeNode episodeNode = new EpisodeNode();
                episodeNode.EpisodeId = 1;
                //episodeNode.ShowId = 1;
                episodeNode.Summary = item.Summary;
                episodeNode.MediaPath = "\\";
                episodeNode.DownloadLink = item.FileDetails.SourceUri;
                serializedXml.Show.EpisodeList.Add(episodeNode);
            }

            









            serializer.Serialize(writer, serializedXml);
            //Show show = CreateShowObject(memoryStreamWithXml);
            //xmlLoader.SetMemoryStreamPositionToStart(memoryStreamWithXml);
            //List<Episode> episodeList = CreateEpisodeListObject(memoryStreamWithXml);

            //Podcast newPodcast = CreatePodcast(show, episodeList);
            //return newPodcast;




        }


    }
}
