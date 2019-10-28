using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodcastGrabbr.ViewModel;
using PodcastGrabbr.View;

namespace PodcastGrabbr
{
    public static class UiDependencies
    {
        public static AllShowsView AllShowsViewInstance()
        {
            AllShowsView allShowsViewInstance = new AllShowsView();
            return allShowsViewInstance;
        }

        public static EpisodesView EpisodesViewInstance()
        {
            EpisodesView episodesViewInstance = new EpisodesView();
            return episodesViewInstance;
        }

        public static AllShowsViewModel AllShowsViewModelInstance()
        {
            AllShowsViewModel allShowsVm = new AllShowsViewModel();
            return allShowsVm;
        }

        public static EpisodesViewModel EpisodesViewModelInstance()
        {
            EpisodesViewModel episodesVm = new EpisodesViewModel();
            return episodesVm;
        }

        public static void SetSubcriptionToShowSelectedEvent(AllShowsViewModel allShowsVm, EpisodesViewModel episodesVm)
        {
            allShowsVm.ShowSelected += episodesVm.SubscriberNewShowSelected;
        }

    }
}
