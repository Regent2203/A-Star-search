using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesFieldGenerator
    {
        private VertexesField _field;

        private int _newId = 0;

        private readonly VertexViewFactory _viewsFactory;
        private readonly VertexNodeFactory _nodesFactory;

        public VertexesFieldGenerator(VertexViewFactory viewFactory, VertexNodeFactory nodeFactory)
        {
            _viewsFactory = viewFactory;
            _nodesFactory = nodeFactory;
        }

        public void SetConfiguration(VertexesField field, Transform container)
        {
            _field = field;

            _viewsFactory.SetConfiguration(Vector2.one, container);
        }

        //temp
        public void TestPopulate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var id = _newId++;

                var pos = new Vector3(UnityEngine.Random.value * 40 - 20, UnityEngine.Random.value * 40 - 20, 0);
                var view = _viewsFactory.Create(id, pos);
                var node = _nodesFactory.Create(id, pos);

                _field.AddFieldData(node, view);
            }
        }

        //todo
        public void BuildNodeForField(VertexesField field, Vector3 position, Transform container, Vector2 scale)
        {
            var id = _newId++;

            //var view = _viewsFactory.Create(id, position);
            //var node = _nodesFactory.Create(id, position);

            //todo unsubscribe if ever needed
            /*
            field.FieldChanged += (node, type) =>
            {
                var view = field.GetViewById(node);
                view.UpdateSprite(type.Sprite);
            };*/

            //field.SetFieldData(nodes, views);
        }
    }
}