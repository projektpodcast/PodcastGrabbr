using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class FileDataGeneral
    {
        internal DirectoryInfo GetFilePath()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string xmlFolderName = "Xml\\";
            string folderPathToCreate = filePath + xmlFolderName;
            DirectoryInfo fileDirectory = Directory.CreateDirectory(folderPathToCreate);
            return fileDirectory;
        }
    }
}
