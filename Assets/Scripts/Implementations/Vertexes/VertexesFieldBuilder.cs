using ThisProject.Fields;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesFieldBuilder
    {
        private readonly SpatialField _field;
        private readonly DictTypeStorage<VertexNode, int> _nodes;
        private readonly DictTypeStorage<VertexView, int> _views;
        private readonly VertexNodeFactory _nodesFactory;
        private readonly VertexViewPool _viewsPool;

        private int _newId = 0;


        public VertexesFieldBuilder(SpatialField field, DictTypeStorage<VertexNode, int> nodes, DictTypeStorage<VertexView, int> views, 
            VertexViewPool viewsPool, VertexNodeFactory nodeFactory)
        {
            _field = field;
            _nodes = nodes;
            _views = views;
            _viewsPool = viewsPool;
            _nodesFactory = nodeFactory;
        }

        //temp
        public void TestPopulate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var id = _newId++;

                var pos = new Vector3(UnityEngine.Random.value * 40 - 20, UnityEngine.Random.value * 40 - 20, 0);
                var node = _nodesFactory.Create(id, pos);
                var view = _viewsPool.Spawn(id, _field.ScaleFactor);
                view.Move(pos);

                _nodes.TryAddItem(id, node);
                _views.TryAddItem(id, view);
            }
        }

        //todo
        public void CreateItem(Vector3 pos)
        {
            var id = _newId++;
            
            var node = _nodesFactory.Create(id, pos);
            var view = _viewsPool.Spawn(id, _field.ScaleFactor);
            view.Move(pos);

            //_field.AddFieldData(node, view);
        }

        public void DeleteItem(int id)
        {
            //_field.RemoveFieldData(id);
        }
    }
}