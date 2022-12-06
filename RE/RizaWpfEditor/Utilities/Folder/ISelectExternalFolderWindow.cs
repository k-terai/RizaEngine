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
    public interface ISelectExternalFolderWindow : IWindow
    {
        /// <summary>
        /// Show window.
        /// </summary>
        /// <param name="description">Window title.</param>
        /// <returns>Select folder path.</returns>
        string ShowWindow(string description);

        /// <summary>
        /// Show window.
        /// </summary>
        /// <param name="description">Window title.</param>
        /// <param name="selectedpath">Specify the first folder to select.</param>
        /// <param name="shownewfolderbutton">True = allow user to create new folder</param>
        /// <returns>Select folder path.</returns>
        string ShowWindow(string description, string selectedpath, bool shownewfolderbutton);
    }
}
