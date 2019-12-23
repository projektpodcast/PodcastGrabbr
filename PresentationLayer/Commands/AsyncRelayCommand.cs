using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer
{
    /// <summary>
    /// RelayCommand werden in ViewModels verwendet. Erlaubt es Buttons in der View an einen ausdefinierten ViewModel ICommand zu binden.
    /// AsyncRelayCommand ermöglicht es dabei diese Commands asynchron auszufüren, um zu verhindern, dass bei zeitaufwändigen
    /// Operation die UI einfriert.
    /// </summary>
    public class AsyncRelayCommand<T> : IAsyncCommand<T>
    {

        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute(T parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).FireAndForgetSafeAsync(_errorHandler);
        }
       
    }


}

