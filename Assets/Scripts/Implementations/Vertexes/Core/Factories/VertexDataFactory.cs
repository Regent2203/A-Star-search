using UnityEngine;

namespace EasyField.Implementations.Vertexes
{
    public class VertexDataFactory
    {
        private readonly VertexDataPool _vertexDatasPool;

        public VertexDataFactory(VertexDataPool vertexDatasPool)
        {
            _vertexDatasPool = vertexDatasPool;
        }

        public VertexData CreateItem(int id, Vector2 scaleFactor)
        {
            var vertexData = _vertexDatasPool.Spawn(id, scaleFactor);

            return vertexData;
        }

        public void DeleteItem(VertexData item)
        {
            _vertexDatasPool.Despawn(item);
        }
    }
}