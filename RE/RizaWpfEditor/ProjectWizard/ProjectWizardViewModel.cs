// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using RizaEdCore.WindowSystem;
using RizaEdCore.ProjectSystem;
using RizaEdShare.CoreSystem;

namespace RizaWpfEditor.ProjectWizard
{
    public sealed class ProjectWizardViewModel : WindowViewModel
    {
        private ProjectViewModel projectViewModel;

        public DelegateCommand SelectLocationCommand { get; private set; }
        public DelegateCommand CancelCommand { get; set; }
        public ProjectViewModel ProjectViewModel { get => projectViewModel; set { projectViewModel = value; NotifyPropertyChanged(); } }

        public event System.Action OnCancel;

        public ProjectWizardViewModel() : base()
        {
            Width = 1000;
            MinWidth = 800;

            Height = 600;
            MinHeight = 600;
            Title = "Project Wizard";
            Style = WindowStyle.ToolWindow;
            ResizeModeType = ResizeMode.NoResize;

            ProjectViewModel = new ProjectViewModel();
            InitCommands();
        }

        private void InitCommands()
        {
            ProjectViewModel.OnProjectCreated += (Project p) =>
            {
                if (p != null)
                {
                    EditorManager.Restart(p.DataPath);
                }
            };

            SelectLocationCommand = new DelegateCommand(

                 (object p) =>
                 {
                     var path = EditorManager.CreateSelectExternalFolderWindow().ShowWindow("Select new project path.");
                     if (path != string.Empty)
                     {
                         ProjectViewModel.Location = path;
                     }
                 }
                 );

            CancelCommand = new DelegateCommand(

                  (object p) =>
                  {
                      OnCancel?.Invoke();
                  }
                  );
        }
    }
}
