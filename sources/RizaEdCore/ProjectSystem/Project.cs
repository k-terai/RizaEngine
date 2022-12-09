// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RizaEdCore.LocalizationSystem;
using static RizaEdCore.CoreSystem.EditorCommon;

namespace RizaEdCore.ProjectSystem
{
    public sealed class Project : NotifyPropertyChangedBase
    {
        private ProjectData _data;
        private DirectoryInfo _rootDirectory;
        private DirectoryInfo _assetsDirectory;
        private DirectoryInfo _runtimesDirectory;
        private DirectoryInfo _pluginsDirectory;
        private DirectoryInfo _projectSettingsDirectory;
        private DirectoryInfo _userSettingsDirectory;

        public static Project Current { get; private set; }

        public DirectoryInfo RootDirectory { get => _rootDirectory; private set { _rootDirectory = value; NotifyPropertyChanged(); } }
        public DirectoryInfo AssetsDirectory
        {
            get
            {
                Debug.Assert(RootDirectory != null);
                var path = Path.Combine(_rootDirectory.FullName, EditorConsts.ASSETS_DIRECTORY_NAME);
                if (!Directory.Exists(path))
                {
                    _assetsDirectory = Directory.CreateDirectory(path);
                }
                else _assetsDirectory ??= new DirectoryInfo(path);

                return _assetsDirectory;
            }
        }
        public DirectoryInfo RuntimesDirectory
        {
            get
            {
                Debug.Assert(RootDirectory != null);
                var path = Path.Combine(_rootDirectory.FullName, EditorConsts.RUNTIME_DIRECTORY_NAME);
                if (!Directory.Exists(path))
                {
                    _runtimesDirectory = Directory.CreateDirectory(path);
                }
                else _runtimesDirectory ??= new DirectoryInfo(path);

                return _runtimesDirectory;
            }
        }
        public DirectoryInfo PluginsDirectory
        {
            get
            {
                Debug.Assert(RootDirectory != null);
                var path = Path.Combine(_rootDirectory.FullName, EditorConsts.PLUGIN_DIRECTORY_NAME);
                if (!Directory.Exists(path))
                {
                    _pluginsDirectory = Directory.CreateDirectory(path);
                }
                else _pluginsDirectory ??= new DirectoryInfo(path);

                return _pluginsDirectory;
            }
        }
        public DirectoryInfo ProjectSettingsDirectory
        {
            get
            {
                Debug.Assert(RootDirectory != null);
                var path = Path.Combine(_rootDirectory.FullName, EditorConsts.PROJECTSETTINGS_DIRECTORY_NAME);
                if (!Directory.Exists(path))
                {
                    _projectSettingsDirectory = Directory.CreateDirectory(path);
                }
                else _projectSettingsDirectory ??= new DirectoryInfo(path);

                return _projectSettingsDirectory;
            }
        }
        public DirectoryInfo UserSettingsDirectory
        {
            get
            {
                Debug.Assert(RootDirectory != null);
                var path = Path.Combine(_rootDirectory.FullName, EditorConsts.USERSETTINGS_DIRECTORY_NAME);
                if (!Directory.Exists(path))
                {
                    _userSettingsDirectory = Directory.CreateDirectory(path);
                }
                else _userSettingsDirectory ??= new DirectoryInfo(path);

                return _userSettingsDirectory;
            }
        }
        public string Name
        {
            get
            {
                Debug.Assert(_rootDirectory != null);
                return _rootDirectory.Name;
            }
        }
        public string DataPath
        {
            get
            {
                Debug.Assert(RootDirectory != null);
                return Path.Combine(RootDirectory.FullName, Name + EditorConsts.PROJECT_DATA_EXTENSION);
            }
        }
        public Guid Id
        {
            get
            {
                Debug.Assert(_data != null);
                return _data.Id;
            }
        }
        public string Version
        {
            get
            {
                Debug.Assert(_data != null);
                return _data.EngineVersion;
            }
        }

        public bool Save()
        {
            return Serialization.Serializer.Serialize(_data, DataPath);
        }

        /// <summary>
        /// Create new project.
        /// If <see cref="Current"/> is null,will be set.
        /// </summary>
        public static Project Create(string name, string parentFolderPath)
        {
            try
            {
                var p = new Project(Directory.CreateDirectory(Path.Combine(parentFolderPath, name)));
                p.CreateProjectDirectoriesIfNotExists();
                p.Save();

                if (Current == null)
                {
                    Current = p;
                }

                return p;

            }
            catch (Exception exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Load project.
        /// If <see cref="Project.Current"/> is null.will be set.
        /// </summary>
        public static Project Load(string datapath)
        {
            var data = Serialization.Serializer.Deserialize<ProjectData>(datapath);
            if (data == null)
            {
                return null;
            }

            var p = new Project(new FileInfo(datapath).Directory, data);
            p.CreateProjectDirectoriesIfNotExists();

            if (Current == null)
            {
                Current = p;
            }

            return p;
        }

        public static Result IsValidName(string name, string location)
        {
            bool valid = true;

            //Check min length.
            if (valid && (name == null || name.Length == 0))
            {
                return Result.ERROR_PROJECTNAME_MIN;
            }

            //Check max length.
            if (valid && name.Length > EditorConsts.MAX_PROJECTNAME_LENGTH)
            {
                return Result.ERROR_PROJECTNAME_MAX;
            }

            //Check characters.
            char[] invalidChars = Path.GetInvalidFileNameChars();
            char[] invalidPathChars = Path.GetInvalidPathChars();

            if (valid && (name.IndexOfAny(invalidChars) > 0 || name.IndexOfAny(invalidPathChars) > 0 || name.Contains(" ") || name.Contains("　")))
            {
                return Result.ERROR_PROJECTNAME_INVALID;
            }

            //Check Half-width character.
            if (valid && LocalizationManager.Shift_JIS_Encoding.GetByteCount(name) != name.Length)
            {
                return Result.ERROR_PROJECTNAME_DOUBLEBYTE;
            }

            //Check new project path.
            var projectpath = Path.Combine(location, name);
            if (valid && Directory.Exists(projectpath))
            {
                return Result.ERROR_PROJECTNAME_PATH_EXISTS;
            }



            return Result.OK;
        }

        public static Result IsValidLocation(string location)
        {
            if (!Directory.Exists(location))
            {
                return Result.ERROR_PROJECTPATH_NOT_EXISTS;
            }

            return Result.OK;
        }

        private Project(DirectoryInfo root)
        {
            RootDirectory = root;
            _data = new ProjectData()
            {
                Id = Guid.NewGuid(),
                EngineVersion = EditorConsts.ENGINE_VERSION
            };
        }

        private Project(DirectoryInfo root, ProjectData deserializeddata)
        {
            RootDirectory = root;
            _data = deserializeddata;
        }

        private void CreateProjectDirectoriesIfNotExists()
        {
            //Access property,create folder.
            AssetsDirectory.ToString();
            RuntimesDirectory.ToString();
            PluginsDirectory.ToString();
            ProjectSettingsDirectory.ToString();
            UserSettingsDirectory.ToString();
        }

    }
}
