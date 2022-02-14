using System.Linq;
using NUnit.Framework;
using Trees;
using Trees.Algorithms;
using Trees.Implementations;

namespace DataStructures.Tests.Trees;

public class BinaryTreeMaxDepthTests
{
    private static object[][] ProvideTestCases => BinaryTreeMaxDepthTestCase.Cases
        .Select(c => new object[] {c.Root!, c.ExpectedDepth})
        .ToArray();
    
    [TestCaseSource(nameof(ProvideTestCases))]
    public void MaxDepthTest(IBinaryTreeNode<int>? root, int expectedDepth)
    {
        var depth = root.MaxDepth();
        Assert.AreEqual(expectedDepth, depth);
    }
}