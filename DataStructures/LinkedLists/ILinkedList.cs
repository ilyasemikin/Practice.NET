namespace LinkedLists;

public interface ILinkedList<T> : IEnumerable<T>
{
    int Count { get; }
    
    ILinkedListNode<T>? Head { get; }
    ILinkedListNode<T>? Tail { get; }

    void AddToTail(T value);
    void AddToHead(T value);

    void Remove(ILinkedListNode<T> node);
}