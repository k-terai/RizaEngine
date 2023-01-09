// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdShare.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.Commands
{
    public static partial class EditorCommand
    {
        public static DelegateCommand OpenProjectCommand { get; private set; }
        public static DelegateCommand OpenProjectWindowCommand { get; private set; }

        private static void InitProjectCommands()
        {
            OpenProjectCommand = new DelegateCommand(

            (object p) =>
            {
                var window = EditorManager.CreateSelectExternalFileWindow();
                var path = window.ShowWindow("Please select open project file.", "Project File(*.project) |*.project", false);
                if (path != null && path.Count != 0)
                {
                    EditorManager.Restart(path[0]);
                }
            }
            ,
            (object p) =>
            {
                return true;
            }

            );

            OpenProjectWindowCommand = new DelegateCommand(

            (object p) =>
            {
                var window = EditorManager.CreateProjectWindow();
                window.ShowWindow();
            }
            ,
            (object p) =>
            {
                return true;
            }

            );

            AllCommands["OpenProjectCommand"] = OpenProjectCommand;
            AllCommands["OpenProjectWizardCommand"] = OpenProjectWindowCommand;
        }
    }
}
