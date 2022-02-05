namespace Trees.Implementations;

public class BinaryTreeNode<TValue> : IBinaryTreeNode<TValue>
{
    public BinaryTreeNode<TValue>? Left { get; set; }
    public BinaryTreeNode<TValue>? Right { get; set; }
    public TValue Value { get; set; }

    IBinaryTreeNode<TValue>? IBinaryTreeNode<TValue>.Left => Left;
    IBinaryTreeNode<TValue>? IBinaryTreeNode<TValue>.Right => Right;

    public BinaryTreeNode(TValue value, BinaryTreeNode<TValue>? left = null, BinaryTreeNode<TValue>? right = null)
    {
        Value = value;
        Left = left;
        Right = right;
    }
}
