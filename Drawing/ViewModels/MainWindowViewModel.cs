using Drawing.Models;
using Drawing.Views;
using System.Collections.Generic;
using static Drawing.Models.RenderCube;

namespace Drawing.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private bool _isDrawing;
        private string _selectedMode;
        private readonly string _defaultItem = "Поверхности";


        public RelayCommand DrawingStartCommand { get; }
        public RelayCommand DrawingEndCommand { get; }

        public List<string> Modes { get; } = new List<string>
        {
        "Ребра",
        "Ребра и поверхности",
        "Поверхности"
        };

        public bool IsDrawing
        {
            get => _isDrawing;
            private set
            {
                if (_isDrawing != value)
                {
                    _isDrawing = value;
                    OnPropertyChanged(nameof(IsDrawingControlsEnabled));
                    DrawingStartCommand.RaiseCanExecuteChanged();
                    DrawingEndCommand.RaiseCanExecuteChanged();
                    MainWindow.Drawing(_isDrawing); 

                    RenderCube.Restart();
                }
            }
        }

        public string SelectedMode
        {
            get => _selectedMode;
            set
            {
                if (_selectedMode != value)
                {
                    _selectedMode = value;
                    OnPropertyChanged(nameof(SelectedMode));

                    UpdateRendering();
                    RenderCube.Restart();
                }
            }
        }

        public bool IsDrawingControlsEnabled => IsDrawing;


        public MainWindowViewModel()
        {
            DrawingStartCommand = new RelayCommand(DrawingStart);
            DrawingEndCommand = new RelayCommand(DrawingEnd);
            SelectedMode = _defaultItem;
        }

        private void DrawingStart()
        {
            IsDrawing = true;
            SelectedMode = _defaultItem;

        }

        private void DrawingEnd()
        {
            IsDrawing = false;
            SelectedMode = null;
        }

        public void UpdateRendering()
        {
            switch (SelectedMode)
            {
                case "Ребра":
                    RenderCube.SetRenderMode(RenderMode.Edges);
                    break;
                case "Ребра и поверхности":
                    RenderCube.SetRenderMode(RenderMode.FacesAndEdges);
                    break;
                case "Поверхности":
                    RenderCube.SetRenderMode(RenderMode.Faces);
                    break;
                default:
                    RenderCube.SetRenderMode(RenderMode.Faces);
                    break;
            }
        }
    }
}
