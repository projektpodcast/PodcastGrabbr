using PresentationLayer.View;
using PresentationLayer.ViewModel;

namespace PresentationLayer.Services
{
    public interface IInitializerService
    {
        IView InitializeView(IViewModel viewModel);
    }
}