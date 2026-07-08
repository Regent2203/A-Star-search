using System;
using System.Collections.Generic;
using System.Linq;
using ThisProject.GridNeighbours;
using UnityEngine;

namespace ThisProject.ObjectsStorages
{
    public class GridTypeStorage<T> : IObjectsStorage<T, Vector2Int>
    {
        private T[,] _data;
        private Vector2Int _size;

        public IEnumerable<T> AllItems => _data != null ? _data.Cast<T>() : Enumerable.Empty<T>();

        public event Action<Vector2Int> ItemAdded;
        public event Action<Vector2Int> ItemRemoved;
        

        public void Init(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), $"Incorrect size argument for grid: {size}");
            }

            _size = size;
            _data = new T[size.x, size.y];
        }
        
        public T GetItem(Vector2Int id)
        {
            ValidateStorageInitialized();
            ValidateBounds(id);

            return _data[id.x, id.y];
        }

        public void AddItem(Vector2Int id, T item)
        {
            ValidateStorageInitialized();
            ValidateBounds(id);

            if (!EqualityComparer<T>.Default.Equals(_data[id.x, id.y], default))
            {
                throw new InvalidOperationException($"Cannot add item with id {id}. Item with this id already exists.");
            }

            _data[id.x, id.y] = item;
            ItemAdded?.Invoke(id);
        }

        public void RemoveItem(Vector2Int id)
        {
            ValidateStorageInitialized();
            ValidateBounds(id);

            if (EqualityComparer<T>.Default.Equals(_data[id.x, id.y], default))
            {
                throw new InvalidOperationException($"Cannot remove item with id {id}. Item with this id does not exist.");
            }

            _data[id.x, id.y] = default;
            ItemRemoved?.Invoke(id);
        }

        public void ClearData()
        {
            if (_data == null) 
                return;

            Array.Clear(_data, 0, _data.Length);
        }

        private void ValidateStorageInitialized()
        {
            if (_data == null)
            {
                throw new InvalidOperationException($"Grid storage is not initialized. You must call Init() first.");
            }
        }

        private void ValidateBounds(Vector2Int id)
        {
            if (id.x < 0 || id.x >= _size.x || id.y < 0 || id.y >= _size.y)
            {
                throw new IndexOutOfRangeException($"Id {id} is out of grid bounds. Grid size is {_size}.");
            }
        }

        public IReadOnlyList<T> GetNeighbourObjects(Vector2Int index, IGridNeighboursProvider<T> neighboursProvider)
        {
            return neighboursProvider.GetNeighbours(index, _data);
        }
    }
}