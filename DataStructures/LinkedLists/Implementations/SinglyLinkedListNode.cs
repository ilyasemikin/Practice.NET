namespace LinkedLists.Implementations;

public class SinglyLinkedListNode<T> : ILinkedListNode<T>
{
    public SinglyLinkedListNode<T>? Next { get; internal set; }
    ILinkedListNode<T>? ILinkedListNode<T>.Next => Next;
    
    public T Value { get; set; }

    public ILinkedList<T> List { get; }

    internal SinglyLinkedListNode(T value, ILinkedList<T> list, SinglyLinkedListNode<T>? next = null)
    {
        Value = value;
        List = list;
        Next = next;
    }
}