using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Eine Gruppierung an Informationen, die eine downloadbare Datei beschreiben
    /// </summary>
    public class FileInformation
    {
        public int Length { get; set; }
        public string FileType { get; set; }
        public string SourceUri { get; set; }

        public FileInformation()
        {
        }

        public FileInformation(string _sourceUri, int _length, string _fileType)
        {
            this.SourceUri = _sourceUri;
            this.Length = _length;
            this.FileType = _fileType;
        }
    }
}
