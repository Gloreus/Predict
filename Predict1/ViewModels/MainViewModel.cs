
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagicGems.Core;

namespace Predict1.ViewModels;

public enum GemStatus
{
    None = 0,
    Hot,
    Cold,
}

[PseudoClasses (":hot", ":cold")]
public class GemControl : TemplatedControl
{
    public static readonly StyledProperty<int> GemColorProperty =
        AvaloniaProperty.Register<GemControl, int>(nameof(GemColor), defaultValue: 0);
    public int GemColor
    {
        get => GetValue(GemColorProperty);
        set => SetValue(GemColorProperty, value);
    }
    
    private GemStatus _gemStatus = GemStatus.None;
    public GemStatus GemStatus
    {
        get => _gemStatus;
        set
        {
            _gemStatus = value;
            PseudoClasses.Set(":hot", value==GemStatus.Hot);
            PseudoClasses.Set(":cold", value==GemStatus.Cold);
        }
    }
    
    public static readonly StyledProperty<IBrush> GemBrushProperty =
        AvaloniaProperty.Register<GemControl, IBrush>(nameof(GemColor), defaultValue: Brushes.Black);

    public IBrush GemBrush
    {
        get => GetValue(GemBrushProperty);
        set => SetValue(GemBrushProperty, value);
    }

    public override string ToString()
    {
        return GemColor.ToString();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        var pen = new Pen(Brushes.Blue, 2, lineCap: PenLineCap.Square);


        var r = new Rect(0, 0, Bounds.Width - 4, Bounds.Height - 4);

        context.DrawRectangle(pen, r);
        var r1 = new Rect(2, 2, r.Width -2, r.Height - 2);
        
        context.FillRectangle(GemBrush, r1);
    }
}

public class Gem
{
    private readonly int _value;
    public int Color => int.Abs(_value);
    public bool Hot => _value > 0;
    public bool Light { get; set; } = false;

    public Gem(int color)
    {
        _value = color;
    }
}

public partial class MainViewModel : ViewModelBase
{
    private readonly GemDesk _desk;
    private readonly int _colorCount = 4;
    public int RowCount => _desk.RowCount;
    public int ColumnCount => _desk.ColumnCount;

    public Gem HotGem => new Gem(2);
    public Gem ColdGem => new Gem(-3);


    [ObservableProperty] private ObservableCollection<Gem> _gemList = new ObservableCollection<Gem>();

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