namespace LinkedLists.Implementations;

public class DoublyLinkedListNode<T> : ILinkedListNode<T>
{
    public DoublyLinkedListNode<T>? Prev { get; internal set; }
    public DoublyLinkedListNode<T>? Next { get; internal set; }
    ILinkedListNode<T>? ILinkedListNode<T>.Next => Next;

    public T Value { get; set; }

    public DoublyLinkedList<T> List { get; }
    ILinkedList<T> ILinkedListNode<T>.List => List;

    public DoublyLinkedListNode(T value, DoublyLinkedList<T> list, DoublyLinkedListNode<T>? prev = null, DoublyLinkedListNode<T>? next = null)
    {
        Value = value;
        List = list;
        Prev = prev;
        Next = next;
    }
}