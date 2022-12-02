// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using RizaEdShare.ControlSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.WindowSystem
{
    public class TabViewModel : ViewModelBase
    {
        private string _name;
        private Uri _iconUri;
        private IControl _content;

        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }
        public Uri IconUri { get => _iconUri; set { _iconUri = value; NotifyPropertyChanged(); } }

        public IControl Content { get => _content; set { _content = value; NotifyPropertyChanged(); } }
    }
}
