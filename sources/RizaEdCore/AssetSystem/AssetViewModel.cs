// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using RizaEdCore.LocalizationSystem;
using RizaEdCore.TreeSystem;
using RizaEdShare.CoreSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static RizaEdCore.CoreSystem.EditorCommon;

namespace RizaEdCore.AssetSystem
{
    public class AssetViewModel : ViewModelBase
    {
        private NormalFolder parent;
        private AssetTreeComponent parentTree;
        private string assetName;

        public NormalFolder Parent { get => parent; protected set => parent = value; }
        public AssetTreeComponent ParentTree { get => parentTree; protected set => parentTree = value; }

        public string AssetName
        {
            get => assetName;
            set
            {
                assetName = value;
                var result = AssetDatabase.IsValidName(assetName, Parent);

                if (result == Result.OK)
                {
                    _errors["AssetName"] = null;
                }
                else
                {
                    _errors["AssetName"] = new[] { LocalizationManager.GetString(result) };
                }

                NotifyPropertyChanged();
                NotifyErrorsChanged();
            }
        }

        public DelegateCommand CreateFolderCommand { get; set; }

        public DelegateCommand RenameCommand { get; set; }

        public AssetViewModel()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitCommands();
        }

        public void SetParent(AssetTreeComponent tree)
        {
            Debug.Assert(tree.Owner is NormalFolder);

            Parent = tree.Owner as NormalFolder;
            ParentTree = tree;
        }

        private void InitCommands()
        {
            CreateFolderCommand = new DelegateCommand((object p) =>
            {
                var folder = parent.CreateFolder(AssetName);
                var tree = parentTree.AddChild(folder, AssetName);
            },
            (object p) =>
            {
                return parent != null && HasErrors == false;
            });

            RenameCommand = new DelegateCommand((object p) =>
            {
                var tree = p as AssetTreeComponent;
                Debug.Assert(tree != null);
                tree.Rename(AssetName);
            },
            (object p) =>
            {
                return HasErrors == false;
            });
        }

    }
}
