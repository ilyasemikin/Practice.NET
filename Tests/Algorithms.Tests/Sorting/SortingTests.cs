using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sorting;
using Sorting.Implementations;

namespace Algorithms.Tests.Sorting;

[TestFixture(typeof(BubbleListSorting<int>))]
[TestFixture(typeof(HeapListSorting<int>))]
[TestFixture(typeof(InsertionListSorting<int>))]
[TestFixture(typeof(MergeListSorting<int>))]
[TestFixture(typeof(SelectionListSorting<int>))]
public class SortingTests<TSortMethod>
    where TSortMethod : IListSorting<int>, new()
{
    private IListSorting<int>? _sortMethod;

    [SetUp]
    public void SetUp()
    {
        _sortMethod = new TSortMethod();
    }

    [TestCase(new[] { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 }, new[] { 1, 2, 3, 4, 7, 8, 9, 10, 14, 16 })]
    [TestCase(new[] { 5, 13, 2, 25, 7, 17, 20, 8, 4 }, new[] { 2, 4, 5, 7, 8, 13, 17, 20, 25 })]
    public void TestPreparedData(int[] data, int[] expected)
    {
        _sortMethod!.Sort(data);
        CollectionAssert.AreEqual(expected, data);
    }

    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000)]
    public void TestRandomData(int size)
    {
        var data = Enumerable.Range(0, size)
            .Select(_ => Random.Shared.Next(int.MinValue, int.MaxValue))
            .ToArray();

        _sortMethod!.Sort(data);
        Assert.IsTrue(List.IsNonDesc(data, int.MinValue));
    }

    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000)]
    public void TestCustomComparerRandomData(int size)
    {
        var data = Enumerable.Range(0, size)
            .Select(_ => Random.Shared.Next(int.MinValue, int.MaxValue))
            .ToArray();

        var comparer = Comparer<int>.Create((x, y) =>
        {
            if (x == y)
                return 0;
            if (x > y)
                return -1;
            return 1;
        });
        _sortMethod!.Sort(data, comparer);
        Assert.IsTrue(List.IsNonAsc(data, int.MaxValue));
    }
}