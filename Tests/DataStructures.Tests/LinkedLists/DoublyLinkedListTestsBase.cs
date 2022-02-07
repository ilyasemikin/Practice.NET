using System.Linq;
using LinkedLists.Implementations;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedLists;

public class DoublyLinkedListTestsBase : LinkedListTestsBase<DoublyLinkedList<int>>
{
    [TestCase(new [] { 1, 2, 3, 4, 5 })]
    [TestCase(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    public void TestReversed(int[] values)
    {
        var list = new DoublyLinkedList<int>();
        
        foreach (var value in values)
            list.AddToTail(value);
        
        CollectionAssert.AreEqual(values, list);
        CollectionAssert.AreEqual(values.Reverse(), list.Reversed());
    }
}