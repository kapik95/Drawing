using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Drawing.Models;
using System;
using System.Diagnostics;

namespace Drawing.Views
{
    public partial class MainWindow : Window
    {
        private RenderCube _cube = new RenderCube();
        private bool _isDragging;
        private Point _lastPos;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        private void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            // Проверяем, что событие действительно получено
            Debug.WriteLine("Колесо мыши вращается, delta = " + e.Delta);

            // Масштабируем куб
            _cube.Scale((float)e.Delta.Y * 0.1f);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            _isDragging = true;
            e.Pointer.Capture(this);
            _lastPos = e.GetPosition(null);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            _isDragging = false;
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

            if (_lastPos.X >  pos.X || _lastPos.Y > pos.Y)
            {
                if(revers = false)
                {
                    deltaX = 0.0f;
                    deltaY = 0.0f;
                    revers = true;
                }
            }
            revers = false;
            Debug.WriteLine($"{deltaX} {deltaY}");
            _cube.Rotate((float)deltaX, (float)deltaY);
        }
    }
}