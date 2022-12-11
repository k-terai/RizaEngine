// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RizaEdCore.NativeSystem
{
    public static class NativeLibraryManager
    {
        private static Dictionary<Guid, IntPtr> s_loadLib = new();

        public static bool LoadNativeLibrary(in DllContext context, out DllInfo info)
        {
            info = new DllInfo();

            if (File.Exists(context.FilePath) == false)
            {
                return false;
            }

            info.Id = Guid.NewGuid();
            var ptr = NativeLibrary.Load(context.FilePath);
            if (ptr == IntPtr.Zero)
            {
                return false;
            }

            s_loadLib[info.Id] = ptr;
            return true;
        }

        public static void FreeNativeLibrary(in Guid id)
        {
            if (s_loadLib.ContainsKey(id))
            {
                NativeLibrary.Free(s_loadLib[id]);
                s_loadLib.Remove(id);
            }
        }

        public static T GetNativeDelegate<T>(in Guid id)
            where T : System.Delegate
        {
            if (s_loadLib.ContainsKey(id) == false)
            {
                return null;
            }

            var export = NativeLibrary.GetExport(s_loadLib[id], typeof(T).Name);
            return (T)Marshal.GetDelegateForFunctionPointer(export, typeof(T));
        }
    }
}
