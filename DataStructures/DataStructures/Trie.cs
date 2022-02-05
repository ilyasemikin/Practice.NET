namespace DataStructures;

public class Trie<TInput, TSymbol>
    where TSymbol : notnull
{
    private readonly ITrieSymbolRetrieveStrategy<TInput, TSymbol> _trieItemsStrategy;
    public TrieNode<TSymbol> Root { get; }

    public Trie(ITrieSymbolRetrieveStrategy<TInput, TSymbol>? trieItemsStrategy = null)
    {
        Root = new TrieNode<TSymbol>();

        if (trieItemsStrategy is null)
        {
            if (!typeof(IEnumerable<TSymbol>).IsAssignableFrom(typeof(TInput)))
                throw new InvalidOperationException();

            var strategyType = typeof(DefaultTrieSymbolRetrieveStrategy<,>).MakeGenericType(typeof(TInput), typeof(TSymbol));
            trieItemsStrategy = Activator.CreateInstance(strategyType) as ITrieSymbolRetrieveStrategy<TInput, TSymbol>;

            if (trieItemsStrategy is null)
                throw new InvalidOperationException();
        }

        _trieItemsStrategy = trieItemsStrategy;
    }

    public void Insert(TInput input)
        => Root.Insert(_trieItemsStrategy.GetSymbols(input));

    public bool Contains(TInput input)
        => Root.Contains(_trieItemsStrategy.GetSymbols(input));
}