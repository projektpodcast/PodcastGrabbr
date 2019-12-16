using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for PasswordBoxUserControl.xaml
    /// </summary>
    public partial class PasswordBoxUserControl : UserControl
    {
        public string EncryptedPassword
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, SecurityLibrary.StringCipher.Encrypt(value)); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("EncryptedPassword", typeof(string), typeof(PasswordBoxUserControl));


        public PasswordBoxUserControl()
        {
            InitializeComponent();

            PasswordBox.PasswordChanged += (sender, args) => {EncryptedPassword = ((PasswordBox)sender).Password;};

        }

    }
}
