using PodcastGrabbr.View;
using PodcastGrabbr.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.Services
{
    public class ViewInitializer : IInitializerService
    {
        public ViewInitializer()
        {

        }
        public IView InitializeView(IViewModel viewModel)
        {
            IView viewType;
            switch (viewModel)
            {
                case PodcastViewModel a:
                    viewType = new PodcastView(viewModel);
                    break;
                case SettingsViewModel b:
                    viewType = new SettingsView(viewModel);
                    break;
                case UserNavigationViewModel c:
                    viewType = new UserNavigationView(viewModel);
                    break;
                default:
                    throw new Exception();
            }
            return viewType;
        }


        //public void SetUpSubscriber()
        //{
        //    InitializerVm.OnTestChanged += UserNavigationVm.MainViewModel_OnTestChanged;
        //    UserNavigationVm.OnTestChanged += InitializerVm.UserNavigationUi_OnTestChanged;
        //}

        //private void IsDataTypeSet()
        //{

        //}

    }
}
