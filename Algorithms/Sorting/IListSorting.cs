namespace Sorting;

/// <summary>
/// Interface-method for sort elements in ascending order
/// </summary>
public interface IListSorting<T>
{
    void Sort(IList<T> data, IComparer<T> comparer);

    void Sort(IList<T> data)
        => Sort(data, Comparer<T>.Default);
}