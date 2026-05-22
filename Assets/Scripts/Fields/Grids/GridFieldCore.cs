using Core.Nodes;
using Core.ObjectsStorages;
using System;
using UnityEngine;

namespace Core.Fields.Grids
{
    public class GridFieldCore<T> : IField<T, Vector2Int> where T : class, INode<Vector2Int>
    {
        protected GridTypeStorage<T> _nodes;

        public IObjectsStorage<T, Vector2Int> Nodes => _nodes;
        public event Action FieldChanged;

        public void SetNodesData(T[,] data) => _nodes.SetData(data);

        public GridFieldCore(GridTypeStorage<T> nodes) 
        { 
            _nodes = nodes; 
        }
    }
}