using Trees.Implementations;

namespace Trees.Algorithms;

public class BinaryTreeSerializer<TValue>
{
    private readonly Func<string, TValue> _valueSerializer;

    public BinaryTreeSerializer(Func<string, TValue> valueSerializer)
    {
        _valueSerializer = valueSerializer;
    }

    public string Serialize(IBinaryTreeNode<TValue>? tree)
    {
        if (tree is null)
            return "[]";

        var order = new List<IBinaryTreeNode<TValue>?> { tree };

        var queue = new Queue<IBinaryTreeNode<TValue>>();
        var nextQueue = new Queue<IBinaryTreeNode<TValue>>();

        queue.Enqueue(tree);

        while (queue.Count != 0)
        {
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                order.Add(node.Left);
                order.Add(node.Right);

                if (node.Left is not null)
                    nextQueue.Enqueue(node.Left);
                if (node.Right is not null)
                    nextQueue.Enqueue(node.Right);
            }

            (queue, nextQueue) = (nextQueue, queue);
        }

        var lastNotNull = order.FindLastIndex(n => n is not null);
        var orderStrings = order.Take(lastNotNull + 1).Select(n => n?.Value!.ToString() ?? "null");
        return $"[{string.Join(',', orderStrings)}]";
    }

    public IBinaryTreeNode<TValue>? Deserialize(string input)
    {
        input = input.Trim();
        if (input.Length < 2 || input[0] != '[' || input[^1] != ']')
            throw new FormatException();

        var values = input.Substring(1, input.Length - 2);
        var nodes = values.Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(v => v == "null" ? null : new BinaryTreeNode<TValue>(_valueSerializer(v)))
            .ToArray();

        if (nodes.Length == 0)
            return null;

        var root = nodes[0];
        if (root is null)
            return root;
        
        var queue = new Queue<BinaryTreeNode<TValue>>();
        queue.Enqueue(root);

        int i = 1;
        while (queue.Count > 0 && i < nodes.Length)
        {
            var count = queue.Count;
            while (count > 0)
            {
                var node = queue.Dequeue();

                if (i < nodes.Length)
                    node.Left = nodes[i++];
                if (i < nodes.Length)
                    node.Right = nodes[i++];
                
                if (node.Left is not null)
                    queue.Enqueue(node.Left!);
                if (node.Right is not null)
                    queue.Enqueue(node.Right!);

                count--;
            }
        }

        return root;
    }
}
