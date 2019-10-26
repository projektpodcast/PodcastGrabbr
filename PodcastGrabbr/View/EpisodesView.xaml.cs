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

namespace PodcastGrabbr.View
{
    /// <summary>
    /// Interaction logic for EpisodesView.xaml
    /// </summary>
    public partial class EpisodesView : Page
    {
        public EpisodesView()
        {
            InitializeComponent();
        }
        private void HeaderContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateColumnsWidth(sender as ListView);
        }
        private void UpdateColumnsWidth(ListView listView)
        {
            int columnToResize = (listView.View as GridView).Columns.Count - 3;
            //if (listView.ActualWidth == Double.NaN)
            //{
            //    listView.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            //}

            double remainingSpace = listView.ActualWidth - 29;
            for (int i = 0; i < (listView.View as GridView).Columns.Count; i++)
            {
                if (i != columnToResize)
                {
                    var columnWidth = (listView.View as GridView).Columns[i].ActualWidth;
                    remainingSpace -= columnWidth;
                    if (remainingSpace <= 70)
                    {
                        remainingSpace = 70;
                    }
                }
                     (listView.View as GridView).Columns[columnToResize].Width = remainingSpace >= 0 ? remainingSpace : 0;
            }


        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateColumnsWidth(sender as ListView);
        }
    }
}
