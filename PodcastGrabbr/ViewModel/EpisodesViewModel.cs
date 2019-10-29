using PodcastGrabbr.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PodcastGrabbr.ViewModel
{
    public class EpisodesViewModel : BaseViewModel
    {
        private ShowModel ShowSelection { get; set; }
        public ObservableCollection<EpisodeModel> EpisodesCollection { get; set; }

        public EpisodesViewModel()
        {
            EpisodesCollection = new ObservableCollection<EpisodeModel>();

            FillWithExampleData();
            PagesSingletonViewModel.Instance.LoadSettingsView();
        }

        private void MakeVisible()
        {
            if (Visible != Visibility.Visible)
            {
                Visible = Visibility.Visible;
            }
        }

        private Visibility _visible = Visibility.Collapsed;
        public Visibility Visible { get { return _visible; } set { _visible = value; OnPropertyChanged("Visible"); } }

        public void SubscriberNewShowSelected(object sender, OnShowSelected e)
        {
            MakeVisible();

            DateTime now = DateTime.Now;
            ShowSelection = e.ShowSelection;
            EpisodesCollection.Add(new EpisodeModel() { Title = "Neue Show Selected", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg",
                Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364",
                Summary = ShowSelection.Description });
        }

        public void FillWithExampleData()
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < 4; i++)
            {
                EpisodesCollection.Add(new EpisodeModel() { Title = "#1364 - Brian RedbanRedbanRedb anRedbanRedbanRedbanRedba nRedbanRedbanRed banRedbanRedban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg" });
                EpisodesCollection.Add(new EpisodeModel() { Title = "#1364 - Brian Redban", PublishDate = now, ImageUri = "http://static.libsyn.com/p/assets/9/7/4/9/97497ae393125526/JRE1364.jpg", Keywords = "podcast,joe,party,experience,brian,freak,rogan,redban,deathsquad,jre,1364", Summary = "Brian Redban is a comedian and the founder of the Deathsquad podcast network. Also look for him on “Kill Tony” available on Apple Podcasts & YouTube: https://www.youtube.com/channel/UCwzCMiicL-hBUzyjWiJaseg" });
            }
        }
        

    }
}

               