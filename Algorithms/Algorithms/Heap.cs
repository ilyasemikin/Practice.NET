namespace Algorithms;

public static class Heap
{
    public static int Parent(int index)
        => (index - 1) / 2;
    
    public static int Left(int index)
        => 2 * index + 1;

    public static int Right(int index)
        => 2 * index + 2;

    public static void MaxHeapify<T>(IList<T> heap, int size, int index, IComparer<T>? comparer = null)
    {
        if (index >= size) 
            return;

        comparer ??= Comparer<T>.Default;
        
        while (true)
        {
            var left = Left(index);
            var right = Right(index);

            var largest = index;
            if (left < size && comparer.Compare(heap[largest], heap[left]) < 0) 
                largest = left;
            if (right < size && comparer.Compare(heap[largest], heap[right]) < 0) 
                largest = right;

            if (largest == index)
                break;

            (heap[index], heap[largest]) = (heap[largest], heap[index]);
            index = largest;
        }
    }

    public static void MinHeapify<T>(IList<T> heap, int size, int index, IComparer<T>? comparer = null)
    {
        if (index >= size)
            return;

        comparer ??= Comparer<T>.Default;

        while (true)
        {
            var left = Left(index);
            var right = Right(index);

            var smallest = index;
            if (left < size && comparer.Compare(heap[smallest], heap[left]) > 0)
                smallest = left;
            if (right < size && comparer.Compare(heap[smallest], heap[right]) > 0)
                smallest = right;

            if (smallest == index)
                break;
            
            (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
            index = smallest;
        }
    }

    public static void BuildMaxHeap<T>(IList<T> heap, int size, IComparer<T>? comparer = null)
    {
        for (var i = size / 2 - 1; i >= 0; i--)
            MaxHeapify(heap, size, i, comparer);
    }

    public static void BuildMinHeap<T>(IList<T> heap, int size, IComparer<T>? comparer = null)
    {
        for (var i = size / 2 - 1; i >= 0; i--)
            MinHeapify(heap, size, i, comparer);
    }
}
