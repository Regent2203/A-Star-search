using System;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellNodeFactory
    {
        private Action<Vector2> _nodePositionChangedCallback;
        private Action<CellNode, CellType> _nodeTypeChangedCallback;

        private readonly IInstantiator _instantiator;


        public CellNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void SetConfiguration(Action<Vector2> nodePositionChangedCallback, Action<CellNode, CellType> nodeTypeChangedCallback)
        {
            _nodePositionChangedCallback = nodePositionChangedCallback;
            _nodeTypeChangedCallback = nodeTypeChangedCallback;
        }

        public CellNode Create(Vector2Int index, Vector2 nodePosition, CellType cellType)
        {
            var cellNode = _instantiator.Instantiate<CellNode>(new object[] { index, nodePosition, cellType, _nodePositionChangedCallback, _nodeTypeChangedCallback });

            return cellNode;
        }
    }
}