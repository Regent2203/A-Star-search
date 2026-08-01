using ThisProject.Fields;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesNodesBuilder
    {
        private readonly SpatialField _field;
        private readonly DictTypeStorage<VertexData, int> _nodeDatas;
        private readonly DictTypeStorage<VertexView, int> _nodeViews;
        private readonly VertexDataPool _nodeDatasPool;
        private readonly VertexViewPool _nodeViewsPool;

        private int _newId = 0;


        public VertexesNodesBuilder(SpatialField field, DictTypeStorage<VertexData, int> nodeDatas, DictTypeStorage<VertexView, int> nodeViews,
             VertexDataPool nodeDatasPool, VertexViewPool nodeViewsPool)
        {
            _field = field;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _nodeDatasPool = nodeDatasPool;
            _nodeViewsPool = nodeViewsPool;

            ResetId();
        }

        public void CreateItem(int id, Vector3 pos)
        {            
            CreateItemInternal(id, pos);
            _newId = id;
        }

        public void CreateItem(Vector3 pos)
        {
            var id = _newId++;
            CreateItemInternal(id, pos);
        }

        private void CreateItemInternal(int id, Vector3 pos)
        {
            var nodeData = _nodeDatasPool.Spawn(id, pos);
            var nodeView = _nodeViewsPool.Spawn(id, _field.ScaleFactor);
            nodeView.Move(pos);

            _nodeDatas.AddItem(id, nodeData);
            _nodeViews.AddItem(id, nodeView);
        }        

        public void DeleteItem(int id)
        {
            var nodeData = _nodeDatas.GetItem(id);
            var nodeView = _nodeViews.GetItem(id);

            _nodeDatasPool.Despawn(nodeData);
            _nodeViewsPool.Despawn(nodeView);

            _nodeDatas.RemoveItem(id);
            _nodeViews.RemoveItem(id);
        }

        public void ClearAll()
        {
            foreach (var data in _nodeDatas.AllItems)
            {
                _nodeDatasPool.Despawn(data);
            }
            _nodeDatas.ClearData();

            foreach (var view in _nodeViews.AllItems)
            {
                _nodeViewsPool.Despawn(view);
            }
            _nodeViews.ClearData();

            ResetId();
        }

        private void ResetId()
        {
            _newId = 0;
        }
    }
}