namespace Trees.Algorithms;

public static class BinaryTree
{
    public static bool IsSame<TValue>(IBinaryTreeNode<TValue>? t1, IBinaryTreeNode<TValue>? t2)
        where TValue : IEquatable<TValue>
    {
        if (t1 == null && t2 == null)
            return true;
        if (t1 == null || t2 == null || !t1.Value.Equals(t2.Value))
            return false;
        return IsSame(t1.Left, t2.Left) && IsSame(t1.Right, t2.Right);
    }

    public static int MaxDepth<TValue>(this IBinaryTreeNode<TValue>? root)
    {
        var queue = new Queue<IBinaryTreeNode<TValue>>();
        
        if (root is not null)
            queue.Enqueue(root);

        var depth = 0;
        while (queue.Count > 0)
        {
            var count = queue.Count;
            for (var i = 0; i < count; i++)
            {
                var node = queue.Dequeue();
                
                if (node.Left is not null)
                    queue.Enqueue(node.Left);
                if (node.Right is not null)
                    queue.Enqueue(node.Right);
            }

            depth++;
        }

        return depth;
    }
}
