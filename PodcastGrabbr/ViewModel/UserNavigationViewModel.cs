using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PodcastGrabbr.ViewModel
{
    public class UserNavigationViewModel : BaseViewModel
    {
        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visible { get { return _visibility; } set { _visibility = value; OnPropertyChanged("Visible") ; } }
        private ICommand _doSomething;
        public ICommand DoSomethingCommand
        {
            get
            {
                if (_doSomething == null)
                {
                    _doSomething = new RelayCommand(
                        p => true,
                        p => this.DoSomeImportantMethod());
                }
                return _doSomething;
            }
        }

        //private bool _canDoSomething { get; set; }
        ////public bool CanDoSomething { get { return true; } }
        //public bool CanDoSomething
        //{
        //    get { return _canDoSomething; }
        //    set {; }
        //}

        public void DoSomeImportantMethod()
        {
            if (Visible == Visibility.Collapsed)
            {
                this.Visible = Visibility.Visible;
            }
            else
            {
                this.Visible = Visibility.Collapsed;
            }

        }
    }
}
