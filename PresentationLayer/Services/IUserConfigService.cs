using System;

namespace PresentationLayer.Services
{
    public interface IUserConfigService
    {
        int ConfigValue { get; set; }
        //event EventHandler<OnConfigChanged> OnTestChanged;

        bool IsPropertySet();
        //void OnTest(string property);
    }
}