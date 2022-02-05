namespace Sorting.Implementations;

public class MergeListSorting<T> : IListSorting<T>
{
    private static void Sort(IList<T> data, IComparer<T> comparer, int left, int right)
    {
        if (left >= right)
            return;

        var middle = left + (right - left) / 2;
        Sort(data, comparer, left, middle);
        Sort(data, comparer, middle + 1, right);

        var tmp = new T[right - left + 1];
        var l = left;
        var r = middle + 1;
        for (var i = 0; i < tmp.Length; i++)
        {
            if (l == middle + 1)
                tmp[i] = data[r++];
            else if (r == right + 1)
                tmp[i] = data[l++];
            else if (comparer.Compare(data[l], data[r]) < 0)
                tmp[i] = data[l++];
            else
                tmp[i] = data[r++];
        }

        for (var i = 0; i < tmp.Length; i++)
            data[left + i] = tmp[i];
    }

    public void Sort(IList<T> data, IComparer<T> comparer)
        => Sort(data, comparer, 0, data.Count - 1);
}