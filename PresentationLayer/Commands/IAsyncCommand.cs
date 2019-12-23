using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer
{
    /// <summary>
    /// Command-Klassen, die dieses Interface implementieren können aus der WPF-Oberfläche asynchron CommandParameter erhalten und weitergeben.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }
}
