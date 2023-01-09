// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaWpfEditor.Main;
using RizaWpfEditor.Projects;
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

namespace RizaWpfEditor
{
    public static class EditorManager
    {
        public const string MAIN_WINDOW_URI_PATH = "Main/MainWindow.xaml";

        public static IMainWindow MainWindow
        {
            get
            {
                return Application.Current.MainWindow as IMainWindow;
            }
        }

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

            if (RizaEdCore.ProjectSystem.Project.Load(projectpath) == null)
            {
                return false;
            }

            //if (!AssetDatabase.Startup())
            //{
            //    return false;
            //}

            //AssetDatabase.RegisterIconUrl<RootFolder>(new Uri(Resources.Icon_Library, UriKind.RelativeOrAbsolute));
            //AssetDatabase.RegisterIconUrl<NormalFolder>(new Uri(Resources.Icon_Folder, UriKind.RelativeOrAbsolute));
            //AssetDatabase.RegisterIconUrl<Texture>(new Uri(Resources.Icon_Image, UriKind.RelativeOrAbsolute));

            //if (!AssetDatabase.Initialize())
            //{
            //    return false;
            //}

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

        public static IProjectWindow CreateProjectWindow()
        {
            return new ProjectWindow();
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
