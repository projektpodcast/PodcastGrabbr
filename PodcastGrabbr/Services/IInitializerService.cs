using PodcastGrabbr.View;
using PodcastGrabbr.ViewModel;

namespace PodcastGrabbr.Services
{
    public interface IInitializerService
    {
        IView InitializeView(IViewModel viewModel);
    }
}