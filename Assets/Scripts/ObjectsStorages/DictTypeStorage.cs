using System.Collections.Generic;

namespace ThisProject.ObjectsStorages
{
    public class DictTypeStorage<T, TId> : IObjectsStorage<T, TId> where T : class
    {
        private Dictionary<TId, T> _data;


        public T GetById(TId id)
        {
            if (_data.TryGetValue(id, out var node))
                return node;

            return null;
        }

        public void SetData(Dictionary<TId, T> data)
        {
            _data = data;
        }

        public void ClearData()
        {
            _data = null;
        }
    }
}