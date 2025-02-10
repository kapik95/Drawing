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
            // ���������, ��� ������� ������������� ��������
            Debug.WriteLine("������ ���� ���������, delta = " + e.Delta);

            // ������������ ���
            _cube.Scale((float)e.Delta.Y * 0.1f);
        }
    }
}