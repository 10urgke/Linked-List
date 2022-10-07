using System.Collections;
using System.Runtime.Serialization;

namespace Linked_List;

public class ConnectedList<T> : ICollection<T>
{

    #nullable disable
    internal ConnectedListLoop<T> head;
    internal int count;
    internal int Version;
    private SerializationInfo _siInfo;
    
    public int Count => count;

    
    public ConnectedListLoop<T>? First => head;


    public ConnectedListLoop<T>? Last => head != null ? head.prev :  null;
    
    bool ICollection<T>.IsReadOnly => false;

    void ICollection<T>.Add(T value) => AddLast(value);



    public ConnectedListLoop<T> AddAfter(ConnectedListLoop<T> loop, T value)
    {
      ValidateNode(loop);
      ConnectedListLoop<T> newLoop = new ConnectedListLoop<T>(loop.list, value);
      InternalInsertNodeBefore(loop.next, newLoop);
      return newLoop;
    }
    public ConnectedListLoop<T> AddBefore(ConnectedListLoop<T> loop, T value)
    {
      ValidateNode(loop);
      ConnectedListLoop<T> newLoop = new ConnectedListLoop<T>(loop.list, value);
      InternalInsertNodeBefore(loop, newLoop);
      if (loop == head)
        head = newLoop;
      return newLoop;
    }
    
    public void AddBefore(ConnectedListLoop<T> loop, ConnectedListLoop<T> newLoop)
    {
      ValidateNode(loop);
      ValidateNewNode(newLoop);
      InternalInsertNodeBefore(loop, newLoop);
      newLoop.list = this;
      if (loop != head)
        return;
      head = newLoop;
    }
    
    public ConnectedListLoop<T> AddFirst(T value)
    {
      ConnectedListLoop<T> newLoop = new ConnectedListLoop<T>(this, value);
      if (head == null)
      {
        InternalInsertNodeToEmptyList(newLoop);
      }
      else
      {
        InternalInsertNodeBefore(head, newLoop);
        head = newLoop;
      }
      return newLoop;
    }

    public ConnectedListLoop<T> AddLast(T value)
    {
      ConnectedListLoop<T> newLoop = new ConnectedListLoop<T>(this, value);
      if (head == null)
        InternalInsertNodeToEmptyList(newLoop);
      else
        InternalInsertNodeBefore(head, newLoop);
      return newLoop;
    }
    
    public void Clear()
    {
      ConnectedListLoop<T> connectedListNode1 = head;
      while (connectedListNode1 != null)
      {
        ConnectedListLoop<T> connectedListNode2 = connectedListNode1;
        connectedListNode1 = connectedListNode1.Next;
        connectedListNode2.Invalidate();
      }
      head = null;
      count = 0;
      ++Version;
    }

    
    public bool Contains(T value) => Find(value) != null;
    
    public void CopyTo(T[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (index < 0)
        throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");
      if (index > array.Length)
        throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");
      if (array.Length - index < Count)
        throw new ArgumentException("ArgumentException");
      ConnectedListLoop<T> connectedListLoop = this.head;
      if (connectedListLoop == null)
        return;
      do
      {
        array[index++] = connectedListLoop.item;
        connectedListLoop = connectedListLoop.next;
      }
      while (connectedListLoop != head);
    }

   
    public ConnectedListLoop<T>? Find(T value)
    {
      ConnectedListLoop<T> connectedListLoop = head;
      EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
      if (connectedListLoop != null)
      {
        if ((object) value != null)
        {
          while (!equalityComparer.Equals(connectedListLoop.item, value))
          {
            connectedListLoop = connectedListLoop.next;
            if (connectedListLoop == head)
              goto label_8;
          }
          return connectedListLoop;
        }
        while ((object) connectedListLoop.item != null)
        {
          connectedListLoop = connectedListLoop.next;
          if (connectedListLoop == head)
            goto label_8;
        }
        return connectedListLoop;
      } label_8:
      return null;
    }

    public Enumerator GetEnumerator() => new (this);

    
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    
    public bool Remove(T value)
    {
      ConnectedListLoop<T> loop = Find(value);
      if (loop == null)
        return false;
      InternalRemoveNode(loop);
      return true;
    }
    
    public void RemoveFirst()
    {
      if (head == null)
        throw new InvalidOperationException("InvalidOperationException");
      InternalRemoveNode(head);
    }
    
    public void RemoveLast()
    {
      if (head == null)
        throw new InvalidOperationException("InvalidOperationException");
      InternalRemoveNode(head.prev);
    }

#nullable disable
    private void InternalInsertNodeBefore(ConnectedListLoop<T> loop, ConnectedListLoop<T> newLoop)
    {
      newLoop.next = loop;
      newLoop.prev = loop.prev;
      loop.prev.next = newLoop;
      loop.prev = newLoop;
      ++Version;
      ++count;
    }

    private void InternalInsertNodeToEmptyList(ConnectedListLoop<T> newLoop)
    {
      newLoop.next = newLoop;
      newLoop.prev = newLoop;
      head = newLoop;
      ++Version;
      ++count;
    }

    internal void InternalRemoveNode(ConnectedListLoop<T> loop)
    {
      if (loop.next == loop)
      {
        head = null;
      }
      else
      {
        loop.next.prev = loop.prev;
        loop.prev.next = loop.next;
        if (head == loop)
          head = loop.next;
      }
      loop.Invalidate();
      --count;
      ++Version;
    }

    internal void ValidateNewNode(ConnectedListLoop<T> loop)
    {
      if (loop == null)
        throw new ArgumentNullException(nameof (loop));
      if (loop.list != null)
        throw new InvalidOperationException("InvalidOperationException");
    }

    internal void ValidateNode(ConnectedListLoop<T> loop)
    {
      if (loop == null)
        throw new ArgumentNullException(nameof (loop));
      if (loop.list != this)
        throw new InvalidOperationException("InvalidOperationException");
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public struct Enumerator : 
      IEnumerator<T>
    {

      #nullable disable
      private readonly ConnectedList<T> _list;
      private ConnectedListLoop<T> _loop;
      private readonly int _version;
      private T _current;
      private int _index;

      internal Enumerator(ConnectedList<T> list)
      {
      _list = list;
      _version = list.Version;
      _loop = list.head;
      _current = default (T);
      _index = 0;
      }
      
      public T Current => _current;

      object? IEnumerator.Current
      {
        get
        {
          if (_index == 0 || _index == _list.Count + 1)
            throw new InvalidOperationException("InvalidOperationException");
          return (object) this.Current;
        }
      }

      public bool MoveNext()
      {
        if (_version != _list.Version)
          throw new InvalidOperationException("InvalidOperationException");
        if (_loop == null)
        {
          _index = _list.Count + 1;
          return false;
        }
        ++_index;
        _current = _loop.item;
        _loop = _loop.next;
        if (_loop == _list.head)
          _loop = null;
        return true;
      }
      
      void IEnumerator.Reset()
      {
        if (_version != _list.Version)
          throw new InvalidOperationException("InvalidOperationException");
        _current = default;
        _loop = _list.head;
        _index = 0;
      }
      
      public void Dispose()
      {
      }
      
    }
  }