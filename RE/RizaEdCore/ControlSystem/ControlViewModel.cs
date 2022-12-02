// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.ControlSystem
{
    public class ControlViewModel : ViewModelBase
    {
        private string _title;
        private bool _enableToolBar;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        public bool EnableToolBar
        {
            get => _enableToolBar;
            set
            {
                _enableToolBar = value;
                NotifyPropertyChanged();
            }
        }

        public ControlViewModel()
        {
            Title = string.Empty;
            EnableToolBar = true;
        }
    }
}
