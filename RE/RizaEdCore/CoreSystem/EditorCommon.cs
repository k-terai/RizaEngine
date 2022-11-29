// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace RizaEdCore.CoreSystem
{
    public static class EditorCommon
    {
        /// <summary>
        /// Define all editor results that should be check.
        /// This enum is only define editor results such as folder path missing,not include runtime.
        /// </summary>
        public enum Result
        {
            OK = 0,                         // Success
            ERROR_PROJECTNAME_MIN,          // Project name error.A character has not been entered.
            ERROR_PROJECTNAME_MAX,          // Project name error.name is too long.
            ERROR_PROJECTNAME_INVALID,      // Project name error.Invalid character in project name.
            ERROR_PROJECTNAME_DOUBLEBYTE,   // Project name error.Contains double-byte characters.
            ERROR_PROJECTNAME_PATH_EXISTS,  // Path error.A folder with that name already exists at that location.
            ERROR_PROJECTPATH_NOT_EXISTS,   // Location error.A folder with that name not exists.
            ERROR_ASSETNAME_MIN,            // Asset name error.A character has not been entered.
            ERROR_ASSETNAME_MAX,            // Asset name error.name is too long.
            ERROR_ASSETNAME_INVALID,        // Asset name error.Invalid character in asset name.
            ERROR_ASSETNAME_DOUBLEBYTE,     // Asset name error. Contains double-byte characters.
            ERROR_ASSETNAME_SAMENAME        // Asset name error. Same name asset already exists.
        }

        /// <summary>
        /// Define all editor and engine log types.
        /// </summary>
        [System.Flags]
        public enum LogType
        {
            None = 0x01 << 1,
            Information = 0x01 << 2,
            Warning = 0x01 << 3,
            Error = 0x01 << 4,
            All = None | Information | Warning | Error
        }

        /// <summary>
        /// Define all remote type.
        /// </summary>
        public enum RemoteType
        {
            Managed, // Managed mode
            Native   // Native mode
        }
    }
}

