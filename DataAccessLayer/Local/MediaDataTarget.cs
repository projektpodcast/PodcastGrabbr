using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using LocalStorage;

namespace DataAccessLayer
{
    public class MediaDataTarget : ILocalMediaTarget
    {
        public async Task DownloadEpisode(Show show, Episode episode)
        {
            MediaStorage mediaDl = new MediaStorage();
            await mediaDl.InitializeMediaDownload(show, episode);
        }

    }
}
