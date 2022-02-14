using Trees;
using Trees.Implementations;

namespace DataStructures.Tests.Trees;

public class BinaryTreeMaxDepthTestCase
{
    public IBinaryTreeNode<int>? Root { get; }
    public int ExpectedDepth { get; }

    public BinaryTreeMaxDepthTestCase(IBinaryTreeNode<int>? root, int expectedDepth)
    {
        Root = root;
        ExpectedDepth = expectedDepth;
    }

    public static BinaryTreeMaxDepthTestCase[] Cases { get; } = new[]
    {
        new BinaryTreeMaxDepthTestCase(
            new BinaryTreeNode<int>(3, new BinaryTreeNode<int>(9), new BinaryTreeNode<int>(20,
                new BinaryTreeNode<int>(15), new BinaryTreeNode<int>(7))), 3),
        new BinaryTreeMaxDepthTestCase(
            new BinaryTreeNode<int>(1, null, new BinaryTreeNode<int>(2)), 2),
        new BinaryTreeMaxDepthTestCase(null, 0)
    };
}