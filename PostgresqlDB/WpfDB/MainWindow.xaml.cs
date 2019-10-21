using System;
using System.Collections.Generic;
using System.Data;
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
using Npgsql;

namespace WpfDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // check connection 
            DBConnection myConnection = new DBConnection();


            //Boolean x = myConexion.Connect();
            //Console.WriteLine(x);
            // Requests req = new Requests();

            string dbName = "podcast";
            // check if the DB exist
            Boolean dbExist = myConnection.CheckDatenBank(dbName);
            if (dbExist == false)
            {
                myConnection.Createdb(dbName);
                Console.WriteLine("The db cas created");
            }
            else
            {
                Console.WriteLine("The db ist already to use");
            }



            // Insert Values
            for (int i = 0; i < 4; i++)
            {
                //insert values
                //myConnection.InsertValuesShows(dbName);
            }

            DataSet values = new DataSet();
            // show values
            values = myConnection.DownloadValues(dbName);

            //gridView.ItemsSource = ds.Tables["Table"].AsEnumerable();

            dbDatagrid.ItemsSource = values.Tables;

        }
    }
}
