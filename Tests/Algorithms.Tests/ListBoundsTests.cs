using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Tests;

public class ListBoundsTests
{
    private class BoundTestData
    {
        public readonly IReadOnlyList<int> Data;
        public readonly int Target;
        public readonly int UpperBoundIndex;
        public readonly int LowerBoundIndex;

        public BoundTestData(IReadOnlyList<int> data, int target, int upperBoundIndex, int lowerBoundIndex)
        {
            Data = data;
            Target = target;
            UpperBoundIndex = upperBoundIndex;
            LowerBoundIndex = lowerBoundIndex;
        }
    }

    private static readonly BoundTestData[] Data = 
    {
        new (new int[] { }, 0, -1, -1),
        new (new [] { 1 }, 1, -1, 0 ),
        new (new [] { 1 }, 0, 0, -1),
        new (new [] { 1, 2, 3, 4, 5 }, 3, 3, 2),
        new (new [] { 1, 2, 2, 2, 3, 3 }, 2, 4, 1),
        new (new [] { 0, 2, 5, 10, 15 }, 10, 4, 3),
        new (new [] { 1, 2, 3, 4, 4, 4, 5, 5, 5, 5, 5, 6, 7 }, 5, 11, 6)
    };

    private static object[][] ProvideLowerBoundTestData() =>
        Data.Select(d => new object[] { d.Data, d.Target, d.LowerBoundIndex })
            .ToArray();

    private static object[][] ProvideUpperBoundTestData() =>
        Data.Select(d => new object[] { d.Data, d.Target, d.UpperBoundIndex })
            .ToArray();

    [TestCaseSource(nameof(ProvideLowerBoundTestData))]
    public void LowerBoundTest(IReadOnlyList<int> values, int target, int expected)
    {
        var index = List.LowerBound(values, target);
        Assert.AreEqual(expected, index);

        if (index >= 0)
            Assert.AreEqual(target, values[index]);
    }

    [TestCaseSource(nameof(ProvideUpperBoundTestData))]
    public void UpperBoundTest(IReadOnlyList<int> values, int target, int expected)
    {
        var index = List.UpperBound(values, target);
        Assert.AreEqual(expected, index);
    }
}