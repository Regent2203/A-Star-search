using ThisProject.Implementations.Cells;
using System;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexViewFactory
    {
        //todo
        private Action<int, Vector2> _nodeDragBeginCallback;
        private Action<int, Vector2> _nodeDragEndCallback;


        private Vector2 _scaleFactor;
        private Transform _container;

        private readonly IInstantiator _instantiator;
        private readonly VertexView _vertexPrefab;


        public VertexViewFactory(IInstantiator instantiator, VertexView prefab)
        {
            _instantiator = instantiator;
            _vertexPrefab = prefab;
        }

        public void SetConfiguration(Vector2 scaleFactor, Transform container, 
            Action<int, Vector2> nodeDragBeginCallback, Action<int, Vector2> nodeDragEndCallback)
        {
            _scaleFactor = scaleFactor;
            _container = container;

            _nodeDragBeginCallback = nodeDragBeginCallback;
            _nodeDragEndCallback = nodeDragEndCallback;
        }

        public VertexView Create(int id, Vector3 position)
        {
            var vertexView = _instantiator.InstantiatePrefabForComponent<VertexView>(_vertexPrefab, position, Quaternion.identity, _container);
            vertexView.Init(id, position, _nodeDragBeginCallback, _nodeDragEndCallback);

            return vertexView;
        }
    }
}
