// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RizaEdCore.AssetSystem
{
    [DataContract]
    [Serialization.Serialize(Serialization.SerializeAttribute.SerializeType.Json)]
    public sealed class AssetMetaData : SerializableBase
    {
        [DataContract]
        public record TextureData
        {
            [DataMember]
            public double Width { get; set; }

            [DataMember]
            public double Height { get; set; }
        }

        /// <summary>
        /// Asset unique id.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Asset convert type.(Ex. texture id .dds format)
        /// </summary>
        [DataMember]
        public string ConvertType { get; set; }

        /// <summary>
        /// Texture data.
        /// </summary>
        [DataMember]
        public TextureData Texture { get; set; }

        public AssetMetaData()
        {
            Version = 1;
        }

    }
}
