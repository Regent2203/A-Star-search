using UnityEngine;

namespace Core.ObjectsStorages
{
    public class GridTypeStorage<T> : IObjectsStorage<T, Vector2Int> where T : class
    {
        protected T[,] _data;

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
    }
}