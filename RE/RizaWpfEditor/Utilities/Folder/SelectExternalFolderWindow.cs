// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RizaWpfEditor.Utilities
{
    public sealed class SelectExternalFolderWindow : ISelectExternalFolderWindow
    {
        /// <summary>
        /// Show window.
        /// </summary>
        /// <param name="description">Window title.</param>
        /// <returns>Select folder path.</returns>
        public string ShowWindow(string description)
        {
            FolderBrowserDialog fbd;

            using (fbd = new FolderBrowserDialog())
            {
                fbd.Description = description;
                var r = fbd.ShowDialog();

                if (r == DialogResult.OK)
                {
                    return fbd.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Show window.
        /// </summary>
        /// <param name="description">Window title.</param>
        /// <param name="selectedpath">Specify the first folder to select.</param>
        /// <param name="shownewfolderbutton">True = allow user to create new folder</param>
        /// <returns>Select folder path.</returns>
        public string ShowWindow(string description, string selectedpath, bool shownewfolderbutton)
        {
            FolderBrowserDialog fbd;

            using (fbd = new FolderBrowserDialog())
            {
                fbd.Description = description;
                //fbd.RootFolder = System.Environment.SpecialFolder.CommonDocuments;
                fbd.SelectedPath = selectedpath;
                fbd.ShowNewFolderButton = shownewfolderbutton;
                var r = fbd.ShowDialog();

                if (r == DialogResult.OK)
                {
                    return fbd.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void ShowWindow()
        {
            ShowWindow("default", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), false);
        }
    }
}
