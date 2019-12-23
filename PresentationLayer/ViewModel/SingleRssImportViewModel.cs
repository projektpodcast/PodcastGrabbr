using BusinessLayer;
using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    /// AUTHOR DER KLASSE: PG
    /// 
    public class SingleRssImportViewModel : BaseViewModel
    {
        public IBusinessAccessService _businessAccessService { get; set; }
        private string _rssUri { get; set; }
        public string RssUri
        {
            get { return _rssUri; }
            set { _rssUri = value; OnPropertyChanged("RssURi"); }
        }

        public SingleRssImportViewModel(IBusinessAccessService businessAccessService)
        {
            _businessAccessService = businessAccessService;
        }

        private ICommand _linkProcessing { get; set; }
        public ICommand LinkProcessing
        {
            get
            {
                if (_linkProcessing == null)
                {
                    _linkProcessing = new RelayCommand(
                        p => this.IsRssLinkSet(),
                        p => this.ExecuteLinkProcessing());
                }
                return _linkProcessing;
            }
        }

        private bool IsRssLinkSet()
        {  
            return !string.IsNullOrEmpty(RssUri); ;
        }

        private void ExecuteLinkProcessing()
        {
            if (CheckIfValidUrl(RssUri))
            {
                try
                {
                    _businessAccessService.Save.SavePodcast(RssUri);
                    OnPodcastsInserted();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Problemme mit Link {RssUri}\n\nFehlercolde:\n{ex.ToString()}");
                }
            }
            else
            {
                MessageBox.Show("Bitte fügen Sie einen gültigen Rss-Link ein.", "Ungültiger Link");
            }
            
        }

        public bool CheckIfValidUrl(string feedUri)
        {
            Uri uriResult;
            return Uri.TryCreate(feedUri, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Das Event soll ausgelöst werden, wenn über die Einstellungen (z.B. import/löschen) Podcasts im Datenziel hinzugefügt oder entfernt werden.
        /// </summary>
        public event System.EventHandler<OnPodcastsManipulated> OnPodcastsUpdated;
        public void OnPodcastsInserted()
        {
            if (OnPodcastsUpdated != null)
            {
                OnPodcastsUpdated(this, new OnPodcastsManipulated());
            }
        }
    }
}
