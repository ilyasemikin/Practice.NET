namespace LinkedLists.Implementations;

public class SinglyLinkedListNode<T> : ILinkedListNode<T>
{
    public SinglyLinkedListNode<T>? Next { get; internal set; }
    ILinkedListNode<T>? ILinkedListNode<T>.Next => Next;
    
    public T Value { get; set; }

    public SinglyLinkedList<T> List { get; }
    ILinkedList<T> ILinkedListNode<T>.List => List;

    internal SinglyLinkedListNode(T value, SinglyLinkedList<T> list, SinglyLinkedListNode<T>? next = null)
    {
        Value = value;
        List = list;
        Next = next;
    }
}