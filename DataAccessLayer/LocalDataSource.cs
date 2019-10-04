using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class LocalDataSource
    {
        internal abstract string GetFolderName();

        internal protected DirectoryInfo GetDirectoryInfo(string folderName)
        {
            FileDataGeneral fileMethods = new FileDataGeneral();
            DirectoryInfo dirInfo = fileMethods.GetFilePath(folderName);
            return dirInfo;
        }

    }
}
