using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Queues;

namespace DataStructures.Tests.Queues;

public abstract class QueueTests<TQueue>
    where TQueue : IQueue<int>
{
    private IQueue<int> _queue = null!;

    [SetUp]
    public void SetUp()
    {
        _queue = CreateQueue();
    }

    protected abstract TQueue CreateQueue();

    [TestCase(new [] { 1 })]
    [TestCase(new [] { 1, 2, 3, 4 })]
    public void TestSizeAfterEnqueue(IReadOnlyList<int> values)
    {
        foreach (var value in values)
            _queue.EnQueue(value);

        _queue.Count().Should().Be(values.Count);
    }

    [Test]
    public void TestSizeAfterEnqueueAndDequeue()
    {
        _queue.EnQueue(1);
        _queue.EnQueue(2);
        _queue.DeQueue();
        _queue.EnQueue(3);
        _queue.DeQueue();
        _queue.EnQueue(5);

        _queue.Count().Should().Be(2);
    }

    [Test]
    public void FrontAtEmptyQueue()
    {
        var frontCall = () => _queue.Front();
        frontCall.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void DequeueAtEmptyQueue()
    {
        var dequeueCall = () => _queue.DeQueue();
        dequeueCall.Should().Throw<InvalidOperationException>();
    }
}