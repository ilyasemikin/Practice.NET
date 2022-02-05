namespace Sorting.Implementations;

public class InsertionListSorting<T> : IListSorting<T>
{
    public void Sort(IList<T> data, IComparer<T> comparer)
    {
        for (var i = 1; i < data.Count; i++)
        {
            var value = data[i];

            var j = i - 1;
            while (j >= 0 && comparer.Compare(value, data[j]) < 0)
            {
                data[j + 1] = data[j];
                j--;
            }

            data[j + 1] = value;
        }
    }
}