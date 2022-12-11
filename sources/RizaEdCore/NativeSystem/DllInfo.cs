﻿// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.NativeSystem
{
    public struct DllInfo
    {
        public Guid Id { get; set; }

        public DllInfo()
        {
            Id = Guid.Empty;
        }
    }
}
