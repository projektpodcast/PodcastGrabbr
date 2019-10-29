using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastGrabbr.Model
{
    public class EpisodeModel : Episode, INotifyPropertyChanged
    {
        public string _title;

        public override string Title
        {
            get { return _title; }
            set { _title = value;/* OnPropertyChanged("Title");*/ }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
