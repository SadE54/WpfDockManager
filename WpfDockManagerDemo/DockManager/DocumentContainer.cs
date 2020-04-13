﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfDockManagerDemo.DockManager
{
    internal class DocumentContainer : Grid
    {
        public DocumentContainer()
        {

        }

        public event EventHandler TitleChanged;

        public int DocumentCount
        {
            get
            {
                int count = -1;
                if (Children.Count == 0)
                {
                    count = 0;
                }
                if (Children[0] is UserControl)
                {
                    count = 1;
                }
                if (Children[0] is TabControl)
                {
                    count = (Children[0] as TabControl).Items.Count;
                }

                return count;
             }
        }

        public string Title
        {
            get
            {
                if (Children[0] == null)
                {
                    return null;
                }

                IDocument iDocument = Children[0] as IDocument;
                if (iDocument != null)
                {
                    return (iDocument as IDocument).Title;
                }

                TabControl tabControl = Children[0] as TabControl;

                if (tabControl == null)
                {
                    throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Children[0] is not a valid type -> " + Children[0].GetType().FullName);
                }

                if (tabControl.SelectedItem == null)
                {
                    return null;
                }

                TabItem tabItem = tabControl.SelectedItem as TabItem;
                if (tabItem == null)
                {
                    return null;
                }

                UserControl userControl = tabItem.Content as UserControl;
                if (userControl == null)
                {
                    throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": selected item is not a User Control");
                }

                iDocument = userControl.DataContext as IDocument;
                if (iDocument == null)
                {
                    throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": User Control is not a document");
                }

                return iDocument.Title;
            }
        }

        public void AddUserControl(UserControl userControl)
        {
            if (Children.Count == 0)
            {
                Children.Add(userControl);
            }
            else if (Children[0] is UserControl)
            {
                UserControl userControl2 = Children[0] as UserControl;
                Children.RemoveAt(0);
                TabControl tabControl = new TabControl();
                tabControl.Padding = new System.Windows.Thickness(-2);
                tabControl.TabStripPlacement = System.Windows.Controls.Dock.Bottom;
                tabControl.SelectionChanged += delegate { TitleChanged?.Invoke(this, null); };
                Children.Add(tabControl);

                TabItem tabItem = new TabItem();
                tabItem.Content = userControl2;
                tabItem.Header = (userControl2.DataContext as IDocument).Title;
                tabControl.Items.Add(tabItem);

                tabItem = new TabItem();
                tabItem.Content = userControl;
                tabItem.Header = (userControl.DataContext as IDocument).Title;
                tabControl.Items.Add(tabItem);
            }
            else if (Children[0] is TabControl)
            {
                TabControl tabControl = Children[0] as TabControl;
                TabItem tabItem = new TabItem();
                tabItem.Content = userControl;
                tabItem.Header = (userControl.DataContext as IDocument).Title;
                tabControl.Items.Add(tabItem);
            }
            else
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Children[0] is not an expected type -> " + Children[0].GetType().FullName);
            }
        }

        public UserControl ExtractUserControl(int index)
        {
            if (Children.Count == 0)
            {
                return null;
            }

            UserControl userControl = null;
            if (Children[0] is UserControl)
            {
                userControl = Children[0] as UserControl;
                Children.RemoveAt(0);
            }
            else if (Children[0] is TabControl)
            {
                TabControl tabControl = Children[0] as TabControl;
                if (index >= tabControl.Items.Count)
                {
                    index = tabControl.Items.Count - 1;
                }
                if (index < 0)
                {
                    index = 0;
                }
                TabItem tabItem = (tabControl.Items[index] as TabItem);
                userControl = tabItem.Content as UserControl;
                tabItem.Content = null;
                tabControl.Items.RemoveAt(index);
                if (tabControl.Items.Count == 1)
                {
                    tabItem = (tabControl.Items[0] as TabItem);
                    UserControl userControl2 = tabItem.Content as UserControl;
                    tabItem.Content = null;
                    tabControl.Items.Remove(tabItem);
                    Children.Remove(tabControl);
                    Children.Add(userControl2);
                }
            }
            else
            {
                throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Children[0] is not an expected type -> " + Children[0].GetType().FullName);
            }

            return userControl;
        }

        public int GetUserControlCount()
        {
            if (Children.Count <= 0)
            {
                return 0;
            }
            else if (Children[0] is UserControl)
            {
                return 1;
            }
            else if (Children[0] is TabControl)
            {
                TabControl tabControl = Children[0] as TabControl;
                return (Children[0] as TabControl).Items.Count;
            }

            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Children[0] is not an expected type -> " + Children[0].GetType().FullName);
        }

        public int GetCurrentTabIndex()
        {
            if (Children.Count <= 0)
            {
                return -1;
            }
            else if (Children[0] is UserControl)
            {
                return -1;
            }
            else if (Children[0] is TabControl)
            {
                TabControl tabControl = Children[0] as TabControl;
                return tabControl.SelectedIndex;
            }

            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Children[0] is not an expected type -> " + Children[0].GetType().FullName);
        }

        public UserControl GetUserControl(int index)
        {
            if (Children.Count <= 0)
            {
                return null;
            }

            if (Children[0] is UserControl)
            {
                return (index == 0) ? Children[0] as UserControl : null;
            }
            else if (Children[0] is TabControl)
            {
                TabControl tabControl = Children[0] as TabControl;

                if (index >= tabControl.Items.Count)
                {
                    return null;
                }

                TabItem tabItem = (tabControl.Items[index] as TabItem);
                return tabItem.Content as UserControl;
            }

            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Children[0] is not an expected type -> " + Children[0].GetType().FullName);
        }
    }
}