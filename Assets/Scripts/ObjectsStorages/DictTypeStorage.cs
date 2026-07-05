using System;
using System.Collections.Generic;

namespace ThisProject.ObjectsStorages
{
    public class DictTypeStorage<T, TId> : IObjectsStorage<T, TId>
    {
        private Dictionary<TId, T> _data = new Dictionary<TId, T>();

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;


        public T GetItemById(TId id)
        {
            if (_data.TryGetValue(id, out var item))
                return item;

            return default(T);
        }

        public bool TryAddItem(TId id, T item)
        {
            if (_data.TryAdd(id, item))
            {
                ItemAdded?.Invoke(id);
                return true;
            }
            return false;
        }

        public bool TryRemoveItem(TId id)
        {
            if (_data.Remove(id))
            {
                ItemRemoved?.Invoke(id);
                return true;
            }
            return false;
        }

        public void ClearData()
        {
            _data.Clear();
        }
    }
}