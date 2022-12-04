// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RizaEdCore.Serialization
{
    public static class JsonSerializer
    {
        public static bool ToJson<Type>(Type file, string path)
           where Type : class
        {
            try
            {
                using (StreamWriter stream = new StreamWriter(new FileStream(path, FileMode.Create)))
                {
                    stream.Write(System.Text.Json.JsonSerializer.Serialize(file));
                }
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        public static Type FromJson<Type>(string path)
            where Type : class
        {
            try
            {
                using (StreamReader stream = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Type>(stream.ReadToEnd());
                }
            }
            catch (Exception exception)
            {
                return null;
            }

        }

    }
}
