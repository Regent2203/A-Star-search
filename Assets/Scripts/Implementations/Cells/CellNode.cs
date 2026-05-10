using Core.Nodes;
using System;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellNode : INode<Vector2Int>
    {
        private readonly Vector2Int _index;
        private readonly Vector2 _position;        
        private CellType _cellType;

        private readonly Action<CellNode, CellType> _typeChangedCallback;

        public Vector2 NodePosition => _position;
        public Vector2Int Id => _index;
        public CellType CellType => _cellType;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.Weight);


        public CellNode(Vector2 position, Vector2Int index, CellType cellType, Action<CellNode, CellType> typeChangedCallback)
        {
            _position = position;
            _index = index;
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