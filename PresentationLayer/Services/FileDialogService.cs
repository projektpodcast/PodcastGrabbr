using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    /// <summary>
    /// Bietet Methoden um einen FileDialog in einem ViewModel zu öffnen.
    /// </summary>
    public class FileDialogService : IDialogService
    {
        /// <summary>
        /// Wird gesetzt, wenn im FileDialog eine Datei ausgewählt wird.
        /// </summary>
        public string FilePath { get; set; }
        public List<string> UriList { get; set; }

        /// <summary>
        /// Öffnet einen FileDialog, der die Auswahl von .txt-Dateien erlaubt.
        /// Der Pfad einer ausgewählten Datei wird in der Property "FilePath" gespeichert.
        /// </summary>
        public List<string> StartFileDialog()
        {
            UriList = new List<string>();
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Textdatei (*.txt)|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                FilePath = fileDialog.FileName;
                ReadLineOfFile();
            }
            return UriList;
        }

        public void ReadLineOfFile()
        {
            string line = string.Empty;
            StreamReader sReader = new StreamReader(FilePath);
            while ((line = sReader.ReadLine()) != null)
            {
                if (CheckIfValidUrl(line))
                {
                    UriList.Add(line);
                }
            }
        }

        public bool CheckIfValidUrl(string feedUri)
        {
            Uri uriResult;
            return Uri.TryCreate(feedUri, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
