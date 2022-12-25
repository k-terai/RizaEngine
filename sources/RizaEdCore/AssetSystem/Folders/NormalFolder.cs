// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;

namespace RizaEdCore.AssetSystem
{
    public class NormalFolder : Asset
    {
        private ObservableCollection<Asset> _childs;

        public ObservableCollection<Asset> Childs { get => _childs; protected set { _childs = value; NotifyPropertyChanged(); } }
        public NormalFolder ParentFolder { get => Parent as NormalFolder; }

        public NormalFolder() : base()
        {
            Childs = new ObservableCollection<Asset>();
        }

        public override void Initialize(AssetContext context)
        {
            base.Initialize(context);

            OnChildAssetInitialized += (Asset ca) =>
            {
                Childs.Add(ca);
            };
        }

        public DirectoryInfo GetDirectoryInfo()
        {
            return new DirectoryInfo(FullPath);
        }

        public override bool Rename(string name)
        {
            try
            {
                var newName = name;
                var newDirectoryPath = Path.Combine(Parent.FullPath, newName + Extension);
                var newMetaPath = Path.Combine(Parent.FullPath, newName + Extension + EditorConsts.ASSET_METADATA_EXTENSION);

                Directory.Move(FullPath, newDirectoryPath);
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
    }
}
