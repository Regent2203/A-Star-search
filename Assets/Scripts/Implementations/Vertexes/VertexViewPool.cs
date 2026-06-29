using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexViewPool : MonoPoolableMemoryPool<int, Vector2, VertexView>
    {
    }
}
