using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SearchTrees.Implementations;

public class RedBlackBinarySearchTree<TKey, TValue> : IBinarySearchTree<TKey, TValue>
{
    private RedBlackBinarySearchTreeNode<TKey, TValue> _root;
    public RedBlackBinarySearchTreeNode<TKey, TValue> Root
    {
        get => _root;
        set
        {
            _root = value;
            Null.Left = Null.Right = _root;
        }
    }

    IBinarySearchTreeNode<TKey, TValue> IBinarySearchTree<TKey, TValue>.Root => Root;

    public RedBlackBinarySearchTreeNode<TKey, TValue> Null { get; }

    public IComparer<TKey> Comparer { get; init; }
    public int Count { get; private set; }
    public bool IsReadOnly => false;
    
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
    public RedBlackBinarySearchTree() 
        : this(null)
    {

    }

    public RedBlackBinarySearchTree(IComparer<TKey>? comparer)
    {
        comparer ??= Comparer<TKey>.Default;
        Comparer = comparer;

        Null = new RedBlackBinarySearchTreeNode<TKey, TValue>(default!, default!, this, comparer)
        {
            Color = RedBlackNodeColor.Black
        };

        _root = Null;
    }

    private bool TryLeftRotate(RedBlackBinarySearchTreeNode<TKey, TValue> searchTreeNode)
    {
        if (searchTreeNode.Right == Null)
            return false;
        
        var right = searchTreeNode.Right;

        searchTreeNode.Right = right.Left;
        if (searchTreeNode.Right != Null)
            searchTreeNode.Right.Parent = searchTreeNode;

        right.Parent = searchTreeNode.Parent;
        
        if (searchTreeNode.Parent == Null)
            _root = right;
        else if (searchTreeNode.Parent.Left == searchTreeNode)
            searchTreeNode.Parent.Left = right;
        else
            searchTreeNode.Parent.Right = right;
        
        right.Left = searchTreeNode;
        
        searchTreeNode.Parent = right;

        return true;
    }

    private bool TryRightRotate(RedBlackBinarySearchTreeNode<TKey, TValue> searchTreeNode)
    {
        if (searchTreeNode.Left == Null)
            return false;

        var left = searchTreeNode.Left;
        
        searchTreeNode.Left = left.Right;
        if (searchTreeNode.Left != Null)
            searchTreeNode.Left.Parent = searchTreeNode;

        left.Parent = searchTreeNode.Parent;

        if (searchTreeNode.Parent == Null)
            _root = left;
        if (searchTreeNode.Parent.Left == searchTreeNode)
            searchTreeNode.Parent.Left = left;
        else
            searchTreeNode.Parent.Right = left;

        left.Right = searchTreeNode;
        
        searchTreeNode.Parent = left;

        return true;
    }

    private void InsertFixed(RedBlackBinarySearchTreeNode<TKey, TValue> searchTreeNode)
    {
        while (searchTreeNode.Parent.Color == RedBlackNodeColor.Red)
        {
            if (searchTreeNode.Parent == searchTreeNode.Parent.Parent.Left)
            {
                var y = searchTreeNode.Parent.Parent.Right;
                if (y.Color == RedBlackNodeColor.Red)
                {
                    searchTreeNode.Parent.Color = RedBlackNodeColor.Black;
                    y.Color = RedBlackNodeColor.Black;
                    searchTreeNode.Parent.Parent.Color = RedBlackNodeColor.Red;
                    searchTreeNode = searchTreeNode.Parent.Parent;
                }
                else
                {
                    if (searchTreeNode == searchTreeNode.Parent.Right)
                    {
                        searchTreeNode = searchTreeNode.Parent;
                        TryLeftRotate(searchTreeNode);
                    }
                    
                    searchTreeNode.Parent.Color = RedBlackNodeColor.Black;
                    searchTreeNode.Parent.Parent.Color = RedBlackNodeColor.Red;
                    TryRightRotate(searchTreeNode.Parent.Parent);
                }
            }
            else
            {
                if (searchTreeNode.Parent == searchTreeNode.Parent.Parent.Right)
                {
                    var y = searchTreeNode.Parent.Parent.Left;
                    if (y.Color == RedBlackNodeColor.Red)
                    {
                        searchTreeNode.Parent.Color = RedBlackNodeColor.Black;
                        y.Color = RedBlackNodeColor.Black;
                        searchTreeNode.Parent.Parent.Color = RedBlackNodeColor.Red;
                        searchTreeNode = searchTreeNode.Parent.Parent;
                    }
                    else
                    {
                        if (searchTreeNode == searchTreeNode.Parent.Left)
                        {
                            searchTreeNode = searchTreeNode.Parent;
                            TryRightRotate(searchTreeNode);
                        }
                        searchTreeNode.Parent.Color = RedBlackNodeColor.Black;
                        searchTreeNode.Parent.Parent.Color = RedBlackNodeColor.Red;
                        TryLeftRotate(searchTreeNode.Parent.Parent);
                    }
                }
            }
        }

        Root.Color = RedBlackNodeColor.Black;
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
    
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
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
        var parent = Null;
        var cur = Root;

        while (cur != Null)
        {
            parent = cur;

            if (Comparer.Compare(key, cur.Key) < 0)
                cur = cur.Left;
            else
                cur = cur.Right;
        }

        var child = new RedBlackBinarySearchTreeNode<TKey, TValue>(key, value, this, Comparer)
        {
            Color = RedBlackNodeColor.Red,
            Parent = parent
        };

        if (parent == Null)
            Root = child;
        else if (Comparer.Compare(child.Key, parent.Key) < 0)
            parent.Left = child;
        else
            parent.Right = child;

        Count++;
        
        InsertFixed(child);
        return true;
    }
    
    public bool TryUpdate(TKey key, TValue value)
    {
        var node = Find(key);
        if (node is null)
            return false;

        node.Value = value;
        return true;
    }

    public bool Remove(TKey key)
        => TryRemove(key);

    public bool Remove(KeyValuePair<TKey, TValue> item)
        => TryRemove(item.Key);
    
    public bool TryRemove(IBinarySearchTreeNode<TKey, TValue> node)
    {
        throw new NotImplementedException();
    }
    
    public bool TryRemove(TKey key)
    {
        var node = Find(key);
        if (node is null)
            return false;
        return TryRemove(node);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        var cur = Root.Min();
        while (cur != Null)
        {
            yield return cur;
            cur = cur.Successor();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();

    public void Clear()
    {
        Root = Null;
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