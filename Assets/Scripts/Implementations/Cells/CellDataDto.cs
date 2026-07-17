using System;
using ThisProject.SaveSystem.Dto;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    [Serializable]
    public class CellDataDto : NodeDataDto<Vector2Int>
    {
        public CellType CellType;

        public CellDataDto(Vector2Int id, Vector2Dto nodePosition, CellType cellType) : base(id, nodePosition)
        {
            CellType = cellType;
        }
    }
}