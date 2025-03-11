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

    public bool Hot { get; set; }

    public static readonly StyledProperty<int> GemColorProperty =
        AvaloniaProperty.Register<GemControl, int>(nameof(GemColor), defaultValue: 0);
    
    public static readonly StyledProperty<bool> HotProperty =
        AvaloniaProperty.Register<GemControl, bool>(nameof(Hot), defaultValue: false);
    
    public static readonly StyledProperty<bool> LightProperty =
        AvaloniaProperty.Register<GemControl, bool>(nameof(Gem.Light), defaultValue: false);

    public override string ToString()
    {
        return GemColor.ToString();
    }
}

public class Gem
{
    private readonly int _value;
    public int Color => int.Abs(_value);
    public bool Hot  => _value > 0;
    public bool Light { get; set; } = false;

    public Gem(int color)
    {
        _value = color;
    }
}

public partial class MainViewModel : ViewModelBase
{
    private readonly GemDesk _desk;
    private readonly int _colorCount =4;
    public int RowCount => _desk.RowCount;
    public int ColumnCount => _desk.ColumnCount;

    public Gem HotGem => new Gem(2);
    public Gem ColdGem => new Gem(-3);
    
    
    [ObservableProperty]
    private ObservableCollection<Gem> _gemList  = new ObservableCollection<Gem>();

    public MainViewModel()
    {
        _desk = new GemDesk(5, 5, _colorCount);
        RefreshGems();
    }

    private void RefreshGems()
    {
        GemList.Clear();
        foreach (int i in _desk.Items)
        {
            Gem g = new Gem(i);
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