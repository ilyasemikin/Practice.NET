using Queues.Implementations;

namespace DataStructures.Tests.Queues;

public class CircularQueueTests : QueueTests<CircularQueue<int>>
{
    protected override CircularQueue<int> CreateQueue() => new CircularQueue<int>(100);
}