using EasyField.SaveSystem.Dto;
using System;

namespace EasyField.Implementations.Cells
{
    [Serializable]
    public class CellsFieldSaveDto : FieldSaveDto<CellDataDto>
    {
        public Vector2IntDto FieldSize = new();
    }
}