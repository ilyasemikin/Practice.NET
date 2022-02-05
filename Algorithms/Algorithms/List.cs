namespace Algorithms;

public class List
{
    public static int LowerBound<T>(IReadOnlyList<T> array, T value, int left = -1, int right = -1, IComparer<T>? comparer = null)
    {
        if (array.Count == 0)
            return -1;

        comparer ??= Comparer<T>.Default;

        if (array.Count == 1)
            return comparer.Compare(array[0], value) <= 0 ? 0 : -1;

        if (left == -1)
            left = 0;
        if (right == -1)
            right = array.Count - 1;

        while (left < right)
        {
            var middle = left + (right - left) / 2;

            if (comparer.Compare(array[middle], value) >= 0)
                right = middle;
            else
                left = middle + 1;
        }

        return right;
    }

    public static int UpperBound<T>(IReadOnlyList<T> array, T value, int left = -1, int right = -1, IComparer<T>? comparer = null)
    {
        if (array.Count == 0)
            return -1;

        comparer ??= Comparer<T>.Default;

        if (array.Count == 1)
            return comparer.Compare(array[0], value) > 0 ? 0 : -1;

        if (left == -1)
            left = 0;
        if (right == -1)
            right = array.Count - 1;

        while (left < right)
        {
            var middle = right - (right - left) / 2;

            if (comparer.Compare(array[middle], value) <= 0)
                left = middle;
            else
                right = middle - 1;
        }

        return left + 1;
    }

    public static IReadOnlyList<T> LongestIncreasingSubsequence<T>(IReadOnlyList<T> array, T minValue, T maxValue, IComparer<T>? comparer = null)
    {
        if (array.Count == 0)
            return Array.Empty<T>();
        if (array.Count == 1)
            return new T[] { array[0] };

        comparer ??= Comparer<T>.Default;

        var dp = new T[array.Count + 1];
        dp[0] = minValue;
        for (int i = 1; i < dp.Length; i++)
            dp[i] = maxValue;

        foreach (var item in array)
        {
            var index = UpperBound(dp, item);
            if (index == -1)
                continue;

            if (comparer.Compare(dp[index - 1], item) < 0 && comparer.Compare(item, dp[index]) < 0)
                dp[index] = item;
        }

        return dp.Skip(1)
            .TakeWhile(v => comparer.Compare(v, maxValue) != 0)
            .ToArray();
    }
    
    public static bool IsSubsequence<T>(IEnumerable<T> sequence, IEnumerable<T> subsequence, IComparer<T>? comparer = null)
    {
        using var it = subsequence.GetEnumerator();
        if (!it.MoveNext())
            return true;

        comparer ??= Comparer<T>.Default;

        return sequence.Any(item => comparer.Compare(item, it.Current) == 0 && !it.MoveNext());
    }
}
