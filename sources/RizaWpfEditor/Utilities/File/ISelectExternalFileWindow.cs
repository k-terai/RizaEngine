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
    public interface ISelectExternalFileWindow : IWindow
    {
        /// <summary>
        /// Show window.
        /// </summary>
        /// <param name="description">Window description.</param>
        /// <param name="filter">Select filter.</param>
        /// <param name="multiselect">True = Multi file select enable.</param>
        /// <returns>Select file list.</returns>
        List<string> ShowWindow(string description, string filter, bool multiselect);
    }
}
