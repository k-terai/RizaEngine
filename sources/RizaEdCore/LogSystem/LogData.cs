// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using static RizaEdCore.CoreSystem.EditorCommon;

namespace RizaEdCore.LogSystem
{
    [DataContract]
    [Serialization.Serialize(Serialization.SerializeAttribute.SerializeType.Json)]
    public sealed class LogData : SerializableBase
    {
        [DataContract]
        public class LogPack : NotifyPropertyChangedBase
        {
            private string _message;

            [DataMember]
            public LogType Type { get; set; }

            [DataMember]
            public string Message { get => _message; set { _message = value; NotifyPropertyChanged(); } }

            [DataMember]
            public string From { get; set; }

            [DataMember]
            public DateTime Time { get; set; }
        }

        [DataMember]
        public List<LogPack> Informations { get; set; }

        [DataMember]
        public List<LogPack> Warnings { get; set; }

        [DataMember]
        public List<LogPack> Errors { get; set; }

        public LogData() : base()
        {
            Version = 1;
            Informations = new List<LogPack>();
            Warnings = new List<LogPack>();
            Errors = new List<LogPack>();
        }

    }
}
