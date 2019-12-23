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
using System.Windows.Shapes;
using PresentationLayer.ViewModel;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for SingleRssImportView.xaml
    /// </summary>
    public partial class SingleRssImportView : Window, IView
    {
        public IViewModel ViewModelType { get; set; }
        public SingleRssImportView(IViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            this.Owner = App.Current.MainWindow;
        }

    }
}
