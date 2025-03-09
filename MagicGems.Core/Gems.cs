namespace MagicGems.Core;

public class Gems
{
    private readonly int[] _items;

    private readonly int _colorsCount;
    private static readonly Random Rnd = new();

    public int this[int index] => _items[index];

    public Gems(int capacity, int colorsCount)
    {
        _items = new int[capacity];
        _colorsCount = colorsCount;
        FillGems();
    }

    /// Первоначальное заполнение игрового поля
    private void FillGems()
    {
        // Полные серии камней
        for (int i = 0; i < _items.Length / _colorsCount; i++)
        {
            for (int c = 0; c < _colorsCount; c++)
            {
                _items[i * _colorsCount + c] = c + 1;
            }
        }

        // Оставщиеся заполним случайным цветом
        int tail = _items.Length % _colorsCount;
        for (int i = _items.Length - tail; i < _items.Length; i++)
        {
            _items[i] = Rnd.Next(0, _colorsCount) + 1;
        }
    }

    /// <summary>
    ///  Перемешивает камни
    /// </summary>
    /// <returns>число "горячих" камней на поле</returns>
    public int Shuffle()
    {
        // Процент камней, что будут горячими
        int hotPercent = Rnd.Next(45, 60);
        // А остальные - Холодные
        int coldCount = _items.Length * (100 - hotPercent) / 100;
        for (int i = 0; i < coldCount; i++)
        {
            _items[i] = -_items[i];
        }

        Rnd.Shuffle(_items);
        return _items.Length - coldCount;
    }
}