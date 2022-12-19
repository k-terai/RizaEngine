﻿// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using AvalonDock.Layout;
using RizaEdCore.WindowSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RizaWpfEditor.Panes
{
    public class PanesTemplateSelector : DataTemplateSelector
    {
        public ObservableCollection<DockingWindowViewModel> DockingWindows = new();

        public PanesTemplateSelector()
        {

        }

        public DataTemplate StartPagePaneTemplate
        {
            get;
            set;
        }

        public DataTemplate LogViewerPaneTemplate
        {
            get;
            set;
        }



        //public class AssetBrowserPaneViewModel : DockingWindowViewModel
        //{
        //    public const string ID = "4319353E-A6E3-4678-8394-B41BBAB30289";

        //    public AssetBrowserPaneViewModel() : base(DockingType.Anchorable, DockingControlType.AssetBrowser)
        //    {
        //        Title = "AssetBrowser";
        //        Name = "Asset Browser";
        //        ContentId = ID;
        //        ToolTip = Resources.AddFile;
        //        IconSource = new Uri(Resources.icons8_book, UriKind.RelativeOrAbsolute);
        //    }
        //}

        public class StartPagePaneViewModel : DockingWindowViewModel
        {
            public const string ID = "1D38BF69-2983-4B43-ABB2-993F7A9B9627";

            public StartPagePaneViewModel() : base(DockingType.Document, DockingControlType.LogViewer)
            {
                Title = "Start Page";
                Name = "Start Page";
                ContentId = ID;
                ToolTip = Resources.LogViewerToolTip;
                IconSource = new Uri(Resources.Icon_View_Desktop, UriKind.RelativeOrAbsolute);
            }
        }

        public class LogViewerPaneViewModel : DockingWindowViewModel
        {
            public const string ID = "CF09E818-618B-45DE-93FE-B8F646F2CB03";

            public LogViewerPaneViewModel() : base(DockingType.Anchorable, DockingControlType.LogViewer)
            {
                Title = "LogViewer";
                Name = "Log Viewer";
                ContentId = ID;
                ToolTip = Resources.LogViewerToolTip;
                IconSource = new Uri(Resources.Icon_View_Desktop, UriKind.RelativeOrAbsolute);
            }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is StartPagePaneViewModel)
            {
                return StartPagePaneTemplate;
            }
            if (item is LogViewerPaneViewModel)
            {
                return LogViewerPaneTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
