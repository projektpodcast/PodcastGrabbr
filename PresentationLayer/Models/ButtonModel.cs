using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.Models
{
    public class ButtonModel
    {
        public string BtnContent { get; set; }
        public ICommand BtnCommand { get; set; }

        public ButtonModel(string _content, ICommand _command)
        {
            this.BtnContent = _content;
            this.BtnCommand = _command;
        }
    }
}
