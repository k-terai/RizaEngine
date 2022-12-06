// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdShare.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RizaWpfEditor.Commands
{
    public static partial class EditorCommand
    {
        public static DelegateCommand ExitCommand { get; private set; }

        public static void InitCommonCommands()
        {
            ExitCommand = new DelegateCommand(

            (object p) =>
            {
                EditorManager.Shutdown();
            }
            ,
            (object p) =>
            {
                return Application.Current.MainWindow != null;
            }

            );

            AllCommands["ExitCommand"] = ExitCommand;
        }
    }
}
