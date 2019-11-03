using CommonTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MediaDataSource : LocalDataSource, IDataSource
    {
        public List<Show> GetAllShows()
        {
            string mainFolder = GetFolderName();
            DirectoryInfo mainDir = base.GetDirectoryInfo(mainFolder);
            DirectoryInfo[] subDir = GetSubdirectories(mainDir);
            Dictionary<string, List<FileInfo>> allDownloadedFiles = GetPodcastSubfolder(subDir);
            return null;
        }

        internal override string GetFolderName()
        {
            string mediaFolderName = "MediaFiles\\";
            return mediaFolderName;
        }

        private DirectoryInfo[] GetSubdirectories(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
            return subDirectories;
        }

        private Dictionary<string, List<FileInfo>> GetPodcastSubfolder(DirectoryInfo[] allDirectories)
        {
            Dictionary<string, List<FileInfo>> allEpisodesOfAllShows = new Dictionary<string, List<FileInfo>>();

            foreach (var item in allDirectories)
            {
                List<FileInfo> fileList = item.EnumerateFiles().ToList();
                allEpisodesOfAllShows.Add(item.Name, fileList);
            }
            return allEpisodesOfAllShows;
        }

    }
}
