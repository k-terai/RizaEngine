// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.WindowSystem;
using RizaEdShare.CoreSystem;
using RizaWpfEditor.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Main
{
    public class MainWindowViewModel : WindowViewModel
    {
        private DockingWindowViewModel _activeContent;
        private ObservableCollection<DockingWindowViewModel> _dockingWindows;
        private ObservableCollection<DockingWindowViewModel> _anchorables;
        private ObservableCollection<DockingWindowViewModel> _documents;

        public ObservableCollection<DockingWindowViewModel> DockingWindows { get => _dockingWindows; set { _dockingWindows = value; NotifyPropertyChanged(); } }

        public ObservableCollection<DockingWindowViewModel> Anchorables { get => _anchorables; set { _anchorables = value; NotifyPropertyChanged(); } }

        public ObservableCollection<DockingWindowViewModel> Documents { get => _documents; set { _documents = value; NotifyPropertyChanged(); } }

        public DockingWindowViewModel ActiveContent { get => _activeContent; set { _activeContent = value; NotifyPropertyChanged(); } }

        public DelegateCommand OpenAssetBrowserCommand { get; set; }
        public DelegateCommand OpenLogViewerCommand { get; set; }

        public MainWindowViewModel() : base()
        {
            EnableToolBar = true;
            //IconUri = new Uri(Resources.icon_app, UriKind.RelativeOrAbsolute);
            MinimizeImageUri = new Uri(Resources.Icon_Arrow_Minimize_Vertical_Custom, UriKind.RelativeOrAbsolute);
            MaximizeImageUri = new Uri(Resources.Icon_FullScreenMaximize, UriKind.RelativeOrAbsolute);
            RestoreImageUri = new Uri(Resources.Icon_FullScreenMinimize, UriKind.RelativeOrAbsolute);
            CloseImageUri = new Uri(Resources.Icon_Dismiss, UriKind.RelativeOrAbsolute);
            Title = "None";
            DockingWindows = new();
            Anchorables = new();
            Documents = new();
            InitCommands();

            ResourceService.Current.ChangeCulture("en-US");
        }

        private void InitCommands()
        {
          //  OpenAssetBrowserCommand = new DelegateCommand((object p) =>
          //  {
          //      OpenToolCommand<PanesTemplateSelector.AssetBrowserPaneViewModel>();
          //  },
          // (object p) =>
          // {
          //     return true;
          // });

          //  OpenLogViewerCommand = new DelegateCommand((object p) =>
          //  {
          //      OpenToolCommand<PanesTemplateSelector.LogViewerPaneViewModel>();
          //  },
          //(object p) =>
          //{
          //    return true;
          //});
        }

        private void OpenToolCommand<T>()
            where T : DockingWindowViewModel, new()
        {
            var tool = DockingWindows.FirstOrDefault(t => t is T);

            if (tool == null)
            {
                tool = new T();

                switch (tool.Type)
                {
                    case DockingWindowViewModel.DockingType.Anchorable:
                        {
                            tool.CloseCommand = new DelegateCommand((object p) =>
                            {
                                DockingWindows.Remove(tool);
                                Anchorables.Remove(tool);
                            });
                            Anchorables.Add(tool);
                        }
                        break;
                    case DockingWindowViewModel.DockingType.Document:
                        {
                            tool.CloseCommand = new DelegateCommand((object p) =>
                            {
                                DockingWindows.Remove(tool);
                                Documents.Remove(tool);
                            });
                            Documents.Add(tool);
                        }
                        break;
                }

                DockingWindows.Add(tool);
            }

            tool.IsSelected = true;
            ActiveContent = tool;
        }
    }
}
