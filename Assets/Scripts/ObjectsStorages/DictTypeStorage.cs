using System;
using System.Collections.Generic;
using System.Linq;

namespace ThisProject.ObjectsStorages
{
    public class DictTypeStorage<T, TId> : IObjectsStorage<T, TId>
    {
        private readonly Dictionary<TId, T> _data = new Dictionary<TId, T>();

        public IEnumerable<T> AllItems => _data.Values;

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;


        public T GetItem(TId id)
        {
            if (_data.TryGetValue(id, out var item))
                return item;

            throw new KeyNotFoundException($"Cannot get item with id {id}. Item with this id does not exist.");
        }

        public void AddItem(TId id, T item)
        {
            if (!_data.TryAdd(id, item))
            {
                throw new ArgumentException($"Cannot add item with id {id}. Item with this id already exists.");
            }

            ItemAdded?.Invoke(id);
        }

        public void RemoveItem(TId id)
        {
            if (!_data.Remove(id))
            {
                throw new KeyNotFoundException($"Cannot remove item with id {id}. Item with this id does not exist.");
            }

            ItemRemoved?.Invoke(id);
        }

        public void ClearData()
        {
            var keys = _data.Keys.ToArray();
            _data.Clear();

            foreach (var id in keys)
            {
                ItemRemoved?.Invoke(id);
            }
        }
    }
}