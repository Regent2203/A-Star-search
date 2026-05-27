using ThisProject.Nodes;
using System;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellNode : INode<Vector2Int>
    {
        private readonly Vector2Int _index;
        private Vector2 _nodePosition;        
        private CellType _cellType;
        private Action<CellNode, CellType> _nodeTypeChangedCallback;
        private Action<CellNode, Vector2> _nodeMovedCallback;

        public Vector2Int Id => _index;
        public Vector2 NodePosition => _nodePosition;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.MoveCost);
        public CellType CellType => _cellType;

        public event Action<INode, Vector2> NodePositionChanged
        {
            add => _nodeMovedCallback += value;
            remove => _nodeMovedCallback -= value;
        }


        public CellNode(Vector2Int index, Vector2 nodePosition, CellType cellType, 
            Action<CellNode, Vector2> nodeMovedCallback, Action<CellNode, CellType> nodeTypeChangedCallback)
        {
            _index = index;
            _nodePosition = nodePosition;
            _cellType = cellType;
            _nodeMovedCallback = nodeMovedCallback;
            _nodeTypeChangedCallback = nodeTypeChangedCallback;
        }

        public void ChangeType(CellType cellType)
        {
            if (_cellType == cellType)
                return;

            _cellType = cellType;
            _nodeTypeChangedCallback?.Invoke(this, cellType);
        }

        public void Move(Vector2 position)
        {
            if (position == _nodePosition)
                return;

            _nodePosition = position;
            _nodeMovedCallback?.Invoke(this, _nodePosition);
        }
    }
}