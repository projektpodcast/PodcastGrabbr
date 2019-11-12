using CommonTypes;
using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for DownloadsView.xaml
    /// </summary>
    public partial class DownloadsView : Page, IView
    {
        public IViewModel ViewModelType { get; set; }
        public DownloadsView(IViewModel viewModel)
        {
            InitializeComponent();
            ViewModelType = viewModel;
            this.DataContext = ViewModelType;
            //this.DataContext = new PodcastViewModel(new BusinessLayer.BusinessAccess());
        }

        private void ListBoxItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}
