using System;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellNodeFactory
    {
        private Action<CellNode, CellType> _nodeTypeChangedCallback;

        private readonly IInstantiator _instantiator;


        public CellNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void SetConfiguration(Action<CellNode, CellType> nodeTypeChangedCallback)
        {
            _nodeTypeChangedCallback = nodeTypeChangedCallback;
        }

        public CellNode Create(Vector2Int index, Vector2 nodePosition, CellType cellType)
        {
            var cellNode = _instantiator.Instantiate<CellNode>(new object[] { index, nodePosition, cellType, _nodeTypeChangedCallback });

            return cellNode;
        }
    }
}