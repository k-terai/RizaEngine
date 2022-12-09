// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace RizaEdShare.CoreSystem
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

#if WPF
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
#else
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
#endif

        public DelegateCommand(Action<object> execute) : this(execute, null)
        {

        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public bool SafeExecute(in object canExecuteParametor = null, in object executeParametor = null)
        {
            if (CanExecute(canExecuteParametor))
            {
                Execute(executeParametor);
                return true;
            }

            return false;
        }

        public void RaiseCanExecuteChanged(object parametor)
        {
            CanExecute(parametor);
        }
    }
}