using NUnit.Framework;
using SearchTrees;
using SearchTrees.Implementations;

namespace DataStructures.Tests;

[TestFixture(typeof(BinarySearchTree<int, int>))]
[TestFixture(typeof(RedBlackBinarySearchTree<int, int>))]
public class BinarySearchTreeTests<TBinarySearchTree>
    where TBinarySearchTree : IBinarySearchTree<int, int>, new()
{
    [TestCase(new[] { 1, 2, 3, 4, 5 })]
    [TestCase(new[] { 5, 2, 1, 3, 4 })]
    [TestCase(new[] { 3, 1, 5, 4, 2 })]
    [TestCase(new[] { 2, 3, 4, 5, 1 })]
    public void TreeInsertTest(int[] keys)
    {
        var tree = new TBinarySearchTree();

        foreach (var value in keys)
            Assert.IsTrue(tree.TryInsert(value, value));

        System.Array.Sort(keys);
        CollectionAssert.AreEqual(keys, tree.Keys);
    }

    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 20)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 4)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 30)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 25)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 12)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 11)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 40)]
    [TestCase(new[] { 20, 4, 30, 2, 10, 24, 40, 6, 12, 25, 80, 5, 8, 11 }, 24)]
    public void TreeDeleteTest(int[] values, int deleteValue)
    {
        var expected = new int[values.Length - 1];
        var i = 0;

        foreach (var v in values)
            if (v.CompareTo(deleteValue) != 0)
                expected[i++] = v;

        System.Array.Sort(expected);

        var tree = new TBinarySearchTree();
        foreach (var value in values)
            Assert.IsTrue(tree.TryInsert(value, value));

        Assert.IsTrue(tree.TryRemove(deleteValue));
        CollectionAssert.AreEqual(expected, tree.Values);
    }
}