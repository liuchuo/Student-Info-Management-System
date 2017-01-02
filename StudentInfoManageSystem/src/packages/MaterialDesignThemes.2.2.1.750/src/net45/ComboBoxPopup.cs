﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal enum ComboBoxPopupPlacement
    {
        Undefined,
        Down,
        Up, 
        Classic
    }

    internal class ComboBoxPopup : Popup
    {
        #region UpContentTemplate property

        public static readonly DependencyProperty UpContentTemplateProperty
            = DependencyProperty.Register(nameof(UpContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null, CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement.Classic)));

        public ControlTemplate UpContentTemplate
        {
            get { return (ControlTemplate)GetValue(UpContentTemplateProperty); }
            set { SetValue(UpContentTemplateProperty, value); }
        }

        #endregion

        #region DownContentTemplate region

        public static readonly DependencyProperty DownContentTemplateProperty
            = DependencyProperty.Register(nameof(DownContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null, CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement.Down)));

        public ControlTemplate DownContentTemplate
        {
            get { return (ControlTemplate)GetValue(DownContentTemplateProperty); }
            set { SetValue(DownContentTemplateProperty, value); }
        }

        #endregion

        #region ClassicContentTemplate property

        public static readonly DependencyProperty ClassicContentTemplateProperty
            = DependencyProperty.Register(nameof(ClassicContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null, CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement.Up)));

        public ControlTemplate ClassicContentTemplate
        {
            get { return (ControlTemplate)GetValue(ClassicContentTemplateProperty); }
            set { SetValue(ClassicContentTemplateProperty, value); }
        }

        #endregion

        #region UpVerticalOffset property

        public static readonly DependencyProperty UpVerticalOffsetProperty
            = DependencyProperty.Register(nameof(UpVerticalOffset),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double UpVerticalOffset
        {
            get { return (double)GetValue(UpVerticalOffsetProperty); }
            set { SetValue(UpVerticalOffsetProperty, value); }
        }

        #endregion

        #region DownVerticalOffset property

        public static readonly DependencyProperty DownVerticalOffsetProperty
            = DependencyProperty.Register(nameof(DownVerticalOffset),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double DownVerticalOffset
        {
            get { return (double)GetValue(DownVerticalOffsetProperty); }
            set { SetValue(DownVerticalOffsetProperty, value); }
        }

        #endregion

        #region PopupPlacement property

        public static readonly DependencyProperty PopupPlacementProperty
            = DependencyProperty.Register(nameof(PopupPlacement),
                typeof(ComboBoxPopupPlacement),
                typeof(ComboBoxPopup),
                new PropertyMetadata(ComboBoxPopupPlacement.Undefined, PopupPlacementPropertyChangedCallback));

        public ComboBoxPopupPlacement PopupPlacement
        {
            get { return (ComboBoxPopupPlacement)GetValue(PopupPlacementProperty); }
            set { SetValue(PopupPlacementProperty, value); }
        }

        #endregion
        
        #region Background property

        private static readonly DependencyPropertyKey BackgroundPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Background", typeof(Brush), typeof(ComboBoxPopup),
                new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty BackgroundProperty =
            BackgroundPropertyKey.DependencyProperty;

        public Brush Background
        {
            get { return (Brush) GetValue(BackgroundProperty); }
            private set { SetValue(BackgroundPropertyKey, value); }
        }

        #endregion

        #region DefaultVerticalOffset

        public static readonly DependencyProperty DefaultVerticalOffsetProperty
            = DependencyProperty.Register(nameof(DefaultVerticalOffset),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double DefaultVerticalOffset
        {
            get { return (double)GetValue(DefaultVerticalOffsetProperty); }
            set { SetValue(DefaultVerticalOffsetProperty, value); }
        }

        #endregion

        #region VisiblePlacementWidth

        public double VisiblePlacementWidth
        {
            get { return (double)GetValue(VisiblePlacementWidthProperty); }
            set { SetValue(VisiblePlacementWidthProperty, value); }
        }

        public static readonly DependencyProperty VisiblePlacementWidthProperty
            = DependencyProperty.Register(nameof(VisiblePlacementWidth),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        #endregion

        #region ClassicMode property

        public static readonly DependencyProperty ClassicModeProperty
            = DependencyProperty.Register(
                nameof(ClassicMode),
                typeof(bool),
                typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(true));

        public bool ClassicMode
        {
            get { return (bool)GetValue(ClassicModeProperty); }
            set { SetValue(ClassicModeProperty, value); }
        }

        #endregion

        public ComboBoxPopup()
        {
            CustomPopupPlacementCallback = ComboBoxCustomPopupPlacementCallback;

            DependencyPropertyDescriptor.FromProperty(ComboBoxPopup.ChildProperty, typeof(ComboBoxPopup))
                .AddValueChanged(this,
                    delegate
                    {
                        if (PopupPlacement != ComboBoxPopupPlacement.Undefined)
                        {
                            UpdateChildTemplate(PopupPlacement);
                        }
                    });
        }

        private void SetupBackground(IEnumerable<DependencyObject> visualAncestry)
        {
            var background = visualAncestry
                .Select(v => (v as Control)?.Background ?? (v as Panel)?.Background ?? (v as Border)?.Background)
                .FirstOrDefault(v => v != null && !Equals(v, Brushes.Transparent) && v is SolidColorBrush);

            if (background != null)
            {
                Background = background;
            }
        }

        private void SetupVisiblePlacementWidth(IEnumerable<DependencyObject> visualAncestry)
        {
            var parent = visualAncestry.OfType<Panel>().ElementAt(1);
            VisiblePlacementWidth = TreeHelper.GetVisibleWidth((FrameworkElement)PlacementTarget, parent);
        }

        private CustomPopupPlacement[] ComboBoxCustomPopupPlacementCallback(
            Size popupSize, Size targetSize, Point offset)
        {
            var visualAncestry = PlacementTarget.GetVisualAncestry().ToList();

            SetupBackground(visualAncestry);

            SetupVisiblePlacementWidth(visualAncestry);

            var data = GetPositioningData(visualAncestry, popupSize, targetSize, offset);

            if (ClassicMode ||
                (data.LocationX + data.PopupSize.Width - data.RealOffsetX > data.ScreenWidth
                || data.LocationX - data.RealOffsetX < 0))
            {
                PopupPlacement = ComboBoxPopupPlacement.Classic;

                return new[] { GetClassicPopupPlacement(this, data) };
            }
            else if (data.LocationY + data.PopupSize.Height > data.ScreenHeight)
            {
                PopupPlacement = ComboBoxPopupPlacement.Up;

                return new[] { GetUpPopupPlacement(this, data) };
            }
            else
            {
                PopupPlacement = ComboBoxPopupPlacement.Down;

                return new[] { GetDownPopupPlacement(this, data) };
            }
        }

        private void SetChildTemplateIfNeed(ControlTemplate template)
        {
            var contentControl = Child as ContentControl;
            if (contentControl == null) return;
            //throw new InvalidOperationException($"The type of {nameof(Child)} must be {nameof(ContentControl)}");

            if (!ReferenceEquals(contentControl.Template, template))
            {
                contentControl.Template = template;
            }
        }

        private PositioningData GetPositioningData(IEnumerable<DependencyObject> visualAncestry, Size popupSize, Size targetSize, Point offset)
        {
            var locationFromScreen = PlacementTarget.PointToScreen(new Point(0, 0));

            var mainVisual = visualAncestry.OfType<Visual>().LastOrDefault();
            if (mainVisual == null) throw new ArgumentException($"{nameof(visualAncestry)} must contains unless one {nameof(Visual)} control inside.");

            var screenWidth = (int)DpiHelper.TransformToDeviceX(mainVisual, SystemParameters.PrimaryScreenWidth);
            var screenHeight = (int)DpiHelper.TransformToDeviceY(mainVisual, SystemParameters.PrimaryScreenHeight);

            var locationX = (int)locationFromScreen.X % screenWidth;
            var locationY = (int)locationFromScreen.Y % screenHeight;

            double offsetX;
            const int rtlHorizontalOffset = 20;

            if (FlowDirection == FlowDirection.LeftToRight)
                offsetX = DpiHelper.TransformToDeviceX(mainVisual, offset.X);
            else
                offsetX = DpiHelper.TransformToDeviceX(mainVisual,
                    offset.X - targetSize.Width - rtlHorizontalOffset);

            return new PositioningData(
                mainVisual, offsetX,
                popupSize, targetSize,
                locationX, locationY,
                screenHeight, screenWidth);
        }

        private static PropertyChangedCallback CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement popupPlacement)
        {
            return delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var popup = d as ComboBoxPopup;
                if (popup == null) return;

                var template = e.NewValue as ControlTemplate;
                if (template == null) return;

                if (popup.PopupPlacement == popupPlacement)
                {
                    popup.SetChildTemplateIfNeed(template);
                }
            };
        }

        private void UpdateChildTemplate(ComboBoxPopupPlacement placement)
        {
            switch (placement)
            {
                case ComboBoxPopupPlacement.Classic:
                    SetChildTemplateIfNeed(ClassicContentTemplate);
                    break;
                case ComboBoxPopupPlacement.Down:
                    SetChildTemplateIfNeed(DownContentTemplate);
                    break;
                case ComboBoxPopupPlacement.Up:
                    SetChildTemplateIfNeed(UpContentTemplate);
                    break;
//                default:
//                    throw new NotImplementedException($"Unexpected value {placement} of the {nameof(PopupPlacement)} property inside the {nameof(ComboBoxPopup)} control.");
            }
        }

        private static void PopupPlacementPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var popup = d as ComboBoxPopup;
            if (popup == null) return;

            if (!(e.NewValue is ComboBoxPopupPlacement)) return;
            var placement = (ComboBoxPopupPlacement)e.NewValue;
            popup.UpdateChildTemplate(placement);
        }

        private static CustomPopupPlacement GetClassicPopupPlacement(ComboBoxPopup popup, PositioningData data)
        {
            var defaultVerticalOffsetIndepent = DpiHelper.TransformToDeviceY(data.MainVisual, popup.DefaultVerticalOffset);
            var newY = data.LocationY + data.PopupSize.Height > data.ScreenHeight
                ? -(defaultVerticalOffsetIndepent + data.PopupSize.Height)
                : defaultVerticalOffsetIndepent + data.TargetSize.Height;

            return new CustomPopupPlacement(new Point(data.OffsetX, newY), PopupPrimaryAxis.Horizontal);
        }

        private static CustomPopupPlacement GetDownPopupPlacement(ComboBoxPopup popup, PositioningData data)
        {
            var downVerticalOffsetIndepent = DpiHelper.TransformToDeviceY(data.MainVisual, popup.DownVerticalOffset);

            return new CustomPopupPlacement(new Point(data.OffsetX, downVerticalOffsetIndepent), PopupPrimaryAxis.None);
        }

        private static CustomPopupPlacement GetUpPopupPlacement(ComboBoxPopup popup, PositioningData data)
        {
            var upVerticalOffsetIndepent = DpiHelper.TransformToDeviceY(data.MainVisual, popup.UpVerticalOffset);
            var newY = upVerticalOffsetIndepent - data.PopupSize.Height + data.TargetSize.Height;

            return new CustomPopupPlacement(new Point(data.OffsetX, newY), PopupPrimaryAxis.None);
        }

        private struct PositioningData
        {
            public Visual MainVisual { get; }
            public double OffsetX { get; }
            public double RealOffsetX => (PopupSize.Width - TargetSize.Width) / 2.0;
            public Size PopupSize { get; }
            public Size TargetSize { get; }
            public double LocationX { get; }
            public double LocationY { get; }
            public double ScreenHeight { get; }
            public double ScreenWidth { get; }

            public PositioningData(
                Visual mainVisual,
                double offsetX,
                Size popupSize, Size targetSize,
                double locationX, double locationY,
                double screenHeight, double screenWidth)
            {
                MainVisual = mainVisual;
                OffsetX = offsetX;
                PopupSize = popupSize; TargetSize = targetSize;
                LocationX = locationX; LocationY = locationY;
                ScreenWidth = screenWidth; ScreenHeight = screenHeight;
            }
        }
    }
}
