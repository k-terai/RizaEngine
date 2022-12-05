// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.LogSystem
{
    public partial class Debug
    {
        public static event Action<string, string> OnLog;
        public static event Action<string, string> OnWarning;
        public static event Action<string, string> OnError;

        public static event Action<Exception> OnException;

        public static void Log(object message, object from = null)
        {
            OnLog?.Invoke(message?.ToString(), from?.ToString());
        }

        public static void LogWarning(object message, object from = null)
        {
            OnWarning?.Invoke(message?.ToString(), from?.ToString());
        }

        public static void LogError(object message, object from = null)
        {
            OnError?.Invoke(message?.ToString(), from?.ToString());
        }

        public static void LogException(Exception exception)
        {
            OnException?.Invoke(exception);
        }

        public static void Assert(bool condition)
        {
            System.Diagnostics.Debug.Assert(condition);
        }
    }
}
