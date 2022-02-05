namespace Trees;

public interface IBinaryTreeNode<TValue>
{
    public IBinaryTreeNode<TValue>? Left { get; }
    public IBinaryTreeNode<TValue>? Right { get; }
    public TValue Value { get; set; }
}
