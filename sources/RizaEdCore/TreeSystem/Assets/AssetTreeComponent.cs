// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.AssetSystem;
using RizaEdCore.AssetSystem.Folders.Root;
using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.TreeSystem
{
    public sealed class AssetTreeComponent : TreeComponent<Asset>
    {
        public Asset ParentAsset { get { return Parent != null ? Parent.Owner : null; } }

        /// <summary>
        /// Create new root tree component.
        /// </summary>
        /// <param name="owner">Root owner asset.</param>
        /// <param name="ownername">Root owner object name.</param>
        /// <returns>New instance.</returns>
        public static new AssetTreeComponent CreateNewRootTree(Asset owner, string ownername)
        {
            var root = new AssetTreeComponent(owner, ownername, null);
            root.ThumbnailUri = root.IconUri = AssetDatabase.GetIconUri<RootFolder>();

            return root;
        }

        /// <summary>
        /// Create new root tree component as dammy.
        /// Ex : using search tree root 
        /// </summary>
        /// <returns>New instance,</returns>
        public static new AssetTreeComponent CreateNewRootTreeAsDammy()
        {
            var root = new AssetTreeComponent();
            return root;
        }

        /// <summary>
        /// Add child.
        /// </summary>
        /// <param name="owner">Owner <see cref="Asset"/>.</param>
        /// <param name="ownername">Owner asset name.</param>
        /// <returns>Return AssetTreeComponent added as a child of this tree.</returns>
        public new AssetTreeComponent AddChild(Asset owner, string ownername)
        {
            var ct = new AssetTreeComponent(owner, ownername, this);
            Child.Add(ct);
            return ct;
        }

        public void Rename(string name)
        {
            Owner.Rename(name);
        }

        private AssetTreeComponent() : base(null!, null!, null!)
        {
            _isOwnerAllowDrop = false;
        }

        private AssetTreeComponent(Asset owner, string name, TreeComponent<Asset> parent) : base(owner, name, parent)
        {
            if (owner == null)
            {
                return;
            }

            if (owner is NormalFolder)
            {
                _isOwnerAllowDrop = true;
                ThumbnailUri = IconUri = AssetDatabase.GetIconUri<NormalFolder>();
            }
            else
            {
                IconUri = owner.DefaultUri;
                ThumbnailUri = owner.ThumbnailUri;
                _isOwnerAllowDrop = false;
            }
        }

    }
}
