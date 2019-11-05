using PodcastGrabbr.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PodcastGrabbr.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel(UserNavigationViewModel userVm, Initializer init)
        {
            init.UserNavigationUi.ToString();
        }
        //public PodcastView PodcastUi { get; set; }
        //public SettingsView SettingsUi { get; set; }
        //public UserNavigationView UserNavigationUi {get;set;}

        //public Initializer InitSevice { get; set; }

        //private object _currentContent { get; set; }
        //public object CurrentContent { get { return _currentContent; } set { _currentContent = value; OnTest(value.ToString()); } }

        //public object UserNavigation { get; set; }

        ////public void ToUserNavigation()
        ////{
        ////    UserNavigationUi.OnTestChanged += UserNavigationUi_OnTestChanged;
        ////}
        //public MainViewModel()
        //{
        //    CurrentContent = new object();
        //    CurrentContent = InitSevice.PodcastUi;
        //    UserNavigation = InitSevice.UserNavigationUi;
        //}

        //public void UserNavigationUi_OnTestChanged(object sender, OnTestEventChanged e)
        //{
        //    if (CurrentContent == InitSevice.PodcastUi)
        //    {
        //        CurrentContent = InitSevice.SettingsUi;
        //    }
        //    else
        //    {
        //        CurrentContent = InitSevice.PodcastUi;
        //    }
        //}



        //public event EventHandler<OnTestEventChanged> OnTestChanged;

        //public void OnTest(string property)
        //{
        //    if (OnTestChanged != null)
        //    {
        //        OnTestChanged(this, new OnTestEventChanged() { PropertyName = property });
        //    }
        //}


    }
}
