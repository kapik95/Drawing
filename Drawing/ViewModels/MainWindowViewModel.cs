using Avalonia.Animation;
using Avalonia.Input;
using Drawing.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Drawing.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private bool _isDrawing;
        private string _selectedMode;
        private readonly string _defaultItem = "Поверхности";
        private RenderCube _cube = new RenderCube();

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



    }
}
