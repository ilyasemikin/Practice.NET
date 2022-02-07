using LinkedLists;
using LinkedLists.Implementations;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedLists;

[TestFixture(typeof(SinglyLinkedList<int>))]
public class DefaultLinkedListTests<TLinkedList> : LinkedListTestsBase<TLinkedList>
    where TLinkedList : ILinkedList<int>, new()
{
    
}