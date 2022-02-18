namespace Queues;

public interface IQueue<T>
{
    void EnQueue(T value);
    T DeQueue();

    bool TryEnQueue(T value);
    bool TryDeQueue(out T value);

    void Clear();
    
    int Count();
    T Front();

    bool TryFront(out T value);
}