using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModel
{
    /// <summary>
    /// AUTHOR DER KLASSE: PG
    /// 
    /// Basis, die von allen ViewModel implementiert werden soll um das 
    /// Interface IViewModel und INotifyPropertyChanged automatisch zu implementieren.
    /// </summary>
    public class BaseViewModel : IViewModel , INotifyPropertyChanged
    {
        public string Name { get; set; }

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
