using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNodePool : PoolableMemoryPool<int, Vector2, VertexNode>
    {
    }
}