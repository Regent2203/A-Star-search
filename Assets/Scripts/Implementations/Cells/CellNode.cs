using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellNode : NodeData<Vector2Int>
    {
        private CellType _cellType;

        public override bool IsBlocked => float.IsPositiveInfinity(_cellType.MoveCost) || _isBlocked;
        public CellType CellType => _cellType;


        public CellNode(Vector2Int index, Vector2 nodePosition, CellType cellType)
        {
            _id = index;
            _nodePosition = nodePosition;
            _cellType = cellType;
        }

        public bool TryChangeCellType(CellType cellType)
        {
            if (_cellType != cellType)
            {
                _cellType = cellType;
                return true;
            }
            return false;
        }
    }
}