using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Definiert Methoden, welche benötigt werden um Medien-Dateien abzuspielen 
    /// </summary>
    public interface ILocalMediaSource
    {
        /// <summary>
        /// Methode ruft eine Methode auf um die übergebene Methode auf dem Gerät abzuspielen.
        /// </summary>
        /// <param name="episode">Enthält den zur Episode gehörigen Pfad, welcher abgespielt werden soll</param>
        void PlayLocalMedia(Episode episode);
    }
}
