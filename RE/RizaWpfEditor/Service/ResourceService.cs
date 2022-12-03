// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


using RizaEdCore.CoreSystem;
using RizaWpfEditor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Service
{
    public class ResourceService : NotifyPropertyChangedBase
    {
        private static readonly ResourceService _current = new ResourceService();

        private readonly Resources _resources = new Resources();

        public static ResourceService Current
        {
            get => _current;
        }

        public Resources Resources
        {
            get { return _resources; }
        }

        public void ChangeCulture(string name)
        {
            Resources.Culture = CultureInfo.GetCultureInfo(name);
            CultureInfo.CurrentCulture = Resources.Culture;
            NotifyPropertyChanged("Resources");
        }
    }
}
