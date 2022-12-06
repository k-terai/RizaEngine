// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.ProjectSystem;
using RizaWpfEditor.ProjectWizard;
using RizaWpfEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace RizaWpfEditor
{
    public static class EditorManager
    {
        /// <summary>
        /// Editor startup.
        /// </summary>
        /// <param name="projectpath">.project file path</param>
        /// <returns>Return true if startup successed.</returns>
        public static bool Startup(string projectpath)
        {
            RizaEdCore.LogSystem.Debug.OnException += (Exception e) =>
            {
                MessageBox.Show(e.Message, e.StackTrace, MessageBoxButton.OK, MessageBoxImage.Error);
            };

            if (Project.Load(projectpath) == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Editor shutdown.
        /// </summary>
        /// <returns>Return true if shutdown successed.</returns>
        public static bool Shutdown()
        {
            Application.Current.Shutdown();
            return true;
        }

        public static void Restart(string projectpath)
        {
            var asm = Assembly.GetEntryAssembly();
            var exes = new FileInfo(asm.Location).Directory.GetFiles("*.exe", SearchOption.TopDirectoryOnly);

            if (exes.Length != 0)
            {
                Process.Start(exes[0].FullName, projectpath);
                Application.Current.Shutdown();
            }
        }

        public static IProjectWizard CreateProjectWizard()
        {
            return new ProjectWizardWindow();
        }

        public static ISelectExternalFileWindow CreateSelectExternalFileWindow()
        {
            return new SelectExternalFileWindow();
        }

        public static ISelectExternalFolderWindow CreateSelectExternalFolderWindow()
        {
            return new SelectExternalFolderWindow();
        }

        public static ISaveFileDialog CreateSaveFileDialog()
        {
            return new SaveFileDialog();
        }

    }
}
