// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.WindowSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Utilities
{
    public interface IExplorerWindow : IWindow
    {
        /// <summary>
        /// Show explorer window.
        /// </summary>
        /// <param name="path">File or directory absolute path.</param>
        void ShowWindow(string path);
    }
}
