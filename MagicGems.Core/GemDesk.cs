namespace MagicGems.Core;

public class Chain : List<int>;

public class GemDesk
{
    private readonly Gems _gems;
    public int RowCount { get; }
    public int ColumnCount { get; }
    public int ColorCount { get; }

    public GemDesk(int rowCount, int columnCount, int colorCount)
    {
        if (rowCount < 1 || columnCount < 1)
            throw new ArgumentException("Row and column count must be greater than 1");

        RowCount = rowCount;
        ColumnCount = columnCount;
        ColorCount = colorCount;
        _gems = new Gems(rowCount * columnCount, ColorCount);
    }

    /// Индексы соседних (верх-низ-право-лево) клеток по отношению к id
    private IEnumerable<int> NeighboringId(int id)
    {
        // left
        if (id % ColumnCount > 0)
            yield return id - 1;
        // right
        if (id % ColumnCount < ColumnCount - 1)
            yield return id + 1;
        // top
        if (id > ColumnCount)
            yield return id - ColumnCount;
        // bottom
        if (id < (RowCount - 1) * ColumnCount)
            yield return id + ColumnCount;
    }

    public List<Chain> GetChains()
    {
        List<Chain> chains = new();
        Queue<int> queue = new();
        bool[] visited = new bool[RowCount * ColumnCount];

        for (int i = 0; i < visited.Length; i++)
            visited[i] = false;
        int start = 0;

        while (start < visited.Length && !visited[start])
        {
            if (visited[start])
                break;
            // Начинаем поиск цепчки камней
            Chain chain = new();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                chain.Add(n);
                visited[n] = true;

                // Перебираем соседние клетки
                foreach (int p in NeighboringId(n))
                {
                    if (_gems[p] == _gems[n] && !visited[p]) queue.Enqueue(p); // В очередь на проверку..    
                }
            }

            chains.Add(chain);
            start = 0;
            while (start < visited.Length && visited[start]) start++;
        }

        return chains;
    }

    public Dictionary<int, int> GetTopChains(List<Chain> chains)
    {
        Dictionary<int, int> gemLength = new();
        foreach (var ch in chains)
        {
            if (ch.Count == 0) continue;
            int key = _gems[ch[0]];
            if (!gemLength.TryGetValue(key, out int len) || len < ch.Count)
                gemLength[key] = ch.Count;
        }

        return gemLength;
    }

    public int Shuffle() => _gems.Shuffle();
    public int this[int row, int column] => _gems[row * ColumnCount + column];

    public IEnumerable<int> Items
    {
        get
        {
            for (int i = 0; i < ColumnCount * RowCount; i++)
                yield return _gems[i];
        }
    }
}