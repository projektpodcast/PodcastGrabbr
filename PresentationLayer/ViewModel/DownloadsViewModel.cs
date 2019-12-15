using BusinessLayer;
using CommonTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class DownloadsViewModel : BaseViewModel, IViewModel
    {
        private IBusinessAccessService _businessAccess { get; set; }
        public ObservableCollection<Podcast> Podcasts { get; set; }

        private Episode _selectedEpisode { get; set; }
        public Episode SelectedEpisode
        {
            get { return _selectedEpisode; }
            set { _selectedEpisode = value; OnPropertyChanged("SelectedEpisode"); }
        }


        private ICommand _playEpisode { get; set; }
        public ICommand PlayEpisode
        {
            get
            {
                if (_playEpisode == null)
                {
                    _playEpisode = new RelayCommand(
                        param => true, 
                        param => this.ExecutePlayMedia(param));
                }
                return _playEpisode;
            }
        }

        private ICommand _deleteEpisode { get; set; }
        public ICommand DeleteEpisode
        {
            get
            {
                if (_deleteEpisode == null)
                {
                    _deleteEpisode = new RelayCommand(
                        param => this.IsEpisodeSelected(param), 
                        param => this.ExecuteDeleteEpisode());
                }
                return _deleteEpisode;
            }
        }

        private bool IsEpisodeSelected(object param)
        {
            if (param != null)
            {
                SelectedEpisode = (Episode)param;
                return true;
            }
            return false;
        }

        private void ExecutePlayMedia(object param)
        {
            _businessAccess.Get.PlayMediaFile((Episode)param);
        }

        private void ExecuteDeleteEpisode()
        {
            SelectedEpisode = null;
            throw new NotImplementedException();
            //
        }

        public DownloadsViewModel(IBusinessAccessService businessAccess)
        {
            this._businessAccess = businessAccess;
            //Mockdata
            GetMockData();
        }

        #region MockData
        public void GetMockData()
        {
            List<Podcast> a = _businessAccess.Get.GetMockDownloadedPodcasts();

            Episode extraEpisode = new Episode();
            extraEpisode.IsDownloaded = true;
            extraEpisode.Title = "Joe Rogan and the world. Now. ";
            extraEpisode.Summary = "Joe Rogan and the world. Now. Joe Rogan and the world. Now. Joe Rogan and the world. Now. Joe Rogan and the world. Now. Joe Rogan and the world. Now. ";

            a[0].EpisodeList.Add(extraEpisode);
            Podcasts = new ObservableCollection<Podcast>(a);

        }
        #endregion MockData









        private ICommand _fileImport { get; set; }
        public ICommand FileImport
        {
            get
            {
                if (_fileImport == null)
                {
                    _fileImport = new RelayCommand(
                        p => this.DataTrue(),
                        p => this.ExecuteImport());
                }
                return _fileImport;
            }
        }

        private bool DataTrue()
        {
            return false;
        }

        private void ExecuteImport()
        {
            Console.WriteLine("awsd");
        }





    }
}
