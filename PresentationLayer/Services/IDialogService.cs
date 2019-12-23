using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    /// <summary>
    /// Veröffentlich Methoden um einen FileService per Dependency Injection zu übergeben.
    /// Die Methoden öffnen einen FileDialog.
    /// </summary>
    public interface IDialogService
    {
        string FilePath { get; set; }
        List<string> StartFileDialog();
    }
}
