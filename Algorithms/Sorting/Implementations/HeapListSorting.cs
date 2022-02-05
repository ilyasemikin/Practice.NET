using Algorithms;

namespace Sorting.Implementations;

public class HeapListSorting<T> : IListSorting<T>
{
    public void Sort(IList<T> data, IComparer<T> comparer)
    {
        var size = data.Count;

        Heap.BuildMaxHeap(data, size, comparer);
        for (var i = data.Count - 1; i >= 1; i--)
        {
            (data[0], data[i]) = (data[i], data[0]);
            Heap.MaxHeapify(data, --size, 0, comparer);
        }
    }
}