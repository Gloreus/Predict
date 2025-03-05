using System;
using System.Collections.Generic;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;


namespace Predict1.ViewModels;

public enum GemKind
{
    White,
    Red,
    Blue,
    Yellow,
}

public class Gem
{
    public int Status { get; set; } = 0;
    public GemKind Kind { get; set; } = 0;
}

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
    public const int ColCount = 14;
    public const int RowCount = 14;

    static readonly IImmutableSolidColorBrush[] gemColors =
    [
        Brushes.Beige,
        Brushes.Yellow,
        Brushes.Red,
        Brushes.Indigo
    ];
    
    [ObservableProperty]
    private List<Gem> _gems = new List<Gem>();

    public MainViewModel()
    {
        Random rnd = new();
        for (int i = 0; i < RowCount; i++)
        for (int j = 0; j < ColCount; j++)
        {
            int c = rnd.Next(0, gemColors.Length);
            _gems.Add(new Gem {Status = 0, Kind = (GemKind) c });
        }
    }
}