using Core.Nodes;
using Core.ObjectsStorages;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Spatials
{
    public class SpatialFieldCore<T> : IField<T, int> where T : class, INode<int>
    {
        protected DictTypeStorage<T> _nodes;

        public IObjectsStorage<T, int> Nodes => _nodes;
        public event Action FieldChanged;

        public void SetNodesData(Dictionary<int, T> data) => _nodes.SetData(data);

        public SpatialFieldCore(DictTypeStorage<T> nodes)
        {
            _nodes = nodes;
        }
    }
}