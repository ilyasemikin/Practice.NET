namespace Sorting.Implementations;

public class SelectionListSorting<T> : IListSorting<T>
{
    public void Sort(IList<T> data, IComparer<T> comparer)
    {
        for (var i = 0; i < data.Count; i++)
        {
            var minIndex = i;
            for (var j = i + 1; j < data.Count; j++)
                if (comparer.Compare(data[minIndex], data[j]) > 0)
                    minIndex = j;
            
            (data[minIndex], data[i]) = (data[i], data[minIndex]);
        }
    }
}
