using BusinessLayer;
using CommonTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModel
{
    public class DownloadsViewModel : IViewModel
    {
        public string Name { get; set; }
        private IBusinessAccessService _businessAccess { get; set; }
        public ObservableCollection<Podcast> Podcasts { get; set; }

        public DownloadsViewModel(IBusinessAccessService businessAccess)
        {
            this._businessAccess = businessAccess;
            //Mockdata
            GetMockData();
        }

        #region MockData
        public void GetMockData()
        {
            var a = _businessAccess.Get.GetMockDownloadedPodcasts();
            Podcasts = new ObservableCollection<Podcast>(a);
            
        }
        #endregion MockData
    }
}
