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
        public static Dictionary<string, DelegateCommand> AllCommands { get; private set; } = new Dictionary<string, DelegateCommand>();

        public static void Initialize()
        {
            InitCommonCommands();
            InitProjectCommands();
        }
    }
}
