namespace Linked_List;

public sealed class ConnectedListLoop<T>
  {
    internal ConnectedList<T> list;
    internal ConnectedListLoop<T> next;
    internal ConnectedListLoop<T> prev;
    internal T item;
    
    internal ConnectedListLoop(ConnectedList<T> list, T value)
    {
      this.list = list;
      item = value;
    }

    public ConnectedListLoop<T>? Next => next != null && next != list.head ? next : null;
    public ConnectedListLoop<T>? Previous => prev != null && this != list.head ? prev : null;
    
    public T Value
    {
      get => item;
    }

    internal void Invalidate()
    {
      list =  null;
      next =  null;
      prev =  null;
    }
  }
  