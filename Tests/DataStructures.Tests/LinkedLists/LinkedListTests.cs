using System.Linq;
using LinkedLists;
using LinkedLists.Implementations;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedLists;

[TestFixture(typeof(SinglyLinkedList<int>))]
public class LinkedListTests<TLinkedList>
    where TLinkedList : ILinkedList<int>, new()
{
    [TestCase(new [] { 1 })]
    [TestCase(new [] { 1, 2, 3, 4, 5 })]
    [TestCase(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    public void AddToHeadTest(int[] values)
    {
        var list = new TLinkedList();
        
        foreach (var value in values)
            list.AddToHead(value);
        
        Assert.AreEqual(values.Length, list.Count);
        Assert.IsNotNull(list.Head);
        Assert.IsNotNull(list.Tail);
        Assert.AreEqual(values[^1], list.Head!.Value);
        Assert.AreEqual(values[0], list.Tail!.Value);
        CollectionAssert.AreEqual(values.Reverse(), list);
    }

    [TestCase(new [] { 1 })]
    [TestCase(new [] { 1, 2, 3, 4, 5 })]
    [TestCase(new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10})]
    public void AddToTail(int[] values)
    {
        var list = new TLinkedList();
        
        foreach (var value in values)
            list.AddToTail(value);
        
        Assert.AreEqual(values.Length, list.Count);
        Assert.IsNotNull(list.Head);
        Assert.IsNotNull(list.Tail);
        Assert.AreEqual(values[0], list.Head!.Value);
        Assert.AreEqual(values[^1], list.Tail!.Value);
        CollectionAssert.AreEqual(values, list);
    }

    [TestCase(new [] { 1, 2, 3, 4, 5 }, 0)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, 1)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, 2)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, 4)]
    public void RemoveNode(int[] values, int index)
    {
        var list = new TLinkedList();
        
        foreach (var value in values)
            list.AddToTail(value);

        var node = list.Head;
        for (var i = 0; i < index; i++)
            node = node!.Next;

        list.Remove(node!);
        
        var expected = Enumerable.Range(0, values.Length)
            .Where(i => i != index)
            .Select(i => values[i])
            .ToArray();
        
        Assert.AreEqual(expected.Length, list.Count);
        Assert.AreEqual(expected[0], list.Head!.Value);
        Assert.AreEqual(expected[^1], list.Tail!.Value);
        CollectionAssert.AreEqual(expected, list);
    }

    [Test]
    public void RemoveLastNode()
    {
        var list = new TLinkedList();
        list.AddToHead(1);
        list.Remove(list.Head!);
        
        Assert.Zero(list.Count);
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
    }
}