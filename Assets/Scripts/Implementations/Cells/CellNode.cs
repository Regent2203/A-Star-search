using Core.Links;
using Core.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellNode : INode
    {
        private Vector2Int _index;
        private CellType _cellType;
        private Vector2 _position;
        private List<ILink> _links = new List<ILink>();

        public Vector2Int Index => _index;
        public CellType CellType => _cellType;
        public Vector2 Position => _position;
        public IReadOnlyList<ILink> Links => _links;
        public bool IsBlocked => float.IsPositiveInfinity(_cellType.Weight);

        public event Action<CellType> CellTypeChanged;


        public CellNode(Vector2 position, Vector2Int index, CellType cellType)
        {
            _position = position;
            _index = index;
            _cellType = cellType;
        }

        public void ChangeType(CellType cellType)
        {
            if (_cellType == cellType)
                return;

            _cellType = cellType;
            CellTypeChanged?.Invoke(cellType);
        }
    }
}