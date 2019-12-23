using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class EpisodeModel : Episode, INotifyPropertyChanged
    {
        private bool _isDownloading { get; set; }
        public bool IsDownloading
        {
            get { return _isDownloading; }
            set { _isDownloading = value; OnPropertyChanged("IsDownloading"); }
        }
        public EpisodeModel(Episode episode)
        {
            this.EpisodeId = episode.EpisodeId;
            this.Title = episode.Title;
            this.PublishDate = episode.PublishDate;
            this.Summary = episode.Summary;
            this.Keywords = episode.Keywords;
            this.ImageUri = episode.ImageUri;
            this.FileDetails = episode.FileDetails;
            this.DownloadPath = episode.DownloadPath;
            this.IsDownloaded = episode.IsDownloaded;
            this.IsDownloading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
