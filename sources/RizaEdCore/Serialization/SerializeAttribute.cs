// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.Serialization
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SerializeAttribute : Attribute
    {
        public enum SerializeType
        {
            Json,
            Xml
        }

        public SerializeType Type { get; private set; }

        public SerializeAttribute(SerializeType type)
        {
            Type = type;
        }
    }
}
