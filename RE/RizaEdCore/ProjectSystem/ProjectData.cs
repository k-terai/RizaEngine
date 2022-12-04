// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using RizaEdCore.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RizaEdCore.ProjectSystem
{
    [DataContract]
    [Serialize(SerializeAttribute.SerializeType.Json)]
    public sealed class ProjectData : SerializableBase
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string EngineVersion { get; set; }

        public ProjectData() : base()
        {
            Id = Guid.NewGuid();
            Version = 1;
        }

    }
}
