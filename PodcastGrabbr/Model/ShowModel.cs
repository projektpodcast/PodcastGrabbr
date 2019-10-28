using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.Model
{
    public class ShowModel : Show
    {
        //private string _publisherName { get; set; }
        //private string _podcastTitle { get; set; }
        //private string _imageUri { get; set; }

        //public override string PublisherName
        //{
        //    get { return _publisherName; }
        //    set { _publisherName = value; OnPropertyChanged("PublisherName"); }
        //}
        //public override string PodcastTitle
        //{
        //    get { return _podcastTitle; }
        //    set { _podcastTitle = value; OnPropertyChanged("PodcastTitle"); }
        //}
        //public override string ImageUri
        //{
        //    get { return _imageUri; }
        //    set { _imageUri = value; OnPropertyChanged("ImageUri"); }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        public ShowModel()
        {

        }

        public ShowModel(Show show)
        {
            this.PublisherName = show.PublisherName;
            this.PodcastTitle = show.PodcastTitle;
            this.Category = show.Category;
            this.Keywords = show.Keywords;
            this.Subtitle = show.Subtitle;
            this.Language = show.Language;
            this.Description = show.Description;
            this.LastBuildDate = show.LastBuildDate;
            this.LastUpdated = show.LastUpdated;
            this.ImageUri = show.ImageUri;
        }

    }
}
