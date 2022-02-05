namespace DataStructures;

public class TrieNode<TSymbol>
    where TSymbol : notnull
{
    private readonly Dictionary<TSymbol, TrieNode<TSymbol>> _childs = new ();

    public IReadOnlyDictionary<TSymbol, TrieNode<TSymbol>> Childs => _childs;
    public bool IsEnd { get; private set; }

    internal void Insert(IEnumerable<TSymbol> items)
    {
        var cur = this;

        foreach (var item in items)
        {
            if (!cur._childs.ContainsKey(item))
                cur._childs.Add(item, new TrieNode<TSymbol>());

            cur = cur._childs[item];
        }

        cur.IsEnd = true;
    }

    public bool Contains(IEnumerable<TSymbol> items)
    {
        var cur = this;

        foreach (var item in items)
        {
            if (!cur._childs.TryGetValue(item, out var child))
                return false;

            cur = child;
        }

        return cur.IsEnd;
    }
}