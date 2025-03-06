using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
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
    public bool IsHot { get; set; } = false;
    public GemKind Kind { get; set; } = GemKind.White;
    public override string ToString()
    {
        return $"Gem_{Kind}";
    }
}

public class GemHelper
{
    public int KindsCount => Enum.GetNames(typeof(GemKind)).Length; // Количество видов камней
    public Gem GetNext(int n) => new() { Status = 0, Kind = (GemKind) (n % KindsCount) };
    public Gem GetRandom() => new() { Status = 0, Kind = (GemKind) Random.Shared.Next( KindsCount) };
}

public partial class MainViewModel : ViewModelBase
{
    private GemHelper _gemHelper = new GemHelper();
    private static Random rnd = new Random();
    
    private Gem[,] _gemDesk;
    public static int ColCount { get; set; } = 3;
    public static int RowCount { get; set; } = 5;


    public IEnumerable<Gem> Gems
    {
        get
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    yield return _gemDesk[i, j];
                }
            }
        }
    } 

    
    private void InitDesk(int rowCount, int colCount)
    {
        if (rowCount <= 0 || colCount <= 0)
            throw new ArgumentException("RowCount and ColCount must be greater than 0");
        
        int len = Enum.GetNames(typeof(GemKind)).Length; // Количество видов камней


        int fullSeries = (rowCount * colCount / _gemHelper.KindsCount) * _gemHelper.KindsCount;
        int n = 0;
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                if (n < fullSeries)
                {
                    _gemDesk[i, j] = _gemHelper.GetNext(n);
                    n++;
                }
                else
                {
                    _gemDesk[i, j] = _gemHelper.GetRandom();
                }
            }
        }
    }

    private void Suffle()
    {
        Gem[] items = _gemDesk.Cast<Gem>().ToArray();
        rnd.Shuffle<Gem>(items);
        for (int i = 0; i < RowCount; i++)
        for (int j = 0; j < ColCount; j++)
        {
            _gemDesk[i, j] = items[i * ColCount + j];
            _gemDesk[i, j].IsHot = (rnd.Next(0, 2) == 0);
        }
    }

    public MainViewModel()
    {
        _gemDesk = new Gem[RowCount, ColCount];
        InitDesk(RowCount, ColCount);
        Suffle();
    }
}