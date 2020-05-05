﻿using System;
using System.Windows.Controls;

namespace WpfDockManagerDemo.DockManager
{
    internal interface IUserViewContainer
    {
        string Title { get; }
        void AddUserControl(UserControl userControl);
        UserControl ExtractUserControl(int index);
        int GetUserControlCount();
        int GetCurrentTabIndex();
        UserControl GetUserControl(int index);

        event EventHandler SelectionChanged;
        event EventHandler TabClosed;
    }
}