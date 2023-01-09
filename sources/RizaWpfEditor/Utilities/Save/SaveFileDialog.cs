// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Utilities
{
    public sealed class SaveFileDialog : ISaveFileDialog
    {
        /// <summary>
        ///Absolute path to the most recently opened folder.
        /// </summary>
        private static string s_recentOpenDirectory = null;

        /// <summary>
        /// Show dialog.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="filter">Save file filter.</param>
        /// <returns>Save file path.</returns>
        public string ShowFileDialog(string title, string filter)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();


            if (s_recentOpenDirectory != null && Directory.Exists(s_recentOpenDirectory))
            {
                sfd.InitialDirectory = s_recentOpenDirectory;
            }
            else
            {
                sfd.InitialDirectory = @"C:\";
            }

            sfd.Filter = filter;
            sfd.Title = title;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                s_recentOpenDirectory = new FileInfo(sfd.FileName).DirectoryName;
                return sfd.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
