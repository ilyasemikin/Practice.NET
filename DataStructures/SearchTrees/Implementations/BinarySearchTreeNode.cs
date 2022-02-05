namespace SearchTrees.Implementations;

public class BinarySearchTreeNode<TKey, TValue> : IBinarySearchTreeNode<TKey, TValue>
    where TKey : notnull
{
    public TKey Key { get; init; }
    public TValue Value { get; set; }
    public IComparer<TKey> Comparer { get; init; }

    public BinarySearchTreeNode<TKey, TValue>? Parent { get; internal set; }
    public BinarySearchTreeNode<TKey, TValue>? Left { get; internal set; }
    public BinarySearchTreeNode<TKey, TValue>? Right { get; internal set; }

    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Parent => Parent;
    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Left => Left;
    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Right => Right;
    public IBinarySearchTree<TKey, TValue> Tree { get; }
    public IBinarySearchTreeNode<TKey, TValue>? NullNode => null;

    internal BinarySearchTreeNode(TKey key, TValue value, BinarySearchTree<TKey, TValue> tree, IComparer<TKey>? comparer = null)
    {
        comparer ??= Comparer<TKey>.Default;

        Key = key;
        Value = value;
        Tree = tree;
        Comparer = comparer;
    }

    public BinarySearchTreeNode<TKey, TValue>? Find(TKey key)
    {
        var cur = this;
        while (cur is not null && Comparer.Compare(cur.Key, key) != 0)
        {
            if (Comparer.Compare(key, cur.Key) < 0)
                cur = cur.Left;
            else
                cur = cur.Right;
        }
        return cur;
    }

    public BinarySearchTreeNode<TKey, TValue> Min()
    {
        var cur = this;
        while (cur.Left is not null)
            cur = cur.Left;
        return cur;
    }

    public BinarySearchTreeNode<TKey, TValue> Max()
    {
        var cur = this;
        while (cur.Right is not null)
            cur = cur.Right;
        return cur;
    }

    public BinarySearchTreeNode<TKey, TValue>? Successor()
    {
        if (Right is not null)
            return Right.Min();

        var parent = Parent;
        var child = this;
        while (parent is not null && parent.Right == child)
        {
            child = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    public BinarySearchTreeNode<TKey, TValue>? Predecessor()
    {
        if (Left is not null)
            return Left.Max();

        var parent = Parent;
        var child = this;

        while (parent is not null && parent.Left == child)
        {
            child = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Find(TKey key)
        => Find(key);
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Min()
        => Min();
    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTreeNode<TKey, TValue>.Max()
        => Max();
    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Successor() 
        => Successor();
    IBinarySearchTreeNode<TKey, TValue>? IBinarySearchTreeNode<TKey, TValue>.Predecessor() 
        => Predecessor();

    public static implicit operator KeyValuePair<TKey, TValue>(BinarySearchTreeNode<TKey, TValue> node)
        => new (node.Key, node.Value);
}
