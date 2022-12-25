// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RizaEdCore.CoreSystem;

namespace RizaEdCore.AssetSystem
{
    public class AssetContext
    {
        public Asset Parent { get; private set; }
        public string Name { get; private set; }
        public string FullPath { get; private set; }
        public string Extension { get; private set; }
        public string ConvertType { get; private set; }
        public bool IsMetaDataExists { get; set; } = false;

        public bool IsFolderAsset { get; set; } = false;

        public Uri DefaultUri { get; private set; }

        public AssetContext(string fullpath, Asset parent)
        {
            Parent = parent;

            if (Directory.Exists(fullpath))
            {
                Initialize(new DirectoryInfo(fullpath), parent);
            }
            else if (File.Exists(fullpath))
            {
                Initialize(new FileInfo(fullpath), parent);
            }

            IsMetaDataExists = File.Exists(FullPath + EditorConsts.ASSET_METADATA_EXTENSION);
        }

        public AssetContext(DirectoryInfo info, Asset parent)
        {
            Initialize(info, parent);
        }

        private void Initialize(DirectoryInfo info, Asset parent)
        {
            Parent = parent;
            Name = info.Name;
            FullPath = info.FullName;
            Extension = string.Empty;
            ConvertType = string.Empty;
            IsFolderAsset = true;
        }

        private void Initialize(FileInfo info, Asset parent)
        {

            Parent = parent;
            Name = System.IO.Path.GetFileNameWithoutExtension(info.Name);
            FullPath = info.FullName;
            Extension = info.Extension;
            ConvertType = string.Empty;
            IsFolderAsset = false;
            DefaultUri = AssetDatabase.GetIconUri<Texture>();
            ConvertType = ".dds";
        }
    }
}
