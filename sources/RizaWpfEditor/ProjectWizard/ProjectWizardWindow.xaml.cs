﻿// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RizaWpfEditor.ProjectWizard
{
    /// <summary>
    /// ProjectWizardWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ProjectWizardWindow : Window, IProjectWizard
    {
        public ProjectWizardWindow()
        {
            InitializeComponent();
        }

        public void ShowWindow()
        {
            Show();
        }
    }
}