namespace DataStructures;

public class Trie<TInput, TItem>
    where TItem : notnull
{
    private class TrieNode
    {
        public readonly Dictionary<TItem, TrieNode> Childs = new Dictionary<TItem, TrieNode>();

        public bool IsEnd { get; private set; }

        internal void Insert(IEnumerable<TItem> items)
        {
            var cur = this;

            foreach (var item in items)
            {
                if (!cur.Childs.ContainsKey(item))
                    cur.Childs.Add(item, new TrieNode());

                cur = cur.Childs[item];
            }

            cur.IsEnd = true;
        }

        public bool Contains(IEnumerable<TItem> items)
        {
            var cur = this;

            foreach (var item in items)
            {
                if (!cur.Childs.TryGetValue(item, out var child))
                    return false;

                cur = child;
            }

            return cur.IsEnd;
        }
    }
    
    private readonly TrieNode _root;
    private readonly ITrieSymbolRetrieveStrategy<TInput, TItem> _trieItemsStrategy;

    public Trie(ITrieSymbolRetrieveStrategy<TInput, TItem>? trieItemsStrategy = null)
    {
        _root = new TrieNode();

        if (trieItemsStrategy is null)
        {
            if (!typeof(IEnumerable<TItem>).IsAssignableFrom(typeof(TInput)))
                throw new InvalidOperationException();

            var strategyType = typeof(DefaultTrieSymbolRetrieveStrategy<,>).MakeGenericType(typeof(TInput), typeof(TItem));
            trieItemsStrategy = Activator.CreateInstance(strategyType) as ITrieSymbolRetrieveStrategy<TInput, TItem>;

            if (trieItemsStrategy is null)
                throw new InvalidOperationException();
        }

        _trieItemsStrategy = trieItemsStrategy;
    }

    public void Insert(TInput input)
        => _root.Insert(_trieItemsStrategy.GetSymbols(input));

    public bool Contains(TInput input)
        => _root.Contains(_trieItemsStrategy.GetSymbols(input));
}