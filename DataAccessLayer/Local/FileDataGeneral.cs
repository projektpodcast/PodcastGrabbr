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
        internal DirectoryInfo GetFilePath(string folderName)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string folderPathToCreate = filePath + folderName;
            DirectoryInfo fileDirectory = Directory.CreateDirectory(folderPathToCreate);
            return fileDirectory;
        }
    }
}
