﻿using BusinessLayer;
using PresentationLayer.View;
using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class DependencyService : IDependencyService
    {
        public DependencyService()
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
                    throw new NotImplementedException();
            }
            return viewType;
        }

        public IUserConfigService InitializeConfigService()
        {
            IUserConfigService configService = new UserConfigService();
            return configService;
        }

        public IBusinessAccessService InitializeBusinessLayer()
        {
            IBusinessAccessService businessLayer = new BusinessAccess();
            return businessLayer;
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