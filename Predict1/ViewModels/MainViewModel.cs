
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagicGems.Core;

namespace Predict1.ViewModels;



public partial class MainViewModel : ViewModelBase
{
    private readonly GemDesk _desk;
    private readonly int _colorCount = 4;
    public int RowCount => _desk.RowCount;
    public int ColumnCount => _desk.ColumnCount;

    public Gem HotGem => new Gem(2);
    
    [ObservableProperty]
    private Gem _coldGem;


    [ObservableProperty]
    private ObservableCollection<Gem> _gemList = new ObservableCollection<Gem>();

    public MainViewModel()
    {
        _desk = new GemDesk(5, 5, _colorCount);
        RefreshGems();
        ColdGem = new Gem(3)
        {
            Status = GemStatus.None,
            Marked = false,
        };
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
        // _ = _desk.Shuffle();
        // RefreshGems();
        ColdGem = new Gem(2)
        {
            Status = GemStatus.Cold,
            Marked = true,
        };
    }

    [RelayCommand]
    private void SetHot()
    {
        ColdGem = ColdGem with { Status = GemStatus.Hot };
    }
}