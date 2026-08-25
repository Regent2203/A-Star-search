using EasyField.Implementations.Links;
using EasyField.SaveSystem.Dto;
using System;
using UnityEngine;

namespace EasyField.Implementations.Cells.DynamicCells
{
    [Serializable]
    public class DynamicCellsFieldSaveDto : FieldSaveDto<CellDataDto, LinkDataDto<Vector2Int>>
    {
        public Vector2IntDto FieldSize = new();
    }
}