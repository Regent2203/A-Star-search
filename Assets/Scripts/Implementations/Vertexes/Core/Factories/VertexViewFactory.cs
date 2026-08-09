using UnityEngine;

namespace EasyField.Implementations.Vertexes
{
    public class VertexViewFactory
    {
        private readonly VertexViewPool _vertexViewsPool;

        public VertexViewFactory(VertexViewPool vertexViewsPool)
        {
            _vertexViewsPool = vertexViewsPool;
        }

        public VertexView CreateItem(int id, Vector2 pos)
        {
            var vertexView = _vertexViewsPool.Spawn(id, pos);

            return vertexView;
        }

        public void DeleteItem(VertexView item)
        {
            _vertexViewsPool.Despawn(item);
        }
    }
}