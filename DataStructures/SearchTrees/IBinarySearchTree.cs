namespace SearchTrees;

public interface IBinarySearchTree<TKey, TValue> : IDictionary<TKey, TValue>
{
    IBinarySearchTreeNode<TKey, TValue>? Root { get; }

    KeyValuePair<TKey, TValue> Min();
    KeyValuePair<TKey, TValue> Max();
    IBinarySearchTreeNode<TKey, TValue>? Find(TKey key);

    TValue GetValue(TKey key);
    new bool TryGetValue(TKey key, out TValue? value);

    bool TryInsert(TKey key, TValue value);
    bool TryUpdate(TKey key, TValue value);
    bool TryRemove(TKey key);
    bool TryRemove(IBinarySearchTreeNode<TKey, TValue> node);
}
