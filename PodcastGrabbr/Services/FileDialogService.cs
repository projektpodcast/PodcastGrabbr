using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.Services
{
    public class FileDialogService : IDialogService
    {
        public string FilePath { get; set; }
        public FileDialogService()
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

            }
        }
    }
}
