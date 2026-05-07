using Core.Fields;
using Core.Links;
using Core.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellNode : INode
    {
        private readonly Vector2 _position;
        private readonly Vector2Int _index;
        private CellType _cellType;
        private readonly IField<CellNode> _field;

        public Vector2 Position => _position;
        public Vector2Int Index => _index;
        public CellType CellType => _cellType;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.Weight);
        public float Weight => _cellType.Weight;

        public event Action<CellType> CellTypeChanged;


        public CellNode(Vector2 position, Vector2Int index, CellType cellType, IField<CellNode> field)
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
            CellTypeChanged?.Invoke(cellType);
        }

        public IEnumerable<ILink> GetLinks()
        {
            return _field.GetLinksForNode(this);
        }
    }
}