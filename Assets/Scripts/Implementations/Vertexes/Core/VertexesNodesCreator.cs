using EasyField.Fields;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesNodesCreator
    {
        private readonly SpatialField _field;
        private readonly VertexDataStorage _nodeDatas;
        private readonly VertexViewStorage _nodeViews;
        private readonly VertexDataFactory _nodeDatasFactory;
        private readonly VertexViewFactory _nodeViewsFactory;

        private int _newId = 0;


        public VertexesNodesCreator(SpatialField field, VertexDataStorage nodeDatas, VertexViewStorage nodeViews,
            VertexDataFactory nodeDatasFactory, VertexViewFactory nodeViewsFactory)
        {
            _field = field;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _nodeDatasFactory = nodeDatasFactory;
            _nodeViewsFactory = nodeViewsFactory;

            ResetId();
        }

        public void CreateItem(int id, Vector2 pos)
        {            
            CreateItemInternal(id, pos);
            _newId = id;
        }

        public void CreateItem(Vector2 pos)
        {
            var id = ++_newId;
            CreateItemInternal(id, pos);
        }

        private void CreateItemInternal(int id, Vector2 pos)
        {
            var nodeView = _nodeViewsFactory.CreateItem(id, _field.ScaleFactor);            
            var offset = nodeView.GetSize() / 2;
            pos = pos.Clamp(_field.Box.bounds, offset);
            nodeView.Move(pos);

            var nodeData = _nodeDatasFactory.CreateItem(id, pos);            

            _nodeDatas.AddItem(id, nodeData);
            _nodeViews.AddItem(id, nodeView);
        }

        public void DeleteItem(int id)
        {
            var nodeData = _nodeDatas.GetItem(id);
            var nodeView = _nodeViews.GetItem(id);

            _nodeDatasFactory.DeleteItem(nodeData);
            _nodeViewsFactory.DeleteItem(nodeView);

            _nodeDatas.RemoveItem(id);
            _nodeViews.RemoveItem(id);
        }

        public void ClearAll()
        {
            foreach (var data in _nodeDatas.AllItems)
            {
                _nodeDatasFactory.DeleteItem(data);
            }
            _nodeDatas.ClearData();

            foreach (var view in _nodeViews.AllItems)
            {
                _nodeViewsFactory.DeleteItem(view);
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