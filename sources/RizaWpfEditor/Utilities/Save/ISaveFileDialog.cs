// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Utilities
{
    public interface ISaveFileDialog
    {
        /// <summary>
        /// Show dialog.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="filter">Save file filter.</param>
        /// <returns>Save file path.</returns>
        string ShowFileDialog(string title, string filter);
    }
}
