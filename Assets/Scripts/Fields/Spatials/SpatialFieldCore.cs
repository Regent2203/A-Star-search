using Core.Fields.Grids;
using Core.Nodes;
using Core.ObjectsStorages;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Spatials
{
    public class SpatialFieldCore<T> : FieldCore<T, int> where T : class, INode<int>
    {
        protected DictTypeStorage<T> _nodes;
        public override IObjectsStorage<T, int> Nodes => _nodes;
        

        public SpatialFieldCore(DictTypeStorage<T> nodes)
        {
            _nodes = nodes;
        }

        public void SetNodesData(Dictionary<int, T> data) => _nodes.SetData(data);
    }
}