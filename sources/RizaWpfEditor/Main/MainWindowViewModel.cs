// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.WindowSystem;
using RizaWpfEditor.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Main
{
    public class MainWindowViewModel : WindowViewModel
    {
        public MainWindowViewModel() : base()
        {
            EnableToolBar = true;
            //IconUri = new Uri(Resources.icon_app, UriKind.RelativeOrAbsolute);
            MinimizeImageUri = ResourceService.Current.GetFluentIconUri(Resources.Icon_Arrow_Minimize_Vertical_Custom);
            MaximizeImageUri = ResourceService.Current.GetFluentIconUri(Resources.Icon_FullScreen_Maximize);
            RestoreImageUri = ResourceService.Current.GetFluentIconUri(Resources.Icon_FullScreen_Minimize);
            CloseImageUri = ResourceService.Current.GetFluentIconUri(Resources.Icon_Dismiss);
            Title = "None";

        }
    }
}
