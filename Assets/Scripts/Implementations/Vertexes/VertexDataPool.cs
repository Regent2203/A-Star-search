using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexDataPool : PoolableMemoryPool<int, Vector2, VertexData>
    {
    }
}