namespace DataStructures;

public interface ITrieSymbolRetrieveStrategy<in TInput, out TSymbol>
{
    IEnumerable<TSymbol> GetSymbols(TInput input);
}