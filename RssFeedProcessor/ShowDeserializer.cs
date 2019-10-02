using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RssFeedProcessor
{
    [XmlRoot("rss")]
    internal class ShowDeserializer
    {
        [XmlElement("channel")]
        public DeserializedShow DeserializedShowData { get; set; }
        [XmlIgnore]
        public Show ShowDTO { get; set; }
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
            //[XmlElement("image")]
            //public ImageValue ImageUri2 { get; set; }
        }

        public class Categories
        {
            [XmlAttribute("text")]
            public string CategoryName { get; set; }
        }
        public class ImageValue
        {
            [XmlAttribute("href")]
            public string ImageLink { get; set; }
        }

        public Show XmlToDeserializedShow(MemoryStream memoryStreamWithXml)
        {
            DeserializeXmlToMappedPodcastShow(memoryStreamWithXml);
            SerializedShowToDataTransferObject(DeserializedShowData);
            return ShowDTO;
        }

        private void DeserializeXmlToMappedPodcastShow(MemoryStream memoryStreamWithXml)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ShowDeserializer));

            ShowDeserializer serializedShow = new ShowDeserializer();
            serializedShow = (ShowDeserializer)deserializer.Deserialize(memoryStreamWithXml);
            DeserializedShowData = serializedShow.DeserializedShowData;
        }

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

        private DateTime ConvertDateTime(string dateTimeForParsing)
        {
            DateTimeParser dateParser = new DateTimeParser();
            return dateParser.ConvertStringToDateTime(dateTimeForParsing);
        }

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
