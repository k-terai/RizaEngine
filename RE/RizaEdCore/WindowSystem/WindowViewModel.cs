// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using RizaEdCore.CoreSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace RizaEdCore.WindowSystem
{
    public abstract class WindowViewModel : ViewModelBase
    {
        private string _title;
        private bool _isMaximize;
        private bool _isTopMost;
        private Uri _iconUri;
        private bool _isDialogMode;

        private float _width;
        private float _height;
        private float _minWidth;
        private float _minHeight;

        private Uri _minimizeImageUri;
        private Uri _maximizeImageUri;
        private Uri _restoreImageUri;
        private Uri _closeImageUri;

        private bool _enableToolBar;

#if WPF
        private WindowState _state;
        private ResizeMode _resizeModeType;
        // private WindowStartupLocation _startupLocation;
        private WindowStyle style;
#endif

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsMaximize
        {
            get => _isMaximize;
            set
            {
                _isMaximize = value;
                NotifyPropertyChanged();
            }
        }
        public Uri IconUri { get => _iconUri; set { _iconUri = value; NotifyPropertyChanged(); } }
        public bool IsTopMost { get => _isTopMost; set { _isTopMost = value; NotifyPropertyChanged(); } }
        public bool IsDialogMode { get => _isDialogMode; set { _isDialogMode = value; NotifyPropertyChanged(); } }

        public float Width { get => _width; set { _width = value; NotifyPropertyChanged(); } }
        public float Height { get => _height; set { _height = value; NotifyPropertyChanged(); } }
        public float MinWidth { get => _minWidth; set { _minWidth = value; NotifyPropertyChanged(); } }
        public float MinHeight { get => _minHeight; set { _minHeight = value; NotifyPropertyChanged(); } }

        public Uri MinimizeImageUri { get => _minimizeImageUri; set { _minimizeImageUri = value; NotifyPropertyChanged(); } }
        public Uri MaximizeImageUri { get => _maximizeImageUri; set { _maximizeImageUri = value; NotifyPropertyChanged(); } }
        public Uri RestoreImageUri { get => _restoreImageUri; set { _restoreImageUri = value; NotifyPropertyChanged(); } }
        public Uri CloseImageUri { get => _closeImageUri; set { _closeImageUri = value; NotifyPropertyChanged(); } }

        public bool EnableToolBar
        {
            get => _enableToolBar;
            set
            {
                _enableToolBar = value;
                NotifyPropertyChanged();
            }
        }



#if WPF
        public ResizeMode ResizeModeType { get => _resizeModeType; set { _resizeModeType = value; NotifyPropertyChanged(); } 

        public WindowStyle Style { get => style; set { style = value; NotifyPropertyChanged(); } }

        public WindowState State { get => _state; set { _state = value; NotifyPropertyChanged(); } }
#endif

        public WindowViewModel() : base()
        {
            Title = string.Empty;
            IsTopMost = false;
            IsDialogMode = false;
            Width = MinWidth = 1000;
            Height = MinHeight = 650;
            EnableToolBar = true;

#if WPF
            State = System.Windows.WindowState.Maximized;
            ResizeModeType = ResizeMode.CanResize;
            Style = WindowStyle.ThreeDBorderWindow;
#endif

        }
    }
}
