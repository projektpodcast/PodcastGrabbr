using PodcastGrabbr.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.View
{
    public interface IView
    {
        IViewModel ViewModelType { get; set; }
    }
}
