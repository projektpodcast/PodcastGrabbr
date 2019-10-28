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
    /// Interaction logic for NavigationUserControl.xaml
    /// </summary>
    public partial class UserNavigationView : UserControl
    {
        public UserNavigationView()
        {
            InitializeComponent();
        }
        private void TestMenueButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.wow.Visibility == Visibility.Visible)
            {
                this.wow.Visibility = Visibility.Collapsed;
                //this.MenueBackground.Visibility = Visibility.Visible;

            }
            else
            {
                this.wow.Visibility = Visibility.Visible;
                //this.MenueBackground.Visibility = Visibility.Collapsed;

            }
        }

        private void MenueButton4_Click(object sender, RoutedEventArgs e)
        {
            PagesSingletonViewModel.Instance.LoadSettingsView();
        }

        private void MenueButton1_Click(object sender, RoutedEventArgs e)
        {
            PagesSingletonViewModel.Instance.LoadShowView();
            //PagesSingletonViewModel.Instance.LoadShowView();
        }
    }
}
