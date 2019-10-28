using PodcastGrabbr.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.ViewModel
{
    public class PagesViewModel : BaseViewModel
    {
        //private AllShowsView _allShowsPage { get; set; }
        //public AllShowsView AllShowsPage
        //{
        //    get { return _allShowsPage; }
        //    set { _allShowsPage = value; OnPropertyChanged("AllShowsPage"); }
        //}


        //private EpisodesView _episodesPage { get; set; }
        //public EpisodesView EpisodesPage
        //{
        //    get { return _episodesPage; }
        //    set { _episodesPage = value; OnPropertyChanged("EpisodesPage"); }
        //}

        //private SettingsView _settingsPage { get; set; }
        //public SettingsView SettingsPage
        //{
        //    get { return _settingsPage; }
        //    set { _settingsPage = value; OnPropertyChanged("SettingsPage"); }
        //}

        //private object _currentTopPage { get; set; }
        //public object CurrentTopPage
        //{
        //    get { return _currentTopPage; }
        //    set { _currentTopPage = value; OnPropertyChanged("CurrentTopPage"); }
        //}

        //private object _currentBottomPage { get; set; }
        //public object CurrentBottomPage
        //{
        //    get { return _currentBottomPage; }
        //    set { _currentBottomPage = value; OnPropertyChanged("CurrentBottomPage"); }
        //}



        //public PagesViewModel()
        //{
        //    InstantiateStandardView();
        //}

        //public void LoadSettingsView()
        //{
        //    AllShowsPage = null;
        //    EpisodesPage = null;
        //    CurrentTopPage = null;
        //    SettingsPage = new SettingsView();
        //    //CurrentTopPage = SettingsPage;
        //    //SettingsPage = new SettingsVieWModel();
        //    //CurrentTopPage = SettingsPage;
        //}



        //private void InstantiateStandardView()
        //{
        //    App.Current.Dispatcher.BeginInvoke((Action)delegate
        //    {
        //        AllShowsPage = UiDependencies.AllShowsViewInstance();
        //        EpisodesPage = UiDependencies.EpisodesViewInstance();

        //        AllShowsViewModel showsVm = UiDependencies.AllShowsViewModelInstance();
        //        EpisodesViewModel episodesVm = UiDependencies.EpisodesViewModelInstance();

        //        UiDependencies.SetSubcriptionToShowSelectedEvent(showsVm, episodesVm);

        //        AllShowsPage.DataContext = showsVm;
        //        EpisodesPage.DataContext = episodesVm;

        //        CurrentTopPage = AllShowsPage;
        //    });
        //}

    }
}
