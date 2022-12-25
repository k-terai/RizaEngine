// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace RizaEdCore.TreeSystem
{
    public class TreeComponent<T> : NotifyPropertyChangedBase
        where T : class
    {
        private string _name;
        private T _owner;
        private bool _isSelected;
        private bool _isSubSelected;
        private bool _isExpanded;
        private bool _isEditMode;
        private int _iconIndex;
        private Uri _iconUri;
        private Uri _thumbnailUri;
        private TreeComponent<T> _parent;
        private ObservableCollection<TreeComponent<T>> _child;

        /// <summary>
        /// Does tree owner accept drops? (Ex Folder =  true, Texture = false)
        /// </summary>
        protected bool _isOwnerAllowDrop;

        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }
        public T Owner { get => _owner; private set { _owner = value; NotifyPropertyChanged(); } }
        public bool IsSubSelected { get => _isSubSelected; set { _isSubSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged("IsAllowDrop"); } }
        public bool IsSelected { get => _isSelected; set { _isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged("IsAllowDrop"); } }
        public bool IsExpanded { get => _isExpanded; set { _isExpanded = value; NotifyPropertyChanged(); } }

        public bool IsEditMode { get => _isEditMode; set { _isEditMode = value; NotifyPropertyChanged(); } }

        public int IconIndex { get => _iconIndex; set { _iconIndex = value; NotifyPropertyChanged(); } }
        public Uri IconUri { get => _iconUri; set { _iconUri = value; NotifyPropertyChanged(); } }
        public Uri ThumbnailUri { get => _thumbnailUri; set { _thumbnailUri = value; NotifyPropertyChanged(); } }
        public TreeComponent<T> Parent { get => _parent; set { _parent = value; NotifyPropertyChanged(); } }
        public ObservableCollection<TreeComponent<T>> Child { get => _child; set { _child = value; NotifyPropertyChanged(); } }
        public string Path
        {
            get
            {
                if (_parent == null)
                {
                    return Name;
                }
                else
                {
                    return System.IO.Path.Combine(Parent.Path, Name);
                }
            }
        }

        /// <summary>
        /// Related <see cref="_isOwnerAllowDrop"/> <seealso cref="IsSelected"/> <seealso cref="IsSubSelected"/>
        /// </summary>
        public virtual bool IsAllowDrop { get => _isOwnerAllowDrop && !IsSubSelected && !IsSelected; }

        /// <summary>
        /// Called before the Tree's parent changes.
        /// </summary>
        public Func<bool> OnCheckChangeParent { get; set; }

        /// <summary>
        /// Callled when parent changed.
        /// </summary>
        /// <value> 1 = old parent 2 = new parent</value>
        public Action<TreeComponent<T>, TreeComponent<T>> OnParentChanged { get; set; }

        /// <summary>
        /// Create new root tree component.
        /// </summary>
        /// <param name="owner">Root owner object.</param>
        /// <param name="ownername">Root owner object name.</param>
        /// <returns>Return instance.</returns>
        public static TreeComponent<T> CreateNewRootTree(T owner, string ownername)
        {
            return new TreeComponent<T>(owner, ownername, null);
        }

        /// <summary>
        /// Select between tree1 and tree2.
        /// </summary>
        /// <param name="tree1"></param>
        /// <param name="tree2"></param>
        /// <returns>Select component list.</returns>
        public static List<TreeComponent<T>> RangeSelect(TreeComponent<T> tree1, TreeComponent<T> tree2)
        {
            if (tree1 == null || tree2 == null)
            {
                //Log.DebugError(Log.DebugType.Editor, "Argument can't be null.", " LangdonEditor.Core.Components.Trees.TreeComponent<T>.RangeSelect");
                return null;
            }

            var selects = new List<TreeComponent<T>>();
            TreeComponent<T> top = null;
            TreeComponent<T> under = null;

            var root = tree1.GetRoot();

            // Whether tree1 or tree2 is higher up
            if (root.GetHierarchy(tree1) < root.GetHierarchy(tree2))
            {
                top = tree1;
                under = tree2;
            }
            else
            {
                top = tree2;
                under = tree1;
            }

            selects.Add(top);
            top.IsSubSelected = true; //If Top is the root node, the subselect flag is not set, so set it here.

            bool isend = false;
            RangeSelectRecursive(root, root, under, root.GetHierarchy(top), root.GetHierarchy(under), ref isend, selects);

            return selects;
        }

        /// <summary>
        /// Add child tree.
        /// </summary>
        /// <param name="owner">Child tree owner.</param>
        /// <param name="name">Child tree owner name</param>
        /// <returns>Return new child tree componrnt.</returns>
        public TreeComponent<T> AddChild(T owner, string ownername)
        {
            var ct = new TreeComponent<T>(owner, ownername, this);
            Child.Add(ct);
            return ct;
        }

        /// <summary>
        /// Root self and return the number of hierarchy of tree specified by the argument.
        /// </summary>
        /// <param name="childtree">Child tree.</param>
        /// <returns>Returns the number of hierarchy (returns -1 if not in hierarchy)</returns>
        public int GetHierarchy(TreeComponent<T> childtree)
        {
            int count = 0;
            bool isfind = false;

            if (childtree == this)
            {
                return count;
            }

            GetHierarchyRecursive(this, childtree, ref count, ref isfind);

            if (!isfind)
            {
                count = -1;
            }

            return count;
        }

        /// <summary>
        /// Checks if tree specified by the argument is in the hierarchy.
        /// </summary>
        /// <param name="checktree">Check tree.</param>
        /// <returns>Return true if child.</returns>
        public bool IsChild(TreeComponent<T> checktree)
        {
            return GetHierarchy(checktree) != -1; //-1 no child.
        }

        /// <summary>
        /// Get root tree.
        /// </summary>
        /// <returns>Return root instance.</returns>
        public TreeComponent<T> GetRoot()
        {
            if (_parent == null) return this;

            return _parent.GetRoot();
        }

        /// <summary>
        /// Reset select status.
        /// </summary>
        /// <param name="tophierarchyonly">True = reset select status only components of yourself and top hierarchy</param>
        public void ResetSelectIncludeChilds(bool tophierarchyonly = false)
        {
            IsSelected = IsSubSelected = false;

            if (tophierarchyonly)
            {
                foreach (var c in Child)
                {
                    c.IsSelected = c.IsSubSelected = false;
                }
            }
            else
            {
                ResetSelectIncludeChildRecursive(this);
            }
        }

        /// <summary>
        /// Change parent tree.
        /// </summary>
        /// <param name="newparent">New parent.</param>
        /// <returns>Return true if change successed.</returns>
        public bool ChangeParent(TreeComponent<T> newparent)
        {
            var oldparent = Parent;

            //Check new parent allow drop currently.
            if (!newparent.IsAllowDrop)
            {
                return false;
            }

            //No change same parent.
            if (oldparent == newparent)
            {
                return false;
            }

            //Not make child a new parent.
            if (IsChild(newparent))
            {
                return false;
            }

            //additional check.
            var result = OnCheckChangeParent?.Invoke();
            if (result.HasValue && !result.Value) return false;

            Parent.Child.Remove(this);

            Parent = newparent;
            Parent.Child.Add(this);

            OnParentChanged?.Invoke(oldparent, newparent);
            return true;
        }

        public List<TreeComponent<T>> GetChilds(string searchPattern, bool isTopTreeOnly = false)
        {
            var result = new List<TreeComponent<T>>();
            result.AddRange(Child);

            if (isTopTreeOnly == false)
            {
                GetAllChildRecursive(this, result);
            }

            if (string.IsNullOrEmpty(searchPattern) == false)
            {
                result = result.Where(tree => tree.Name.Contains(searchPattern)).ToList();
            }

            return result;
        }

        /// <summary>
        /// Constructor.(As child)
        /// </summary>
        /// <param name="owner">Owner object.</param>
        /// <param name="name">Owner name.</param>
        /// <param name="parent">Parent.(Null == Root)</param>
        protected TreeComponent(T owner, string name, TreeComponent<T> parent)
        {
            Owner = owner;
            Name = name;
            Parent = parent;
            Child = new ObservableCollection<TreeComponent<T>>();
            IsSelected = IsExpanded;
            _isOwnerAllowDrop = true;
        }


        private static void GetHierarchyRecursive(TreeComponent<T> parent, TreeComponent<T> target, ref int count, ref bool isfind)
        {
            foreach (var c in parent.Child)
            {
                if (c == target)
                {
                    ++count;
                    isfind = true;
                    return;
                }
                else if (!isfind)
                {
                    ++count;
                    GetHierarchyRecursive(c, target, ref count, ref isfind);
                }
            }

        }

        private static void RangeSelectRecursive(TreeComponent<T> root, TreeComponent<T> top, TreeComponent<T> under, int tophierarchy, int underhierarchy, ref bool isend, List<TreeComponent<T>> selects)
        {

            foreach (var c in top.Child)
            {
                if (isend) return;

                var hierarchy = root.GetHierarchy(c);
                if (tophierarchy <= hierarchy && underhierarchy >= hierarchy)
                {
                    c.IsSubSelected = true;
                    selects.Add(c);

                }

                if (c == under)
                {
                    isend = true;
                    return;
                }

                RangeSelectRecursive(root, c, under, tophierarchy, underhierarchy, ref isend, selects);
            }
        }

        private static void ResetSelectIncludeChildRecursive(TreeComponent<T> parent)
        {
            foreach (var c in parent.Child)
            {
                c.IsSelected = c.IsSubSelected = false;
                ResetSelectIncludeChildRecursive(c);
            }
        }

        private static void GetAllChildRecursive(TreeComponent<T> parent, List<TreeComponent<T>> result)
        {
            if (parent.Child == null)
            {
                return;
            }
            else
            {
                foreach (var c in parent.Child)
                {
                    result.AddRange(c.Child);
                    GetAllChildRecursive(c, result);
                }
            }
        }
    }
}
