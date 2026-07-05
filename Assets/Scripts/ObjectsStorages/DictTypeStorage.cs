using System;
using System.Collections.Generic;

namespace ThisProject.ObjectsStorages
{
    public class DictTypeStorage<T, TId> : IObjectsStorage<T, TId>
    {
        private readonly Dictionary<TId, T> _data = new Dictionary<TId, T>();

        public IEnumerable<T> AllItems => _data.Values;

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;


        public T GetItemById(TId id)
        {
            if (_data.TryGetValue(id, out var item))
                return item;

            return default;
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