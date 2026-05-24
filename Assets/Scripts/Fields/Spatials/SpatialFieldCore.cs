using ThisProject.Fields.Grids;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.Fields.Spatials
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