using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PodcastGrabbr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<ShowModel> showItem = new List<ShowModel>();
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www1.wdr.de/mediathek/audio/sendereihen-bilder/wdr2-sendereihe-108~_v-Podcast.jpg", Description = "Super Sendung", Publisher = "WDR2" });
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-1x1@2dXL_-1f32c27c4978132dd0854e53b5ed30e10facc189.jpg", Description = "Dieser Podcast erzhählt von Bienen und Blumen und Sachen und Dingen", Publisher = "SWR2" });
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www1.wdr.de/mediathek/audio/sendereihen-bilder/wdr2-sendereihe-108~_v-Podcast.jpg" });
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-1x1@2dXL_-1f32c27c4978132dd0854e53b5ed30e10facc189.jpg" });
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www1.wdr.de/mediathek/audio/sendereihen-bilder/wdr2-sendereihe-108~_v-Podcast.jpg" });
            showItem.Add(new ShowModel() { Title = "Klasse Podcast mit viel Zu langem Namen: Die Supershow. Viel Spaß", ImageUri = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-1x1@2dXL_-1f32c27c4978132dd0854e53b5ed30e10facc189.jpg" });
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www1.wdr.de/mediathek/audio/sendereihen-bilder/wdr2-sendereihe-108~_v-Podcast.jpg" });
            showItem.Add(new ShowModel() { Title = "WDR2 Reportage", ImageUri = "https://www.swr.de/swr2/programm/Podcastbild-SWR2-Forum,1564746463623,swr2-forum-podcast-106~_v-1x1@2dXL_-1f32c27c4978132dd0854e53b5ed30e10facc189.jpg" });
            showViewList.ItemsSource = showItem;
            
        }
        public class ShowModel
        {
            public string Title { get; set; }
            public string ImageUri { get; set; }
            public string Publisher { get; set; }
            public string Description { get; set; }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.LineRight();
            }
            else
            {
                scrollViewer.LineLeft();
            }
            e.Handled = true;
        }
    }
}
