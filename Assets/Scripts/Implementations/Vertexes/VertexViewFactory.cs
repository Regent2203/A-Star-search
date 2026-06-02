using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexViewFactory
    {
        private Vector2 _scaleFactor;
        private Transform _container;

        private readonly IInstantiator _instantiator;
        private readonly VertexView _vertexPrefab;


        public VertexViewFactory(IInstantiator instantiator, VertexView prefab)
        {
            _instantiator = instantiator;
            _vertexPrefab = prefab;
        }

        public void SetConfiguration(Vector2 scaleFactor, Transform container)
        {
            _scaleFactor = scaleFactor;
            _container = container;
        }

        public VertexView Create(int id, Vector3 position)
        {
            var vertexView = _instantiator.InstantiatePrefabForComponent<VertexView>(_vertexPrefab, position, Quaternion.identity, _container);
            vertexView.Init(id, position);

            return vertexView;
        }
    }
}
