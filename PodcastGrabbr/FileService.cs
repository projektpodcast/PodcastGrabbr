using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr
{
    public class FileService : IFileService
    {
        public string FilePath { get; set; }
        public FileService()
        {
        }
        public void StartFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Textdatei (*.txt)|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                FilePath = fileDialog.FileName;

                //if (openFileDialog.ShowDialog() == true)
                //{
                //    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                //}

            //Oder:

                //using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                //{
                //    string ln;
                //    while (reader.Read() != 0)
                //    {
                //        ln = reader.ReadToEnd();
                //    }
                //}
            }
        }
    }
}
