// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using RizaEdShare.CoreSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.WindowSystem
{
    public class DockingWindowViewModel : ViewModelBase
    {
        public enum DockingType
        {
            None,
            Anchorable,
            Document
        }

        public enum DockingControlType
        {
            None,
            StartPage,
            AssetBrowser,
            LogViewer
        }

        private bool _isVisible;
        private string _title;
        private string _name;
        private string _contentId;
        private bool _isSelected;
        private bool _isActive;
        private string _toolTip;
        private bool _canClose;
        private Uri _iconSource;
        private DelegateCommand _closeCommand;
        private DelegateCommand _dockCommand;

        public DockingType Type { get; private set; }
        public DockingControlType ControlType { get; private set; }

        public bool IsVisible { get => _isVisible; set { _isVisible = value; NotifyPropertyChanged(); } }
        public string Title { get => _title; set { _title = value; NotifyPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }
        public string ContentId { get => _contentId; set { _contentId = value; NotifyPropertyChanged(); } }
        public bool IsSelected { get => _isSelected; set { _isSelected = value; NotifyPropertyChanged(); } }
        public bool IsActive { get => _isActive; set { _isActive = value; NotifyPropertyChanged(); } }
        public string ToolTip { get => _toolTip; set { _toolTip = value; NotifyPropertyChanged(); } }
        public Uri IconSource { get => _iconSource; set { _iconSource = value; NotifyPropertyChanged(); } }
        public bool CanClose { get => _canClose; set { _canClose = value; NotifyPropertyChanged(); } }
        public DelegateCommand CloseCommand { get => _closeCommand; set { _closeCommand = value; NotifyPropertyChanged(); } }

        public DelegateCommand DockCommand { get => _dockCommand; set { _dockCommand = value; NotifyPropertyChanged(); } }

        public DockingWindowViewModel(DockingType type, DockingControlType controlType) : base()
        {
            Type = type;
            ControlType = controlType;
            CanClose = true;
            IsActive = true;
            IsVisible = true;
        }

    }
}
