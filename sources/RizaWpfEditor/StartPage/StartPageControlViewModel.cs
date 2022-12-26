// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.ControlSystem;
using RizaEdCore.WindowSystem;
using RizaEdShare.CoreSystem;
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
                    Name = Resources.AssetBrowser,
                    ImageUri = new Uri(Resources.Icon_Library, UriKind.RelativeOrAbsolute),
                    ToolTip = Resources.AssetBrowserToolTip,
                    ClickCommand = new DelegateCommand((object p) =>
                    {
                          EditorManager.MainWindow.ViewModel.OpenAssetBrowserCommand.SafeExecute(null);
                    },
                    (object p) =>
                    {
                        return true;
                    })
                },
                new DataGridToolViewModel()
                {
                    Name = Resources.LogViewer,
                    ImageUri = new Uri(Resources.Icon_View_Desktop, UriKind.RelativeOrAbsolute),
                    ToolTip = Resources.LogViewerToolTip,
                    ClickCommand = new DelegateCommand((object p) =>
                    {
                        EditorManager.MainWindow.ViewModel.OpenLogViewerCommand.SafeExecute(null);
                    },
                    (object p) =>
                    {
                        return true;
                    })
                },
                new DataGridToolViewModel()
                {
                    Name = Resources.LookDev,
                    ImageUri = new Uri(Resources.Icon_Eye_Tracking, UriKind.RelativeOrAbsolute),
                    ToolTip = Resources.LookDevToolTip,
                     ClickCommand = new DelegateCommand((object p) =>
                    {

                    },
                    (object p) =>
                    {
                        return true;
                    })
                }
            };


        }
    }
}
