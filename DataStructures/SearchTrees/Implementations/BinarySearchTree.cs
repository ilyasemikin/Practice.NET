using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SearchTrees.Implementations;

public class BinarySearchTree<TKey, TValue> : IBinarySearchTree<TKey, TValue>
    where TKey : notnull
{
    private BinarySearchTreeNode<TKey, TValue>? _root;
    
    public IBinarySearchTreeNode<TKey, TValue>? Root => _root;
    
    public int Count { get; private set; }
    public bool IsReadOnly => false;
    public IComparer<TKey> Comparer { get; init; }

    public TValue this[TKey key]
    {
        get => GetValue(key);
        set => TryUpdate(key, value);
    }

    public ICollection<TKey> Keys
        => this.Select(p => p.Key)
            .ToArray();

    public ICollection<TValue> Values
        => this.Select(p => p.Value)
            .ToArray();

    // For NUnit TestFixture purpose
    public BinarySearchTree()
        : this(null)
    {

    }

    public BinarySearchTree(IComparer<TKey>? comparer)
    {
        comparer ??= Comparer<TKey>.Default;

        Comparer = comparer;
    }

    private void Transplant(BinarySearchTreeNode<TKey, TValue> u, BinarySearchTreeNode<TKey, TValue>? v)
    {
        if (u.Parent is null)
            _root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;

        if (v is not null)
            v.Parent = u.Parent;
    }

    public KeyValuePair<TKey, TValue> Min()
    {
        if (_root is null)
            throw new InvalidOperationException("Tree empty");

        return _root.Min();
    }

    public KeyValuePair<TKey, TValue> Max()
    {
        if (_root is null)
            throw new InvalidOperationException("Tree empty");

        return _root.Max();
    }

    public IBinarySearchTreeNode<TKey, TValue>? Find(TKey key)
        => _root?.Find(key);
    
    public bool ContainsKey(TKey key)
        => Find(key) is not null;

    public bool Contains(KeyValuePair<TKey, TValue> item)
        => ContainsKey(item.Key);

    public TValue GetValue(TKey key)
    {
        // TODO: Check to null is never checked, only for compiler check purpose
        if (!TryGetValue(key, out var value) || value is null)
            throw new ArgumentException($"Node with key {key} not found");
        return value;
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)]out TValue value)
    {
        value = default;
        var node = Find(key);
        if (node is null)
            return false;

        value = node.Value;
        return true;
    }

    public void Add(TKey key, TValue value)
    {
        if (!TryInsert(key, value))
            throw new InvalidOperationException();
    }
    
    public void Add(KeyValuePair<TKey, TValue> item)
        => Add(item.Key, item.Value);

    public bool TryInsert(TKey key, TValue value)
    {
        BinarySearchTreeNode<TKey, TValue>? parent = null;
        var cur = _root;

        while (cur is not null)
        {
            parent = cur;

            if (Comparer.Compare(key, cur.Key) < 0)
                cur = cur.Left;
            else
                cur = cur.Right;
        }

        var node = new BinarySearchTreeNode<TKey, TValue>(key, value, this, Comparer)
        {
            Parent = parent
        };
        if (parent is null)
            _root = node;
        else if (Comparer.Compare(key, parent.Key) < 0)
            parent.Left = node;
        else
            parent.Right = node;

        Count++;
        return true;
    }
    
    public bool TryUpdate(TKey key, TValue value)
    {
        if (_root is null)
            return false;

        var node = _root.Find(key);
        if (node is null)
            return false;

        node.Value = value;
        return true;
    }
    
    public bool Remove(TKey key)
        => TryRemove(key);

    public bool Remove(KeyValuePair<TKey, TValue> item)
        => TryRemove(item.Key);

    public bool TryRemove(TKey key)
    {
        var node = Find(key);
        if (node is null)
            return false;

        return TryRemove(node);
    }

    public bool TryRemove(IBinarySearchTreeNode<TKey, TValue> deleted)
    {
        if (deleted.Tree != this)
            return false;

        var node = (BinarySearchTreeNode<TKey, TValue>)deleted;

        Count--;
        
        if (node.Left is null || node.Right is null)
        {
            if (node.Left is null)
                Transplant(node, node.Right);
            else
                Transplant(node, node.Left);

            return true;
        }
        
        var y = node.Right.Min();
        if (node.Right != y)
        {
            Transplant(y, y.Right);
            y.Right = node.Right;
            y.Right.Parent = y;
        }
            
        Transplant(node, y);
        y.Left = node.Left;
        y.Left.Parent = y;

        return true;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        if (_root is null)
            yield break;

        var cur = _root.Min();
        while (cur is not null)
        {
            yield return cur;
            cur = cur.Successor();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public void Clear()
    {
        _root = null;
        Count = 0;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array.Length - arrayIndex > Count)
            throw new InvalidOperationException();

        foreach (var item in this)
            array[arrayIndex++] = item;
    }
}