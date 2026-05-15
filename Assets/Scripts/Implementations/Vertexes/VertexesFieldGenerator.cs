using Core.Implementations.Vertexes;
using Core.Links.Providers;
using System;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class VertexesFieldGenerator
    {
        private int _newId = 0;

        private readonly VertexViewFactory _viewsFactory;
        private readonly VertexNodeFactory _nodesFactory;

        public VertexesFieldGenerator(VertexViewFactory viewFactory, VertexNodeFactory nodeFactory)
        {
            _viewsFactory = viewFactory;
            _nodesFactory = nodeFactory;
            //_config = config;
        }

        //temp
        public void Test(VertexesField field, Transform container)
        {
            _viewsFactory.SetConfiguration(Vector2.one, container);

            for (int i = 0; i < 4; i++)
            {
                var id = _newId++;

                var view = _viewsFactory.Create(Vector2.zero + UnityEngine.Random.Range(0, 1) * Vector2.one, id);
                var node = _nodesFactory.Create(Vector2.zero + UnityEngine.Random.Range(0, 1) * Vector2.one, id);

                field.AddFieldData(node, view);
            }
        }

        public void BuildNodeForField(VertexesField field, Vector3 position, Transform container, Vector2 scale, Action<CellNode, CellType> callback)
        {
            //_viewsFactory.SetConfiguration(scale, container);
            //_nodesFactory.SetConfiguration(callback);

            
                    
            var id = _newId++;

            //var view = _viewsFactory.Create(id, position);
            //var node = _nodesFactory.Create(id, position);

            //todo unsubscribe if ever needed
            /*
            field.CellNodeTypeChanged += (node, type) =>
            {
                var view = field.GetViewForNode(node);
                view.UpdateSprite(type.Sprite);
            };*/

            //field.SetFieldData(nodes, views);
        }
    }
}