namespace SearchTrees;

public interface IBinarySearchTreeNode<TKey, TValue>
{
    TKey Key { get; init; }
    TValue Value { get; set; }
    
    IBinarySearchTreeNode<TKey, TValue>? NullNode { get; }
    
    IBinarySearchTreeNode<TKey, TValue>? Parent { get; }
    IBinarySearchTreeNode<TKey, TValue>? Left { get; }
    IBinarySearchTreeNode<TKey, TValue>? Right { get; }
    
    IBinarySearchTree<TKey, TValue> Tree { get; }
    
    IBinarySearchTreeNode<TKey, TValue>? Find(TKey key);
    IBinarySearchTreeNode<TKey, TValue> Min();
    IBinarySearchTreeNode<TKey, TValue> Max();

    IBinarySearchTreeNode<TKey, TValue>? Successor()
    {
        if (Right != NullNode)
            return Right!.Min();

        var parent = Parent;
        var child = this;

        while (parent != NullNode && parent!.Right == child)
        {
            child = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    IBinarySearchTreeNode<TKey, TValue>? Predecessor()
    {
        if (Left != NullNode)
            return Left!.Max();

        var parent = Parent;
        var child = this;

        while (parent != NullNode && parent!.Left == child)
        {
            child = parent;
            parent = parent.Parent;
        }

        return parent;
    }
}
