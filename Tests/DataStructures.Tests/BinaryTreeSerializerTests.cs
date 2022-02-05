using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trees;
using Trees.Algorithms;
using Trees.Implementations;

namespace DataStructures.Tests;

public class BinaryTreeSerializerTests
{
    private class BinaryTreeNodeTestCase
    {
        public readonly IBinaryTreeNode<int>? Root;
        public readonly string SerializedString;

        public BinaryTreeNodeTestCase(IBinaryTreeNode<int>? root, string serializedString)
        {
            Root = root;
            SerializedString = serializedString;
        }
    }

    private static readonly IReadOnlyList<BinaryTreeNodeTestCase> TestCases = new BinaryTreeNodeTestCase[]
    {
        new (null, "[]"),
        new (new BinaryTreeNode<int>(0), "[0]"),
        new (new BinaryTreeNode<int>(0, new BinaryTreeNode<int>(1), new BinaryTreeNode<int>(2)), "[0,1,2]"),
        new (new BinaryTreeNode<int>(0, null, new BinaryTreeNode<int>(1)), "[0,null,1]"),
        new (new BinaryTreeNode<int>(0,
            new BinaryTreeNode<int>(1, null,
                new BinaryTreeNode<int>(2,
                    new BinaryTreeNode<int>(3),
                    new BinaryTreeNode<int>(4)))), "[0,1,null,null,2,3,4]")
    };

    private static object[][] ProvideTestCases()
        => TestCases.Select(t => new object[] { t.Root!, t.SerializedString })
        .ToArray();

    private BinaryTreeSerializer<int>? _serializer;

    [SetUp]
    public void SetUp()
    {
        _serializer = new BinaryTreeSerializer<int>(int.Parse);
    }

    [TestCaseSource(nameof(ProvideTestCases))]
    public void SerializeTreeTest(IBinaryTreeNode<int> root, string expected)
    {
        var serializedString = _serializer!.Serialize(root);
        Assert.AreEqual(expected, serializedString);
    }

    [TestCaseSource(nameof(ProvideTestCases))]
    public void DeserializeTreeTest(IBinaryTreeNode<int> expected, string input)
    {
        var tree = _serializer!.Deserialize(input);

        Assert.IsTrue(BinaryTree.IsSame(tree, expected));
    }

    [TestCase("")]
    [TestCase("1,2,3")]
    [TestCase("null")]
    [TestCase("[hello]")]
    public void DeserializeTreeExceptionTest(string input)
        => Assert.Throws<FormatException>(() => { _serializer!.Deserialize(input); });
}