using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Fields.Grids
{
    public class GridFieldCore<T> : IFieldCore<T, Vector2Int> where T : class, INode<Vector2Int>
    {
        protected GridTypeStorage<T> _nodes;
        public IObjectsStorage<T, Vector2Int> Nodes => _nodes;


        public GridFieldCore(GridTypeStorage<T> nodes) 
        {
            _nodes = nodes; 
        }

        public void SetNodesData(T[,] data) => _nodes.SetData(data);
    }
}