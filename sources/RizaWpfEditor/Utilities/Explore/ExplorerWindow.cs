// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Utilities
{
    public sealed class ExplorerWindow : IExplorerWindow
    {
        public void ShowWindow(string path)
        {
            System.Diagnostics.Process.Start(EditorConsts.EXPLORER, path);
        }

        public void ShowWindow()
        {
            ShowWindow(Environment.CurrentDirectory);
        }
    }
}
