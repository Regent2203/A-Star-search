using System;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellNodeFactory
    {
        private Action<CellNode, Vector2> _nodeMovedCallback;
        private Action<CellNode, CellType> _nodeTypeChangedCallback;

        private readonly IInstantiator _instantiator;


        public CellNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void SetConfiguration(Action<CellNode, Vector2> nodeMovedCallback, Action<CellNode, CellType> nodeTypeChangedCallback)
        {
            _nodeMovedCallback = nodeMovedCallback;
            _nodeTypeChangedCallback = nodeTypeChangedCallback;
        }

        public CellNode Create(Vector2Int index, Vector2 nodePosition, CellType cellType)
        {
            var cellNode = _instantiator.Instantiate<CellNode>(new object[] { index, nodePosition, cellType, _nodeMovedCallback, _nodeTypeChangedCallback });

            return cellNode;
        }
    }
}