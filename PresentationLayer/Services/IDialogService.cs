using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public interface IDialogService
    {
        string FilePath { get; set; }
        void StartFileDialog();
    }
}
