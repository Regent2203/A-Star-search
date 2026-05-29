using ThisProject.Nodes;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThisProject.Implementations.Cells
{
    public class CellNode : INode<Vector2Int>
    {
        private readonly Vector2Int _index;
        private Vector2 _nodePosition;        
        private CellType _cellType;

        public Vector2Int Id => _index;
        public Vector2 NodePosition => _nodePosition;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.MoveCost);
        public CellType CellType => _cellType;


        public CellNode(Vector2Int index, Vector2 nodePosition, CellType cellType)
        {
            _index = index;
            _nodePosition = nodePosition;
            _cellType = cellType;
        }

        public bool ChangeType(CellType cellType)
        {
            if (_cellType != cellType)
            {
                _cellType = cellType;
                return true;
            }
            return false;
        }

        public bool Move(Vector2 position)
        {
            if (position != _nodePosition)
            {
                _nodePosition = position;
                return true;
            }
            return false;
        }
    }
}