// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.ControlSystem;
using RizaEdCore.CoreSystem;
using RizaEdCore.LogSystem;
using RizaEdShare.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.LogViewer
{
    public class LogViewerControlViewModel : ControlViewModel
    {
        private Log _targetLog;
        private string _commandInput;

        public Log TargetLog { get { return _targetLog; } set { _targetLog = value; NotifyPropertyChanged(); } }

        public DelegateCommand ToggleDisplayLogCommand { get; set; }
        public DelegateCommand ClearLogCommand { get; set; }

        public DelegateCommand PlayConsoleCommand { get; set; }

        public string CommandInput { get => _commandInput; set { _commandInput = value; NotifyPropertyChanged(); } }

        public LogViewerControlViewModel() : base()
        {
            TargetLog = Log.Create();
            InitCommands();
        }

        private void InitCommands()
        {
            ToggleDisplayLogCommand = new DelegateCommand(

                (object p) =>
                {
                    var flag = (EditorCommon.LogType)(p);
                    var current = TargetLog.DisplayLogType;

                    if (TargetLog.DisplayLogType.HasFlag(flag))
                    {
                        TargetLog.ChangeDisplay(current & ~flag);
                    }
                    else
                    {
                        TargetLog.ChangeDisplay(current | flag);
                    }

                }
                ,
                (object p) =>
                {
                    return TargetLog != null;
                }
                );

            ClearLogCommand = new DelegateCommand(

              (object p) =>
              {
                  TargetLog.Clear();
              }
              ,
              (object p) =>
              {
                  return TargetLog != null;
              }
              );


            PlayConsoleCommand = new DelegateCommand(

              (object p) =>
              {

              }
              ,
              (object p) =>
              {
                  return true;
              }
              );
        }

        private void GenerateTestLog()
        {
            for (int i = 0; i < 100; i++)
            {
                Debug.Log(string.Format("Information Log : {0}", i));
                Debug.LogWarning(string.Format("Warning Log : {0}", i));
                Debug.LogError(string.Format("Error Log : {0}", i));
            }
        }
    }
}
