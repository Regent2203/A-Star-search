using System;
using ThisProject.Fields;
using ThisProject.Fields.FieldBuilders;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem;
using ThisProject.SaveSystem.Dto;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesFieldBuilder //: IFieldBuilder
    {
        private readonly SpatialField _field;
        private readonly DictTypeStorage<VertexData, int> _nodes;
        private readonly DictTypeStorage<VertexView, int> _views;
        private readonly VertexDataPool _nodesPool;
        private readonly VertexViewPool _viewsPool;

        private int _newId = 0;


        public VertexesFieldBuilder(SpatialField field, DictTypeStorage<VertexData, int> nodes, DictTypeStorage<VertexView, int> views, 
            VertexViewPool viewsPool, VertexDataPool nodesPool)
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

        public void BuildFromDto(FieldSaveDto<VertexDataDto, LinkDataDto> data)
        {
            //todo field clear
            //vertexNodePool.Despawn(node);

            foreach (var item in data.Nodes)
            {
                var id = item.Id;

                var pos = (Vector2)item.NodePosition;

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