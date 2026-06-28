using System;
using System.Collections.Generic;
using Zenject;

namespace ThisProject.ObjectsStorages
{
    public class DictTypeStorage<T, TId> : IObjectsStorage<T, TId>
        where T : class//, IPoolable
    {
        private IMemoryPool _pool;
        private Dictionary<TId, T> _data = new Dictionary<TId, T>();

        public event Action<TId> ItemAdded;
        public event Action<TId> ItemRemoved;

        /*
        [Inject]
        public DictTypeStorage(IMemoryPool pool)
        {
            _pool = pool;
        }*/

        public T GetItemById(TId id)
        {
            if (_data.TryGetValue(id, out var item))
                return item;

            return null;
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
            //todo pool
            /*
            foreach (var item in _data.Values)
            {
                _pool.Despawn(item); 
            }
            */

            _data.Clear();
        }
    }
}