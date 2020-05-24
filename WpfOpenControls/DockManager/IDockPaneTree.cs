﻿using System.Windows;
using System.Windows.Controls;

namespace WpfOpenControls.DockManager
{
    internal interface IDockPaneTree
    {
        void FrameworkElementRemoved(FrameworkElement frameworkElement);
        Grid RootPane { get; set; }
        Grid ParentGrid { get; }
        UIElementCollection Children { get; }
    }
}