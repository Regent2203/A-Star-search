using System;

namespace ThisProject.ObjectsStorages
{
    public interface IObjectsStorage<T, TId>
    {
        public T GetItemById(TId id);
        public bool TryAddItem(TId id, T item);
        public bool TryRemoveItem(TId id);
        public void ClearData();

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;
    }
}