namespace SearchTrees.Implementations;

public enum RedBlackNodeColor
{
    Black, Red
}

public class RedBlackBinarySearchTreeNode<TKey, TValue> : IBinarySearchTreeNode<TKey, TValue>
{
    public TKey Key { get; init; }
    public TValue Value { get; set; }
    public IComparer<TKey> Comparer { get; init;}
    
    public RedBlackNodeColor Color { get; internal set; }

    public RedBlackBinarySearchTreeNode<TKey, TValue> Parent { get; set; }
    public RedBlackBinarySearchTreeNode<TKey, TValue> Left { get; set; }
    public RedBlackBinarySearchTreeNode<TKey, TValue> Right { get; set; }
    
    public RedBlackBinarySearchTree<TKey, TValue> SearchTree { get; }
    public IBinarySearchTreeNode<TKey, TValue> NullNode => SearchTree.Null;

    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Parent => Parent;
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Left => Left;
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Right => Right;

    internal RedBlackBinarySearchTreeNode(TKey key, TValue value, RedBlackBinarySearchTree<TKey, TValue> searchTree, IComparer<TKey>? comparer)
    {
        comparer ??= Comparer<TKey>.Default;

        Key = key;
        Value = value;
        SearchTree = searchTree;
        Comparer = comparer;

        Parent = Left = Right = SearchTree.Null;
    }

    public RedBlackBinarySearchTreeNode<TKey, TValue> Find(TKey key)
    {
        var cur = this;
        while (cur != SearchTree.Null && Comparer.Compare(cur.Key, key) != 0)
        {
            if (Comparer.Compare(key, cur.Key) < 0)
                cur = cur.Left;
            else
                cur = cur.Right;
        }
        return cur;
    }

    public RedBlackBinarySearchTreeNode<TKey, TValue> Min()
    {
        var cur = this;
        while (cur != SearchTree.Null && cur.Left != SearchTree.Null)
            cur = cur.Left;
        return cur;
    }

    public RedBlackBinarySearchTreeNode<TKey, TValue> Max()
    {
        var cur = this;
        while (cur != SearchTree.Null && cur.Right != SearchTree.Null)
            cur = cur.Right;
        return cur;
    }

    public RedBlackBinarySearchTreeNode<TKey, TValue> Successor()
    {
        if (Right != SearchTree.Null)
            return Right.Min();

        var parent = Parent;
        var child = this;
        while (parent != SearchTree.Null && parent.Right == child)
        {
            child = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    public RedBlackBinarySearchTreeNode<TKey, TValue> Predecessor()
    {
        if (Left != SearchTree.Null)
            return Left.Max();

        var parent = Parent;
        var child = this;
        while (parent != SearchTree.Null && parent.Left == child)
        {
            child = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    IBinarySearchTree<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Tree => SearchTree;

    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Find(TKey key)
        => Find(key);
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Min()
        => Min();
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Max()
        => Max();
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Successor()
        => Successor();
    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Predecessor()
        => Predecessor();

    public static implicit operator KeyValuePair<TKey, TValue>(RedBlackBinarySearchTreeNode<TKey, TValue> searchTreeNode)
        => new (searchTreeNode.Key, searchTreeNode.Value);
}