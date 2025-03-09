using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagicGems.Core;

namespace Predict1.ViewModels;


public class GemControl:Rectangle
{
    public int GemColor { get; set; }
    private bool _hot = false;
    public static readonly StyledProperty<int> GemColorProperty =
        AvaloniaProperty.Register<GemControl, int>(nameof(GemColor), defaultValue: 0);
    public bool Hot
    {   
        get => _hot;
        set
        {
            _hot = value;
        }
    }

    private bool _ligth = false;

    public bool Ligth
    {
        get => _ligth;
        set
        {
            _ligth = value;
        }
    }

    public GemControl(int color, bool hot)
    {
        GemColor = color;
        Hot = hot;
        Ligth = false;
    }

    public GemControl() : this(0, false)
    {
    }
    
    public override string ToString()
    {
        return GemColor.ToString();
    }
}

public class Gem
{
    public int Color { get; set; } = 0;
    public bool Hot { get; set; } = false;

    public Gem(int color, bool hot)
    {
        Color = color;
        Hot = hot;
    }
}

public partial class MainViewModel : ViewModelBase
{
    private readonly List<string> _gemColors =
    [
        "White",
        "Red",
        "Blue",
        "Yellow",
    ];

    private readonly GemDesk _desk;
    public int RowCount { get => _desk.RowCount; }
    public int ColumnCount { get => _desk.ColumnCount; }

    [ObservableProperty]
    private ObservableCollection<Gem> _gemList  = new ObservableCollection<Gem>();

    public MainViewModel()
    {
        _desk = new GemDesk(5, 5, _gemColors.Count);
        RefreshGems();
    }

    private void RefreshGems()
    {
        GemList.Clear();
        foreach (int i in _desk.Items)
        {
            Gem g = new Gem(i, false);
            GemList.Add(g);
        }
    }

    [RelayCommand]
    private void Start()
    {
        _ = _desk.Shuffle();
        RefreshGems();
    }
}