// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.ControlSystem;
using RizaEdCore.WindowSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.StartPage
{
    public class StartPageControlViewModel : ControlViewModel
    {
        private ObservableCollection<DataGridToolViewModel> _tools;

        public ObservableCollection<DataGridToolViewModel> Tools { get => _tools; set { _tools = value; NotifyPropertyChanged(); } }

        public StartPageControlViewModel() : base()
        {
            Tools = new ObservableCollection<DataGridToolViewModel>
            {
                new DataGridToolViewModel()
                {
                    Name = Resources.LogViewer,
                    ImageUri = new Uri(Resources.Icon_View_Desktop, UriKind.RelativeOrAbsolute),
                    ToolTip = Resources.LogViewerToolTip
                }
            };


        }
    }
}
