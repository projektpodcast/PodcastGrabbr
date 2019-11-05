using PodcastGrabbr.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr
{
    public class InitializerService
    {
        private UserNavigationViewModel UserNavigationVm { get; set; }
        private Initializer InitializerVm { get; set; }
        public InitializerService(UserNavigationViewModel viewModelUserNavigation, Initializer viewModelInitializer)
        {
            UserNavigationVm = viewModelUserNavigation;
            InitializerVm = viewModelInitializer;

            SetUpSubscriber();
        }

        public InitializerService()
        {
        }

        public void SetUpSubscriber()
        {
            InitializerVm.OnTestChanged += UserNavigationVm.MainViewModel_OnTestChanged;
            UserNavigationVm.OnTestChanged += InitializerVm.UserNavigationUi_OnTestChanged;
        }

        private void IsDataTypeSet()
        {

        }

    }
}
