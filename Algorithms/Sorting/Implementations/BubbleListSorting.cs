namespace Sorting.Implementations;

public class BubbleListSorting<T> : IListSorting<T>
{
    public void Sort(IList<T> data, IComparer<T> comparer)
    {
        for (var i = 0; i < data.Count; i++)
        for (var j = 0; j < data.Count - i - 1; j++)
            if (comparer.Compare(data[j], data[j + 1]) > 0)
                (data[j], data[j + 1]) = (data[j + 1], data[j]);
    }
}