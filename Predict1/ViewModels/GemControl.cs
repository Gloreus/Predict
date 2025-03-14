using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Predict1.ViewModels;

[PseudoClasses(":hot", ":cold", ":mark")]
public class GemControl : TemplatedControl
{
    public static readonly DirectProperty<GemControl, Gem> GemProperty =
        AvaloniaProperty.RegisterDirect<GemControl, Gem>(
            nameof(Gem), t => t.Gem, (t, v) => t.Gem = v);
    
    private Gem _gem;

    public Gem Gem
    {
        get => _gem;
        set
        {
            var old = Gem.GemColor;
            SetAndRaise(GemProperty, ref _gem, value);
            RaisePropertyChanged(GemColorProperty, old, value.GemColor);
        }
    }

    public static readonly DirectProperty<GemControl, int> GemColorProperty =
        AvaloniaProperty.RegisterDirect<GemControl, int>(
            nameof(GemColor), t => t.Gem.GemColor);
    public int GemColor => Gem.GemColor;   
    

    public static readonly StyledProperty<IBrush> GemBrushProperty =
        AvaloniaProperty.Register<GemControl, IBrush>(nameof(GemBrush), defaultValue: Brushes.Black);
    public IBrush GemBrush
    {
        get => GetValue(GemBrushProperty);
        set => SetValue(GemBrushProperty, value);
    }

    public override string ToString()
    {
        return Gem.GemColor.ToString();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        var pen = new Pen(Brushes.Blue, 2, lineCap: PenLineCap.Square);


        var r = new Rect(0, 0, Bounds.Width - 4, Bounds.Height - 4);

        context.DrawRectangle(pen, r);
        var r1 = new Rect(2, 2, r.Width - 2, r.Height - 2);

        context.FillRectangle(GemBrush, r1);
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":hot", Gem.Status == GemStatus.Hot);
        PseudoClasses.Set(":cold", Gem.Status == GemStatus.Cold);
        PseudoClasses.Set(":mark", Gem.Marked);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == GemProperty)
        {
            UpdatePseudoClasses();
            InvalidateVisual();
        }
    }
}