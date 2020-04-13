﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDockManagerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.MainViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _layoutManager.Initialise();

            //_layoutManager.SaveLayout(out System.Xml.XmlDocument xmlDocument, "C:\\Temp\\Layout.xml");
            //_layoutManager.LoadLayout(out System.Xml.XmlDocument xmlDocument_Loaded, "C:\\Temp\\Layout.xml");
            //_layoutManager.SaveLayout(out System.Xml.XmlDocument xmlDocument_saved, "C:\\Temp\\Layout_2.xml");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_layoutManager != null)
            {
                _layoutManager.Shutdown();
            }
        }

        private void LoadLayout()
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog == null)
            {
                return;
            }

            //dialog.FileName = viewModel.FileName;
            dialog.Filter = "Layout Files (*.xml)|*.xml";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            try
            {
                _layoutManager.LoadLayout(out System.Xml.XmlDocument xmlDocument_saved, dialog.FileName);
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show("Unable to load layout: " + exception.Message);
            }
        }

        private void SaveLayout()
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog == null)
            {
                return;
            }

            //dialog.FileName = viewModel.FileName;
            dialog.Filter = "Layout Files (*.xml)|*.xml";
            dialog.CheckFileExists = false;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            try
            {
                _layoutManager.SaveLayout(out System.Xml.XmlDocument xmlDocument_saved, dialog.FileName);
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show("Unable to save layout: " + exception.Message);
            }
        }

        private void _buttonMenu_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = null;

            menuItem = new MenuItem();
            menuItem.Header = "Save Layout";
            menuItem.IsChecked = false;
            menuItem.Command = new WpfDockManagerDemo.DockManager.Command(delegate { SaveLayout(); }, delegate { return true; });
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Load Layout";
            menuItem.IsChecked = false;
            menuItem.Command = new WpfDockManagerDemo.DockManager.Command(delegate { LoadLayout(); }, delegate { return true; });
            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Freeze Aspect Ratio";
            menuItem.IsChecked = false;
            contextMenu.Items.Add(menuItem);

            contextMenu.IsOpen = true;
        }
    }
}
