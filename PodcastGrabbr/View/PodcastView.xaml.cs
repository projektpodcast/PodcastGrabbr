using PodcastGrabbr.ViewModel;
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
    /// Interaction logic for AllShowsViewModel.xaml
    /// </summary>
    public partial class PodcastView : Page, IView
    {
        public IViewModel ViewModelType { get; set; }
        public PodcastView(IViewModel viewModel)
        {
            InitializeComponent();
            ViewModelType = viewModel;
            this.DataContext = ViewModelType;
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
                private void HeaderContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateColumnsWidth(sender as ListView);
        }
        private void UpdateColumnsWidth(ListView listView)
        {
            int columnToResize = (listView.View as GridView).Columns.Count - 3;

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
