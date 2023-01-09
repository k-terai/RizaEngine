using RizaEdCore.CoreSystem;
using RizaEdCore.LocalizationSystem;
using RizaWpfEditor.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RizaWpfEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            EditorCommand.Initialize();
            LocalizationManager.Initialize();

            if (e.Args.Length == 0)
            {
                EditorCommand.OpenProjectWindowCommand.Execute(null);
                return;
            }

            foreach (var s in e.Args)
            {
                if (s.Contains(EditorConsts.PROJECT_DATA_EXTENSION)) //[ProjectFile] path.
                {
                    if (EditorManager.Startup(s))
                    {
                        StartupUri = new Uri(EditorManager.MAIN_WINDOW_URI_PATH, UriKind.RelativeOrAbsolute);
                    }
                    else
                    {
                        EditorCommand.OpenProjectWindowCommand.Execute(null);
                    }
                }
            }
        }
    }
}
