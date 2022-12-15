// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.WindowSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RizaWpfEditor.Panes
{
    public class PanesStyleSelector : StyleSelector
    {
        public Style AnchorableStyle
        {
            get;
            set;
        }

        public Style DocumentStyle
        {
            get;
            set;
        }

        public override Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is DockingWindowViewModel docking)
            {
                switch (docking.Type)
                {
                    case DockingWindowViewModel.DockingType.Anchorable: return AnchorableStyle;
                    case DockingWindowViewModel.DockingType.Document: return DocumentStyle;
                }
            }

            return base.SelectStyle(item, container);
        }
    }
}
