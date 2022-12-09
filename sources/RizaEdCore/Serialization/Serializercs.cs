// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RizaEdCore.Serialization
{
    public static class Serializer
    {
        public static bool Serialize<T>(T file, string path) where T : SerializableBase
        {
            var att = typeof(T).GetCustomAttribute<SerializeAttribute>();

            switch (att.Type)
            {
                case SerializeAttribute.SerializeType.Json:
                    return JsonSerializer.ToJson(file, path);

                case SerializeAttribute.SerializeType.Xml:
                    return false;

            }

            return false;
        }

        public static T Deserialize<T>(string path) where T : SerializableBase
        {
            var att = typeof(T).GetCustomAttribute<SerializeAttribute>();

            switch (att.Type)
            {
                case SerializeAttribute.SerializeType.Json:
                    return JsonSerializer.FromJson<T>(path);

                case SerializeAttribute.SerializeType.Xml:
                    return null;

            }

            return null;
        }
    }
}
