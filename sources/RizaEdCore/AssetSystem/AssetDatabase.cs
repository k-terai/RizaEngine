// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.AssetSystem.Folders.Root;
using RizaEdCore.CoreSystem;
using RizaEdCore.LocalizationSystem;
using RizaEdCore.TreeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Linq;
using static RizaEdCore.CoreSystem.EditorCommon;

namespace RizaEdCore.AssetSystem
{
    public static class AssetDatabase
    {
        private static Dictionary<string, Uri> s_assetIconUriCache = new Dictionary<string, Uri>();
        private static Dictionary<Guid, Asset> s_projectAssets;

        public static RootFolder Root { get; private set; }

        public static bool Startup()
        {
            s_projectAssets = new Dictionary<Guid, Asset>();
            Asset.OnBaseAssetInitialized += OnBaseAssetInitialized;
            return true;
        }

        public static bool Initialize()
        {
            InitializeProjectAssets();
            return true;
        }

        public static void RegisterIconUrl<T>(Uri uri)
            where T : Asset
        {
            var name = typeof(T).Name;
            s_assetIconUriCache[name] = uri;
        }

        public static Uri GetIconUri<T>()
            where T : Asset
        {
            return s_assetIconUriCache[typeof(T).Name];
        }

        public static T GetAssetFromFullPath<T>(string fullPath)
           where T : Asset
        {
            var a = s_projectAssets.Values.FirstOrDefault(t => t.FullPath.Equals(fullPath));
            return a as T;
        }

        /// <summary>
        /// Create <see cref="AssetTreeComponent"/> basis on <see cref="RootFolder"/>
        /// </summary>
        /// <returns>New <see cref="AssetTreeComponent"/> instance.</returns>
        public static AssetTreeComponent CreateNewRootTree()
        {
            var root = AssetTreeComponent.CreateNewRootTree(Root, Root.Name);
            InitAssetTreeFromAssetsFolderRecursive(Root, root);
            return root;
        }

        public static T CreateAsset<T>(AssetContext context) where T : Asset, new()
        {
            var asset = new T();
            asset.Initialize(context);
            return asset;
        }

        public static NormalFolder CreateFolder(this NormalFolder folder, string name)
        {
            try
            {
                var d = Directory.CreateDirectory(Path.Combine(folder.FullPath, name));
                var f = new NormalFolder();
                f.Initialize(new AssetContext(d, folder));
                return f;
            }
            catch (IOException exception)
            {
                LogSystem.Debug.LogException(exception);
                return null;
            }
        }

        public static Result IsValidName(string name, NormalFolder parent)
        {
            var c = name;

            //Check min length.
            if (c == null || c.Length == 0)
            {
                return Result.ERROR_PROJECTNAME_MIN;
            }

            //Check max length.
            if (c.Length > EditorConsts.MAX_ASSET_NAME_LENGTH)
            {
                return Result.ERROR_ASSETNAME_MAX;
            }

            //Check characters.
            char[] invalidChars = Path.GetInvalidFileNameChars();
            char[] invalidPathChars = Path.GetInvalidPathChars();

            if (c.IndexOfAny(invalidChars) > 0 || c.IndexOfAny(invalidPathChars) > 0 || c.Contains(" ") || c.Contains("　"))
            {
                return Result.ERROR_ASSETNAME_INVALID;
            }

            //Check Half-width character.
            if (LocalizationManager.Shift_JIS_Encoding.GetByteCount(c) != c.Length)
            {
                return Result.ERROR_ASSETNAME_DOUBLEBYTE;
            }

            //Check if there is a asset with the same name.
            if (parent.Childs.Count(t => t.Name == c) != 0)
            {
                return Result.ERROR_ASSETNAME_SAMENAME;
            }

            return Result.OK;
        }

        public static Asset ImportAsset(AssetContext context)
        {
            Asset asset = null;

            //Copy source file into project.
            File.Copy(context.FullPath, Path.Combine(context.Parent.FullPath, string.Concat(context.Name, context.Extension)));

            switch (context.Extension)
            {
                case ".png":
                case ".jpg":
                    {
                        asset = new Texture();
                        asset.Initialize(context);
                    }
                    break;
            }

            return asset;
        }

        private static void RegisterAsset(Asset asset)
        {
            Debug.Assert(s_projectAssets.ContainsKey(asset.Id) == false);
            s_projectAssets[asset.Id] = asset;
        }

        private static void InitializeProjectAssets()
        {
            Root = RootFolder.CreateInstance();
            InitializeProjectAssetsRecursive(Root);
        }

        private static void InitializeProjectAssetsRecursive(NormalFolder folder)
        {
            var d = folder.GetDirectoryInfo();
            var cds = d.GetDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
            var cfs = d.GetFiles("*", System.IO.SearchOption.TopDirectoryOnly);


            foreach (var file in cfs)
            {
                Asset asset = null;
                var context = new AssetContext(file.FullName, folder);

                switch (file.Extension)
                {
                    case ".png":
                    case ".jpg":
                        {
                            asset = new Texture();
                            asset.Initialize(context);
                        }
                        break;
                }
            }

            foreach (var directory in cds)
            {
                var f = new NormalFolder();
                f.Initialize(new AssetContext(directory, folder));
                InitializeProjectAssetsRecursive(f);
            }
        }

        private static void InitAssetTreeFromAssetsFolderRecursive(NormalFolder folder, AssetTreeComponent tree)
        {
            if (folder.Childs == null)
            {
                return;
            }

            foreach (var c in folder.Childs)
            {
                var nt = tree.AddChild(c, c.Name);
                if (c is NormalFolder normalFolder)
                {
                    InitAssetTreeFromAssetsFolderRecursive(normalFolder, nt);
                }
            }
        }

        private static void OnBaseAssetInitialized(Asset obj)
        {
            RegisterAsset(obj);
        }

    }
}
