using System;
using System.Windows.Input;

namespace simulation_app.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _exec;
        private readonly Func<bool> _can;

        public RelayCommand(Action exec, Func<bool> can = null)
        {
            _exec = exec;
            _can = can;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return _can == null ? true : _can(); }
        public void Execute(object parameter) { _exec(); }
        public void RaiseCanExecuteChanged()
        {
            var h = CanExecuteChanged; if (h != null) h(this, EventArgs.Empty);
        }
    }
}
