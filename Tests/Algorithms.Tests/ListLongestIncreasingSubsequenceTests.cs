using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.Tests;

public class ListLongestIncreasingSubsequenceTests
{
    [TestCase(new int[] { }, 0)]
    [TestCase(new [] { 0 }, 1)]
    [TestCase(new [] { 10, 9, 2, 5, 3, 7, 101, 18 }, 4)]
    [TestCase(new [] { 7, 7, 7, 7, 7, 7, 7 }, 1)]
    [TestCase(new [] { 1, 5, 3, 8, 15, 1 }, 4)]
    public void LisTest(IReadOnlyList<int> sequence, int expectedCount)
    {
        var longestSequence = List.LongestIncreasingSubsequence(sequence, int.MinValue, int.MaxValue);
        Assert.AreEqual(expectedCount, longestSequence.Count);
        Assert.IsTrue(List.IsSubsequence(sequence, longestSequence));
    }
}