using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Vertexes
{
    public class VertexViewPool : MonoPoolableMemoryPool<int, Vector2, VertexView>
    {
    }
}
