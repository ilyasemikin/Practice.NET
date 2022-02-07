using System.Collections;

namespace LinkedLists.Implementations;

public class DoublyLinkedList<T> : ILinkedList<T>
{
    public DoublyLinkedListNode<T>? Head { get; private set; }
    public DoublyLinkedListNode<T>? Tail { get; private set; }

    public int Count { get; private set; }

    ILinkedListNode<T>? ILinkedList<T>.Head => Head;
    ILinkedListNode<T>? ILinkedList<T>.Tail => Tail;

    public void AddToTail(T value)
    {
        var tail = new DoublyLinkedListNode<T>(value, this, Tail);
        if (tail.Prev is not null)
            tail.Prev.Next = tail;

        Tail = tail;
        Head ??= tail;

        Count++;
    }

    public void AddToHead(T value)
    {
        var head = new DoublyLinkedListNode<T>(value, this, next: Head);
        if (head.Next is not null)
            head.Next.Prev = head;

        Head = head;
        Tail ??= head;

        Count++;
    }

    public void Remove(ILinkedListNode<T> node)
    {
        if (node.List != this || node is not DoublyLinkedListNode<T> doublyNode)
            throw new InvalidOperationException();
        
        if (Head == node)
        {
            Head = Head.Next;
            Count--;

            if (Head is null)
            {
                Tail = null;
                return;
            }

            if (Head is not null)
                Head.Prev = null;
        }
        else
        {
            Count--;
            doublyNode.Prev!.Next = doublyNode.Next;
            if (doublyNode.Next is not null)
                doublyNode.Next.Prev = null;

            if (Tail == node)
                Tail = doublyNode.Prev;
        }
    }

    public IEnumerable<T> Reversed()
    {
        var cur = Tail;
        while (cur is not null)
        {
            yield return cur.Value;
            cur = cur.Prev;
        }
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        var cur = Head;
        while (cur is not null)
        {
            yield return cur.Value;
            cur = cur.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}