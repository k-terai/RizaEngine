// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using RizaEdCore.ProjectSystem;
using RizaEdCore.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RizaEdCore.AssetSystem
{
    public abstract class Asset : NotifyPropertyChangedBase
    {
        /// <summary>
        /// NOTE: null is possible.(Ex: Root folder)
        /// </summary>
        private Asset _parent;
        private string _name;
        private AssetMetaData _metaData;
        private Uri _defaultUri;
        private Uri _thumbnailUri;
        private bool _isDirty;

        protected event Action<Asset> OnChildAssetInitialized;
        protected AssetMetaData MetaData { get => _metaData; set => _metaData = value; }

        public static event Action<Asset> OnBaseAssetInitialized;

        public Asset Parent { get => _parent; protected set { _parent = value; NotifyPropertyChanged(); } }
        public string Name { get => _name; protected set { _name = value; NotifyPropertyChanged(); } }
        public Uri DefaultUri { get => _defaultUri; protected set { _defaultUri = value; NotifyPropertyChanged(); } }

        public Uri ThumbnailUri { get => _thumbnailUri; protected set => _thumbnailUri = value; }

        public string Extension { get; protected set; }
        public string NameWithExtension { get => Name + Extension; }
        public Guid Id { get => _metaData.Id; }
        public string ConvertType { get => _metaData.ConvertType; }
        public uint Version { get => _metaData.Version; }

        /// <summary>
        /// Absolute path (Ex. C:...)
        /// </summary>
        public virtual string FullPath { get => Path.Combine(_parent.FullPath, NameWithExtension); }

        /// <summary>
        /// Relative path from project folder.(Ex. Assets/Textures/SampleTexture.png)
        /// </summary>
        public virtual string RelativePath
        {
            get => Path.Combine(_parent.RelativePath, NameWithExtension);
        }

        public string MetaDataFullPath { get => FullPath + EditorConsts.ASSET_METADATA_EXTENSION; }

        public string RuntimeDirPath
        {
            get => Path.Combine(Project.Current.RuntimesDirectory.FullName, Id.ToString().Substring(0, 2));
        }

        public bool IsDirty { get => _isDirty; set { _isDirty = value; NotifyPropertyChanged(); } }

        public virtual void Initialize(AssetContext context)
        {
            Parent = context.Parent;
            Name = context.Name;
            Extension = context.Extension;
            ThumbnailUri = DefaultUri = context.DefaultUri;

            if (!context.IsMetaDataExists)
            {
                CreateMetaData(context);
            }
            else
            {
                MetaData = Serializer.Deserialize<AssetMetaData>(MetaDataFullPath);
            }

            if (Parent != null)
            {
                Parent.OnChildAssetInitialized?.Invoke(this);
            }

            CreateThumbnail(true);

            IsDirty = false;
            OnBaseAssetInitialized?.Invoke(this);
        }

        public virtual bool Save()
        {
            if (IsDirty)
            {
                IsDirty = false;
                return Serializer.Serialize(_metaData, MetaDataFullPath);
            }

            return true;
        }

        public virtual bool Rename(string name)
        {
            try
            {
                var newName = name;
                var newFilePath = Path.Combine(Parent.FullPath, newName + Extension);
                var newMetaPath = Path.Combine(Parent.FullPath, newName + Extension + EditorConsts.ASSET_METADATA_EXTENSION);

                File.Move(FullPath, newFilePath);
                File.Move(MetaDataFullPath, newMetaPath);

                Name = name;
                return true;

            }
            catch (IOException exception)
            {
                LogSystem.Debug.LogException(exception);
                return false;
            }
        }

        public virtual void CreateThumbnail(bool isForceUpdate)
        {
            ThumbnailUri = DefaultUri;
        }

        protected virtual bool CreateMetaData(AssetContext context)
        {
            if (_metaData == null)
            {
                _metaData = new AssetMetaData();
            }

            _metaData.Version = 1;
            _metaData.ConvertType = context.ConvertType;
            _metaData.Id = Guid.NewGuid();

            return Serializer.Serialize(_metaData, MetaDataFullPath);
        }

        protected void CreateRuntimeDirectoryIfNotExists()
        {
            var path = RuntimeDirPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
