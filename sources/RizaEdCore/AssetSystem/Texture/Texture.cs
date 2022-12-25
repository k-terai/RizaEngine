// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if WPF
using System.Drawing;
#endif

namespace RizaEdCore.AssetSystem
{
    public sealed class Texture : Asset
    {
        public Texture() : base()
        {

        }

        public override void Initialize(AssetContext context)
        {
            base.Initialize(context);
        }

        public override bool Rename(string name)
        {
            return base.Rename(name);
        }

        public override bool Save()
        {
            return base.Save();
        }

        public override void CreateThumbnail(bool isForceUpdate)
        {
#if WPF
            CreateRuntimeDirectoryIfNotExists();
            var path = System.IO.Path.Combine(RuntimeDirPath, Id.ToString() + EditorConsts.THUMBNAIL_FILE_IMAGE_EXTENSION);

            if (isForceUpdate == false && File.Exists(path))
            {
                return;
            }

            using (var source = new Bitmap(FullPath))
            {
                int width = EditorConsts.THUMBNAIL_FILE_SIZE;
                int height = EditorConsts.THUMBNAIL_FILE_SIZE;

                using (var dest = new Bitmap(source, width, height))
                {
                    dest.Save(path);
                    ThumbnailUri = new Uri(path, UriKind.Absolute);
                }
            }

#else
            base.CreateThumbnail(false);
#endif
        }

        protected override bool CreateMetaData(AssetContext context)
        {
            MetaData = new AssetMetaData()
            {
                Texture = new AssetMetaData.TextureData()
                {
                    Width = 100,
                    Height = 100
                }
            };

            return base.CreateMetaData(context);
        }
    }
}
