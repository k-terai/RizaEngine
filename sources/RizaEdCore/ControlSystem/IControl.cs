// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;


#if WPF
using System.Windows.Controls;
#endif

namespace RizaEdShare.ControlSystem
{
    public interface IControl
    {
#if WPF
        UserControl Control { get; }
#endif
    }
}
