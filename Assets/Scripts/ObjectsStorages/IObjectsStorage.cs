using System;
using System.Collections.Generic;

namespace EasyField.ObjectsStorages
{
    public interface IObjectsStorage<T, TId>
    {
        public IEnumerable<T> AllItems { get; }

        public T GetItem(TId id);
        public bool TryGetItem(TId id, out T item);
        public void AddItem(TId id, T item);
        public void RemoveItem(TId id);
        public void ClearData();

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;
    }
}