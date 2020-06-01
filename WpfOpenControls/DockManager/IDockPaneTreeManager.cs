﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfOpenControls.DockManager.Controls;

namespace WpfOpenControls.DockManager
{
    internal delegate DockPane DelegateCreateDockPane();

    internal interface IDockPaneTreeManager
    {
        SelectablePane FindElementOfType(Type type, Grid parentGrid);

        DockPane ExtractDockPane(DockPane dockPane, out FrameworkElement frameworkElement);

        bool UngroupDockPane(DockPane dockPane, int index, double paneWidth);

        void Float(DockPane dockPane, bool drag, bool selectedTabOnly);

        SelectablePane FindSelectablePane(Grid grid, Point pointOnScreen);

        void Unfloat(FloatingPane floatingPane, SelectablePane selectedPane, WindowLocation windowLocation);

        void PinToolPane(UnpinnedToolData unpinnedToolData, WindowLocation defaultWindowLocation);

        void UnpinToolPane(ToolPaneGroup toolPaneGroup, out UnpinnedToolData unpinnedToolData, out WindowLocation toolListBoxLocation);

        void CreateDefaultLayout(List<UserControl> documentViews, List<UserControl> toolViews);

        void ValidateDockPanes(Grid grid, Dictionary<IViewModel, List<string>> viewModels, Type paneType);
    }
}
