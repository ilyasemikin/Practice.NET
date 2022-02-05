using LeetCode.Solutions;
using NUnit.Framework;

namespace LeetCode.Tests;

public class Solution739_Tests
{
    private Solution739? _solution;

    [SetUp]
    public void SetUp()
    {
        _solution = new Solution739();
    }

    [TestCase(new int[] { 73, 74, 75, 71, 69, 72, 76, 73 }, new int[] { 1, 1, 4, 2, 1, 1, 0, 0 })]
    [TestCase(new int[] { 30, 40, 50, 60 }, new int[] { 1, 1, 1, 0 })]
    [TestCase(new int[] { 30, 60, 90 }, new int[] { 1, 1, 0 })]
    public void Test(int[] input, int[] expected)
    {
        var temperatures = _solution!.DailyTemperatures(input);
        CollectionAssert.AreEqual(expected, temperatures);
    }
}