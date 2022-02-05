using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DataStructures.Tests;

public class TrieTests
{
    [Test]
    public void DefaultConstructorTest()
    {
        Assert.DoesNotThrow(() => { new Trie<string, char>(); });
    }
    
    [Test]
    public void DefaultConstructorIncorrectTypesTest()
    {
        Assert.Throws<InvalidOperationException>(() => { new Trie<int, int>(); });
    }

    private static readonly string[][] ConstructTreeFromUniqueItemsTestCases =
    {
        new [] { "Hello", "World" },
        new [] { "Hello", "Help", "Hell" }
    };
    
    [TestCaseSource(nameof(ConstructTreeFromUniqueItemsTestCases))]
    public void ConstructTreeFromUniqueItemsTest(IReadOnlyList<string> data)
    {
        var trie = new Trie<string, char>();
        var uniqueData = data.ToHashSet()
            .ToArray();

        for (var i = 0; i < uniqueData.Length; i++)
        {
            if (i < uniqueData.Length - 1)
                Assert.IsFalse(trie.Contains(data[i + 1]));

            trie.Insert(data[i]);
            Assert.IsTrue(trie.Contains(data[i]));
        }
    }
}