using RizaEdCore.AssetSystem;
using RizaEdCore.AssetSystem.Folders.Root;
using RizaEdCore.TreeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RizaWpfEditor.AssetBrowser
{
    /// <summary>
    /// AssetBrowserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class AssetBrowserControl : UserControl
    {
        private readonly double _minimumDragDistance = 10.0;
        private Point _lastLeftMouseButtonDownPoint;

        public UserControl Control => this;
        public AssetBrowserControlViewModel ViewModel { get => DataContext as AssetBrowserControlViewModel; }

        public AssetBrowserControl()
        {
            InitializeComponent();
        }

        private void AssetTreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            var vm = ViewModel;

            //Check ctrl key not inputed.
            if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down & (Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)
            {
                //Reset.
                foreach (var a in vm.MultiSelectTrees)
                {
                    a.IsSubSelected = false;
                }

                vm.MultiSelectTrees.Clear();
            }

            //Check shift key inputed.
            if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) == KeyStates.Down || (Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) == KeyStates.Down)
            {
                //Range select.
                if (vm.SelectTree != null)
                {
                    var newtree = e.NewValue as AssetTreeComponent;
                    var oldtree = vm.SelectTree;
                    var selects = TreeComponent<Asset>.RangeSelect(oldtree, newtree);
                    if (selects == null) return;

                    foreach (var t in selects)
                    {
                        if (!vm.MultiSelectTrees.Contains(t))
                        {
                            vm.MultiSelectTrees.Add(t as AssetTreeComponent);
                        }
                    }

                    return;
                }
            }

            vm.SelectTree = (e.NewValue as AssetTreeComponent);
            vm.SelectTree.IsSubSelected = true;

            if (!vm.MultiSelectTrees.Contains(vm.SelectTree))
            {
                vm.MultiSelectTrees.Add(vm.SelectTree);
            }
        }

        private void AssetTreeView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) { return; }
            _lastLeftMouseButtonDownPoint = e.GetPosition(AssetTreeView);
        }

        private void AssetTreeView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Released) { return; }
                if (ViewModel.SelectTree == null) return;

                var currentPosition = e.GetPosition(AssetTreeView);
                if (Math.Abs(currentPosition.X - _lastLeftMouseButtonDownPoint.X) <= _minimumDragDistance &&
                    Math.Abs(currentPosition.Y - _lastLeftMouseButtonDownPoint.Y) <= _minimumDragDistance)
                {
                    return;
                }

                //No drag root folder.
                if (ViewModel.MultiSelectTrees.Count(t => t.Owner is RootFolder) != 0)
                {
                    return;
                }

                DragDrop.DoDragDrop(AssetTreeView, ViewModel.SelectTree, DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                //Log.DebugException(ex);
            }

        }

        private void AssetTreeView_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.None;

            var destItem = e.OriginalSource as FrameworkElement;
            var desttree = destItem.DataContext as AssetTreeComponent;
            var sourcetree = e.Data.GetData(typeof(AssetTreeComponent)) as AssetTreeComponent;

            var removelist = new List<AssetTreeComponent>();
            foreach (var at in ViewModel.MultiSelectTrees)
            {
                foreach (var check in ViewModel.MultiSelectTrees)
                {
                    if (check != at && !removelist.Contains(check))
                    {
                        //The selection list contains parent folders = Parents are moved, so ignore.
                        if (at.IsChild(check))
                        {
                            removelist.Add(check);
                        }
                    }
                }
            }

            var moveassetarray = ViewModel.MultiSelectTrees.Where(t => !removelist.Contains(t)).ToArray();
            foreach (var ma in moveassetarray)
            {
                ma.ChangeParent(desttree);
            }

        }

        private void AssetButton_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button b)
            {
                if (b.DataContext is AssetTreeComponent at)
                {
                    ViewModel.SelectTree = at;
                }
            }
        }

        private void TreeViewTextbox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            Debug.Assert(textBox != null);

            var assetView = textBox.DataContext as AssetTreeComponent;
            Debug.Assert(assetView != null);

            if (assetView.IsEditMode && textBox.Visibility == Visibility.Visible)
            {
                textBox.Focus();
                textBox.SelectAll();
            }
        }

        private void TreeViewTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            Debug.Assert(textBox != null);

            var assetView = textBox.DataContext as AssetTreeComponent;

            //Null is posssible when preview key down event direct call this method.
            if (assetView == null || !assetView.IsEditMode)
            {
                return;
            }

            ViewModel.OnEditTempNameEnd?.Invoke(assetView);
        }

        private void TreeViewTextbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            Debug.Assert(textBox != null);

            var assetView = textBox.DataContext as AssetTreeComponent;
            Debug.Assert(assetView != null);

            if (assetView.IsEditMode && (Keyboard.GetKeyStates(Key.Enter) & KeyStates.Down) == KeyStates.Down)
            {
                TreeViewTextbox_LostFocus(sender, null);
            }
        }

    }
}
