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
}
