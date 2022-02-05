using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Tests;

public class ListCommonTests
{
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new int[] { }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 2 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 2, 3, 4, 5 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 2, 3, 4 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 2, 3, 4, 5 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 3, 5 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 2, 5 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 4, 5 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 5 }, true)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 3, 2, 1 }, false)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 1, 2, 3, 6 }, false)]
    [TestCase(new [] { 1, 2, 3, 4, 5 }, new [] { 6, 1, 2, 3 }, false)]
    public void SequenceTest(IReadOnlyList<int> seq, IReadOnlyList<int> subSeq, bool expected)
    {
        var isSubSeq = List.IsSubsequence(seq, subSeq);
        Assert.AreEqual(expected, isSubSeq);
    }
}