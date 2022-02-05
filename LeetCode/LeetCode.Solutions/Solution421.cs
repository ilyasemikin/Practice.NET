using DataStructures;

namespace LeetCode.Solutions;

public class Solution421
{
    public static IEnumerable<bool> GetBits(int x)
    {
        for (int i = 31; i >= 0; i--)
            yield return (x & 1 << i) > 0;
    }

    public class Strategy : ITrieSymbolRetrieveStrategy<int, bool>
    {
        public IEnumerable<bool> GetSymbols(int input)
            => GetBits(input);
    }

    public int Find(TrieNode<bool> root, int value)
    {
        var bits = GetBits(value)
            .Reverse()
            .ToArray();
        var cur = root;
        int answer = 0;
        for (int i = 31; i >= 0; i--)
        {
            if (!cur.Childs.ContainsKey(!bits[i]))
                bits[i] = !bits[i];

            answer |= (!bits[i] == true ? 1 : 0) << i;
            cur = cur.Childs[!bits[i]];
        }

        return answer;
    }

    // ReSharper disable once InconsistentNaming
    public int FindMaximumXOR(int[] nums)
    {
        var trie = new Trie<int, bool>(new Strategy());
        foreach (var num in nums)
            trie.Insert(num);

        return nums.Select(num => num ^ Find(trie.Root, num))
            .Prepend(0)
            .Max();
    }
}