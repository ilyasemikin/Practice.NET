using System.Collections;

namespace LinkedLists.Implementations;

public class SinglyLinkedList<T> : ILinkedList<T>
{
    public SinglyLinkedListNode<T>? Head { get; private set; }
    public SinglyLinkedListNode<T>? Tail { get; private set; }

    public int Count { get; private set; }

    ILinkedListNode<T>? ILinkedList<T>.Head => Head;
    ILinkedListNode<T>? ILinkedList<T>.Tail => Tail;

    public void AddToTail(T value)
    {
        if (Tail is not null)
        {
            Tail.Next = new SinglyLinkedListNode<T>(value, this);
            Tail = Tail.Next;
        }
        else
        {
            Head = Tail = new SinglyLinkedListNode<T>(value, this);
        }

        Count++;
    }

    public void AddToHead(T value)
    {
        var head = new SinglyLinkedListNode<T>(value, this, Head);
        
        if (Head is not null)
        {
            Head = head;
        }
        else
        {
            Head = Tail = head;
        }

        Count++;
    }

    public void Remove(ILinkedListNode<T> node)
    {
        if (node.List != this || node is not SinglyLinkedListNode<T> singlyNode)
            throw new InvalidOperationException();

        if (Head == node)
        {
            Head = Head.Next;
            Count--;

            if (Count == 0)
                Tail = Head = null;
                
            return;
        }

        var prev = Head;
        while (prev is not null && prev.Next != node)
            prev = prev.Next;

        if (prev is null)
            throw new InvalidOperationException();

        prev.Next = singlyNode.Next;
        Count--;

        if (Tail == node)
            Tail = prev;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var node = Head;
        while (node is not null)
        {
            yield return node.Value;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}