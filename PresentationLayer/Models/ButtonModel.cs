using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.Models
{
    /// <summary>
    /// Ermöglicht es Buttons im ViewModel dynamisch (auf Grundlage einer Collection) zu erstellen.
    /// So kann z.B. bei der Navigation von einer harten Verdrahtung abgesehen werden.
    /// </summary>
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
