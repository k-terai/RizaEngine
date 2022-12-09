// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.Serialization;

namespace RizaEdCore.CoreSystem
{
    [DataContract]
    public abstract class SerializableBase : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Data serialize version.
        /// </summary>
        [DataMember]
        public uint Version { get; set; }
    }
}

