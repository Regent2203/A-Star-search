using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using System.Collections.Generic;

namespace ThisProject.Fields.Spatials
{
    public class SpatialFieldCore<T> : IFieldCore<T, int> where T : class, INode<int>
    {
        protected DictTypeStorage<T> _nodes;
        public IObjectsStorage<T, int> Nodes => _nodes;
        

        public SpatialFieldCore(DictTypeStorage<T> nodes)
        {
            _nodes = nodes;
        }

        public void SetNodesData(Dictionary<int, T> data) => _nodes.SetData(data);
    }
}