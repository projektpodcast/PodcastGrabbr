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

        /// <summary>
        /// Öffnet einen FileDialog, der die Auswahl von .txt-Dateien erlaubt.
        /// Der Pfad einer ausgewählten Datei wird in der Property "FilePath" gespeichert.
        /// </summary>
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
