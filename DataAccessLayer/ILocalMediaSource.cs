using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ILocalMediaSource
    {
        void PlayLocalMedia(Episode episode);
    }
}
