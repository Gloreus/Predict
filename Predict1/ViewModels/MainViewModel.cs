using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Shapes;
using MagicGems.Core;

namespace Predict1.ViewModels;

[PseudoClasses(":Hot", ":Light")]
public class Gem : Rectangle
{
    public int Color { get; }
    private bool _hot = false;

    public bool Hot
    {
        get => _hot;
        set
        {
            _hot = value;
            PseudoClasses.Set(":Hot", value);
        }
    }

    private bool _ligth = false;

    public bool Ligth
    {
        get => _ligth;
        set
        {
            _ligth = value;
            PseudoClasses.Set(":Ligth", value);
        }
    }

    public Gem(int color, bool hot)
    {
        Color = color;
        Hot = hot;
    }

    public Gem() : this(0, false)
    {
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

    private ObservableCollection<Gem> GemList { get; } = new ObservableCollection<Gem>();

    public MainViewModel()
    {
        _desk = new GemDesk(5, 5, _gemColors.Count);
        GemList.Clear();
        foreach (int i in _desk.Items)
        {
            Gem g = new Gem(i, false);
            GemList.Add(g);
        }
    }
}