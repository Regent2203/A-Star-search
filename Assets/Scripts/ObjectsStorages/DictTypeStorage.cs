using System.Collections.Generic;

namespace ThisProject.ObjectsStorages
{
    public class DictTypeStorage<T> : IObjectsStorage<T, int> where T : class
    {
        private Dictionary<int, T> _data;


        public T GetById(int id)
        {
            if (_data.TryGetValue(id, out var node))
                return node;

            return null;
        }

        public void SetData(Dictionary<int, T> data)
        {
            _data = data;
        }
    }
}