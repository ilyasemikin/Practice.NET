namespace Queues.Implementations;

public class CircularQueue<T> : IQueue<T>
{
    private readonly T[] _values;
    private int _size;
    private int _start;
    private int _end;

    public CircularQueue(int maxSize)
    {
        _values = new T[maxSize];
        Clear();
    }

    public void EnQueue(T value)
    {
        if (!TryEnQueue(value))
            throw new InvalidOperationException("Max size reached");
    }

    public T DeQueue()
    {
        if (!TryDeQueue(out var value))
            throw new InvalidOperationException("Queue empty");
        return value;
    }

    public bool TryEnQueue(T value)
    {
        if (_size == _values.Length)
            return false;

        _size++;

        _values[_end] = value;
        _end = (_end + 1) % _values.Length;
        return true;
    }

    public bool TryDeQueue(out T value)
    {
        value = default!;
        if (_size == 0)
            return false;

        _size--;

        value = _values[_start];
        _start = (_start + 1) % _values.Length;
        return true;
    }

    public void Clear()
    {
        _size = _start = _end = 0;
    }

    public int Count()
    {
        return _size;
    }

    public T Front()
    {
        if (!TryFront(out var value))
            throw new InvalidOperationException("Queue empty");
        return value;
    }

    public bool TryFront(out T value)
    {
        value = default!;
        if (_size == 0)
            return false;

        value = _values[_start];
        return true;
    }
}