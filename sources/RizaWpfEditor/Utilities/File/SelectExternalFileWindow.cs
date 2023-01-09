// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RizaWpfEditor.Utilities
{
    public sealed class SelectExternalFileWindow : ISelectExternalFileWindow
    {
        /// <summary>
        /// Absolute path to the most recently opened folder.
        /// </summary>
        private static string s_recentOpenDirectory = null;

        /// <summary>
        /// Show window.
        /// </summary>
        /// <param name="description">Window description.</param>
        /// <param name="filter">Select filter.</param>
        /// <param name="multiselect">True = Multi file select enable.</param>
        /// <returns>Select file list.</returns>
        public List<string> ShowWindow(string description, string filter, bool multiselect)
        {
            OpenFileDialog Dialog = null;
            using (Dialog = new OpenFileDialog())
            {
                if (s_recentOpenDirectory != null && Directory.Exists(s_recentOpenDirectory))
                {
                    Dialog.InitialDirectory = s_recentOpenDirectory;
                }
                else
                {
                    Dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                Dialog.Multiselect = multiselect;
                Dialog.Title = description;
                Dialog.Filter = filter;
                var result = Dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var list = new List<string>();
                    foreach (var s in Dialog.FileNames)
                    {
                        list.Add(s);
                    }

                    s_recentOpenDirectory = new FileInfo(Dialog.FileName).DirectoryName;
                    return list;

                }
                else
                {
                    return null;
                }
            }
        }

        public void ShowWindow()
        {
            ShowWindow("Default", "*", false);
        }
    }
}
