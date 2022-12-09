// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static RizaEdCore.CoreSystem.EditorCommon;

namespace RizaEdCore.LogSystem
{
    public sealed class Log : NotifyPropertyChangedBase
    {
        private static List<Log> s_activeLogs = new();
        private LogData _data;
        private ObservableCollection<LogData.LogPack> _displayLogs;
        private LogType _displayLogType;

        public ObservableCollection<LogData.LogPack> DisplayLogs { get => _displayLogs; set { _displayLogs = value; NotifyPropertyChanged(); } }
        public int InfoCount { get => _data.Informations.Count; }
        public int WarningCount { get => _data.Warnings.Count; }
        public int ErrorCount { get => _data.Errors.Count; }
        public int AllLogCount { get => InfoCount + WarningCount + ErrorCount; }
        public LogType DisplayLogType { get => _displayLogType; set { _displayLogType = value; NotifyPropertyChanged(); } }

        public bool IsEnableInfomation { get => DisplayLogType.HasFlag(LogType.Information); }

        public bool IsEnableWarning { get => DisplayLogType.HasFlag(LogType.Warning); }
        public bool IsEnableError { get => DisplayLogType.HasFlag(LogType.Error); }

        public void ChangeDisplay(LogType displayLog)
        {
            DisplayLogType = displayLog;
            DisplayLogs.Clear();

            if (DisplayLogType.HasFlag(LogType.Information))
            {
                foreach (var d in _data.Informations)
                {
                    DisplayLogs.Add(d);
                }
            }

            if (DisplayLogType.HasFlag(LogType.Warning))
            {
                foreach (var d in _data.Warnings)
                {
                    DisplayLogs.Add(d);
                }
            }

            if (DisplayLogType.HasFlag(LogType.Error))
            {
                foreach (var d in _data.Errors)
                {
                    DisplayLogs.Add(d);
                }
            }

            NotifyCommonProperties();

        }

        public void Add(LogData.LogPack pack)
        {
            switch (pack.Type)
            {
                case LogType.Information:
                    _data.Informations.Add(pack);
                    break;
                case LogType.Warning:
                    _data.Warnings.Add(pack);
                    break;
                case LogType.Error:
                    _data.Errors.Add(pack);
                    break;
            }

            if (DisplayLogType.HasFlag(pack.Type))
            {
                DisplayLogs.Add(pack);
            }

            NotifyCommonProperties();
        }

        public void Clear()
        {
            DisplayLogs.Clear();
            _data.Informations.Clear();
            _data.Warnings.Clear();
            _data.Errors.Clear();
            NotifyCommonProperties();
        }

        public bool Save(string filePath)
        {
            try
            {
                return Serialization.Serializer.Serialize(_data, filePath);
            }
            catch (IOException exception)
            {
                Debug.LogException(exception);
                return false;
            }
        }

        public void Load(string filePath)
        {
            try
            {
                _data = Serialization.Serializer.Deserialize<LogData>(filePath);
                ChangeDisplay(DisplayLogType);
            }
            catch (IOException exception)
            {
                Debug.LogException(exception);
            }
        }

        public static Log Create()
        {
            var l = new Log(new LogData());

            Debug.OnLog += l.OnLog;
            Debug.OnWarning += l.OnWarning;
            Debug.OnError += l.OnError;

            s_activeLogs.Add(l);
            return l;
        }

        public static void Destroy(Log log)
        {
            if (s_activeLogs.Contains(log))
            {
                Debug.OnLog -= log.OnLog;
                Debug.OnWarning -= log.OnWarning;
                Debug.OnError -= log.OnError;
                s_activeLogs.Remove(log);
            }
        }

        private void OnLog(string message, string from)
        {
            Add(new LogData.LogPack()
            {
                From = from,
                Message = message,
                Type = LogType.Information,
                Time = DateTime.Now
            });
        }

        private void OnWarning(string message, string from)
        {
            Add(new LogData.LogPack()
            {
                From = from,
                Message = message,
                Type = LogType.Warning,
                Time = DateTime.Now
            });
        }

        private void OnError(string message, string from)
        {
            Add(new LogData.LogPack()
            {
                From = from,
                Message = message,
                Type = LogType.Error,
                Time = DateTime.Now
            });
        }

        private Log(LogData data) : base()
        {
            _data = data;
            DisplayLogs = new ObservableCollection<LogData.LogPack>();
            ChangeDisplay(LogType.All);
        }

        private void NotifyCommonProperties()
        {
            NotifyPropertyChanged("InfoCount");
            NotifyPropertyChanged("WarningCount");
            NotifyPropertyChanged("ErrorCount");
            NotifyPropertyChanged("IsEnableInfomation");
            NotifyPropertyChanged("IsEnableWarning");
            NotifyPropertyChanged("IsEnableError");
        }
    }
}
