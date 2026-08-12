using EasyField.Nodes.Dto;
using EasyField.SaveSystem.Dto;
using System;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    [Serializable]
    public class CellDataDto : NodeDataDto<Vector2Int>
    {
        public CellTypeId CellType;

        public CellDataDto(Vector2Int id, Vector2Dto nodePosition, CellType cellType) : base(id, nodePosition)
        {
            CellType = cellType.Id;
        }
    }
}