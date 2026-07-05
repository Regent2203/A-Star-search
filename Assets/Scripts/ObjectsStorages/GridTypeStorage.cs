using System;
using System.Collections.Generic;
using System.Linq;
using ThisProject.Fields.GridNeighbours;
using UnityEngine;

namespace ThisProject.ObjectsStorages
{
    public class GridTypeStorage<T> : IObjectsStorage<T, Vector2Int>
    {
        private T[,] _data;
        private Vector2Int _size;

        public IEnumerable<T> AllItems => _data.Cast<T>();

        public event Action<Vector2Int> ItemAdded;
        public event Action<Vector2Int> ItemRemoved;
        

        public void Init(Vector2Int size)
        {
            _size = size;
            _data = new T[size.x, size.y];
        }
        
        public T GetItemById(Vector2Int id)
        {
            if (_data.IsIndexWithinBounds(id.x, id.y))
                return _data[id.x, id.y];

            return default;
        }

        public bool TryAddItem(Vector2Int id, T item)
        {
            if (_data.IsIndexWithinBounds(id.x, id.y) && _data[id.x, id.y] == null)
            {
                _data[id.x, id.y] = item;
                ItemAdded?.Invoke(id);
                return true;
            }
            return false;
        }

        public bool TryRemoveItem(Vector2Int id)
        {
            if (_data.IsIndexWithinBounds(id.x, id.y) && _data[id.x, id.y] != null)
            {
                _data[id.x, id.y] = default;
                ItemRemoved?.Invoke(id);
                return true;
            }
            return false;
        }

        public void ClearData()
        {
            if (_data == null) 
                return;

            Array.Clear(_data, 0, _data.Length);
        }

        public IReadOnlyList<T> GetNeighbourObjects(Vector2Int index, IGridNeighboursProvider<T> neighboursProvider)
        {
            return neighboursProvider.GetNeighbours(index, _data);
        }
    }
}