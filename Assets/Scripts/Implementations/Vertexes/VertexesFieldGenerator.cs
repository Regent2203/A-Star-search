using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesFieldGenerator
    {
        private readonly VertexesField _field;
        private DictTypeStorage<VertexNode, int> _nodes;
        private DictTypeStorage<VertexView, int> _views;
        private readonly VertexNodeFactory _nodesFactory;
        private readonly VertexViewFactory _viewsFactory;

        private int _newId = 0;


        public VertexesFieldGenerator(DictTypeStorage<VertexNode, int> nodes, DictTypeStorage<VertexView, int> views, 
            VertexViewFactory viewFactory, VertexNodeFactory nodeFactory)
        {
            _nodes = nodes;
            _views = views;
            _viewsFactory = viewFactory;
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
                var view = _viewsFactory.Create(id, pos);

                _nodes.TryAddItem(id, node);
                _views.TryAddItem(id, view);
            }
        }

        //todo
        public void CreateFieldItem(Vector3 pos)
        {
            var id = _newId++;

            var view = _viewsFactory.Create(id, pos);
            var node = _nodesFactory.Create(id, pos);

            //_field.AddFieldData(node, view);
        }

        public void DeleteFieldItem(int id)
        {
            //_field.RemoveFieldData(id);
        }
    }
}