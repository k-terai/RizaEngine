// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RizaEdCore.CoreSystem
{
    public abstract class ViewModelBase : NotifyPropertyChangedBase, INotifyDataErrorInfo
    {
        protected Dictionary<string, IEnumerable> _errors = new Dictionary<string, IEnumerable>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Values.Any(e => e != null);

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.Values;
            }
            else
            {
                IEnumerable error = null;
                _errors.TryGetValue(propertyName, out error);
                return error;
            }
        }

        public void SetErrors(IEnumerable value, [CallerMemberName] string propertyname = "")
        {
            _errors[propertyname] = value;
            NotifyErrorsChanged(propertyname);
        }

        public void ClearError([CallerMemberName] string propertyName = "")
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                NotifyPropertyChanged("HasError");
            }
        }

        public void ClearErrors()
        {
            this._errors.Clear();
            NotifyPropertyChanged("HasError");
        }

        public void NotifyErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}

