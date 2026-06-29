using System;
using System.Collections.Generic;
using ThisProject.Fields.GridNeighbours;
using UnityEngine;
using Zenject;

namespace ThisProject.ObjectsStorages
{
    public class GridTypeStorage<T> : IObjectsStorage<T, Vector2Int>
    {
        private IMemoryPool _pool;
        private T[,] _data;

        public event Action<Vector2Int> ItemAdded;
        public event Action<Vector2Int> ItemRemoved;

        /*
        [Inject]
        public GridTypeStorage(IMemoryPool pool)
        {
            _pool = pool;
        }*/
        
        public void Init(Vector2Int size)
        {
            _data = new T[size.x, size.y];
        }
        
        public T GetItemById(Vector2Int id)
        {
            if (_data.IsIndexWithinBounds(id.x, id.y))
                return _data[id.x, id.y];

            return default(T);
        }

        public bool TryAddItem(Vector2Int id, T item)
        {
            if (_data.IsIndexWithinBounds(id.x, id.y))
            {
                if (_data[id.x, id.y] == null)
                {
                    _data[id.x, id.y] = item;
                    ItemAdded?.Invoke(id);
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveItem(Vector2Int id)
        {
            if (_data.IsIndexWithinBounds(id.x, id.y))
            {
                _data[id.x, id.y] = default(T);
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

            _data = null;
        }

        public IReadOnlyList<T> GetNeighbourObjects(Vector2Int index, IGridNeighboursProvider<T> neighboursProvider)
        {
            return neighboursProvider.GetNeighbours(index, _data);
        }
    }
}