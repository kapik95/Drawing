using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Drawing.Models;
using Drawing.ViewModels;
using System;
using System.Diagnostics;

namespace Drawing.Views
{
    public partial class MainWindow : Window
    {
        private bool _isDragging;
        private Point _lastPos;
        private static bool _isDrawing;

        public MainWindow()
        {
            InitializeComponent();
        }

        public static void Drawing(bool draw)
        {
            _isDrawing = draw;
            Debug.WriteLine(_isDrawing);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            if (_isDrawing == true) RenderCube.Scale((float)e.Delta.Y * 0.1f);
        }

        private void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if(_isDrawing == true) 
            {
                _isDragging = true;
                Debug.WriteLine(_isDragging);
                e.Pointer.Capture(this);
                _lastPos = e.GetPosition(null);
            }
        }

        protected override void OnPointerReleased( PointerReleasedEventArgs e)
        {
            _isDragging = false;
            e.Pointer.Capture(null);
            Debug.WriteLine(_isDragging);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            bool revers = false;
            if (!_isDragging)
                return;

            //Work out the change in position
            var pos = e.GetPosition(null);

            var deltaY = pos.X - _lastPos.X;
            var deltaX = pos.Y - _lastPos.Y;

            if (_lastPos.X > pos.X || _lastPos.Y > pos.Y)
            {
                if (revers = false)
                {
                    deltaX = 0.0f;
                    deltaY = 0.0f;
                    revers = true;
                }
            }
            revers = false;
            RenderCube.Rotate((float)deltaX, (float)deltaY);
        }
    }
}