﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfDockManagerDemo.DockManager
{
    internal class ToolPaneGroup : DockPane
    {
        public ToolPaneGroup() : base(new ToolContainer())
        {
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            RowDefinitions.Add(new RowDefinition());
            RowDefinitions[0].Height = new GridLength(20, GridUnitType.Pixel);
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            Border = new Border();
            Border.VerticalAlignment = VerticalAlignment.Stretch;
            Border.HorizontalAlignment = HorizontalAlignment.Stretch;
            Border.Background = System.Windows.Media.Brushes.MidnightBlue;
            Border.BorderBrush = System.Windows.Media.Brushes.Black;
            Border.BorderThickness = new Thickness(1);
            Grid.SetRow(Border, 0);
            Grid.SetColumn(Border, 0);
            Grid.SetColumnSpan(Border, 5);
            Children.Add(Border);

            _titleLabel = new Label();
            _titleLabel.FontSize = 12;
            _titleLabel.Padding = new Thickness(4, 0, 0, 0);
            _titleLabel.VerticalAlignment = VerticalAlignment.Center;
            _titleLabel.Background = System.Windows.Media.Brushes.Transparent;
            _titleLabel.Foreground = System.Windows.Media.Brushes.White;
            Grid.SetRow(_titleLabel, 0);
            Grid.SetColumn(_titleLabel, 0);
            Children.Add(_titleLabel);

            Button menuButton = new Button();
            menuButton.Style = FindResource("styleHeaderMenuButton") as Style;
            menuButton.Click += delegate { DisplayGeneralMenu(); };
            Grid.SetRow(menuButton, 0);
            Grid.SetColumn(menuButton, 2);
            Children.Add(menuButton);

            _pinButton = new Button();
            _pinButton.LayoutTransform = new System.Windows.Media.RotateTransform();
            _pinButton.Style = FindResource("styleHeaderPinButton") as Style;
            _pinButton.Click += PinButton_Click;
            Grid.SetRow(_pinButton, 0);
            Grid.SetColumn(_pinButton, 3);
            Children.Add(_pinButton);

            Button closeButton = new Button();
            closeButton.Style = FindResource("styleHeaderCloseButton") as Style;
            Grid.SetRow(closeButton, 0);
            Grid.SetColumn(closeButton, 4);
            Panel.SetZIndex(closeButton, 99);
            Children.Add(closeButton);
            closeButton.Click += delegate { FireClose(); };

            IViewContainer.SelectionChanged += DocumentContainer_SelectionChanged;
            Grid.SetRow(IViewContainer as System.Windows.UIElement, 1);
            Grid.SetColumn(IViewContainer as System.Windows.UIElement, 0);
            Grid.SetColumnSpan(IViewContainer as System.Windows.UIElement, ColumnDefinitions.Count);
        }

        public void ShowAsUnPinned()
        {
            (_pinButton.LayoutTransform as System.Windows.Media.RotateTransform).Angle = 90.0;
            (_pinButton.LayoutTransform as System.Windows.Media.RotateTransform).CenterX = 0.5;
            (_pinButton.LayoutTransform as System.Windows.Media.RotateTransform).CenterY = 0.5;
        }

        public event EventHandler UnPinClick;

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            UnPinClick?.Invoke(this, null);
        }

        private void DocumentContainer_SelectionChanged(object sender, EventArgs e)
        {
            _titleLabel.Content = IViewContainer.Title;
        }

        protected Label _titleLabel;

        public string Title { get { return IViewContainer.Title; } }

        public Border Border { get; private set; }
        Button _pinButton;

        private bool _isHighlighted;
        public override bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }
            set
            {
                _isHighlighted = value;
                Border.Background = IsHighlighted ? System.Windows.Media.Brushes.Firebrick : System.Windows.Media.Brushes.SteelBlue;
            }
        }

        Point _mouseDownPosition;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _mouseDownPosition = e.GetPosition(this);
            base.OnMouseLeftButtonDown(e);
            System.Windows.Input.Mouse.Capture(this);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            System.Windows.Input.Mouse.Capture(this, CaptureMode.None);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (System.Windows.Input.Mouse.Captured == this)
            {
                Point mousePosition = e.GetPosition(this);
                double xdiff = mousePosition.X - _mouseDownPosition.X;
                double ydiff = mousePosition.Y - _mouseDownPosition.Y;
                if ((xdiff * xdiff + ydiff * ydiff) > 200)
                {

                    FireFloat(true);
                    System.Windows.Input.Mouse.Capture(this, CaptureMode.None);
                }
            }
        }
    }

}
