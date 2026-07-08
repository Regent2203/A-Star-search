using System;
using System.Collections.Generic;

namespace ThisProject.ObjectsStorages
{
    public interface IObjectsStorage<T, TId>
    {
        public IEnumerable<T> AllItems { get; }

        public T GetItem(TId id);
        public void AddItem(TId id, T item);
        public void RemoveItem(TId id);
        public void ClearData();

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;
    }
}