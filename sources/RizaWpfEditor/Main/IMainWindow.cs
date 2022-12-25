﻿// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.WindowSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WPF
using System.Windows;
#endif

namespace RizaWpfEditor.Main
{
    public interface IMainWindow : IWindow
    {
        MainWindowViewModel ViewModel { get; }

#if WPF
        void SetState(WindowState state);
#endif
    }
}
