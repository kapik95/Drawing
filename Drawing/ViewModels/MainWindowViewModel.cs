﻿using System.Collections.Generic;

namespace Drawing.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private bool _isDrawing;
        private string _selectedMode;

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
                    //OnPropertyChanged(nameof(IsDrawing));
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
            SelectedMode = "Поверхности";
        }

        private void DrawingStart()
        {
            IsDrawing = true;
            SelectedMode = "Поверхности"; // По умолчанию

        }

        private void DrawingEnd()
        {
            IsDrawing = false;
            SelectedMode = null;
        }
    }
}
