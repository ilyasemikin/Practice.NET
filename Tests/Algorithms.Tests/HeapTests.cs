using NUnit.Framework;

namespace Algorithms.Tests;

public class HeapTests
{
    [TestCase(new[] { 16, 4, 10, 14, 7, 9, 3, 2, 8, 1 }, 1, new[] { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 })]
    [TestCase(new[] { 27, 17, 3, 16, 13, 10, 1, 5, 7, 12, 4, 8, 9, 0 }, 2, new[] { 27, 17, 10, 16, 13, 9, 1, 5, 7, 12, 4, 8, 3, 0 })]
    public void MaxHeapifyTest(int[] heap, int index, int[] expected)
    {
        Heap.MaxHeapify(heap, heap.Length, index);
        CollectionAssert.AreEqual(expected, heap);
    }
    
    [TestCase(new[] { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 }, new[] { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 })]
    [TestCase(new[] { 5, 3, 17, 10, 84, 19, 6, 22, 9 }, new[] { 84, 22, 19, 10, 3, 17, 6, 5, 9 })]
    public void MaxHeapBuildTest(int[] heap, int[] expected)
    {
        Heap.BuildMaxHeap(heap, heap.Length);
        CollectionAssert.AreEqual(expected, heap);
    }
}