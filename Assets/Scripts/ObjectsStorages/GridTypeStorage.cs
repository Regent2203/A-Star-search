using ThisProject.Fields.GridNeighbours;
using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.ObjectsStorages
{
    public class GridTypeStorage<T> : IObjectsStorage<T, Vector2Int> where T : class
    {
        private T[,] _data;


        public T GetById(Vector2Int id)
        {
            if (_data.IsWithinBounds(id.x, id.y))
                return _data[id.x, id.y];

            return null;
        }

        public void SetData(T[,] data)
        {
            _data = data;
        }

        public IReadOnlyList<T> GetNeighbourObjects(Vector2Int index, IGridNeighboursProvider<T> neighboursProvider)
        {
            return neighboursProvider.GetNeighbours(index, _data);
        }
    }
}