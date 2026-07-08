using ThisProject.Fields;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesFieldBuilder
    {
        private readonly SpatialField _field;
        private readonly DictTypeStorage<VertexNode, int> _nodes;
        private readonly DictTypeStorage<VertexView, int> _views;
        private readonly VertexNodePool _nodesPool;
        private readonly VertexViewPool _viewsPool;

        private int _newId = 0;


        public VertexesFieldBuilder(SpatialField field, DictTypeStorage<VertexNode, int> nodes, DictTypeStorage<VertexView, int> views, 
            VertexViewPool viewsPool, VertexNodePool nodesPool)
        {
            _field = field;
            _nodes = nodes;
            _views = views;
            _viewsPool = viewsPool;
            _nodesPool = nodesPool;
        }

        //temp
        public void TestPopulate(int count)
        {
            return;

            for (int i = 0; i < count; i++)
            {
                var id = _newId++;

                var pos = new Vector3(UnityEngine.Random.value * 40 - 20, UnityEngine.Random.value * 40 - 20, 0);

                var node = _nodesPool.Spawn(id, pos);
                var view = _viewsPool.Spawn(id, _field.ScaleFactor);
                view.Move(pos);

                _nodes.AddItem(id, node);
                _views.AddItem(id, view);
            }
        }

        public void BuildFromDto(FieldSaveDTO<int> data)
        {
            //todo field clear
            //vertexNodePool.Despawn(node);

            foreach (var item in data.Nodes)
            {
                var id = item.Id;

                var pos = item.NodePosition.ToVector2();

                var node = _nodesPool.Spawn(id, pos);
                var view = _viewsPool.Spawn(id, _field.ScaleFactor);
                view.Move(pos);

                _nodes.AddItem(id, node);
                _views.AddItem(id, view);
            }
        }

        //todo
        public void CreateItem(Vector3 pos)
        {
            var id = _newId++;
            
            var node = _nodesPool.Spawn(id, pos);
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