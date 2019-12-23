using BusinessLayer;
using PresentationLayer.View;
using PresentationLayer.ViewModel;

namespace PresentationLayer.Services
{
    public interface IDependencyService
    {
        IView InitializeView(IViewModel viewModel);

        IConfigurationService InitializeConfigService();

        IBusinessAccessService InitializeBusinessLayer();
    }
}