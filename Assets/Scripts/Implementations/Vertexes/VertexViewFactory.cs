using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly VertexView _vertexPrefab;


        public VertexViewFactory(IInstantiator instantiator, VertexView prefab)
        {
            _instantiator = instantiator;
            _vertexPrefab = prefab;
        }

        public VertexView Create(int id, Vector3 position, Vector2 scaleFactor, Transform container)
        {
            var vertexView = _instantiator.InstantiatePrefabForComponent<VertexView>(_vertexPrefab, position, Quaternion.identity, container);
            vertexView.Init(id, scaleFactor);

            return vertexView;
        }
    }
}
