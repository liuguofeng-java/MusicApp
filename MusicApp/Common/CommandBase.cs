using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MusicApp.Common
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return DoCanExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            DoExecute?.Invoke(parameter);
        }

        public Action<object> DoExecute { get; set; }
        public Func<object,bool> DoCanExecute { get; set; }
    }
}
