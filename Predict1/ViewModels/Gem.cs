using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Predict1.ViewModels;

public enum GemStatus
{
    None = 0,
    Hot,
    Cold,
}

public struct Gem
{
    private readonly int _value;
    public int GemColor => int.Abs(_value);
    public GemStatus Status { get; set; } = GemStatus.None;
    public bool Marked { get; set; } = false;

    public Gem(int value)
    {
        _value = value;
    }

    public Gem(int value, GemStatus status)
    {
        _value = value;
        Status = status;
    }
}