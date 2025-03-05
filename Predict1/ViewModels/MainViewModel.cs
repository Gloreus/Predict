using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Metadata;
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
    public override string ToString()
    {
        return $"Gem_{Kind}";
    }
}

public class MyTemplateSelector : IDataTemplate
{
    public bool SupportsRecycling => false;
    [Content]
    public Dictionary<string, IDataTemplate> Templates {get;} = new Dictionary<string, IDataTemplate>();

    
    public bool Match(object? data)
    {
        return data is Gem;
    }

    public Control? Build(object? data)
    {
        if (data is Gem gem)
            return Templates[gem.ToString()].Build(data);
        else return null;
    }
}


public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";

    public static int ColCount { get; set; } = 14;
    public static int RowCount { get; set; } = 14;

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
            int c = rnd.Next(0, Enum.GetNames(typeof(GemKind)).Length);
            _gems.Add(new Gem {Status = 0, Kind = (GemKind) c });
        }
    }
}