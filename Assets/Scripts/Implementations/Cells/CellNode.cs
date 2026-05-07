using Core.Links;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellNode : INode<CellNode>
    {
        private readonly Vector2 _position;
        private readonly Vector2Int _index;
        private CellType _cellType;
        private readonly CellsGridField _field;

        public Vector2 Position => _position;
        public Vector2Int Index => _index;
        public CellType CellType => _cellType;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.Weight);
        public float Weight => _cellType.Weight;


        public CellNode(Vector2 position, Vector2Int index, CellType cellType, CellsGridField field)
        {
            _position = position;
            _index = index;
            _cellType = cellType;
            _field = field;
        }

        public void ChangeType(CellType cellType)
        {
            if (_cellType == cellType)
                return;

            _cellType = cellType;
            _field.NotifyNodeTypeChanged(this, cellType);
        }

        public IEnumerable<ILink<CellNode>> GetLinks()
        {
            return _field.GetLinksForNode(this);
        }
    }
}