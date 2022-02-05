using Algorithms;

namespace DataStructures;

public class PriorityQueue<TItem>
{
    private struct Item : IComparable<Item>
    {
        public int Priority;
        public TItem Value;

        public int CompareTo(Item other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }

    private int _heapSize;
    private IList<Item> _heap;

    public int Count => _heapSize;
    
    public PriorityQueue()
    {
        _heapSize = 0;
        _heap = new List<Item>();
    }

    private bool IncreasePriority(int index, int priority)
    {
        if (priority < _heap[index].Priority)
            return false;

        var item = _heap[index];
        item.Priority = priority;
        _heap[index] = item;

        while (index > 0 && _heap[Heap.Parent(index)].Priority < _heap[index].Priority)
        {
            (_heap[index], _heap[Heap.Parent(index)]) = (_heap[Heap.Parent(index)], _heap[index]);
            index = Heap.Parent(index);
        }
        
        return true;
    }

    public void Insert(TItem item, int priority)
    {
        if (_heapSize >= _heap.Count)
            _heap.Add(new Item
            {
                Priority = int.MinValue,
                Value = item
            });
        else
            _heap[_heapSize] = new Item
            {
                Priority = int.MinValue,
                Value = item
            };

        _heapSize++;

        IncreasePriority(_heapSize - 1, priority);
    }

    public TItem GetMaximum()
    {
        if (_heapSize == 0)
            throw new InvalidOperationException("Priority queue is empty");

        return _heap[0].Value;
    }

    public bool TryGetMaximum(out TItem? item)
    {
        item = default;
        
        if (_heapSize == 0)
            return false;

        item = _heap[0].Value;
        return true;
    }

    public TItem ExtractMaximum()
    {
        if (_heapSize == 0)
            throw new InvalidOperationException("Priority queue is empty");

        var item = _heap[0].Value;
        (_heap[0], _heap[_heapSize - 1]) = (_heap[_heapSize - 1], _heap[0]);
        Heap.MaxHeapify(_heap, --_heapSize, 0);
        return item;
    }

    public bool TryExtractMaximum(out TItem? item)
    {
        item = default;
        if (_heapSize == 0)
            return false;

        item = _heap[0].Value;
        (_heap[0], _heap[_heapSize - 1]) = (_heap[_heapSize - 1], _heap[0]);
        Heap.MaxHeapify(_heap, --_heapSize, 0);
        return true;
    }
}