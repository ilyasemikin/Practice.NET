namespace LinkedLists;

public interface ILinkedListNode<T>
{
    ILinkedListNode<T>? Next { get; }
    T Value { get; set; }
    
    ILinkedList<T> List { get; }
}