namespace DataStructures;

public class DefaultTrieSymbolRetrieveStrategy<TInput, TSymbol> : ITrieSymbolRetrieveStrategy<TInput, TSymbol>
    where TInput : notnull, IEnumerable<TSymbol>
    where TSymbol : notnull
{
    public IEnumerable<TSymbol> GetSymbols(TInput input) => input;
}