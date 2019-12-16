﻿using PresentationLayer.ViewModel;
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

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsView : Page, IView
    {
        public IViewModel ViewModelType { get; set; }
        public SettingsView(IViewModel viewModel)
        {
            InitializeComponent();
            ViewModelType = viewModel;
            this.DataContext = ViewModelType;
        }

        //private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    string rijndaelEncryptedPassword = SecurityLibrary.StringCipher.Encrypt(DbPassword.Password);

        //}
    }
}
