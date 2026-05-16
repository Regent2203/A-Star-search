using Core.Nodes;
using System;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellNode : INode<Vector2Int>
    {
        private readonly Vector2Int _index;
        private readonly Vector2 _nodePosition;        
        private CellType _cellType;
        private readonly Action<CellNode, CellType> _typeChangedCallback;


        public Vector2Int Id => _index;
        public Vector2 NodePosition => _nodePosition;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.MoveCost);
        public CellType CellType => _cellType;

        public event Action<Vector2> NodePositionChanged;


        public CellNode(Vector2Int index, Vector2 nodePosition, CellType cellType, Action<CellNode, CellType> typeChangedCallback)
        {
            _index = index;
            _nodePosition = nodePosition;
            _cellType = cellType;
            _typeChangedCallback = typeChangedCallback;
        }

        public void ChangeType(CellType cellType)
        {
            if (_cellType == cellType)
                return;

            _cellType = cellType;
            _typeChangedCallback?.Invoke(this, cellType);
        }
    }
}