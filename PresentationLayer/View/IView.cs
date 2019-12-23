using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.View
{
    /// AUTHOR DER KLASSE: PG
    /// 
    public interface IView
    {
        IViewModel ViewModelType { get; set; }
    }
}
