// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.AssetSystem;
using RizaEdCore.AssetSystem.Folders.Root;
using RizaEdCore.ControlSystem;
using RizaEdCore.CoreSystem;
using RizaEdCore.TreeSystem;
using RizaEdShare.CoreSystem;
using RizaWpfEditor.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizaWpfEditor.AssetBrowser
{
    public class AssetBrowserControlViewModel : ControlViewModel
    {
        private AssetTreeComponent _displayTree;
        private AssetTreeComponent _assetTree;
        private AssetTreeComponent _searchResultRootTree;
        private AssetTreeComponent _selectTree;
        private string _searchAssetName;

        private AssetViewModel _assetViewModel;

        private double _thumbnailSize;

        public AssetTreeComponent DisplayTree { get => _displayTree; set { _displayTree = value; NotifyPropertyChanged(); } }
        public List<AssetTreeComponent> MultiSelectTrees { get; private set; }
        public AssetTreeComponent SelectTree
        {
            get => _selectTree;
            set
            {
                _selectTree = value;
                if (_selectTree == null)
                {
                    return;
                }

                _selectTree.IsSelected = _selectTree.IsSubSelected = true;

                if (_selectTree.Parent != null) // root folder = parent is always null 
                {
                    _selectTree.Parent.IsExpanded = true;
                    AssetViewModel.SetParent(_selectTree.Parent as AssetTreeComponent);
                }

                NotifyPropertyChanged();
                OnSelectionChanged?.Invoke(value);
            }
        }

        public double ThumbnailSize { get => _thumbnailSize; set { _thumbnailSize = value; NotifyPropertyChanged(); NotifyPropertyChanged("ImageThumbnailSize"); } }

        public double ImageThumbnailSize { get => _thumbnailSize - 20; }

        public bool IsTinyThumbnail
        {
            get => ThumbnailSize == EditorConsts.TINY_THUMBNAIL_SIZE;
        }

        public bool IsSmallThumbnail
        {
            get => ThumbnailSize == EditorConsts.SMALL_THUMBNAIL_SIZE;
        }

        public bool IsMediumThumbnail
        {
            get => ThumbnailSize == EditorConsts.MEDIUM_THUMBNAIL_SIZE;
        }

        public bool IsLargeThumbnail
        {
            get => ThumbnailSize == EditorConsts.LARGE_THUMBNAIL_SIZE;
        }

        public bool IsHugeThumbnail
        {
            get => ThumbnailSize == EditorConsts.HUGE_THUMBNAIL_SIZE;
        }


        public AssetViewModel AssetViewModel { get => _assetViewModel; set { _assetViewModel = value; NotifyPropertyChanged(); } }

        public event Action<AssetTreeComponent> OnSelectionChanged;

        public Action<AssetTreeComponent> OnEditTempNameEnd { get; private set; }

        public DelegateCommand CreateFolderCommand { get; set; }

        public DelegateCommand RenameCommand { get; set; }

        public DelegateCommand ImportCommand { get; set; }

        public DelegateCommand ChangeThumbnailSizeCommand { get; private set; }

        public string SearchAssetName
        {
            get => _searchAssetName;
            set
            {
                _searchAssetName = value;

                if (string.IsNullOrEmpty(value))
                {
                    DisplayTree = _assetTree;
                    SelectTree = null;
                }
                else
                {
                    _searchResultRootTree.Child.Clear();
                    DisplayTree = _searchResultRootTree;
                    var searchResults = _assetTree.GetChilds(value);
                    foreach (var t in searchResults)
                    {
                        DisplayTree.Child.Add(t);
                    }
                    SelectTree = _searchResultRootTree;
                }

                NotifyPropertyChanged();
            }
        }

        public AssetBrowserControlViewModel()
        {
            _assetTree = AssetTreeComponent.CreateNewRootTreeAsDammy();
            _assetTree.Child.Add(AssetDatabase.CreateNewRootTree());
            _searchResultRootTree = AssetTreeComponent.CreateNewRootTreeAsDammy();

            DisplayTree = _assetTree;
            MultiSelectTrees = new List<AssetTreeComponent>();
            AssetViewModel = new AssetViewModel();
            InitCommands();

            ChangeThumbnailSizeCommand.SafeExecute(executeParametor: "Medium");
        }

        private void InitCommands()
        {
            CreateFolderCommand = new DelegateCommand((object p) =>
            {
                var temp = SelectTree.AddChild(null, "NewFolder");
                temp.IsSelected = true;

                SelectTree.IsExpanded = true;

                //NOTE : IsEditMode sets on in render query execute timing because same frame sets not working.
                EditorDispatcher.Execute(() =>
                {
                    temp.IsEditMode = true;
                }, System.Windows.Threading.DispatcherPriority.Render);

                OnEditTempNameEnd = null;
                OnEditTempNameEnd += (AssetTreeComponent tree) =>
                {
                    //NOTE: tree is a dammy, so parent to set.
                    AssetViewModel.SetParent(tree.Parent as AssetTreeComponent);
                    AssetViewModel.AssetName = tree.Name;

                    AssetViewModel.CreateFolderCommand.SafeExecute();

                    // The selected tree item will change when remove it, so we needs to call it last.
                    tree.Parent.Child.Remove(tree);
                };


            },
            (object p) =>
            {
                return SelectTree != null;
            });

            RenameCommand = new DelegateCommand((object p) =>
            {
                //NOTE : IsEditMode sets on in render query execute timing because same frame sets not working.
                EditorDispatcher.Execute(() =>
                {
                    SelectTree.IsEditMode = true;
                }, System.Windows.Threading.DispatcherPriority.Render);

                OnEditTempNameEnd = null;
                OnEditTempNameEnd += (AssetTreeComponent tree) =>
                {
                    AssetViewModel.AssetName = tree.Name;
                    if (AssetViewModel.RenameCommand.SafeExecute(tree, tree) == false)
                    {
                        tree.Name = tree.Owner.Name;
                    }

                    SelectTree.IsEditMode = false;
                };

            },
            (object p) =>
            {
                return SelectTree != null && SelectTree.Owner is RootFolder == false;
            });

            ChangeThumbnailSizeCommand = new DelegateCommand(

              (object p) =>
              {
                  switch ((string)p)
                  {
                      case "Tiny":
                          ThumbnailSize = EditorConsts.TINY_THUMBNAIL_SIZE;
                          break;
                      case "Small":
                          ThumbnailSize = EditorConsts.SMALL_THUMBNAIL_SIZE;
                          break;
                      case "Medium":
                          ThumbnailSize = EditorConsts.MEDIUM_THUMBNAIL_SIZE;
                          break;
                      case "Large":
                          ThumbnailSize = EditorConsts.LARGE_THUMBNAIL_SIZE;
                          break;
                      case "Huge":
                          ThumbnailSize = EditorConsts.HUGE_THUMBNAIL_SIZE;
                          break;
                  }

                  NotifyPropertyChanged("IsTinyThumbnail");
                  NotifyPropertyChanged("IsSmallThumbnail");
                  NotifyPropertyChanged("IsMediumThumbnail");
                  NotifyPropertyChanged("IsLargeThumbnail");
                  NotifyPropertyChanged("IsHugeThumbnail");
              }
              ,
              (object p) =>
              {
                  return true;
              }

              );

            ImportCommand = new DelegateCommand((object p) =>
            {
                var importer = EditorManager.CreateSelectExternalFileWindow();
                var results = importer.ShowWindow(Resources.ImportDescription, EditorConsts.ASSET_IMPORT_FILTER, true);

                if (results == null)
                {
                    return;
                }

                foreach (var r in results)
                {
                    var context = new AssetContext(r, SelectTree.Owner);
                    var asset = AssetDatabase.ImportAsset(context);
                    SelectTree.AddChild(asset, asset.Name);
                }
            },
            (object p) =>
            {
                return true;
            });
        }
    }
}
